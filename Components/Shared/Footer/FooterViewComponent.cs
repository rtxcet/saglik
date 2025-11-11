using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace saglik.Components.Shared.Header
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public FooterViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var siteSettings = await _context.Homes.FirstOrDefaultAsync();
            return View(siteSettings);
        }
    }
}
