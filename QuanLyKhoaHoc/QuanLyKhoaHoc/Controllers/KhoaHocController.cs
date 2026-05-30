using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoaHoc.Data;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Controllers
{
    public class KhoaHocController : Controller
    {
        private readonly AppDbContext _context;

        public KhoaHocController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            var khoaHocs = _context.KhoaHocs.Include(k => k.GiangVien).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                khoaHocs = khoaHocs.Where(s => s.strTenKhoaHoc.Contains(searchString));
            }

            return View(await khoaHocs.ToListAsync());
        }
        public IActionResult Create()
        {
            ViewData["GiangVienId"] = new SelectList(_context.GiangViens, "Id", "strHoTen");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,strTenKhoaHoc,strMoTa,TrangThai,HocPhi,SoBuoiHoc,dtNgayBatDau,dtNgayKetThuc,GiangVienId")] KhoaHoc khoaHoc)
        {
            if (ModelState.IsValid)
            {
                bool isIdExists = await _context.KhoaHocs.AnyAsync(k => k.Id == khoaHoc.Id);
                if (isIdExists)
                {
                    ModelState.AddModelError("Id", "Mã khóa học này đã tồn tại. Vui lòng nhập mã khác.");
                    ViewData["GiangVienId"] = new SelectList(_context.GiangViens, "Id", "strHoTen", khoaHoc.GiangVienId);
                    return View(khoaHoc);
                }

                _context.Add(khoaHoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["GiangVienId"] = new SelectList(_context.GiangViens, "Id", "strHoTen", khoaHoc.GiangVienId);
            return View(khoaHoc);
        }
    }
}