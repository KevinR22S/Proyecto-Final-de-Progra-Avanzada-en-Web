using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FrontEnd.Models;
using System.Linq;
using System.Threading.Tasks;
using FrontEnd.Helpers.Implementations;
using FrontEnd.Helpers.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using System.Security.Claims;

namespace FrontEnd.Controllers
{
    public class MazoController : Controller
    {
        IMazoHelper _MazoHelper;


        private readonly ProyectoFinalWebContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MazoController(IMazoHelper mazoHelper, ProyectoFinalWebContext context , UserManager<ApplicationUser> userManager)
        {
           _MazoHelper = mazoHelper;
            _userManager = userManager;
            _context = context;
        }


        [Authorize]
        // GET: Mazos
        public async Task<IActionResult> Index()


        {
            var isCliente = User.IsInRole("Usuario");

            List<Mazo> mazos;
            if (isCliente)
            {
                // Obtener el ID del cliente actual
                var clienteId = User.FindFirstValue(ClaimTypes.NameIdentifier);


                mazos = await _context.Mazos
                    .Where(c => c.UsuarioId == clienteId)
                    .Include(c => c.NombreMazo)
                    .Include(c => c.CreadoEn)
                    .Include(c => c.UsuarioModificacion)
                    .ToListAsync();
            }
            else
            {

                mazos = await _context.Mazos
                    .Include(c => c.NombreMazo)
                    .Include(c => c.CreadoEn)
                    .Include(c => c.UsuarioModificacion)
                    .ToListAsync();
            }

            return View(mazos);
        }


        [Authorize]
        public async Task<IActionResult> MisMazos()
        {
            // Obtener el ID del usuario actual
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            var mazos = await _context.Mazos
                .Where(c => c.UsuarioId == userId)
                .ToListAsync();

            return View(mazos);
        }

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            var ci = _context.Mazos.FirstOrDefault(c => c.MazoId == id);

            if (ci == null)
            {
                return NotFound();
            }

           var mazos = await _context.Mazos
            .Include(c => c.NombreMazo)
            .Include(c => c.CreadoEn)
            .Include(c => c.UsuarioModificacion)
            .ToListAsync();

            if (mazos == null)
            {
                return NotFound();
            }


            return View(mazos);
        }

        // GET: Citas/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["MazoId"] = new SelectList(_context.Mazos, "MazoId", "Nombre");
            ViewData["UsuarioModificacionId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Nombre");


            //ViewData["Veterinario"] = new SelectList(veterinarios, "Value", "Text");

            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Mazo mazo)
        {
            if (mazo.MazoId == 0)
            {
                // Agregar un error de modelo para mostrar un mensaje al usuario
                ModelState.AddModelError("", "No hay mazos registrados, se debe crear al menos un mazo para crear el mazo");
            }

            if (ModelState.IsValid)
            {
                DateTime fechaCita = mazo.CreadoEn.Date;

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                mazo.UsuarioId = userId;

                var isCliente = User.IsInRole("Cliente");

                // Verificar si la cita es creada por un veterinario
                if (isCliente)
                {
                    // Guardar la cita en la base de datos
                    _context.Add(mazo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    mazo.Estado = "Creado";
                }


            }
            ViewData["MazoId"] = new SelectList(_context.Mazos, "MazoId", "Nombre", mazo.MazoId);
            ViewData["UsuarioId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", mazo.UsuarioId);
            ViewData["UsuarioModificacion"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", mazo.UsuarioModificacion);


            return View(mazo);


        }


        // GET: Citas/Edit/5
        [Authorize]
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
            ViewData["MazoId"] = new SelectList(_context.Mazos, "MazoId", "Nombre", mazo.MazoId);
            ViewData["UsuarioId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", mazo.UsuarioId);
            ViewData["UsuarioModificacionId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", mazo.UsuarioModificacion);



            return View(mazo);
        }


        // POST: Citas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("MazoId,UsuarioId,UsuarioModificacionId,NombreMazo,CreadoEn,Estado")] Mazo mazo)
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
                    if (!mazoExist(mazo.MazoId))
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
            ViewData["MazoId"] = new SelectList(_context.Mazos, "MazoId", "Nombre", mazo.MazoId);
            ViewData["UsuarioId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", mazo.UsuarioId);
            ViewData["UsuarioModificacionId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", mazo.UsuarioModificacion);


            return View(mazo);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mazos = await _context.Mazos
                    .Include(c => c.NombreMazo)
                    .Include(c => c.CreadoEn)
                    .Include(c => c.UsuarioModificacion)
                .FirstOrDefaultAsync(m => m.MazoId == id);
            if (mazos == null)
            {
                return NotFound();
            }

            var citas = _context.Mazos.FirstOrDefault(c => c.MazoId == id);


            return View(mazos);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mazos = await _context.Mazos.FindAsync(id);
            if (mazos != null)
            {
                _context.Mazos.Remove(mazos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool mazoExist(int id)
        {
            return _context.Mazos.Any(e => e.MazoId == id);
        }

    }
}
