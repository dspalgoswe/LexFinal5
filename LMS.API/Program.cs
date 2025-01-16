using Domain.Contracts;
using Domain.Models.Entities;
using LMS.API.Extensions;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.Repositories;
using LMS.Presemtation;
using LMS.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;
using System.Security.Claims;


namespace LMS.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<LmsContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("LmsContext") ?? throw new InvalidOperationException("Connection string 'CompaniesContext' not found.")));

        builder.Services.AddControllers(configure =>
        {
            configure.ReturnHttpNotAcceptable = true;
        })
                        .AddNewtonsoftJson()
                        .AddApplicationPart(typeof(AssemblyReference).Assembly);

        builder.Services.ConfigureOpenApi();
        builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
        builder.Services.ConfigureServiceLayerServices();
        builder.Services.ConfigureRepositories();
        builder.Services.ConfigureJwt(builder.Configuration);
        builder.Services.ConfigureCors();

        builder.Services.AddHttpClient();
        //builder.Services.AddScoped<ITeacherService, TeacherService>();
        //builder.Services.AddLazy<ITeacherService>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        //builder.Services.AddScoped<ICourseService, CourseService>();

        builder.Services.AddIdentityCore<ApplicationUser>(opt =>
            {
                opt.SignIn.RequireConfirmedAccount = false;
                opt.User.RequireUniqueEmail = true;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<LmsContext>()
                .AddDefaultTokenProviders();

        builder.Services.AddAuthorization(options =>
        {
            // Teacher policies
            options.AddPolicy("TeacherPolicy", policy => policy.RequireRole("Teacher"));

            // Create a combined policy for administrative actions
            options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Teacher")); // Teachers have admin rights

              options.AddPolicy("StudentDocumentAccess", policy => policy.RequireRole("Student", "Teacher"));
        });

        //logging for clearer and easier debugging
        builder.Services.AddLogging(options =>
        {
            options.AddConsole();
            options.AddDebug();
        });

        builder.Services.Configure<PasswordHasherOptions>(options => options.IterationCount = 10000);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            await app.SeedDataAsync();
        }

        app.UseHttpsRedirection();

        app.UseCors("AllowAll");

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
