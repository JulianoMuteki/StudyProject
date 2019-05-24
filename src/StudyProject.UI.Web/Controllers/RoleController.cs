using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudyProject.Application.Identity.RoleViewModel;
using StudyProject.Infra.Context.Identity;

namespace StudyProject.UI.WebCore.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_roleManager.Roles.ToList());
        }

        public IActionResult Create()
        {
            var role = new ApplicationRole();
            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationRole role)
        {
            IdentityResult roleResult;
            //Adding Admin Role
            var roleCheck = await _roleManager.RoleExistsAsync(role.Name);
            if (!roleCheck)
            {
                //create the roles and seed them to the database
                roleResult = await _roleManager.CreateAsync(role);
            }

            return RedirectToAction("Index");
        }

        public IActionResult AssignRoleToUser()
        {
            RolesToUsersViewModel rolesToUsersViewModel = new RolesToUsersViewModel();

            rolesToUsersViewModel.AllRoles = _roleManager.Roles.ToList()
                                                   .Select(role => new SelectListItem
                                                   {
                                                       Value = role.Name,
                                                       Text = role.Name
                                                   }).ToList();

            rolesToUsersViewModel.AllUsers = _userManager.Users.ToList()
                                                   .Select(user => new SelectListItem
                                                   {
                                                       Value = user.Id.ToString(),
                                                       Text = user.UserName
                                                   }).ToList();

            return View(rolesToUsersViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleToUser(RolesToUsersViewModel rolesToUsersViewModel)
        {
           // ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(rolesToUsersViewModel.UserSelected);

                var result = await _userManager.AddToRoleAsync(user, rolesToUsersViewModel.RoleSelected);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
               // AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("Index");
        }
    }
}