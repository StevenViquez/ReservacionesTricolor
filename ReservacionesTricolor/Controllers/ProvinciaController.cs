﻿using System;
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
    public class ProvinciaController : Controller
    {
        private ReservacionesTricolorEntities db = new ReservacionesTricolorEntities();

        // GET: Provincia
        public ActionResult Index()
        {
            var provincia = db.Provincia.Include(p => p.Pais);
            return View(provincia.ToList());
        }

        // GET: Provincia/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provincia provincia = db.Provincia.Find(id);
            if (provincia == null)
            {
                return HttpNotFound();
            }
            return View(provincia);
        }

        // GET: Provincia/Create
        public ActionResult Create()
        {
            ViewBag.IdPais = new SelectList(db.Pais, "IdPais", "NombrePais");
            return View();
        }

        // POST: Provincia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProvincia,NombreProvincia,IdPais")] Provincia provincia)
        {
            if (ModelState.IsValid)
            {
                db.Provincia.Add(provincia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdPais = new SelectList(db.Pais, "IdPais", "NombrePais", provincia.IdPais);
            return View(provincia);
        }

        // GET: Provincia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provincia provincia = db.Provincia.Find(id);
            if (provincia == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPais = new SelectList(db.Pais, "IdPais", "NombrePais", provincia.IdPais);
            return View(provincia);
        }

        // POST: Provincia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProvincia,NombreProvincia,IdPais")] Provincia provincia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(provincia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdPais = new SelectList(db.Pais, "IdPais", "NombrePais", provincia.IdPais);
            return View(provincia);
        }

        // GET: Provincia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provincia provincia = db.Provincia.Find(id);
            if (provincia == null)
            {
                return HttpNotFound();
            }
            return View(provincia);
        }

        // POST: Provincia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Provincia provincia = db.Provincia.Find(id);
            db.Provincia.Remove(provincia);
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