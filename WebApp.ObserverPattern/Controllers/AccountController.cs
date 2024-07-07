using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.ObserverPattern.Entities;
using WebApp.ObserverPattern.Models;
using WebApp.ObserverPattern.Observer;

namespace WebApp.ObserverPattern.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserObserverSubject _userObserverSubject;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            UserObserverSubject userObserverSubject)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userObserverSubject = userObserverSubject;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var hasUser = await _userManager.FindByEmailAsync(email);

            if (hasUser == null) return View();

            var signInResult = await _signInManager.PasswordSignInAsync(hasUser, password, true, false);

            if (!signInResult.Succeeded)
            {
                return View();
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserCreateViewModel userCreate)
        {
            var appUser = new AppUser()
            {
                UserName = userCreate.UserName,
                Email = userCreate.Email
            };
            var identityResult = await _userManager.CreateAsync(appUser, userCreate.Password);
            if (identityResult.Succeeded)
            {
                _userObserverSubject.NotifyObservers(appUser);
                ViewBag.Message = "Üyelik işlemi başarılı";
            }
            else
            {
                ViewBag.Message = identityResult.Errors.ToList().First().Description;
            }

            return View();
        }
    }
}