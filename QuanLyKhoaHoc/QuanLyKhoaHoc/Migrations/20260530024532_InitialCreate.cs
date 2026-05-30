using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyKhoaHoc.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GiangViens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    strHoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    strEmail = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    strSoDienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    strChuyenMon = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SoNamKinhNghiem = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiangViens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KhoaHocs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    strTenKhoaHoc = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    strMoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    HocPhi = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoBuoiHoc = table.Column<int>(type: "int", nullable: false),
                    dtNgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtNgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GiangVienId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhoaHocs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KhoaHocs_GiangViens_GiangVienId",
                        column: x => x.GiangVienId,
                        principalTable: "GiangViens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KhoaHocs_GiangVienId",
                table: "KhoaHocs",
                column: "GiangVienId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KhoaHocs");

            migrationBuilder.DropTable(
                name: "GiangViens");
        }
    }
}
