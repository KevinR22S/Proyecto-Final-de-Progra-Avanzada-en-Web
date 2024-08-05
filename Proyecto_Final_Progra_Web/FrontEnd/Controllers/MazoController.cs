using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FrontEnd.Models;
using System.Linq;
using System.Threading.Tasks;
using FrontEnd.Helpers.Implementations;
using FrontEnd.Helpers.Intefaces;

namespace FrontEnd.Controllers
{
    public class MazoController : Controller
    {
        IMazoHelper _MazoHelper;

        public MazoController(IMazoHelper mazoHelper)
        {
           _MazoHelper = mazoHelper;
        }

        // GET: Mazos
        public ActionResult Index()
        {
            return View(_MazoHelper.GetMazos());
        }

        // GET: Mazos/Details/5
        public ActionResult Details(int id)
        {
            MazoViewModel mazo = _MazoHelper.GetMazo(id);
            return View(mazo);
        }

        // GET: Mazos/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Mazos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("MazoId,UsuarioId,NombreMazo,CreadoEn")] MazoViewModel mazo)
        {
            try
            {
                _ = _MazoHelper.Add(mazo);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Mazos/Edit/5
        public ActionResult Edit(int id)
        {
            MazoViewModel mazo= _MazoHelper.GetMazo(id);
            return View(mazo);
        }

        // POST: Mazos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MazoViewModel mazo)
        {
            try
            {
                _ = _MazoHelper.Update(mazo);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Mazos/Delete/5
        public ActionResult Delete(int id)
        {
            MazoViewModel mazo = _MazoHelper.GetMazo(id);
            return View(mazo);
        }

        // POST: Mazos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(MazoViewModel mazo)
        {
            try
            {
                _ = _MazoHelper.Remove(mazo.MazoId);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
