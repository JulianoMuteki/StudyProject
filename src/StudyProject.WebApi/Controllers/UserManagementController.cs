using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudyProject.Application.Identity.AccountViewModels;
using StudyProject.Application.Identity.ManageViewModels;
using StudyProject.Domain.Identity;

namespace StudyProject.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = "Admin")]
    public class UserManagementController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        public UserManagementController(
           UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager,
           ILogger<AccountController> logger, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _configuration = configuration;
        }


        [HttpGet("Users")]
        public IActionResult Get()
        {
           var users = _userManager.Users.ToList()
                                                      .Select(user => new
                                                      {                                                         
                                                          User = user.UserName
                                                      }).ToList();
            return Ok(users);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return BadRequest(ModelState.Values);
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, model.Password);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest("Error in reset password");
        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                //throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (changePasswordResult.Succeeded)
            {
                return Ok("Changeded Succeeded");
            }else
                return BadRequest(changePasswordResult.Errors);

            return BadRequest("Error in change password");
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
               
                var rolesForUser = await _userManager.GetRolesAsync(user);

                if (rolesForUser.Count() > 0)
                {
                    foreach (var item in rolesForUser.ToList())
                    {
                        // item should be the name of the role
                        var result = await _userManager.RemoveFromRoleAsync(user, item);
                    }
                }

                await _userManager.DeleteAsync(user);
                return Ok("Deleteded Succeeded");
            }

            return BadRequest("Error in Delete");
        }
    }
}
