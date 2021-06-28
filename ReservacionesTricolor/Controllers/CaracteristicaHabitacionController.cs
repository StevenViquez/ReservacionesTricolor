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
    public class CaracteristicaHabitacionController : Controller
    {
        private ReservacionesTricolorEntities db = new ReservacionesTricolorEntities();

        // GET: CaracteristicaHabitacion
        public ActionResult Index()
        {
            var caracteristicaHabitacion = db.CaracteristicaHabitacion.Include(c => c.Caracteristica).Include(c => c.Habitacion);
            return View(caracteristicaHabitacion.ToList());
        }

        // GET: CaracteristicaHabitacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaracteristicaHabitacion caracteristicaHabitacion = db.CaracteristicaHabitacion.Find(id);
            if (caracteristicaHabitacion == null)
            {
                return HttpNotFound();
            }
            return View(caracteristicaHabitacion);
        }

        // GET: CaracteristicaHabitacion/Create
        public ActionResult Create()
        {
            ViewBag.IdCaracteristica = new SelectList(db.Caracteristica, "IdCaracteristica", "NombreCaracteristica");
            ViewBag.IdHabitacion = new SelectList(db.Habitacion, "IdHabitacion", "NombreHabitacion");
            return View();
        }

        // POST: CaracteristicaHabitacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdHabitacion,IdCaracteristica")] CaracteristicaHabitacion caracteristicaHabitacion)
        {
            if (ModelState.IsValid)
            {
                db.CaracteristicaHabitacion.Add(caracteristicaHabitacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCaracteristica = new SelectList(db.Caracteristica, "IdCaracteristica", "NombreCaracteristica", caracteristicaHabitacion.IdCaracteristica);
            ViewBag.IdHabitacion = new SelectList(db.Habitacion, "IdHabitacion", "NombreHabitacion", caracteristicaHabitacion.IdHabitacion);
            return View(caracteristicaHabitacion);
        }

        // GET: CaracteristicaHabitacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaracteristicaHabitacion caracteristicaHabitacion = db.CaracteristicaHabitacion.Find(id);
            if (caracteristicaHabitacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCaracteristica = new SelectList(db.Caracteristica, "IdCaracteristica", "NombreCaracteristica", caracteristicaHabitacion.IdCaracteristica);
            ViewBag.IdHabitacion = new SelectList(db.Habitacion, "IdHabitacion", "NombreHabitacion", caracteristicaHabitacion.IdHabitacion);
            return View(caracteristicaHabitacion);
        }

        // POST: CaracteristicaHabitacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdHabitacion,IdCaracteristica")] CaracteristicaHabitacion caracteristicaHabitacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(caracteristicaHabitacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCaracteristica = new SelectList(db.Caracteristica, "IdCaracteristica", "NombreCaracteristica", caracteristicaHabitacion.IdCaracteristica);
            ViewBag.IdHabitacion = new SelectList(db.Habitacion, "IdHabitacion", "NombreHabitacion", caracteristicaHabitacion.IdHabitacion);
            return View(caracteristicaHabitacion);
        }

        // GET: CaracteristicaHabitacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaracteristicaHabitacion caracteristicaHabitacion = db.CaracteristicaHabitacion.Find(id);
            if (caracteristicaHabitacion == null)
            {
                return HttpNotFound();
            }
            return View(caracteristicaHabitacion);
        }

        // POST: CaracteristicaHabitacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CaracteristicaHabitacion caracteristicaHabitacion = db.CaracteristicaHabitacion.Find(id);
            db.CaracteristicaHabitacion.Remove(caracteristicaHabitacion);
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
