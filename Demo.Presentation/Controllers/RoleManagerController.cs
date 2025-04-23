using Demo.DataAccess.Models.IdentityModels;
using Demo.Presentation.ViewModels.RolesViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.Presentation.Controllers
{
    [Authorize/*(Policy = "RequireAdmin")*/]
    public class RoleManagerController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleManagerController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        #region Get All Roles
        //public IActionResult Index()
        //{
        //    var roles = _roleManager.Roles.ToList();
        //    return View(roles);
        //}
        public async Task<IActionResult> Index(string RoleSearchName)
        {
            // Get all roles from RoleManager
            var roles = _roleManager.Roles;

            // Apply search filter if provided
            if (!string.IsNullOrEmpty(RoleSearchName))
            {
                roles = roles.Where(r => r.Name.Contains(RoleSearchName));
            }

            // Convert to list for the view
            var roleList = await roles.ToListAsync();

            return View(roleList);
        }
        #endregion



        #region Details
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var viewModel = new RoleViewModel()
            {
                Id = role.Id,
                RoleName = role.Name,
            };

            return View(viewModel);
        }
        #endregion



        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name")] IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(role);
        }
        #endregion



        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var model = new EditRoleViewModel()
            {
                Id = role.Id,
                RoleName = role.Name
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(string id, [Bind("Id,RoleName")] EditRoleViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingRole = await _roleManager.FindByIdAsync(model.Id);
                if (existingRole == null)
                {
                    return NotFound();
                }

                existingRole.Name = model.RoleName;
                existingRole.NormalizedName = model.RoleName?.ToUpper();

                var result = await _roleManager.UpdateAsync(existingRole);
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Role updated successfully";
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }
        #endregion



        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var model = new RoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(string id )
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = $"Role '{role.Name}' was deleted successfully";
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("Delete", new RoleViewModel { Id = role.Id, RoleName = role.Name });
        }
        #endregion
    }
}
