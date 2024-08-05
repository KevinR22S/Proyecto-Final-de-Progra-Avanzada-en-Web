using FrontEnd.Helpers.Intefaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FrontEnd.Controllers
{
    public class CartaController : Controller
    {
        ICartaHelper CartaHelper;

        public CartaController(ICartaHelper cartaHelper)
        {
            CartaHelper = cartaHelper;
        }
        // GET: CartaController
        public async Task<ActionResult> Index()
        {
            return View(await CartaHelper.GetCartas());
        }

        // GET: CartaController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            CartaViewModel carta = await CartaHelper.GetCarta(id);
            return View(carta);
        }

        // GET: CartaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CartaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CartaViewModel carta)
        {
            try
            {
                await CartaHelper.Add(carta);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CartaController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            CartaViewModel carta = await CartaHelper.GetCarta(id);
            return View(carta);
        }

        // POST: CartaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CartaViewModel carta)
        {
            try
            {
                await CartaHelper.Update(carta);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CartaController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            CartaViewModel carta = await CartaHelper.GetCarta(id);
            return View(carta);
        }

        // POST: CartaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(CartaViewModel carta)
        {
            try
            {
                await CartaHelper.Remove(carta.CartaId);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
