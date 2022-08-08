using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model;
using PagedList;
using wr_anit_cushman_one.Tags;

namespace wr_anit_cushman_one.Controllers
{
    public class UsuariosCController : Controller
    {
        private CushmanContext db = new CushmanContext();

        // GET: UsuariosC
        //public ActionResult Index()
        //{
        //    ViewBag.UsuarioActivo = true;
        //    ViewBag.disabled = true;
        //    var usuario = db.Usuario.Include(u => u.Rol);
        //    return View(usuario.ToList());
        //}
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.UsuarioActivo = true;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.EstadoSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Rol_id" ? "Rol_id_desc" : "Rol";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var Usuario = from e in db.Usuario
                                 select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                Usuario = Usuario.Where(e => e.Nombre.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    Usuario = Usuario.OrderByDescending(e => e.Nombre);
                    break;
                case "Rol_id":
                    Usuario = Usuario.OrderBy(e => e.Rol);
                    break;
                case "Rol_id_desc":
                    Usuario = Usuario.OrderByDescending(e => e.Rol);
                    break;
                default:
                    Usuario = Usuario.OrderBy(e => e.Nombre);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(Usuario.ToPagedList(pageNumber, pageSize));
        }



        // GET: UsuariosC/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.UsuarioActivo = true;
            ViewBag.disabled = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: UsuariosC/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioActivo = true;
            ViewBag.disabled = true;
            ViewBag.Rol_id = new SelectList(db.Rol, "id", "Nombre");
            return View();
        }

        // POST: UsuariosC/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Nombre,Correo,Password,Rol_id")] Usuario usuario)
        {
            ViewBag.UsuarioActivo = true;
            ViewBag.disabled = true;
            if (ModelState.IsValid)
            {
                db.Usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Rol_id = new SelectList(db.Rol, "id", "Nombre", usuario.Rol_id);
            return View(usuario);
        }

        // GET: UsuariosC/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.UsuarioActivo = true;
            ViewBag.disabled = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.Rol_id = new SelectList(db.Rol, "id", "Nombre", usuario.Rol_id);
            return View(usuario);
        }

        // POST: UsuariosC/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Nombre,Correo,Password,Rol_id")] Usuario usuario)
        {
            ViewBag.UsuarioActivo = true;
            ViewBag.disabled = true;
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Rol_id = new SelectList(db.Rol, "id", "Nombre", usuario.Rol_id);
            return View(usuario);
        }

        // GET: UsuariosC/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.UsuarioActivo = true;
            ViewBag.disabled = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: UsuariosC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
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
