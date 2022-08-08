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

namespace wr_anit_cushman_one.Controllers
{
    public class TerrenosController : Controller
    {
        private CushmanContext db = new CushmanContext();

        // GET: Terrenos
        public async Task<ActionResult> Index()
        {
            //var tsg001_terreno = db.tsg001_terreno.Include(t => t.tsg007_mercado).Include(t => t.tsg008_corredor_ind);
            //return View(await tsg001_terreno.ToListAsync());
            return View();
        }

        // GET: Terrenos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg001_terreno tsg001_terreno = await db.tsg001_terreno.FindAsync(id);
            if (tsg001_terreno == null)
            {
                return HttpNotFound();
            }
            return View(tsg001_terreno);
        }

        // GET: Terrenos/Create
        public ActionResult Create()
        {
            ViewBag.cd_mercado = new SelectList(db.tsg007_mercado, "cd_mercado", "nb_mercado");
            ViewBag.cd_corredor = new SelectList(db.tsg008_corredor_ind, "cd_corredor", "nb_corredor");
            return View();
        }

        // POST: Terrenos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "cd_terreno,nb_comercial,nb_calle,nu_direcion,nu_cp,cd_estado,cd_ciudad,cd_municipio,cd_colonia,cd_mercado,cd_corredor,st_parque_ind,nu_tamano,nu_latitud_longitud,nu_poligono,fh_modif")] tsg001_terreno tsg001_terreno)
        {
            if (ModelState.IsValid)
            {
                db.tsg001_terreno.Add(tsg001_terreno);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.cd_mercado = new SelectList(db.tsg007_mercado, "cd_mercado", "nb_mercado", tsg001_terreno.cd_mercado);
            ViewBag.cd_corredor = new SelectList(db.tsg008_corredor_ind, "cd_corredor", "nb_corredor", tsg001_terreno.cd_corredor);
            return View(tsg001_terreno);
        }

        // GET: Terrenos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg001_terreno tsg001_terreno = await db.tsg001_terreno.FindAsync(id);
            if (tsg001_terreno == null)
            {
                return HttpNotFound();
            }
            ViewBag.cd_mercado = new SelectList(db.tsg007_mercado, "cd_mercado", "nb_mercado", tsg001_terreno.cd_mercado);
            ViewBag.cd_corredor = new SelectList(db.tsg008_corredor_ind, "cd_corredor", "nb_corredor", tsg001_terreno.cd_corredor);
            return View(tsg001_terreno);
        }

        // POST: Terrenos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "cd_terreno,nb_comercial,nb_calle,nu_direcion,nu_cp,cd_estado,cd_ciudad,cd_municipio,cd_colonia,cd_mercado,cd_corredor,st_parque_ind,nu_tamano,nu_latitud_longitud,nu_poligono,fh_modif")] tsg001_terreno tsg001_terreno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tsg001_terreno).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.cd_mercado = new SelectList(db.tsg007_mercado, "cd_mercado", "nb_mercado", tsg001_terreno.cd_mercado);
            ViewBag.cd_corredor = new SelectList(db.tsg008_corredor_ind, "cd_corredor", "nb_corredor", tsg001_terreno.cd_corredor);
            return View(tsg001_terreno);
        }

        // GET: Terrenos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg001_terreno tsg001_terreno = await db.tsg001_terreno.FindAsync(id);
            if (tsg001_terreno == null)
            {
                return HttpNotFound();
            }
            return View(tsg001_terreno);
        }

        // POST: Terrenos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tsg001_terreno tsg001_terreno = await db.tsg001_terreno.FindAsync(id);
            db.tsg001_terreno.Remove(tsg001_terreno);
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
