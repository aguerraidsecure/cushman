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
    public class UsuariosController : Controller
    {
        private CushmanContext db = new CushmanContext();

        // GET: Usuarios
        public ActionResult Index()
        {
            ViewBag.UsuarioActivo = true;   
            var tsg030_usuarios = db.tsg030_usuarios.Include(t => t.tsg031_perfil);
            return View(tsg030_usuarios.ToList());
        }
        public  ActionResult Login()
        {
            ViewBag.UsuarioActivo = false;
            return View();
        }
        [HttpPost]
        public ActionResult Login([Bind(Include = "cd_usuario_,nb_nombre,cd_jerarquia,st_usuario,nb_usuario,nb_password,cd_perfil,fh_modif")] tsg030_usuarios tsg030_usuarios)
        {

            if (tsg030_usuarios.nb_usuario == "anitcushmanone" && tsg030_usuarios.nb_password == "@dm1n1str@d0r")
            {
               
                return RedirectToAction("Index","Home");
            }
            else
            {
                return View();
            }
        }
        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg030_usuarios tsg030_usuarios = db.tsg030_usuarios.Find(id);
            if (tsg030_usuarios == null)
            {
                return HttpNotFound();
            }
            return View(tsg030_usuarios);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.cd_perfil = new SelectList(db.tsg031_perfil, "cd_perfil", "nb_perfil");
            return View();
        }
        
        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cd_usuario_,nb_nombre,cd_jerarquia,st_usuario,nb_usuario,nb_password,cd_perfil,fh_modif")] tsg030_usuarios tsg030_usuarios)
        {
            if (ModelState.IsValid)
            {
                db.tsg030_usuarios.Add(tsg030_usuarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cd_perfil = new SelectList(db.tsg031_perfil, "cd_perfil", "nb_perfil", tsg030_usuarios.cd_perfil);
            return View(tsg030_usuarios);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg030_usuarios tsg030_usuarios = db.tsg030_usuarios.Find(id);
            if (tsg030_usuarios == null)
            {
                return HttpNotFound();
            }
            ViewBag.cd_perfil = new SelectList(db.tsg031_perfil, "cd_perfil", "nb_perfil", tsg030_usuarios.cd_perfil);
            return View(tsg030_usuarios);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cd_usuario_,nb_nombre,cd_jerarquia,st_usuario,nb_usuario,nb_password,cd_perfil,fh_modif")] tsg030_usuarios tsg030_usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tsg030_usuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cd_perfil = new SelectList(db.tsg031_perfil, "cd_perfil", "nb_perfil", tsg030_usuarios.cd_perfil);
            return View(tsg030_usuarios);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg030_usuarios tsg030_usuarios = db.tsg030_usuarios.Find(id);
            if (tsg030_usuarios == null)
            {
                return HttpNotFound();
            }
            return View(tsg030_usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tsg030_usuarios tsg030_usuarios = db.tsg030_usuarios.Find(id);
            db.tsg030_usuarios.Remove(tsg030_usuarios);
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
