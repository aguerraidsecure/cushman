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
    public class HvacsController : Controller
    {
        private CushmanContext db = new CushmanContext();

        // GET: Hvacs
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

            var tsg015_hvac = from e in db.tsg015_hvac
                                 select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                tsg015_hvac = tsg015_hvac.Where(e => e.nb_hvac.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    tsg015_hvac = tsg015_hvac.OrderByDescending(e => e.nb_hvac);
                    break;
                case "Date":
                    tsg015_hvac = tsg015_hvac.OrderBy(e => e.fh_modif);
                    break;
                case "date_desc":
                    tsg015_hvac = tsg015_hvac.OrderByDescending(e => e.fh_modif);
                    break;
                default:
                    tsg015_hvac = tsg015_hvac.OrderBy(e => e.nb_hvac);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(tsg015_hvac.ToPagedList(pageNumber, pageSize));
        }

        // GET: Hvacs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg015_hvac tsg015_hvac = await db.tsg015_hvac.FindAsync(id);
            if (tsg015_hvac == null)
            {
                return HttpNotFound();
            }
            return View(tsg015_hvac);
        }

        // GET: Hvacs/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioActivo = true;
            return View();
        }

        // POST: Hvacs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "cd_hvac,nb_hvac,fh_modif")] tsg015_hvac tsg015_hvac)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg015_hvac.fh_modif = DateTime.Now;
                db.tsg015_hvac.Add(tsg015_hvac);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tsg015_hvac);
        }

        // GET: Hvacs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg015_hvac tsg015_hvac = await db.tsg015_hvac.FindAsync(id);
            if (tsg015_hvac == null)
            {
                return HttpNotFound();
            }
            return View(tsg015_hvac);
        }

        // POST: Hvacs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "cd_hvac,nb_hvac,fh_modif")] tsg015_hvac tsg015_hvac)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg015_hvac.fh_modif = DateTime.Now;
                db.Entry(tsg015_hvac).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tsg015_hvac);
        }

        // GET: Hvacs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg015_hvac tsg015_hvac = await db.tsg015_hvac.FindAsync(id);
            if (tsg015_hvac == null)
            {
                return HttpNotFound();
            }
            return View(tsg015_hvac);
        }

        // POST: Hvacs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ViewBag.UsuarioActivo = true;
            tsg015_hvac tsg015_hvac = await db.tsg015_hvac.FindAsync(id);
            db.tsg015_hvac.Remove(tsg015_hvac);
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
