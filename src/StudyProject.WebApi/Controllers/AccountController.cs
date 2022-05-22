using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudyProject.Application.Identity.AccountViewModels;
using StudyProject.Application.Services;
using StudyProject.Domain.Identity;
using StudyProject.Secutity;

namespace StudyProject.WebApi.Controllers
{
    [AllowAnonymous]
    [ApiController]    
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly ICustomEmailSender _customEmailSender;
        public AccountController(
           UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager,
           RoleManager<ApplicationRole> roleManager,
           ILogger<AccountController> logger, IConfiguration configuration,
           ICustomEmailSender customEmailSender)
        {
            _customEmailSender = customEmailSender;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(model.Email);

                if (existingUser == null)
                {
                    return BadRequest(new {
                        Errors = "Invalid login request",
                        Success = false
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, model.Password);

                if (!isCorrect)
                {
                    return BadRequest(new
                    {
                        Errors = "Invalid login request",
                        Success = false
                    });
                }

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");                    
                    
                    var claims = await ApplicationUserService.GetClaimsByUser(existingUser, _roleManager, _userManager);
                    return Ok(CustomToken.GenerateToken(model.Email, _configuration["Jwt:key"],
                                                             _configuration["TokenConfiguration:ExpireHours"],
                                                             _configuration["TokenConfiguration:Issuer"],
                                                             _configuration["TokenConfiguration:Audience"],
                                                             claims));
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return BadRequest(new
                    {
                        Errors = "User account locked out.",
                        Success = false
                    });
                }
            }
            _logger.LogWarning("Invalid login attempt.");
            return BadRequest(new
            {
                Errors = "Invalid login attempt.",
                Success = false
            });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            string jwtToken = string.Empty;

            if (ModelState.IsValid)
            {
                // We can utilise the model
                var existingUser = await _userManager.FindByEmailAsync(model.Email);

                if (existingUser != null)
                {
                    return BadRequest(new
                    {
                        Errors = "Email already in use",
                        Success = false
                    });
                }

                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    try
                    {
                        var newuser = await _userManager.FindByEmailAsync(model.Email);

                        var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(newuser);

                        await _customEmailSender.SendEmailAsync(newuser.Email, "StudyProject TOKEN", emailConfirmationToken);
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                    return BadRequest(result.Errors);
            }
            else
                return BadRequest(ModelState);

            return Ok(jwtToken); // passtoken
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("ValidateRegister")]
        public async Task<IActionResult> ValidateRegister([FromBody] LoginViewModel model, string token)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return BadRequest("Not found");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Ok(); // passtoken                        
            }

            return BadRequest("Error to validate token");
        }

    }
}