using System;
using System.Collections.Generic;
using System.Text;

namespace StudyProject.Application.ViewModels
{
    public class ClientVM: ViewModelBase
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

    }
}
