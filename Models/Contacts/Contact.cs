using System.ComponentModel.DataAnnotations;

namespace saglik.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad soyad zorunludur.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunludur.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Açıklama zorunludur.")]
        public string Description { get; set; }
    }
}
