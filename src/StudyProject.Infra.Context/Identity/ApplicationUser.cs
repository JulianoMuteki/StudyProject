using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyProject.Infra.Context.Identity
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
        public ICollection<ApplicationUserClaim> UserClaims { get; set; }
    }
}
