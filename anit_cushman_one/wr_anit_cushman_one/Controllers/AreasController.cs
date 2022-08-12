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
    public class AreasController : Controller
    {
        private CushmanContext db = new CushmanContext();
        tsg029_bitacora_mov tsg029_bitacora_mov = new tsg029_bitacora_mov();

        tsg010_area_of tsg010_area_of;

        public AreasController()
        {
            tsg010_area_of = new tsg010_area_of();
        }
        // GET: Areas
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

            var tsg010_area_of = from e in db.tsg010_area_of
                                 select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                tsg010_area_of = tsg010_area_of.Where(e => e.nb_area.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    tsg010_area_of = tsg010_area_of.OrderByDescending(e => e.nb_area);
                    break;
                case "Date":
                    tsg010_area_of = tsg010_area_of.OrderBy(e => e.fh_modif);
                    break;
                case "date_desc":
                    tsg010_area_of = tsg010_area_of.OrderByDescending(e => e.fh_modif);
                    break;
                default:
                    tsg010_area_of = tsg010_area_of.OrderBy(e => e.nb_area);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(tsg010_area_of.ToPagedList(pageNumber, pageSize));
        }

        // GET: Areas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg010_area_of tsg010_area_of = await db.tsg010_area_of.FindAsync(id);
            if (tsg010_area_of == null)
            {
                return HttpNotFound();
            }
            return View(tsg010_area_of);
        }

        // GET: Areas/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioActivo = true;
            return View(tsg010_area_of);
        }

        // POST: Areas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "cd_area,nb_area,fh_modif")] tsg010_area_of tsg010_area_of)
        {
             

            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg010_area_of.fh_modif = DateTime.Now;
                db.tsg010_area_of.Add(tsg010_area_of);
                await db.SaveChangesAsync();
                tsg029_bitacora_mov.GuardaBitacora("tsg010_area_of","cd_area", tsg010_area_of.cd_area, "Crear");
                return RedirectToAction("Index");
            }

            return View(tsg010_area_of);
        }

        // GET: Areas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg010_area_of tsg010_area_of = await db.tsg010_area_of.FindAsync(id);
            if (tsg010_area_of == null)
            {
                return HttpNotFound();
            }
            return View(tsg010_area_of);
        }

        // POST: Areas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "cd_area,nb_area,fh_modif")] tsg010_area_of tsg010_area_of)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg010_area_of.fh_modif = DateTime.Now;
                db.Entry(tsg010_area_of).State = EntityState.Modified;                
                await db.SaveChangesAsync();
                tsg029_bitacora_mov.GuardaBitacora("tsg010_area_of", "cd_area", tsg010_area_of.cd_area, "Modificar");
                return RedirectToAction("Index");
            }
            return View(tsg010_area_of);
        }

        // GET: Areas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg010_area_of tsg010_area_of = await db.tsg010_area_of.FindAsync(id);
            if (tsg010_area_of == null)
            {
                return HttpNotFound();
            }
            return View(tsg010_area_of);
        }

        // POST: Areas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ViewBag.UsuarioActivo = true;
            tsg010_area_of tsg010_area_of = await db.tsg010_area_of.FindAsync(id);
            db.tsg010_area_of.Remove(tsg010_area_of);
            await db.SaveChangesAsync();
            tsg029_bitacora_mov.GuardaBitacora("tsg010_area_of", "cd_area", tsg010_area_of.cd_area, "Eliminar");
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
