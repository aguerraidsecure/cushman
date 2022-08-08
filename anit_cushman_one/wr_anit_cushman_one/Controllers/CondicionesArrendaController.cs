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
    public class CondicionesArrendaController : Controller
    {
        private CushmanContext db = new CushmanContext();

        // GET: CondicionesArrenda
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

            var tsg024_cond_arr = from e in db.tsg024_cond_arr
                                  select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                tsg024_cond_arr = tsg024_cond_arr.Where(e => e.nb_cond_arr.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    tsg024_cond_arr = tsg024_cond_arr.OrderByDescending(e => e.nb_cond_arr);
                    break;
                case "Date":
                    tsg024_cond_arr = tsg024_cond_arr.OrderBy(e => e.fh_modif);
                    break;
                case "date_desc":
                    tsg024_cond_arr = tsg024_cond_arr.OrderByDescending(e => e.fh_modif);
                    break;
                default:
                    tsg024_cond_arr = tsg024_cond_arr.OrderBy(e => e.nb_cond_arr);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(tsg024_cond_arr.ToPagedList(pageNumber, pageSize));
        }

        // GET: CondicionesArrenda/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg024_cond_arr tsg024_cond_arr = await db.tsg024_cond_arr.FindAsync(id);
            if (tsg024_cond_arr == null)
            {
                return HttpNotFound();
            }
            return View(tsg024_cond_arr);
        }

        // GET: CondicionesArrenda/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioActivo = true;
            return View();
        }

        // POST: CondicionesArrenda/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "cd_cond_arr,nb_cond_arr,fh_modif")] tsg024_cond_arr tsg024_cond_arr)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg024_cond_arr.fh_modif = DateTime.Now;
                db.tsg024_cond_arr.Add(tsg024_cond_arr);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tsg024_cond_arr);
        }

        // GET: CondicionesArrenda/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg024_cond_arr tsg024_cond_arr = await db.tsg024_cond_arr.FindAsync(id);
            if (tsg024_cond_arr == null)
            {
                return HttpNotFound();
            }
            return View(tsg024_cond_arr);
        }

        // POST: CondicionesArrenda/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "cd_cond_arr,nb_cond_arr,fh_modif")] tsg024_cond_arr tsg024_cond_arr)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg024_cond_arr.fh_modif = DateTime.Now;
                db.Entry(tsg024_cond_arr).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tsg024_cond_arr);
        }

        // GET: CondicionesArrenda/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg024_cond_arr tsg024_cond_arr = await db.tsg024_cond_arr.FindAsync(id);
            if (tsg024_cond_arr == null)
            {
                return HttpNotFound();
            }
            return View(tsg024_cond_arr);
        }

        // POST: CondicionesArrenda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ViewBag.UsuarioActivo = true;
            tsg024_cond_arr tsg024_cond_arr = await db.tsg024_cond_arr.FindAsync(id);
            db.tsg024_cond_arr.Remove(tsg024_cond_arr);
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
