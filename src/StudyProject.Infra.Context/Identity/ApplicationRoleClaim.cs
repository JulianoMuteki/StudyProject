using Microsoft.AspNetCore.Identity;
using System;

namespace StudyProject.Infra.Context.Identity
{
    public class ApplicationRoleClaim : IdentityRoleClaim<Guid>
    {
        public virtual ApplicationRole Role { get; set; }

    }
}
