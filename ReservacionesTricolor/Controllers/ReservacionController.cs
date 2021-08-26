using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ReservacionesTricolor;
using ReservacionesTricolor.Security;

namespace ReservacionesTricolor.Controllers
{
    [CustomAuthenticationFilter]
    public class ReservacionController : Controller
    {
        private ReservacionesTricolorEntities db = new ReservacionesTricolorEntities();

        // GET: Reservacion
        public enum Roles { Administrador = 2, Usuario = 3 }
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Usuario)]
        public ActionResult Index()
        {
            var reservacion = db.Reservacion.Include(r => r.Habitacion).Include(r => r.Usuario);
            return View(reservacion.ToList());
        }

        // GET: Reservacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = db.Reservacion.Find(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }
            return View(reservacion);
        }

        // GET: Reservacion/Create
        public ActionResult Create()
        {
            ViewBag.IdHabitacion = new SelectList(db.Habitacion, "IdHabitacion", "NombreHabitacion");
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Nombre");
            return View();
        }

        // POST: Reservacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdReservacion,CantidadPersonas,CheckIn,CheckOut,IdHabitacion,IdUsuario")] Reservacion reservacion)
        {
            if (ModelState.IsValid)
            {
                db.Reservacion.Add(reservacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdHabitacion = new SelectList(db.Habitacion, "IdHabitacion", "NombreHabitacion", reservacion.IdHabitacion);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Nombre", reservacion.IdUsuario);
            return View(reservacion);
        }

        // GET: Reservacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = db.Reservacion.Find(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdHabitacion = new SelectList(db.Habitacion, "IdHabitacion", "NombreHabitacion", reservacion.IdHabitacion);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Nombre", reservacion.IdUsuario);
            return View(reservacion);
        }

        // POST: Reservacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdReservacion,CantidadPersonas,CheckIn,CheckOut,IdHabitacion,IdUsuario")] Reservacion reservacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdHabitacion = new SelectList(db.Habitacion, "IdHabitacion", "NombreHabitacion", reservacion.IdHabitacion);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Nombre", reservacion.IdUsuario);
            return View(reservacion);
        }

        // GET: Reservacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = db.Reservacion.Find(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }
            return View(reservacion);
        }

        // POST: Reservacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservacion reservacion = db.Reservacion.Find(id);
            db.Reservacion.Remove(reservacion);
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


        [HttpPost]
        public JsonResult CompletarReserva([System.Web.Http.FromBody] Reservacion reservacion)
        {
            Usuario oUsuario = Session["User"] as Usuario;
            reservacion.IdUsuario = oUsuario.IdUsuario;

            if (ModelState.IsValid)
            {
                db.Reservacion.Add(reservacion);
                db.SaveChanges();

                var habitacion = db.Habitacion.Where(x => x.IdHabitacion == reservacion.IdHabitacion).FirstOrDefault();
                reservacion.Habitacion = habitacion;
                var diferenciaDias = reservacion.CheckOut - reservacion.CheckIn;
                var subtotal = reservacion.Habitacion.Precio * diferenciaDias.Days;
                var IVA = subtotal * (decimal)0.13;
                var total = IVA + subtotal;

                Factura oFacturas = new Factura
                {
                    SubTotal = subtotal,
                    IVA = IVA,
                    Total = total,
                    IdReservacion = reservacion.IdReservacion,
                };

                db.Factura.Add(oFacturas);
                db.SaveChanges();

                oFacturas.Reservacion = reservacion;
                return Json(new
                {
                    facturaId = oFacturas.IdFactura,
                    subtotal = oFacturas.SubTotal,
                    iva = oFacturas.IVA,
                    total = oFacturas.Total,
                    nombreHabitacion = oFacturas.Reservacion.Habitacion.NombreHabitacion
                });
            }          
            return null;
        }
    }
}
