using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using saglik.Models;

namespace saglik.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SloganController : Controller
    {
        private readonly AppDbContext _context;

        public SloganController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var slogans = await _context.Slogans.ToListAsync();

            ViewBag.Message = TempData["SuccessMessage"];
            return View(slogans);
        }

        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Slogan slogan)
        {
            if (ModelState.IsValid)
            {

                _context.Slogans.Add(slogan);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Slogan başarıyla eklendi.";
                return RedirectToAction("Index");
            }

            return View(slogan);
        }

        public IActionResult Edit(int id)
        {
            var slogan = _context.Slogans.FirstOrDefault(s => s.Id == id);
            if (slogan == null)
            {
                return NotFound();
            }
            return View(slogan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Slogan slogan)
        {
            if (ModelState.IsValid)
            {
                var existingSlogan = await _context.Slogans.FindAsync(slogan.Id);
                if (existingSlogan == null)
                {
                    return NotFound();
                }

                // Alanları güncelle
                existingSlogan.Title = slogan.Title;

                _context.Update(existingSlogan);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Slogan başarıyla güncellendi.";
                return RedirectToAction("Index");
            }

            return View(slogan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var slogan = await _context.Slogans.FindAsync(id);
            if (slogan == null)
            {
                return NotFound();
            }

            _context.Slogans.Remove(slogan);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Slogan başarıyla silindi.";
            return RedirectToAction("Index");
        }
    }
}
