using System.ComponentModel.DataAnnotations;

namespace StudyProject.Secutity.Auth
{
    public enum RoleAuthorize
    {

        [Display(GroupName = "Manager", Name = "Admin", Description = "System admin")]
        Admin,

        [Display(GroupName = "Manager", Name = "Manager", Description = "View all system")]
        Manager,

        [Display(GroupName = "Staff", Name = "Staff", Description = "Operation")]
        Staff,

        [Display(GroupName = "Client", Name = "Client", Description = "Just client")]
        Client,

        [Display(GroupName = "Employee", Name = "Employee", Description = "Employee")]
        Employee,

        [Display(GroupName = "Driver", Name = "Driver", Description = "Driver")]
        Driver


    }
}
