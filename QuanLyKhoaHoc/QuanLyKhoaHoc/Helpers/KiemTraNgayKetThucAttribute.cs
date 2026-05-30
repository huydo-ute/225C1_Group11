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
                return new ValidationResult("Không tìm thấy thuộc tính ngày bắt đầu.");
            }

            var giaTriNgayBatDau = propertyInfo.GetValue(validationContext.ObjectInstance);

            if (value is DateTime ngayKetThuc &&
                giaTriNgayBatDau is DateTime ngayBatDau)
            {
                if (ngayBatDau >= ngayKetThuc)
                {
                    return new ValidationResult(
                        "Ngày bắt đầu phải nhỏ hơn ngày kết thúc."
                    );
                }
            }

            return ValidationResult.Success;
        }
    }
}