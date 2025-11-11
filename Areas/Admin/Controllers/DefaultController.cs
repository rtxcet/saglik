using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using saglik.Models;

namespace saglik.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DefaultController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public DefaultController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var home = await _context.Homes.FirstOrDefaultAsync();

            if (home == null)
            {
                home = new Home
                {
                    Title = "",
                    Description = "",
                    WhattsapPhone = "",
                    Phone = "",
                    ImageUrl = "",
                    Mail = "",
                    Adress = "",
                    Maps = "",
                    GoogleTag = "",
                };

                _context.Homes.Add(home);
                await _context.SaveChangesAsync();
            }

            ViewBag.Message = TempData["SuccessMessage"];
            return View(home);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Home model)
        {
            var home = await _context.Homes.FirstOrDefaultAsync();
            if (home == null)
                return NotFound();

            // Metin alanlarını güncelle
            home.Title = model.Title;
            home.Description = model.Description;
            home.WhattsapPhone = model.WhattsapPhone;
            home.Phone = model.Phone;
            home.Mail = model.Mail;
            home.Adress = model.Adress;
            home.Maps = model.Maps;
            home.GoogleTag = model.GoogleTag;

            // 📸 Görsel yükleme işlemi
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                // Klasör yolu
                string uploadDir = Path.Combine(_env.WebRootPath, "uploads/home");
                if (!Directory.Exists(uploadDir))
                    Directory.CreateDirectory(uploadDir);

                // Benzersiz dosya adı oluştur
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                string filePath = Path.Combine(uploadDir, fileName);

                // Dosyayı yükle
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(fileStream);
                }

                // Eski resmi sil
                if (!string.IsNullOrEmpty(home.ImageUrl))
                {
                    string oldImagePath = Path.Combine(_env.WebRootPath, home.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                        System.IO.File.Delete(oldImagePath);
                }

                // Yeni resmin yolunu kaydet
                home.ImageUrl = "/uploads/home/" + fileName;
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Başarıyla güncellendi.";

            return RedirectToAction("Index");
        }
    }
}
