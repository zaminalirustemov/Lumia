using Lumia_ECommerce.Areas.Manage.ViewModels;
using Lumia_ECommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

namespace Lumia_ECommerce.Areas.Manage.Controllers;
[Area("Manage")]
public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    //Login----------------------------------------------------------------------------------------------------------
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(AdminLoginViewModel adminLoginVM)
    {
        if(!ModelState.IsValid) return View(adminLoginVM);

        AppUser appUser =await _userManager.FindByNameAsync(adminLoginVM.UserName);
        if (appUser == null)
        {
            ModelState.AddModelError("", "Username or password is invalid");
            return View(adminLoginVM);
        }

        var result = await _signInManager.PasswordSignInAsync(appUser, adminLoginVM.Password, false, false);
        if (!result.Succeeded)
        {

            ModelState.AddModelError("", "Username or password is invalid");
            return View(adminLoginVM);
        }

        return RedirectToAction("Index","dashboard");
    }
    //Logout---------------------------------------------------------------------------------------------------------
    public IActionResult Logout()
    {
        _signInManager.SignOutAsync();
        return RedirectToAction(nameof(Login));
    }
}
