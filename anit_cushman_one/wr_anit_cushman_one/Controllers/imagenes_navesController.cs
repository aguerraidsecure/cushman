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
    public class imagenes_navesController : Controller
    {
        private CushmanContext db = new CushmanContext();

        // GET: imagenes_naves
        public ActionResult Index()
        {
            return View(db.tsg045_imagenes_naves.ToList());
        }

        // GET: imagenes_naves/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg045_imagenes_naves tsg045_imagenes_naves = db.tsg045_imagenes_naves.Find(id);
            if (tsg045_imagenes_naves == null)
            {
                return HttpNotFound();
            }
            return View(tsg045_imagenes_naves);
        }

        // GET: imagenes_naves/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: imagenes_naves/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cd_id,cd_nave,nb_archivo,tx_archivo,fh_modif")] tsg045_imagenes_naves tsg045_imagenes_naves)
        {
            if (ModelState.IsValid)
            {
                db.tsg045_imagenes_naves.Add(tsg045_imagenes_naves);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tsg045_imagenes_naves);
        }

        // GET: imagenes_naves/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg045_imagenes_naves tsg045_imagenes_naves = db.tsg045_imagenes_naves.Find(id);
            if (tsg045_imagenes_naves == null)
            {
                return HttpNotFound();
            }
            return View(tsg045_imagenes_naves);
        }

        // POST: imagenes_naves/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cd_id,cd_nave,nb_archivo,tx_archivo,fh_modif")] tsg045_imagenes_naves tsg045_imagenes_naves)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tsg045_imagenes_naves).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tsg045_imagenes_naves);
        }

        // GET: imagenes_naves/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg045_imagenes_naves tsg045_imagenes_naves = db.tsg045_imagenes_naves.Find(id);
            if (tsg045_imagenes_naves == null)
            {
                return HttpNotFound();
            }
            return View(tsg045_imagenes_naves);
        }

        // POST: imagenes_naves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tsg045_imagenes_naves tsg045_imagenes_naves = db.tsg045_imagenes_naves.Find(id);
            db.tsg045_imagenes_naves.Remove(tsg045_imagenes_naves);
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
