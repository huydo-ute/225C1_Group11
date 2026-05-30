using Microsoft.EntityFrameworkCore;
using QuanLyKhoaHoc.Models;

namespace QuanLyKhoaHoc.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<KhoaHoc> KhoaHocs { get; set; }
        public DbSet<GiangVien> GiangViens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<KhoaHoc>()
                .HasOne(k => k.GiangVien)
                .WithMany(g => g.lstKhoaHocs)
                .HasForeignKey(k => k.GiangVienId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}