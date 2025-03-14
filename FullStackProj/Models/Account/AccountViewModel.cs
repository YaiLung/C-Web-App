using System.ComponentModel.DataAnnotations;

namespace FullStackProj.Models.Account
{
    public class AccountViewModel
    {
        public LoginViewModel LogingViewModel { get; set; }
        public RegisterViewModel RegisterViewModel { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Данное поле обязательно")]

        public string Login { get; set; }

        [Required(ErrorMessage = "Данное поле обязательно")]

        public string Password { get; set; }

    }
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Данное поле обязательно")]

        public string Login { get; set; }

        [Required(ErrorMessage = "Данное поле обязательно")]

        public string Password { get; set; }

        [Required(ErrorMessage = "Данное поле обязательно")]
        [Compare("Password", ErrorMessage = "Пороли не совпадают")]

        public string RepeatPassword { get; set; }

    }
}
