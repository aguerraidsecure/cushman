using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using wr_anit_cushman_one.Models;
using PagedList;
using wr_anit_cushman_one.Tags;

namespace wr_anit_cushman_one.Controllers
{
    [Autenticado]
    public class Nivel_PisoController : Controller
    {
        private CushmanContext db = new CushmanContext();

        // GET: Nivel_Piso
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

            var tsg042_Nivel_Piso = from e in db.tsg042_Nivel_Piso
                                 select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                tsg042_Nivel_Piso = tsg042_Nivel_Piso.Where(e => e.nb_nivel_piso.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    tsg042_Nivel_Piso = tsg042_Nivel_Piso.OrderByDescending(e => e.nb_nivel_piso);
                    break;
                case "Date":
                    tsg042_Nivel_Piso = tsg042_Nivel_Piso.OrderBy(e => e.fh_modif);
                    break;
                case "date_desc":
                    tsg042_Nivel_Piso = tsg042_Nivel_Piso.OrderByDescending(e => e.fh_modif);
                    break;
                default:
                    tsg042_Nivel_Piso = tsg042_Nivel_Piso.OrderBy(e => e.nb_nivel_piso);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(tsg042_Nivel_Piso.ToPagedList(pageNumber, pageSize));
        }


        // GET: Nivel_Piso/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg042_Nivel_Piso tsg042_Nivel_Piso = db.tsg042_Nivel_Piso.Find(id);
            if (tsg042_Nivel_Piso == null)
            {
                return HttpNotFound();
            }
            return View(tsg042_Nivel_Piso);
        }

        // GET: Nivel_Piso/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nivel_Piso/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cd_nivel_piso,nb_nivel_piso,fh_modif")] tsg042_Nivel_Piso tsg042_Nivel_Piso)
        {
            if (ModelState.IsValid)
            {
                tsg042_Nivel_Piso.fh_modif = DateTime.Now;
                db.tsg042_Nivel_Piso.Add(tsg042_Nivel_Piso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tsg042_Nivel_Piso);
        }

        // GET: Nivel_Piso/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg042_Nivel_Piso tsg042_Nivel_Piso = db.tsg042_Nivel_Piso.Find(id);
            if (tsg042_Nivel_Piso == null)
            {
                return HttpNotFound();
            }
            return View(tsg042_Nivel_Piso);
        }

        // POST: Nivel_Piso/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cd_nivel_piso,nb_nivel_piso,fh_modif")] tsg042_Nivel_Piso tsg042_Nivel_Piso)
        {
            if (ModelState.IsValid)
            {
                tsg042_Nivel_Piso.fh_modif = DateTime.Now;
                db.Entry(tsg042_Nivel_Piso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tsg042_Nivel_Piso);
        }

        // GET: Nivel_Piso/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg042_Nivel_Piso tsg042_Nivel_Piso = db.tsg042_Nivel_Piso.Find(id);
            if (tsg042_Nivel_Piso == null)
            {
                return HttpNotFound();
            }
            return View(tsg042_Nivel_Piso);
        }

        // POST: Nivel_Piso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tsg042_Nivel_Piso tsg042_Nivel_Piso = db.tsg042_Nivel_Piso.Find(id);
            db.tsg042_Nivel_Piso.Remove(tsg042_Nivel_Piso);
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
