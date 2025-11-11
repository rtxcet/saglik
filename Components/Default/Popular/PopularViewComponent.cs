using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using saglik.Models;

namespace saglik.Components.Default.Popular
{
    public class PopularViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public PopularViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _context.Categories
                .Where(c => c.IsPopular)
                .ToListAsync();

            var popularCategoryViewModels = new List<PopularCategoryViewModel>();

            foreach (var category in categories)
            {
                var products = await _context.Products
                    .Where(p => p.CategoryId == category.Id)
                    .ToListAsync();

                if (products.Any())
                {
                    popularCategoryViewModels.Add(new PopularCategoryViewModel
                    {
                        CategoryName = category.Title,
                        Products = products
                    });
                }
            }

            return View(popularCategoryViewModels);
        }
    }
}
