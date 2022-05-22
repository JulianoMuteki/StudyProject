using Microsoft.AspNetCore.Identity;
using StudyProject.Domain.Identity;
using System.Security.Claims;

namespace StudyProject.WebApi
{
    public static class ApplicationUserService
    {
        // Get all valid claims for the corresponding user
        public static async Task<List<Claim>> GetClaimsByUser(ApplicationUser user, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            var claims = new List<Claim>();

            // Getting the claims that we have assigned to the user
            var userClaims = await userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            // Get the user role and add it to the claims
            var userRoles = await userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
            {
                var role = await roleManager.FindByNameAsync(userRole);

                if (role != null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole));

                    var roleClaims = await roleManager.GetClaimsAsync(role);
                    foreach (var roleClaim in roleClaims)
                    {
                        if (!claims.Any(x => x.Value == roleClaim.Value))
                            claims.Add(roleClaim);
                    }
                }
            }

            return claims;
        }
    }
}
