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
    public class BrokersController : Controller
    {
        private CushmanContext db = new CushmanContext();

        // GET: Brokers
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.EstadoSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            //return View(db.tsg047_brokers.ToList());
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var tsg047_brokers = from e in db.tsg047_brokers
                                 select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                tsg047_brokers = tsg047_brokers.Where(e => e.nb_broker.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    tsg047_brokers = tsg047_brokers.OrderByDescending(e => e.nb_broker);
                    break;
                case "Date":
                    tsg047_brokers = tsg047_brokers.OrderBy(e => e.fh_modif);
                    break;
                case "date_desc":
                    tsg047_brokers = tsg047_brokers.OrderByDescending(e => e.fh_modif);
                    break;
                default:
                    tsg047_brokers = tsg047_brokers.OrderBy(e => e.nb_broker);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(tsg047_brokers.ToPagedList(pageNumber, pageSize));
        }

        // GET: Brokers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg047_brokers tsg047_brokers = db.tsg047_brokers.Find(id);
            if (tsg047_brokers == null)
            {
                return HttpNotFound();
            }
            return View(tsg047_brokers);
        }

        // GET: Brokers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brokers/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cd_broker,nb_broker,fh_modif")] tsg047_brokers tsg047_brokers)
        {
            if (ModelState.IsValid)
            {
                db.tsg047_brokers.Add(tsg047_brokers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tsg047_brokers);
        }

        // GET: Brokers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg047_brokers tsg047_brokers = db.tsg047_brokers.Find(id);
            if (tsg047_brokers == null)
            {
                return HttpNotFound();
            }
            return View(tsg047_brokers);
        }

        // POST: Brokers/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cd_broker,nb_broker,fh_modif")] tsg047_brokers tsg047_brokers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tsg047_brokers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tsg047_brokers);
        }

        // GET: Brokers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg047_brokers tsg047_brokers = db.tsg047_brokers.Find(id);
            if (tsg047_brokers == null)
            {
                return HttpNotFound();
            }
            return View(tsg047_brokers);
        }

        // POST: Brokers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tsg047_brokers tsg047_brokers = db.tsg047_brokers.Find(id);
            db.tsg047_brokers.Remove(tsg047_brokers);
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
