using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDemo.DAL.Models;
using MyDemo.PL.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDemo.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager ,IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                var roles = await _roleManager.Roles.Select(R => new RoleViewModel()
                {
                    Id = R.Id,
                    RoleName = R.Name
                   
                }).ToListAsync();

                return View(roles);
            }
            else
            {
                var role = await _roleManager.FindByNameAsync(name);
               
                if(role is not null)
                {
                    var mappedRole = new RoleViewModel()
                    {
                        Id = role.Id,
                        RoleName = role.Name

                    };
                    return View(new List<RoleViewModel>() { mappedRole });
                }
                else
                {
                    return View(Enumerable.Empty<RoleViewModel>());  
                }
               
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleVM)
        {
            if(ModelState.IsValid)
            {
                var mappedRole = _mapper.Map<RoleViewModel , IdentityRole>(roleVM);
                await _roleManager.CreateAsync(mappedRole);
                return RedirectToAction(nameof(Index));

            }
            return View(roleVM);
        }
        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();

            var mappedRole = _mapper.Map<IdentityRole, RoleViewModel>(role);

            return View(ViewName, mappedRole);
        }

        [HttpGet]
        public Task<IActionResult> Edit(string id)
        {

            return Details(id, "Edit");

        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel updatedRole, [FromRoute] string id)
        {
            if (id != updatedRole.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(id); // i get Role from database then upadate manually

                    role.Name = updatedRole.RoleName;
                    

                    await _roleManager.UpdateAsync(role);

                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(updatedRole);



        }

        public Task<IActionResult> Delete(string id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(string id)
        {

            try
            {
                var role = await _roleManager.FindByIdAsync(id);

                await _roleManager.DeleteAsync(role);

                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            
            if(role is null)
                return NotFound();
            
            ViewBag.RoleId = roleId;

            var users = await _userManager.Users.ToListAsync();

            var usersInRole = new List<UserInRoleViewModel>();

            foreach (var user in users)
            {
                var userInRole = new UserInRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };
               
                if(await _userManager.IsInRoleAsync(user , roleId))
                    userInRole.IsInRole = true;
                else
                    userInRole.IsInRole = false;

                usersInRole.Add(userInRole);

            }
            return View(usersInRole);
        }

        [HttpPost]
        public async Task<ActionResult> AddOrRemoveUsers(string roleId, List<UserInRoleViewModel> users)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null) 
                return NotFound();
            
            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appUser = await _userManager.FindByIdAsync(user.UserId);

                    if (appUser is not null)
                    {
                        if (user.IsInRole && !await _userManager.IsInRoleAsync(appUser, role.Name))
                            await _userManager.AddToRoleAsync(appUser, role.Name);
                        else if (!user.IsInRole && await _userManager.IsInRoleAsync(appUser, role.Name))
                            await _userManager.RemoveFromRoleAsync(appUser, role.Name);
                    }

                }
                return RedirectToAction(nameof(Edit), new { id = roleId });

            }
            return View(users);

        }

    }
}
