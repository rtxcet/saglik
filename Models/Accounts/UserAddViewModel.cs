using System.ComponentModel.DataAnnotations;

namespace saglik.Models
{
    public class UserAddViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
