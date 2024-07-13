using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.CompositePattern.Context;
using WebApp.CompositePattern.Entities;
using WebApp.CompositePattern.Services.Concrete;

namespace WebApp.CompositePattern.Controllers;

[Authorize]
public class CategoryController : Controller {
    private readonly AppIdentityDbContext _context;

    public CategoryController(AppIdentityDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        //// category==> bookcomposite;
        //// book ==> bookcomponent;
        var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        var categories = await _context.Categories.Include(i => i.Books).Where(w => w.UserId == userId).OrderBy(o => o.Id).ToListAsync();

        var menu = GetMenus(categories, new Category() { Id = 0, Name = "Top Category" }, new BookComposite(0, "Top Menu"));
        ViewBag.menus = menu;
        ViewBag.selectList = menu.Components.SelectMany(s => ((BookComposite)s).GetSelectListItems(""));
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(int categoryId, string bookName)
    {
        await _context.Books.AddAsync(new Book()
        {
            CategoryId = categoryId,
            Name = bookName,
        });

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private static BookComposite GetMenus(List<Category> categories, Category topCategory, BookComposite topComposite, BookComposite last = null)
    {
        categories.Where(w => w.RefId == topCategory.Id).ToList().ForEach(categoryItem => {
            var bookComposite = new BookComposite(categoryItem.Id, categoryItem.Name);
            categoryItem.Books.ToList().ForEach(bookItem => {
                bookComposite.Add(new BookComponent(bookItem.Id, bookItem.Name));
            });

            if (last is not null){
                last.Add(bookComposite);
            }
            else{
                topComposite.Add(bookComposite);
            }
            GetMenus(categories, categoryItem, topComposite, bookComposite);
        });

        return topComposite;
    }
}
