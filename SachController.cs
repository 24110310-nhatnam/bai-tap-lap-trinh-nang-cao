using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLTV.Data;
using QLTV.Models;

namespace QLTV.Controllers;

public class SachController : Controller
{
    private readonly QLTVDBContext _context;

    public SachController(QLTVDBContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var sachList = await _context.Sach
            .Include(s => s.TacGia)
            .OrderBy(s => s.TenSach)
            .ToListAsync();

        return View(sachList);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var sach = await _context.Sach
            .Include(s => s.TacGia)
            .FirstOrDefaultAsync(s => s.SachId == id);

        if (sach == null) return NotFound();
        return View(sach);
    }

    public IActionResult Create()
    {
        LoadTacGia();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("TenSach,NamXuatBan,TacGiaId")] Sach sach)
    {
        if (ModelState.IsValid)
        {
            _context.Add(sach);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        LoadTacGia(sach.TacGiaId);
        return View(sach);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var sach = await _context.Sach.FindAsync(id);
        if (sach == null) return NotFound();

        LoadTacGia(sach.TacGiaId);
        return View(sach);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("SachId,TenSach,NamXuatBan,TacGiaId")] Sach sach)
    {
        if (id != sach.SachId) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(sach);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SachExists(sach.SachId)) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        LoadTacGia(sach.TacGiaId);
        return View(sach);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var sach = await _context.Sach
            .Include(s => s.TacGia)
            .FirstOrDefaultAsync(s => s.SachId == id);

        if (sach == null) return NotFound();
        return View(sach);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var sach = await _context.Sach.FindAsync(id);
        if (sach != null)
        {
            _context.Sach.Remove(sach);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private void LoadTacGia(object? selectedId = null)
    {
        ViewData["TacGiaId"] = new SelectList(
            _context.TacGia.OrderBy(t => t.TenTacGia),
            "TacGiaId",
            "TenTacGia",
            selectedId);
    }

    private bool SachExists(int id) => _context.Sach.Any(e => e.SachId == id);
}
