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
    public class EspesoresController : Controller
    {
        private CushmanContext db = new CushmanContext();

        // GET: Espesores
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

            var tsg016_espesor = from e in db.tsg016_espesor
                                 select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                tsg016_espesor = tsg016_espesor.Where(e => e.nb_espesor.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    tsg016_espesor = tsg016_espesor.OrderByDescending(e => e.nb_espesor);
                    break;
                case "Date":
                    tsg016_espesor = tsg016_espesor.OrderBy(e => e.fh_modif);
                    break;
                case "date_desc":
                    tsg016_espesor = tsg016_espesor.OrderByDescending(e => e.fh_modif);
                    break;
                default:
                    tsg016_espesor = tsg016_espesor.OrderBy(e => e.nb_espesor);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(tsg016_espesor.ToPagedList(pageNumber, pageSize));
        }

        // GET: Espesores/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg016_espesor tsg016_espesor = await db.tsg016_espesor.FindAsync(id);
            if (tsg016_espesor == null)
            {
                return HttpNotFound();
            }
            return View(tsg016_espesor);
        }

        // GET: Espesores/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioActivo = true;
            return View();
        }

        // POST: Espesores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "cd_espesor,nb_espesor,fh_modif")] tsg016_espesor tsg016_espesor)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg016_espesor.fh_modif = DateTime.Now;
                db.tsg016_espesor.Add(tsg016_espesor);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tsg016_espesor);
        }

        // GET: Espesores/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg016_espesor tsg016_espesor = await db.tsg016_espesor.FindAsync(id);
            if (tsg016_espesor == null)
            {
                return HttpNotFound();
            }
            return View(tsg016_espesor);
        }

        // POST: Espesores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "cd_espesor,nb_espesor,fh_modif")] tsg016_espesor tsg016_espesor)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg016_espesor.fh_modif = DateTime.Now;
                db.Entry(tsg016_espesor).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tsg016_espesor);
        }

        // GET: Espesores/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg016_espesor tsg016_espesor = await db.tsg016_espesor.FindAsync(id);
            if (tsg016_espesor == null)
            {
                return HttpNotFound();
            }
            return View(tsg016_espesor);
        }

        // POST: Espesores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ViewBag.UsuarioActivo = true;
            tsg016_espesor tsg016_espesor = await db.tsg016_espesor.FindAsync(id);
            db.tsg016_espesor.Remove(tsg016_espesor);
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
