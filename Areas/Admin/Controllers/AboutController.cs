using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using saglik.Models;
using saglik.Models.About;

namespace saglik.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;

        public AboutController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var service = await _context.Abouts.FirstOrDefaultAsync();

            if (service == null)
            {
                service = new About
                {
                    Title = "",
                    Description = "",
                };

                _context.Abouts.Add(service);
                await _context.SaveChangesAsync();
            }

            ViewBag.Message = TempData["SuccessMessage"];
            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(About model)
        {
            var service = await _context.Abouts.FirstOrDefaultAsync();
            if (service == null)
                return NotFound();

            // Metin alanlarını güncelle    
            service.Title = model.Title;
            service.Description = model.Description;

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Başarıyla güncellendi.";

            return RedirectToAction("Index");
        }
    }
}
