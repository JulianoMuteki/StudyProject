using Microsoft.AspNetCore.Mvc.Rendering;
using StudyProject.Infra.Context.Identity;
using System;
using System.Collections.Generic;
using System.Text;

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
