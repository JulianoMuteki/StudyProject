using Microsoft.AspNetCore.Identity;
using System;

namespace StudyProject.Infra.Context.Identity
{
    public class ApplicationUserClaim : IdentityUserClaim<Guid>
    {
        public virtual ApplicationUser User { get; set; }

    }
}
