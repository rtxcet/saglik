using System.ComponentModel.DataAnnotations;

namespace saglik.Models
{
    public class UserEditViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Kullanıcı Adı zorunludur.")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Yeni Şifre")]
        public string? Password { get; set; }  // artık zorunlu değil
    }
}
