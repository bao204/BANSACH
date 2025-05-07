using BANSACH.Data;
using BANSACH.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BANSACH.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashBoardController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DashBoardController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(string selectedMenuItem)
        {
            var model = new DashBoardViewModel
            {
                SelectedMenuItem = selectedMenuItem,
                SanPhams = _db.SanPham.ToList(),
                TheLoais = _db.TheLoai.ToList(),
                NhaCungCaps = _db.NhaCungCap.ToList(),


                // Load data for other items as needed
            };

            return View(model);
        }
    }
}
