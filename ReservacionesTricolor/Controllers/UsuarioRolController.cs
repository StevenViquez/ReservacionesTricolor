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
    public class UsuarioRolController : Controller
    {
        private ReservacionesTricolorEntities db = new ReservacionesTricolorEntities();

        // GET: UsuarioRol
        public enum Roles { Administrador = 2 }
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Index()
        {
            var usuarioRol = db.UsuarioRol.Include(u => u.Rol).Include(u => u.Usuario);
            return View(usuarioRol.ToList());
        }

        // GET: UsuarioRol/Details/5
        public ActionResult _Info(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioRol usuarioRol = db.UsuarioRol.Find(id);
            if (usuarioRol == null)
            {
                return HttpNotFound();
            }
            return PartialView(usuarioRol);
        }

        // GET: UsuarioRol/Create
        public ActionResult Create()
        {
            ViewBag.IdRol = new SelectList(db.Rol, "IdRol", "NombreRol");
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Nombre");
            return View();
        }

        // POST: UsuarioRol/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdRol,IdUsuario")] UsuarioRol usuarioRol)
        {
            if (ModelState.IsValid)
            {
                db.UsuarioRol.Add(usuarioRol);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdRol = new SelectList(db.Rol, "IdRol", "NombreRol", usuarioRol.IdRol);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Nombre", usuarioRol.IdUsuario);
            return View(usuarioRol);
        }

        // GET: UsuarioRol/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioRol usuarioRol = db.UsuarioRol.Find(id);
            if (usuarioRol == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdRol = new SelectList(db.Rol, "IdRol", "NombreRol", usuarioRol.IdRol);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Nombre", usuarioRol.IdUsuario);
            return View(usuarioRol);
        }

        // POST: UsuarioRol/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdRol,IdUsuario")] UsuarioRol usuarioRol)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuarioRol).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdRol = new SelectList(db.Rol, "IdRol", "NombreRol", usuarioRol.IdRol);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Nombre", usuarioRol.IdUsuario);
            return View(usuarioRol);
        }

        // GET: UsuarioRol/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioRol usuarioRol = db.UsuarioRol.Find(id);
            if (usuarioRol == null)
            {
                return HttpNotFound();
            }
            return View(usuarioRol);
        }

        // POST: UsuarioRol/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            UsuarioRol usuarioRol = db.UsuarioRol.Find(id);
            db.UsuarioRol.Remove(usuarioRol);
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
