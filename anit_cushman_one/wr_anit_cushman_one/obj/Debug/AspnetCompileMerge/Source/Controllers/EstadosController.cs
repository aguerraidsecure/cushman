using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using wr_anit_cushman_one.Models;
using PagedList;
using wr_anit_cushman_one.Tags;

namespace wr_anit_cushman_one.Controllers
{
    [Autenticado]
    public class EstadosController : Controller
    {
        private CushmanContext db = new CushmanContext();

        // GET: Estados
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.UsuarioActivo = true;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.EstadoSortParam =  String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var tsg037_estados = from e in db.tsg037_estados
                           select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                tsg037_estados = tsg037_estados.Where(e => e.nb_estado.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    tsg037_estados = tsg037_estados.OrderByDescending(e => e.nb_estado);
                    break;
                case "Date":
                    tsg037_estados = tsg037_estados.OrderBy(e => e.fh_modif);
                    break;
                case "date_desc":
                    tsg037_estados = tsg037_estados.OrderByDescending(e => e.fh_modif);
                    break;
                default:
                    tsg037_estados = tsg037_estados.OrderBy(e => e.nb_estado);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(tsg037_estados.ToPagedList(pageNumber, pageSize));
        }

        // GET: Estados/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg037_estados tsg037_estados = db.tsg037_estados.Find(id);
            if (tsg037_estados == null)
            {
                return HttpNotFound();
            }
            return View(tsg037_estados);
        }

        // GET: Estados/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioActivo = true;
            return View();
        }

        // POST: Estados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cd_estado,nb_estado,fh_modif")] tsg037_estados tsg037_estados)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg037_estados.fh_modif = DateTime.Now;
                db.tsg037_estados.Add(tsg037_estados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tsg037_estados);
        }

        // GET: Estados/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg037_estados tsg037_estados = db.tsg037_estados.Find(id);
            if (tsg037_estados == null)
            {
                return HttpNotFound();
            }
            return View(tsg037_estados);
        }

        // POST: Estados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cd_estado,nb_estado,fh_modif")] tsg037_estados tsg037_estados)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg037_estados.fh_modif = DateTime.Now;
                db.Entry(tsg037_estados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tsg037_estados);
        }

        // GET: Estados/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg037_estados tsg037_estados = db.tsg037_estados.Find(id);
            if (tsg037_estados == null)
            {
                return HttpNotFound();
            }
            return View(tsg037_estados);
        }

        // POST: Estados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.UsuarioActivo = true;
            tsg037_estados tsg037_estados = db.tsg037_estados.Find(id);
            db.tsg037_estados.Remove(tsg037_estados);
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
