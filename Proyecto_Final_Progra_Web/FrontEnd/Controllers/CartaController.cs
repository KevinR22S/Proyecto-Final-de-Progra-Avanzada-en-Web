using FrontEnd.Helpers.Intefaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult Index()
        {
            return View(CartaHelper.GetCartas());
        }

        // GET: CartaController/Details/5
        public ActionResult Details(int id)
        {
            CartaViewModel carta= CartaHelper.GetCarta(id);
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
        public ActionResult Create(CartaViewModel carta)
        {
            try
            {
                _=CartaHelper.Add(carta);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CartaController/Edit/5
        public ActionResult Edit(int id)
        {
            CartaViewModel carta= CartaHelper.GetCarta(id) ;
            return View(carta);
        }

        // POST: CartaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CartaViewModel carta)
        {
            try
            {
                _ = CartaHelper.Update(carta);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CartaController/Delete/5
        public ActionResult Delete(int id)
        {
            CartaViewModel carta = CartaHelper.GetCarta(id);
            return View(carta);
        }

        // POST: CartaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CartaViewModel carta)
        {
            try
            {
                _ = CartaHelper.Remove(carta.CartaId);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
