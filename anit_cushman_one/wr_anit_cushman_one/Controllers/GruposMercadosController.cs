using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using wr_anit_cushman_one.Models;


namespace wr_anit_cushman_one.Controllers
{
    public class GruposMercadosController : Controller
    {
        protected JsonSerializerSettings jsonSerializerSettings;
        private CushmanContext db = new CushmanContext();

        // GET: GruposMercados
        public ActionResult Index()
        {
            ViewBag.UsuarioActivo = true;
            var tsg048_grupos_mercados = db.tsg048_grupos_mercados.Include(t => t.tsg007_mercado).Include(t => t.tsg038_municipios);
            return View(tsg048_grupos_mercados.ToList());
        }

        // GET: GruposMercados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg048_grupos_mercados tsg048_grupos_mercados = db.tsg048_grupos_mercados.Find(id);
            if (tsg048_grupos_mercados == null)
            {
                return HttpNotFound();
            }
            return View(tsg048_grupos_mercados);
        }

        // GET: GruposMercados/Create
        public ActionResult Create()
        {
            ViewBag.cd_mercado = new SelectList(db.tsg007_mercado, "cd_mercado", "nb_mercado");
            ViewBag.cd_municipio = new SelectList(db.tsg038_municipios, "cd_municipio", "nb_municipio");
            return View();
        }

        // POST: GruposMercados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cd_grupo,cd_mercado,cd_estado,cd_municipio,fh_modif")] tsg048_grupos_mercados tsg048_grupos_mercados)
        {
            if (ModelState.IsValid)
            {
                db.tsg048_grupos_mercados.Add(tsg048_grupos_mercados);
                db.SaveChanges();

                var result = (from a in db.tsg048_grupos_mercados                       
                       select new 
                       {
                           cd_grupo = a.cd_grupo,                           
                       });

                result = result.OrderByDescending(c => c.cd_grupo);
                var data = result.ToList();

                return Json(data, JsonRequestBehavior.AllowGet);
            }

            ViewBag.cd_mercado = new SelectList(db.tsg007_mercado, "cd_mercado", "nb_mercado", tsg048_grupos_mercados.cd_mercado);
            ViewBag.cd_municipio = new SelectList(db.tsg038_municipios, "cd_municipio", "nb_municipio", tsg048_grupos_mercados.cd_municipio);
            return View(tsg048_grupos_mercados);
        }
        // POST: GruposMercados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreaMercado([Bind(Include = "cd_mercado,cd_estado,cd_municipio")] tsg048_grupos_mercados tsg048_grupos_mercados)
        {
            if (ModelState.IsValid)
            {
                var verifyMunicipio = (from a in db.tsg048_grupos_mercados
                                      where a.cd_municipio == tsg048_grupos_mercados.cd_municipio &
                                      a.cd_mercado == tsg048_grupos_mercados.cd_mercado
                                      select new
                                      {
                                          a.cd_municipio
                                      }).FirstOrDefault();
                if (null != verifyMunicipio)
                {                 
                    return new HttpStatusCodeResult(HttpStatusCode.Conflict, "El municipio ya se encuentra registrado");
                }


                var MdoCreate = new tsg048_grupos_mercados()
                {
                    cd_mercado  = tsg048_grupos_mercados.cd_mercado,
                    cd_estado   = tsg048_grupos_mercados.cd_estado,                    
                    cd_municipio    = tsg048_grupos_mercados.cd_municipio,
                    fh_modif    = DateTime.Now
                };

                db.tsg048_grupos_mercados.Add(MdoCreate);
                db.SaveChanges();

                var result = (
                              from a in db.tsg048_grupos_mercados
                              orderby a.cd_grupo descending
                              select new 
                              {
                                  cd_grupo = a.cd_grupo
                              }).FirstOrDefault();
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            ViewBag.cd_mercado = new SelectList(db.tsg007_mercado, "cd_mercado", "nb_mercado", tsg048_grupos_mercados.cd_mercado);
            ViewBag.cd_municipio = new SelectList(db.tsg038_municipios, "cd_municipio", "nb_municipio", tsg048_grupos_mercados.cd_municipio);
            return View(tsg048_grupos_mercados);
        }
        // GET: GruposMercados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg048_grupos_mercados tsg048_grupos_mercados = db.tsg048_grupos_mercados.Find(id);
            if (tsg048_grupos_mercados == null)
            {
                return HttpNotFound();
            }
            ViewBag.cd_mercado = new SelectList(db.tsg007_mercado, "cd_mercado", "nb_mercado", tsg048_grupos_mercados.cd_mercado);
            ViewBag.cd_municipio = new SelectList(db.tsg038_municipios, "cd_municipio", "nb_municipio", tsg048_grupos_mercados.cd_municipio);
            return View(tsg048_grupos_mercados);
        }

