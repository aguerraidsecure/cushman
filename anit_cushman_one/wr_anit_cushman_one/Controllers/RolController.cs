using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure.ServiceRuntime;
using Model;
using Model.Commons;
using PagedList;

namespace wr_anit_cushman_one.Controllers
{
    public class RolController : Controller
    {
        private CushmanContext db = new CushmanContext();

        // GET: Rol
        //public ActionResult Index()
        //{
        //    ViewBag.UsuarioActivo = true;
        //    ViewBag.disabled = true;
        //    return View(db.Rol.ToList());
        //}
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.UsuarioActivo = true;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.EstadoSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var Rol = from e in db.Rol
                                 select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                Rol = Rol.Where(e => e.Nombre.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    Rol = Rol.OrderByDescending(e => e.Nombre);
                    break;
                //case "Date":
                //    Rol = Rol.OrderBy(e => e.Permiso);
                //    break;
                //case "date_desc":
                //    Rol = Rol.OrderByDescending(e => e.Permiso);
                //    break;
                default:
                    Rol = Rol.OrderBy(e => e.Nombre);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(Rol.ToPagedList(pageNumber, pageSize));
        }

        // GET: Rol/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.UsuarioActivo = true;
            ViewBag.disabled = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rol rol = db.Rol.Find(id);
            if (rol == null)
            {
                return HttpNotFound();
            }
            return View(rol);
        }

        // GET: Rol/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioActivo = true;
            ViewBag.disabled = true;
            return View();
        }

        // POST: Rol/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Nombre")] Rol rol)
        {
            ViewBag.UsuarioActivo = true;
            ViewBag.disabled = true;
            if (ModelState.IsValid)
            {
                db.Rol.Add(rol);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rol);
        }

        // GET: Rol/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.UsuarioActivo = true;
            ViewBag.disabled = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rol rol = db.Rol.Find(id);
            if (rol == null)
            {
                return HttpNotFound();
            }
            return View(rol);
        }

        // POST: Rol/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Nombre")] Rol rol)
        {
            ViewBag.UsuarioActivo = true;
            ViewBag.disabled = true;
            if (ModelState.IsValid)
            {
                db.Entry(rol).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rol);
        }

        // GET: Rol/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.UsuarioActivo = true;
            ViewBag.disabled = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rol rol = db.Rol.Find(id);
            if (rol == null)
            {
                return HttpNotFound();
            }
            return View(rol);
        }

        // POST: Rol/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rol rol = db.Rol.Find(id);
            db.Rol.Remove(rol);
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

        public ActionResult Roles()
        {
            ViewBag.UsuarioActivo = true;
            ViewBag.disabled = true;
            return View(db.Rol.ToList());
        }

        public ActionResult Crud(int? id = 0)
        {
            ViewBag.UsuarioActivo = true;
            ViewBag.disabled = true;
            ViewBag.Permisos = db.Permiso.ToList();                        
            Rol rol = db.Rol.Find(id);
                                  
            return View(rol);
        }
        [HttpPost]
        public JsonResult GuardarRol(Rol model, string[] permisos_seleccionados)
        {
            var respuesta = new ResponseModel
            {
                response = true,
                href = "~/Rol/Roles/" + model.id,
                message = ""
            };

            //var opciones = Enum.GetNames(typeof(RolesPermisos));
            //var opciones = Enum.GetValues(typeof(RolesPermisos));

            if (permisos_seleccionados != null)
            {
                foreach (var c in permisos_seleccionados)
                {
                    RolesPermisos MyRolPermiso = (RolesPermisos)Enum.Parse(typeof(RolesPermisos), c, true);

                    model.Permiso.Add(new Permiso { PermisoID = MyRolPermiso });
                }
                //model.Permiso.Add(new Permiso { PermisoID = opciones[c] });
            }
            else
            {
                ModelState.AddModelError("permisos", "Debe seleccionar por lo menos un permiso");
                respuesta.response = false;
                respuesta.message = "Debe seleccionar por lo menos un permiso";
            }

            if (ModelState.IsValid)
            {
                model.Guardar();
            }

            return Json(respuesta);
        }
    }
}
