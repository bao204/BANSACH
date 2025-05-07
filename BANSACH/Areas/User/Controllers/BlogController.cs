using Microsoft.AspNetCore.Mvc;
using System.Linq;
using BANSACH.Data;

namespace BANSACH.Areas.User.Controllers
{
    [Area("User")]
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BlogController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var blogs = _db.Blogs.ToList(); // Lấy danh sách blog từ cơ sở dữ liệu
            return View(blogs);
        }

        public IActionResult BlogDetail(int id)
        {
            var blog = _db.Blogs.FirstOrDefault(b => b.Id == id);
            if (blog == null)
            {
                return NotFound(); // Xử lý trường hợp không tìm thấy blog
            }
            return View(blog);
        }
    }
}
