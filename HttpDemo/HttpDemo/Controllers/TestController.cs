using Microsoft.AspNetCore.Mvc;

namespace HttpDemo.Controllers
{
    public class TestController : Controller
    {
        public IActionResult TestGetSession()
        {
            var session = HttpContext.GetSession();

            session.SetString("Name", "123456789ABC");

            session = HttpContext.GetSession();// get session again // explain how the AddScoped works

            var value = session.GetString("Name");

            if (value == "123456789ABC")
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
