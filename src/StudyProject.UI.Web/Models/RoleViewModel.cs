using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace StudyProject.UI.WebCore.Models
{
    public class RoleViewModel
    {
        public IEnumerable<SelectListItem> AllUsers { get; set; }
        public IEnumerable<SelectListItem> AllRoles { get; set; }
        public IEnumerable<SelectListItem> AllClaims { get; set; }


        public string RoleSelected { get; set; }
        public string UserSelected { get; set; }
        public string ClaimSelected { get; set; }

        public RoleViewModel()
        {
            this.AllRoles = new HashSet<SelectListItem>();
            this.AllUsers = new HashSet<SelectListItem>();
            this.AllClaims = new HashSet<SelectListItem>();
        }
    }
}
