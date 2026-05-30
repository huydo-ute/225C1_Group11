using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoaHoc.Data;
using QuanLyKhoaHoc.Models;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyKhoaHoc.Controllers
{
    public class GiangVienController : Controller
    {
        private readonly AppDbContext _context;

        public GiangVienController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var giangViens = _context.GiangViens
                .Include(g => g.lstKhoaHocs)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                giangViens = giangViens.Where(g =>
                    g.strHoTen.Contains(searchString) ||
                    g.lstKhoaHocs.Any(k => k.strTenKhoaHoc.Contains(searchString)));
            }

            ViewData["CurrentFilter"] = searchString;

            return View(await giangViens.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var giangVien = await _context.GiangViens
                .Include(g => g.lstKhoaHocs)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (giangVien == null) return NotFound();

            return View(giangVien);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("strHoTen,strEmail,strSoDienThoai,strChuyenMon,SoNamKinhNghiem")] GiangVien giangVien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(giangVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(giangVien);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var giangVien = await _context.GiangViens.FindAsync(id);

            if (giangVien == null) return NotFound();

            return View(giangVien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,strHoTen,strEmail,strSoDienThoai,strChuyenMon,SoNamKinhNghiem")] GiangVien giangVien)
        {
            if (id != giangVien.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(giangVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GiangVienExists(giangVien.Id))
                        return NotFound();

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(giangVien);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var giangVien = await _context.GiangViens
                .Include(g => g.lstKhoaHocs)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (giangVien == null) return NotFound();

            return View(giangVien);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var giangVien = await _context.GiangViens
                .Include(g => g.lstKhoaHocs)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (giangVien == null) return NotFound();

            if (giangVien.lstKhoaHocs.Any())
            {
                ModelState.AddModelError("", "Không thể xóa giảng viên vì đang có khóa học phụ trách.");
                return View(giangVien);
            }

            _context.GiangViens.Remove(giangVien);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool GiangVienExists(int id)
        {
            return _context.GiangViens.Any(g => g.Id == id);
        }
    }
}