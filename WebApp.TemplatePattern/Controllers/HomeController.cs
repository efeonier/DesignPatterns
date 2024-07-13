using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApp.TemplatePattern.Entities;
using WebApp.TemplatePattern.Models;

namespace WebApp.TemplatePattern.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<AppUser> _userManager;

    public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var userList = await _userManager.Users.ToListAsync();
        if (User.Identity.IsAuthenticated)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user is not null)
                userList = userList.Where(w => w.Id != user.Id).ToList();
        }

        return View(userList);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
