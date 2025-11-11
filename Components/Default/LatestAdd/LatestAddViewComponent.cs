using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace saglik.Components.Default.LatestAdd
{
    public class LatestAddViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public LatestAddViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Son eklenen 12 ürünü Id'ye göre tersten sırala
            var latestProducts = await _context.Products
                .OrderByDescending(p => p.Id)
                .Take(8)
                .ToListAsync();

            return View(latestProducts);
        }
    }
}
