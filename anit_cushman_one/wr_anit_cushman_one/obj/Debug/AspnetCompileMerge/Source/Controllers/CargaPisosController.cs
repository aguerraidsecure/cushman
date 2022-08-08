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
    public class CargaPisosController : Controller
    {
        private CushmanContext db = new CushmanContext();

        // GET: CargaPisos
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

            var tsg011_carga_piso = from e in db.tsg011_carga_piso
                                 select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                tsg011_carga_piso = tsg011_carga_piso.Where(e => e.nb_carga.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    tsg011_carga_piso = tsg011_carga_piso.OrderByDescending(e => e.nb_carga);
                    break;
                case "Date":
                    tsg011_carga_piso = tsg011_carga_piso.OrderBy(e => e.fh_modif);
                    break;
                case "date_desc":
                    tsg011_carga_piso = tsg011_carga_piso.OrderByDescending(e => e.fh_modif);
                    break;
                default:
                    tsg011_carga_piso = tsg011_carga_piso.OrderBy(e => e.nb_carga);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(tsg011_carga_piso.ToPagedList(pageNumber, pageSize));
        }

        // GET: CargaPisos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg011_carga_piso tsg011_carga_piso = await db.tsg011_carga_piso.FindAsync(id);
            if (tsg011_carga_piso == null)
            {
                return HttpNotFound();
            }
            return View(tsg011_carga_piso);
        }

        // GET: CargaPisos/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioActivo = true;
            return View();
        }

        // POST: CargaPisos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "cd_carga,nb_carga,fh_modif")] tsg011_carga_piso tsg011_carga_piso)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg011_carga_piso.fh_modif = DateTime.Now;
                db.tsg011_carga_piso.Add(tsg011_carga_piso);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tsg011_carga_piso);
        }

        // GET: CargaPisos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg011_carga_piso tsg011_carga_piso = await db.tsg011_carga_piso.FindAsync(id);
            if (tsg011_carga_piso == null)
            {
                return HttpNotFound();
            }
            return View(tsg011_carga_piso);
        }

        // POST: CargaPisos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "cd_carga,nb_carga,fh_modif")] tsg011_carga_piso tsg011_carga_piso)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg011_carga_piso.fh_modif = DateTime.Now;
                db.Entry(tsg011_carga_piso).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tsg011_carga_piso);
        }

        // GET: CargaPisos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg011_carga_piso tsg011_carga_piso = await db.tsg011_carga_piso.FindAsync(id);
            if (tsg011_carga_piso == null)
            {
                return HttpNotFound();
            }
            return View(tsg011_carga_piso);
        }

        // POST: CargaPisos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ViewBag.UsuarioActivo = true;
            tsg011_carga_piso tsg011_carga_piso = await db.tsg011_carga_piso.FindAsync(id);
            db.tsg011_carga_piso.Remove(tsg011_carga_piso);
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
