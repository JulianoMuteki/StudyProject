using Microsoft.AspNetCore.Identity;
using System;

namespace StudyProject.Domain.Identity
{
    public class ApplicationUserToken : IdentityUserToken<Guid>
    {
        public virtual ApplicationUser User { get; set; }
    }
}
