using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using wr_anit_cushman_one.Models;
using PagedList;
using System.Threading.Tasks;
using wr_anit_cushman_one.Tags;

namespace wr_anit_cushman_one.Controllers
{
    [Autenticado]
    public class CorredoresController : Controller
    {
        private CushmanContext db = new CushmanContext();

        // GET: Corredores
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

            var tsg008_corredor = from e in db.tsg008_corredor_ind
                                  select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                tsg008_corredor = tsg008_corredor.Where(e => e.nb_corredor.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    tsg008_corredor = tsg008_corredor.OrderByDescending(e => e.nb_corredor);
                    break;
                case "Date":
                    tsg008_corredor = tsg008_corredor.OrderBy(e => e.fh_modif);
                    break;
                case "date_desc":
                    tsg008_corredor = tsg008_corredor.OrderByDescending(e => e.fh_modif);
                    break;
                default:
                    tsg008_corredor = tsg008_corredor.OrderBy(e => e.nb_corredor);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
          
            return View(tsg008_corredor.ToPagedList(pageNumber, pageSize));
        }

        // GET: Corredores/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg008_corredor_ind tsg008_corredor_ind = await db.tsg008_corredor_ind.FindAsync(id);            
            if (tsg008_corredor_ind == null)
            {
                return HttpNotFound();
            }
            return View(tsg008_corredor_ind);
        }

        // GET: Corredores/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioActivo = true;
            return View();
        }

        // POST: Corredores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "cd_corredor,nb_corredor,fh_modif")] tsg008_corredor_ind tsg008_corredor_ind)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg008_corredor_ind.fh_modif = DateTime.Now;
                db.tsg008_corredor_ind.Add(tsg008_corredor_ind);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tsg008_corredor_ind);
        }

        // GET: Corredores/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg008_corredor_ind tsg008_corredor_ind = await db.tsg008_corredor_ind.FindAsync(id);
            if (tsg008_corredor_ind == null)
            {
                return HttpNotFound();
            }
            return View(tsg008_corredor_ind);
        }

        // POST: Corredores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "cd_corredor,nb_corredor,fh_modif")] tsg008_corredor_ind tsg008_corredor_ind)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg008_corredor_ind.fh_modif = DateTime.Now;
                db.Entry(tsg008_corredor_ind).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tsg008_corredor_ind);
        }

        // GET: Corredores/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg008_corredor_ind tsg008_corredor_ind = await db.tsg008_corredor_ind.FindAsync(id);
            if (tsg008_corredor_ind == null)
            {
                return HttpNotFound();
            }
            return View(tsg008_corredor_ind);
        }

        // POST: Corredores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ViewBag.UsuarioActivo = true;
            tsg008_corredor_ind tsg008_corredor_ind = await db.tsg008_corredor_ind.FindAsync(id);
            db.tsg008_corredor_ind.Remove(tsg008_corredor_ind);
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
