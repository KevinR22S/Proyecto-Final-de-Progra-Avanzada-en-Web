using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final_Progra_Web.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Final_Progra_Web.Controllers
{
    public class CartasController : Controller
    {
        private readonly ProyectoFinalWebContext _context;

        public CartasController(ProyectoFinalWebContext context)
        {
            _context = context;
        }

        // GET: Cartas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cartas.ToListAsync());
        }

        // GET: Cartas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carta = await _context.Cartas
                .FirstOrDefaultAsync(m => m.CartaId == id);
            if (carta == null)
            {
                return NotFound();
            }

            return View(carta);
        }

        // GET: Cartas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cartas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartaId,Nombre,Descripcion,PuntosAtaque,PuntosDefensa,CreadoEn")] Carta carta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carta);
        }

        // GET: Cartas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carta = await _context.Cartas.FindAsync(id);
            if (carta == null)
            {
                return NotFound();
            }
            return View(carta);
        }

        // POST: Cartas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CartaId,Nombre,Descripcion,PuntosAtaque,PuntosDefensa,CreadoEn")] Carta carta)
        {
            if (id != carta.CartaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartaExists(carta.CartaId))
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
            return View(carta);
        }

        // GET: Cartas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carta = await _context.Cartas
                .FirstOrDefaultAsync(m => m.CartaId == id);
            if (carta == null)
            {
                return NotFound();
            }

            return View(carta);
        }

        // POST: Cartas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carta = await _context.Cartas.FindAsync(id);
            _context.Cartas.Remove(carta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartaExists(int id)
        {
            return _context.Cartas.Any(e => e.CartaId == id);
        }
    }
}
