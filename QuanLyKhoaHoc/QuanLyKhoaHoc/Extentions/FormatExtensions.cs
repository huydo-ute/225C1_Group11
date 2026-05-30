using System.Globalization;

namespace QuanLyKhoaHoc.Extensions
{
    public static class FormatExtensions
    {
        public static string ToHocPhiVN(this decimal hocPhi)
        {
            return hocPhi.ToString("N0", new CultureInfo("vi-VN")) + " VNĐ";
        }

        public static string ToNgayVN(this DateTime ngay)
        {
            return ngay.ToString("dd/MM/yyyy");
        }

    }
}