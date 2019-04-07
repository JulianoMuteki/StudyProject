using System.ComponentModel.DataAnnotations;

namespace StudyProject.Application.Identity.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
