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
using wr_anit_cushman_one.Tags;

namespace wr_anticushmanone.Controllers
{
    [Autenticado]
    public class TeContactosController : Controller
    {
        private CushmanContext db = new CushmanContext();

        // GET: TeContactos
        public async Task<ActionResult> Index()
        {
            ViewBag.UsuarioActivo = true;
            var tsg028_te_contacto = db.tsg028_te_contacto.Include(t => t.tsg001_terreno);
            return View(await tsg028_te_contacto.ToListAsync());
        }

        // GET: TeContactos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg028_te_contacto tsg028_te_contacto = await db.tsg028_te_contacto.FindAsync(id);
            if (tsg028_te_contacto == null)
            {
                return HttpNotFound();
            }
            return View(tsg028_te_contacto);
        }

        // GET: TeContactos/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioActivo = true;
            ViewBag.cd_terreno = new SelectList(db.tsg001_terreno, "cd_terreno", "nb_comercial");
            return View();
        }

        // POST: TeContactos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "cd_contacto,cd_terreno,nb_propietario,nu_telefono,nb_email,nu_interior,cd_estado,cd_ciudad,cd_municipio,cd_colonia,nu_cp,st_contacto,fh_modif")] tsg028_te_contacto tsg028_te_contacto)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                db.tsg028_te_contacto.Add(tsg028_te_contacto);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.cd_terreno = new SelectList(db.tsg001_terreno, "cd_terreno", "nb_comercial", tsg028_te_contacto.cd_terreno);
            return View(tsg028_te_contacto);
        }

        // GET: TeContactos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg028_te_contacto tsg028_te_contacto = await db.tsg028_te_contacto.FindAsync(id);
            if (tsg028_te_contacto == null)
            {
                return HttpNotFound();
            }
            ViewBag.cd_terreno = new SelectList(db.tsg001_terreno, "cd_terreno", "nb_comercial", tsg028_te_contacto.cd_terreno);
            return View(tsg028_te_contacto);
        }

        // POST: TeContactos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "cd_contacto,cd_terreno,nb_propietario,nu_telefono,nb_email,nu_interior,cd_estado,cd_ciudad,cd_municipio,cd_colonia,nu_cp,st_contacto,fh_modif")] tsg028_te_contacto tsg028_te_contacto)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                db.Entry(tsg028_te_contacto).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.cd_terreno = new SelectList(db.tsg001_terreno, "cd_terreno", "nb_comercial", tsg028_te_contacto.cd_terreno);
            return View(tsg028_te_contacto);
        }

        // GET: TeContactos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg028_te_contacto tsg028_te_contacto = await db.tsg028_te_contacto.FindAsync(id);
            if (tsg028_te_contacto == null)
            {
                return HttpNotFound();
            }
            return View(tsg028_te_contacto);
        }

        // POST: TeContactos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ViewBag.UsuarioActivo = true;
            tsg028_te_contacto tsg028_te_contacto = await db.tsg028_te_contacto.FindAsync(id);
            db.tsg028_te_contacto.Remove(tsg028_te_contacto);
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
