using Microsoft.AspNetCore.Mvc;
using StudyProject.Domain.Security;
using StudyProject.UI.WebCore.Extensions;

namespace StudyProject.UI.WebCore.Controllers
{
    [AuthorizeEnum(RoleAuthorize.Admin, RoleAuthorize.Client)]
    public class ClientController : Controller
    {
        [AuthorizePolicyEnum(PERMISSIONS.Read)]
        public IActionResult Index()
        {
            return View();
        }
    }
}