using Microsoft.AspNetCore.Mvc;
using StudyProject.Domain.Security;
using StudyProject.UI.WebCore.Extensions;

namespace StudyProject.UI.WebCore.Controllers
{
    [AuthorizeEnum(RoleAuthorize.Admin, RoleAuthorize.Manager)]
    public class ProductController : Controller
    {
        [AuthorizePolicyEnum(PERMISSIONS.Index)]
        public IActionResult Index()
        {
            return View();
        }
    }
}