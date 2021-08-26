using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReservacionesTricolor;
using ReservacionesTricolor.Security;

namespace ReservacionesTricolor.Controllers
{
    [CustomAuthenticationFilter]
    public class FacturaController : Controller
    {
        private ReservacionesTricolorEntities db = new ReservacionesTricolorEntities();

        // GET: Factura
        public enum Roles { Administrador = 2 , Usuario = 3}
        [CustomAuthorize((int)Roles.Administrador,(int)Roles.Usuario)]
        public ActionResult Index()
        {
            Usuario oUsuario = Session["User"] as Usuario;
            var factura = db.Factura.Include(f => f.Reservacion.Habitacion.Hotel).Where(x => x.Reservacion.IdUsuario == oUsuario.IdUsuario);
            return View(factura.ToList());
        }

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult MostrarReservaciones()
        {
            var factura = db.Factura.Include(f => f.Reservacion.Habitacion.Hotel);
            return View(factura.ToList());
        }

        // GET: Factura/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // GET: Factura/Create
        public ActionResult Create()
        {
            ViewBag.IdReservacion = new SelectList(db.Reservacion, "IdReservacion", "IdReservacion");
            return View();
        }

        // POST: Factura/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdFactura,SubTotal,IVA,Total,IdReservacion")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                db.Factura.Add(factura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdReservacion = new SelectList(db.Reservacion, "IdReservacion", "IdReservacion", factura.IdReservacion);
            return View(factura);
        }

        // GET: Factura/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdReservacion = new SelectList(db.Reservacion, "IdReservacion", "IdReservacion", factura.IdReservacion);
            return View(factura);
        }

        // POST: Factura/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdFactura,SubTotal,IVA,Total,IdReservacion")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(factura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdReservacion = new SelectList(db.Reservacion, "IdReservacion", "IdReservacion", factura.IdReservacion);
            return View(factura);
        }

        // GET: Factura/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // POST: Factura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Factura factura = db.Factura.Find(id);
            db.Factura.Remove(factura);
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
