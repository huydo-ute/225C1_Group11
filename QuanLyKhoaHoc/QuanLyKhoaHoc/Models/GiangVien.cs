namespace QuanLyKhoaHoc.Models
{
    public class GiangVien
    {
        public int Id { get; set; }

        public string strHoTen { get; set; }
        public string strEmail { get; set; }
        public string strSoDienThoai { get; set; }

        public string strChuyenMon { get; set; }
        public int SoNamKinhNghiem { get; set; }

        public virtual ICollection<KhoaHoc> lstKhoaHocs { get; set; } = new List<KhoaHoc>();
    }
}
