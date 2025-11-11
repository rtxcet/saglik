using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using saglik.Models;

namespace saglik.Controllers
{
    public class DefaultController : Controller
    {
        private readonly AppDbContext _context;

        public DefaultController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("kategori/{id}")]
        public IActionResult ProductsByCategory(string slug, int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return NotFound();

            var products = _context.Products
                .Where(p => p.CategoryId == id)
                .Include(p => p.Category)
                .ToList();

            ViewBag.CategoryName = category.Title;

            return View(products);
        }


        public IActionResult Detail(int id)
        {
            var product = _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpGet("/products/search")]
        public async Task<IActionResult> Search(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return View("SearchResults", new List<Product>());

            var results = await _context.Products
                .Where(p => p.Name.Contains(q) || p.Description.Contains(q))
                .ToListAsync();

            return View(results);
        }

    }
}
