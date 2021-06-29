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
    public class HabitacionController : Controller
    {
        private ReservacionesTricolorEntities db = new ReservacionesTricolorEntities();

        // GET: Habitacion
        public ActionResult Index()
        {
            var habitacion = db.Habitacion.Include(h => h.Hotel);
            return View(habitacion.ToList());
        }

        // GET: Habitacion/Details/5
        public ActionResult _Info(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Habitacion habitacion = db.Habitacion.Find(id);
            if (habitacion == null)
            {
                return HttpNotFound();
            }
            return PartialView(habitacion);
        }

        // GET: Habitacion/Create
        public ActionResult Create()
        {
            ViewBag.IdHotel = new SelectList(db.Hotel, "IdHotel", "NombreHotel");
            return View();
        }

        // POST: Habitacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdHabitacion,NombreHabitacion,Descripcion,Precio,Capacidad,IdHotel")] Habitacion habitacion)
        {
            if (ModelState.IsValid)
            {
                db.Habitacion.Add(habitacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdHotel = new SelectList(db.Hotel, "IdHotel", "NombreHotel", habitacion.IdHotel);
            return View(habitacion);
        }

        // GET: Habitacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Habitacion habitacion = db.Habitacion.Find(id);
            if (habitacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdHotel = new SelectList(db.Hotel, "IdHotel", "NombreHotel", habitacion.IdHotel);
            return View(habitacion);
        }

        // POST: Habitacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdHabitacion,NombreHabitacion,Descripcion,Precio,Capacidad,IdHotel")] Habitacion habitacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(habitacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdHotel = new SelectList(db.Hotel, "IdHotel", "NombreHotel", habitacion.IdHotel);
            return View(habitacion);
        }

        // GET: Habitacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Habitacion habitacion = db.Habitacion.Find(id);
            if (habitacion == null)
            {
                return HttpNotFound();
            }
            return View(habitacion);
        }

        // POST: Habitacion/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Habitacion habitacion = db.Habitacion.Find(id);
            db.Habitacion.Remove(habitacion);
            db.SaveChanges();
            return Json(new { Message = "ok", JsonRequestBehavior.AllowGet });
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
