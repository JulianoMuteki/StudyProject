using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace StudyProject.Application.Identity.RoleViewModel
{
    public class RolesToUsersViewModel
    {
        public IEnumerable<SelectListItem> AllUsers { get; set; }
        public IEnumerable<SelectListItem> AllRoles { get; set; }

        public string RoleSelected { get; set; }
        public string UserSelected { get; set; }
    }
}
