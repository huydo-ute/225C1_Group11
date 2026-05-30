using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoaHoc.Helpers
{
    public class KiemTraNgayKetThucAttribute : ValidationAttribute
    {
        private readonly string _tenThuocTinhNgayBatDau;
        public KiemTraNgayKetThucAttribute(string tenThuocTinhNgayBatDau)
        {
            _tenThuocTinhNgayBatDau = tenThuocTinhNgayBatDau;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyInfo = validationContext.ObjectType.GetProperty(_tenThuocTinhNgayBatDau);

            if (propertyInfo == null)
            {
                return new ValidationResult($"Không tìm thấy thuộc tính {_tenThuocTinhNgayBatDau} trong model.");
            }
            var giaTriNgayBatDau = propertyInfo.GetValue(validationContext.ObjectInstance, null);

            if (value is DateTime ngayKetThuc && giaTriNgayBatDau is DateTime ngayBatDau)
            {
                if (ngayKetThuc <= ngayBatDau)
                {
                    return new ValidationResult(ErrorMessage ?? "Ngày kết thúc phải lớn hơn ngày bắt đầu.");
                }
            }
            return ValidationResult.Success;
        }
    }
}