using Microsoft.AspNetCore.Mvc;
using saglik.Models;

namespace saglik.Components.Default.ContactMap
{
    public class ContactMapViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var model = new Contact(); // Boş form modeli
            return View(model);        // Form sayfasına bu modeli gönderiyoruz
        }
    }
}
