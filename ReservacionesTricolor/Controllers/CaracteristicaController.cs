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
    public class CaracteristicaController : Controller
    {
        private ReservacionesTricolorEntities db = new ReservacionesTricolorEntities();

        // GET: Caracteristica
        public enum Roles { Administrador = 2 }
        [CustomAuthorize((int)Roles.Administrador)]
        [CustomAuthenticationFilter]
        public ActionResult Index()
        {
            return View(db.Caracteristica.ToList());
        }

        // GET: Caracteristica/Details/5
        public ActionResult _Info(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caracteristica caracteristica = db.Caracteristica.Find(id);
            if (caracteristica == null)
            {
                return HttpNotFound();
            }
            return PartialView(caracteristica);
        }

        // GET: Caracteristica/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Caracteristica/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCaracteristica,NombreCaracteristica")] Caracteristica caracteristica)
        {
            if (ModelState.IsValid)
            {
                db.Caracteristica.Add(caracteristica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(caracteristica);
        }

        // GET: Caracteristica/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caracteristica caracteristica = db.Caracteristica.Find(id);
            if (caracteristica == null)
            {
                return HttpNotFound();
            }
            return View(caracteristica);
        }

        // POST: Caracteristica/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCaracteristica,NombreCaracteristica")] Caracteristica caracteristica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(caracteristica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(caracteristica);
        }

        // GET: Caracteristica/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caracteristica caracteristica = db.Caracteristica.Find(id);
            if (caracteristica == null)
            {
                return HttpNotFound();
            }
            return View(caracteristica);
        }

        // POST: Caracteristica/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Caracteristica caracteristica = db.Caracteristica.Find(id);
            db.Caracteristica.Remove(caracteristica);
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
