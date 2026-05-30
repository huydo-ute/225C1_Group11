using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoaHoc.Data;
using QuanLyKhoaHoc.Enums;
using QuanLyKhoaHoc.Helpers;
using QuanLyKhoaHoc.Models;
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

        public async Task<IActionResult> Index(string searchString, TrangThaiKhoaHoc? trangThai)
        {
            var khoaHocs = _context.KhoaHocs
                .Include(k => k.GiangVien)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                khoaHocs = khoaHocs.Where(k =>
                    k.strTenKhoaHoc.Contains(searchString) ||
                    k.GiangVien.strHoTen.Contains(searchString));
            }

            if (trangThai.HasValue)
            {
                khoaHocs = khoaHocs.Where(k => k.TrangThai == trangThai.Value);
            }

            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentTrangThai"] = trangThai;
            ViewBag.TrangThaiList = Enum.GetValues(typeof(TrangThaiKhoaHoc))
                    .Cast<TrangThaiKhoaHoc>()
                    .Select(t => new SelectListItem
                    {
                        Value = t.ToString(),
                        Text = t.LayTenHienThi(),
                        Selected = trangThai.HasValue && trangThai.Value == t
                    })
                .ToList();

            return View(await khoaHocs.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var khoaHoc = await _context.KhoaHocs
                .Include(k => k.GiangVien)
                .FirstOrDefaultAsync(k => k.Id == id);

            if (khoaHoc == null) return NotFound();

            return View(khoaHoc);
        }

        public IActionResult Create()
        {
            ViewData["GiangVienId"] = new SelectList(_context.GiangViens, "Id", "strHoTen");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KhoaHoc khoaHoc)
        {
            ModelState.Remove("GiangVien");

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                ViewData["GiangVienId"] = new SelectList(_context.GiangViens, "Id", "strHoTen", khoaHoc.GiangVienId);
                return View(khoaHoc);
            }

            _context.KhoaHocs.Add(khoaHoc);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var khoaHoc = await _context.KhoaHocs.FindAsync(id);

            if (khoaHoc == null) return NotFound();

            ViewData["GiangVienId"] = new SelectList(_context.GiangViens, "Id", "strHoTen", khoaHoc.GiangVienId);
            return View(khoaHoc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,strTenKhoaHoc,strMoTa,TrangThai,HocPhi,SoBuoiHoc,dtNgayBatDau,dtNgayKetThuc,GiangVienId")] KhoaHoc khoaHoc)
        {
            ModelState.Remove("GiangVien");
            if (id != khoaHoc.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khoaHoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhoaHocExists(khoaHoc.Id))
                        return NotFound();

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["GiangVienId"] = new SelectList(_context.GiangViens, "Id", "strHoTen", khoaHoc.GiangVienId);
            return View(khoaHoc);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var khoaHoc = await _context.KhoaHocs
                .Include(k => k.GiangVien)
                .FirstOrDefaultAsync(k => k.Id == id);

            if (khoaHoc == null) return NotFound();

            return View(khoaHoc);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var khoaHoc = await _context.KhoaHocs.FindAsync(id);

            if (khoaHoc != null)
            {
                _context.KhoaHocs.Remove(khoaHoc);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool KhoaHocExists(int id)
        {
            return _context.KhoaHocs.Any(k => k.Id == id);
        }
    }
}