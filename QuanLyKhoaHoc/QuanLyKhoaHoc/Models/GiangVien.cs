using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhoaHoc.Models
{
    public class GiangVien
    {
        [Key]
        [Display(Name = "Mã giảng viên")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ tên giảng viên.")]
        [StringLength(100, ErrorMessage = "Họ tên không vượt quá 100 ký tự.")]
        [Display(Name = "Họ và tên")]
        public string strHoTen { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [EmailAddress(ErrorMessage = "Định dạng email không hợp lệ.")]
        [StringLength(150)]
        [Display(Name = "Địa chỉ Email")]
        public string strEmail { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        [Phone(ErrorMessage = "Định dạng số điện thoại không hợp lệ.")]
        [StringLength(15)]
        [Display(Name = "Số điện thoại")]
        public string strSoDienThoai { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập chuyên môn.")]
        [StringLength(200)]
        [Display(Name = "Chuyên môn")]
        public string strChuyenMon { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số năm kinh nghiệm.")]
        [Range(0, 50, ErrorMessage = "Số năm kinh nghiệm không hợp lệ (0 - 50 năm).")]
        [Display(Name = "Số năm kinh nghiệm")]
        public int SoNamKinhNghiem { get; set; }

        public virtual ICollection<KhoaHoc> lstKhoaHocs { get; set; } = new List<KhoaHoc>();
    }
}
