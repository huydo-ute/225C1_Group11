using QuanLyKhoaHoc.Enums;
namespace QuanLyKhoaHoc.Models
{
    public class KhoaHoc
    {
        public int Id { get; set; }

        public string strTenKhoaHoc { get; set; }
        public string strMoTa { get; set; }
        public TrangThaiKhoaHoc TrangThai { get; set; }
        public decimal HocPhi { get; set; }
        public int SoBuoiHoc { get; set; }

        public DateTime dtNgayBatDau { get; set; }
        public DateTime dtNgayKetThuc { get; set; }

        public int GiangVienId { get; set; }
        public virtual GiangVien GiangVien { get; set; }
    }
}
