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
    public class CajonesController : Controller
    {
        private CushmanContext db = new CushmanContext();

        // GET: Cajones
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

            var tsg019_cajon_est = from e in db.tsg019_cajon_est
                                 select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                tsg019_cajon_est = tsg019_cajon_est.Where(e => e.nb_cajon_est.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    tsg019_cajon_est = tsg019_cajon_est.OrderByDescending(e => e.nb_cajon_est);
                    break;
                case "Date":
                    tsg019_cajon_est = tsg019_cajon_est.OrderBy(e => e.fh_modif);
                    break;
                case "date_desc":
                    tsg019_cajon_est = tsg019_cajon_est.OrderByDescending(e => e.fh_modif);
                    break;
                default:
                    tsg019_cajon_est = tsg019_cajon_est.OrderBy(e => e.nb_cajon_est);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(tsg019_cajon_est.ToPagedList(pageNumber, pageSize));
        }

        // GET: Cajones/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg019_cajon_est tsg019_cajon_est = await db.tsg019_cajon_est.FindAsync(id);
            if (tsg019_cajon_est == null)
            {
                return HttpNotFound();
            }
            return View(tsg019_cajon_est);
        }

        // GET: Cajones/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioActivo = true;
            return View();
        }

        // POST: Cajones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "cd_cajon_est,nb_cajon_est,fh_modif")] tsg019_cajon_est tsg019_cajon_est)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg019_cajon_est.fh_modif = DateTime.Now;
                db.tsg019_cajon_est.Add(tsg019_cajon_est);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tsg019_cajon_est);
        }

        // GET: Cajones/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg019_cajon_est tsg019_cajon_est = await db.tsg019_cajon_est.FindAsync(id);
            if (tsg019_cajon_est == null)
            {
                return HttpNotFound();
            }
            return View(tsg019_cajon_est);
        }

        // POST: Cajones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "cd_cajon_est,nb_cajon_est,fh_modif")] tsg019_cajon_est tsg019_cajon_est)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg019_cajon_est.fh_modif = DateTime.Now;
                db.Entry(tsg019_cajon_est).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tsg019_cajon_est);
        }

        // GET: Cajones/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg019_cajon_est tsg019_cajon_est = await db.tsg019_cajon_est.FindAsync(id);
            if (tsg019_cajon_est == null)
            {
                return HttpNotFound();
            }
            return View(tsg019_cajon_est);
        }

        // POST: Cajones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ViewBag.UsuarioActivo = true;
            tsg019_cajon_est tsg019_cajon_est = await db.tsg019_cajon_est.FindAsync(id);
            db.tsg019_cajon_est.Remove(tsg019_cajon_est);
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
