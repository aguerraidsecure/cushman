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
    public class tipos_reportesController : Controller
    {
        private CushmanContext db = new CushmanContext();

        // GET: tipos_reportes
        public ActionResult Index()
        {
            return View(db.tsg046_tipos_reportes.ToList());
        }

        // GET: tipos_reportes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg046_tipos_reportes tsg046_tipos_reportes = db.tsg046_tipos_reportes.Find(id);
            if (tsg046_tipos_reportes == null)
            {
                return HttpNotFound();
            }
            return View(tsg046_tipos_reportes);
        }

        // GET: tipos_reportes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tipos_reportes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cd_reporte,nb_reporte,fh_modif")] tsg046_tipos_reportes tsg046_tipos_reportes)
        {
            if (ModelState.IsValid)
            {
                db.tsg046_tipos_reportes.Add(tsg046_tipos_reportes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tsg046_tipos_reportes);
        }

        // GET: tipos_reportes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg046_tipos_reportes tsg046_tipos_reportes = db.tsg046_tipos_reportes.Find(id);
            if (tsg046_tipos_reportes == null)
            {
                return HttpNotFound();
            }
            return View(tsg046_tipos_reportes);
        }

        // POST: tipos_reportes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cd_reporte,nb_reporte,fh_modif")] tsg046_tipos_reportes tsg046_tipos_reportes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tsg046_tipos_reportes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tsg046_tipos_reportes);
        }

        // GET: tipos_reportes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg046_tipos_reportes tsg046_tipos_reportes = db.tsg046_tipos_reportes.Find(id);
            if (tsg046_tipos_reportes == null)
            {
                return HttpNotFound();
            }
            return View(tsg046_tipos_reportes);
        }

        // POST: tipos_reportes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tsg046_tipos_reportes tsg046_tipos_reportes = db.tsg046_tipos_reportes.Find(id);
            db.tsg046_tipos_reportes.Remove(tsg046_tipos_reportes);
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
