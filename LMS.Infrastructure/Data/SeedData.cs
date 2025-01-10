using Bogus;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LMS.Infrastructure.Data;

public static class SeedData
{
    private static UserManager<ApplicationUser> userManager = null!;
    private static RoleManager<IdentityRole> roleManager = null!;
    private const string teacherRole = "Teacher";
    private const string studentRole = "Student";

    public static async Task SeedDataAsync(this IApplicationBuilder builder)
    {
        using (var scope = builder.ApplicationServices.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var db = serviceProvider.GetRequiredService<LmsContext>();

            if (await db.Users.AnyAsync()) return;

            userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>() ??
                throw new ArgumentNullException(nameof(userManager));
            roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>() ??
                throw new ArgumentNullException(nameof(roleManager));

            try
            {
                // Create roles
                await CreateRolesAsync([teacherRole, studentRole]);

                // Generate teachers and students
                await GenerateTeachersAsync(2);  // Generate 2 teachers
                await GenerateStudentsAsync(10); // Generate 10 students

                // Generate courses with modules and activities
                await GenerateCoursesAsync(db);

                await db.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    private static async Task CreateRolesAsync(string[] roleNames)
    {
        foreach (var roleName in roleNames)
        {
            if (await roleManager.RoleExistsAsync(roleName)) continue;
            var role = new IdentityRole { Name = roleName };
            var result = await roleManager.CreateAsync(role);
            if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
        }
    }

    private static async Task GenerateTeachersAsync(int nrOfTeachers)
    {
        var faker = new Faker<ApplicationUser>("sv").Rules((f, e) =>
        {
            e.Email = f.Internet.Email(provider: "lexicon.se"); // Teachers get lexicon.se emails
            e.UserName = e.Email;
        });

        var teachers = faker.Generate(nrOfTeachers);
        var passWord = "BytMig123!";

        if (string.IsNullOrEmpty(passWord))
            throw new Exception("Password not found");

        foreach (var teacher in teachers)
        {
            var result = await userManager.CreateAsync(teacher, passWord);
            if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));

            // Assign teacher role
            await userManager.AddToRoleAsync(teacher, teacherRole);
        }
    }

    private static async Task GenerateStudentsAsync(int nrOfStudents)
    {
        var faker = new Faker<ApplicationUser>("sv").Rules((f, e) =>
        {
            e.Email = f.Internet.Email();
            e.UserName = e.Email;
        });

        var students = faker.Generate(nrOfStudents);
        var passWord = "BytMig123!";

        if (string.IsNullOrEmpty(passWord))
            throw new Exception("Password not found");

        foreach (var student in students)
        {
            var result = await userManager.CreateAsync(student, passWord);
            if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));

            // Assign student role
            await userManager.AddToRoleAsync(student, studentRole);
        }
    }
    // method to generate courses, modules and activities
    private static async Task GenerateCoursesAsync(LmsContext db)
    {
        if (await db.Courses.AnyAsync()) return;

        // First, get all students to assign to courses
        var students = await userManager.GetUsersInRoleAsync(studentRole);
        var studentGroups = students.Chunk(5).ToList(); // Split students into groups of 5

        // Create activity types
        var activityTypes = new[]
        {
        new ActivityType { Type = "E-Learning", Deadlines = "Submit within 24h" },
        new ActivityType { Type = "Lecture", Deadlines = "Attendance required" },
        new ActivityType { Type = "Assignment", Deadlines = "Submit by end date" },
        new ActivityType { Type = "Workshop", Deadlines = "Attendance required" },
        new ActivityType { Type = "Exercise", Deadlines = "Complete by end date" }
    };
        await db.ActivityTypes.AddRangeAsync(activityTypes);

        // Create courses
        var coursesFaker = new Faker<Course>()
            .RuleFor(c => c.Name, f => f.PickRandomParam([".NET Development 2024", "JavaScript Development 2024"]))
            .RuleFor(c => c.Description, f => f.Lorem.Paragraph())
            .RuleFor(c => c.StartDate, f => f.Date.Future(0, DateTime.Now.AddDays(30)));

        var courses = coursesFaker.Generate(2);

        // For each course, create modules
        foreach (var course in courses)
        {
            var moduleStartDate = course.StartDate;

            // Create 4 modules per course
            for (int i = 0; i < 4; i++)
            {
                var module = new Module
                {
                    Name = GetModuleName(course.Name, i),
                    Description = new Faker().Lorem.Paragraph(),
                    StartDate = moduleStartDate,
                    EndDate = moduleStartDate.AddDays(30),
                    Course = course,
                    Activities = new List<Activity>()
                };

                // Create 5 activities per module
                for (int j = 0; j < 5; j++)
                {
                    var activityType = activityTypes[j % activityTypes.Length];
                    var activity = new Activity
                    {
                        Name = GetActivityName(module.Name, j),
                        Description = new Faker().Lorem.Paragraph(),
                        StartDate = moduleStartDate.AddDays(j * 5),
                        EndDate = moduleStartDate.AddDays((j * 5) + 4),
                        Module = module,
                        ActivityType = activityType
                    };

                    module.Activities.Add(activity);
                }

                course.Modules.Add(module);
                moduleStartDate = module.EndDate.AddDays(1); // Next module starts after previous ends
            }

            // Assign students to the course
            var courseStudents = studentGroups[courses.IndexOf(course)];
            course.Users = courseStudents.ToList();
        }

        await db.Courses.AddRangeAsync(courses);
        await db.SaveChangesAsync();
    }

    private static string GetModuleName(string courseName, int index)
    {
        if (courseName.Contains(".NET"))
        {
            return index switch
            {
                0 => "C# Fundamentals",
                1 => "Database Development",
                2 => "Backend Development",
                3 => "Frontend Development",
                _ => $"Module {index + 1}"
            };
        }
        else // JavaScript course
        {
            return index switch
            {
                0 => "JavaScript Basics",
                1 => "Advanced JavaScript",
                2 => "React Fundamentals",
                3 => "Full Stack JavaScript",
                _ => $"Module {index + 1}"
            };
        }
    }

    private static string GetActivityName(string moduleName, int index)
    {
        return index switch
        {
            0 => $"{moduleName} - Introduction Lecture",
            1 => $"{moduleName} - E-Learning Session",
            2 => $"{moduleName} - Workshop",
            3 => $"{moduleName} - Practice Exercise",
            4 => $"{moduleName} - Final Assignment",
            _ => $"Activity {index + 1}"
        };
    }
}