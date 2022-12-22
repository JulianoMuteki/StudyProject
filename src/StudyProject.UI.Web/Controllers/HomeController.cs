using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudyProject.Application.Identity;
using StudyProject.Application.ViewModels;
using StudyProject.Domain.Interfaces.Application;
using System.Collections.Generic;
using System.Diagnostics;

namespace StudyProject.UI.WebCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClientApplicationService _userService;
        private readonly IMapper _mapper;

        public HomeController(IClientApplicationService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var clients = _userService.GetAll();

            var clientsVM = _mapper.Map<ICollection<ClientVM>>(clients);
            return View(clientsVM);
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