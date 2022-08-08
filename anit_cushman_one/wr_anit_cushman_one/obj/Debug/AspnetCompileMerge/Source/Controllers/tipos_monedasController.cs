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
    public class tipos_monedasController : Controller
    {
        private CushmanContext db = new CushmanContext();

        // GET: tipos_monedas
        public ActionResult Index()
        {
            return View(db.tsg044_tipos_monedas.ToList());
        }

        // GET: tipos_monedas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg044_tipos_monedas tsg044_tipos_monedas = db.tsg044_tipos_monedas.Find(id);
            if (tsg044_tipos_monedas == null)
            {
                return HttpNotFound();
            }
            return View(tsg044_tipos_monedas);
        }

        // GET: tipos_monedas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tipos_monedas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cd_moneda,nb_moneda,fh_modif")] tsg044_tipos_monedas tsg044_tipos_monedas)
        {
            if (ModelState.IsValid)
            {
                db.tsg044_tipos_monedas.Add(tsg044_tipos_monedas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tsg044_tipos_monedas);
        }

        // GET: tipos_monedas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg044_tipos_monedas tsg044_tipos_monedas = db.tsg044_tipos_monedas.Find(id);
            if (tsg044_tipos_monedas == null)
            {
                return HttpNotFound();
            }
            return View(tsg044_tipos_monedas);
        }

        // POST: tipos_monedas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cd_moneda,nb_moneda,fh_modif")] tsg044_tipos_monedas tsg044_tipos_monedas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tsg044_tipos_monedas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tsg044_tipos_monedas);
        }

        // GET: tipos_monedas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg044_tipos_monedas tsg044_tipos_monedas = db.tsg044_tipos_monedas.Find(id);
            if (tsg044_tipos_monedas == null)
            {
                return HttpNotFound();
            }
            return View(tsg044_tipos_monedas);
        }

        // POST: tipos_monedas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tsg044_tipos_monedas tsg044_tipos_monedas = db.tsg044_tipos_monedas.Find(id);
            db.tsg044_tipos_monedas.Remove(tsg044_tipos_monedas);
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
