using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Account
{
    public class AccountViewModel
    {
        public LoginVeiwModel LogingViewModel { get; set; }
        public RegisterViewModel RegisterViewModel { get; set; }
    }

    public class LoginVeiwModel
    {
        [Required(ErrorMessage = "Данное поле обязательно")]

        public string Login {  get; set; }

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

        public string RepeatPasswrod { get; set; }

    }
}
