using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyProject.UI.Web.Models
{
    public class ClientViewModel
    {
        public int ClientID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
    }
}
