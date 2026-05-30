namespace QuanLyKhoaHoc.Models
{
    public class GiangVien
    {
        public int Id { get; set; }

        public string HoTen { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }

        public string ChuyenMon { get; set; }
        public int SoNamKinhNghiem { get; set; }

        public ICollection<KhoaHoc> KhoaHocs { get; set; }
    }
}
