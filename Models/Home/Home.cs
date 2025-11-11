using System.ComponentModel.DataAnnotations.Schema;

namespace saglik.Models
{
    public class Home
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string WhattsapPhone { get; set; }
        public string Phone { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public string? ImageUrl { get; set; }

        public string Mail { get; set; }
        public string Adress { get; set; }
        public string Maps { get; set; }
        public string GoogleTag { get; set; }

    }
}
