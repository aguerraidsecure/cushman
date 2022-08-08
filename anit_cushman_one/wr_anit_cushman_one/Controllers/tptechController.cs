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
public class tptechController : Controller
    {
        private CushmanContext db = new CushmanContext();

    // GET: tptech
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

        var tsg041_tp_tech = from e in db.tsg041_tp_tech
                             select e;
        if (!String.IsNullOrEmpty(searchString))
        {
            tsg041_tp_tech = tsg041_tp_tech.Where(e => e.nb_tp_tech.Contains(searchString));
        }
        switch (sortOrder)
        {
            case "name_desc":
                tsg041_tp_tech = tsg041_tp_tech.OrderByDescending(e => e.nb_tp_tech);
                break;
            case "Date":
                tsg041_tp_tech = tsg041_tp_tech.OrderBy(e => e.fh_modif);
                break;
            case "date_desc":
                tsg041_tp_tech = tsg041_tp_tech.OrderByDescending(e => e.fh_modif);
                break;
            default:
                tsg041_tp_tech = tsg041_tp_tech.OrderBy(e => e.nb_tp_tech);
                break;
        }

        int pageSize = 5;
        int pageNumber = (page ?? 1);

        return View(tsg041_tp_tech.ToPagedList(pageNumber, pageSize));
    }

    // GET: tptech/Details/5
    public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tsg041_tp_tech tsg041_tp_tech = db.tsg041_tp_tech.Find(id);
            if (tsg041_tp_tech == null)
            {
                return HttpNotFound();
            }
            return View(tsg041_tp_tech);
        }

        // GET: tptech/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tptech/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cd_tp_tech,nb_tp_tech,fh_modif")] tsg041_tp_tech tsg041_tp_tech)
        {
            if (ModelState.IsValid)
            {
                tsg041_tp_tech.fh_modif = DateTime.Now;
                db.tsg041_tp_tech.Add(tsg041_tp_tech);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tsg041_tp_tech);
        }

        // GET: tptech/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg041_tp_tech tsg041_tp_tech = db.tsg041_tp_tech.Find(id);
            if (tsg041_tp_tech == null)
            {
                return HttpNotFound();
            }
            return View(tsg041_tp_tech);
        }

        // POST: tptech/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cd_tp_tech,nb_tp_tech,fh_modif")] tsg041_tp_tech tsg041_tp_tech)
        {
            if (ModelState.IsValid)
            {
                tsg041_tp_tech.fh_modif = DateTime.Now;
                db.Entry(tsg041_tp_tech).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tsg041_tp_tech);
        }

        // GET: tptech/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg041_tp_tech tsg041_tp_tech = db.tsg041_tp_tech.Find(id);
            if (tsg041_tp_tech == null)
            {
                return HttpNotFound();
            }
            return View(tsg041_tp_tech);
        }

        // POST: tptech/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tsg041_tp_tech tsg041_tp_tech = db.tsg041_tp_tech.Find(id);
            db.tsg041_tp_tech.Remove(tsg041_tp_tech);
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
