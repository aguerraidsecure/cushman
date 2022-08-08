using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using wr_anit_cushman_one.Models;
using PagedList;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.Data.OData.Query.SemanticAst;
using wr_anit_cushman_one.Tags;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
using System.Xml;
using System.Xml.Linq;
using System.Threading;
using System.Globalization;
using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.text;
using PPTReport.clases;

namespace wr_anit_cushman_one.Controllers
{

    [Autenticado]
    public class Terrenos1Controller : Controller
    {

        private CushmanContext db = new CushmanContext();
        //List<UploadFilesResult> 
        List<UploadFilesResult> r;
        tsg029_bitacora_mov tsg029_bitacora_mov = new tsg029_bitacora_mov();

        Terreno terreno;

        public Terrenos1Controller()
        {

                                        terreno             = new Terreno();
            tsg001_terreno              tsg001_terreno      = new tsg001_terreno();
            tsg023_ni_precio            tsg023_te_precio    = new tsg023_ni_precio();
            tsg026_te_dt_gral           tsg026_te_dt_gral   = new tsg026_te_dt_gral();
            tsg027_te_servicio          tsg027_te_servicio  = new tsg027_te_servicio();
            tsg028_te_contacto          propietario         = new tsg028_te_contacto();
            tsg028_te_contacto          administrador       = new tsg028_te_contacto();
            tsg028_te_contacto          corredor            = new tsg028_te_contacto();
            tsg040_imagenes_terrenos    imagenes_Terrenos   = new tsg040_imagenes_terrenos();

            terreno.tsg001_terreno              = tsg001_terreno;
            terreno.tsg023_ni_precio            = tsg023_te_precio;
            terreno.tsg026_te_dt_gral           = tsg026_te_dt_gral;
            terreno.tsg027_te_servicio          = tsg027_te_servicio;
            terreno.tsg028_te_contacto_Prop     = propietario;
            terreno.tsg028_te_contacto_Adm      = administrador;
            terreno.tsg028_te_contacto_Corr     = corredor;
            terreno.tsg040_imagenes_terrenos    = imagenes_Terrenos;
            r = new List<UploadFilesResult>();
        }

        private IEnumerable<SelectItem> _makes = new List<SelectItem>
        {
            new SelectItem { id = 1, text = "Telmex" },
            new SelectItem { id = 2, text = "Alestra" },
            new SelectItem { id = 3, text = "MCM" },
            new SelectItem { id = 4, text = "Telecom" },
            new SelectItem { id = 5, text = "AT&T" }
        };

        [HttpGet]
        public IEnumerable<SelectItem> SearchMake(string id)
        {
            
            var query = _makes.Where(m => m.text.ToLower().Contains(id.ToLower()));

            return query;
        }

        [HttpGet]
        public IEnumerable<SelectItem> GetMake(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return null;

            var items = new List<SelectItem>();

            string[] idList = id.Split(new char[] { ',' });
            foreach (var idStr in idList)
            {
                int idInt;
                if (int.TryParse(idStr, out idInt))
                {
                    items.Add(_makes.FirstOrDefault(m => m.id == idInt));
                }
            }

            return items;
        }

        const string Municipio_KEY = "tsg038_municipios";

        
        public async Task<ActionResult> Index()
        {
            var tsi001_terreno_dts_gra = db.tsg001_terreno.Include(t => t.tsg023_ni_precio)
                .Include(t => t.tsg027_te_servicio)
                .Include(t => t.tsg028_te_contacto)
                .Include(t => t.tsg026_te_dt_gral);
            return View(await tsi001_terreno_dts_gra.ToListAsync());
        }

        public ActionResult Change(String strLenguanje)
        {
            ObtenerTipoCambio((decimal)(ViewBag.TipoCambio == null ? 0 : ViewBag.TipoCambio));
            if (strLenguanje != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(strLenguanje);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(strLenguanje);
            }
            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = strLenguanje;
            Response.Cookies.Add(cookie);

            return View("Crear", terreno);
        }

        public void ObtenerTipoCambio(decimal nu_tipo_cambio)
        {
            string valor = null;
            try
            {
                ws_banxico.DgieWSPortClient banxico = new ws_banxico.DgieWSPortClient();
                string respuesta_ws = banxico.tiposDeCambioBanxico();
                XmlDocument xdoc = new XmlDocument();
                XNamespace bm = "http://www.banxico.org.mx/structure/key_families/dgie/sie/series/compact";
                xdoc.LoadXml(respuesta_ws);
                //XmlNodeList xnList = xdoc.SelectNodes("CompactData/"+bm+"DataSet/"+bm+"Series");
                XmlNodeList info = xdoc.GetElementsByTagName("CompactData");
                XmlNodeList lista =
                      ((XmlElement)info[0]).GetElementsByTagName("bm:DataSet");

                XmlNodeList lista2 =
                ((XmlElement)info[0]).GetElementsByTagName("bm:Series");

                foreach (XmlElement nodo in lista2)
                {

                    XmlNodeList lista5 = nodo.GetElementsByTagName("bm:Obs");
                    ///lista5[0].Attributes.Item
                    valor = lista5[0].Attributes.GetNamedItem("OBS_VALUE").InnerText;
                    break;
                }
            }
            catch (Exception e)
            {
            }

            ViewBag.TipoCambio = (nu_tipo_cambio > 0 ? nu_tipo_cambio.ToString() : valor);
        }

        // GET: Terrenos1/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg001_terreno tsi001_terreno_dts_gra = await db.tsg001_terreno.FindAsync(id);
            if (tsi001_terreno_dts_gra == null)
            {
                return HttpNotFound();
            }
            return View(tsi001_terreno_dts_gra);
        }

        // GET: Terrenos1/Create
        public ActionResult Crear(int? id)

        {



            Terreno                     terreno                 = new Terreno();
            tsg001_terreno              tsg001_terreno          = new tsg001_terreno();
            tsg023_ni_precio            tsg023_te_precio        = new tsg023_ni_precio();
            tsg026_te_dt_gral           tsg026_te_dt_gral       = new tsg026_te_dt_gral();
            tsg027_te_servicio          tsg027_te_servicio      = new tsg027_te_servicio();
            tsg028_te_contacto          propietario             = new tsg028_te_contacto();
            tsg028_te_contacto          administrador           = new tsg028_te_contacto();
            tsg028_te_contacto          corredor                = new tsg028_te_contacto();
            tsg040_imagenes_terrenos    imagenes_Terrenos       = new tsg040_imagenes_terrenos();

            // tsg023_ni_precio tsg023_ni_precio = null;

            ViewBag.UsuarioActivo = true;
            ViewBag.disabled = true;
           
            if (id == null)
            {
                ViewBag.TpRegistro          = "Nuevo";
                ViewBag.cd_mercado          = new SelectList(db.tsg007_mercado, "cd_mercado", "nb_mercado");
                ViewBag.cd_corredor         = new SelectList(db.tsg008_corredor_ind, "cd_corredor", "nb_corredor");
                ViewBag.cd_st_entrega       = new SelectList(db.tsg017_st_entrega, "cd_st_entrega", "nb_st_entrega");
                ViewBag.cd_esp_ferr         = new SelectList(db.tsg022_esp_ferr, "cd_esp_ferr", "nb_esp_ferr");
                
                ObtenerTipoCambio((decimal)0);

                terreno.tsg001_terreno              = tsg001_terreno;
                terreno.tsg023_ni_precio            = tsg023_te_precio;
                terreno.tsg026_te_dt_gral           = tsg026_te_dt_gral;
                terreno.tsg027_te_servicio          = tsg027_te_servicio;
                terreno.tsg028_te_contacto_Prop     = propietario;
                terreno.tsg028_te_contacto_Adm      = administrador;
                terreno.tsg028_te_contacto_Corr     = corredor;
                terreno.tsg040_imagenes_terrenos    = imagenes_Terrenos;

                return View(terreno);
            }
            else
            {
                ViewBag.Folio = "T" + id;
                ViewBag.cd_tech = new SelectList(db.tsg041_tp_tech, "cd_tp_tech", "nb_tp_tech");
                ViewBag.TpRegistro = "Editar";
                tsg001_terreno = db.tsg001_terreno.Find(id);
                if (tsg001_terreno == null)
                {
                    return View();
                }
                int cd_general = db.tsg026_te_dt_gral.Where(x => x.cd_terreno == id).Max(x => x.cd_id);
                tsg026_te_dt_gral = db.tsg026_te_dt_gral.Find(cd_general);
                int cd_servicio = db.tsg027_te_servicio.Where(x => x.cd_terreno == id).Max(x => x.cd_servicio);
                tsg027_te_servicio = db.tsg027_te_servicio.Find(cd_servicio);
                int cd_precio = db.tsg023_ni_precio.Where(x => x.cd_terreno == id).DefaultIfEmpty().Max(x => x.cd_ni_precio == null ? 0 : x.cd_ni_precio);
                tsg023_te_precio = db.tsg023_ni_precio.Find(cd_precio);
                int cd_propietario = db.tsg028_te_contacto.Where(x => x.cd_terreno == id && x.st_contacto == 1).DefaultIfEmpty().Max(x => x.cd_contacto == null ? 0 :x.cd_contacto);
                propietario = db.tsg028_te_contacto.Find(cd_propietario);
                int cd_administrador = db.tsg028_te_contacto.Where(x => x.cd_terreno == id && x.st_contacto == 2).DefaultIfEmpty().Max(x => x.cd_contacto == null ? 0 :x.cd_contacto);
                administrador = db.tsg028_te_contacto.Find(cd_administrador);
                int cd_corredor = db.tsg028_te_contacto.Where(x => x.cd_terreno == id && x.st_contacto == 3).DefaultIfEmpty().Max(x => x.cd_contacto == null ? 0 :x.cd_contacto);
                corredor = db.tsg028_te_contacto.Find(cd_corredor);
                terreno.tsg001_terreno = tsg001_terreno;
                terreno.tsg026_te_dt_gral = tsg026_te_dt_gral;
                terreno.tsg027_te_servicio = tsg027_te_servicio;
                terreno.tsg023_ni_precio = tsg023_te_precio;
                terreno.tsg028_te_contacto_Prop = propietario;
                terreno.tsg028_te_contacto_Adm = administrador;
                terreno.tsg028_te_contacto_Corr = corredor;
                ViewBag.cd_mercado = new SelectList(db.tsg007_mercado, "cd_mercado", "nb_mercado", tsg001_terreno.cd_mercado);
                ViewBag.cd_corredor = new SelectList(db.tsg008_corredor_ind, "cd_corredor", "nb_corredor", tsg001_terreno.cd_corredor);
                ViewBag.cd_st_entrega = new SelectList(db.tsg017_st_entrega, "cd_st_entrega", "nb_st_entrega");
                ViewBag.cd_esp_ferr = new SelectList(db.tsg022_esp_ferr, "cd_esp_ferr", "nb_esp_ferr");

                if (terreno.tsg023_ni_precio != null)
                {
                    if (terreno.tsg023_ni_precio.nu_tipo_cambio != null)
                    {
                        ObtenerTipoCambio((decimal)terreno.tsg023_ni_precio.nu_tipo_cambio);
                    }
                }
            }
            // @ViewBag.accion = 0;
            return View(terreno);
        }

        public ActionResult Buscar(String id)
        {
            //var model = GetFooModel();
            if (Request.IsAjaxRequest())
            {
                //return PartialView("UserDetails", model);
                return PartialView();
            }
            //return View(model);
            return View();
        }

        public virtual ActionResult Test()
        {
            var m = new Terreno();
            return View(m);
        }

