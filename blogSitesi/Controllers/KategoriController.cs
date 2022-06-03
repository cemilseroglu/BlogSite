using blogSitesi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace blogSitesi.Controllers
{
    public class KategoriController : Controller
    {
        private readonly BlogContext _db;
        public KategoriController(BlogContext db)
        {
            _db = db;
        }
        // GET: KategoriController
        public async Task<IActionResult> Index()
        {
            return View(await _db.Kategoriler.ToListAsync());
        }

        // GET: KategoriController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            { 
            return NotFound();
            }
            var kategori = await _db.Kategoriler.FirstOrDefaultAsync(m => m.KategoriId == id);

            if(kategori == null)
            {
                return NotFound();
            }

            return View(kategori);
        }

        // GET: KategoriController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KategoriController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KategoriId,KategoriAdi")] Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                _db.Add(kategori);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kategori);
        }

        // GET: KategoriController/Edit/5
        public async Task<ActionResult> Edit(int? id)  
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategori = await _db.Kategoriler.FindAsync(id);
            if(kategori == null)
            {
                return NotFound();
            }

            return View(kategori);
        }

        // POST: KategoriController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KategoriId,KategoriAdi")] Kategori kategori)
        {
            if (id != kategori.KategoriId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(kategori);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KategoriExists(kategori.KategoriId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(kategori);
        }

        // GET: KategoriController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategori = await _db.Kategoriler
                .FirstOrDefaultAsync(m => m.KategoriId == id);
            if (kategori == null)
            {
                return NotFound();
            }

            return View(kategori);
        }

        // POST: KategoriController/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kategori = await _db.Kategoriler.FindAsync(id);
            _db.Kategoriler.Remove(kategori);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KategoriExists(int id)
        {
            return _db.Kategoriler.Any(e => e.KategoriId == id);
        }
    }
}
