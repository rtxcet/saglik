using System.ComponentModel.DataAnnotations;

namespace saglik.Models
{
    public class ChangeUsernameViewModel
    {
        [Required]
        [Display(Name = "Yeni Kullanıcı Adı")]
        public string NewUsername { get; set; }
    }
}