        [HttpPost]
        ////[ValidateAntiForgeryToken]
        ////[AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Crear(Terreno Terreno, int? id)
        {
            var respuesta = new ResponseModel
            {
                respuesta = true,
                redirect = "/",
                error = ""
            };

            if (ModelState.IsValid)
            {
                //var tp_producto = this.Request.Form["opciones"];
                if (id == null) //Nuevo para guardar
                    guardarTerrenos(Terreno);
                else // no es nuevo, hay que  guardar(editar), o 
                {
                    if (Terreno.tsg001_terreno.cd_terreno == 0)
                    {
                        guardarTerrenos(Terreno);
                    }
                    else
                    {
                        actualizaTerrenos(Terreno, id);
                    }
                  
                }
            }
            else
            {
                var message = string.Join(" |\n ", ModelState.Values
                 .SelectMany(v => v.Errors)
                 .Select(e => e.ErrorMessage));
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest, message);
                //ViewBag.Error = message;
                respuesta.respuesta = false;
                respuesta.error = message;
                return Json(respuesta);
            }
            respuesta.redirect = Terreno.tsg001_terreno.cd_terreno.ToString();
            return Json(respuesta);
        }

        public JsonResult BuscaCP(string id)
        {
            var localizacion = db.tsg039_colonias.Where(i => i.nu_cp.Contains(id)).Select("new (cd_colonia, cd_municipio, cd_estado)");

            return new JsonResult { Data = localizacion.ToListAsync(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult ObtenerCP(int id_estado, int id_municipio, int id_colonia)
        {
            var localizacion = db.tsg039_colonias.Where(i => i.cd_estado == id_estado && i.cd_municipio == id_municipio && i.cd_colonia == id_colonia).Select("new(nu_cp)");
            return new JsonResult { Data = localizacion, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }

        public void EliminaTerrenos(int? id)
        {
            try
            {
                var t6 = db.tsg040_imagenes_terrenos.First(i => i.cd_terreno == id);
                if (t6 != null)
                {
                    db.tsg040_imagenes_terrenos.Remove(t6);
                    db.SaveChanges();
                }
                var t5 = db.tsg028_te_contacto.First(i => i.cd_terreno == id && i.st_contacto == 3);
                if (t5 != null)
                {
                    db.tsg028_te_contacto.Remove(t5);
                    db.SaveChanges();
                }

                var t4 = db.tsg028_te_contacto.First(i => i.cd_terreno == id && i.st_contacto == 2);
                if (t4 != null)
                {
                    db.tsg028_te_contacto.Remove(t4);
                    db.SaveChanges();
                }

                var t3 = db.tsg028_te_contacto.First(i => i.cd_terreno == id && i.st_contacto == 1);
                if (t3 != null)
                {
                    db.tsg028_te_contacto.Remove(t3);
                    db.SaveChanges();
                }

                var t2 = db.tsg026_te_dt_gral.First(i => i.cd_terreno == id);
                if (t2 != null)
                {
                    db.tsg026_te_dt_gral.Remove(t2);
                    db.SaveChanges();
                }

                var t1 = db.tsg027_te_servicio.First(i => i.cd_terreno == id);
                if (t1 != null)
                {
                    db.tsg027_te_servicio.Remove(t1);
                    db.SaveChanges();
                }

                var t0 = db.tsg023_ni_precio.First(i => i.cd_terreno == id);
                if (t0 != null)
                {
                    db.tsg023_ni_precio.Remove(t0);
                    db.SaveChanges();
                }

                var t = db.tsg001_terreno.First(i => i.cd_terreno == id);
                if (t != null)
                {
                    db.tsg001_terreno.Remove(t);
                    db.SaveChanges();
                }
                tsg029_bitacora_mov.GuardaBitacora("tsg001_terreno", "cd_terreno", t.cd_terreno, "Eliminar");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
            }
        }


        private void actualizaTerrenos(Terreno terreno, int? id)
        {
            try
            {
                terreno.tsg001_terreno.fh_modif = DateTime.Now;
                terreno.tsg027_te_servicio.fh_modif = DateTime.Now;
                terreno.tsg028_te_contacto_Adm.fh_modif = DateTime.Now;
                terreno.tsg028_te_contacto_Corr.fh_modif = DateTime.Now;
                terreno.tsg028_te_contacto_Prop.fh_modif = DateTime.Now;
                terreno.tsg023_ni_precio.fh_modif = DateTime.Now;

                //tsg001_terreno t = db.tsg001_terreno.First(i => i.cd_terreno == id);
                var t = db.tsg001_terreno.Find(id);
                if (t != null)
                {
                    terreno.tsg001_terreno.cd_terreno = t.cd_terreno; // seteo el mismo numero de terreno
                    db.Entry(t).CurrentValues.SetValues(terreno.tsg001_terreno);
                    db.SaveChanges();

                    var t0 = db.tsg023_ni_precio.First(i => i.cd_terreno == id);
                    if (t0 != null)
                    {
                        terreno.tsg023_ni_precio.cd_ni_precio = t0.cd_ni_precio;
                        terreno.tsg023_ni_precio.cd_terreno = id;
                        
                        db.Entry(t0).CurrentValues.SetValues(terreno.tsg023_ni_precio);
                        db.SaveChanges();
                    }

                    var t1 = db.tsg027_te_servicio.First(i => i.cd_terreno == id);
                    if (t1 != null)
                    {
                        terreno.tsg027_te_servicio.cd_servicio = t1.cd_servicio;
                        terreno.tsg027_te_servicio.cd_terreno = t1.cd_terreno;
                        
                        db.Entry(t1).CurrentValues.SetValues(terreno.tsg027_te_servicio);
                        db.SaveChanges();
                    }

                    var t2 = db.tsg026_te_dt_gral.First(i => i.cd_terreno == id);
                    if (t2 != null)
                    {
                        terreno.tsg026_te_dt_gral.cd_id = t2.cd_id;
                        terreno.tsg026_te_dt_gral.cd_terreno = t2.cd_terreno;
                        db.Entry(t2).CurrentValues.SetValues(terreno.tsg026_te_dt_gral);
                        db.SaveChanges();
                    }

                    var t3 = db.tsg028_te_contacto.First(i => i.cd_terreno == id && i.st_contacto==1);
                    if (t3 != null)
                    {
                        terreno.tsg028_te_contacto_Prop.cd_contacto = t3.cd_contacto;
                        terreno.tsg028_te_contacto_Prop.cd_terreno = t3.cd_terreno;
                        terreno.tsg028_te_contacto_Prop.st_contacto = t3.st_contacto;
                        db.Entry(t3).CurrentValues.SetValues(terreno.tsg028_te_contacto_Prop);
                        db.SaveChanges();
                    }

                    var t4 = db.tsg028_te_contacto.First(i => i.cd_terreno == id && i.st_contacto == 2);
                    if (t4 != null)
                    {
                        terreno.tsg028_te_contacto_Adm.cd_contacto = t4.cd_contacto;
                        terreno.tsg028_te_contacto_Adm.cd_terreno = t4.cd_terreno;
                        terreno.tsg028_te_contacto_Adm.st_contacto = t4.st_contacto;
                        db.Entry(t4).CurrentValues.SetValues(terreno.tsg028_te_contacto_Adm);
                        db.SaveChanges();
                    }

                    var t5 = db.tsg028_te_contacto.First(i => i.cd_terreno == id && i.st_contacto == 3);
                    if (t5 != null)
                    {
                        terreno.tsg028_te_contacto_Corr.cd_contacto = t5.cd_contacto;
                        terreno.tsg028_te_contacto_Corr.cd_terreno = t5.cd_terreno;
                        terreno.tsg028_te_contacto_Corr.st_contacto = t5.st_contacto;
                        db.Entry(t5).CurrentValues.SetValues(terreno.tsg028_te_contacto_Corr);
                        db.SaveChanges();
                    }

                }
                //t.nb_comercial = terreno.tsg001_terreno.nb_comercial;
                //db.Entry(t).CurrentValues.SetValues(terreno.tsg001_terreno);                
                //db.SaveChanges();

            }
            catch(Exception e)
            {
                Console.WriteLine(e.InnerException);
            }
        }


        private void guardarTerrenos(Terreno terreno)
        {

            terreno.tsg001_terreno.fh_modif = DateTime.Now;
            terreno.tsg027_te_servicio.fh_modif = DateTime.Now;
            terreno.tsg028_te_contacto_Adm.fh_modif = DateTime.Now;
            terreno.tsg028_te_contacto_Corr.fh_modif = DateTime.Now;
            terreno.tsg028_te_contacto_Prop.fh_modif = DateTime.Now;
            terreno.tsg023_ni_precio.fh_modif = DateTime.Now;


            // terreno.tsg027_te_servicio.cd_terreno = terreno.tsg001_terreno.cd_terreno;                
            db.tsg001_terreno.Add(terreno.tsg001_terreno);
            db.SaveChanges();

            int idValor = db.tsg001_terreno.Max(x => x.cd_terreno);
            terreno.tsg027_te_servicio.cd_terreno = idValor;
            db.tsg027_te_servicio.Add(terreno.tsg027_te_servicio);
            db.SaveChanges();

            terreno.tsg026_te_dt_gral.cd_terreno = idValor;
            terreno.tsg026_te_dt_gral.fh_modif = DateTime.Now;
            db.tsg026_te_dt_gral.Add(terreno.tsg026_te_dt_gral);
            db.SaveChanges();

            terreno.tsg023_ni_precio.cd_terreno = idValor;
            db.tsg023_ni_precio.Add(terreno.tsg023_ni_precio);
            db.SaveChanges();

            terreno.tsg028_te_contacto_Prop.cd_terreno = idValor;
            terreno.tsg028_te_contacto_Prop.st_contacto = 1;
            db.tsg028_te_contacto.Add(terreno.tsg028_te_contacto_Prop);
            db.SaveChanges();

            terreno.tsg028_te_contacto_Adm.cd_terreno = idValor;
            terreno.tsg028_te_contacto_Adm.st_contacto = 2;
            db.tsg028_te_contacto.Add(terreno.tsg028_te_contacto_Adm);
            db.SaveChanges();

            terreno.tsg028_te_contacto_Corr.cd_terreno = idValor;
            terreno.tsg028_te_contacto_Corr.st_contacto = 3;
            db.tsg028_te_contacto.Add(terreno.tsg028_te_contacto_Corr);
            db.SaveChanges();
            tsg029_bitacora_mov.GuardaBitacora("tsg028_te_contacto", "cd_terreno", idValor, "Crear");
            // return RedirectToAction("crear");
        }


        class RoundedBorder : IPdfPCellEvent
        {
            public void CellLayout(PdfPCell cell, Rectangle rect, PdfContentByte[] canvas)
            {
                PdfContentByte cb = canvas[PdfPTable.BACKGROUNDCANVAS];
                cb.RoundRectangle(
                  rect.Left + 1.5f,
                  rect.Bottom + 1.5f,
                  rect.Width - 3,
                  rect.Height - 3, 4
                );
                cb.Stroke();
            }
        }
        private static void uploadfileppt(string ruta)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            /// CloudBlobContainer container = blobClient.GetContainerReference("cushmandocs");
            CloudBlobContainer container = blobClient.GetContainerReference("cushmandocs");

            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("terrenos_rpt.pptx");

            // Create or overwrite the "myblob" blob with contents from a local file.
            using (var fileStream = System.IO.File.OpenRead(ruta))
            {
                blockBlob.UploadFromStream(fileStream);
            }
        }
        public ActionResult GeneraPpt(int? id, int lng)
        {
            UploadFilesResult r1 = new UploadFilesResult();
            if (id == null)
            {
                r1.Name = "";
                r1.Type = "";
                r1.Length = 0;
                return new JsonResult
                {
                    Data = r1,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            string ruta = System.Web.HttpContext.Current.Server.MapPath("~/repositorio/");
            descargaImagen(id, ruta);
            PPTReport.clases.reporte_pp.repPpTerr_ind(id, lng, ruta);
            // reportes.

            uploadfileppt(Server.MapPath("~/repositorio/" + "terrenos_rpt.pptx"));

            string strUrl = descargaBlob2("terrenos_rpt.pptx");
            r1.Name = strUrl;
            r1.Type = "";
            r1.Length = 0;

            return new JsonResult
            {
                Data = r1,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        private void descargaImagen(int? id_nave, String ruta)
        {
            utils util = new utils();
            var imagenes = db.tsg040_imagenes_terrenos.Where(t => t.cd_terreno == id_nave && t.cd_reporte == 1);
            string urlImage = descargaBlob(id_nave, imagenes.First().nb_archivo);
            string rutaArchivo = util.GeneraImagen(urlImage, "T_" + id_nave, ruta);
        }
        public ActionResult GeneraPdf(int? id, int lng)
        {
            // Se valida que venga un id
            if (id == null)
            {
                UploadFilesResult r1 = new UploadFilesResult();
                r1.Name = "";
                r1.Type = "";
                r1.Length = 0;
                return new JsonResult
                {
                    Data = r1,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

            //HttpCookie cookie = new HttpCookie("Language");
            //cookie.Value = strLenguanje;
            //Response.Cookies.Add(cookie);
            //cookie = Response.Cookies["us"];
            //var leng = cookie.Value;
            BaseFont Vn_Helvetica = BaseFont.CreateFont(Server.MapPath("~/fonts/" + "arial.ttf"),"Identity-H", BaseFont.EMBEDDED);

            var imagenes = db.tsg040_imagenes_terrenos.Where(i => i.cd_terreno == id && i.cd_reporte == 1);
            string urlImage = "";
            if (imagenes.Count() != 0) 
            {
                //if (imagenes.First().tx_archivo.ToString().Contains("image"))
                //{
                //    urlImage = imagenes.First().nb_archivo;
                //}
                //else
                //{ 
                //    urlImage = descargaBlob(imagenes.First().nb_archivo);
                //}
                urlImage = descargaBlob(id, imagenes.First().nb_archivo);
            }
            Document documento = new Document(iTextSharp.text.PageSize.LETTER.Rotate(), 20, 20, 30, 35);

            if (lng == 1) { // Español                    
                PdfWriter writer = PdfWriter.GetInstance(documento, new FileStream(Server.MapPath("~/image/" + "Terreno_rpt.pdf"), FileMode.Create));
                //PdfWriter.GetInstance(documento, new FileStream(@"D:\archivo.pdf",FileMode.Create));            
                var t = db.tsg001_terreno.Find(id);
                if (t != null)
                {
                    try
                    {
                        documento.Open();

                        var t0 = db.tsg023_ni_precio.First(i => i.cd_terreno == id);
                        if (t0 == null)
                        {
                        }

                        var t1 = db.tsg027_te_servicio.First(i => i.cd_terreno == id);
                        if (t1 == null)
                        {
                        }

                        var t2 = db.tsg026_te_dt_gral.First(i => i.cd_terreno == id);
                        if (t2 == null)
                        {
                        }


                        Double tp_cmb = 1;
                        var moneda = ""; // por dafault pesos
                        var operador = "*";
                        if (t0.cd_moneda != t0.cd_rep_moneda)
                        {
                            if (t0.cd_moneda != 0) // Se verifica moneda en que fué guardado el registro
                            {
                                var mon = db.tsg044_tipos_monedas.Where(i => i.cd_moneda == t0.cd_moneda).First();
                                if (mon.nb_moneda == "USD") // Dolares
                                {
                                    operador = "*";
                                    if (t0.nu_tipo_cambio != null && t0.nu_tipo_cambio != 0)
                                    {
                                        tp_cmb = (double)t0.nu_tipo_cambio;
                                    }
                                }
                                else
                                {
                                    if (mon.nb_moneda == "MXN") // Pesos
                                    {
                                        operador = "/";
                                        if (t0.nu_tipo_cambio != null && t0.nu_tipo_cambio != 0)
                                        {
                                            tp_cmb = (double)t0.nu_tipo_cambio;
                                        }
                                    }
                                }
                            }
                        }

                        if (t0.cd_rep_moneda != 0)
                        {
                            var mon = db.tsg044_tipos_monedas.Where(i => i.cd_moneda == t0.cd_rep_moneda).First();
                            if (mon.nb_moneda == "USD") // Dolares
                            {
                                moneda = "USD";
                            }
                            else
                            {
                                moneda = "MXN";
                            }
                        }

                        Double preciofinal = 0;
                        Double rentafinal = 0;
                        double p = 0;
                        if (t0.im_venta != null)
                        {
                            p = (Double)t0.im_venta;
                        }
                        if (operador == "*")
                        {
                            preciofinal = Math.Round(p * tp_cmb, 2);
                        }
                        else
                        {
                            preciofinal = Math.Round(p / tp_cmb, 2);
                        }
                        p = 0;
                        if (t0.im_renta != null)
                        {
                            p = (Double)t0.im_renta;
                        }
                        if (operador == "*")
                        {
                            rentafinal = Math.Round(p * tp_cmb, 2);
                        }
                        else
                        {
                            rentafinal = Math.Round(p / tp_cmb, 2);
                        }


                        PdfPTable table = new PdfPTable(6);
                        table.WidthPercentage = 100;

                        //PdfPCell cell = new PdfPCell(new Phrase("Terreno: " + t.nb_comercial + "\n", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE)));
                        PdfPCell cell = new PdfPCell(new Phrase(t.nb_comercial + "\n", new iTextSharp.text.Font(Vn_Helvetica, 14f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE)));
                        cell.BackgroundColor = new iTextSharp.text.BaseColor(0, 51, 102);
                        cell.Colspan = 6;
                        cell.HorizontalAlignment = 0; // 0 izq, 1 centro, 2 der
                        table.AddCell(cell);

                        // Obtener colonia
                        var col = "";
                        if (t.cd_colonia != null)
                        {
                            var t4 = db.tsg039_colonias.First(i => i.cd_colonia == t.cd_colonia);
                            if (t4 != null)
                            {
                                col = t4.nb_colonia;
                            }
                        }
                        // Obtener colonia
                        var mpio = "";
                        if (t.cd_municipio != null)
                        {
                            var t5 = db.tsg038_municipios.First(i => i.cd_municipio == t.cd_municipio);
                            if (t5 != null)
                            {
                                mpio = t5.nb_municipio;
                            }
                        }
                        PdfPCell cell1 = new PdfPCell(new Phrase("" + t.nb_calle + " No. " + t.nu_direcion + "\n" + col + "\n" + mpio + "\n\n", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                        cell1.Colspan = 2;
                        cell1.HorizontalAlignment = 0; // 0 izq, 1 centro, 2 der
                        cell1.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell1);
                        PdfPCell cell29 = new PdfPCell(new Phrase("\nPlano de ubicación / Location Map\n\n", new iTextSharp.text.Font(Vn_Helvetica, 10f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                        cell29.Colspan = 2;
                        cell1.HorizontalAlignment = 1; // 0 izq, 1 centro, 2 der
                        cell29.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell29);
                        PdfPCell cell30 = new PdfPCell(new Phrase("\nComentarios / Comments\n\n", new iTextSharp.text.Font(Vn_Helvetica, 10f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                        cell30.Colspan = 2;
                        cell30.HorizontalAlignment = 1; // 0 izq, 1 centro, 2 der
                        cell30.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell30);

                        iTextSharp.text.Image mapa;
                        try
                        {
                            //imagenes
                            if (urlImage != "")
                            {
                                //imagenes
                                //iTextSharp.text.Image mapa = iTextSharp.text.Image.GetInstance(Server.MapPath("~/image/" + "logo_CW.png"));
                                mapa = iTextSharp.text.Image.GetInstance(urlImage);
                                mapa.ScaleToFit(247f, 260f);
                                //table.AddCell(mapa); //documento.Add(mapa);
                                PdfPCell cell400 = new PdfPCell(mapa);
                                cell400.HorizontalAlignment = 1;
                                cell400.VerticalAlignment = 1;
                                cell400.Colspan = 2;
                                cell400.Padding = 3;
                                table.AddCell(cell400);
                            }
                            else
                            {
                                mapa = iTextSharp.text.Image.GetInstance(Server.MapPath("~/image/" + "nb.png"));
                                mapa.ScaleToFit(247f, 260f);
                                PdfPCell cell400 = new PdfPCell(mapa);
                                cell400.HorizontalAlignment = 1;
                                cell400.VerticalAlignment = 1;
                                cell400.Colspan = 2;
                                cell400.Padding = 3;
                                table.AddCell(cell400);//documento.Add(mapa);

                            }
                        }
                        catch (Exception e)
                        {
                            mapa = iTextSharp.text.Image.GetInstance(Server.MapPath("~/image/" + "nb.png"));
                            mapa.ScaleToFit(247f, 260f);
                            PdfPCell cell400 = new PdfPCell(mapa);
                            cell400.HorizontalAlignment = 1;
                            cell400.VerticalAlignment = 1;
                            cell400.Colspan = 2;
                            cell400.Padding = 3;
                            table.AddCell(cell400);
                        }


                        if (t.nb_posicion != null || t.nb_poligono != null)
                        {
                            //table.AddCell("Informaciòn de la propiedad");                    
                            iTextSharp.text.Image mapa1 = iTextSharp.text.Image.GetInstance("https://maps.googleapis.com/maps/api/staticmap?center=" + t.nb_posicion + "&zoom=13&size=640x500&maptype=hybrid&path=fillcolor:red|" + (t.nb_poligono != null ? t.nb_poligono.TrimEnd('|') : "") + "&key=AIzaSyDhfzatgC_64higR2LlLGkWldmTBudiRWY");
                            mapa1.ScaleToFit(247f, 260f);
                            PdfPCell cell401 = new PdfPCell(mapa1);
                            cell401.HorizontalAlignment = 1;
                            cell401.VerticalAlignment = 1;
                            cell401.Colspan = 2;
                            cell401.Padding = 3;
                            table.AddCell(cell401);
                        }
                        else
                        {
                            iTextSharp.text.Image mapa1 = iTextSharp.text.Image.GetInstance(Server.MapPath("~/image/" + "nb.png"));
                            mapa1.ScaleToFit(247f, 260f);
                            PdfPCell cell401 = new PdfPCell(mapa);
                            cell401.HorizontalAlignment = 1;
                            cell401.VerticalAlignment = 1;
                            cell401.Colspan = 2;
                            cell401.Padding = 3;
                            table.AddCell(cell401);
                        }

                    

                        //table.AddCell("Terreno de " + t2.nu_tam_max + " m2 con servicios disponibles");
                        PdfPCell cell32 = new PdfPCell(new Phrase(t.nb_comentarios, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell32.Colspan = 2;
                        table.AddCell(cell32);


                        PdfPCell cell1000 = new PdfPCell(new Phrase("               ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                        cell1000.UseVariableBorders = true;
                        cell1000.Colspan = 6;
                        cell1000.BorderColorLeft = BaseColor.WHITE;
                        cell1000.BorderColorRight = BaseColor.WHITE;
                        cell1000.BorderColorBottom = BaseColor.WHITE;
                        table.AddCell(cell1000);

                        PdfPCell cell2 = new PdfPCell(new Phrase("Información de la propiedad / Property Information", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                        cell2.Colspan = 4;
                        cell2.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        cell2.BorderColorTop = new iTextSharp.text.BaseColor(1, 1, 1);
                        table.AddCell(cell2);

                        PdfPCell cell40 = new PdfPCell(new Phrase(" Notas Recorrido / Tour Notes:", new iTextSharp.text.Font(Vn_Helvetica, 10f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell40.Rowspan = 2;
                        cell40.Colspan = 2;
                        cell40.Border = PdfPCell.NO_BORDER;
                        cell40.CellEvent = new RoundedBorder();
                        table.AddCell(cell40);

                        if (t.cd_corredor != null)
                        {
                            var t3 = db.tsg008_corredor_ind.First(i => i.cd_corredor == t.cd_corredor);
                            if (t3 != null)
                            {
                                PdfPCell cell3 = new PdfPCell(new Phrase("Corredor / Market ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                                cell3.Colspan = 1;
                                cell3.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                                table.AddCell(cell3);
                                PdfPCell cell300 = new PdfPCell(new Phrase(": " + t3.nb_corredor, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                                cell300.Colspan = 1;
                                cell300.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                                table.AddCell(cell300);
                            }
                            else
                            {
                                PdfPCell cell3 = new PdfPCell(new Phrase("Corredor / Market ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                                cell3.Colspan = 1;
                                cell3.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                                table.AddCell(cell3);
                                PdfPCell cell300 = new PdfPCell(new Phrase(": ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                                cell300.Colspan = 1;
                                cell300.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                                table.AddCell(cell300);
                            }
                        }
                        else
                        {
                            PdfPCell cell3 = new PdfPCell(new Phrase("Corredor / Market ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                            cell3.Colspan = 1;
                            cell3.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                            table.AddCell(cell3);
                            PdfPCell cell300 = new PdfPCell(new Phrase(": ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                            cell300.Colspan = 1;
                            cell300.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                            table.AddCell(cell300);
                        }

                        PdfPCell cell23 = new PdfPCell(new Phrase("Agua Potable / Drinking Water ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell23.Colspan = 1;
                        cell23.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell23);

                        PdfPCell cell24 = new PdfPCell(new Phrase(": " + (!t1.st_aguapozo ? "Servicio Municipal - Municipal Service" : "Pozo – Water well"), new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell24.Colspan = 1;
                        cell24.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell24);

                        //PdfPCell cell25 = new PdfPCell(new Phrase("", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        //cell25.Colspan = 2;
                        //table.AddCell(cell25);


                        PdfPCell cell26 = new PdfPCell(new Phrase("Tamaño mínimo de lote / Min. Lot Size ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell26.Colspan = 1;
                        cell26.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell26);

                        double nu_tam_min = Convert.ToDouble(t2.nu_tam_min);
                        double nu_tam_minF = Convert.ToDouble(t2.nu_tam_min) * 10.7639;

                        PdfPCell cell27 = new PdfPCell(new Phrase(": " + nu_tam_min.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")) + " M2 / " + nu_tam_minF.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")) + " Ft2", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell27.Colspan = 1;
                        cell27.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell27);

                        if (t1.cd_telefonia != null)
                        {
                            var t4 = db.tsg043_telefonia.First(i => i.cd_telefonia == t1.cd_telefonia);
                            if (t4 != null)
                            {
                                PdfPCell cell28 = new PdfPCell(new Phrase("Telefonía / Telephone Service", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                                cell28.Colspan = 1;
                                cell28.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                                table.AddCell(cell28);

                                PdfPCell cell280 = new PdfPCell(new Phrase(": " + t4.nb_telefonia, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                                cell280.Colspan = 1;
                                cell280.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                                table.AddCell(cell280);
                            }
                            else
                            {
                                PdfPCell cell28 = new PdfPCell(new Phrase("Telefonía / Telephone service", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                                cell28.Colspan = 1;
                                cell28.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                                table.AddCell(cell28);
                                PdfPCell cell280 = new PdfPCell(new Phrase(": ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                                cell280.Colspan = 1;
                                cell280.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                                table.AddCell(cell280);
                            }
                        }
                        else
                        {
                            PdfPCell cell28 = new PdfPCell(new Phrase("Telefonía / Telephone service ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                            cell28.Colspan = 1;
                            cell28.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                            table.AddCell(cell28);
                            PdfPCell cell280 = new PdfPCell(new Phrase(": ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                            cell280.Colspan = 1;
                            cell280.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                            table.AddCell(cell280);
                        }

                        PdfPCell cell31 = new PdfPCell(new Phrase("", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell31.Colspan = 2;
                        table.AddCell(cell31);

                        PdfPCell cell33 = new PdfPCell(new Phrase("Tamaño máximo de lote / Max. Lot Size ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell33.Colspan = 1;
                        cell33.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell33);

                        double nu_tam_max = Convert.ToDouble(t2.nu_tam_max);
                        double nu_tam_maxF = Convert.ToDouble(t2.nu_tam_max) * 10.7639;

                        PdfPCell cell34 = new PdfPCell(new Phrase(": " + nu_tam_max.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")) + " M2 / " + nu_tam_maxF.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")) + " Ft2", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell34.Colspan = 1;
                        cell34.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell34);

                        col = "";
                        if (t1.cd_tp_gas_natural != null)
                        {
                            var t4 = db.tsg021_tp_gas_natural.First(i => i.cd_tp_gas_natural == t1.cd_tp_gas_natural);
                            if (t4 != null)
                            {
                                col = t4.nb_tp_gas_natural;
                                if (col == "Otro / Other")
                                {
                                    col = t1.nb_tp_gas_natural;
                                }
                            }
                        }
                        
                        PdfPCell cell35 = new PdfPCell(new Phrase("Gas Natural / Natural Gas ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell35.Colspan = 1;
                        cell35.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell35);

                        
                        PdfPCell cell36 = new PdfPCell(new Phrase(": " + col, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell36.Colspan = 1;
                        cell36.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell36);

                        PdfPCell cell37 = new PdfPCell(new Phrase("", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell37.Colspan = 2;
                        table.AddCell(cell37);

                        
                        PdfPCell cell7 = new PdfPCell(new Phrase("Disponibilidad Total / Total Availability ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell7.Colspan = 1;
                        cell7.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell7);


                        double nu_disponiblidad = Convert.ToDouble(t2.nu_disponiblidad);
                        double nu_disponiblidadF = Convert.ToDouble(t2.nu_disponiblidad) * 10.7639;

                        PdfPCell cell70 = new PdfPCell(new Phrase(": " + nu_disponiblidad.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")) + " M2 / " + nu_disponiblidadF.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")) + " Ft2", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell70.Colspan = 1;
                        cell70.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell70);

                        PdfPCell cell38 = new PdfPCell(new Phrase("Espuela de Ferrocarril / Railroad Track ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell38.Colspan = 1;
                        cell38.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell38);
                        
                        col = "";
                        if (t1.cd_esp_fer != null)
                        {
                            var t4 = db.tsg022_esp_ferr.First(i => i.cd_esp_ferr == t1.cd_esp_fer);
                            if (t4 != null)
                            {
                                col = t4.nb_esp_ferr;
                            }
                        }
                        PdfPCell cell39 = new PdfPCell(new Phrase(": " + col, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell39.Colspan = 1;
                        cell39.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell39);

                        PdfPCell cell41 = new PdfPCell(new Phrase("", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell41.Colspan = 2;
                        table.AddCell(cell41);


                        //Cobertura maxima permitida
                        PdfPCell cell42 = new PdfPCell(new Phrase("Cobertura máxima permitida / Max. Allowed Coverage ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell42.Colspan = 1;
                        cell42.Rowspan = 7;
                        cell42.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell42);
                        
                        PdfPCell cell43 = new PdfPCell(new Phrase(": " + t2.nb_radio_cob, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell43.Colspan = 1;
                        cell43.Rowspan = 7;
                        cell43.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell43);


                        PdfPCell cell44 = new PdfPCell(new Phrase("Estatus de Entrega / Delivery status ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell44.Colspan = 1;
                        cell44.Rowspan = 7;
                        cell44.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell44);

                        col = "";
                        if (t2.cd_st_entrega != null)
                        {
                            var t4 = db.tsg017_st_entrega.First(i => i.cd_st_entrega == t2.cd_st_entrega);
                            if (t4 != null)
                            {
                                col = t4.nb_st_entrega;                                
                            }
                        }
                        PdfPCell cell45 = new PdfPCell(new Phrase(": " + col, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell45.Colspan = 1;
                        cell45.Rowspan = 7;
                        cell45.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell45);

                        PdfPCell cell46 = new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell46.Colspan = 2;
                        table.AddCell(cell46);

                        PdfPCell cell47 = new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell47.Colspan = 2;
                        table.AddCell(cell47);

                        PdfPCell cell48 = new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell48.Colspan = 2;
                        table.AddCell(cell48);

                        PdfPCell cell49 = new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell49.Colspan = 2;
                        table.AddCell(cell49);

                        PdfPTable table1 = new PdfPTable(2);
                        PdfPCell cell50 = new PdfPCell(new Phrase("Precio de Renta / Rent Price \nPrecio de Venta/Sales Price ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell50.Rowspan = 3;                        
                        cell50.Border = PdfPCell.NO_BORDER;
                        //cell50.CellEvent = new RoundedBorder();                       
                        table1.AddCell(cell50);

                        PdfPCell cell51 = new PdfPCell(new Phrase(": " + rentafinal.ToString("C2", CultureInfo.CreateSpecificCulture("en-us")) + " " + moneda + " / SqMt / Month" + " \n: " + preciofinal.ToString("C2", CultureInfo.CreateSpecificCulture("en-us")) + " "+  moneda + " / SqMt", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell51.Rowspan = 3;
                        cell51.Border = PdfPCell.NO_BORDER;
                        //cell50.CellEvent = new RoundedBorder();                        
                        table1.AddCell(cell51);

                        PdfPCell cell311 = new PdfPCell(table1);
                        cell311.Border = PdfPCell.NO_BORDER;
                        cell311.Colspan = 2;
                        cell311.Padding = 3;
                        cell311.CellEvent = new RoundedBorder();
                        table.AddCell(cell311);
                        
                        documento.Add(table);

                        //Paragraph p2 = new Paragraph("\n\n", new iTextSharp.text.Font(Vn_Helvetica, 6f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK));
                        //documento.Add(p2);
                        ////'DIBUJANDO LINEAS
                        //Paragraph p1 = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                        //documento.Add(p1);

                        //Paragraph p100 = new Paragraph("\n\nThis report contains information available to the public and has been relied upon by Cushman & Wakefield on the basis that it is accurate and complete. Cushman & Wakefield accepts no responsibility if this should prove not to be the case. No warranty or representation, express or implied, is made to the accuracy or completeness of the information contained herein, and same is submitted subject to errors, omissions, change of price, rental or other conditions, withdrawal without notice, and to any special listing conditions imposed by our principals.", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 6f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK));                    
                        //documento.Add(p100);

                        PdfContentByte cb = writer.DirectContent;
                        cb.BeginText();
                        BaseFont f_cn = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                        cb.SetFontAndSize(f_cn, 10);
                        cb.SetColorFill(new BaseColor(1, 1, 1));
                        cb.SetTextMatrix(20, 55);  //(xPos, yPos)
                        cb.ShowText("________________________________________________________________________________________________________________________________________");

                        cb.SetTextMatrix(20, 35);  //(xPos, yPos)     
                        BaseFont f_cn1 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                        cb.SetFontAndSize(f_cn1, 7);
                        cb.ShowText("This report contains information available to the public and has been relied upon by Cushman & Wakefield on the basis that it is accurate and complete. Cushman & Wakefield accepts no responsibility if this should prove not to be");
                        cb.SetTextMatrix(20, 28);  //(xPos, yPos)
                        cb.ShowText("the case. No warranty or representation, express or implied, is made to the accuracy or completeness of the information contained herein, and same is submitted subject to errors, omissions, change of price, rental or other");
                        cb.SetTextMatrix(20, 20);  //(xPos, yPos)
                        cb.ShowText("conditions, withdrawal without notice, and to any special listing conditions imposed by our principals.");
                        cb.EndText();

                        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/image/" + "logo_CW.png"));
                        //doc.Add(Image.GetInstance(Server.MapPath("~/image/" + "portadas_naves.png")));
                        logo.ScalePercent(50f);
                        logo.SetAbsolutePosition(documento.PageSize.Width - 120f, documento.PageSize.Height - 10f - 600f);
                        documento.Add(logo);

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.InnerException);
                    }
                    finally
                    {                    
                        documento.Close();
                        writer.Close();
                    }
                }

                uploadfile1(Server.MapPath("~/image/" + "Terreno_rpt.pdf"), "Terreno_rpt.pdf");
                string strUrl = descargaBlob2("Terreno_rpt.pdf");
                //string strUrl = Path.Combine(Server.MapPath("/image/"), "Terreno_rpt_es.pdf");
                UploadFilesResult r = new UploadFilesResult();
                r.Name = strUrl;
                r.Type = "";
                r.Length = 0;
                return new JsonResult
                {
                    Data = r,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else// Ingles
            {

                PdfWriter writer = PdfWriter.GetInstance(documento, new FileStream(Server.MapPath("~/image/" + "Terreno_rpt_us.pdf"), FileMode.Create));
                //PdfWriter.GetInstance(documento, new FileStream(@"D:\archivo.pdf",FileMode.Create));            
                var t = db.tsg001_terreno.Find(id);
                if (t != null)
                {
                    try
                    {
                        documento.Open();

                        var t0 = db.tsg023_ni_precio.First(i => i.cd_terreno == id);
                        if (t0 == null)
                        {
                        }

                        var t1 = db.tsg027_te_servicio.First(i => i.cd_terreno == id);
                        if (t1 == null)
                        {
                        }

                        var t2 = db.tsg026_te_dt_gral.First(i => i.cd_terreno == id);
                        if (t2 == null)
                        {
                        }

                        Double tp_cmb = 1;
                        var moneda = ""; // por dafault pesos
                        var operador = "*";
                        if (t0.cd_moneda != t0.cd_rep_moneda)
                        {
                            if (t0.cd_moneda != 0) // Se verifica moneda en que fué guardado el registro
                            {
                                var mon = db.tsg044_tipos_monedas.Where(i => i.cd_moneda == t0.cd_moneda).First();
                                if (mon.nb_moneda == "USD") // Dolares
                                {
                                    operador = "*";
                                    if (t0.nu_tipo_cambio != null && t0.nu_tipo_cambio != 0)
                                    {
                                        tp_cmb = (double)t0.nu_tipo_cambio;
                                    }
                                }
                                else
                                {
                                    if (mon.nb_moneda == "MXN") // Pesos
                                    {
                                        operador = "/";                                       
                                        if (t0.nu_tipo_cambio != null && t0.nu_tipo_cambio != 0)
                                        {
                                            tp_cmb = (double)t0.nu_tipo_cambio;
                                        }
                                    }
                                }
                            }
                        }

                        if (t0.cd_rep_moneda != 0)
                        {
                            var mon = db.tsg044_tipos_monedas.Where(i => i.cd_moneda == t0.cd_rep_moneda).First();
                            if (mon.nb_moneda == "USD") // Dolares
                            {
                                moneda = "USD";
                            }
                            else
                            {
                                moneda = "MXN";
                            }
                        }

                        Double preciofinal = 0;
                        Double rentafinal = 0;
                        double p = 0;
                        if (t0.im_venta != null)
                        {
                            p = (Double)t0.im_venta;
                        }
                        if (operador == "*")
                        {
                            preciofinal = Math.Round(p * tp_cmb, 2);
                        }
                        else
                        {
                            preciofinal = Math.Round(p / tp_cmb, 2);
                        }
                        p = 0;
                        if (t0.im_renta != null)
                        {
                            p = (Double)t0.im_renta;
                        }
                        if (operador == "*")
                        {
                            rentafinal = Math.Round(p * tp_cmb, 2);
                        }
                        else
                        {
                            rentafinal = Math.Round(p / tp_cmb, 2);
                        }



                        PdfPTable table = new PdfPTable(3);

                        PdfPCell cell = new PdfPCell(new Phrase("" + t.nb_comercial + "\n", new iTextSharp.text.Font(Vn_Helvetica, 11f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE)));
                        cell.BackgroundColor = new iTextSharp.text.BaseColor(0, 51, 102);
                        cell.Colspan = 3;
                        cell.HorizontalAlignment = 0; // 0 izq, 1 centro, 2 der
                        table.AddCell(cell);

                        // Obtener colonia
                        var col = "";
                        if (t.cd_colonia != null)
                        {
                            var t4 = db.tsg039_colonias.First(i => i.cd_colonia == t.cd_colonia);
                            if (t4 != null)
                            {
                                col = t4.nb_colonia;
                            }
                        }
                        // Obtener colonia
                        var mpio = "";
                        if (t.cd_municipio != null)
                        {
                            var t5 = db.tsg038_municipios.First(i => i.cd_municipio == t.cd_municipio);
                            if (t5 != null)
                            {
                                mpio = t5.nb_municipio;
                            }
                        }
                        PdfPCell cell1 = new PdfPCell(new Phrase("" + t.nb_calle + "\n" + col + "\n" + mpio + "\n\n", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                        cell1.Colspan = 1;
                        cell1.HorizontalAlignment = 0; // 0 izq, 1 centro, 2 der
                        cell1.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell1);
                        PdfPCell cell29 = new PdfPCell(new Phrase("Location Map\n\n\n", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                        cell29.Colspan = 1;
                        cell1.HorizontalAlignment = 1; // 0 izq, 1 centro, 2 der
                        cell29.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell29);
                        PdfPCell cell30 = new PdfPCell(new Phrase("Comments.\n\n\n", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                        cell30.Colspan = 1;
                        cell1.HorizontalAlignment = 1; // 0 izq, 1 centro, 2 der
                        cell30.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell30);
                        try
                        {
                            //imagenes
                            if (urlImage != "")
                            {
                                //imagenes
                                iTextSharp.text.Image mapa = iTextSharp.text.Image.GetInstance(urlImage);
                                //mapa.ScalePercent(40f);
                                mapa.ScaleToFit(247f, 260f);
                                PdfPCell cell400 = new PdfPCell(mapa);
                                cell400.HorizontalAlignment = 1;
                                cell400.VerticalAlignment = 1;
                                cell400.Colspan = 2;
                                cell400.Padding = 3;
                                table.AddCell(cell400);
                                //table.AddCell(mapa); //documento.Add(mapa);
                            }
                            else
                            {
                                iTextSharp.text.Image mapa = iTextSharp.text.Image.GetInstance(Server.MapPath("~/image/" + "nb.png"));
                                //mapa.ScalePercent(40f);
                                mapa.ScaleToFit(247f, 260f);
                                PdfPCell cell400 = new PdfPCell(mapa);
                                cell400.HorizontalAlignment = 1;
                                cell400.VerticalAlignment = 1;
                                cell400.Colspan = 2;
                                cell400.Padding = 3;
                                table.AddCell(cell400);
                                //table.AddCell(mapa); //documento.Add(mapa);

                            }

                        }
                        catch (Exception e)
                        {
                            iTextSharp.text.Image mapa = iTextSharp.text.Image.GetInstance(Server.MapPath("~/image/" + "nb.png"));                            
                            mapa.ScaleToFit(247f, 260f);
                            PdfPCell cell400 = new PdfPCell(mapa);
                            cell400.HorizontalAlignment = 1;
                            cell400.VerticalAlignment = 1;
                            cell400.Colspan = 2;
                            cell400.Padding = 3;
                            table.AddCell(cell400);
                        }
                        if (t.nb_posicion != null || t.nb_poligono != null)
                        {
                            //table.AddCell("Informaciòn de la propiedad");                    
                            iTextSharp.text.Image mapa1 = iTextSharp.text.Image.GetInstance("https://maps.googleapis.com/maps/api/staticmap?center=" + t.nb_posicion + "&zoom=17&size=640x500&maptype=satellite&path=fillcolor:red|" + (t.nb_poligono != null ? t.nb_poligono.TrimEnd('|') : "") + "&key=AIzaSyDhfzatgC_64higR2LlLGkWldmTBudiRWY");
                            //mapa1.ScalePercent(40f);
                            mapa1.ScaleToFit(247f, 260f);
                            PdfPCell cell401 = new PdfPCell(mapa1);
                            cell401.HorizontalAlignment = 1;
                            cell401.VerticalAlignment = 1;
                            cell401.Colspan = 2;
                            cell401.Padding = 3;
                            table.AddCell(cell401);
                            //table.AddCell(mapa1);
                        }
                        else
                        {
                            iTextSharp.text.Image mapa1 = iTextSharp.text.Image.GetInstance(Server.MapPath("~/image/" + "nb.png"));
                            //mapa1.ScalePercent(40f);
                            mapa1.ScaleToFit(247f, 260f);
                            PdfPCell cell401 = new PdfPCell(mapa1);
                            cell401.HorizontalAlignment = 1;
                            cell401.VerticalAlignment = 1;
                            cell401.Colspan = 2;
                            cell401.Padding = 3;
                            table.AddCell(cell401);
                        }
                        //table.AddCell("Terreno de " + t2.nu_tam_max + " m2 con servicios disponibles");
                        PdfPCell cell32 = new PdfPCell(new Phrase("", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell32.Colspan = 1;
                        table.AddCell(cell32);

                        PdfPCell cell2 = new PdfPCell(new Phrase("Property Information", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                        cell2.Colspan = 2;
                        cell2.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell2);

                        //table.AddCell("Informaciòn de la propiedad");                    
                        //table.AddCell("");
                        table.AddCell("");

                        if (t.cd_corredor != null)
                        {
                            var t3 = db.tsg008_corredor_ind.First(i => i.cd_corredor == t.cd_corredor);
                            if (t3 != null)
                            {
                                PdfPCell cell3 = new PdfPCell(new Phrase("Market: " + t3.nb_corredor, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                                cell3.Colspan = 1;
                                cell3.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                                table.AddCell(cell3);
                            }
                            else
                            {
                                PdfPCell cell3 = new PdfPCell(new Phrase("Market: ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                                cell3.Colspan = 1;
                                cell3.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                                table.AddCell(cell3);
                            }
                        }
                        else
                        {
                            PdfPCell cell3 = new PdfPCell(new Phrase("Market: ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                            cell3.Colspan = 1;
                            cell3.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                            table.AddCell(cell3);
                        }

                        //table.AddCell("Corredor: " + t3.nb_corredor);
                        PdfPCell cell28 = new PdfPCell(new Phrase("Ventilation system: ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell28.Colspan = 1;
                        cell28.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell28);
                        table.AddCell("");

                        PdfPCell cell4 = new PdfPCell(new Phrase("Building Class: ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell4.Colspan = 1;
                        cell4.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell4);
                        //table.AddCell("Clasificacion del edificio: ");
                        PdfPCell cell16 = new PdfPCell(new Phrase("Dock High Doors: ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell16.Colspan = 1;
                        cell16.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell16);
                        table.AddCell("");

                        PdfPCell cell5 = new PdfPCell(new Phrase("Date Built: ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell5.Colspan = 1;
                        cell5.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell5);
                        //table.AddCell("Año de construccion:");
                        PdfPCell cell17 = new PdfPCell(new Phrase("Grade Level Doors: ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell17.Colspan = 1;
                        cell17.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell17);

                        table.AddCell("");

                        PdfPCell cell6 = new PdfPCell(new Phrase("Warehouse Rentable: ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell6.Colspan = 1;
                        cell6.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell6);
                        //table.AddCell("Superficie de Nave: ");
                        PdfPCell cell18 = new PdfPCell(new Phrase("Ramps: ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell18.Colspan = 1;
                        cell18.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell18);
                        table.AddCell("");

                        PdfPCell cell7 = new PdfPCell(new Phrase("Office Rentable: ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell7.Colspan = 1;
                        cell7.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell7);
                        //table.AddCell("Superficie de oficinas: ");
                        PdfPCell cell19 = new PdfPCell(new Phrase("Floor Structure: ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell19.Colspan = 1;
                        cell19.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell19);
                        table.AddCell("");

                        PdfPCell cell8 = new PdfPCell(new Phrase("Total Availability: " + t2.nu_tam_max + "", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell8.Colspan = 1;
                        cell8.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell8);
                        //table.AddCell("Disponibilidad Total: ");
                        PdfPCell cell20 = new PdfPCell(new Phrase("Parking stalls: ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell20.Colspan = 1;
                        cell20.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell20);
                        table.AddCell("");

                        PdfPCell cell9 = new PdfPCell(new Phrase("Land lot size: " + t.nu_tamano, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell9.Colspan = 1;
                        cell9.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell9);
                        //table.AddCell("Superficie de Terreno: " );
                        PdfPCell cell21 = new PdfPCell(new Phrase("Lightning: ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell21.Colspan = 1;
                        cell21.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell21);
                        table.AddCell("");

                        PdfPCell cell10 = new PdfPCell(new Phrase("Roof type: ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell10.Colspan = 1;
                        cell10.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell10);
                        //table.AddCell("Techumbre: ");
                        PdfPCell cell22 = new PdfPCell(new Phrase("Power Supply: ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell22.Colspan = 1;
                        cell22.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell22);
                        table.AddCell("");

                        PdfPCell cell11 = new PdfPCell(new Phrase("Min / Max Height: ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell11.Colspan = 1;
                        cell11.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell11);
                        //table.AddCell("Altura Mìnima/Maxima: ");
                        PdfPCell cell23 = new PdfPCell(new Phrase("Potable Water: " + t1.nu_agua, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell23.Colspan = 1;
                        cell23.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell23);
                        table.AddCell("");

                        PdfPCell cell12 = new PdfPCell(new Phrase("Column spacing: ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell12.Colspan = 1;
                        cell12.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell12);
                        //table.AddCell("Distancia entre Columnas: ");
                        if (t1.cd_telefonia != null)
                        {
                            var t4 = db.tsg043_telefonia.First(i => i.cd_telefonia == t1.cd_telefonia);
                            if (t4 != null)
                            {
                                PdfPCell cell24 = new PdfPCell(new Phrase("Telephone service: " + t4.nb_telefonia, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                                cell24.Colspan = 1;
                                cell24.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                                table.AddCell(cell24);
                            }
                            else
                            {
                                PdfPCell cell24 = new PdfPCell(new Phrase("Telephone service: ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                                cell24.Colspan = 1;
                                cell24.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                                table.AddCell(cell24);
                            }
                        }
                        else
                        {
                            PdfPCell cell24 = new PdfPCell(new Phrase("Telephone service: ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                            cell24.Colspan = 1;
                            cell24.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                            table.AddCell(cell24);
                        }

                        table.AddCell("");

                        //var t4 = db.tsg012_sist_incendio.First(i => i.cd_sist_inc == t);
                        //if (t4 == null)
                        //{
                        //}
                        PdfPCell cell13 = new PdfPCell(new Phrase("Fire system: ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell13.Colspan = 1;
                        cell13.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell13);
                        //table.AddCell("Sistema contra incendio: ");
                        PdfPCell cell25 = new PdfPCell(new Phrase("Drop Lots & Maneuver patio: ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell25.Colspan = 1;
                        cell25.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell25);
                        table.AddCell("");

                        PdfPCell cell14 = new PdfPCell(new Phrase("Sprinklers: ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell14.Colspan = 1;
                        cell14.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell14);
                        //table.AddCell("Rociadores: ");
                        PdfPCell cell26 = new PdfPCell(new Phrase("", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell26.Colspan = 1;
                        cell26.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell26);
                        //table.AddCell("");
                        PdfPCell cell31 = new PdfPCell(new Phrase("Price: " + preciofinal + " " + moneda + " \nMaintenance: " + " ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell31.Rowspan = 2;
                        cell31.Border = PdfPCell.NO_BORDER;
                        cell31.CellEvent = new RoundedBorder();
                        //cell31.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell31);

                        PdfPCell cell15 = new PdfPCell(new Phrase("Walls: ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell15.Colspan = 1;
                        cell15.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell15);
                        //table.AddCell("Muros: ");
                        PdfPCell cell27 = new PdfPCell(new Phrase("", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell27.Colspan = 1;
                        cell27.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell27);
                        //table.AddCell("");

                        documento.Add(table);


                        Paragraph p2 = new Paragraph("\n\n", new iTextSharp.text.Font(Vn_Helvetica, 6f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK));
                        documento.Add(p2);
                        //'DIBUJANDO LINEAS
                        Paragraph p1 = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                        documento.Add(p1);

                        Paragraph p100 = new Paragraph("\n\nThis report contains information available to the public and has been relied upon by Cushman & Wakefield on the basis that it is accurate and complete. Cushman & Wakefield accepts no responsibility if this should prove not to be the case. No warranty or representation, express or implied, is made to the accuracy or completeness of the information contained herein, and same is submitted subject to errors, omissions, change of price, rental or other conditions, withdrawal without notice, and to any special listing conditions imposed by our principals.", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 6f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK));
                        documento.Add(p100);

                        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/image/" + "logo_CW.png"));
                        //doc.Add(Image.GetInstance(Server.MapPath("~/image/" + "portadas_naves.png")));
                        logo.ScalePercent(50f);
                        logo.SetAbsolutePosition(documento.PageSize.Width - 175f, documento.PageSize.Height - 10f - 580f);
                        documento.Add(logo);

                        //documento.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.InnerException);
                    }
                    finally
                    {
                        documento.Close();
                        writer.Close();
                    }
                }

                uploadfile1(Server.MapPath("~/image/" + "Terreno_rpt_us.pdf"), "Terreno_rpt_us.pdf");
                string strUrl = descargaBlob2("Terreno_rpt_us.pdf");
                UploadFilesResult r = new UploadFilesResult();
                r.Name = strUrl;
                r.Type = "";
                r.Length = 0;
                return new JsonResult
                {
                    Data = r,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            
        }

    private static void uploadfile1(string ruta, string name)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("cushmandocs");

            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(name);

            // Create or overwrite the "myblob" blob with contents from a local file.
            using (var fileStream = System.IO.File.OpenRead(ruta))
            {
                blockBlob.UploadFromStream(fileStream);
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public JsonResult GetMunicipioByCdEstado(int cd_estado)
        {
            return Json(new SelectList(db.tsg038_municipios.Where(x => x.cd_estado == cd_estado).ToList(), "cd_municipio", "nb_municipio"));
        }

        private string Buscador(object terreno)
        {
            
            List<object> datos = new List<object>();
            var personaFiltro = terreno;
            var tabla1 = new Terreno().tsg001_terreno;

            //Separacion de las propiedades que el usuario no uso
            //var properties = terreno
            //    .GetType()
            //    .GetProperties()
            //    .Where(x => x != null &&
            //                !string.IsNullOrEmpty(x.GetValue(terreno).ToString()));
            var properties = terreno
                .GetType().GetProperties()
                .Where(x => x != null);

            //Creacion de un diccionario que contendrá nombre de la propiedad y valor
            var diccionario = properties.ToDictionary(propiedad =>
                                              propiedad.Name, propiedad =>
                                                              propiedad.GetValue(terreno));

            //foreach (var item in diccionario)
            //{
            //    camponame = item.Name.ToString();
            //    if (camponame.Contains(nombreTabla))
            //    {
            //        camponame = camponame.Replace(nombreTabla + ".", "");
            //        iContador++;
            //        string tipoCampo = string.Empty;
            //        if (IsNumeric(item.Value.ToString()))
            //        {
            //            tipoCampo = "int";
            //        }
            //        else
            //        {
            //            if (item.Value.ToString() == "false" || item.Value.ToString() == "true")
            //            {
            //                tipoCampo = "bool";
            //            }
            //            else
            //            {
            //                tipoCampo = item.Value.Type.ToString();
            //            }
            //        }

            //        switch (tipoCampo)
            //        {
            //            case "String":
            //                if (iContador > 1 && (iContador <= iCuantosCampos))
            //                {
            //                    ////strWhere += ((item.Value.ToString() != "" && strWhere.Length > 0) ? " And " : "");
            //                    strWhere += ((item.Value.ToString() != "" && strWhere.Length > 0) ? " And " : "");
            //                }
            //                //strWhere += (item.Value.ToString() != "" ? item.Name.ToString() + " .Contains(" + Comillas + item.Value.ToString() + Comillas + ")" : "");
            //                strWhere += (item.Value.ToString() != "" ? camponame + " .Contains(" + Comillas + item.Value.ToString() + Comillas + ") " : "");

            //                break;
            //            case "bool":
            //                if (iContador > 1 && (iContador <= iCuantosCampos))
            //                {
            //                    ////strWhere += ((item.Value.ToString() != "" && strWhere.Length > 0) ? " And " : "");
            //                    strWhere += ((item.Value.ToString() != "" && strWhere.Length > 0) ? " And " : "");
            //                }
            //                //strWhere += item.Name.ToString() + " = " + item.Value.ToString();
            //                strWhere += camponame + " = " + item.Value.ToString() + " ";
            //                break;
            //            case "int":
            //                if (iContador > 1 && (iContador <= iCuantosCampos))
            //                {
            //                    ////strWhere += ((item.Value.ToString() != "" && strWhere.Length > 0) ? " And " : "");
            //                    strWhere += ((item.Value.ToString() != "" && strWhere.Length > 0) ? " And " : "");
            //                }
            //                //strWhere += item.Name.ToString() + " = " + item.Value.ToString();
            //                strWhere += camponame + " = " + item.Value.ToString() + " ";
            //                break;
            //        }
            //    }
            //}


            return "";
        }

        public JsonResult GetTerreno()
        {

            
            List<object> tsi001_dts_terreno = new List<object>();// db.tsg001_terreno.Select("new (cd_terreno, nb_comercial)");
            
            //terreno.tsg001_terreno.cd_terreno;

            //Buscador(terreno.tsg001_terreno);


            Boolean filtro = false;
            string json;
            using (var reader = new StreamReader(Request.InputStream))
            {
                json = reader.ReadToEnd();
            }

            string strWhere = String.Empty;
            string strWhereTemp = String.Empty;

            var tabla = db.tsg001_terreno.First();
            //************** Filtro en la tabla terrenos ************************
            var dbTerreno = db.tsg001_terreno.First();
            dbTerreno.nb_dir_terr = "";
            dbTerreno.nb_poligono = "";
            dbTerreno.nb_posicion = "";
            if (dbTerreno != null)
            {
                strWhere = obterFiltrado(json, "tsg001_terreno", dbTerreno);
                strWhereTemp = strWhereTemp + strWhere;
                if (strWhere != String.Empty)
                {
                    if (strWhere == "")
                    {
                        strWhere = "1=1";
                    }

                    var cd_terrenos = db.tsg001_terreno.Where(strWhere);//.Select("new (cd_terreno)");          
                    //myDatos = db.tsg001_terreno.Where(strWhere).Select("new (cd_terreno, nb_comercial)");
                    if (cd_terrenos.Count() > 0)
                    {
                        foreach (var id in cd_terrenos)
                        {
                            if (!tsi001_dts_terreno.Contains(id.cd_terreno))
                            {
                                tsi001_dts_terreno.Add(
                                 db.tsg001_terreno.Where(x => x.cd_terreno == id.cd_terreno).Select("new (cd_terreno, nb_comercial)"));
                            }
                        }
                    }
                }
            }

            //filtro = false;

            //************** Filtro en la tabla de precios para terrenos ************************
            var dbPrecio = db.tsg023_ni_precio.First();
            if (dbPrecio != null)
            {
                strWhere = obterFiltrado(json, "tsg023_ni_precio", dbPrecio);
                strWhereTemp = strWhereTemp + strWhere;
                if (strWhere != String.Empty)
                {
                    if (strWhere == "")
                    {
                        strWhere = "1=1";
                    }
                    var precio = db.tsg023_ni_precio.Where(strWhere);//.Select("new (cd_terreno)");
                    

                    if (precio.Count() > 0)
                    {
                        foreach (var id in precio)
                        {
                            if (!tsi001_dts_terreno.Contains(id.cd_terreno))
                            {
                                tsi001_dts_terreno.Add(
                             db.tsg001_terreno.Where(x => x.cd_terreno == id.cd_terreno).
                                 Select("new (cd_terreno, nb_comercial)"));
                            }
                        }
                    }
                }
            }
            filtro = false;

            //************** Filtro en la tabla General de terrenos ************************
            var dbgeneral = db.tsg026_te_dt_gral.First();
            if (dbgeneral != null)
            {
                strWhere = obterFiltrado(json, "tsg026_te_dt_gral", dbgeneral);
                strWhereTemp = strWhereTemp + strWhere;
                if (strWhere != String.Empty)
                {
                    if (strWhere == "")
                    {
                        strWhere = "1=1";
                    }

                    var general = db.tsg026_te_dt_gral.Where(strWhere);//.Select("new (cd_terreno)");                
                    if (general.Count() > 0)
                    {
                        foreach (var id in general)
                        {
                            if (!tsi001_dts_terreno.Contains(id.cd_terreno))
                            {
                                tsi001_dts_terreno.Add(
                                db.tsg001_terreno.Where(x => x.cd_terreno == id.cd_terreno).
                                 Select("new (cd_terreno, nb_comercial)"));
                            }
                        }
                    }
                }
            }
            filtro = false;

            //************** Filtro en la tabla servicios de terrenos ************************
            var dbservicio = db.tsg027_te_servicio.First();
            if (dbservicio != null)
            {
                strWhere = obterFiltrado(json, "tsg027_te_servicio", dbservicio);
                strWhereTemp = strWhereTemp + strWhere;
                if (strWhere != String.Empty)
                {
                    if (strWhere == "")
                    {
                        strWhere = "1=1";
                    }
                    var servicios = db.tsg027_te_servicio.Where(strWhere);//.Select("new (cd_terreno)");

                    if (servicios.Count() > 0)
                    {
                        foreach (var id in servicios)
                        {
                            if (!tsi001_dts_terreno.Contains(id.cd_terreno))
                            {
                                tsi001_dts_terreno.Add(
                             db.tsg001_terreno.Where(x => x.cd_terreno == id.cd_terreno).
                                 Select("new (cd_terreno, nb_comercial)"));
                            }
                        }
                    }
                }
            }
            filtro = false;

            //************** Filtro en la tabla propietarios de terrenos ************************
            var dbcontacto_p = db.tsg028_te_contacto.First();
            if (dbcontacto_p != null)
            {
                strWhere = obterFiltrado(json, "tsg028_te_contacto_Prop", dbcontacto_p);
                strWhereTemp = strWhereTemp + strWhere;
                if (strWhere != String.Empty)
                {
                    if (strWhere == "")
                    {
                        strWhere = "1=1";
                    }

                    var propietarios = db.tsg028_te_contacto.Where(strWhere);//.Select("new (cd_terreno)");
                    propietarios = propietarios.Where(x => x.st_contacto == 1);

                    if (propietarios.Count() > 0)
                    {
                        foreach (var id in propietarios)
                        {
                            if (!tsi001_dts_terreno.Contains(id.cd_terreno))
                            {
                                tsi001_dts_terreno.Add(
                             db.tsg001_terreno.Where(x => x.cd_terreno == id.cd_terreno).
                                 Select("new (cd_terreno, nb_comercial)"));
                            }
                        }
                    }
                }
            }

            filtro = false;

            //************** Filtro en la tabla Administrador de terrenos ************************
            var dbcontacto_a = db.tsg028_te_contacto.First();
           

            if (dbcontacto_a != null)
            {
                strWhere = obterFiltrado(json, "tsg028_te_contacto_Adm", dbcontacto_a);
                strWhereTemp = strWhereTemp + strWhere;
                if (strWhere != String.Empty)
                {
                    if (strWhere == "")
                    {
                        strWhere = "1=1";
                    }
                    var administrador = db.tsg028_te_contacto.Where(strWhere);//.Select("new (cd_terreno)");

                    administrador = administrador.Where(x => x.st_contacto == 2);
                    if (administrador.Count() > 0)
                    {
                        //var tsi001_dts_terreno = from terrenos in db.tsg001_terreno select terrenos new {  }                    
                        foreach (var id in administrador)
                        {
                            if (!tsi001_dts_terreno.Contains(id.cd_terreno))
                            {
                                tsi001_dts_terreno.Add(
                             db.tsg001_terreno.Where(x => x.cd_terreno == id.cd_terreno).
                                 Select("new (cd_terreno, nb_comercial)"));
                            }
                        }
                    }
                }
            }
            filtro = false;

            //************** Filtro en la tabla corredor de terrenos ************************
            var dbcontacto_c = db.tsg028_te_contacto.First();
            if (dbcontacto_c != null)
            {
                strWhere = obterFiltrado(json, "tsg028_te_contacto_Corr", dbcontacto_c);
                strWhereTemp = strWhereTemp + strWhere;
                if (strWhere != String.Empty)
                {
                    if (strWhere == "")
                    {
                        strWhere = "1=1";
                    }
                    var corredor = db.tsg028_te_contacto.Where(x => x.st_contacto == 3);//.Select("new (cd_terreno)");
                    corredor = corredor.Where(strWhere);
                    if (corredor.Count() > 0)
                    {
                        foreach (var id in corredor)
                        {
                            if (!tsi001_dts_terreno.Contains(id.cd_terreno))
                            {
                                tsi001_dts_terreno.Add(
                             db.tsg001_terreno.Where(x => x.cd_terreno == id.cd_terreno).
                                 Select("new (cd_terreno, nb_comercial)"));
                            }
                        }
                    }
                }
            }

            //************** obtengo todo ************************

            if (tsi001_dts_terreno.Count() == 0 && strWhereTemp == String.Empty)
            {
                strWhere = "1=1";
                var todo = db.tsg001_terreno.Where(strWhere);//.Select("new (cd_terreno)");                
                if (todo.Count() > 0)
                {
                    foreach (var id in todo)
                    {
                        int cd_terreno = id.cd_terreno;
                        tsi001_dts_terreno.Add(
                             db.tsg001_terreno.Where(x => x.cd_terreno == id.cd_terreno).
                                 Select("new (cd_terreno, nb_comercial)"));
                    }
                }
            }
           
            return new JsonResult { Data = tsi001_dts_terreno.ToList(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetTerrenoAll()
        {


            List<object> tsi001_dts_terreno = new List<object>();// db.tsg001_terreno.Select("new (cd_terreno, nb_comercial)");
            var todo = db.tsg001_terreno;//.Select("new (cd_terreno)");                
            if (todo.Count() > 0)
            {
                foreach (var id in todo)
                {
                    int cd_terreno = id.cd_terreno;
                    tsi001_dts_terreno.Add(
                         db.tsg001_terreno.Where(x => x.cd_terreno == id.cd_terreno).
                             Select("new (cd_terreno, nb_comercial)"));
                }
            }
            return new JsonResult { Data = tsi001_dts_terreno.ToList(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult GetTerrenoDisponible(string disp_desde_p, string disp_hasta_p)
        {
            //double dis_desde_p, double disp_hasta_p
            decimal inicio = Convert.ToDecimal(disp_desde_p);
            decimal fin = Convert.ToDecimal(disp_hasta_p);
            List<object> tsi001_dts_terreno = new List<object>();// db.tsg001_terreno.Select("new (cd_terreno, nb_comercial)");
            

            var general = db.tsg026_te_dt_gral.Where(x => x.nu_disponiblidad >= inicio && x.nu_disponiblidad <= fin);

            if (general.Count() > 0)
            {
                foreach (var id in general)
                {
                    if (!tsi001_dts_terreno.Contains(id.cd_terreno))
                    {
                        tsi001_dts_terreno.Add(
                        db.tsg001_terreno.Where(x => x.cd_terreno == id.cd_terreno).
                         Select("new (cd_terreno, nb_comercial)"));
                    }
                }
            }
            return new JsonResult { Data = tsi001_dts_terreno.ToList(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        private string obterFiltrado(string json, string nombreTabla, object tabla)
        {
            //string json;
            //using (var reader = new StreamReader(Request.InputStream))
            //{
            //    json = reader.ReadToEnd();
            //}
            Terreno terreno = new Terreno();

            
            var properties = tabla
                .GetType().GetProperties()
                .Where(x => x != null);

            //Creacion de un diccionario que contendrá nombre de la propiedad y valor
            var diccionario = properties.ToDictionary(propiedad =>
                                              propiedad.Name, propiedad =>
                                                              propiedad.GetValue(tabla));



            string strWhere = String.Empty;


            // Create the json.Net Linq object for our json string
            JObject jsonObject = JObject.Parse(json);
            int iCuantosCampos = jsonObject.Count;
            //int iCuatosTerrenos = jsonObject.
            int iContador = 0;

            // eval into an expando
            dynamic dynObject = JsonConvert.DeserializeObject(jsonObject.ToString());
            var vJson = jsonObject;

            const string Comillas = "\"";
            string camponame = "";

            foreach (var item in dynObject)
            {
                camponame = item.Name.ToString();
                if (camponame.Contains(nombreTabla))
                {
                    camponame = camponame.Replace(nombreTabla + ".", "");
                    iContador++;

                    //if (camponame.Contains("cd_") && item.Value.ToString() != "0")
                    //{
                    if (diccionario[camponame].GetType().Name != null)
                    {
                        if (item.Value.ToString() != "[\r\n  \"\",\r\n  \"\"\r\n]")
                        {
                            string strTipoDato = diccionario[camponame].GetType().Name.ToString();
                            switch (strTipoDato)
                            {
                                case "String":
                                    if (iContador > 1 && (iContador <= iCuantosCampos))
                                    {
                                        ////strWhere += ((item.Value.ToString() != "" && strWhere.Length > 0) ? " And " : "");
                                        strWhere += ((item.Value.ToString() != "" && strWhere.Length > 0) ? " And " : "");
                                    }
                                    //strWhere += (item.Value.ToString() != "" ? item.Name.ToString() + " .Contains(" + Comillas + item.Value.ToString() + Comillas + ")" : "");
                                    strWhere += (item.Value.ToString() != "" ? camponame + " .Contains(" + Comillas + item.Value.ToString() + Comillas + ") " : "");

                                    break;
                                case "bool":
                                    if (iContador > 1 && (iContador <= iCuantosCampos))
                                    {
                                        ////strWhere += ((item.Value.ToString() != "" && strWhere.Length > 0) ? " And " : "");
                                        strWhere += ((item.Value.ToString() != "" && strWhere.Length > 0) ? " And " : "");
                                    }
                                    //strWhere += item.Name.ToString() + " = " + item.Value.ToString();
                                    strWhere += (item.Value.ToString() != "" ? camponame + " = " + item.Value.ToString() + " " : "");
                                    break;
                                case "Int32":
                                    if (iContador > 1 && (iContador <= iCuantosCampos))
                                    {
                                        ////strWhere += ((item.Value.ToString() != "" && strWhere.Length > 0) ? " And " : "");
                                        strWhere += ((item.Value.ToString() != "" && strWhere.Length > 0) ? " And " : "");
                                    }
                                    //strWhere += item.Name.ToString() + " = " + item.Value.ToString();
                                    //strWhere += (item.Value.ToString() != "" ? camponame + " == " + item.Value.ToString() + " " : "");
                                    strWhere += (item.Value.ToString() != "" ? camponame + " == " + item.Value[1].Value + " " : "");
                                    break;
                                case "Decimal":
                                    if (iContador > 1 && (iContador <= iCuantosCampos))
                                    {
                                        ////strWhere += ((item.Value.ToString() != "" && strWhere.Length > 0) ? " And " : "");
                                        strWhere += ((item.Value.ToString() != "" && strWhere.Length > 0) ? " And " : "");
                                    }
                                    //strWhere += item.Name.ToString() + " = " + item.Value.ToString();
                                    strWhere += (item.Value.ToString() != "" ? camponame + " == " + item.Value.ToString() + " " : "");
                                    break;
                            }
                        }
                    }
                    //}
                }
            }
            return strWhere;
        }


        public String BlobStorage2(String strFile, FileStream hpf)
        {
            string myURL;
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference("cushmandocs");

            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(strFile);

            // Create or overwrite the "myblob" blob with contents from a local file.

            blockBlob.UploadFromStream(hpf);

            myURL = blockBlob.Uri.ToString();
            return myURL;
        }


        public String BlobStorage(String strFile, HttpPostedFileBase hpf)
        {
            string myURL;
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference("cushmandocs");

            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(hpf.FileName);

            // Create or overwrite the "myblob" blob with contents from a local file.

            blockBlob.UploadFromStream(hpf.InputStream);

            myURL = blockBlob.Uri.ToString();
            return myURL;
        }
        public String BlobStorage2(int? id, String strFile, HttpPostedFileBase hpf)
        {
            string myURL;
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference("cushmandocs/imagenes/T" + id);
            //if (container.CreateIfNotExists())
            //{
            //    var blobContainerPermissions = container.GetPermissions();

            //    blobContainerPermissions.PublicAccess = BlobContainerPublicAccessType.Container;
            //    container.SetPermissions(blobContainerPermissions);
            //}

            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(hpf.FileName);

            // Create or overwrite the "myblob" blob with contents from a local file.

            blockBlob.UploadFromStream(hpf.InputStream);

            myURL = blockBlob.Uri.ToString();
            return myURL;
        }
        private void eliminarBlod(int? id, String strFileName)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("cushmandocs/imagenes/T" + id);

            // Retrieve reference to a blob named "photo1.jpg".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(strFileName);

            // Delete the blob.
            blockBlob.Delete();
        }
        private static string descargaBlob(int? id, String strFileName)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("cushmandocs/imagenes/T" + id);

            // Retrieve reference to a blob named "photo1.jpg".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(strFileName);

            SharedAccessBlobPolicy policy = new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(12),
            };

            SharedAccessBlobHeaders headers = new SharedAccessBlobHeaders()
            {
                ContentDisposition = string.Format("attachment;filename=\"{0}\"", strFileName),
            };

            var sasToken = blockBlob.GetSharedAccessSignature(policy, headers);
            return blockBlob.Uri.AbsoluteUri + sasToken;

            // Save blob contents to a file.=
            //using (var fileStream = System.IO.File.OpenWrite(Path.Combine(Server.MapPath("~/App_Data"), strFileName)))
            //using (var fileStream = System.IO.File.OpenWrite(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), strFileName)))
            //{
                //blockBlob.DownloadToStream(Response.OutputStream);
            //}
        }
        private static string descargaBlob2(String strFileName)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("cushmandocs");

            // Retrieve reference to a blob named "photo1.jpg".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(strFileName);

            SharedAccessBlobPolicy policy = new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(12),
            };

            SharedAccessBlobHeaders headers = new SharedAccessBlobHeaders()
            {
                ContentDisposition = string.Format("attachment;filename=\"{0}\"", strFileName),
            };

            var sasToken = blockBlob.GetSharedAccessSignature(policy, headers);
            return blockBlob.Uri.AbsoluteUri + sasToken;

            // Save blob contents to a file.=
            //using (var fileStream = System.IO.File.OpenWrite(Path.Combine(Server.MapPath("~/App_Data"), strFileName)))
            //using (var fileStream = System.IO.File.OpenWrite(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), strFileName)))
            //{
            //blockBlob.DownloadToStream(Response.OutputStream);
            //}
        }
        [HttpPost]
        public JsonResult UploadKml(int? id)
        {
            UploadFilesResult result = new UploadFilesResult();
            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                result = leerArchivoKml(hpf);
            }

            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        private UploadFilesResult leerArchivoKml(HttpPostedFileBase documento)

        {            
            UploadFilesResult result = new UploadFilesResult();
            if (documento != null)
            {
                if (documento.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(documento.FileName);

                    documento.SaveAs(Server.MapPath("~/App_Data/" + fileName));                    
                    var path = Path.Combine(Server.MapPath("~/App_Data"), fileName);                    
                    XmlDocument xDoc = new XmlDocument();
                    XNamespace ns = "http://earth.google.com/kml/2.2";                    
                    xDoc.Load(path);                    
                    XmlNodeList personas = xDoc.GetElementsByTagName("Document");                    
                    XmlNodeList lista =  ((XmlElement)personas[0]).GetElementsByTagName("Placemark");                    
                    XmlNodeList lista2 = ((XmlElement)personas[0]).GetElementsByTagName("Polygon");
                    XmlNodeList lista3 = ((XmlElement)personas[0]).GetElementsByTagName("outerBoundaryIs");                    
                    XmlNodeList lista4 = ((XmlElement)personas[0]).GetElementsByTagName("LinearRing");

                    String strCoordenas = null;

                    foreach (XmlElement nodo in lista4)

                    {



                        XmlNodeList lista5 = nodo.GetElementsByTagName("coordinates");

                        strCoordenas = lista5[0].InnerText;

                    }

                    strCoordenas = strCoordenas.Replace("\n\t\t\t\t\t\t", "");



                    strCoordenas = strCoordenas.Replace(" \n\t\t\t\t\t", "");

                    string[] puntoCardinales = strCoordenas.Split(' ');

                    int i = 0;

                    String[] valores = null;

                    String puntoLimpios = null;

                    String poligono = null;

                    String posicion = null;

                    foreach (string punto in puntoCardinales)

                    {

                        if (posicion == null)

                        {

                            posicion = punto.Substring(0, punto.Length - 2);

                        }

                        poligono = poligono + punto.Substring(0, punto.Length - 2) + "|";

                        poligono = poligono.Replace(",|", "|");



                        i++;

                    }

                    result.Name = posicion;

                    result.Type = poligono;

                    result.Length = 0;



                    //respuesta.datos = coordenadas.Substring(0, coordenadas.Length - 1);

                }

            }

            else

            {

                result.Name = null;

                result.Type = null;

                result.Length = 0;

                //respuesta.respuesta = false;

                //respuesta.mensaje = "No se adjunto un archivo";

                //respuesta.datos = "";

            }

            return result;

        }
        [HttpPost]
        public JsonResult actualizaArchivo(int? id, int? id_reporte)
        {
            var imagenes = db.tsg040_imagenes_terrenos.Find(id);
            if (imagenes != null)
            {
                imagenes.cd_reporte = id_reporte;
                db.Entry(imagenes).CurrentValues.SetValues(imagenes);
                db.SaveChanges();
            }

            var cd_terreno = db.tsg040_imagenes_terrenos.First(i => i.cd_id == id);
            var imagenes2 = db.tsg040_imagenes_terrenos.Where(t => t.cd_terreno == cd_terreno.cd_terreno).Select("new (cd_terreno, cd_id, nb_archivo, tx_archivo)");
            return new JsonResult { Data = imagenes2, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult UploadFiles(int? id)
        {

            String strUrl;
            //HttpPostedFileBase
            int cd_id = 0;

            foreach (string file in Request.Files)
            {
                
                //r = new List<UploadFilesResult>();
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                
                if (hpf.ContentLength == 0)
                    continue;
                Random num_archivo = new Random(DateTime.Now.Millisecond);

                string NumArch = num_archivo.Next().ToString();
                //NumArch + Path.GetExtension(hpf.FileName);
                ///hpf. = NumArch;
               
                //System.IO.Directory.CreateDirectory(Server.MapPath("~/image/T"+id));
                //string savedFileName = Path.Combine(Server.MapPath("~/image/T"+id +"/"), NumArch + Path.GetExtension(hpf.FileName));
                //hpf.SaveAs(savedFileName);
                
                strUrl = BlobStorage2(id, NumArch + Path.GetExtension(hpf.FileName), hpf);
                tsg040_imagenes_terrenos archivosx = new tsg040_imagenes_terrenos();
                var t = db.tsg001_terreno.Find(id);
                archivosx.cd_terreno = t.cd_terreno;
                //archivosx.nb_archivo = NumArch + Path.GetExtension(hpf.FileName);
                //archivosx.tx_archivo = Path.Combine(Server.MapPath("~/image/T"+id+"/"), NumArch + Path.GetExtension(hpf.FileName));
                archivosx.nb_archivo = hpf.FileName;
                archivosx.tx_archivo = strUrl;
                archivosx.fh_modif = DateTime.Now;

                db.tsg040_imagenes_terrenos.Add(archivosx);
                db.SaveChanges();
                cd_id = archivosx.cd_id;
                tsg029_bitacora_mov.GuardaBitacora("tsg040_imagenes_terrenos", "cd_terreno", t.cd_terreno, "Insertar Imagen");

            }
            var imagenes = db.tsg040_imagenes_terrenos.Where(t => t.cd_terreno == id && t.cd_id == cd_id).Select("new (cd_terreno, cd_id, nb_archivo, tx_archivo)");
            //return Content("{\"name\":\"" + r[0].Name + "\",\"type\":\"" + r[0].Type + "\",\"size\":\"" + string.Format("{0} bytes", r[0].Length) + "\"}", "application/json");
            return new JsonResult { Data = imagenes, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        //public JsonResult downloadFile(int? id, string archivo)
        public ActionResult downloadFile(int? id, string archivo)
        {
            var sasUrl = descargaBlob(id, archivo);

           
            //string savedFileName = Path.Combine(Server.MapPath("~/App_Data"), archivo.Name + archivo.Name.Split('.').LastOrDefault());
            //System.IO.File.Delete(savedFileName);

            //var t = db.tsg001_terreno.Find(id);

            var imagenes = db.tsg040_imagenes_terrenos.Where(t => t.cd_terreno == id).Select("new (cd_id, nb_archivo, tx_archivo)");
            //return Content("{\"name\":\"" + r[0].Name + "\",\"type\":\"" + r[0].Type + "\",\"size\":\"" + string.Format("{0} bytes", r[0].Length) + "\"}", "application/json");
            //
            // return  Redirect(sasUrl);

            // return new JsonResult { Data = imagenes, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            UploadFilesResult r = new UploadFilesResult();
            r.Name = sasUrl;
            r.Type = "";
            r.Length = 0;
            return new JsonResult { Data = r, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult getFile(int? id)
        {
            var imagenes = db.tsg040_imagenes_terrenos.Where(t => t.cd_terreno == id).Select("new (cd_terreno, cd_id, nb_archivo, tx_archivo)");
            //return Content("{\"name\":\"" + r[0].Name + "\",\"type\":\"" + r[0].Type + "\",\"size\":\"" + string.Format("{0} bytes", r[0].Length) + "\"}", "application/json");
            return new JsonResult { Data = imagenes, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [HttpPost]
        public JsonResult EliminarArchivo(int? id, int? claves, string archivo)
        {
            try
            {
                eliminarBlod(id, archivo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
            
            //string savedFileName = Path.Combine(Server.MapPath("~/image/T" + id + "/"), archivo);
            //System.IO.File.Delete(savedFileName);
            var t1 = db.tsg040_imagenes_terrenos.First(i => i.cd_terreno == id && i.cd_id == claves);
            db.tsg040_imagenes_terrenos.Remove(t1);
            tsg029_bitacora_mov.GuardaBitacora("tsg040_imagenes_terrenos", "cd_id", t1.cd_id, "Eliminar Imagen");
            db.SaveChanges();


            var imagenes = db.tsg040_imagenes_terrenos.Where(t => t.cd_terreno == id).Select("new (cd_terreno, cd_id, nb_archivo, tx_archivo)");
            //return Content("{\"name\":\"" + r[0].Name + "\",\"type\":\"" + r[0].Type + "\",\"size\":\"" + string.Format("{0} bytes", r[0].Length) + "\"}", "application/json");
            return new JsonResult { Data = imagenes, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        private static Boolean IsNumeric(string valor)
        {
            int result;
            return int.TryParse(valor, out result);
        }
        public ActionResult iniCreate()
        {
            return RedirectToAction("Crear");
        }

    }

    public class SelectItem
    {
        public int id { get; set; }
        public string text { get; set; }
    }

    public class datos
    {
        public int cd_terrno { get; set; }
        public string nb_terreno { get; set; }
    }
}
