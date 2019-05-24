using Microsoft.AspNetCore.Identity;
using System;

namespace StudyProject.Infra.Context.Identity
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole()
        {

        }

        public ApplicationRole(string roleName)
            :base(roleName)
        {

        }
    }
}
