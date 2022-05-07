using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudyProject.CrossCutting.Ioc.DependencyInjection;
using StudyProject.Domain.Identity;
using StudyProject.Domain.Security;
using StudyProject.Infra.Context;

namespace StudyProject.WebApi.Settings
{
    public static class StudyProjectService
    {
        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            var services = builder.Services;

            services.AddDbContext<StudyProjectContext>(item => item.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                                .AddEntityFrameworkStores<StudyProjectContext>()
                                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                options.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";

                // User settings
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            });
           
            services.AddAuthorization(options =>
            {
                foreach (var item in PolicyTypes.ListAllClaims)
                {
                    options.AddPolicy(item.Value.Value, policy => { policy.RequireClaim(CustomClaimTypes.DefaultPermission, item.Value.Value); });
                }
            });                      

            services.AddAutoMapperSetup();
            services.RegisterInfraBootStrapper();
            services.RegisterApplicationBootStrapper();
        }
    }
}
