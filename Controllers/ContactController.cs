using Microsoft.AspNetCore.Mvc;

namespace ContactManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
