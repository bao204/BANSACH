using Microsoft.AspNetCore.Mvc;

namespace BANSACH.Areas.User.Controllers
{
    [Area("User")]
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
