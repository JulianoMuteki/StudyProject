using Microsoft.AspNetCore.Identity;
using System;

namespace StudyProject.Infra.Context.Identity
{
    public class ApplicationUserRole : IdentityUserRole<Guid>
    {
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}
