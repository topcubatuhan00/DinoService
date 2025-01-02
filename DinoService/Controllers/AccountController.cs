using DinoService.Models;
using DinoService.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DinoService.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<Users> _signInManager;
    private readonly UserManager<Users> _userManager; 
    public AccountController(SignInManager<Users> signInManager, UserManager<Users> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var res = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (res.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Email or password is wrong");
                return View(model);
            }
        }
        return View(model);
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid) { 
            Users users = new Users
            {
                FullName = model.Name,
                Email = model.Email,
                UserName = model.Email
            };

            var res = await _userManager.CreateAsync(users, model.Password);
            if (res.Succeeded)
            {
                return RedirectToAction("Login","Account");
            }
            else
            {
                foreach (var item in res.Errors)
                {
                    ModelState.AddModelError("",item.Description);
                }
                return View(model);
            }
        }
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index","Home");
    }
}
