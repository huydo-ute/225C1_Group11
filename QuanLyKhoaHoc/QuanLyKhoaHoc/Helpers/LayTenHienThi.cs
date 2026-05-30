using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace QuanLyKhoaHoc.Helpers
{
    public static class EnumHelper
    {
        public static string LayTenHienThi(this Enum giaTriEnum)
        {
            if (giaTriEnum == null) return string.Empty;

            Type loaiEnum = giaTriEnum.GetType();
            string tenTruong = giaTriEnum.ToString();

            FieldInfo fieldInfo = loaiEnum.GetField(tenTruong);

            if (fieldInfo == null) return tenTruong;
            var thuocTinhDisplay = fieldInfo.GetCustomAttribute<DisplayAttribute>();
            return thuocTinhDisplay?.Name ?? tenTruong;
        }
    }
}