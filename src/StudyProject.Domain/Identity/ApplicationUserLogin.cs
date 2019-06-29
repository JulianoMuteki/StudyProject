using Microsoft.AspNetCore.Identity;
using System;

namespace StudyProject.Domain.Identity
{
    public class ApplicationUserLogin : IdentityUserLogin<Guid>
    {
        public virtual ApplicationUser User { get; set; }
    }
}
