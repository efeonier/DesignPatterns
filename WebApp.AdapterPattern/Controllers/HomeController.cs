using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApp.AdapterPattern.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.AdapterPattern.Services.Interfaces;

namespace WebApp.AdapterPattern.Controllers;

public class HomeController : Controller {
    private readonly ILogger<HomeController> _logger;
    private readonly IImageProcess _imageProcess;
    public HomeController(ILogger<HomeController> logger, IImageProcess imageProcess)
    {
        _logger = logger;
        _imageProcess = imageProcess;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult AddWatermark()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> AddWatermark(IFormFile image)
    {
        if (image is { Length: > 0 }){
            var imageMs = new MemoryStream();
            await image.CopyToAsync(imageMs);
            _imageProcess.AddWaterMark("Efe Önier", image.FileName, imageMs);
        }
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
