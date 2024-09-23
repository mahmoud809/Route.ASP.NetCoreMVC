﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDemo.DAL.Models;
using MyDemo.PL.Helpers;
using MyDemo.PL.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDemo.PL.Controllers
{
    [Authorize(Roles ="Admin")]
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager , IMapper mapper)
        {
			_userManager = userManager;
			_signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string email)
		{
			if(string.IsNullOrEmpty(email))
			{
				var users = await _userManager.Users.Select(U => new UserViewModel()
				{
					Id = U.Id,
					FName = U.FName,
					LName = U.LName,
					Email = U.Email,
					PhoneNumber = U.PhoneNumber,
					Roles = _userManager.GetRolesAsync(U).Result
				}).ToListAsync();

				return View(users);
			}
			else
			{
				var user = await _userManager.FindByEmailAsync(email);
				var mappedUser = new UserViewModel()
				{
					Id = user.Id,
					FName = user.FName,
					LName = user.LName,
					Email = user.Email,
					PhoneNumber = user.PhoneNumber,
					Roles = _userManager.GetRolesAsync(user).Result

				};
				return View(new List<UserViewModel>() { mappedUser } );
			}
		}

        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();

            var mappedUser = _mapper.Map<ApplicationUser, UserViewModel>(user);

            return View(ViewName, mappedUser);
        }

        [HttpGet]
        public Task<IActionResult> Edit(string id)
        {

            return Details(id, "Edit");
            
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel updatedUser, [FromRoute] string id)
        {
            if (id != updatedUser.Id)
                return BadRequest();
            
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id); // i get user from database then upadate manually
                    
                    user.FName = updatedUser.FName;
                    user.LName = updatedUser.LName;
                    user.PhoneNumber = updatedUser.PhoneNumber;

                    await _userManager.UpdateAsync(user);

                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(updatedUser);



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
                var user = await _userManager.FindByIdAsync(id);

                await _userManager.DeleteAsync(user);

                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error" , "Home");
            }
        }

    }
}
