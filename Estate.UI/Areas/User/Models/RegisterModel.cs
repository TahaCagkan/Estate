﻿using System.ComponentModel.DataAnnotations;
namespace Estate.UI.Areas.User.Models
{
    public class RegisterModel
    {

        [Required(ErrorMessage = "Boş geçilemez")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Boş geçilemez")]
        [DataType(DataType.Password, ErrorMessage = "Şifre tipinde değil")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Boş geçilemez")]
        [DataType(DataType.Password, ErrorMessage = "Şifre tipinde değil")]
        [Compare("Password", ErrorMessage ="Şifreler uyuşmuyor")]
        public string RePassword { get; set; }

        [Required(ErrorMessage = "Boş geçilemez")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Boş geçilemez")]
        public string FullName { get; set; }
    }
}
