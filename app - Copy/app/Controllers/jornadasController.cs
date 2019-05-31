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
    public class jornadasController : Controller
    {
        private cruceroEntities db = new cruceroEntities();

        // GET: jornadas
        public ActionResult Index()
        {
            return View(db.jornadas.ToList());
        }

        // GET: jornadas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jornada jornada = db.jornadas.Find(id);
            if (jornada == null)
            {
                return HttpNotFound();
            }
            return View(jornada);
        }

        // GET: jornadas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: jornadas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre")] jornada jornada)
        {
            if (ModelState.IsValid)
            {
                db.jornadas.Add(jornada);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jornada);
        }

        // GET: jornadas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jornada jornada = db.jornadas.Find(id);
            if (jornada == null)
            {
                return HttpNotFound();
            }
            return View(jornada);
        }

        // POST: jornadas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre")] jornada jornada)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jornada).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jornada);
        }

        // GET: jornadas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jornada jornada = db.jornadas.Find(id);
            if (jornada == null)
            {
                return HttpNotFound();
            }
            return View(jornada);
        }

        // POST: jornadas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            jornada jornada = db.jornadas.Find(id);
            db.jornadas.Remove(jornada);
            db.SaveChanges();
            return RedirectToAction("Index");
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
