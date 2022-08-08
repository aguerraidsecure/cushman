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
    public class TiposGasNatController : Controller
    {
        private CushmanContext db = new CushmanContext();

        // GET: TiposGasNat
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

            var tsg021_tp_gas_natural = from e in db.tsg021_tp_gas_natural
                                 select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                tsg021_tp_gas_natural = tsg021_tp_gas_natural.Where(e => e.nb_tp_gas_natural.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    tsg021_tp_gas_natural = tsg021_tp_gas_natural.OrderByDescending(e => e.nb_tp_gas_natural);
                    break;
                case "Date":
                    tsg021_tp_gas_natural = tsg021_tp_gas_natural.OrderBy(e => e.fh_modif);
                    break;
                case "date_desc":
                    tsg021_tp_gas_natural = tsg021_tp_gas_natural.OrderByDescending(e => e.fh_modif);
                    break;
                default:
                    tsg021_tp_gas_natural = tsg021_tp_gas_natural.OrderBy(e => e.nb_tp_gas_natural);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(tsg021_tp_gas_natural.ToPagedList(pageNumber, pageSize));
        }

        // GET: TiposGasNat/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg021_tp_gas_natural tsg021_tp_gas_natural = await db.tsg021_tp_gas_natural.FindAsync(id);
            if (tsg021_tp_gas_natural == null)
            {
                return HttpNotFound();
            }
            return View(tsg021_tp_gas_natural);
        }

        // GET: TiposGasNat/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioActivo = true;
            return View();
        }

        // POST: TiposGasNat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "cd_tp_gas_natural,nb_tp_gas_natural,fh_modif")] tsg021_tp_gas_natural tsg021_tp_gas_natural)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg021_tp_gas_natural.fh_modif = DateTime.Now;
                db.tsg021_tp_gas_natural.Add(tsg021_tp_gas_natural);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tsg021_tp_gas_natural);
        }

        // GET: TiposGasNat/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg021_tp_gas_natural tsg021_tp_gas_natural = await db.tsg021_tp_gas_natural.FindAsync(id);
            if (tsg021_tp_gas_natural == null)
            {
                return HttpNotFound();
            }
            return View(tsg021_tp_gas_natural);
        }

        // POST: TiposGasNat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "cd_tp_gas_natural,nb_tp_gas_natural,fh_modif")] tsg021_tp_gas_natural tsg021_tp_gas_natural)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg021_tp_gas_natural.fh_modif = DateTime.Now;
                db.Entry(tsg021_tp_gas_natural).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tsg021_tp_gas_natural);
        }

        // GET: TiposGasNat/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg021_tp_gas_natural tsg021_tp_gas_natural = await db.tsg021_tp_gas_natural.FindAsync(id);
            if (tsg021_tp_gas_natural == null)
            {
                return HttpNotFound();
            }
            return View(tsg021_tp_gas_natural);
        }

        // POST: TiposGasNat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ViewBag.UsuarioActivo = true;
            tsg021_tp_gas_natural tsg021_tp_gas_natural = await db.tsg021_tp_gas_natural.FindAsync(id);
            db.tsg021_tp_gas_natural.Remove(tsg021_tp_gas_natural);
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
