using System.ComponentModel.DataAnnotations.Schema;

namespace saglik.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }

        public Category? Category { get; set; } // Nullable olmalı

        public string Price { get; set; }
        public string Stock { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public string? ImageUrl { get; set; }
    }
}
