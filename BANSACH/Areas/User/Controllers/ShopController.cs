using BANSACH.Data;
using BANSACH.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Azure;

namespace BANSACH.Areas.User.Controllers
{
    [Area("User")]
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _appEnvironment;

        public ShopController(ApplicationDbContext db, IWebHostEnvironment appEnvironment)
        {
            _db = db;
            _appEnvironment = appEnvironment;
        }






		public IActionResult Index(string searchTerm, string theloai, decimal? minPrice, decimal? maxPrice, string searchText, string tag)
        {
            IEnumerable<SanPham> sanpham;

            // Lấy danh sách thể loại
            var theloaiList = _db.TheLoai.ToList();

            IQueryable<SanPham> sanPhamQuery = _db.SanPham;


            if (!string.IsNullOrEmpty(theloai))
            {
                // Xử lý tìm kiếm theo thể loại
                sanpham = _db.SanPham.Where(sp => sp.TheLoai.Name == theloai).ToList();
            }
            else if (!string.IsNullOrEmpty(searchTerm))
            {
                // Sử dụng LINQ để tìm kiếm theo tên sản phẩm, không phân biệt chữ hoa, chữ thường
                sanpham = _db.SanPham
                    .Where(sp => sp.Name != null && sp.Name.ToLower().Contains(searchTerm.ToLower()))
                    .ToList();
            }
            else
            {
                // Lấy tất cả sản phẩm nếu không có thể loại hoặc tên sản phẩm được chọn
                sanpham = _db.SanPham.ToList();
            }



		



			// Lọc theo giá
			if (minPrice.HasValue)
            {
                sanpham = sanpham.Where(sp => sp.Price >= minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                sanpham = sanpham.Where(sp => sp.Price <= maxPrice.Value);
            }


            // Lưu giữ giá trị searchTerm và theloai để truyền lại cho view
            ViewBag.SearchTerm = searchTerm;
            ViewBag.TheLoai = theloai;
            ViewBag.TheLoaiList = theloaiList; // Truyền danh sách thể loại đến view


            ViewBag.SanPham = sanpham;
            return View();
        }


        [HttpGet]
        public IActionResult Details(int sanphamId)
        {
            IEnumerable<SanPham> sanpham = _db.SanPham.ToList();
            ViewBag.SanPham = sanpham;
            GioHang giohang = new GioHang()
            {
                SanPhamId = sanphamId,
                SanPham = _db.SanPham
            .Include(sp => sp.TheLoai)
            .Include(sp => sp.NhaCungCap)
            .FirstOrDefault(sp => sp.Id == sanphamId),
                Quantity = 1
            };

            return View(giohang);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Details(GioHang giohang)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            giohang.ApplicationUserId = claim.Value;

            // Retrieve the existing item in the cart
            var giohangdb = _db.GioHang.FirstOrDefault(sp => sp.SanPhamId == giohang.SanPhamId && sp.ApplicationUserId == giohang.ApplicationUserId);

            if (giohangdb == null)
            {
                // Item is not in the cart, add a new entry
                _db.GioHang.Add(giohang);
            }
            else
            {
                // Item is already in the cart, update the quantity
                giohangdb.Quantity += giohang.Quantity;
            }

            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
