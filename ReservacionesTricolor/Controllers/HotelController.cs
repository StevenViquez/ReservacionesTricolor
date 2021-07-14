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
    public class HotelController : Controller
    {
        private ReservacionesTricolorEntities db = new ReservacionesTricolorEntities();

        // GET: Hotel
        public enum Roles { Administrador = 2 }
        [CustomAuthorize((int)Roles.Administrador)]
        [CustomAuthenticationFilter]
        public ActionResult Index()
        {
            var hotel = db.Hotel.Include(h => h.Canton).Include(h => h.Pais).Include(h => h.Provincia).Include(h => h.Usuario);
            return View(hotel.ToList());
        }

        // GET: Hotel/Details/5
        public ActionResult _Info(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotel.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return PartialView(hotel);
        }

        // GET: Hotel/Create
        public ActionResult Create()
        {
            var pais = db.Pais.ToList();
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "--Seleccione Pais--", Value = "0" });
            foreach (var m in pais)
            {
                li.Add(new SelectListItem { Text = m.NombrePais, Value = m.IdPais.ToString() });
            }


            ViewBag.IdPais = li;
            ViewBag.IdAdministrador = new SelectList(db.Usuario, "IdUsuario", "Nombre");
            return View();
        }

        // POST: Hotel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdHotel,NombreHotel,Descripcion,Estrellas,Telefono,Email,Direccion,IdAdministrador,IdPais,IdProvincia,IdCanton")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                db.Hotel.Add(hotel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCanton = new SelectList(db.Canton, "IdCanton", "NombreCanton", hotel.IdCanton);
            ViewBag.IdPais = new SelectList(db.Pais, "IdPais", "NombrePais", hotel.IdPais);
            ViewBag.IdProvincia = new SelectList(db.Provincia, "IdProvincia", "NombreProvincia", hotel.IdProvincia);
            ViewBag.IdAdministrador = new SelectList(db.Usuario, "IdUsuario", "Nombre", hotel.IdAdministrador);
            return View(hotel);
        }

        // GET: Hotel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotel.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCanton = new SelectList(db.Canton, "IdCanton", "NombreCanton", hotel.IdCanton);
            ViewBag.IdPais = new SelectList(db.Pais, "IdPais", "NombrePais", hotel.IdPais);
            ViewBag.IdProvincia = new SelectList(db.Provincia, "IdProvincia", "NombreProvincia", hotel.IdProvincia);
            ViewBag.IdAdministrador = new SelectList(db.Usuario, "IdUsuario", "Nombre", hotel.IdAdministrador);
            return View(hotel);
        }

        // POST: Hotel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdHotel,NombreHotel,Descripcion,Estrellas,Telefono,Email,Direccion,IdAdministrador,IdPais,IdProvincia,IdCanton")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCanton = new SelectList(db.Canton, "IdCanton", "NombreCanton", hotel.IdCanton);
            ViewBag.IdPais = new SelectList(db.Pais, "IdPais", "NombrePais", hotel.IdPais);
            ViewBag.IdProvincia = new SelectList(db.Provincia, "IdProvincia", "NombreProvincia", hotel.IdProvincia);
            ViewBag.IdAdministrador = new SelectList(db.Usuario, "IdUsuario", "Nombre", hotel.IdAdministrador);
            return View(hotel);
        }

        

        // POST: Hotel/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            Hotel hotel = db.Hotel.Find(id);
            db.Hotel.Remove(hotel);
            db.SaveChanges();
            return Json(new { Message="ok", JsonRequestBehavior.AllowGet});
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
