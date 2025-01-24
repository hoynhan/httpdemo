using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HttpDemo.Controllers
{
    public class ResponseDemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult TestOk()
        {
            return Ok("Test");
        }
        public IActionResult TestNotOk()
        {
            return BadRequest("Test");
        }
        public IActionResult TestModifyHeader()
        {
            //Response.Headers.Add("CustomHeader", "CustomValue");// Not recommend using this method
            Response.Headers.Append("CustomHeader", "CustomValue");// Recommend using this method
            FileInfo file = new FileInfo("wwwroot/images/1.jpg");
            Response.ContentType = "application/json";

            using (var stream = file.OpenRead())
            {
                var buffer = new byte[4896];
                int bytesRead;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    Response.Body.Write(buffer, 0, bytesRead);
                }
                stream.CopyTo(Response.Body);
            }

            return Ok();
        }
    }
}
