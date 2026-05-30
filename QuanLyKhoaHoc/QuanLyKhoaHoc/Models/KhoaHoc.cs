using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuanLyKhoaHoc.Enums;
using QuanLyKhoaHoc.Helpers;

namespace QuanLyKhoaHoc.Models
{
    public class KhoaHoc
    {
        [Key]
        [Display(Name = "Mã khóa học")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên khóa học.")]
        [StringLength(200, ErrorMessage = "Tên khóa học không vượt quá 200 ký tự.")]
        [Display(Name = "Tên khóa học")]
        public string strTenKhoaHoc { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Mô tả khóa học")]
        public string strMoTa { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn trạng thái.")]
        [Display(Name = "Trạng thái")]
        public TrangThaiKhoaHoc TrangThai { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập học phí.")]
        [Range(0, double.MaxValue, ErrorMessage = "Học phí phải lớn hơn hoặc bằng 0.")]
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Học phí")]
        public decimal HocPhi { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số buổi học.")]
        [Range(1, 365, ErrorMessage = "Số buổi học phải từ 1 đến 365.")]
        [Display(Name = "Số buổi học")]
        public int SoBuoiHoc { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày bắt đầu.")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày bắt đầu")]
        public DateTime dtNgayBatDau { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày kết thúc.")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày kết thúc")]
        [KiemTraNgayKetThuc("dtNgayBatDau", ErrorMessage = "Ngày kết thúc phải diễn ra sau ngày bắt đầu!")]
        public DateTime dtNgayKetThuc { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn giảng viên.")]
        [Display(Name = "Giảng viên phụ trách")]
        public int GiangVienId { get; set; }

        [ForeignKey("GiangVienId")]
        public virtual GiangVien GiangVien { get; set; }
    }
}