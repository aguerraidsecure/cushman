using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using wr_anit_cushman_one.Models;

namespace wr_anit_cushman_one.Controllers
{
    public class Imagenes_terrenosController : Controller
    {
        private CushmanContext db = new CushmanContext();

        // GET: Imagenes_terrenos
        public ActionResult Index()
        {
            var tsg040_imagenes_terrenos = db.tsg040_imagenes_terrenos;
            return View(tsg040_imagenes_terrenos.ToList());
        }

        // GET: Imagenes_terrenos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg040_imagenes_terrenos tsg040_imagenes_terrenos = db.tsg040_imagenes_terrenos.Find(id);
            if (tsg040_imagenes_terrenos == null)
            {
                return HttpNotFound();
            }
            return View(tsg040_imagenes_terrenos);
        }

        // GET: Imagenes_terrenos/Create
        public ActionResult Create()
        {
            ViewBag.cd_terreno = new SelectList(db.tsg001_terreno, "cd_terreno", "nb_comercial");
            return View();
        }

        // POST: Imagenes_terrenos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cd_id,cd_terreno,nb_archivo,tx_archivo,fh_modif")] tsg040_imagenes_terrenos tsg040_imagenes_terrenos)
        {
            if (ModelState.IsValid)
            {
                db.tsg040_imagenes_terrenos.Add(tsg040_imagenes_terrenos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cd_terreno = new SelectList(db.tsg001_terreno, "cd_terreno", "nb_comercial", tsg040_imagenes_terrenos.cd_terreno);
            return View(tsg040_imagenes_terrenos);
        }

        // GET: Imagenes_terrenos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg040_imagenes_terrenos tsg040_imagenes_terrenos = db.tsg040_imagenes_terrenos.Find(id);
            if (tsg040_imagenes_terrenos == null)
            {
                return HttpNotFound();
            }
            ViewBag.cd_terreno = new SelectList(db.tsg001_terreno, "cd_terreno", "nb_comercial", tsg040_imagenes_terrenos.cd_terreno);
            return View(tsg040_imagenes_terrenos);
        }

        // POST: Imagenes_terrenos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cd_id,cd_terreno,nb_archivo,tx_archivo,fh_modif")] tsg040_imagenes_terrenos tsg040_imagenes_terrenos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tsg040_imagenes_terrenos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cd_terreno = new SelectList(db.tsg001_terreno, "cd_terreno", "nb_comercial", tsg040_imagenes_terrenos.cd_terreno);
            return View(tsg040_imagenes_terrenos);
        }

        // GET: Imagenes_terrenos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg040_imagenes_terrenos tsg040_imagenes_terrenos = db.tsg040_imagenes_terrenos.Find(id);
            if (tsg040_imagenes_terrenos == null)
            {
                return HttpNotFound();
            }
            return View(tsg040_imagenes_terrenos);
        }

        // POST: Imagenes_terrenos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tsg040_imagenes_terrenos tsg040_imagenes_terrenos = db.tsg040_imagenes_terrenos.Find(id);
            db.tsg040_imagenes_terrenos.Remove(tsg040_imagenes_terrenos);
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
