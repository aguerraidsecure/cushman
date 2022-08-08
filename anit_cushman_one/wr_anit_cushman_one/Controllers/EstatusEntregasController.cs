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
    public class EstatusEntregasController : Controller
    {
        private CushmanContext db = new CushmanContext();

        // GET: EstatusEntregas
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

            var tsg017_st_entrega = from e in db.tsg017_st_entrega
                                 select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                tsg017_st_entrega = tsg017_st_entrega.Where(e => e.nb_st_entrega.Contains(searchString));                
            }
            switch (sortOrder)
            {
                case "name_desc":
                    tsg017_st_entrega = tsg017_st_entrega.OrderByDescending(e => e.nb_st_entrega);
                    break;
                case "Date":
                    tsg017_st_entrega = tsg017_st_entrega.OrderBy(e => e.fh_modif);
                    break;
                case "date_desc":
                    tsg017_st_entrega = tsg017_st_entrega.OrderByDescending(e => e.fh_modif);
                    break;
                default:
                    tsg017_st_entrega = tsg017_st_entrega.OrderBy(e => e.nb_st_entrega);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(tsg017_st_entrega.ToPagedList(pageNumber, pageSize));
        }

        // GET: EstatusEntregas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg017_st_entrega tsg017_st_entrega = await db.tsg017_st_entrega.FindAsync(id);
            if (tsg017_st_entrega == null)
            {
                return HttpNotFound();
            }
            return View(tsg017_st_entrega);
        }

        // GET: EstatusEntregas/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioActivo = true;
            return View();
        }

        // POST: EstatusEntregas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "cd_st_entrega,nb_st_entrega,fh_modif")] tsg017_st_entrega tsg017_st_entrega)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg017_st_entrega.fh_modif = DateTime.Now;
                db.tsg017_st_entrega.Add(tsg017_st_entrega);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tsg017_st_entrega);
        }

        // GET: EstatusEntregas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg017_st_entrega tsg017_st_entrega = await db.tsg017_st_entrega.FindAsync(id);
            if (tsg017_st_entrega == null)
            {
                return HttpNotFound();
            }
            return View(tsg017_st_entrega);
        }

        // POST: EstatusEntregas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "cd_st_entrega,nb_st_entrega,fh_modif")] tsg017_st_entrega tsg017_st_entrega)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg017_st_entrega.fh_modif = DateTime.Now;
                db.Entry(tsg017_st_entrega).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tsg017_st_entrega);
        }

        // GET: EstatusEntregas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg017_st_entrega tsg017_st_entrega = await db.tsg017_st_entrega.FindAsync(id);
            if (tsg017_st_entrega == null)
            {
                return HttpNotFound();
            }
            return View(tsg017_st_entrega);
        }

        // POST: EstatusEntregas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ViewBag.UsuarioActivo = true;
            tsg017_st_entrega tsg017_st_entrega = await db.tsg017_st_entrega.FindAsync(id);
            db.tsg017_st_entrega.Remove(tsg017_st_entrega);
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
