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
    public class MunicipiosController : Controller
    {
        private CushmanContext db = new CushmanContext();

        // GET: Municipios
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.UsuarioActivo = true;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.EstadoSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
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

            var tsg038_municipios = from e in db.tsg038_municipios
                                 select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                tsg038_municipios = tsg038_municipios.Where(e => e.nb_municipio.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    tsg038_municipios = tsg038_municipios.OrderByDescending(e => e.nb_municipio);
                    break;
                case "Date":
                    tsg038_municipios = tsg038_municipios.OrderBy(e => e.fh_modif);
                    break;
                case "date_desc":
                    tsg038_municipios = tsg038_municipios.OrderByDescending(e => e.fh_modif);
                    break;
                default:
                    tsg038_municipios = tsg038_municipios.OrderBy(e => e.nb_municipio);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(tsg038_municipios.ToPagedList(pageNumber, pageSize));
        }

        // GET: Municipios/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg038_municipios tsg038_municipios = db.tsg038_municipios.Find(id);
            if (tsg038_municipios == null)
            {
                return HttpNotFound();
            }
            return View(tsg038_municipios);
        }

        // GET: Municipios/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioActivo = true;
            ViewBag.cd_estado = new SelectList(db.tsg037_estados, "cd_estado", "nb_estado");
            return View();
        }

        // POST: Municipios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cd_municipio,cd_estado,nb_municipio,fh_modif")] tsg038_municipios tsg038_municipios)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg038_municipios.fh_modif = DateTime.Now;
                db.tsg038_municipios.Add(tsg038_municipios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cd_estado = new SelectList(db.tsg037_estados, "cd_estado", "nb_estado", tsg038_municipios.cd_estado);
            return View(tsg038_municipios);
        }

        // GET: Municipios/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg038_municipios tsg038_municipios = db.tsg038_municipios.Find(id);
            if (tsg038_municipios == null)
            {
                return HttpNotFound();
            }
            ViewBag.cd_estado = new SelectList(db.tsg037_estados, "cd_estado", "nb_estado", tsg038_municipios.cd_estado);
            return View(tsg038_municipios);
        }

        // POST: Municipios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cd_municipio,cd_estado,nb_municipio,fh_modif")] tsg038_municipios tsg038_municipios)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg038_municipios.fh_modif = DateTime.Now;
                db.Entry(tsg038_municipios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cd_estado = new SelectList(db.tsg037_estados, "cd_estado", "nb_estado", tsg038_municipios.cd_estado);
            return View(tsg038_municipios);
        }

        // GET: Municipios/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg038_municipios tsg038_municipios = db.tsg038_municipios.Find(id);
            if (tsg038_municipios == null)
            {
                return HttpNotFound();
            }
            return View(tsg038_municipios);
        }

        // POST: Municipios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.UsuarioActivo = true;
            tsg038_municipios tsg038_municipios = db.tsg038_municipios.Find(id);
            db.tsg038_municipios.Remove(tsg038_municipios);
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
