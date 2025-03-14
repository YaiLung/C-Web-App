using FullStackProj.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoListDomain;
using ToDoListDomain.Enities;

namespace FullStackProj.Controllers
{
    public class AccountController : Controller
    {
        private readonly TodoListContent _context;

        public AccountController(TodoListContent context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync([Bind(Prefix = "LogingViewModel")] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", new AccountViewModel { LogingViewModel = model });
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);

            if (user is null)
            {
                ViewBag.Error = "Некорректное имя или пароль";
                return View("Index", new AccountViewModel { LogingViewModel = model });
            }

            await AuthenticateAsync(user);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync([Bind(Prefix = "RegisterViewModel")] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", new AccountViewModel { RegisterViewModel = model });
            }

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Login == model.Login);
            if (existingUser != null)
            {
                ViewBag.RegisterError = "Пользователь с таким логином уже существует";
                return View("Index", new AccountViewModel { RegisterViewModel = model });
            }

            var newUser = new User
            {
                Login = model.Login,
                Password = model.Password // Здесь лучше использовать хеширование
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            await AuthenticateAsync(newUser);
            return RedirectToAction("Index", "Home");
        }

        private async Task AuthenticateAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
