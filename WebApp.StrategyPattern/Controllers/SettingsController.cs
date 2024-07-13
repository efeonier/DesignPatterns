using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.StrategyPattern.Entities;
using WebApp.StrategyPattern.Enums;
using WebApp.StrategyPattern.Models;

namespace WebApp.StrategyPattern.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public SettingsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var settingsModel = new SettingsModel();
            if (User.Claims.Any(w => w.Type == SettingsModel.ClaimDatabaseType))
            {
                settingsModel.DatabaseType =
                    (EDatabaseType)int.Parse(User.Claims.First(x => x.Type == SettingsModel.ClaimDatabaseType).Value);
            }
            else
            {
                settingsModel.DatabaseType = SettingsModel.GetDefaultDatabaseType;
            }

            return View(settingsModel);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeDatabase(int databaseType)
        {
            if (User.Identity == null || string.IsNullOrEmpty(User.Identity.Name))
            {
                return RedirectToAction(nameof(AccountController.Login), "Account");
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction(nameof(AccountController.Login), "Account");
            }

            var newClaim = new Claim(SettingsModel.ClaimDatabaseType, databaseType.ToString());
            var claims = await _userManager.GetClaimsAsync(user);

            var hasDatabaseTypeClaim = claims.FirstOrDefault(w => w.Type == SettingsModel.ClaimDatabaseType);
            if (hasDatabaseTypeClaim != null)
            {
                await _userManager.ReplaceClaimAsync(user, hasDatabaseTypeClaim, newClaim);
            }
            else
            {
                await _userManager.AddClaimAsync(user, newClaim);
            }

            await _signInManager.SignOutAsync();
            var authenticateResult = await HttpContext.AuthenticateAsync();
            if (authenticateResult.Properties is null)
            {
                return RedirectToAction(nameof(AccountController.Login), "Account");
            }

            await _signInManager.SignInAsync(user, authenticateResult.Properties);

            return RedirectToAction(nameof(Index));
        }
    }
}