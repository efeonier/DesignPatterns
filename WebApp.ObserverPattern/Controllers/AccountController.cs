using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.ObserverPattern.Entities;
using WebApp.ObserverPattern.Events;
using WebApp.ObserverPattern.Models;

namespace WebApp.ObserverPattern.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMediator _mediator;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            IMediator mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mediator = mediator;
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
                //_userObserverSubject.NotifyObservers(appUser);
                await _mediator.Publish(new UserCreatedEvent() { AppUser = appUser });
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