using Microsoft.AspNetCore.Mvc;

namespace BANSACH.Areas.User.Controllers
{
    [Area("User")]
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
