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
    public class TiposConstruccionController : Controller
    {
        private CushmanContext db = new CushmanContext();

        // GET: TiposConstruccion
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

            var tsg013_tp_construccion = from e in db.tsg013_tp_construccion
                                 select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                tsg013_tp_construccion = tsg013_tp_construccion.Where(e => e.nb_tp_construccion.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    tsg013_tp_construccion = tsg013_tp_construccion.OrderByDescending(e => e.nb_tp_construccion);
                    break;
                case "Date":
                    tsg013_tp_construccion = tsg013_tp_construccion.OrderBy(e => e.fh_modif);
                    break;
                case "date_desc":
                    tsg013_tp_construccion = tsg013_tp_construccion.OrderByDescending(e => e.fh_modif);
                    break;
                default:
                    tsg013_tp_construccion = tsg013_tp_construccion.OrderBy(e => e.nb_tp_construccion);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(tsg013_tp_construccion.ToPagedList(pageNumber, pageSize));
        }


        // GET: TiposConstruccion/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg013_tp_construccion tsg013_tp_construccion = await db.tsg013_tp_construccion.FindAsync(id);
            if (tsg013_tp_construccion == null)
            {
                return HttpNotFound();
            }
            return View(tsg013_tp_construccion);
        }

        // GET: TiposConstruccion/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioActivo = true;
            return View();
        }

        // POST: TiposConstruccion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "cd_tp_construccion,nb_tp_construccion,fh_modif")] tsg013_tp_construccion tsg013_tp_construccion)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg013_tp_construccion.fh_modif = DateTime.Now;
                db.tsg013_tp_construccion.Add(tsg013_tp_construccion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tsg013_tp_construccion);
        }

        // GET: TiposConstruccion/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg013_tp_construccion tsg013_tp_construccion = await db.tsg013_tp_construccion.FindAsync(id);
            if (tsg013_tp_construccion == null)
            {
                return HttpNotFound();
            }
            return View(tsg013_tp_construccion);
        }

        // POST: TiposConstruccion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "cd_tp_construccion,nb_tp_construccion,fh_modif")] tsg013_tp_construccion tsg013_tp_construccion)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg013_tp_construccion.fh_modif = DateTime.Now;
                db.Entry(tsg013_tp_construccion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tsg013_tp_construccion);
        }

        // GET: TiposConstruccion/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg013_tp_construccion tsg013_tp_construccion = await db.tsg013_tp_construccion.FindAsync(id);
            if (tsg013_tp_construccion == null)
            {
                return HttpNotFound();
            }
            return View(tsg013_tp_construccion);
        }

        // POST: TiposConstruccion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ViewBag.UsuarioActivo = true;
            tsg013_tp_construccion tsg013_tp_construccion = await db.tsg013_tp_construccion.FindAsync(id);
            db.tsg013_tp_construccion.Remove(tsg013_tp_construccion);
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
