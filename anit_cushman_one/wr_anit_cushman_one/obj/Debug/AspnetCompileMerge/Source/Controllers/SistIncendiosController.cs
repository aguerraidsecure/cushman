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
    public class SistIncendiosController : Controller
    {
        private CushmanContext db = new CushmanContext();

        // GET: SistIncendios
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

            var tsg012_sist_incendio = from e in db.tsg012_sist_incendio
                                 select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                tsg012_sist_incendio = tsg012_sist_incendio.Where(e => e.nb_sist_inc.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    tsg012_sist_incendio = tsg012_sist_incendio.OrderByDescending(e => e.nb_sist_inc);
                    break;
                case "Date":
                    tsg012_sist_incendio = tsg012_sist_incendio.OrderBy(e => e.fh_modif);
                    break;
                case "date_desc":
                    tsg012_sist_incendio = tsg012_sist_incendio.OrderByDescending(e => e.fh_modif);
                    break;
                default:
                    tsg012_sist_incendio = tsg012_sist_incendio.OrderBy(e => e.nb_sist_inc);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(tsg012_sist_incendio.ToPagedList(pageNumber, pageSize));
        }

        // GET: SistIncendios/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg012_sist_incendio tsg012_sist_incendio = await db.tsg012_sist_incendio.FindAsync(id);
            if (tsg012_sist_incendio == null)
            {
                return HttpNotFound();
            }
            return View(tsg012_sist_incendio);
        }

        // GET: SistIncendios/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioActivo = true;
            return View();
        }

        // POST: SistIncendios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "cd_sist_inc,nb_sist_inc,fh_modif")] tsg012_sist_incendio tsg012_sist_incendio)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg012_sist_incendio.fh_modif = DateTime.Now;
                db.tsg012_sist_incendio.Add(tsg012_sist_incendio);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tsg012_sist_incendio);
        }

        // GET: SistIncendios/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg012_sist_incendio tsg012_sist_incendio = await db.tsg012_sist_incendio.FindAsync(id);
            if (tsg012_sist_incendio == null)
            {
                return HttpNotFound();
            }
            return View(tsg012_sist_incendio);
        }

        // POST: SistIncendios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "cd_sist_inc,nb_sist_inc,fh_modif")] tsg012_sist_incendio tsg012_sist_incendio)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg012_sist_incendio.fh_modif = DateTime.Now;
                db.Entry(tsg012_sist_incendio).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tsg012_sist_incendio);
        }

        // GET: SistIncendios/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg012_sist_incendio tsg012_sist_incendio = await db.tsg012_sist_incendio.FindAsync(id);
            if (tsg012_sist_incendio == null)
            {
                return HttpNotFound();
            }
            return View(tsg012_sist_incendio);
        }

        // POST: SistIncendios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ViewBag.UsuarioActivo = true;
            tsg012_sist_incendio tsg012_sist_incendio = await db.tsg012_sist_incendio.FindAsync(id);
            db.tsg012_sist_incendio.Remove(tsg012_sist_incendio);
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
