using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoaHoc.Enums
{
    public enum TrangThaiKhoaHoc
    {
        [Display(Name = "Sắp mở")]
        SapMo = 0,
        [Display(Name = "Đang học")]
        DangHoc = 1,
        [Display(Name = "Đã kết thúc")]
        DaKetThuc = 2
    }
}
