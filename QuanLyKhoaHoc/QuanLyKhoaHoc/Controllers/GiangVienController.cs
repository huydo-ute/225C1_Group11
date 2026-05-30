using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoaHoc.Data;
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
            var giangViens = from m in _context.GiangViens
                             select m;
            if (!string.IsNullOrEmpty(searchString))
            {
                giangViens = giangViens.Where(s => s.strHoTen.Contains(searchString));
            }

            return View(await giangViens.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,strHoTen,strEmail,strSoDienThoai,strChuyenMon,SoNamKinhNghiem")] GiangVien giangVien)
        {
            if (ModelState.IsValid)
            {
                bool isIdExists = await _context.GiangViens.AnyAsync(g => g.Id == giangVien.Id);
                if (isIdExists)
                {
                    ModelState.AddModelError("Id", "Mã giảng viên này đã tồn tại. Vui lòng nhập mã khác.");
                    return View(giangVien);
                }
                _context.Add(giangVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(giangVien);
        }
    }
}