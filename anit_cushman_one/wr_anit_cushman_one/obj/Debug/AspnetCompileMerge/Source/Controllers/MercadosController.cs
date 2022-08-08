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
using wr_anit_cushman_one.Tags;

namespace wr_anit_cushman_one.Controllers
{
    [Autenticado]
    public class MercadosController : Controller
    {
        private CushmanContext db = new CushmanContext();

        // GET: Mercados
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

            var tsg007_mercado = from e in db.tsg007_mercado
                                  select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                tsg007_mercado = tsg007_mercado.Where(e => e.nb_mercado.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    tsg007_mercado = tsg007_mercado.OrderByDescending(e => e.nb_mercado);
                    break;
                case "Date":
                    tsg007_mercado = tsg007_mercado.OrderBy(e => e.fh_modif);
                    break;
                case "date_desc":
                    tsg007_mercado = tsg007_mercado.OrderByDescending(e => e.fh_modif);
                    break;
                default:
                    tsg007_mercado = tsg007_mercado.OrderBy(e => e.nb_mercado);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(tsg007_mercado.ToPagedList(pageNumber, pageSize));
        }

        // GET: Mercados/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg007_mercado tsg007_mercado = db.tsg007_mercado.Find(id);
            if (tsg007_mercado == null)
            {
                return HttpNotFound();
            }
            return View(tsg007_mercado);
        }

        // GET: Mercados/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioActivo = true;
            return View();
        }

        // POST: Mercados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cd_mercado,nb_mercado,fh_modif")] tsg007_mercado tsg007_mercado)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg007_mercado.fh_modif = DateTime.Now;
                db.tsg007_mercado.Add(tsg007_mercado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tsg007_mercado);
        }

        // GET: Mercados/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg007_mercado tsg007_mercado = db.tsg007_mercado.Find(id);
            if (tsg007_mercado == null)
            {
                return HttpNotFound();
            }
            return View(tsg007_mercado);
        }

        // POST: Mercados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cd_mercado,nb_mercado,fh_modif")] tsg007_mercado tsg007_mercado)
        {
            ViewBag.UsuarioActivo = true;
            if (ModelState.IsValid)
            {
                tsg007_mercado.fh_modif = DateTime.Now;
                db.Entry(tsg007_mercado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tsg007_mercado);
        }

        // GET: Mercados/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.UsuarioActivo = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg007_mercado tsg007_mercado = db.tsg007_mercado.Find(id);
            if (tsg007_mercado == null)
            {
                return HttpNotFound();
            }
            return View(tsg007_mercado);
        }

        // POST: Mercados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.UsuarioActivo = true;
            tsg007_mercado tsg007_mercado = db.tsg007_mercado.Find(id);
            db.tsg007_mercado.Remove(tsg007_mercado);
            db.SaveChanges();
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
