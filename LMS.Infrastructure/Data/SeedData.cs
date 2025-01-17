using Bogus;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Transactions;
namespace LMS.Infrastructure.Data;

public static class SeedData
{
    private const string teacherRole = "Teacher";
    private const string studentRole = "Student";

    public static async Task SeedDataAsync(this IApplicationBuilder builder)
    {
        using var scope = builder.ApplicationServices.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        // Get services including logger
        var db = serviceProvider.GetRequiredService<LmsContext>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var logger = serviceProvider.GetRequiredService<ILogger<LmsContext>>();

        try
        {
            // Ensure database is created
            await db.Database.EnsureCreatedAsync();

            logger.LogInformation("Checking if data exists...");

            // Check existing data - separate checks for each entity
            if (await db.Users.AnyAsync())
            {
                logger.LogInformation("Users already exist in the database.");
            }
            else
            {
                // Create roles
                logger.LogInformation("Creating roles...");
                await CreateRolesAsync(roleManager, new[] { teacherRole, studentRole });

                // Generate teachers and students
                logger.LogInformation("Generating teachers...");
                await GenerateTeachersAsync(userManager, 2);

                logger.LogInformation("Generating students...");
                await GenerateStudentsAsync(userManager, 10);
            }

            // Check and create activity types if they don't exist
            if (!await db.ActivityTypes.AnyAsync())
            {
                logger.LogInformation("Creating activity types...");
                var activityTypes = new[]
                {
                new ActivityType { Type = "E-Learning", Deadlines = "Submit within 24h" },
                new ActivityType { Type = "Lecture", Deadlines = "Attendance required" },
                new ActivityType { Type = "Assignment", Deadlines = "Submit by end date" },
                new ActivityType { Type = "Workshop", Deadlines = "Attendance required" },
                new ActivityType { Type = "Exercise", Deadlines = "Complete by end date" }
            };

                await db.ActivityTypes.AddRangeAsync(activityTypes);
                await db.SaveChangesAsync();
                logger.LogInformation($"Added {activityTypes.Length} activity types");
            }

            // Check and create courses if they don't exist
            if (!await db.Courses.AnyAsync())
            {
                logger.LogInformation("Generating courses...");
                var faker = new Faker();
                var courseNames = new[]
                {
                "ASP.NET Core Web API Seminar",
                ".NET Development 2024",
                "JavaScript Development 2024",
                "Frontend Development 2024"
            };

                // Get activity types for reference
                var activityTypes = await db.ActivityTypes.ToListAsync();
                if (!activityTypes.Any())
                {
                    throw new InvalidOperationException("Activity types must exist before creating courses");
                }

                foreach (var courseName in courseNames)
                {
                    var course = new Course
                    {
                        Name = courseName,
                        Description = courseName.Contains("Seminar")
                            ? "Online Course - 3 Inlämningsuppgifter"
                            : faker.Lorem.Paragraph(),
                        StartDate = DateTime.Now.AddDays(faker.Random.Int(1, 30)),
                        EnrolledUsers = new List<ApplicationUser>(),
                        Modules = new List<Module>()
                    };

                    var moduleStartDate = course.StartDate;
                    var moduleCount = courseName.Contains("Seminar") ? 1 : 4;

                    for (int i = 0; i < moduleCount; i++)
                    {
                        var module = new Module
                        {
                            Name = GetModuleName(courseName, i),
                            Description = faker.Lorem.Paragraph(),
                            StartDate = moduleStartDate,
                            EndDate = moduleStartDate.AddDays(courseName.Contains("Seminar") ? 7 : 30),
                            Activities = new List<Activity>(),
                            Documents = new List<Document>()
                        };

                        var activityCount = courseName.Contains("Seminar") ? 3 : 5;
                        for (int j = 0; j < activityCount; j++)
                        {
                            var activityType = activityTypes[j % activityTypes.Count];
                            var activity = new Activity
                            {
                                Name = courseName.Contains("Seminar")
                                    ? $"Assignment {j + 1}"
                                    : GetActivityName(module.Name, j),
                                Description = faker.Lorem.Paragraph(),
                                StartDate = moduleStartDate.AddDays(j * (courseName.Contains("Seminar") ? 2 : 5)),
                                EndDate = moduleStartDate.AddDays((j * (courseName.Contains("Seminar") ? 2 : 5)) + 4),
                                ActivityType = activityType
                            };

                            module.Activities.Add(activity);
                        }

                        course.Modules.Add(module);
                        moduleStartDate = module.EndDate.AddDays(1);
                    }

                    // Add students to course
                    var students = await userManager.GetUsersInRoleAsync(studentRole);
                    var studentGroup = students.Skip(courseNames.ToList().IndexOf(courseName) * 5).Take(5);
                    foreach (var student in studentGroup)
                    {
                        course.EnrolledUsers.Add(student);
                    }

                    logger.LogInformation($"Adding course: {courseName} with {course.Modules.Count} modules");
                    await db.Courses.AddAsync(course);
                }

                // Save all changes outside the loop
                await db.SaveChangesAsync();
                logger.LogInformation("Successfully saved all courses, modules, and activities");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database");
            throw;
        }
    }


    private static async Task CreateRolesAsync(RoleManager<IdentityRole> roleManager, string[] roleNames)
    {
        foreach (var roleName in roleNames)
        {
            if (await roleManager.RoleExistsAsync(roleName)) continue;

            var role = new IdentityRole { Name = roleName };
            var result = await roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                throw new ApplicationException($"Failed to create role {roleName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
    }

    private static async Task GenerateTeachersAsync(UserManager<ApplicationUser> userManager, int nrOfTeachers)
    {
        var faker = new Faker<ApplicationUser>("sv")
            .Rules((f, e) =>
            {
                e.Email = f.Internet.Email(provider: "lexicon.se");
                e.UserName = e.Email;
            });

        var teachers = faker.Generate(nrOfTeachers);
        const string password = "BytMig123!";

        foreach (var teacher in teachers)
        {
            var result = await userManager.CreateAsync(teacher, password);
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Failed to create teacher: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            var roleResult = await userManager.AddToRoleAsync(teacher, teacherRole);
            if (!roleResult.Succeeded)
            {
                throw new ApplicationException($"Failed to assign teacher role: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
            }
        }
    }

    private static async Task GenerateStudentsAsync(UserManager<ApplicationUser> userManager, int nrOfStudents)
    {
        var faker = new Faker<ApplicationUser>("sv")
            .Rules((f, e) =>
            {
                e.Email = f.Internet.Email();
                e.UserName = e.Email;
            });

        var students = faker.Generate(nrOfStudents);
        const string password = "BytMig123!";

        foreach (var student in students)
        {
            var result = await userManager.CreateAsync(student, password);
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Failed to create student: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            var roleResult = await userManager.AddToRoleAsync(student, studentRole);
            if (!roleResult.Succeeded)
            {
                throw new ApplicationException($"Failed to assign student role: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
            }
        }
    }
    private static string GetModuleName(string courseName, int index) =>
        courseName.Contains(".NET")
            ? index switch
            {
                0 => "C# Fundamentals",
                1 => "Database Development",
                2 => "Backend Development",
                3 => "Frontend Development",
                _ => $"Module {index + 1}"
            }
            : index switch
            {
                0 => "JavaScript Basics",
                1 => "Advanced JavaScript",
                2 => "React Fundamentals",
                3 => "Full Stack JavaScript",
                _ => $"Module {index + 1}"
            };

    private static string GetActivityName(string moduleName, int index) =>
        index switch
        {
            0 => $"{moduleName} - Introduction Lecture",
            1 => $"{moduleName} - E-Learning Session",
            2 => $"{moduleName} - Workshop",
            3 => $"{moduleName} - Practice Exercise",
            4 => $"{moduleName} - Final Assignment",
            _ => $"Activity {index + 1}"
        };
}