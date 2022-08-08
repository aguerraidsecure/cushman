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
    public class TelefoniasController : Controller
    {
        private CushmanContext db = new CushmanContext();

        // GET: Telefonias
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

            var tsg043_telefonia = from e in db.tsg043_telefonia
                                   select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                tsg043_telefonia = tsg043_telefonia.Where(e => e.nb_telefonia.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    tsg043_telefonia = tsg043_telefonia.OrderByDescending(e => e.nb_telefonia);
                    break;
                case "Date":
                    tsg043_telefonia = tsg043_telefonia.OrderBy(e => e.fh_modif);
                    break;
                case "date_desc":
                    tsg043_telefonia = tsg043_telefonia.OrderByDescending(e => e.fh_modif);
                    break;
                default:
                    tsg043_telefonia = tsg043_telefonia.OrderBy(e => e.nb_telefonia);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(tsg043_telefonia.ToPagedList(pageNumber, pageSize));
        }

        // GET: Telefonias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg043_telefonia tsg043_telefonia = db.tsg043_telefonia.Find(id);
            if (tsg043_telefonia == null)
            {
                return HttpNotFound();
            }
            return View(tsg043_telefonia);
        }

        // GET: Telefonias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Telefonias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cd_telefonia,nb_telefonia,fh_modif")] tsg043_telefonia tsg043_telefonia)
        {
            if (ModelState.IsValid)
            {
                tsg043_telefonia.fh_modif = DateTime.Now;
                db.tsg043_telefonia.Add(tsg043_telefonia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tsg043_telefonia);
        }

        // GET: Telefonias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg043_telefonia tsg043_telefonia = db.tsg043_telefonia.Find(id);
            if (tsg043_telefonia == null)
            {
                return HttpNotFound();
            }
            return View(tsg043_telefonia);
        }

        // POST: Telefonias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cd_telefonia,nb_telefonia,fh_modif")] tsg043_telefonia tsg043_telefonia)
        {
            if (ModelState.IsValid)
            {
                tsg043_telefonia.fh_modif = DateTime.Now;
                db.Entry(tsg043_telefonia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tsg043_telefonia);
        }

        // GET: Telefonias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg043_telefonia tsg043_telefonia = db.tsg043_telefonia.Find(id);
            if (tsg043_telefonia == null)
            {
                return HttpNotFound();
            }
            return View(tsg043_telefonia);
        }

        // POST: Telefonias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tsg043_telefonia tsg043_telefonia = db.tsg043_telefonia.Find(id);
            db.tsg043_telefonia.Remove(tsg043_telefonia);
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
