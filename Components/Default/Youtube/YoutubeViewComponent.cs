using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace saglik.Components.Default.Slider
{
    public class YoutubeViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
