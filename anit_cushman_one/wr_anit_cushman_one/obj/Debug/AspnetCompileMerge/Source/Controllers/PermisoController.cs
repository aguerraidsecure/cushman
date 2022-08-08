using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model;
using Model.Commons;

namespace wr_anit_cushman_one.Controllers
{
    public class PermisoController : Controller
    {
        private CushmanContext db = new CushmanContext();

        // GET: Permiso
        public ActionResult Index()
        {
            ViewBag.UsuarioActivo = true;
            ViewBag.disabled = true;
            return View(db.Permiso.ToList());
        }

        // GET: Permiso/Details/5
        public ActionResult Details(RolesPermisos id)
        {
            ViewBag.UsuarioActivo = true;
            ViewBag.disabled = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permiso permiso = db.Permiso.Find(id);
            if (permiso == null)
            {
                return HttpNotFound();
            }
            return View(permiso);
        }

        // GET: Permiso/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioActivo = true;
            ViewBag.disabled = true;
            return View();
        }

        // POST: Permiso/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PermisoID,Modulo,Descripcion")] Permiso permiso)
        {
            ViewBag.UsuarioActivo = true;
            ViewBag.disabled = true;
            if (ModelState.IsValid)
            {
                db.Permiso.Add(permiso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(permiso);
        }

        // GET: Permiso/Edit/5
        public ActionResult Edit(RolesPermisos id)
        {
            ViewBag.UsuarioActivo = true;
            ViewBag.disabled = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permiso permiso = db.Permiso.Find(id);
            if (permiso == null)
            {
                return HttpNotFound();
            }
            return View(permiso);
        }

        // POST: Permiso/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PermisoID,Modulo,Descripcion")] Permiso permiso)
        {
            ViewBag.UsuarioActivo = true;
            ViewBag.disabled = true;
            if (ModelState.IsValid)
            {
                db.Entry(permiso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(permiso);
        }

        // GET: Permiso/Delete/5
        public ActionResult Delete(RolesPermisos id)
        {
            ViewBag.UsuarioActivo = true;
            ViewBag.disabled = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permiso permiso = db.Permiso.Find(id);
            if (permiso == null)
            {
                return HttpNotFound();
            }
            return View(permiso);
        }

        // POST: Permiso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(RolesPermisos id)
        {
            Permiso permiso = db.Permiso.Find(id);
            db.Permiso.Remove(permiso);
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
