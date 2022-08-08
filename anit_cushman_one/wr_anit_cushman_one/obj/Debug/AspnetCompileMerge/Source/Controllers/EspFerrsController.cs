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
    public class EspFerrsController : Controller
    {
        private CushmanContext db = new CushmanContext();

        // GET: EspFerrs
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

            var tsg022_esp_ferr = from e in db.tsg022_esp_ferr
                                 select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                tsg022_esp_ferr = tsg022_esp_ferr.Where(e => e.nb_esp_ferr.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    tsg022_esp_ferr = tsg022_esp_ferr.OrderByDescending(e => e.nb_esp_ferr);
                    break;
                case "Date":
                    tsg022_esp_ferr = tsg022_esp_ferr.OrderBy(e => e.fh_modif);
                    break;
                case "date_desc":
                    tsg022_esp_ferr = tsg022_esp_ferr.OrderByDescending(e => e.fh_modif);
                    break;
                default:
                    tsg022_esp_ferr = tsg022_esp_ferr.OrderBy(e => e.nb_esp_ferr);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(tsg022_esp_ferr.ToPagedList(pageNumber, pageSize));
        }

        // GET: EspFerrs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg022_esp_ferr tsg022_esp_ferr = await db.tsg022_esp_ferr.FindAsync(id);
            if (tsg022_esp_ferr == null)
            {
                return HttpNotFound();
            }
            return View(tsg022_esp_ferr);
        }

        // GET: EspFerrs/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioActivo = true;
            return View();
        }

        // POST: EspFerrs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "cd_esp_ferr,nb_esp_ferr,fh_modif")] tsg022_esp_ferr tsg022_esp_ferr)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg022_esp_ferr.fh_modif = DateTime.Now;
                db.tsg022_esp_ferr.Add(tsg022_esp_ferr);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tsg022_esp_ferr);
        }

        // GET: EspFerrs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg022_esp_ferr tsg022_esp_ferr = await db.tsg022_esp_ferr.FindAsync(id);
            if (tsg022_esp_ferr == null)
            {
                return HttpNotFound();
            }
            return View(tsg022_esp_ferr);
        }

        // POST: EspFerrs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "cd_esp_ferr,nb_esp_ferr,fh_modif")] tsg022_esp_ferr tsg022_esp_ferr)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg022_esp_ferr.fh_modif = DateTime.Now;
                db.Entry(tsg022_esp_ferr).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tsg022_esp_ferr);
        }

        // GET: EspFerrs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg022_esp_ferr tsg022_esp_ferr = await db.tsg022_esp_ferr.FindAsync(id);
            if (tsg022_esp_ferr == null)
            {
                return HttpNotFound();
            }
            return View(tsg022_esp_ferr);
        }

        // POST: EspFerrs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ViewBag.UsuarioActivo = true;
            tsg022_esp_ferr tsg022_esp_ferr = await db.tsg022_esp_ferr.FindAsync(id);
            db.tsg022_esp_ferr.Remove(tsg022_esp_ferr);
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
