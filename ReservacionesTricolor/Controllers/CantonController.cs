using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReservacionesTricolor;

namespace ReservacionesTricolor.Controllers
{
    public class CantonController : Controller
    {
        private ReservacionesTricolorEntities db = new ReservacionesTricolorEntities();

        // GET: Canton
        public ActionResult Index()
        {
            var canton = db.Canton.Include(c => c.Pais).Include(c => c.Provincia);
            return View(canton.ToList());
        }

        // GET: Canton/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Canton canton = db.Canton.Find(id);
            if (canton == null)
            {
                return HttpNotFound();
            }
            return View(canton);
        }

        // GET: Canton/Create
        public ActionResult Create()
        {
            ViewBag.IdPais = new SelectList(db.Pais, "IdPais", "NombrePais");
            ViewBag.IdProvincia = new SelectList(db.Provincia, "IdProvincia", "NombreProvincia");
            return View();
        }

        // POST: Canton/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCanton,NombreCanton,IdPais,IdProvincia")] Canton canton)
        {
            if (ModelState.IsValid)
            {
                db.Canton.Add(canton);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdPais = new SelectList(db.Pais, "IdPais", "NombrePais", canton.IdPais);
            ViewBag.IdProvincia = new SelectList(db.Provincia, "IdProvincia", "NombreProvincia", canton.IdProvincia);
            return View(canton);
        }

        // GET: Canton/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Canton canton = db.Canton.Find(id);
            if (canton == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPais = new SelectList(db.Pais, "IdPais", "NombrePais", canton.IdPais);
            ViewBag.IdProvincia = new SelectList(db.Provincia, "IdProvincia", "NombreProvincia", canton.IdProvincia);
            return View(canton);
        }

        // POST: Canton/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCanton,NombreCanton,IdPais,IdProvincia")] Canton canton)
        {
            if (ModelState.IsValid)
            {
                db.Entry(canton).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdPais = new SelectList(db.Pais, "IdPais", "NombrePais", canton.IdPais);
            ViewBag.IdProvincia = new SelectList(db.Provincia, "IdProvincia", "NombreProvincia", canton.IdProvincia);
            return View(canton);
        }

        // GET: Canton/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Canton canton = db.Canton.Find(id);
            if (canton == null)
            {
                return HttpNotFound();
            }
            return View(canton);
        }

        // POST: Canton/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Canton canton = db.Canton.Find(id);
            db.Canton.Remove(canton);
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
