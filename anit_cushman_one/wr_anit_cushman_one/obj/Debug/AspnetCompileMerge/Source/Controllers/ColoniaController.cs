using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using wr_anit_cushman_one.Models;
using wr_anit_cushman_one.Tags;

namespace wr_anit_cushman_one.Controllers
{
    [Autenticado]
    public class ColoniaController : Controller
    {
        private CushmanContext db = new CushmanContext();

        // GET: Colonia
        public ActionResult Index()
        {
            ViewBag.UsuarioActivo = true;
            return View(db.tsg036_colonias.ToList());
        }

        // GET: Colonia/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg036_colonias tsg036_colonias = db.tsg036_colonias.Find(id);
            if (tsg036_colonias == null)
            {
                return HttpNotFound();
            }
            return View(tsg036_colonias);
        }

        // GET: Colonia/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioActivo = true;
            return View();
        }

        // POST: Colonia/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cd_colonias,cd_municipio,cd_estados,nb_colonia,nu_cp,fh_modif")] tsg036_colonias tsg036_colonias)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                db.tsg036_colonias.Add(tsg036_colonias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tsg036_colonias);
        }

        // GET: Colonia/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg036_colonias tsg036_colonias = db.tsg036_colonias.Find(id);
            if (tsg036_colonias == null)
            {
                return HttpNotFound();
            }
            return View(tsg036_colonias);
        }

        // POST: Colonia/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cd_colonias,cd_municipio,cd_estados,nb_colonia,nu_cp,fh_modif")] tsg036_colonias tsg036_colonias)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                db.Entry(tsg036_colonias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tsg036_colonias);
        }

        // GET: Colonia/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg036_colonias tsg036_colonias = db.tsg036_colonias.Find(id);
            if (tsg036_colonias == null)
            {
                return HttpNotFound();
            }
            return View(tsg036_colonias);
        }

        // POST: Colonia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.UsuarioActivo = true;
            tsg036_colonias tsg036_colonias = db.tsg036_colonias.Find(id);
            db.tsg036_colonias.Remove(tsg036_colonias);
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
