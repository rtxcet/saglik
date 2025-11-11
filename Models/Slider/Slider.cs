using System.ComponentModel.DataAnnotations.Schema;

namespace saglik.Models
{
    public class Slider
    {
        public int Id { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public string? ImageUrl { get; set; }
    }
}
