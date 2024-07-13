using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.CommandPattern.Commands;
using WebApp.CommandPattern.Context;
using WebApp.CommandPattern.Entities;
using WebApp.CommandPattern.Enums;

namespace WebApp.CommandPattern.Controllers;

public class ProductsController : Controller
{
    private readonly AppIdentityDbContext _dbContext;

    public ProductsController(AppIdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        return View(await _dbContext.Products.ToListAsync());
    }

    public async Task<IActionResult> CreateFile(int type)
    {
        var products = await _dbContext.Products.ToListAsync();
        var fileCreateInvoker = new FileCreateInvoker();
        switch ((FileType)type)
        {
            case FileType.Excel:
                var excelFile = new ExcelFile<Product>(products);
                fileCreateInvoker.SetCommand(new CreateExcelTableActionCommand<Product>(excelFile));
                break;
            case FileType.Pdf:
                var pdfFile = new PdfFile<Product>(products, HttpContext);
                fileCreateInvoker.SetCommand(new CreatePdfTableActionCommand<Product>(pdfFile));
                break;
        }

        return fileCreateInvoker.CreateFile();
    }

    public async Task<IActionResult> CreateFiles()
    {
        var products = await _dbContext.Products.ToListAsync();
        var excelFile = new ExcelFile<Product>(products);
        var pdfFile = new PdfFile<Product>(products, HttpContext);
        var fileCreateInvoker = new FileCreateInvoker();
        fileCreateInvoker.AddCommand(new CreateExcelTableActionCommand<Product>(excelFile));
        fileCreateInvoker.AddCommand(new CreatePdfTableActionCommand<Product>(pdfFile));
        var filesResult = fileCreateInvoker.CreateFiles();
        using var zipMs = new MemoryStream();
        using var archive = new ZipArchive(zipMs, ZipArchiveMode.Create);
        foreach (var item in filesResult)
        {
            var fileContent = item as FileContentResult;
            var zipFile = archive.CreateEntry(fileContent.FileDownloadName);
            await using var zipEntryStream = zipFile.Open();
            await new MemoryStream(fileContent.FileContents).CopyToAsync(zipEntryStream);
        }

        return File(zipMs.ToArray(), "application/zip", "all.zip");
    }
}
