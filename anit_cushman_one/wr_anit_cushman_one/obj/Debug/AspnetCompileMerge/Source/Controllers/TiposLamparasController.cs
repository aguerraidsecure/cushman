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
    public class TiposLamparasController : Controller
    {
        private CushmanContext db = new CushmanContext();

        // GET: TiposLamparas
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

            var tsg014_tp_lampara = from e in db.tsg014_tp_lampara
                                  select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                tsg014_tp_lampara = tsg014_tp_lampara.Where(e => e.nb_tp_lampara.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    tsg014_tp_lampara = tsg014_tp_lampara.OrderByDescending(e => e.nb_tp_lampara);
                    break;
                case "Date":
                    tsg014_tp_lampara = tsg014_tp_lampara.OrderBy(e => e.fh_modif);
                    break;
                case "date_desc":
                    tsg014_tp_lampara = tsg014_tp_lampara.OrderByDescending(e => e.fh_modif);
                    break;
                default:
                    tsg014_tp_lampara = tsg014_tp_lampara.OrderBy(e => e.nb_tp_lampara);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(tsg014_tp_lampara.ToPagedList(pageNumber, pageSize));
        }

        // GET: TiposLamparas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg014_tp_lampara tsg014_tp_lampara = await db.tsg014_tp_lampara.FindAsync(id);
            if (tsg014_tp_lampara == null)
            {
                return HttpNotFound();
            }
            return View(tsg014_tp_lampara);
        }

        // GET: TiposLamparas/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioActivo = true;
            return View();
        }

        // POST: TiposLamparas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "cd_tp_lampara,nb_tp_lampara,fh_modif")] tsg014_tp_lampara tsg014_tp_lampara)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg014_tp_lampara.fh_modif = DateTime.Now;
                db.tsg014_tp_lampara.Add(tsg014_tp_lampara);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tsg014_tp_lampara);
        }

        // GET: TiposLamparas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg014_tp_lampara tsg014_tp_lampara = await db.tsg014_tp_lampara.FindAsync(id);
            if (tsg014_tp_lampara == null)
            {
                return HttpNotFound();
            }
            return View(tsg014_tp_lampara);
        }

        // POST: TiposLamparas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "cd_tp_lampara,nb_tp_lampara,fh_modif")] tsg014_tp_lampara tsg014_tp_lampara)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg014_tp_lampara.fh_modif = DateTime.Now;
                db.Entry(tsg014_tp_lampara).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tsg014_tp_lampara);
        }

        // GET: TiposLamparas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg014_tp_lampara tsg014_tp_lampara = await db.tsg014_tp_lampara.FindAsync(id);
            if (tsg014_tp_lampara == null)
            {
                return HttpNotFound();
            }
            return View(tsg014_tp_lampara);
        }

        // POST: TiposLamparas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ViewBag.UsuarioActivo = true;
            tsg014_tp_lampara tsg014_tp_lampara = await db.tsg014_tp_lampara.FindAsync(id);
            db.tsg014_tp_lampara.Remove(tsg014_tp_lampara);
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
