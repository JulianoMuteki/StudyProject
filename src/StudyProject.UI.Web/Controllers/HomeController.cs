using Microsoft.AspNetCore.Mvc;
using StudyProject.Domain.Interfaces.Application;
using StudyProject.UI.Web.Models;
using System.Diagnostics;

namespace StudyProject.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClientApplicationService _userService;

        public HomeController(IClientApplicationService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
           // var clients = _userService.GetAll();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}