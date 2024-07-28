using FrontEnd.Helpers.Intefaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class MazoController : Controller
    {
        IMazoHelper _mazoHelper;

        public MazoController(IMazoHelper mazoHelper)
        {
            _mazoHelper = mazoHelper;
        }
        // GET: MazoController
        public ActionResult Index()
        {
            return View(_mazoHelper.GetMazos());
        }

        // GET: MazoController/Details/5
        public ActionResult Details(int id)
        {
            MazoViewModel mazo = _mazoHelper.GetMazos(id);
            return View(mazo);
        }

        // GET: MazoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MazoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MazoViewModel mazo)
        {
            try
            {
                _ = _mazoHelper.Add(mazo);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MazoController/Edit/5
        public ActionResult Edit(int id)
        {
            MazoViewModel mazo = _mazoHelper.GetMazos(id);
            return View(mazo);
        }

        // POST: MazoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MazoViewModel mazo)
        {
            try
            {
                _= _mazoHelper.Update(mazo);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MazoController/Delete/5
        public ActionResult Delete(int id)
        {
            MazoViewModel mazo = _mazoHelper.GetMazos(id);
            return View(mazo);
        }

        // POST: MazoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(MazoViewModel mazo)
        {
            try
            {
                _ = _mazoHelper.Remove(mazo.MazoId);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
