using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using StudyProject.Domain.Security;
using StudyProject.Infra.Context.Identity;
using System;

namespace StudyProject.Infra.Context
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            try
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

                SeedData(userManager, roleManager);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync("juliano.pestili@outlook.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "juliano.pestili@outlook.com";
                user.Email = "juliano.pestili@outlook.com";
                user.PhoneNumber = "(19) 99999-8888";
                user.FirstName = "Admin";
                user.LastName = "User";

                IdentityResult result = userManager.CreateAsync(user, "Pa$$w0rd").Result;

                if (result.Succeeded)
                {
                    result = userManager.AddToRoleAsync(user, RoleAuthorize.Admin.ToString()).Result;
                }
            }
        }

        public static void SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            foreach (var roleName in Enum.GetNames(typeof(RoleAuthorize)))
            {
                if (!roleManager.RoleExistsAsync(roleName).Result)
                {
                    ApplicationRole role = new ApplicationRole();
                    role.Name = roleName;
                    IdentityResult roleResult = roleManager.CreateAsync(role).Result;

                    //if (RoleAuthorize.Admin.ToString() == roleName)
                    //    roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.DefaultPermission, PolicyTypes.DeliveryPolicy.ExecuteDelivery)).Wait();
                }
            }
        }

    }
}
