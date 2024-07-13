using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.TemplatePattern.Entities;

namespace WebApp.TemplatePattern.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var hasUser = await _userManager.FindByEmailAsync(email);

        if (hasUser == null)
            return View();

        var signInResult = await _signInManager.PasswordSignInAsync(hasUser, password, true, false);

        if (!signInResult.Succeeded)
            return View();

        return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction(nameof(HomeController.Index), "Home");
    }
}
