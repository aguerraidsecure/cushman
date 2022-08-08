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
    public class ColoniasController : Controller
    {
        const string Municipio_KEY = "tsg038_municipios";
        public List<Models.tsg038_municipios> tsg038_municipios
        {
            get { return System.Web.HttpContext.Current.Session[Municipio_KEY] != null ? (List<Models.tsg038_municipios>)System.Web.HttpContext.Current.Session[Municipio_KEY] : new List<Models.tsg038_municipios>(); }
            set { System.Web.HttpContext.Current.Session[Municipio_KEY] = value; }
        }
        private CushmanContext db = new CushmanContext();

        // GET: Colonias
        //public ActionResult Index()
        //{
        //    var tsg039_colonias = db.tsg039_colonias.Include(t => t.tsg038_municipios);
        //    tsg039_colonias = tsg039_colonias.Include(t => t.tsg038_municipios.tsg037_estados);

        //    return View(tsg039_colonias.ToList());
        //}
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

            var tsg039_colonias = from e in db.tsg039_colonias
                                  select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                tsg039_colonias = tsg039_colonias.Where(e => e.nb_colonia.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    tsg039_colonias = tsg039_colonias.OrderByDescending(e => e.nb_colonia);
                    break;
                case "Date":
                    tsg039_colonias = tsg039_colonias.OrderBy(e => e.fh_modif);
                    break;
                case "date_desc":
                    tsg039_colonias = tsg039_colonias.OrderByDescending(e => e.fh_modif);
                    break;
                default:
                    tsg039_colonias = tsg039_colonias.OrderBy(e => e.nb_colonia);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(tsg039_colonias.ToPagedList(pageNumber, pageSize));
        }

        // GET: Colonias/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg039_colonias tsg039_colonias = db.tsg039_colonias.Find(id);
            if (tsg039_colonias == null)
            {
                return HttpNotFound();
            }
            return View(tsg039_colonias);
        }

        // GET: Colonias/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioActivo = true;
            // ViewBag.cd_municipio = new SelectList(db.tsg038_municipios, "cd_municipio", "nb_municipio");
            ViewBag.cd_estado = new SelectList(db.tsg037_estados, "cd_estado", "nb_estado");
            return View();
        }

        // POST: Colonias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cd_colonia,cd_municipio,cd_estado,nb_colonia,nu_cp,fh_modif")] tsg039_colonias tsg039_colonias)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg039_colonias.fh_modif = DateTime.Now;
                db.tsg039_colonias.Add(tsg039_colonias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cd_municipio = new SelectList(db.tsg038_municipios, "cd_municipio", "nb_municipio", tsg039_colonias.cd_municipio);
            return View(tsg039_colonias);
        }

        // GET: Colonias/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg039_colonias tsg039_colonias = db.tsg039_colonias.Find(id);
            if (tsg039_colonias == null)
            {
                return HttpNotFound();
            }
            ViewBag.cd_municipio = new SelectList(db.tsg038_municipios, "cd_municipio", "nb_municipio", tsg039_colonias.cd_municipio);
            return View(tsg039_colonias);
        }

        // POST: Colonias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cd_colonia,cd_municipio,cd_estado,nb_colonia,nu_cp,fh_modif")] tsg039_colonias tsg039_colonias)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg039_colonias.fh_modif = DateTime.Now;
                db.Entry(tsg039_colonias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cd_municipio = new SelectList(db.tsg038_municipios, "cd_municipio", "nb_municipio", tsg039_colonias.cd_municipio);
            return View(tsg039_colonias);
        }

        // GET: Colonias/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg039_colonias tsg039_colonias = db.tsg039_colonias.Find(id);
            if (tsg039_colonias == null)
            {
                return HttpNotFound();
            }
            return View(tsg039_colonias);
        }

        // POST: Colonias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.UsuarioActivo = true;
            tsg039_colonias tsg039_colonias = db.tsg039_colonias.Find(id);
            db.tsg039_colonias.Remove(tsg039_colonias);
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
        public JsonResult GetMunicipioByCdEstado(int cd_estado)
        {
            return Json(new SelectList(db.tsg038_municipios.Where(x => x.cd_estado == cd_estado).ToList(), "cd_municipio", "nb_municipio"));
        }

    }
}
