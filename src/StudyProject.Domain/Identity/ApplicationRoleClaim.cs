using Microsoft.AspNetCore.Identity;
using System;

namespace StudyProject.Domain.Identity
{
    public class ApplicationRoleClaim : IdentityRoleClaim<Guid>
    {
        public virtual ApplicationRole Role { get; set; }

    }
}
