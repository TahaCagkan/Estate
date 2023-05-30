using System.ComponentModel.DataAnnotations;

namespace Estate.UI.Areas.User.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Kullanıcı adı boş geçilemez")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Şifre boş geçilemez")]
        [DataType(DataType.Password, ErrorMessage = "Şifre tipinde değildir.")]
        public string Password { get; set; }
    }
}
