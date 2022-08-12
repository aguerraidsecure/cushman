using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using wr_anit_cushman_one.Models;
using PagedList;
using System.IO;
//using Newtonsoft.Json.Linq;
//using Newtonsoft.Json;
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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace wr_anit_cushman_one.Controllers
{
    [Autenticado]

    public class NavesController : Controller
    {
        //Comentarios

        private CushmanContext db = new CushmanContext();
        tsg029_bitacora_mov tsg029_bitacora_mov = new tsg029_bitacora_mov();
        // GET: Naves
        const string Municipio_KEY = "tsg038_municipios";

        clsNaves naves;

        public NavesController()
        {
                                    naves =                  new clsNaves();
            tsg002_nave_industrial  tsg002_nave_industrial  = new tsg002_nave_industrial();
            tsg009_ni_dt_gral       tsg009_ni_dt_gral       = new tsg009_ni_dt_gral();
            tsg020_ni_servicio      tsg020_ni_servicio      = new tsg020_ni_servicio();
            tsg023_ni_precio        tsg023_ni_precio        = new tsg023_ni_precio(); ;
            tsg025_ni_contacto      propietario             = new tsg025_ni_contacto(); ;
            tsg025_ni_contacto      administrador           = new tsg025_ni_contacto();
            tsg025_ni_contacto      corredor                = new tsg025_ni_contacto();
            tsg045_imagenes_naves   imagenes_Naves          = new tsg045_imagenes_naves();


            naves.tsg002_nave_industrial    = tsg002_nave_industrial;
            naves.tsg009_ni_dt_gral         = tsg009_ni_dt_gral;
            naves.tsg020_ni_servicio        = tsg020_ni_servicio;
            naves.tsg023_ni_precio          = tsg023_ni_precio;
            naves.tsg025_ni_contacto_P      = propietario;
            naves.tsg025_ni_contacto_A      = administrador;
            naves.tsg025_ni_contacto_C      = corredor;
            naves.tsg045_imagenes_naves     = imagenes_Naves;
        }
        public ActionResult Index()
        {
            ViewBag.UsuarioActivo = true;
            return View();
        }

        private void ObtenerTipoCambio( decimal nu_tipo_cambio)
        {
            try
            {
                ViewBag.UsuarioActivo = true;
                ViewBag.disabled = true;
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
                string valor = null;
                foreach (XmlElement nodo in lista2)
                {

                    XmlNodeList lista5 = nodo.GetElementsByTagName("bm:Obs");
                    ///lista5[0].Attributes.Item
                    valor = lista5[0].Attributes.GetNamedItem("OBS_VALUE").InnerText;
                    break;
                }
                
                ViewBag.TipoCambio = (nu_tipo_cambio > 0? nu_tipo_cambio.ToString() : valor) ;
            }
            catch (Exception e)
            {
                ViewBag.TipoCambio = "0";
            }
            
        }
        public ActionResult Change(String strLenguanje)
        {
            ObtenerTipoCambio((decimal)(ViewBag.TipoCambio==null?0: ViewBag.TipoCambio));
            if (strLenguanje != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(strLenguanje);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(strLenguanje);
            }
            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = strLenguanje;
            Response.Cookies.Add(cookie);

            return View("Create", naves);
        }
        // GET: Naves/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tsg002_nave_industrial tsi001_terreno_dts_gra = await db.tsg002_nave_industrial.FindAsync(id);
            if (tsi001_terreno_dts_gra == null)
            {
                return HttpNotFound();
            }
            return View(tsi001_terreno_dts_gra);
        }

        // GET: Naves/Create
        public ActionResult Create(int? id)
        {
           


            clsNaves naves = new clsNaves();
            tsg002_nave_industrial tsg002_nave_industrial = new tsg002_nave_industrial();
            tsg009_ni_dt_gral tsg009_ni_dt_gral = new tsg009_ni_dt_gral();
            tsg020_ni_servicio tsg020_ni_servicio = new tsg020_ni_servicio();
            tsg023_ni_precio tsg023_ni_precio = new tsg023_ni_precio(); ;
            tsg025_ni_contacto propietario = new tsg025_ni_contacto(); ;
            tsg025_ni_contacto administrador = new tsg025_ni_contacto();
            tsg025_ni_contacto corredor = new tsg025_ni_contacto();
            tsg045_imagenes_naves imagenes_Naves = new tsg045_imagenes_naves();


            naves.tsg002_nave_industrial = tsg002_nave_industrial;
            naves.tsg009_ni_dt_gral = tsg009_ni_dt_gral;
            naves.tsg020_ni_servicio = tsg020_ni_servicio;
            naves.tsg023_ni_precio = tsg023_ni_precio;
            naves.tsg025_ni_contacto_P = propietario;
            naves.tsg025_ni_contacto_A = administrador;
            naves.tsg025_ni_contacto_C = corredor;
            naves.tsg045_imagenes_naves = imagenes_Naves;

            ViewBag.UsuarioActivo = true;
            ViewBag.disabled = true;

            if (id == null)
            {
                //ViewBag.cd_mercado = new SelectList(db.tsg007_mercado, "cd_mercado", "nb_mercado");
                ViewBag.cd_corredor = new SelectList(db.tsg008_corredor_ind, "cd_corredor", "nb_corredor");
                ViewBag.cd_st_entrega = new SelectList(db.tsg017_st_entrega, "cd_st_entrega", "nb_st_entrega");
                ViewBag.cd_esp_ferr = new SelectList(db.tsg022_esp_ferr, "cd_esp_ferr", "nb_esp_ferr");
                ObtenerTipoCambio((decimal)0);
                return View(naves);

            }
            else
            {
                ViewBag.Folio = "E"+id;
                tsg002_nave_industrial = db.tsg002_nave_industrial.Find(id);
                int cd_general = db.tsg009_ni_dt_gral.Where(x => x.cd_nave == id).Max(x => x.cd_id);
                tsg009_ni_dt_gral = db.tsg009_ni_dt_gral.Find(cd_general);
                int cd_servicio = db.tsg020_ni_servicio.Where(x => x.cd_nave == id).Max(x => x.cd_servicio);
                tsg020_ni_servicio = db.tsg020_ni_servicio.Find(cd_servicio);
                int cd_precio = db.tsg023_ni_precio.Where(x => x.cd_nave == id).Max(x => x.cd_ni_precio);
                tsg023_ni_precio = db.tsg023_ni_precio.Find(cd_precio);
                int cd_propietario = db.tsg025_ni_contacto.Where(x => x.cd_nave == id && x.st_contacto == 1).Max(x => x.cd_contacto);
                propietario = db.tsg025_ni_contacto.Find(cd_propietario);
                int cd_administrador = db.tsg025_ni_contacto.Where(x => x.cd_nave == id && x.st_contacto == 2).Max(x => x.cd_contacto);
                administrador = db.tsg025_ni_contacto.Find(cd_administrador);
                int cd_corredor = db.tsg025_ni_contacto.Where(x => x.cd_nave == id && x.st_contacto == 3).Max(x => x.cd_contacto);
                corredor = db.tsg025_ni_contacto.Find(cd_corredor);
                naves.tsg002_nave_industrial = tsg002_nave_industrial;
                naves.tsg009_ni_dt_gral = tsg009_ni_dt_gral;
                naves.tsg020_ni_servicio = tsg020_ni_servicio;
                naves.tsg023_ni_precio = tsg023_ni_precio;
                naves.tsg025_ni_contacto_P = propietario;
                naves.tsg025_ni_contacto_A = administrador;
                naves.tsg025_ni_contacto_C = corredor;
                if (naves.tsg023_ni_precio.nu_tipo_cambio != null)
                {
                    ObtenerTipoCambio((decimal)naves.tsg023_ni_precio.nu_tipo_cambio);
                }
                else
                {
                    ObtenerTipoCambio(0);
                }

            }
           
            
            return View(naves);
        
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

        public ActionResult BuscarFolio(String id)
        {
            //id.Replace("NE", "");

            //Create(Convert.ToInt32(id));
            return View();
        }

        public virtual ActionResult Test()
        {
            var m = new clsNaves();
            return View(m);
        }

        // POST: Naves/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(clsNaves Naves, int? id)
        {
            //ViewBag.UsuarioActivo = true;
            var respuesta = new ResponseModel
            {
                respuesta = true,
                redirect = "/",
                error = ""
            };

            //if (ModelState.IsValid)
            //{
            //    //var tp_producto = this.Request.Form["opciones"];
            //    if (id == null) //Nuevo para guardar
            //        guardarNaves(Naves);
            //    else // no es nuevo, hay que  guardar(editar), o eliminar
            //    {
            //        if (Naves.tsg002_nave_industrial.cd_nave == 0)
            //        {
            //            guardarNaves(Naves);
            //        }
            //        else
            //        {
            //            actualizaNaves(Naves, id);
            //        }
            //    }
            //}
            //else
            //{
            //    var message = string.Join(" |\n ", ModelState.Values
            //     .SelectMany(v => v.Errors)
            //     .Select(e => e.ErrorMessage));
            //    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest, message);
            //    //ViewBag.Error = message;
            //    respuesta.respuesta = false;
            //    respuesta.error = message;
            //    return Json(respuesta);
            //}


            if (id == null) //Nuevo para guardar
                guardarNaves(Naves);
            else // no es nuevo, hay que  guardar(editar), o eliminar
            {
                if (Naves.tsg002_nave_industrial.cd_nave == 0)
                {
                    guardarNaves(Naves);
                }
                else
                {
                    actualizaNaves(Naves, id);
                }
            }


            respuesta.redirect = Naves.tsg002_nave_industrial.cd_nave.ToString();
            return Json(respuesta);
        }

        [HttpPost]
        public JsonResult BuscaCP(string id)
        {
            var localizacion = db.tsg039_colonias.Where(i => i.nu_cp.Contains(id)).Select("new (cd_colonia, cd_municipio, cd_estado)");

            return new JsonResult { Data = localizacion.ToListAsync(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult ObtenerCP(int id_estado, int id_municipio, int id_colonia)
        {
            var localizacion = db.tsg039_colonias.Where(i => i.cd_estado == id_estado && i.cd_municipio == id_municipio && i.cd_colonia == id_colonia).Select("new(nu_cp)");
            return new JsonResult { Data = localizacion, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public void EliminaNaves(int? id)
        {
            try
            {
                var t5 = db.tsg025_ni_contacto.First(i => i.cd_nave == id && i.st_contacto == 3);
                if (t5 != null)
                {
                    db.tsg025_ni_contacto.Remove(t5);
                    db.SaveChanges();
                }

                var t4 = db.tsg025_ni_contacto.First(i => i.cd_nave == id && i.st_contacto == 2);
                if (t4 != null)
                {
                    db.tsg025_ni_contacto.Remove(t4);
                    db.SaveChanges();
                }

                var t3 = db.tsg025_ni_contacto.First(i => i.cd_nave == id && i.st_contacto == 1);
                if (t3 != null)
                {
                    db.tsg025_ni_contacto.Remove(t3);
                    db.SaveChanges();
                }

                var t2 = db.tsg009_ni_dt_gral.First(i => i.cd_nave == id);
                if (t2 != null)
                {
                    db.tsg009_ni_dt_gral.Remove(t2);
                    db.SaveChanges();
                }

                var t1 = db.tsg020_ni_servicio.First(i => i.cd_nave == id);
                if (t1 != null)
                {
                    db.tsg020_ni_servicio.Remove(t1);
                    db.SaveChanges();
                }

                var t0 = db.tsg023_ni_precio.First(i => i.cd_nave == id);
                if (t0 != null)
                {
                    db.tsg023_ni_precio.Remove(t0);
                    db.SaveChanges();
                }

                var t = db.tsg002_nave_industrial.First(i => i.cd_nave == id);
                if (t != null)
                {
                    db.tsg002_nave_industrial.Remove(t);
                    db.SaveChanges();
                }
               tsg029_bitacora_mov.GuardaBitacora("tsg002_nave_industrial", "cd_nave", t.cd_nave, "Eliminar");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
            }
        }


        private void actualizaNaves(clsNaves nave, int? id)
        {
            try
            {               
                nave.tsg002_nave_industrial.fh_modif = DateTime.Now;
                nave.tsg020_ni_servicio.fh_modif = DateTime.Now;
                nave.tsg025_ni_contacto_A.fh_modif = DateTime.Now;
                nave.tsg025_ni_contacto_C.fh_modif = DateTime.Now;
                nave.tsg025_ni_contacto_P.fh_modif = DateTime.Now;
                nave.tsg023_ni_precio.fh_modif = DateTime.Now;

                //tsg001_terreno t = db.tsg001_terreno.First(i => i.cd_terreno == id);
                var t = db.tsg002_nave_industrial.Find(id);
                if (t != null)
                {
                    nave.tsg002_nave_industrial.cd_nave = t.cd_nave; // seteo el mismo numero de nave
                    db.Entry(t).CurrentValues.SetValues(nave.tsg002_nave_industrial);
                    db.SaveChanges();

                    var t0 = db.tsg023_ni_precio.First(i => i.cd_nave == id);
                    if (t0 != null)
                    {
                        nave.tsg023_ni_precio.cd_ni_precio = t0.cd_ni_precio;
                        nave.tsg023_ni_precio.cd_nave = id;

                        db.Entry(t0).CurrentValues.SetValues(nave.tsg023_ni_precio);
                        db.SaveChanges();
                    }

                    var t1 = db.tsg020_ni_servicio.First(i => i.cd_nave == id);
                    if (t1 != null)
                    {
                        nave.tsg020_ni_servicio.cd_servicio = t1.cd_servicio;
                        nave.tsg020_ni_servicio.cd_nave = t1.cd_nave;

                        db.Entry(t1).CurrentValues.SetValues(nave.tsg020_ni_servicio);
                        db.SaveChanges();
                    }

                    var t2 = db.tsg009_ni_dt_gral.First(i => i.cd_nave == id);
                    if (t2 != null)
                    {
                        nave.tsg009_ni_dt_gral.cd_id = t2.cd_id;
                        nave.tsg009_ni_dt_gral.cd_nave = t2.cd_nave;
                        db.Entry(t2).CurrentValues.SetValues(nave.tsg009_ni_dt_gral);
                        db.SaveChanges();
                    }

                    var t3 = db.tsg025_ni_contacto.First(i => i.cd_nave == id && i.st_contacto == 1);
                    if (t3 != null)
                    {
                        nave.tsg025_ni_contacto_P.cd_contacto = t3.cd_contacto;
                        nave.tsg025_ni_contacto_P.cd_nave = t3.cd_nave;
                        nave.tsg025_ni_contacto_P.st_contacto = t3.st_contacto;
                        db.Entry(t3).CurrentValues.SetValues(nave.tsg025_ni_contacto_P);
                        db.SaveChanges();
                    }

                    var t4 = db.tsg025_ni_contacto.First(i => i.cd_nave == id && i.st_contacto == 2);
                    if (t4 != null)
                    {
                        nave.tsg025_ni_contacto_A.cd_contacto = t4.cd_contacto;
                        nave.tsg025_ni_contacto_A.cd_nave = t4.cd_nave;
                        nave.tsg025_ni_contacto_A.st_contacto = t4.st_contacto;
                        db.Entry(t4).CurrentValues.SetValues(nave.tsg025_ni_contacto_A);
                        db.SaveChanges();
                    }

                    var t5 = db.tsg025_ni_contacto.First(i => i.cd_nave == id && i.st_contacto == 3);
                    if (t5 != null)
                    {
                        nave.tsg025_ni_contacto_C.cd_contacto = t5.cd_contacto;
                        nave.tsg025_ni_contacto_C.cd_nave = t5.cd_nave;
                        nave.tsg025_ni_contacto_C.st_contacto = t5.st_contacto;
                        db.Entry(t5).CurrentValues.SetValues(nave.tsg025_ni_contacto_C);
                        db.SaveChanges();
                    }

                }
                tsg029_bitacora_mov.GuardaBitacora("tsg002_nave_industrial", "cd_nave", t.cd_nave, "Modificar");                
                //t.nb_comercial = terreno.tsg001_terreno.nb_comercial;
                //db.Entry(t).CurrentValues.SetValues(terreno.tsg001_terreno);                
                //db.SaveChanges();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
            }
        }

        private void guardarNaves(clsNaves nave)
        {

            nave.tsg002_nave_industrial.fh_modif = DateTime.Now;
            nave.tsg020_ni_servicio.fh_modif = DateTime.Now;
            nave.tsg025_ni_contacto_A.fh_modif = DateTime.Now;
            nave.tsg025_ni_contacto_C.fh_modif = DateTime.Now;
            nave.tsg025_ni_contacto_P.fh_modif = DateTime.Now;
            nave.tsg023_ni_precio.fh_modif = DateTime.Now;


            // terreno.tsg027_te_servicio.cd_terreno = terreno.tsg001_terreno.cd_terreno;                
            db.tsg002_nave_industrial.Add(nave.tsg002_nave_industrial);
            db.SaveChanges();

            int idValor = db.tsg002_nave_industrial.Max(x => x.cd_nave);
            nave.tsg020_ni_servicio.cd_nave = idValor;
            db.tsg020_ni_servicio.Add(nave.tsg020_ni_servicio);
            db.SaveChanges();

            nave.tsg009_ni_dt_gral.cd_nave = idValor;
            nave.tsg009_ni_dt_gral.fh_modif = DateTime.Now;
            db.tsg009_ni_dt_gral.Add(nave.tsg009_ni_dt_gral);
            db.SaveChanges();

            nave.tsg023_ni_precio.cd_nave = idValor;
            db.tsg023_ni_precio.Add(nave.tsg023_ni_precio);
            db.SaveChanges();

            nave.tsg025_ni_contacto_P.cd_nave = idValor;
            nave.tsg025_ni_contacto_P.st_contacto = 1;
            db.tsg025_ni_contacto.Add(nave.tsg025_ni_contacto_P);
            db.SaveChanges();

            nave.tsg025_ni_contacto_A.cd_nave = idValor;
            nave.tsg025_ni_contacto_A.st_contacto = 2;
            db.tsg025_ni_contacto.Add(nave.tsg025_ni_contacto_A);
            db.SaveChanges();

            nave.tsg025_ni_contacto_C.cd_nave = idValor;
            nave.tsg025_ni_contacto_C.st_contacto = 3;
            db.tsg025_ni_contacto.Add(nave.tsg025_ni_contacto_C);
            db.SaveChanges();

            tsg029_bitacora_mov.GuardaBitacora("tsg002_nave_industrial", "cd_nave", idValor, "Crear");      
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

        public ActionResult GeneraPdfSum(int cd_estado, int cd_municipio, int cd_colonia)
        {
            UploadFilesResult r = new UploadFilesResult();

            int estado = cd_estado;
            int municipio = cd_municipio;
            int colonia = cd_colonia;

            r.Name = "";
            r.Type = "";
            r.Length = 0;

            Document documento = new Document(iTextSharp.text.PageSize.LETTER.Rotate(), 5, 5, 5, 5);
            BaseFont Vn_Helvetica = BaseFont.CreateFont(Server.MapPath("~/fonts/" + "arial.ttf"), "Identity-H", BaseFont.EMBEDDED);

            PdfWriter writer = PdfWriter.GetInstance(documento, new FileStream(Server.MapPath("~/image/" + "Naves_rpt_sum.pdf"), FileMode.Create));
            documento.Open();

            PdfPTable tableTitulo = new PdfPTable(1);
            tableTitulo.TotalWidth = 200f;
            tableTitulo.HorizontalAlignment = 50;
            float[] widths = new float[] { 140f };
            tableTitulo.SetWidths(widths);
            tableTitulo.LockedWidth = true;
            PdfPCell titulo = new PdfPCell(new Phrase("Survery Summary", new Font(Vn_Helvetica, 24, Font.NORMAL, BaseColor.WHITE)));

            titulo.BackgroundColor = new BaseColor(128, 128, 128);
            titulo.BorderColor = new BaseColor(128, 128, 128);
            titulo.HorizontalAlignment = Element.ALIGN_LEFT;
            tableTitulo.AddCell(titulo);
            documento.Add(tableTitulo);

            //Paragraph titulo = new Paragraph("Survery Summary", new iTextSharp.text.Font(Vn_Helvetica, 18f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK));
            //documento.Add(titulo);

            Paragraph subtitulo = new Paragraph("Overview", new iTextSharp.text.Font(Vn_Helvetica, 12f, iTextSharp.text.Font.NORMAL, new BaseColor(63, 131, 204)));
            documento.Add(subtitulo);

            Paragraph p1 = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(1.5F, 100.0F, new BaseColor(14, 103, 161), Element.ALIGN_LEFT, 1)));
            documento.Add(p1);

            PdfContentByte cb = writer.DirectContent;
            cb.BeginText();
            BaseFont f_cn = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetFontAndSize(f_cn, 10);
            cb.SetColorFill(new BaseColor(14, 103, 161));
            cb.SetTextMatrix(5, 35);  //(xPos, yPos)
            cb.ShowText("____________________________________________________________________________________________________________________________________________");
            cb.SetColorFill(new BaseColor(63, 131, 204));
            cb.SetTextMatrix(5, 15);  //(xPos, yPos)
            cb.ShowText("Cushman & Wakefield México");
            cb.EndText();

            Paragraph espacio = new Paragraph("\n", new iTextSharp.text.Font(Vn_Helvetica, 12f, iTextSharp.text.Font.NORMAL, new BaseColor(63, 131, 204)));
            documento.Add(espacio);
            PdfPTable table = new PdfPTable(10);
            table.WidthPercentage = 95;

            //
            PdfPCell cell = new PdfPCell(new Phrase(" ", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
            cell.BackgroundColor = new BaseColor(70, 130, 180);
            cell.BorderColor = new BaseColor(255, 255, 255);
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Project/Industrial", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
            cell.BackgroundColor = new BaseColor(70, 130, 180);
            cell.BorderColor = new BaseColor(255, 255, 255);
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Total Leasable Area (sqMt)(SqFt)", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
            cell.BackgroundColor = new BaseColor(70, 130, 180);
            cell.BorderColor = new BaseColor(255, 255, 255);

            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Minimun Divisible Area", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
            cell.BackgroundColor = new BaseColor(70, 130, 180);
            cell.BorderColor = new BaseColor(255, 255, 255);

            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Rent + NNN(USD)($/SqFt/year)", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
            cell.BackgroundColor = new BaseColor(70, 130, 180);
            cell.BorderColor = new BaseColor(255, 255, 255);

            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Office % of Total Area", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
            cell.BackgroundColor = new BaseColor(70, 130, 180);
            cell.BorderColor = new BaseColor(255, 255, 255);

            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Clear Height(Mt)(Ft)", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
            cell.BackgroundColor = new BaseColor(70, 130, 180);
            cell.BorderColor = new BaseColor(255, 255, 255);

            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Sky-light (% of roof)", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
            cell.BackgroundColor = new BaseColor(70, 130, 180);
            cell.BorderColor = new BaseColor(255, 255, 255);

            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Fire Protection System", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
            cell.BackgroundColor = new BaseColor(70, 130, 180);
            cell.BorderColor = new BaseColor(255, 255, 255);

            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Timing/Comments", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
            cell.BackgroundColor = new BaseColor(70, 130, 180);
            cell.BorderColor = new BaseColor(255, 255, 255);

            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);


            var query = from n in db.tsg002_nave_industrial
                        join g in db.tsg009_ni_dt_gral on n.cd_nave equals g.cd_nave
                        join p in db.tsg023_ni_precio on n.cd_nave equals p.cd_nave into tmpP
                        join a in db.tsg010_area_of on g.cd_area equals a.cd_area into tmpA
                        
                        join s in db.tsg012_sist_incendio on g.cd_sist_inc equals s.cd_sist_inc into tmpS
                        join w in db.tsg018_ilum_nat on g.cd_ilum_nat equals w.cd_ilum_nat into tmpW
                        from p in tmpP.DefaultIfEmpty()
                        from a in tmpA.DefaultIfEmpty()
                        from s in tmpS.DefaultIfEmpty()
                        from w in tmpW.DefaultIfEmpty()
                        where n.cd_nave != 1 && g.nu_disponibilidad > 0 && n.cd_estado == estado
                        select new
                        {    //nb_mercado = tsg007 != null ? tsg007.nb_mercado : "N/A",
                             cd_nave    =   n != null ? n.cd_nave:0 ,
                             nb_parque  =   n != null ? n.nb_parque:" ",
                             nb_nave    =   n != null ? n.nb_nave:" ",
                             n_ilum_nat =   g != null ? g.n_ilum_nat : " ",
                             nb_area = g != null ? g.nb_area : " ",
                             nb_sist_inc = g != null ? g.nb_sist_inc : " ",

                             nu_disponibilidad  = g != null ? g.nu_disponibilidad:0,
                             nu_min_divisible   = g != null ? g.nu_min_divisible:0,
                             im_renta           = p != null ? p.im_renta:0,
                             nb_area_            = a != null ? a.nb_area:" ",
                             nu_altura          = g != null ? g.nu_altura:0,                                                         
                             nb_ilum_nat        =  w != null ? w.nb_ilum_nat : " ",
                             
                             nb_sist_inc_ = s != null ? s.nb_sist_inc:" ",
                             cd_estado        = n != null ? n.cd_estado:0,
                             cd_municipio     = n != null ? n.cd_municipio : 0,
                             cd_colonia       = n != null ? n.cd_colonia : 0,
                             cd_ilum_nat      = g != null ? g.cd_ilum_nat : 0,
                             cd_area          = a != null ? a.cd_area : 0,
                             cd_sist_inc      = s != null ? s.cd_sist_inc : 0,
                        };

            if (municipio > 0)
            {
                query = query.Where(t => t.cd_municipio == municipio);
            }

            if (colonia > 0)
            {
                query = query.Where(t => t.cd_colonia == colonia);
            }

            int i = 1;
            foreach (var group in query)
            {
                //Console.WriteLine("{0} bought {1}", group.Name, group.Product);
                if ((i + 1) % 2 == 0)
                {
                    PdfPCell row = new PdfPCell(new Phrase(group.cd_nave.ToString(), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                    row.BackgroundColor = new BaseColor(159, 181, 180);
                    row.BorderColor = new BaseColor(255, 255, 255);
                    table.AddCell(row);
                    row = new PdfPCell(new Phrase(group.nb_parque + " " + group.nb_nave, new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                    row.BackgroundColor = new BaseColor(159, 181, 180);
                    row.BorderColor = new BaseColor(255, 255, 255);
                    table.AddCell(row);
                    row = new PdfPCell(new Phrase(group.nu_disponibilidad.ToString(), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                    row.BackgroundColor = new BaseColor(159, 181, 180);
                    row.BorderColor = new BaseColor(255, 255, 255);
                    table.AddCell(row);
                    row = new PdfPCell(new Phrase(group.nu_min_divisible.ToString(), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                    row.BackgroundColor = new BaseColor(159, 181, 180);
                    row.BorderColor = new BaseColor(255, 255, 255);
                    table.AddCell(row);
                    row = new PdfPCell(new Phrase(group.im_renta.ToString(), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                    row.BackgroundColor = new BaseColor(159, 181, 180);
                    row.BorderColor = new BaseColor(255, 255, 255);
                    table.AddCell(row);

                    if (group.cd_area == 8) //este es el caso de otro /other
                    {
                        row = new PdfPCell(new Phrase((group.nb_area != null? group.nb_area.ToString():""), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                        row.BackgroundColor = new BaseColor(159, 181, 180);
                        row.BorderColor = new BaseColor(255, 255, 255);
                        table.AddCell(row);
                    }
                    else
                    {
                        row = new PdfPCell(new Phrase(group.nb_area_.ToString(), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                        row.BackgroundColor = new BaseColor(159, 181, 180);
                        row.BorderColor = new BaseColor(255, 255, 255);
                        table.AddCell(row);
                    }



                    row = new PdfPCell(new Phrase(group.nu_altura.ToString(), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                    row.BackgroundColor = new BaseColor(159, 181, 180);
                    row.BorderColor = new BaseColor(255, 255, 255);
                    table.AddCell(row);
                    //AGGH 15092018
                    if (group.cd_ilum_nat == 8) //este es el caso de otro /other
                    {
                        row = new PdfPCell(new Phrase(group.n_ilum_nat, new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                        row.BackgroundColor = new BaseColor(159, 181, 180);
                        row.BorderColor = new BaseColor(255, 255, 255);
                        table.AddCell(row);
                    }
                    else
                    {
                        
                        row = new PdfPCell(new Phrase(group.nb_ilum_nat, new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                        row.BackgroundColor = new BaseColor(159, 181, 180);
                        row.BorderColor = new BaseColor(255, 255, 255);
                        table.AddCell(row);
                    }


                    if (group.cd_sist_inc == 5) //este es el caso de otro /other
                    {
                        row = new PdfPCell(new Phrase((group.nb_sist_inc != null ? group.nb_sist_inc.ToString() : ""), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                        row.BackgroundColor = new BaseColor(159, 181, 180);
                        row.BorderColor = new BaseColor(255, 255, 255);
                        table.AddCell(row);
                    }
                    else
                    {
                        row = new PdfPCell(new Phrase((group.nb_sist_inc != null ? group.nb_sist_inc.ToString() : ""), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                        row.BackgroundColor = new BaseColor(159, 181, 180);
                        row.BorderColor = new BaseColor(255, 255, 255);
                        table.AddCell(row);
                    }



                    row = new PdfPCell(new Phrase("", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                    row.BackgroundColor = new BaseColor(159, 181, 180);
                    row.BorderColor = new BaseColor(255, 255, 255);
                    table.AddCell(row);
                }
                else
                {
                    PdfPCell row = new PdfPCell(new Phrase(group.cd_nave.ToString(), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                    row.BackgroundColor = new BaseColor(224, 224, 224);
                    row.BorderColor = new BaseColor(255, 255, 255);
                    table.AddCell(row);

                    row = new PdfPCell(new Phrase(group.nb_parque + " " + group.nb_nave, new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                    row.BackgroundColor = new BaseColor(224, 224, 224);
                    row.BorderColor = new BaseColor(255, 255, 255);
                    table.AddCell(row);

                    row = new PdfPCell(new Phrase(group.nu_disponibilidad.ToString(), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                    row.BackgroundColor = new BaseColor(224, 224, 224);
                    row.BorderColor = new BaseColor(255, 255, 255);
                    table.AddCell(row);

                    row = new PdfPCell(new Phrase(group.nu_min_divisible.ToString(), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                    row.BackgroundColor = new BaseColor(224, 224, 224);
                    row.BorderColor = new BaseColor(255, 255, 255);
                    table.AddCell(row);

                    row = new PdfPCell(new Phrase(group.im_renta.ToString(), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                    row.BackgroundColor = new BaseColor(224, 224, 224);
                    row.BorderColor = new BaseColor(255, 255, 255);
                    table.AddCell(row);

                    if (group.cd_area == 8)
                    {
                        row = new PdfPCell(new Phrase(group.nb_area.ToString(), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                        row.BackgroundColor = new BaseColor(159, 181, 180);
                        row.BorderColor = new BaseColor(255, 255, 255);
                        table.AddCell(row);
                    }
                    else
                    {
                        row = new PdfPCell(new Phrase(group.nb_area_.ToString(), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                        row.BackgroundColor = new BaseColor(159, 181, 180);
                        row.BorderColor = new BaseColor(255, 255, 255);
                        table.AddCell(row);
                    }

                    row = new PdfPCell(new Phrase(group.nu_altura.ToString(), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                    row.BackgroundColor = new BaseColor(224, 224, 224);
                    row.BorderColor = new BaseColor(255, 255, 255);
                    table.AddCell(row);

                    //AGGH 15092018
                    if (group.cd_ilum_nat == 8)
                    {
                        row = new PdfPCell(new Phrase(group.n_ilum_nat, new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                        row.BackgroundColor = new BaseColor(159, 181, 180);
                        row.BorderColor = new BaseColor(255, 255, 255);
                        table.AddCell(row);
                    }
                    else
                    {

                        row = new PdfPCell(new Phrase(group.nb_ilum_nat, new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                        row.BackgroundColor = new BaseColor(159, 181, 180);
                        row.BorderColor = new BaseColor(255, 255, 255);
                        table.AddCell(row);
                    }

                    if (group.cd_sist_inc == 5) //este es el caso de otro /other
                    {
                        row = new PdfPCell(new Phrase((group.nb_sist_inc != null ? group.nb_sist_inc.ToString() : ""), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                        row.BackgroundColor = new BaseColor(159, 181, 180);
                        row.BorderColor = new BaseColor(255, 255, 255);
                        table.AddCell(row);
                    }
                    else
                    {
                        row = new PdfPCell(new Phrase((group.nb_sist_inc != null ? group.nb_sist_inc.ToString() : ""), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                        row.BackgroundColor = new BaseColor(159, 181, 180);
                        row.BorderColor = new BaseColor(255, 255, 255);
                        table.AddCell(row);
                    }

                    row = new PdfPCell(new Phrase("", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                    row.BackgroundColor = new BaseColor(224, 224, 224);
                    row.BorderColor = new BaseColor(255, 255, 255);
                    table.AddCell(row);
                }

                if ((i + 1) % 7 == 0)
                {
                    documento.Add(table);
                    table = new PdfPTable(10);
                    table.WidthPercentage = 95;
                    //Paragraph p3 = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(3.0F, 100.0F, new BaseColor(14, 103, 161), Element.ALIGN_LEFT, 1)));
                    //documento.Add(p3);

                    //Paragraph pie2 = new Paragraph("Cushman & Wakefield México", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, new BaseColor(63, 131, 204)));
                    //documento.Add(pie2);


                    //PdfContentByte cb = writer.DirectContent;
                    //cb.BeginText();
                    //BaseFont f_cn = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    //cb.SetFontAndSize(f_cn, 10); 
                    //cb.SetColorFill(new BaseColor(14, 103, 161));
                    //cb.SetTextMatrix(5, 35);  //(xPos, yPos)
                    //cb.ShowText("____________________________________________________________________________________________________________________________________________");
                    //cb.SetColorFill(new BaseColor(63, 131, 204));
                    //cb.SetTextMatrix(5, 15);  //(xPos, yPos)
                    //cb.ShowText("Cushman & Wakefield México");
                    //cb.EndText();

                    //PdfContentByte cb1 = writer.DirectContent;
                    //cb1.BeginText();
                    //cb1.SetFontAndSize(f_cn, 10);
                    //cb1.SetTextMatrix(15, 15);  //(xPos, yPos)
                    //cb1.ShowText("Cushman & Wakefield México");
                    //cb1.EndText();


                    //documento.Add(table);
                    documento.NewPage();
                    tableTitulo = new PdfPTable(1);
                    tableTitulo.TotalWidth = 200f;
                    tableTitulo.HorizontalAlignment = 50;
                    widths = new float[] { 140f };
                    tableTitulo.SetWidths(widths);
                    tableTitulo.LockedWidth = true;
                    titulo = new PdfPCell(new Phrase("Survery Summary", new Font(Vn_Helvetica, 24, Font.NORMAL, BaseColor.WHITE)));
                    titulo.BackgroundColor = new BaseColor(128, 128, 128);
                    titulo.BorderColor = new BaseColor(128, 128, 128);
                    titulo.HorizontalAlignment = Element.ALIGN_LEFT;
                    tableTitulo.AddCell(titulo);
                    documento.Add(tableTitulo);

                    Paragraph subtitulo1 = new Paragraph("Overview", new iTextSharp.text.Font(Vn_Helvetica, 12f, iTextSharp.text.Font.NORMAL, new BaseColor(63, 131, 204)));
                    documento.Add(subtitulo1);

                    Paragraph p4 = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(1.5F, 100.0F, new BaseColor(14, 103, 161), Element.ALIGN_LEFT, 1)));
                    documento.Add(p4);

                    espacio = new Paragraph("\n", new iTextSharp.text.Font(Vn_Helvetica, 12f, iTextSharp.text.Font.NORMAL, new BaseColor(63, 131, 204)));
                    documento.Add(espacio);


                    PdfContentByte cb2 = writer.DirectContent;
                    cb2.BeginText();
                    BaseFont f_cn2 = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    cb2.SetFontAndSize(f_cn, 10);
                    cb2.SetColorFill(new BaseColor(14, 103, 161));
                    cb2.SetTextMatrix(5, 35);  //(xPos, yPos)
                    cb2.ShowText("____________________________________________________________________________________________________________________________________________");
                    cb2.SetColorFill(new BaseColor(63, 131, 204));
                    cb2.SetTextMatrix(5, 15);  //(xPos, yPos)
                    cb2.ShowText("Cushman & Wakefield México");
                    cb2.EndText();


                    cell = new PdfPCell(new Phrase(" ", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
                    cell.BackgroundColor = new BaseColor(70, 130, 180);
                    cell.BorderColor = new BaseColor(255, 255, 255);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Project/Industrial", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
                    cell.BackgroundColor = new BaseColor(70, 130, 180);
                    cell.BorderColor = new BaseColor(255, 255, 255);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Total Leasable Area (sqMt)(SqFt)", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
                    cell.BackgroundColor = new BaseColor(70, 130, 180);
                    cell.BorderColor = new BaseColor(255, 255, 255);

                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Minimun Divisible Area", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
                    cell.BackgroundColor = new BaseColor(70, 130, 180);
                    cell.BorderColor = new BaseColor(255, 255, 255);

                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Rent + NNN(USD)($/SqFt/year)", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
                    cell.BackgroundColor = new BaseColor(70, 130, 180);
                    cell.BorderColor = new BaseColor(255, 255, 255);

                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Office % of Total Area", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
                    cell.BackgroundColor = new BaseColor(70, 130, 180);
                    cell.BorderColor = new BaseColor(255, 255, 255);

                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Clear Height(Mt)(Ft)", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
                    cell.BackgroundColor = new BaseColor(70, 130, 180);
                    cell.BorderColor = new BaseColor(255, 255, 255);

                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Sky-light (% of roof)", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
                    cell.BackgroundColor = new BaseColor(70, 130, 180);
                    cell.BorderColor = new BaseColor(255, 255, 255);

                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Fire Protection System", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
                    cell.BackgroundColor = new BaseColor(70, 130, 180);
                    cell.BorderColor = new BaseColor(255, 255, 255);

                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Timing/Comments", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
                    cell.BackgroundColor = new BaseColor(70, 130, 180);
                    cell.BorderColor = new BaseColor(255, 255, 255);

                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);


                }
                i++;

            }

            Paragraph p2 = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(3.0F, 100.0F, new BaseColor(14, 103, 161), Element.ALIGN_LEFT, 1)));
            Paragraph pie = new Paragraph("Cushman & Wakefield México", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, new BaseColor(63, 131, 204)));
            if (i % 7 != 0)
            {
                documento.Add(table);

                //    Paragraph p2 = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(3.0F, 100.0F, new BaseColor(14, 103, 161), Element.ALIGN_LEFT, 1)));
                //    documento.Add(p2);

                //    documento.Add(pie);
            }


            // parte en español
            documento.NewPage();

            documento.Add(tableTitulo);
            documento.Add(subtitulo);
            documento.Add(p1);

            documento.Add(espacio);

            table = new PdfPTable(10);
            table.WidthPercentage = 95;
            //

            cell = new PdfPCell(new Phrase(" ", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
            cell.BackgroundColor = new BaseColor(70, 130, 180);
            cell.BorderColor = new BaseColor(70, 130, 180);
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Nombre Parque / Nombre Nave", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
            cell.BackgroundColor = new BaseColor(70, 130, 180);
            cell.BorderColor = new BaseColor(70, 130, 180);
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Disponibilidad Total (sqMt)(SqFt)", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
            cell.BackgroundColor = new BaseColor(70, 130, 180);
            cell.BorderColor = new BaseColor(70, 130, 180);

            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Minimo divisible m2", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
            cell.BackgroundColor = new BaseColor(70, 130, 180);
            cell.BorderColor = new BaseColor(70, 130, 180);

            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Precio de Renta y Mantenimiento Areas Comunes(USD)($/SqFt/year)", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
            cell.BackgroundColor = new BaseColor(70, 130, 180);
            cell.BorderColor = new BaseColor(70, 130, 180);

            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Area Oficina (% del Area Total)", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
            cell.BackgroundColor = new BaseColor(70, 130, 180);
            cell.BorderColor = new BaseColor(70, 130, 180);

            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Altura Libre(Mt)(Ft)", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
            cell.BackgroundColor = new BaseColor(70, 130, 180);
            cell.BorderColor = new BaseColor(70, 130, 180);

            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Iluminación Natural (% of roof)", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
            cell.BackgroundColor = new BaseColor(70, 130, 180);
            cell.BorderColor = new BaseColor(70, 130, 180);

            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Sistemas contra incendio", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
            cell.BackgroundColor = new BaseColor(70, 130, 180);
            cell.BorderColor = new BaseColor(70, 130, 180);

            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Tiempos / Comentarios", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
            cell.BackgroundColor = new BaseColor(70, 130, 180);
            cell.BorderColor = new BaseColor(70, 130, 180);

            cell.HorizontalAlignment = Element.ALIGN_CENTER;

            table.AddCell(cell);



            //var query2 = from n in db.tsg002_nave_industrial
            //             join g in db.tsg009_ni_dt_gral on n.cd_nave equals g.cd_nave
            //             join p in db.tsg023_ni_precio on n.cd_nave equals p.cd_nave
            //             join a in db.tsg010_area_of on g.cd_area equals a.cd_area
            //             join s in db.tsg012_sist_incendio on g.cd_sist_inc equals s.cd_sist_inc
            //             where n.cd_nave != 1 && g.nu_disponibilidad > 0 && n.cd_estado == estado
            //             select new { n.cd_nave, n.nb_parque, n.nb_nave, g.nu_disponibilidad, g.nu_min_divisible, p.im_renta, a.nb_area, g.nu_altura, g.nb_tp_lampara, s.nb_sist_inc, n.cd_estado, n.cd_municipio, n.cd_colonia };

            var query2 = from n in db.tsg002_nave_industrial
                        join g in db.tsg009_ni_dt_gral on n.cd_nave equals g.cd_nave
                        join p in db.tsg023_ni_precio on n.cd_nave equals p.cd_nave into tmpP
                        join a in db.tsg010_area_of on g.cd_area equals a.cd_area into tmpA

                        join s in db.tsg012_sist_incendio on g.cd_sist_inc equals s.cd_sist_inc into tmpS
                        join w in db.tsg018_ilum_nat on g.cd_ilum_nat equals w.cd_ilum_nat into tmpW
                        from p in tmpP.DefaultIfEmpty()
                        from a in tmpA.DefaultIfEmpty()
                        from s in tmpS.DefaultIfEmpty()
                        from w in tmpW.DefaultIfEmpty()
                        where n.cd_nave != 1 && g.nu_disponibilidad > 0 && n.cd_estado == estado
                        select new
                        {    //nb_mercado = tsg007 != null ? tsg007.nb_mercado : "N/A",
                            cd_nave = n != null ? n.cd_nave : 0,
                            nb_parque = n != null ? n.nb_parque : " ",
                            nb_nave = n != null ? n.nb_nave : " ",
                            n_ilum_nat = g != null ? g.n_ilum_nat : " ",
                            nb_area = g != null ? g.nb_area : " ",
                            nb_sist_inc = g != null ? g.nb_sist_inc : " ",

                            nu_disponibilidad = g != null ? g.nu_disponibilidad : 0,
                            nu_min_divisible = g != null ? g.nu_min_divisible : 0,
                            im_renta = p != null ? p.im_renta : 0,
                            nb_area_ = a != null ? a.nb_area : " ",
                            nu_altura = g != null ? g.nu_altura : 0,
                            nb_ilum_nat = w != null ? w.nb_ilum_nat : " ",

                            nb_sist_inc_ = s != null ? s.nb_sist_inc : " ",
                            cd_estado = n != null ? n.cd_estado : 0,
                            cd_municipio = n != null ? n.cd_municipio : 0,
                            cd_colonia = n != null ? n.cd_colonia : 0,
                            cd_ilum_nat = g != null ? g.cd_ilum_nat : 0,
                            cd_area = a != null ? a.cd_area : 0,
                            cd_sist_inc = s != null ? s.cd_sist_inc : 0,
                        };

            if (municipio > 0)
            {
                query2 = query2.Where(t => t.cd_municipio == municipio);
            }

            if (colonia > 0)
            {
                query2 = query2.Where(t => t.cd_colonia == colonia);
            }

            int j = 1;
            foreach (var group in query2)
            {
                //Console.WriteLine("{0} bought {1}", group.Name, group.Product);
                if ((j + 1) % 2 == 0)
                {
                    PdfPCell row = new PdfPCell(new Phrase(group.cd_nave.ToString(), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                    row.BackgroundColor = new BaseColor(159, 181, 180);
                    row.BorderColor = new BaseColor(255, 255, 255);
                    table.AddCell(row);
                    row = new PdfPCell(new Phrase(group.nb_parque + " " + group.nb_nave, new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                    row.BackgroundColor = new BaseColor(159, 181, 180);
                    row.BorderColor = new BaseColor(255, 255, 255);
                    table.AddCell(row);
                    row = new PdfPCell(new Phrase(group.nu_disponibilidad.ToString(), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                    row.BackgroundColor = new BaseColor(159, 181, 180);
                    row.BorderColor = new BaseColor(255, 255, 255);
                    table.AddCell(row);
                    row = new PdfPCell(new Phrase(group.nu_min_divisible.ToString(), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                    row.BackgroundColor = new BaseColor(159, 181, 180);
                    row.BorderColor = new BaseColor(255, 255, 255);
                    table.AddCell(row);
                    row = new PdfPCell(new Phrase(group.im_renta.ToString(), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                    row.BackgroundColor = new BaseColor(159, 181, 180);
                    row.BorderColor = new BaseColor(255, 255, 255);
                    table.AddCell(row);
                    if (group.cd_area == 8) //este es el caso de otro /other
                    {
                        row = new PdfPCell(new Phrase((group.nb_area != null ? group.nb_area.ToString() : ""), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                        row.BackgroundColor = new BaseColor(159, 181, 180);
                        row.BorderColor = new BaseColor(255, 255, 255);
                        table.AddCell(row);
                    }
                    else
                    {
                        row = new PdfPCell(new Phrase((group.nb_area != null ? group.nb_area.ToString() : ""), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                        row.BackgroundColor = new BaseColor(159, 181, 180);
                        row.BorderColor = new BaseColor(255, 255, 255);
                        table.AddCell(row);
                    }
                    row = new PdfPCell(new Phrase(group.nu_altura.ToString(), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                    row.BackgroundColor = new BaseColor(159, 181, 180);
                    row.BorderColor = new BaseColor(255, 255, 255);
                    table.AddCell(row);

                    if (group.cd_ilum_nat == 8) //este es el caso de otro /other
                    {
                        row = new PdfPCell(new Phrase(group.n_ilum_nat, new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                        row.BackgroundColor = new BaseColor(159, 181, 180);
                        row.BorderColor = new BaseColor(255, 255, 255);
                        table.AddCell(row);
                    }
                    else
                    {
                        row = new PdfPCell(new Phrase(group.nb_ilum_nat, new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                        row.BackgroundColor = new BaseColor(159, 181, 180);
                        row.BorderColor = new BaseColor(255, 255, 255);
                        table.AddCell(row);
                    }

                    if (group.cd_sist_inc == 5) //este es el caso de otro /other
                    {
                        row = new PdfPCell(new Phrase((group.nb_sist_inc != null ? group.nb_sist_inc.ToString() : ""), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                        row.BackgroundColor = new BaseColor(159, 181, 180);
                        row.BorderColor = new BaseColor(255, 255, 255);
                        table.AddCell(row);
                    }
                    else
                    {
                        row = new PdfPCell(new Phrase((group.nb_sist_inc != null ? group.nb_sist_inc.ToString() : ""), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                        row.BackgroundColor = new BaseColor(159, 181, 180);
                        row.BorderColor = new BaseColor(255, 255, 255);
                        table.AddCell(row);
                    }

                    row = new PdfPCell(new Phrase("", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                    row.BackgroundColor = new BaseColor(159, 181, 180);
                    row.BorderColor = new BaseColor(255, 255, 255);
                    table.AddCell(row);
                }
                else
                {
                    PdfPCell row = new PdfPCell(new Phrase(group.cd_nave.ToString(), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                    row.BackgroundColor = new BaseColor(224, 224, 224);
                    row.BorderColor = new BaseColor(255, 255, 255);
                    table.AddCell(row);

                    row = new PdfPCell(new Phrase(group.nb_parque + " " + group.nb_nave, new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                    row.BackgroundColor = new BaseColor(224, 224, 224);
                    row.BorderColor = new BaseColor(255, 255, 255);
                    table.AddCell(row);

                    row = new PdfPCell(new Phrase(group.nu_disponibilidad.ToString(), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                    row.BackgroundColor = new BaseColor(224, 224, 224);
                    row.BorderColor = new BaseColor(255, 255, 255);
                    table.AddCell(row);

                    row = new PdfPCell(new Phrase(group.nu_min_divisible.ToString(), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                    row.BackgroundColor = new BaseColor(224, 224, 224);
                    row.BorderColor = new BaseColor(255, 255, 255);
                    table.AddCell(row);

                    row = new PdfPCell(new Phrase(group.im_renta.ToString(), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                    row.BackgroundColor = new BaseColor(224, 224, 224);
                    row.BorderColor = new BaseColor(255, 255, 255);
                    table.AddCell(row);

                    if (group.cd_area == 8) //este es el caso de otro /other
                    {
                        row = new PdfPCell(new Phrase((group.nb_area != null ? group.nb_area.ToString() : ""), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                        row.BackgroundColor = new BaseColor(159, 181, 180);
                        row.BorderColor = new BaseColor(255, 255, 255);
                        table.AddCell(row);
                    }
                    else
                    {
                        row = new PdfPCell(new Phrase((group.nb_area != null ? group.nb_area.ToString() : ""), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                        row.BackgroundColor = new BaseColor(159, 181, 180);
                        row.BorderColor = new BaseColor(255, 255, 255);
                        table.AddCell(row);
                    }

                    row = new PdfPCell(new Phrase(group.nu_altura.ToString(), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                    row.BackgroundColor = new BaseColor(224, 224, 224);
                    row.BorderColor = new BaseColor(255, 255, 255);
                    table.AddCell(row);

                    if (group.cd_ilum_nat == 8) //este es el caso de otro /other
                    {
                        row = new PdfPCell(new Phrase(group.n_ilum_nat, new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                        row.BackgroundColor = new BaseColor(159, 181, 180);
                        row.BorderColor = new BaseColor(255, 255, 255);
                        table.AddCell(row);
                    }
                    else
                    {
                        row = new PdfPCell(new Phrase(group.nb_ilum_nat, new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                        row.BackgroundColor = new BaseColor(159, 181, 180);
                        row.BorderColor = new BaseColor(255, 255, 255);
                        table.AddCell(row);
                    }

                    if (group.cd_sist_inc == 5) //este es el caso de otro /other
                    {
                        row = new PdfPCell(new Phrase((group.nb_sist_inc != null ? group.nb_sist_inc.ToString() : ""), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                        row.BackgroundColor = new BaseColor(159, 181, 180);
                        row.BorderColor = new BaseColor(255, 255, 255);
                        table.AddCell(row);
                    }
                    else
                    {
                        row = new PdfPCell(new Phrase((group.nb_sist_inc != null ? group.nb_sist_inc.ToString() : ""), new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                        row.BackgroundColor = new BaseColor(159, 181, 180);
                        row.BorderColor = new BaseColor(255, 255, 255);
                        table.AddCell(row);
                    }

                    row = new PdfPCell(new Phrase("", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.BLACK)));
                    row.BackgroundColor = new BaseColor(224, 224, 224);
                    row.BorderColor = new BaseColor(255, 255, 255);
                    table.AddCell(row);
                }

                if ((j + 1) % 7 == 0)
                {
                    documento.Add(table);
                    table = new PdfPTable(10);
                    table.WidthPercentage = 95;
                    //Paragraph p3 = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(3.0F, 100.0F, new BaseColor(14, 103, 161), Element.ALIGN_LEFT, 1)));
                    //documento.Add(p3);

                    //Paragraph pie2 = new Paragraph("Cushman & Wakefield México", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, new BaseColor(63, 131, 204)));
                    //documento.Add(pie2);
                    //PdfContentByte cb = writer.DirectContent;
                    //cb.BeginText();
                    //BaseFont f_cn = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    //cb.SetFontAndSize(f_cn, 10);
                    //cb.SetColorFill(new BaseColor(14, 103, 161));
                    //cb.SetTextMatrix(5, 35);  //(xPos, yPos)
                    //cb.ShowText("____________________________________________________________________________________________________________________________________________");
                    //cb.SetColorFill(new BaseColor(63, 131, 204));
                    //cb.SetTextMatrix(5, 15);  //(xPos, yPos)
                    //cb.ShowText("Cushman & Wakefield México");
                    //cb.EndText();

                    //documento.Add(table);
                    documento.NewPage();

                    documento.Add(tableTitulo);

                    Paragraph subtitulo1 = new Paragraph("Overview", new iTextSharp.text.Font(Vn_Helvetica, 12f, iTextSharp.text.Font.NORMAL, new BaseColor(63, 131, 204)));
                    documento.Add(subtitulo1);

                    Paragraph p4 = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(1.5F, 100.0F, new BaseColor(14, 103, 161), Element.ALIGN_LEFT, 1)));
                    documento.Add(p4);

                    PdfContentByte cb1 = writer.DirectContent;
                    cb1.BeginText();
                    BaseFont f_cn1 = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    cb1.SetFontAndSize(f_cn, 10);
                    cb1.SetColorFill(new BaseColor(14, 103, 161));
                    cb1.SetTextMatrix(5, 35);  //(xPos, yPos)
                    cb1.ShowText("____________________________________________________________________________________________________________________________________________");
                    cb1.SetColorFill(new BaseColor(63, 131, 204));
                    cb1.SetTextMatrix(5, 15);  //(xPos, yPos)
                    cb1.ShowText("Cushman & Wakefield México");
                    cb1.EndText();


                    documento.Add(espacio);
                    cell = new PdfPCell(new Phrase(" ", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
                    cell.BackgroundColor = new BaseColor(70, 130, 180);
                    cell.BorderColor = new BaseColor(255, 255, 255);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Nombre Parque / Nombre Nave", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
                    cell.BackgroundColor = new BaseColor(70, 130, 180);
                    cell.BorderColor = new BaseColor(255, 255, 255);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Disponibilidad Total (sqMt)(SqFt)", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
                    cell.BackgroundColor = new BaseColor(70, 130, 180);
                    cell.BorderColor = new BaseColor(255, 255, 255);

                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Minimo divisible m2", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
                    cell.BackgroundColor = new BaseColor(70, 130, 180);
                    cell.BorderColor = new BaseColor(255, 255, 255);

                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Precio de Renta y Mantenimiento Areas Comunes(USD)($/SqFt/year)", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
                    cell.BackgroundColor = new BaseColor(70, 130, 180);
                    cell.BorderColor = new BaseColor(255, 255, 255);

                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Area Oficina (% del Area Total)", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
                    cell.BackgroundColor = new BaseColor(70, 130, 180);
                    cell.BorderColor = new BaseColor(255, 255, 255);

                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Altura Libre(Mt)(Ft)", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
                    cell.BackgroundColor = new BaseColor(70, 130, 180);
                    cell.BorderColor = new BaseColor(255, 255, 255);

                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Iluminación Natural (% of roof)", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
                    cell.BackgroundColor = new BaseColor(70, 130, 180);
                    cell.BorderColor = new BaseColor(255, 255, 255);

                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Sistemas contra incendio", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
                    cell.BackgroundColor = new BaseColor(70, 130, 180);
                    cell.BorderColor = new BaseColor(255, 255, 255);

                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Tiempos / Comentarios", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
                    cell.BackgroundColor = new BaseColor(70, 130, 180);
                    cell.BorderColor = new BaseColor(255, 255, 255);

                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);
                }
                j++;

            }

            if (j % 7 != 0)
            {
                documento.Add(table);
                //documento.Add(p2);
                //documento.Add(pie);

                //PdfContentByte cb2 = writer.DirectContent;
                //cb2.BeginText();
                //BaseFont f_cn2 = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                //cb2.SetFontAndSize(f_cn, 10);
                //cb2.SetColorFill(new BaseColor(14, 103, 161));
                //cb2.SetTextMatrix(5, 35);  //(xPos, yPos)
                //cb2.ShowText("____________________________________________________________________________________________________________________________________________");
                //cb2.SetColorFill(new BaseColor(63, 131, 204));
                //cb2.SetTextMatrix(5, 15);  //(xPos, yPos)
                //cb2.ShowText("Cushman & Wakefield México");
                //cb2.EndText();
            }

            documento.Close();
            writer.Close();

            uploadfile1(Server.MapPath("~/image/" + "Naves_rpt_sum.pdf"), "Naves_rpt_sum.pdf");
            string strUrl = descargaBlob2("Naves_rpt_sum.pdf");
            r = new UploadFilesResult();
            r.Name = strUrl;
            r.Type = "";
            r.Length = 0;

            return new JsonResult
            {
                Data = r,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

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
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("Naves_rpt.pptx");

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
            PPTReport.clases.reporte_pp.repPpNaves_ind(id, lng, ruta);
            // reportes.
            uploadfileppt(Server.MapPath("~/repositorio/" + "Naves_rpt.pptx"));

            string strUrl = descargaBlob2("Naves_rpt.pptx");
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
            int result = db.tsg045_imagenes_naves.Where(t => t.cd_nave == id_nave && t.cd_reporte == 1).Count();
            if (result > 0)
            {
                var imagenes = db.tsg045_imagenes_naves.Where(t => t.cd_nave == id_nave && t.cd_reporte == 1).Single();
                if (imagenes != null)
                {
                    string urlImage = descargaBlob(id_nave, imagenes.nb_archivo);
                    string rutaArchivo = util.GeneraImagen(urlImage, "N_" + id_nave, ruta);
                }
            }
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
            ////cookie.Value = strLenguanje;
            ////Response.Cookies.Add(cookie);
            //cookie = Response.Cookies["us"];
            //var leng = cookie.Value;


            var imagenes = db.tsg045_imagenes_naves.Where(i => i.cd_nave == id && i.cd_reporte==1);
            string urlImage = "";
            if (imagenes.Count() != 0)
            {
                //if (imagenes.First().tx_archivo.ToString().Contains("image"))
                //{
                //    urlImage = imagenes.First().tx_archivo;
                //}
                //else
                //{
                //    urlImage = descargaBlob(imagenes.First().nb_archivo);
                //}
                urlImage = descargaBlob(id, imagenes.First().nb_archivo);
            }


            Document documento = new Document(iTextSharp.text.PageSize.LETTER.Rotate(), 20, 20, 30, 35);
            BaseFont Vn_Helvetica = BaseFont.CreateFont(Server.MapPath("~/fonts/" + "arial.ttf"), "Identity-H", BaseFont.EMBEDDED);

            if (lng == 1)
            { // Español                    
                PdfWriter writer = PdfWriter.GetInstance(documento, new FileStream(Server.MapPath("~/image/" + "Naves_rpt.pdf"), FileMode.Create));
                //PdfWriter.GetInstance(documento, new FileStream(@"D:\archivo.pdf",FileMode.Create));            
                var t = db.tsg002_nave_industrial.Find(id);
                if (t != null)
                {
                    try
                    {
                        documento.Open();
                        //documento.Add(new Paragraph(" "));

                        var t0 = db.tsg023_ni_precio.First(i => i.cd_nave == id);
                        if (t0 == null)
                        {
                        }

                        var t1 = db.tsg020_ni_servicio.First(i => i.cd_nave == id);
                        if (t1 == null)
                        {
                        }

                        var t2 = db.tsg009_ni_dt_gral.First(i => i.cd_nave == id);
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
                            preciofinal = Math.Round(p * tp_cmb,2);
                        }
                        else
                        {
                            preciofinal = Math.Round(p / tp_cmb,2);
                        }
                        p = 0;
                        if (t0.im_renta != null)
                        {
                            p = (Double)t0.im_renta;
                        }
                        if (operador == "*")
                        {
                            rentafinal = Math.Round(p * tp_cmb,2);
                        }
                        else
                        {
                            rentafinal = Math.Round(p / tp_cmb,2);
                        }

                        PdfPTable table = new PdfPTable(6);                        
                        table.WidthPercentage = 100;
                        //float[] widths = new float[] { 100f,100f,100f,100f,100f,100f };
                        //table.SetWidths(widths);
                        //table.LockedWidth = true;

                        PdfPCell cell = new PdfPCell(new Phrase(t.nb_parque + " / " + t.nb_nave + "\n", new iTextSharp.text.Font(Vn_Helvetica, 14f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE)));
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
                        PdfPCell cell1 = new PdfPCell(new Phrase("" + t.nb_calle + " No. " + t.nu_direcion + "\n" + col + "\n" + mpio + "\n", new iTextSharp.text.Font(Vn_Helvetica, 10f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                        cell1.Colspan = 2;
                        cell1.HorizontalAlignment = 0; // 0 izq, 1 centro, 2 der
                        cell1.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell1);
                        PdfPCell cell29 = new PdfPCell(new Phrase("\nPlano de ubicación/Location Map\n\n", new iTextSharp.text.Font(Vn_Helvetica, 10f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                        cell29.Colspan = 2;
                        cell29.HorizontalAlignment = 1; // 0 izq, 1 centro, 2 der
                        cell29.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell29);
                        PdfPCell cell30 = new PdfPCell(new Phrase("\nComentarios/Comments\n\n", new iTextSharp.text.Font(Vn_Helvetica, 10f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
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
                                mapa = iTextSharp.text.Image.GetInstance(urlImage);
                                //mapa.ScalePercent(40);
                                //mapa.ScaleAbsolute(80f, 50f);
                                //table.AddCell(mapa); //documento.Add(mapa);
                                //mapa = iTextSharp.text.Image.GetInstance(Server.MapPath("~/image/" + "logo_CW.png"));
                                mapa.ScaleToFit(247f, 260f);
                                PdfPCell cell400 = new PdfPCell(mapa);
                                cell400.HorizontalAlignment = 1;                                
                                cell400.Colspan = 2;
                                cell400.Padding = 3;
                                table.AddCell(new PdfPCell(cell400));
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
                                table.AddCell(cell400);
                                //table.AddCell(mapa); //documento.Add(mapa);

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
                            //table.AddCell(mapa); //documento.Add(mapa);
                        }

                        if (t.nb_posicion != null || t.nb_poligono != null)
                        {
                            //table.AddCell("Informaciòn de la propiedad");    
                            try
                            {
                                iTextSharp.text.Image mapa1 = iTextSharp.text.Image.GetInstance("https://maps.googleapis.com/maps/api/staticmap?center=" + t.nb_posicion + "&zoom=15&size=640x500&maptype=hybrid&path=fillcolor:red|" + t.nb_poligono.TrimEnd('|') + "&key=AIzaSyDhfzatgC_64higR2LlLGkWldmTBudiRWY");
                            
                                //mapa1.ScalePercent(35f);
                                mapa1.ScaleToFit(247f, 260f);
                                PdfPCell cell401 = new PdfPCell(mapa1);
                                cell401.HorizontalAlignment = 1;
                                cell401.VerticalAlignment = 1;
                                cell401.Colspan = 2;
                                cell401.Padding = 3;
                                table.AddCell(cell401);
                                //table.AddCell(mapa1);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.InnerException);
                            }
                        }
                        else
                        {
                            iTextSharp.text.Image mapa1 = iTextSharp.text.Image.GetInstance(Server.MapPath("~/image/" + "nb.png"));
                            mapa1.ScaleToFit(247f, 260f);
                            PdfPCell cell401 = new PdfPCell(mapa);
                            cell401.HorizontalAlignment = 1;
                            cell401.VerticalAlignment = 1;
                            cell401.Padding = 3;
                            cell401.Colspan = 2;
                            table.AddCell(cell401);
                            //table.AddCell(mapa1);
                        }

                        //table.AddCell("Terreno de " + t2.nu_tam_max + " m2 con servicios disponibles");
                        PdfPCell cell32 = new PdfPCell(new Phrase(t.nb_comentarios, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell32.Colspan = 2;
                        //cell32.Border = Rectangle.TOP_BORDER + Rectangle.BOTTOM_BORDER;
                        table.AddCell(cell32);

                        PdfPCell cell1000 = new PdfPCell(new Phrase("               ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                        cell1000.UseVariableBorders = true;
                        cell1000.Colspan = 6;
                        cell1000.BorderColorLeft = BaseColor.WHITE;
                        cell1000.BorderColorRight = BaseColor.WHITE;
                        cell1000.BorderColorBottom = BaseColor.WHITE;
                        table.AddCell(cell1000);


                        PdfPCell cell2 = new PdfPCell(new Phrase("Información de la propiedad/Property Information", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                        cell2.Colspan = 4;
                        cell2.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        cell2.BorderColorTop = new iTextSharp.text.BaseColor(1, 1, 1);
                        table.AddCell(cell2);
                        
                        //table.AddCell("");
                        PdfPCell cell40 = new PdfPCell(new Phrase(" Notas Recorrido/Tour Notes:", new iTextSharp.text.Font(Vn_Helvetica, 10f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
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
                                PdfPCell cell3 = new PdfPCell(new Phrase("Corredor / Market: ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
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
                            PdfPCell cell3 = new PdfPCell(new Phrase("Corredor / Market: ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                            cell3.Colspan = 1;
                            cell3.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                            table.AddCell(cell3);
                            PdfPCell cell300 = new PdfPCell(new Phrase(": ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                            cell300.Colspan = 1;
                            cell300.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                            table.AddCell(cell300);
                        }

                        //table.AddCell("Corredor: " + t3.nb_corredor);
                        col = "";
                        if (t2.cd_hvac != null)
                        {
                            var t11 = db.tsg015_hvac.First(i => i.cd_hvac == t2.cd_hvac);
                            if (t11 != null)
                            {
                                col = t11.nb_hvac;
                                if (col == "Otro / Other")
                                {
                                    col = t2.nb_hvac;
                                }
                            }
                        }
                        PdfPCell cell28 = new PdfPCell(new Phrase("Sistema de Ventilación/Ventilation System ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell28.Colspan = 1;
                        cell28.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell28);
                        PdfPCell cell280 = new PdfPCell(new Phrase(": " + col, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell280.Colspan = 1;
                        cell280.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell280);
                        //table.AddCell("");


                        PdfPCell cell5 = new PdfPCell(new Phrase("Año de construccion / Date Built ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell5.Colspan = 1;
                        cell5.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell5);
                        PdfPCell cell500 = new PdfPCell(new Phrase(": " + t2.nu_anio_cons, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell500.Colspan = 1;
                        cell500.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell500);

                        //table.AddCell("Clasificacion del edificio: ");
                        PdfPCell cell16 = new PdfPCell(new Phrase("Puertas de Anden / Dock High Doors ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell16.Colspan = 1;
                        cell16.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell16);
                        PdfPCell cell160 = new PdfPCell(new Phrase(": " + t2.nu_puertas, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell160.Colspan = 1;
                        cell160.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell160);


                        PdfPCell cell161 = new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell161.Colspan = 2;                        
                        table.AddCell(cell161);

                        //table.AddCell(cell4);

                        //table.AddCell(cell5);
                        //table.AddCell("Año de construccion:");

                        col = "";
                        if (t2.cd_Nivel_Piso != null)
                        {
                            var t12 = db.tsg042_Nivel_Piso.First(i => i.cd_nivel_piso == t2.cd_Nivel_Piso);
                            if (t12 != null)
                            {
                                col = t12.nb_nivel_piso;
                            }
                        }
                        PdfPCell cell17 = new PdfPCell(new Phrase("Entrada a nivel de piso / Grade Level Doors ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell17.Colspan = 1;
                        cell17.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell17);
                        PdfPCell cell170 = new PdfPCell(new Phrase(": " + col, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell170.Colspan = 1;
                        cell170.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell170);

                        PdfPCell cell6 = new PdfPCell(new Phrase("Superficie de Nave / Warehouse Rentable: ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell6.Colspan = 1;
                        cell6.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell6);
                        double superficieTotal = Convert.ToDouble(t2.nu_superficie);
                        double superficieTotalF = Convert.ToDouble(t2.nu_superficie) * 10.7639; 

                        //PdfPCell cell60 = new PdfPCell(new Phrase(": " + String.Format("{0:#,##0.00}", t2.nu_superficie) + " M2", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        PdfPCell cell60 = new PdfPCell(new Phrase(": " + superficieTotal.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")) + " M2 / "+ superficieTotalF.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")) + " Ft2", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell60.Colspan = 1;
                        cell60.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell60);
                        //table.AddCell("Superficie de Nave: ");

                        PdfPCell cell61 = new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell61.Colspan = 2;
                        table.AddCell(cell61);
                        //table.AddCell("");

                        PdfPCell cell18 = new PdfPCell(new Phrase("Rampas de Acceso / Ramps ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell18.Colspan = 1;
                        cell18.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell18);
                        PdfPCell cell180 = new PdfPCell(new Phrase(": " + t2.nu_rampas, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell180.Colspan = 1;
                        cell180.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell180);

                        PdfPCell cell7 = new PdfPCell(new Phrase("Superficie Terreno / Land Size ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell7.Colspan = 1;
                        cell7.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell7);
                        double edificio = Convert.ToDouble(t2.nu_bodega);
                        double edificioft = Convert.ToDouble(t2.nu_bodega) * 10.7639;
                        
                        PdfPCell cell70 = new PdfPCell(new Phrase(": " + edificio.ToString("N2", CultureInfo.CreateSpecificCulture("en-US")) + " M2 / " + String.Format("{0:#,##0.00}", edificioft) + " Ft2", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell70.Colspan = 1;
                        cell70.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell70);

                        PdfPCell cell71 = new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell71.Colspan = 2;
                        table.AddCell(cell71);
                        //table.AddCell("");

                        //table.AddCell("Superficie de oficinas: ");    
                        col = "";                    
                        if (t2.cd_carga != null)
                        {
                            var t6 = db.tsg011_carga_piso.First(i => i.cd_carga == t2.cd_carga);
                            if (t6 != null)
                            {
                                col = t6.nb_carga;
                            }
                        }
                        PdfPCell cell19 = new PdfPCell(new Phrase("Capacidad de carga Piso / Floor Load Capacity ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell19.Colspan = 1;
                        cell19.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell19);
                        PdfPCell cell190 = new PdfPCell(new Phrase(": " + col, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell190.Colspan = 1;
                        cell190.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell190);

                        double dDisponibilidad = Convert.ToDouble(t2.nu_disponibilidad);
                        double dDisponibilidad_ft = Convert.ToDouble(t2.nu_disponibilidad) * 10.7639;
                        
                        PdfPCell cell8 = new PdfPCell(new Phrase("Disponibilidad Total / Total Availability ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell8.Colspan = 1;
                        cell8.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell8);
                        PdfPCell cell80 = new PdfPCell(new Phrase(": " + String.Format("{0:#,##0.00}", dDisponibilidad) + " M2 / " + String.Format("{0:#,##0.00}", dDisponibilidad_ft) + " Ft2" , new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell80.Colspan = 1;
                        cell80.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell80);
                        //table.AddCell("Disponibilidad Total: ");
                        PdfPCell cell81 = new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell81.Colspan = 2;
                        table.AddCell(cell81);
                        //table.AddCell("");

                        col = "";
                        if (t2.cd_cajon_est != null)
                        {
                            var t13 = db.tsg019_cajon_est.First(i => i.cd_cajon_est == t2.cd_cajon_est);
                            if (t13 != null)
                            {
                                col = t13.nb_cajon_est;

                                if (t13.nb_cajon_est == "Otro / Other")
                                {
                                    col = col + " - " + t2.nb_cajon_est;
                                }
                            }
                        }
                        PdfPCell cell20 = new PdfPCell(new Phrase("Cajones de Estacionamiento / Parking stalls " , new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell20.Colspan = 1;
                        cell20.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell20);
                        PdfPCell cell200 = new PdfPCell(new Phrase(": " + col, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell200.Colspan = 1;
                        cell200.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell200);

                        col = "";
                        if (t2.cd_tp_lampara != null)
                        {
                            var t7 = db.tsg014_tp_lampara.First(i => i.cd_tp_lampara == t2.cd_tp_lampara);
                            if (t7 != null)
                            {
                                col = t7.nb_tp_lampara;
                            }
                        }
                        PdfPCell cell21 = new PdfPCell(new Phrase("Tipo de Lámparas / Lightning: ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell21.Colspan = 1;
                        cell21.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell21);
                        PdfPCell cell210 = new PdfPCell(new Phrase(": " + col, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell210.Colspan = 1;
                        cell210.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell210);

                        PdfPCell cell211 = new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell211.Colspan = 2;
                        table.AddCell(cell211);
                        //table.AddCell("");

                        col = "";
                        if (t2.cd_tp_tech != null)
                        {
                            var t8 = db.tsg041_tp_tech.First(i => i.cd_tp_tech == t2.cd_tp_tech);
                            if (t8 != null)
                            {
                                col = t8.nb_tp_tech;
                            }
                        }
                        PdfPCell cell10 = new PdfPCell(new Phrase("Techumbre / Roof type ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell10.Colspan = 1;
                        cell10.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell10);
                        PdfPCell cell100 = new PdfPCell(new Phrase(": " + col, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell100.Colspan = 1;
                        cell100.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell100);
                        //table.AddCell("Techumbre: ");//////

                        PdfPCell cell22 = new PdfPCell(new Phrase("Energía eléctrica / Power Supply: ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell22.Colspan = 1;
                        cell22.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell22);
                        PdfPCell cell220 = new PdfPCell(new Phrase(": " + String.Format("{0:#,##0}", t1.nu_kva) + " KvA", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell220.Colspan = 1;
                        cell220.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell220);

                        PdfPCell cell221 = new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell221.Colspan = 2;
                        table.AddCell(cell221);
                        //table.AddCell("");

                        PdfPCell cell11 = new PdfPCell(new Phrase("Altura Mínima-Máxima / Min-Max Height ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell11.Colspan = 1;
                        cell11.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell11);
                        double alturaft = Convert.ToDouble(t2.nu_altura) * 3.28084;
                        string alturaft_d = String.Format("{0:#,##0.00}", alturaft);
                        PdfPCell cell110 = new PdfPCell(new Phrase(": " + t2.nu_altura + " M / " + alturaft_d + " Ft", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell110.Colspan = 1;
                        cell110.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell110);
                        //table.AddCell("Altura Mìnima/Maxima: ");
                        PdfPCell cell23 = new PdfPCell(new Phrase("Agua Potable / Potable Water ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell23.Colspan = 1;
                        cell23.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell23);
                        PdfPCell cell230 = new PdfPCell(new Phrase(": " + (!t1.st_aguapozo? "Servicio Municipal – Municipal Service" : "Pozo – Water well"), new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell230.Colspan = 1;
                        cell230.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell230);

                        PdfPCell cell231 = new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell231.Colspan = 2;
                        table.AddCell(cell231);

                        //table.AddCell("");
                        if (t1.cd_telefonia != null)
                        {
                            var t4 = db.tsg043_telefonia.First(i => i.cd_telefonia == t1.cd_telefonia);
                            if (t4 != null)
                            {
                                PdfPCell cell24 = new PdfPCell(new Phrase("Telefonía / Telephone Service ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                                cell24.Colspan = 1;
                                cell24.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                                table.AddCell(cell24);
                                PdfPCell cell240 = new PdfPCell(new Phrase(": " + t4.nb_telefonia, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                                cell240.Colspan = 1;
                                cell240.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                                table.AddCell(cell240);
                            }
                            else
                            {
                                PdfPCell cell24 = new PdfPCell(new Phrase("Telefonía / Telephone service ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                                cell24.Colspan = 1;
                                cell24.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                                table.AddCell(cell24);
                                PdfPCell cell240 = new PdfPCell(new Phrase(": ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                                cell240.Colspan = 1;
                                cell240.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                                table.AddCell(cell240);
                            }
                        }
                        else
                        {
                            PdfPCell cell24 = new PdfPCell(new Phrase("Telefonía / Telephone service: ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                            cell24.Colspan = 1;
                            cell24.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                            table.AddCell(cell24);
                            PdfPCell cell240 = new PdfPCell(new Phrase(": ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                            cell240.Colspan = 1;
                            cell240.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                            table.AddCell(cell240);
                        }

                        //table.AddCell("");

                        //var t4 = db.tsg012_sist_incendio.First(i => i.cd_sist_inc == t);
                        //if (t4 == null)
                        //{
                        //}
                        col = "";
                        if (t2.cd_sist_inc != null)
                        {
                            var t9 = db.tsg012_sist_incendio.First(i => i.cd_sist_inc == t2.cd_sist_inc);
                            if (t9 != null)
                            {
                                col = t9.nb_sist_inc;
                                if (t9.nb_sist_inc == "Otro / Other")
                                {
                                    col = col + " - " + t2.nb_sist_inc;
                                }
                            }
                        }
                        PdfPCell cell13 = new PdfPCell(new Phrase("Sistema contra incendio / Fire System ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell13.Colspan = 1;
                        cell13.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell13);
                        PdfPCell cell130 = new PdfPCell(new Phrase(": " + col, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell130.Colspan = 1;
                        cell130.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell130);

                        PdfPCell cell232 = new PdfPCell(new Phrase(" ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell232.Colspan = 2;
                        table.AddCell(cell232);
                        //table.AddCell("");

                        //table.AddCell("Sistema contra incendio: ");
                        PdfPCell cell25 = new PdfPCell(new Phrase("Estacionamiento Trailers / Drop Lots ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell25.Colspan = 1;
                        cell25.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell25);
                        PdfPCell cell250 = new PdfPCell(new Phrase(": " + t2.nu_caj_trailer, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell250.Colspan = 1;
                        cell250.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell250);
                        //table.AddCell("");

                        col = "";
                        if (t2.cd_tp_construccion != null)
                        {
                            var t10 = db.tsg013_tp_construccion.First(i => i.cd_tp_construccion == t2.cd_tp_construccion);
                            if (t10 != null)
                            {
                                col = t10.nb_tp_construccion;
                                if (col == "Otro / Other")
                                {
                                    col = t2.nb_tp_construccion;
                                }
                            }
                        }
                        PdfPCell cell15 = new PdfPCell(new Phrase("Tipo de Construcción / Walls Type ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell15.Colspan = 1;
                        cell15.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell15);
                        PdfPCell cell150 = new PdfPCell(new Phrase(": " + col, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell150.Colspan = 1;
                        cell150.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell150);

                        var cond_arrend = (t0.cd_cond_arr!=null) ? db.tsg024_cond_arr.First(i => i.cd_cond_arr == t0.cd_cond_arr) : null;

                        PdfPTable table1 = new PdfPTable(2);
                        PdfPCell cell31 = null;
                        if (t0.cd_cond_arr != null)
                        {
                            cell31 = new PdfPCell(new Phrase(" Precio de Venta / Sales Price \n Precio de Renta / Rent Price \n Mantenimiento de Áreas / Maintenance Fee " + cond_arrend.nb_cond_arr + " ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));

                        }
                        else
                        {
                            cell31 = new PdfPCell(new Phrase(" Precio de Venta / Sales Price \n Precio de Renta / Rent Price \n Mantenimiento de Áreas / Maintenance Fee " + " - " + " ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        }
                        cell31.Rowspan = 4;
                        cell31.Colspan = 1;
                        cell31.Border = PdfPCell.NO_BORDER;
                        cell31.Padding = 3;
                        cell31.PaddingBottom = 3;
                        //cell31.CellEvent = new RoundedBorder();
                        //cell31.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table1.AddCell(cell31);

                        double mantenimiento = Convert.ToDouble(t0.nu_ma_ac);
                        PdfPCell cell310 = new PdfPCell(new Phrase(": " + preciofinal.ToString("C2", CultureInfo.CreateSpecificCulture("en-us")) +" " + moneda+"\n: " + rentafinal.ToString("C2", CultureInfo.CreateSpecificCulture("en-us")) + " " + moneda + "/SqMt/Month\n: " + mantenimiento.ToString("C2", CultureInfo.CreateSpecificCulture("en-us")) + " " + moneda +"/SqMt/Month", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell310.Rowspan = 4;
                        cell310.Colspan = 1;
                        cell310.Border = PdfPCell.NO_BORDER;
                        //cell310.CellEvent = new RoundedBorder();
                        cell31.Padding = 3;                        
                        table1.AddCell(cell310);

                        PdfPCell cell311 = new PdfPCell(table1);
                        cell311.Border = PdfPCell.NO_BORDER;
                        cell311.Colspan = 2;                        
                        cell311.CellEvent = new RoundedBorder();
                        table.AddCell(cell311);
                        
                        documento.Add(table);


                        //Paragraph p2 = new Paragraph("\n\n", new iTextSharp.text.Font(Vn_Helvetica, 6f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK));
                        //documento.Add(p2);
                        //'DIBUJANDO LINEAS
                        //Paragraph p1 = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                        //documento.Add(p1);

                        //Paragraph p100 = new Paragraph("\nThis report contains information available to the public and has been relied upon by Cushman & Wakefield on the basis that it is accurate and complete. Cushman & Wakefield accepts no responsibility if this should prove not to be the case. No warranty or representation, express or implied, is made to the accuracy or completeness of the information contained herein, and same is submitted subject to errors, omissions, change of price, rental or other conditions, withdrawal without notice, and to any special listing conditions imposed by our principals.", new iTextSharp.text.Font(Vn_Helvetica, 7f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK));
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

                uploadfile1(Server.MapPath("~/image/" + "Naves_rpt.pdf"), "Naves_rpt.pdf");
                string strUrl = descargaBlob2("Naves_rpt.pdf");
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

                PdfWriter writer = PdfWriter.GetInstance(documento, new FileStream(Server.MapPath("~/image/" + "Naves_rpt_us.pdf"), FileMode.Create));
                //PdfWriter.GetInstance(documento, new FileStream(@"D:\archivo.pdf",FileMode.Create));            
                var t = db.tsg002_nave_industrial.Find(id);
                if (t != null)
                {
                    try
                    {
                        documento.Open();

                        var t0 = db.tsg023_ni_precio.First(i => i.cd_nave == id);
                        if (t0 == null)
                        {
                        }

                        var t1 = db.tsg020_ni_servicio.First(i => i.cd_nave == id);
                        if (t1 == null)
                        {
                        }

                        var t2 = db.tsg009_ni_dt_gral.First(i => i.cd_nave == id);
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
                            preciofinal = p * tp_cmb;
                        }
                        else
                        {
                            preciofinal = p / tp_cmb;
                        }
                        p = 0;
                        if (t0.im_renta != null)
                        {
                            p = (Double)t0.im_renta;
                        }
                        if (operador == "*")
                        {
                            rentafinal = p * tp_cmb;
                        }
                        else
                        {
                            rentafinal = p / tp_cmb;
                        }

                        PdfPTable table = new PdfPTable(3);

                        PdfPCell cell = new PdfPCell(new Phrase(t.nb_parque + " / " + t.nb_nave + "\n", new iTextSharp.text.Font(Vn_Helvetica, 14f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE)));
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
                        PdfPCell cell1 = new PdfPCell(new Phrase("" + t.nb_calle + "\n" + col + "\n" + mpio + "\n\n", new iTextSharp.text.Font(Vn_Helvetica, 10f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                        cell1.Colspan = 1;
                        cell1.HorizontalAlignment = 0; // 0 izq, 1 centro, 2 der
                        cell1.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell1);
                        PdfPCell cell29 = new PdfPCell(new Phrase("\nLocation Map\n\n", new iTextSharp.text.Font(Vn_Helvetica, 10f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                        cell29.Colspan = 1;
                        cell29.HorizontalAlignment = 1; // 0 izq, 1 centro, 2 der
                        cell29.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell29);
                        PdfPCell cell30 = new PdfPCell(new Phrase("\nComments.\n\n", new iTextSharp.text.Font(Vn_Helvetica, 10f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                        cell30.Colspan = 1;
                        cell30.HorizontalAlignment = 0; // 0 izq, 1 centro, 2 der
                        cell30.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell30);
                        try
                        {
                            //imagenes
                            if (urlImage != "")
                            {
                                //imagenes
                                iTextSharp.text.Image mapa = iTextSharp.text.Image.GetInstance(urlImage);
                                mapa.ScalePercent(40f);
                                table.AddCell(mapa); //documento.Add(mapa);
                            }
                            else
                            {
                                iTextSharp.text.Image mapa = iTextSharp.text.Image.GetInstance(Server.MapPath("~/image/" + "nb.png"));
                                mapa.ScalePercent(40f);
                                
                                table.AddCell(mapa); //documento.Add(mapa);

                            }
                        }
                        catch (Exception e)
                        {
                            iTextSharp.text.Image mapa = iTextSharp.text.Image.GetInstance(Server.MapPath("~/image/" + "nb.png"));
                            mapa.ScalePercent(40f);

                            table.AddCell(mapa); //documento.Add(mapa);
                        }

                        if (t.nb_posicion != null || t.nb_poligono != null)
                        {
                            //table.AddCell("Informaciòn de la propiedad");                    
                            iTextSharp.text.Image mapa1 = iTextSharp.text.Image.GetInstance("https://maps.googleapis.com/maps/api/staticmap?center=" + t.nb_posicion + "&zoom=15&size=640x640&maptype=hybrid&path=fillcolor:red|" + t.nb_poligono.TrimEnd('|') + "&key=AIzaSyDhfzatgC_64higR2LlLGkWldmTBudiRWY"); mapa1.ScalePercent(40f);
                            table.AddCell(mapa1);
                        }
                        else
                        {
                            iTextSharp.text.Image mapa1 = iTextSharp.text.Image.GetInstance(Server.MapPath("~/image/" + "nb.png"));
                            //mapa1.ScalePercent(40f);
                            table.AddCell(mapa1);
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
                        //table.AddCell("");
                        PdfPCell cell40 = new PdfPCell(new Phrase("Notes:", new iTextSharp.text.Font(Vn_Helvetica, 10f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell40.Rowspan = 2;
                        cell40.Border = PdfPCell.NO_BORDER;
                        cell40.CellEvent = new RoundedBorder();
                        table.AddCell(cell40);

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
                        //table.AddCell("");

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

                        PdfPCell cell5 = new PdfPCell(new Phrase("Date Built: " + t2.nu_anio_cons, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell5.Colspan = 1;
                        cell5.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell5);
                        //table.AddCell("Año de construccion:");
                        PdfPCell cell17 = new PdfPCell(new Phrase("Grade Level Doors: " , new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell17.Colspan = 1;
                        cell17.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell17);

                        table.AddCell("");

                        PdfPCell cell6 = new PdfPCell(new Phrase("Warehouse Rentable: " + t2.nu_superficie, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell6.Colspan = 1;
                        cell6.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell6);
                        //table.AddCell("Superficie de Nave: ");
                        PdfPCell cell18 = new PdfPCell(new Phrase("Ramps: " + t2.nu_rampas, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell18.Colspan = 1;
                        cell18.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell18);
                        table.AddCell("");

                        PdfPCell cell7 = new PdfPCell(new Phrase("Office Rentable: " + t2.nu_bodega, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell7.Colspan = 1;
                        cell7.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell7);
                        //table.AddCell("Superficie de oficinas: ");
                        //table.AddCell("Superficie de oficinas: ");    
                        col = "";
                        if (t2.tsg042_Nivel_Piso != null)
                        {
                            var t6 = db.tsg042_Nivel_Piso.First(i => i.cd_nivel_piso == t2.cd_Nivel_Piso);
                            if (t6 != null)
                            {
                                col = t6.nb_nivel_piso;
                            }
                        }
                        PdfPCell cell19 = new PdfPCell(new Phrase("Floor Structure: " + col, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell19.Colspan = 1;
                        cell19.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell19);
                        table.AddCell("");

                        PdfPCell cell8 = new PdfPCell(new Phrase("Total Availability: " + t2.nu_disponibilidad, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
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
                        col = "";
                        if (t2.cd_tp_lampara != null)
                        {
                            var t7 = db.tsg014_tp_lampara.First(i => i.cd_tp_lampara == t2.cd_tp_lampara);
                            if (t7 != null)
                            {
                                col = t7.nb_tp_lampara;
                            }
                        }
                        PdfPCell cell21 = new PdfPCell(new Phrase("Lightning: " + col, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell21.Colspan = 1;
                        cell21.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell21);
                        table.AddCell("");

                        col = "";
                        if (t2.cd_tp_tech != null)
                        {
                            var t8 = db.tsg041_tp_tech.First(i => i.cd_tp_tech == t2.cd_tp_tech);
                            if (t8 != null)
                            {
                                col = t8.nb_tp_tech;
                            }
                        }
                        PdfPCell cell10 = new PdfPCell(new Phrase("Roof type: " + col, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell10.Colspan = 1;
                        cell10.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell10);
                        //table.AddCell("Techumbre: ");
                        PdfPCell cell22 = new PdfPCell(new Phrase("Power Supply: ", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell22.Colspan = 1;
                        cell22.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell22);
                        table.AddCell("");

                        PdfPCell cell11 = new PdfPCell(new Phrase("Min / Max Height: " + t2.nu_altura, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
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
                        col = "";
                        if (t2.cd_sist_inc != null)
                        {
                            var t9 = db.tsg012_sist_incendio.First(i => i.cd_sist_inc == t2.cd_sist_inc);
                            if (t9 != null)
                            {
                                col = t9.nb_sist_inc;
                            }
                        }
                        PdfPCell cell13 = new PdfPCell(new Phrase("Fire system: " + col, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        cell13.Colspan = 1;
                        cell13.BorderColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        table.AddCell(cell13);
                        //table.AddCell("Sistema contra incendio: ");
                        PdfPCell cell25 = new PdfPCell(new Phrase("Drop Lots & Maneuver patio: " + t2.nu_caj_trailer, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
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
                        PdfPCell cell31 = new PdfPCell(new Phrase("Price: " + preciofinal + " " + moneda + "\nMaintenance:" + rentafinal + " " + moneda, new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
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

                        Paragraph p100 = new Paragraph("\nThis report contains information available to the public and has been relied upon by Cushman & Wakefield on the basis that it is accurate and complete. Cushman & Wakefield accepts no responsibility if this should prove not to be the case. No warranty or representation, express or implied, is made to the accuracy or completeness of the information contained herein, and same is submitted subject to errors, omissions, change of price, rental or other conditions, withdrawal without notice, and to any special listing conditions imposed by our principals.", new iTextSharp.text.Font(Vn_Helvetica, 6f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK));
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

                uploadfile1(Server.MapPath("~/image/" + "Naves_rpt_us.pdf"), "Naves_rpt_us.pdf");
                string strUrl = descargaBlob2("Naves_rpt_us.pdf");
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

        [HttpPost]
        public JsonResult actualizaArchivo(int? id, int? id_reporte)
        {
            var imagenes = db.tsg045_imagenes_naves.Find(id);
            if (imagenes != null)
            {
                imagenes.cd_reporte = id_reporte;
                db.Entry(imagenes).CurrentValues.SetValues(imagenes);
                db.SaveChanges();
            }

            var cd_nave = db.tsg045_imagenes_naves.First(i => i.cd_id == id);
            var imagenes2 = db.tsg045_imagenes_naves.Where(t => t.cd_nave == cd_nave.cd_nave).Select("new (cd_nave, cd_id, nb_archivo, tx_archivo)");
            return new JsonResult { Data = imagenes2, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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


        //public JsonResult GetNaves()
        //{

        //    clsNaves naves = new clsNaves();

        //    var tsi002_dts_naves = db.tsg002_nave_industrial.Select("new (cd_nave, nb_nave)");
        //    //= db.tsg001_terreno.Include(t => t.tsg026_te_dt_gral).Include(t => t.tsg027_te_servicio).Include(t => t.tsg023_ni_precio);            

        //    return new JsonResult { Data = tsi002_dts_naves, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}
        public JsonResult GetNaves()
        {


            List<object> tsi001_dts_naves = new List<object>();// db.tsg001_terreno.Select("new (cd_terreno, nb_comercial)");

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

            
            //************** Filtro en la tabla naves ************************
            var dbnave = db.tsg002_nave_industrial.First();
            dbnave.nb_dir_nave = "";
            dbnave.nb_poligono = "";
            dbnave.nb_posicion = "";
            if (dbnave != null)
            {
                strWhere = obterFiltrado(json, "tsg002_nave_industrial", dbnave);
                strWhereTemp = strWhereTemp + strWhere;
                if (strWhere != String.Empty)
                {
                    if (strWhere == "")
                    {
                        strWhere = "1=1";
                    }

                    var cd_nave = db.tsg002_nave_industrial.Where(strWhere);//.Select("new (cd_terreno)");          
                    //myDatos = db.tsg001_terreno.Where(strWhere).Select("new (cd_terreno, nb_comercial)");
                    if (cd_nave.Count() > 0)
                    {
                        foreach (var id in cd_nave)
                        {
                            if (!tsi001_dts_naves.Contains(id.cd_nave))
                            {
                                tsi001_dts_naves.Add(
                                 db.tsg002_nave_industrial.Where(x => x.cd_nave == id.cd_nave).Select("new (cd_nave, nb_parque, nb_nave)"));
                            }
                        }
                    }
                }
            }

            //filtro = false;

            //************** Filtro en la tabla de precios para naves ************************
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
                            if (!tsi001_dts_naves.Contains(id.cd_nave))
                            {
                                tsi001_dts_naves.Add(
                             db.tsg002_nave_industrial.Where(x => x.cd_nave == id.cd_nave).
                                 Select("new (cd_nave, nb_parque, nb_nave)"));
                            }
                        }
                    }
                }
            }
            filtro = false;

            //************** Filtro en la tabla General de naves ************************
            var dbgeneral = db.tsg009_ni_dt_gral.First();
            if (dbgeneral != null)
            {
                strWhere = obterFiltrado(json, "tsg009_ni_dt_gral", dbgeneral);
                strWhereTemp = strWhereTemp + strWhere;
                if (strWhere != String.Empty)
                {
                    if (strWhere == "")
                    {
                        strWhere = "1=1";
                    }

                    var general = db.tsg009_ni_dt_gral.Where(strWhere);//.Select("new (cd_terreno)");                
                    if (general.Count() > 0)
                    {
                        foreach (var id in general)
                        {
                            if (!tsi001_dts_naves.Contains(id.cd_nave))
                            {
                                tsi001_dts_naves.Add(
                                db.tsg002_nave_industrial.Where(x => x.cd_nave == id.cd_nave).
                                 Select("new (cd_nave, nb_parque, nb_nave)"));
                            }
                        }
                    }
                }
            }
            filtro = false;

            //************** Filtro en la tabla servicios de naves ************************
            var dbservicio = db.tsg020_ni_servicio.First();
            if (dbservicio != null)
            {
                strWhere = obterFiltrado(json, "tsg020_ni_servicio", dbservicio);
                strWhereTemp = strWhereTemp + strWhere;
                if (strWhere != String.Empty)
                {
                    if (strWhere == "")
                    {
                        strWhere = "1=1";
                    }
                    var servicios = db.tsg020_ni_servicio.Where(strWhere);//.Select("new (cd_terreno)");

                    if (servicios.Count() > 0)
                    {
                        foreach (var id in servicios)
                        {
                            if (!tsi001_dts_naves.Contains(id.cd_nave))
                            {
                                tsi001_dts_naves.Add(
                             db.tsg002_nave_industrial.Where(x => x.cd_nave == id.cd_nave).
                                 Select("new (cd_nave, nb_parque, nb_nave)"));
                            }
                        }
                    }
                }
            }
            filtro = false;

            //************** Filtro en la tabla propietarios de terrenos ************************
            var dbcontacto_p = db.tsg025_ni_contacto.First();
            if (dbcontacto_p != null)
            {
                strWhere = obterFiltrado(json, "tsg025_ni_contacto_P", dbcontacto_p);
                strWhereTemp = strWhereTemp + strWhere;
                if (strWhere != String.Empty)
                {
                    if (strWhere == "")
                    {
                        strWhere = "1=1";
                    }

                    var propietarios = db.tsg025_ni_contacto.Where(strWhere);//.Select("new (cd_terreno)");
                    propietarios = propietarios.Where(x => x.st_contacto == 1);

                    if (propietarios.Count() > 0)
                    {
                        foreach (var id in propietarios)
                        {
                            if (!tsi001_dts_naves.Contains(id.cd_nave))
                            {
                                tsi001_dts_naves.Add(
                             db.tsg002_nave_industrial.Where(x => x.cd_nave == id.cd_nave).
                                 Select("new (cd_nave, nb_parque, nb_nave)"));
                            }
                        }
                    }
                }
            }

            filtro = false;

            //************** Filtro en la tabla Administrador de terrenos ************************
            var dbcontacto_a = db.tsg025_ni_contacto.First();


            if (dbcontacto_a != null)
            {
                strWhere = obterFiltrado(json, "tsg025_ni_contacto_A", dbcontacto_a);
                strWhereTemp = strWhereTemp + strWhere;
                if (strWhere != String.Empty)
                {
                    if (strWhere == "")
                    {
                        strWhere = "1=1";
                    }
                    var administrador = db.tsg025_ni_contacto.Where(strWhere);//.Select("new (cd_terreno)");

                    administrador = administrador.Where(x => x.st_contacto == 2);
                    if (administrador.Count() > 0)
                    {
                        //var tsi001_dts_terreno = from terrenos in db.tsg001_terreno select terrenos new {  }                    
                        foreach (var id in administrador)
                        {
                            if (!tsi001_dts_naves.Contains(id.cd_nave))
                            {
                                tsi001_dts_naves.Add(
                             db.tsg002_nave_industrial.Where(x => x.cd_nave == id.cd_nave).
                                 Select("new (cd_nave, nb_parque, nb_nave)"));
                            }
                        }
                    }
                }
            }
            filtro = false;

            //************** Filtro en la tabla corredor de terrenos ************************
            var dbcontacto_c = db.tsg025_ni_contacto.First();
            if (dbcontacto_c != null)
            {
                strWhere = obterFiltrado(json, "tsg025_ni_contacto_C", dbcontacto_c);
                strWhereTemp = strWhereTemp + strWhere;
                if (strWhere != String.Empty)
                {
                    if (strWhere == "")
                    {
                        strWhere = "1=1";
                    }
                    var corredor = db.tsg025_ni_contacto.Where(x => x.st_contacto == 3);//.Select("new (cd_terreno)");
                    corredor = corredor.Where(strWhere);
                    if (corredor.Count() > 0)
                    {
                        foreach (var id in corredor)
                        {
                            if (!tsi001_dts_naves.Contains(id.cd_nave))
                            {
                                tsi001_dts_naves.Add(
                             db.tsg002_nave_industrial.Where(x => x.cd_nave == id.cd_nave).
                                 Select("new (cd_nave, nb_parque, nb_nave)"));
                            }
                        }
                    }
                }
            }

            //************** obtengo todo ************************

            if (tsi001_dts_naves.Count() == 0 && strWhereTemp == String.Empty)
            {
                strWhere = "1=1";
                var todo = db.tsg002_nave_industrial.Where(strWhere);//.Select("new (cd_terreno)");                
                if (todo.Count() > 0)
                {
                    foreach (var id in todo)
                    {
                        int cd_nave = id.cd_nave;
                        tsi001_dts_naves.Add(
                             db.tsg002_nave_industrial.Where(x => x.cd_nave == id.cd_nave).
                                 Select("new (cd_nave, nb_parque, nb_nave)"));
                    }
                }
            }

            return new JsonResult { Data = tsi001_dts_naves.ToList(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetNavesAll()
        {
            List<object> tsi001_dts_naves = new List<object>();// db.tsg001_terreno.Select("new (cd_terreno, nb_comercial)");
            var todo = db.tsg002_nave_industrial;//.Select("new (cd_terreno)");

            //List<tsg002_nave_industrial> tsi001_dts_naves = db.tsg002_nave_industrial.ToList();


            if (todo.Count() > 0)
            {
                foreach (var id in todo)
                {
                    int cd_nave = id.cd_nave;
                    tsi001_dts_naves.Add(
                         db.tsg002_nave_industrial.Where(x => x.cd_nave == id.cd_nave).
                             Select("new (cd_nave, nb_parque, nb_nave)"));
                }
            }
            return new JsonResult { Data = tsi001_dts_naves, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public ActionResult GetNavesDisponible(string disp_desde_p, string disp_hasta_p)
        {
            //double dis_desde_p, double disp_hasta_p
            decimal inicio = Convert.ToDecimal(disp_desde_p);
            decimal fin = Convert.ToDecimal(disp_hasta_p);
            List<object> tsi001_dts_naves = new List<object>();// db.tsg001_terreno.Select("new (cd_terreno, nb_comercial)");
            //var general = from a in db.tsg009_ni_dt_gral
            //              where a.nu_disponibilidad >= inicio
            //              where a.nu_disponibilidad <= fin
            //              select(a.cd_nave);
            //.Where(strWhere);//.Select("new (cd_terreno)");           

            var general = db.tsg009_ni_dt_gral.Where(x => x.nu_disponibilidad >= inicio && x.nu_disponibilidad <= fin);

            if (general.Count() > 0)
            {
                foreach (var id in general)
                {
                    if (!tsi001_dts_naves.Contains(id.cd_nave))
                    {
                        tsi001_dts_naves.Add(
                        db.tsg002_nave_industrial.Where(x => x.cd_nave == id.cd_nave).
                         Select("new (cd_nave, nb_parque, nb_nave)"));
                    }
                }
            }
            return new JsonResult { Data = tsi001_dts_naves.ToList(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };            
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

            if (diccionario==null)
            {
                return strWhere;
            }


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
            string valor = null;

            foreach (var item in dynObject)
            {
                camponame = item.Name.ToString();
                valor = item.Value.ToString();

                if (camponame.Contains(nombreTabla))
                {
                    camponame = camponame.Replace(nombreTabla + ".", "");
                    iContador++;

                    //if (camponame.Contains("cd_") && item.Value.ToString() != "0")
                    //{
                    try
                    {
                        if (diccionario[camponame].GetType().Name != null)
                        {
                            string strTipoDato = diccionario[camponame].GetType().Name.ToString();
                            if (item.Value.ToString() != "[\r\n  \"\",\r\n  \"\"\r\n]")
                            {
                                
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
                    }
                    catch (Exception e)
                    {
                        
                    }
                    //}
                }
            }
            return strWhere;
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
            CloudBlobContainer container = blobClient.GetContainerReference("cushmandocs/imagenes/N" + id);

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
            CloudBlobContainer container = blobClient.GetContainerReference("cushmandocs/imagenes/N" + id);

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
            CloudBlobContainer container = blobClient.GetContainerReference("cushmandocs/imagenes/N" + id);

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
        public JsonResult UploadFiles(int? id)
        {

            String strUrl;
            int cd_id = 0;
            //HttpPostedFileBase

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
                strUrl = BlobStorage2(id, NumArch + Path.GetExtension(hpf.FileName), hpf);
                //System.IO.Directory.CreateDirectory(Server.MapPath("~/image/N" + id));
                //string savedFileName = Path.Combine(Server.MapPath("~/image/N" + id + "/"), NumArch + Path.GetExtension(hpf.FileName));
                //hpf.SaveAs(savedFileName);

                tsg045_imagenes_naves archivosx = new tsg045_imagenes_naves();
                var t = db.tsg002_nave_industrial.Find(id);
                archivosx.cd_nave = t.cd_nave;
                //archivosx.nb_archivo = NumArch + Path.GetExtension(hpf.FileName);
                //archivosx.tx_archivo = Path.Combine(Server.MapPath("~/image/N" + id + "/"), NumArch + Path.GetExtension(hpf.FileName));
                archivosx.nb_archivo = hpf.FileName;
                archivosx.tx_archivo = strUrl;
                archivosx.fh_modif = DateTime.Now;

                db.tsg045_imagenes_naves.Add(archivosx);
                tsg029_bitacora_mov.GuardaBitacora("tsg040_imagenes_terrenos", "cd_nave", t.cd_nave, "Insertar Imagen");
                db.SaveChanges();
                cd_id = archivosx.cd_id;

            }
            var imagenes = db.tsg045_imagenes_naves.Where(t => t.cd_nave == id && t.cd_id == cd_id).Select("new (cd_nave, cd_id, nb_archivo, tx_archivo)");
            //return Content("{\"name\":\"" + r[0].Name + "\",\"type\":\"" + r[0].Type + "\",\"size\":\"" + string.Format("{0} bytes", r[0].Length) + "\"}", "application/json");
            return new JsonResult { Data = imagenes, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public ActionResult downloadFile(int? id, string archivo)
        {
            var sasUrl = descargaBlob(id, archivo);
            //string savedFileName = Path.Combine(Server.MapPath("~/App_Data"), archivo.Name + archivo.Name.Split('.').LastOrDefault());
            //System.IO.File.Delete(savedFileName);

            //var t = db.tsg001_terreno.Find(id);

            var imagenes = db.tsg045_imagenes_naves.Where(t => t.cd_nave == id).Select("new (cd_id, nb_archivo, tx_archivo)");
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
            var imagenes = db.tsg045_imagenes_naves.Where(t => t.cd_nave == id).Select("new (cd_nave, cd_id, nb_archivo, tx_archivo)");
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
            catch(Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
            //string savedFileName = Path.Combine(Server.MapPath("~/image/N" + id + "/"), archivo);
            //System.IO.File.Delete(savedFileName);
            var t1 = db.tsg045_imagenes_naves.First(i => i.cd_nave == id && i.cd_id == claves);
            db.tsg045_imagenes_naves.Remove(t1);
            db.SaveChanges();
            tsg029_bitacora_mov.GuardaBitacora("tsg040_imagenes_terrenos", "cd_id", t1.cd_id, "Eliminar Imagen");

            var imagenes = db.tsg045_imagenes_naves.Where(t => t.cd_nave == id).Select("new (cd_nave, cd_id, nb_archivo, tx_archivo)");
            //return Content("{\"name\":\"" + r[0].Name + "\",\"type\":\"" + r[0].Type + "\",\"size\":\"" + string.Format("{0} bytes", r[0].Length) + "\"}", "application/json");
            return new JsonResult { Data = imagenes, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult iniCreate()
        {
            return RedirectToAction("create");
        }

    }

}