        // POST: GruposMercados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cd_grupo,cd_mercado,cd_estado,cd_municipio,fh_modif")] tsg048_grupos_mercados tsg048_grupos_mercados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tsg048_grupos_mercados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cd_mercado = new SelectList(db.tsg007_mercado, "cd_mercado", "nb_mercado", tsg048_grupos_mercados.cd_mercado);
            ViewBag.cd_municipio = new SelectList(db.tsg038_municipios, "cd_municipio", "nb_municipio", tsg048_grupos_mercados.cd_municipio);
            return View(tsg048_grupos_mercados);
        }

        // GET: GruposMercados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg048_grupos_mercados tsg048_grupos_mercados = db.tsg048_grupos_mercados.Find(id);
            if (tsg048_grupos_mercados == null)
            {
                return HttpNotFound();
            }
            return View(tsg048_grupos_mercados);
        }

        // POST: GruposMercados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tsg048_grupos_mercados tsg048_grupos_mercados = db.tsg048_grupos_mercados.Find(id);
            db.tsg048_grupos_mercados.Remove(tsg048_grupos_mercados);
            db.SaveChanges();
            //return new HttpStatusCodeResult(HttpStatusCode.OK,"Municipio Eliminado");
            return Json(new
            {
                success = 1                
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetGruposMercadosAll(int? id) {            
            string RequestBoby = Request.RequestContext.HttpContext.Request.Form[0];
            DtParameters bodytRequests = JsonConvert.DeserializeObject<DtParameters>(RequestBoby, jsonSerializerSettings);            
            IQueryable<GruposMercados> result = (from a in db.tsg048_grupos_mercados                                                
                         join b in db.tsg007_mercado on a.cd_mercado equals b.cd_mercado
                         join c in db.tsg038_municipios on a.cd_municipio equals c.cd_municipio
                         join d in db.tsg037_estados on a.cd_estado equals d.cd_estado
                         select new GruposMercados
                         {
                             cd_grupo = a.cd_grupo,
                             cd_mercado = (int)a.cd_mercado,
                             cd_estado = (int)a.cd_estado,
                             nb_mercado = b.nb_mercado,
                             nb_municipio = c.nb_municipio,
                             nb_estado = d.nb_estado
                         });


            result = result.Where(x => x.cd_mercado == id);

            if (bodytRequests.Search.Value != "")
            {
                result = result.Where( x => x.nb_municipio.Contains(bodytRequests.Search.Value) ||
                x.nb_mercado.Contains(bodytRequests.Search.Value));
            }

            var sortColumnIndex = Convert.ToInt32(bodytRequests.Order[0].Column);
            var sortDirection = bodytRequests.Order[0].Dir.ToString();
            if (sortColumnIndex == 1)
            {
                result = sortDirection == "Asc" ? result.OrderBy(c => c.nb_mercado) : result.OrderByDescending(c => c.nb_mercado);
            }
            else if (sortColumnIndex == 2)
            {
                result = sortDirection == "Asc" ? result.OrderBy(c => c.nb_estado) : result.OrderByDescending(c => c.nb_estado);
            }
            else if (sortColumnIndex == 3)
            {
                result = sortDirection == "Asc" ? result.OrderBy(c => c.nb_municipio) : result.OrderByDescending(c => c.nb_municipio);
            }
            else {
                result = sortDirection == "Asc" ? result.OrderBy(c => c.nb_mercado) : result.OrderByDescending(c => c.nb_mercado);                               
            }
            IEnumerable<GruposMercados> displayResult = result;

            if (bodytRequests.Length > 0)
            {
                displayResult = result.Skip(bodytRequests.Start)
                .Take(bodytRequests.Length).ToList();
            }

            var totalRecords = result.Count();

            //return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return Json(new           
            {                
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = displayResult
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetGruposListById(int? id)
        {
                        
            var result = (from a in db.tsg048_grupos_mercados
                                                 join b in db.tsg007_mercado on a.cd_mercado equals b.cd_mercado
                                                 join c in db.tsg038_municipios on a.cd_municipio equals c.cd_municipio
                                                 join d in db.tsg037_estados on a.cd_estado equals d.cd_estado
                                                 where a.cd_mercado == id
                                                 select new 
                                                 {
                                                     cd_grupo = a.cd_grupo,
                                                     cd_mercado = (int)a.cd_mercado,
                                                     cd_estado = (int)a.cd_estado,
                                                     Value = (int)a.cd_municipio,
                                                     nb_mercado = b.nb_mercado,
                                                     Text = c.nb_municipio,
                                                     nb_estado = d.nb_estado
                                                 });


            //result = result.Where(x => x.cd_mercado == id);                        
            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //var totalRecords = result.Count();
            //return Json(new
            //{
            //    iTotalRecords = totalRecords,
            //    iTotalDisplayRecords = totalRecords,
            //    aaData = displayResult
            //}, JsonRequestBehavior.AllowGet);
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
