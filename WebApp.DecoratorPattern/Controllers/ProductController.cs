using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.DecoratorPattern.Entities;
using WebApp.DecoratorPattern.Repositories.Interfaces;

namespace WebApp.DecoratorPattern.Controllers {
    [Authorize]
    public class ProductsController : Controller {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IActionResult> Index()
        {
            var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

            return View(await _productRepository.GetAll(userId));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (!ModelState.IsValid){
                return View();
            }
            if (id == null){
                return NotFound();
            }

            var product = await _productRepository.GetById(id.Value);
            if (product == null){
                return NotFound();
            }

            return View(product);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Stock")] Product product)
        {
            if (ModelState.IsValid){
                var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                product.UserId = userId;
                await _productRepository.Create(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (!ModelState.IsValid){
                return View();
            }
            if (id == null){
                return NotFound();
            }

            var product = await _productRepository.GetById(id.Value);
            if (product == null){
                return NotFound();
            }
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Stock,UserId")] Product product)
        {
            if (id != product.Id){
                return NotFound();
            }

            if (!ModelState.IsValid)
                return View(product);

            try{
                await _productRepository.Update(product);
            }
            catch (DbUpdateConcurrencyException){
                if (!ProductExists(product.Id)){
                    return NotFound();
                }
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (!ModelState.IsValid){
                return View();
            }
            if (id == null){
                return NotFound();
            }
            var product = await _productRepository.GetById(id.Value);
            if (product == null){
                return NotFound();
            }

            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!ModelState.IsValid){
                return RedirectToAction(nameof(Index));
            }
            var product = await _productRepository.GetById(id);

            await _productRepository.Delete(product);
            return RedirectToAction(nameof(Index));
        }
        private bool ProductExists(int id)
        {
            return _productRepository.GetById(id) != null;
        }
    }
}
