using System.ComponentModel.DataAnnotations;

namespace StudyProject.Application.Identity.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
