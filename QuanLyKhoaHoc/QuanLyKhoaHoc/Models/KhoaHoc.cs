using QuanLyKhoaHoc.Enums
namespace QuanLyKhoaHoc.Models
{
    public class KhoaHoc
    {
        public int Id { get; set; }

        public string TenKhoaHoc { get; set; }
        public string MoTa { get; set; }
        public TrangThaiKhoaHoc TrangThai { get; set; }
        public decimal HocPhi { get; set; }
        public int SoBuoiHoc { get; set; }

        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }

        public int GiangVienId { get; set; }
        public GiangVien GiangVien { get; set; }
    }
}
