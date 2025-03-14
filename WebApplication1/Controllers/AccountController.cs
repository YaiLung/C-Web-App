using Microsoft.AspNetCore.Mvc;
namespace WebApplication1.Controllers
{
    public class AccountController : TodoBaseController

    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
