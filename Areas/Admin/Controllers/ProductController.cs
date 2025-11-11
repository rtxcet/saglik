using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using saglik.Models;

namespace saglik.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p => p.Category) // Kategori bilgilerini dahil et
                .OrderByDescending(p => p.Id)
                .ToListAsync();

            ViewBag.Message = TempData["SuccessMessage"];
            return View(products);
        }


        public async Task<IActionResult> Add()
        {
            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Product product)
        {
            if (ModelState.IsValid)
            {
                // Eğer görsel yükleme varsa (opsiyonel olarak işle)
                if (product.ImageFile != null)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/product");

                    // Klasör yoksa oluştur
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(product.ImageFile.FileName);
                    var path = Path.Combine(uploadsFolder, fileName);

                    // Dosyayı asenkron şekilde kaydet (await çok önemli!)
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await product.ImageFile.CopyToAsync(stream);
                    }

                    product.ImageUrl = "/uploads/product/" + fileName;
                }


                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Ürün başarıyla eklendi.";
                return RedirectToAction("Index");
            }

            // Hatalıysa kategorileri tekrar gönder
            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Title", product.CategoryId);
            return View(product);
        }



        // ProductController.cs - Silme ve düzenleme işlemleri

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Title", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var existingProduct = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
                if (existingProduct == null)
                    return NotFound();

                // Yeni görsel yüklendiyse
                if (product.ImageFile != null)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/product");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(product.ImageFile.FileName);
                    var path = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await product.ImageFile.CopyToAsync(stream);
                    }

                    // Eski resmi sil
                    if (!string.IsNullOrEmpty(existingProduct.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingProduct.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                            System.IO.File.Delete(oldImagePath);
                    }

                    product.ImageUrl = "/uploads/product/" + fileName;
                }
                else
                {
                    product.ImageUrl = existingProduct.ImageUrl; // Aynı kalacaksa
                }

                _context.Update(product);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Ürün başarıyla güncellendi.";
                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Title", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            // Resmi sil
            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", product.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Ürün başarıyla silindi.";
            return RedirectToAction("Index");
        }
    }
}
