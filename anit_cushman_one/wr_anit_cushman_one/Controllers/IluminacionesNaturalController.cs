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

namespace wr_anit_cushman_one.Controllers
{
    public class IluminacionesNaturalController : Controller
    {
        private CushmanContext db = new CushmanContext();

        // GET: IluminacionesNatural
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
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

            var tsg018_ilum_nat = from e in db.tsg018_ilum_nat
                                 select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                tsg018_ilum_nat = tsg018_ilum_nat.Where(e => e.nb_ilum_nat.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    tsg018_ilum_nat = tsg018_ilum_nat.OrderByDescending(e => e.nb_ilum_nat);
                    break;
                case "Date":
                    tsg018_ilum_nat = tsg018_ilum_nat.OrderBy(e => e.fh_modif);
                    break;
                case "date_desc":
                    tsg018_ilum_nat = tsg018_ilum_nat.OrderByDescending(e => e.fh_modif);
                    break;
                default:
                    tsg018_ilum_nat = tsg018_ilum_nat.OrderBy(e => e.nb_ilum_nat);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(tsg018_ilum_nat.ToPagedList(pageNumber, pageSize));
        }

        // GET: IluminacionesNatural/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg018_ilum_nat tsg018_ilum_nat = await db.tsg018_ilum_nat.FindAsync(id);
            if (tsg018_ilum_nat == null)
            {
                return HttpNotFound();
            }
            return View(tsg018_ilum_nat);
        }

        // GET: IluminacionesNatural/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IluminacionesNatural/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "cd_ilum_nat,nb_ilum_nat,fh_modif")] tsg018_ilum_nat tsg018_ilum_nat)
        {
            if (ModelState.IsValid)
            {
                tsg018_ilum_nat.fh_modif = DateTime.Now;
                db.tsg018_ilum_nat.Add(tsg018_ilum_nat);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tsg018_ilum_nat);
        }

        // GET: IluminacionesNatural/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg018_ilum_nat tsg018_ilum_nat = await db.tsg018_ilum_nat.FindAsync(id);
            if (tsg018_ilum_nat == null)
            {
                return HttpNotFound();
            }
            return View(tsg018_ilum_nat);
        }

        // POST: IluminacionesNatural/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "cd_ilum_nat,nb_ilum_nat,fh_modif")] tsg018_ilum_nat tsg018_ilum_nat)
        {
            if (ModelState.IsValid)
            {
                tsg018_ilum_nat.fh_modif = DateTime.Now;
                db.Entry(tsg018_ilum_nat).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tsg018_ilum_nat);
        }

        // GET: IluminacionesNatural/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg018_ilum_nat tsg018_ilum_nat = await db.tsg018_ilum_nat.FindAsync(id);
            if (tsg018_ilum_nat == null)
            {
                return HttpNotFound();
            }
            return View(tsg018_ilum_nat);
        }

        // POST: IluminacionesNatural/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tsg018_ilum_nat tsg018_ilum_nat = await db.tsg018_ilum_nat.FindAsync(id);
            db.tsg018_ilum_nat.Remove(tsg018_ilum_nat);
            await db.SaveChangesAsync();
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
