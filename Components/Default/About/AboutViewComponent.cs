using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace saglik.Components.Default.Slider
{
    public class AboutViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public AboutViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            // Kategoriler
            var service = await _context.Abouts.FirstOrDefaultAsync();

            return View(service);
        }
    }
}
