using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudyProject.Domain.Security;
using StudyProject.Infra.Context.Identity;
using StudyProject.UI.WebCore.Extensions;
using StudyProject.UI.WebCore.Models;

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
                RoleViewModel rolesToUsersViewModel = new RoleViewModel();
                rolesToUsersViewModel.AllRoles = PolicyTypes.ListAllRoles
                                           .Select(role => new SelectListItem
                                           {
                                               Value = role,
                                               Text = role
                                           }).ToList();
                rolesToUsersViewModel.AllClaims = PolicyTypes.ListClaimsAuthorizations
                               .Select(claim => new SelectListItem
                               {
                                   Value = claim,
                                   Text = claim
                               }).ToList();
                return View(rolesToUsersViewModel);
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
                RoleViewModel rolesToUsersViewModel = new RoleViewModel();

                rolesToUsersViewModel.AllRoles = _roleManager.Roles.ToList()
                                                       .Select(role => new SelectListItem
                                                       {
                                                           Value = role.Id.ToString(),
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
            public async Task<IActionResult> AssignRoleToUser(RoleViewModel rolesToUsersViewModel)
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

            [HttpGet]
            public IActionResult GetAjaxHandlerRoles(string userID)
            {
                var user = _userManager.FindByIdAsync(userID).Result;
                RoleViewModel rolesToUsersViewModel = new RoleViewModel();

                rolesToUsersViewModel.AllRoles = _roleManager.Roles.ToList()
                                                       .Select(role => new SelectListItem
                                                       {
                                                           Value = role.Id.ToString(),
                                                           Text = role.Name
                                                       }).ToList();

                var rolesSelectedNames = _userManager.GetRolesAsync(user).Result;

                rolesToUsersViewModel.AllRoles = rolesToUsersViewModel.AllRoles.Select(x => { x.Selected = rolesSelectedNames.Any(r => r.Equals(x.Text)); return x; }).ToList();
                return Json(rolesToUsersViewModel.AllRoles);
            }

            [HttpPost]
            public IActionResult PostAjaxHandlerRoles(string[] rolesJSON, string userID)
            {
                Guid id;

                if (Guid.TryParse(userID, out id))
                {
                    var user = _userManager.Users.Where(u => u.Id.ToString() == userID).Include(x => x.UserRoles).FirstOrDefault();
                    var rolesExists = user.UserRoles;

                    JsonSerialize jsonS = new JsonSerialize();
                    var rolesVM = jsonS.JsonDeserializeObject<List<DropDpwnViewModel>>(rolesJSON[0]);
                    var appUsersRolesToRemove = rolesExists.Where(x => !rolesVM.Any(z => z.id == x.RoleId.ToString())).ToList();

                    IdentityResult result;

                    var rolesToAdd = rolesVM.Where(x => !rolesExists.Any(z => z.RoleId.ToString() == x.id)).ToList();
                    if (rolesToAdd.Count > 0)
                        result = _userManager.AddToRolesAsync(user, rolesToAdd.Select(x => x.text).ToArray()).Result;

                    if (appUsersRolesToRemove.Count > 0)
                    {
                        var rolesNameToRemove = _roleManager.Roles.Where(x => appUsersRolesToRemove.Any(z => z.RoleId == x.Id)).Select(x => x.Name).ToList();
                        if (rolesNameToRemove.Count > 0)
                            result = _userManager.RemoveFromRolesAsync(user, rolesNameToRemove).Result;
                    }
                    return Json(new { OK = "ok" });
                }
                else
                {
                    return Json(new { OK = "Without user" });
                }
            }

            [HttpPost]
            public IActionResult PostAjaxHandlerClaimsToRole(string[] claimsJSON, string roleID)
            {
                Guid id;

                if (Guid.TryParse(roleID, out id))
                {
                    var role = _roleManager.Roles.Where(r => r.Id == id).Include(c => c.RoleClaims).FirstOrDefault();
                    var rolesExists = role.RoleClaims;

                    JsonSerialize jsonS = new JsonSerialize();
                    var claimsVM = jsonS.JsonDeserializeObject<List<DropDpwnViewModel>>(claimsJSON[0]);
                    var appRolesClaimsToRemove = rolesExists.Where(x => !claimsVM.Any(z => z.id == x.ClaimValue.ToString())).ToList();

                    var claimsToAdd_VMs = claimsVM.Where(x => !rolesExists.Any(z => z.ClaimValue == x.id)).ToList();
                    if (claimsToAdd_VMs.Count > 0)
                    {
                        var claimsAdd = PolicyTypes.ListAllClaims.Where(x => claimsToAdd_VMs.Any(c => c.id == x.Value.Value)).Select(x => x.Value).ToList();
                        foreach (var item in claimsAdd)
                        {
                            var result = _roleManager.AddClaimAsync(role, item);
                            result.Wait();
                        }
                    }

                    if (appRolesClaimsToRemove.Count > 0)
                    {
                        var claims = _roleManager.GetClaimsAsync(role).Result;
                        var claimsToRemove = claims.Where(x => appRolesClaimsToRemove.Any(z => z.ClaimValue == x.Value)).ToList();

                        if (claimsToRemove.Count > 0)
                        {
                            foreach (var item in claimsToRemove)
                            {
                                var result = _roleManager.RemoveClaimAsync(role, item);
                                result.Wait();
                            }
                        }
                    }

                    return Json(new { OK = "ok" });
                }
                else
                {
                    return Json(new { OK = "Without user" });
                }
            }
            [HttpGet]
            public IActionResult GetAjaxHandlerClaimsRoles(string roleID)
            {
                RoleViewModel rolesViewModel = new RoleViewModel();

                var rol = _roleManager.FindByIdAsync(roleID).Result;
                var claims = _roleManager.GetClaimsAsync(rol).Result;

                var claimsListItem = PolicyTypes.ListAllClaims
                                                     .Select(claim => new SelectListItem
                                                     {
                                                         Value = claim.Value.Value,
                                                         Text = claim.Value.Value
                                                     }).ToList();

                rolesViewModel.AllClaims = claimsListItem.Select(x => { x.Selected = claims.Any(r => r.Value.Equals(x.Value)); return x; }).ToList();
                return Json(rolesViewModel.AllClaims);
            }

            [HttpPost]
            public IActionResult PostAjaxHandlerClaimsToUser(string[] claimsJSON, string userID)
            {
                Guid id;

                if (Guid.TryParse(userID, out id))
                {
                    var user = _userManager.Users.Where(r => r.Id == id).Include(c => c.UserClaims).FirstOrDefault();
                    var claimsExists = user.UserClaims;

                    JsonSerialize jsonS = new JsonSerialize();
                    var claimsVM = jsonS.JsonDeserializeObject<List<DropDpwnViewModel>>(claimsJSON[0]);
                    var appUsersClaimsToRemove = claimsExists.Where(x => !claimsVM.Any(z => z.id == x.ClaimValue.ToString())).ToList();

                    var claimsToAdd_VMs = claimsVM.Where(x => !claimsExists.Any(z => z.ClaimValue == x.id)).ToList();
                    if (claimsToAdd_VMs.Count > 0)
                    {
                        var claimsAdd = PolicyTypes.ListAllClaims.Where(x => claimsToAdd_VMs.Any(c => c.id == x.Value.Value)).Select(x => x.Value).ToList();
                        var result = _userManager.AddClaimsAsync(user, claimsAdd);
                        result.Wait();
                    }

                    if (appUsersClaimsToRemove.Count > 0)
                    {
                        var claims = _userManager.GetClaimsAsync(user).Result;
                        var claimsToRemove = claims.Where(x => appUsersClaimsToRemove.Any(z => z.ClaimValue == x.Value)).ToList();

                        if (claimsToRemove.Count > 0)
                        {
                            var result = _userManager.RemoveClaimsAsync(user, claimsToRemove);
                            result.Wait();
                        }
                    }

                    return Json(new { OK = "ok" });
                }
                else
                {
                    return Json(new { OK = "Without user" });
                }
            }

            [HttpGet]
            public IActionResult GetAjaxHandlerClaimsUsers(string userID)
            {
                RoleViewModel rolesViewModel = new RoleViewModel();

                var user = _userManager.FindByIdAsync(userID).Result;
                var claims = _userManager.GetClaimsAsync(user).Result;

                var claimsListItem = PolicyTypes.ListAllClaims
                                                     .Select(claim => new SelectListItem
                                                     {
                                                         Value = claim.Value.Value,
                                                         Text = claim.Value.Value
                                                     }).ToList();

                rolesViewModel.AllClaims = claimsListItem.Select(x => { x.Selected = claims.Any(r => r.Value.Equals(x.Value)); return x; }).ToList();
                return Json(rolesViewModel.AllClaims);
            }

            public IActionResult AssignClaimToUser()
            {
                RoleViewModel rolesToUsersViewModel = new RoleViewModel();

                rolesToUsersViewModel.AllRoles = _roleManager.Roles.ToList()
                                                       .Select(role => new SelectListItem
                                                       {
                                                           Value = role.Name,
                                                           Text = role.Name
                                                       }).ToList();

                return View(rolesToUsersViewModel);
            }

            public IActionResult AssignClaimToRole()
            {
                RoleViewModel rolesToUsersViewModel = new RoleViewModel();

                rolesToUsersViewModel.AllRoles = _roleManager.Roles.ToList()
                                                       .Select(role => new SelectListItem
                                                       {
                                                           Value = role.Name,
                                                           Text = role.Name
                                                       }).ToList();

                return View(rolesToUsersViewModel);
            }
        }
}