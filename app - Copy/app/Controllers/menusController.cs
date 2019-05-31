using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using app.Models;

namespace app.Controllers
{
    public class menusController : Controller
    {
        private cruceroEntities db = new cruceroEntities();

        // GET: menus
        public ActionResult Index(int? restaurante)
        {
            var menus = db.menus.Where(m => m.restaurante == restaurante);
            if(menus == null)
            {
                ViewBag.restaurante = menus.ToList()[0].restaurante1;
            }
            else
            {
                ViewBag.restaurante = db.restaurantes.Find(restaurante);
            }
            
            return View(menus.ToList());
        }

        public ActionResult List()
        {
            var menus = db.menus.Include(m => m.jornada1).Include(m => m.restaurante1);
            return View(menus.ToList());
        }

        // GET: menus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            menu menu = db.menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: menus/Create
        public ActionResult Create(int? restaurante)
        {            
            ViewBag.jornada = new SelectList(db.jornadas, "id", "nombre");
            ViewBag.restaurante = db.restaurantes.Find(restaurante);
            return View();
        }

        // POST: menus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,restaurante,jornada,fecha,tipo")] menu menu)
        {
            if (ModelState.IsValid)
            {
                db.menus.Add(menu);
                db.SaveChanges();
                return RedirectToAction("Index",new { restaurante = menu.restaurante });
            }

            ViewBag.jornada = new SelectList(db.jornadas, "id", "nombre", menu.jornada);
            ViewBag.restaurante = db.restaurantes.Find(menu.restaurante);
            return View(menu);
        }

        // GET: menus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            menu menu = db.menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            ViewBag.jornada = new SelectList(db.jornadas, "id", "nombre", menu.jornada);            
            ViewBag.restaurante = db.restaurantes.Find(menu.restaurante);
            return View(menu);
        }

        // POST: menus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,restaurante,jornada,fecha,tipo")] menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { restaurante = menu.restaurante });
            }
            ViewBag.jornada = new SelectList(db.jornadas, "id", "nombre", menu.jornada);
            ViewBag.restaurante = new SelectList(db.restaurantes, "id", "nombre", menu.restaurante);
            ViewBag.restaurante = db.restaurantes.Find(menu.restaurante);
            return View(menu);
        }

        // GET: menus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            menu menu = db.menus.Find(id);
            ViewBag.restaurante = db.restaurantes.Find(menu.restaurante);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            menu menu = db.menus.Find(id);
            db.menus.Remove(menu);
            db.SaveChanges();                        
            return RedirectToAction("Index", new { restaurante = menu.restaurante });
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
