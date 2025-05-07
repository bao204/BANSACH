using BANSACH.Data;
using BANSACH.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BANSACH.Controllers
{
	[Area("User")]
	public class HomeController : Controller
	{
        private readonly ApplicationDbContext _db;

        private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
		{
			_logger = logger;
            _db = db;
        }

		public IActionResult Index()
		{
            var products = _db.SanPham.ToList();
            var blogs = _db.Blogs.ToList();

            // Lấy danh sách 6 sản phẩm nổi bật
            var featuredProducts = _db.SanPham.Take(6).ToList();

            // Lấy danh sách 6 sản phẩm mới
            var newArrivalProducts = _db.SanPham.Skip(6).Take(6).ToList();

            ViewBag.FeaturedProducts = featuredProducts;
            ViewBag.NewArrivalProducts = newArrivalProducts;
            ViewBag.Blogs = blogs;

            return View(products);
        }

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}