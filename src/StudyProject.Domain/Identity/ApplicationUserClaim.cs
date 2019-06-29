using Microsoft.AspNetCore.Identity;
using System;

namespace StudyProject.Domain.Identity
{
    public class ApplicationUserClaim : IdentityUserClaim<Guid>
    {
        public virtual ApplicationUser User { get; set; }

    }
}
