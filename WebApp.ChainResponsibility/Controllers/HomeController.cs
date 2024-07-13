using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebbApp.ChainResponsibility.Context;
using WebbApp.ChainResponsibility.Entities;
using WebbApp.ChainResponsibility.Models;
using WebbApp.ChainResponsibility.Services.Concrete;

namespace WebbApp.ChainResponsibility.Controllers;

public class HomeController : Controller
{
    private readonly AppIdentityDbContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, AppIdentityDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
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

    public async Task<IActionResult> SendEmail()
    {
        var products = await _context.Products.ToListAsync();
        var excelProcessHandler = new ExcelProcessHandler<Product>();
        var zipFileHandler = new ZipFileProcessHandler<Product>();
        var emailProcessHandler = new SendEmailProcessHandler("products.zip", "efeonier@outlook.com");
        excelProcessHandler.SetNext(zipFileHandler).SetNext(emailProcessHandler);
        excelProcessHandler.Handle(products);
        return View(nameof(Index));
    }
}
