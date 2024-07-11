using Microsoft.AspNetCore.Mvc;
using Proyecto_Final_Progra_Web.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Final_Progra_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProyectoFinalWebContext _context;

        public HomeController(ProyectoFinalWebContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var cartas = await _context.Cartas.ToListAsync();
            var mazos = await _context.Mazos.ToListAsync();
            var usuarios = await _context.Usuarios.ToListAsync();

            var model = new HomeViewModel
            {
                Cartas = cartas,
                Mazos = mazos,
                Usuarios = usuarios
            };

            // Pasar el modelo a la página de Razor
            return RedirectToPage("/Index", model);
        }
    }

    public class HomeViewModel
    {
        public List<Carta> Cartas { get; set; }
        public List<Mazo> Mazos { get; set; }
        public List<Usuario> Usuarios { get; set; }
    }
}
