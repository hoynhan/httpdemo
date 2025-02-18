using Microsoft.AspNetCore.Mvc;

namespace HttpDemo.Controllers
{
    public class CustomSessionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
