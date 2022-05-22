using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudyProject.Domain.Identity;
using StudyProject.Secutity.Auth;

namespace StudyProject.WebApi.Controllers
{
    [ApiController]   
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AuthorizeEnum(RoleAuthorize.Admin, RoleAuthorize.Manager)]
    public class ProductController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        public ProductController(
           UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager,
           ILogger<AccountController> logger, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _configuration = configuration;
        }
        [HttpGet]
        [AuthorizePolicyEnum(PERMISSIONS.Index)]
        public IActionResult Get()
        {
            try
            {
                return Ok("Index Succeeded");
            }
            catch
            {
                return BadRequest("Error in Index");
            }
        }

        // POST: ProductController/Delete/5
        [HttpDelete("{id}")]
        [AuthorizePolicyEnum(PERMISSIONS.Delete)]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok("Deleteded Succeeded");
            }
            catch
            {
                return BadRequest("Error in Delete");
            }
        }
    }
}
