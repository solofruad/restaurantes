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
    public class restaurantesController : Controller
    {
        private cruceroEntities db = new cruceroEntities();

        // GET: restaurantes
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var restaurante = db.restaurantes.Find(id);
            return View(restaurante);
        }

        public ActionResult List()
        {
            return View(db.restaurantes.ToList());
        }

        // GET: restaurantes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            restaurante restaurante = db.restaurantes.Find(id);
            if (restaurante == null)
            {
                return HttpNotFound();
            }
            return View(restaurante);
        }

        // GET: restaurantes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: restaurantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,horario,principal,capacidad")] restaurante restaurante)
        {
            if (ModelState.IsValid)
            {
                db.restaurantes.Add(restaurante);
                db.SaveChanges();
                return RedirectToAction("List");
            }

            return View(restaurante);
        }

        // GET: restaurantes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            restaurante restaurante = db.restaurantes.Find(id);
            if (restaurante == null)
            {
                return HttpNotFound();
            }
            return View(restaurante);
        }

        // POST: restaurantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,horario,principal,capacidad")] restaurante restaurante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurante).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index/" + restaurante.id);
            }
            return View(restaurante);
        }

        // GET: restaurantes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            restaurante restaurante = db.restaurantes.Find(id);
            if (restaurante == null)
            {
                return HttpNotFound();
            }
            return View(restaurante);
        }

        // POST: restaurantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            restaurante restaurante = db.restaurantes.Find(id);
            db.restaurantes.Remove(restaurante);
            db.SaveChanges();
            return RedirectToAction("List");
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
