using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BANSACH.Models;
using BANSACH.Data;

namespace BANSACH.ViewComponents
{
    public class TheLoaiViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db; // Thay thế YourDbContext bằng tên của DbContext của bạn

        public TheLoaiViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await GetCategoriesAsync();
            return View("Default", categories);
        }

        private async Task<List<TheLoai>> GetCategoriesAsync()
        {
            // Thực hiện truy vấn từ cơ sở dữ liệu để lấy danh sách thể loại
            var categoryList = await _db.TheLoai.ToListAsync();
            return categoryList;
        }
    }
}
