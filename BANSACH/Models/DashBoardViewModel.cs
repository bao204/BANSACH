using System.Reflection.Metadata;

namespace BANSACH.Models
{
    public class DashBoardViewModel
    {
        public string SelectedMenuItem { get; set; }
        //public List<ApplicationUser> Users { get; set; }
        public List<SanPham> SanPhams { get; set; }
        public List<TheLoai> TheLoais { get; set; }
        public List<NhaCungCap> NhaCungCaps { get; set; }
        //public List<Blog> Blogs { get; set; }

    }
}
