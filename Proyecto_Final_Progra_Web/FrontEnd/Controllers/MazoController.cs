using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FrontEnd.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Final_Progra_Web.Controllers
{
    public class MazosController : Controller
    {
        private readonly ProyectoFinalWebContext _context;

        public MazosController(ProyectoFinalWebContext context)
        {
            _context = context;
        }

        // GET: Mazos
        public async Task<IActionResult> Index()
        {
            var mazos = _context.Mazos.Include(m => m.Usuario);
            return View(await mazos.ToListAsync());
        }

        // GET: Mazos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mazo = await _context.Mazos
                .Include(m => m.Usuario)
                .FirstOrDefaultAsync(m => m.MazoId == id);
            if (mazo == null)
            {
                return NotFound();
            }

            return View(mazo);
        }

        // GET: Mazos/Create
        public IActionResult Create()
        {
            ViewBag.Usuarios = new SelectList(_context.Usuarios, "UsuarioId", "NombreUsuario");
            return View();
        }

        // POST: Mazos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MazoId,UsuarioId,NombreMazo,CreadoEn")] Mazo mazo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mazo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Usuarios = new SelectList(_context.Usuarios, "UsuarioId", "NombreUsuario", mazo.UsuarioId);
            return View(mazo);
        }

        // GET: Mazos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mazo = await _context.Mazos.FindAsync(id);
            if (mazo == null)
            {
                return NotFound();
            }
            ViewBag.Usuarios = new SelectList(_context.Usuarios, "UsuarioId", "NombreUsuario", mazo.UsuarioId);
            return View(mazo);
        }

        // POST: Mazos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MazoId,UsuarioId,NombreMazo,CreadoEn")] Mazo mazo)
        {
            if (id != mazo.MazoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mazo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MazoExists(mazo.MazoId))
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
            ViewBag.Usuarios = new SelectList(_context.Usuarios, "UsuarioId", "NombreUsuario", mazo.UsuarioId);
            return View(mazo);
        }

        // GET: Mazos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mazo = await _context.Mazos
                .Include(m => m.Usuario)
                .FirstOrDefaultAsync(m => m.MazoId == id);
            if (mazo == null)
            {
                return NotFound();
            }

            return View(mazo);
        }

        // POST: Mazos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mazo = await _context.Mazos.FindAsync(id);
            _context.Mazos.Remove(mazo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MazoExists(int id)
        {
            return _context.Mazos.Any(e => e.MazoId == id);
        }
    }
}
