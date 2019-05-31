using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using foodcruise.Models;

namespace foodcruise.Controllers
{
    public class platosController : Controller
    {
        private cruceroEntities db = new cruceroEntities();

        // GET: platos
        public ActionResult Index(int? menu)
        {
            var platoes = db.platoes.Where(p => p.menu == menu).Include(p => p.categoria1).Include(p => p.menu1).OrderBy(p => p.categoria);

            if (platoes == null)
            {
                ViewBag.menuPrincipal = platoes.ToList()[0].menu1;
            }
            else
            {
                ViewBag.menuPrincipal = db.menus.Find(menu);
            }
            return View(platoes.ToList());
        }

        public ActionResult List()
        {
            var platoes = db.platoes.Include(p => p.categoria1).Include(p => p.menu1);
            return View(platoes.ToList());
        }

        // GET: platos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plato plato = db.platoes.Find(id);
            if (plato == null)
            {
                return HttpNotFound();
            }
            return View(plato);
        }

        // GET: platos/Create
        public ActionResult Create(int? menu)
        {
            ViewBag.categoria = new SelectList(db.categorias, "id", "nombre");
            ViewBag.menu = db.menus.Find(menu);
            return View();
        }

        // POST: platos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,descripcion,menu,categoria")] plato plato)
        {
            if (ModelState.IsValid)
            {
                db.platoes.Add(plato);
                db.SaveChanges();
                return RedirectToAction("Index", new { menu = plato.menu });
            }

            ViewBag.categoria = new SelectList(db.categorias, "id", "nombre", plato.categoria);
            ViewBag.menu = db.menus.Find(plato.menu);
            return View(plato);
        }

        // GET: platos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plato plato = db.platoes.Find(id);
            if (plato == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoria = new SelectList(db.categorias, "id", "nombre", plato.categoria);
            ViewBag.menu = db.menus.Find(plato.menu);
            return View(plato);
        }

        // POST: platos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,descripcion,menu,categoria")] plato plato)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plato).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { menu = plato.menu });
            }
            ViewBag.categoria = new SelectList(db.categorias, "id", "nombre", plato.categoria);
            ViewBag.menu = new SelectList(db.menus, "id", "nombre", plato.menu);
            return View(plato);
        }

        // GET: platos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plato plato = db.platoes.Find(id);
            if (plato == null)
            {
                return HttpNotFound();
            }
            return View(plato);
        }

        // POST: platos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            plato plato = db.platoes.Find(id);
            db.platoes.Remove(plato);
            db.SaveChanges();
            return RedirectToAction("Index", new { menu = plato.menu });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
