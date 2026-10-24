using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLTV.Data;
using QLTV.Models;

namespace QLTV.Controllers;

public class TacGiaController : Controller
{
    private readonly QLTVDBContext _context;

    public TacGiaController(QLTVDBContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var tacGiaList = await _context.TacGia
            .Include(t => t.SachList)
            .OrderBy(t => t.TenTacGia)
            .ToListAsync();

        return View(tacGiaList);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var tacGia = await _context.TacGia
            .Include(t => t.SachList)
            .FirstOrDefaultAsync(t => t.TacGiaId == id);

        if (tacGia == null) return NotFound();
        return View(tacGia);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("TenTacGia,QuocTich")] TacGia tacGia)
    {
        if (ModelState.IsValid)
        {
            _context.Add(tacGia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(tacGia);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var tacGia = await _context.TacGia.FindAsync(id);
        if (tacGia == null) return NotFound();

        return View(tacGia);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("TacGiaId,TenTacGia,QuocTich")] TacGia tacGia)
    {
        if (id != tacGia.TacGiaId) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(tacGia);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TacGiaExists(tacGia.TacGiaId)) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(tacGia);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var tacGia = await _context.TacGia
            .Include(t => t.SachList)
            .FirstOrDefaultAsync(t => t.TacGiaId == id);

        if (tacGia == null) return NotFound();
        return View(tacGia);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var tacGia = await _context.TacGia
            .Include(t => t.SachList)
            .FirstOrDefaultAsync(t => t.TacGiaId == id);

        if (tacGia != null)
        {
            if (tacGia.SachList.Count > 0)
            {
                ModelState.AddModelError(string.Empty, "Không thể xóa tác giả đang có sách. Hãy chuyển hoặc xóa sách trước.");
                return View("Delete", tacGia);
            }

            _context.TacGia.Remove(tacGia);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool TacGiaExists(int id) => _context.TacGia.Any(e => e.TacGiaId == id);
}
