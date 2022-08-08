using System;


using System.IO;
using System.Linq;

using System.Web.Mvc;
using System.Web.UI;

using iTextSharp.text;
using System.Linq.Dynamic;
using iTextSharp.text.pdf;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
using wr_anit_cushman_one.Models;
//using Microsoft.Office.Interop.PowerPoint;
using System.Web.Helpers;
using System.Globalization;

namespace wr_anit_cushman_one.Controllers
{
    public class ReportNavesController : Controller
    {
        // GET: ReportNaves
        CushmanContext db = new CushmanContext();
        public ActionResult Index()
        {
            ViewBag.UsuarioActivo = true;
            ViewBag.disabled = true;
            return View();
        }
        [HttpPost]
        public ActionResult ReportePP(int cd_estado, int cd_municipio, int cd_colonia)
        {
            UploadFilesResult r = new UploadFilesResult();
            try
            {
                GeneraPowerPoint(cd_estado, cd_municipio, cd_colonia);
                uploadfileppt(Server.MapPath("~/image/" + "reporte.pptx"));
                string strUrl = descargaBlob2("reporte.pptx");
                
                r.Name = strUrl;
                r.Type = "";
                r.Length = 0;
                return new JsonResult { Data = r, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch(Exception ex)
            {
                r.Name = ex.Message;
                r.Type = "1";
                r.Length = 0;
                return new JsonResult { Data = r, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

        }
        [HttpPost]
        public ActionResult Reporte(int cd_estado, int cd_municipio, int cd_colonia, string cd_titulo, string cd_idioma)
        {
            ViewBag.UsuarioActivo = true;
            ViewBag.disabled = true;
            var estados = db.tsg037_estados.ToList();



            GeneraPDF(cd_estado, cd_municipio, cd_colonia, cd_titulo, cd_idioma);
            //byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/App_Data/" + "reporte.pdf"));
            //string filename =  "reporte.pdf";
            //return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
            string strUrl = descargaBlob2("reporte.pdf");
            UploadFilesResult r = new UploadFilesResult();
            r.Name = strUrl;
            r.Type = "";
            r.Length = 0;
            return new JsonResult { Data = r, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        [HttpPost]
        public void GetExcel(int estado, int municipio, int colonia)
        {           
            var query = from tsg002 in db.tsg002_nave_industrial
                        join o in db.tsg009_ni_dt_gral
                        on tsg002.cd_nave equals o.cd_nave
                        join tsg008 in db.tsg008_corredor_ind
                        on tsg002.cd_corredor equals tsg008.cd_corredor
                        join tsg007 in db.tsg007_mercado
                        on tsg002.cd_mercado equals tsg007.cd_mercado
                        join tsg037 in db.tsg037_estados
                        on tsg002.cd_estado equals tsg037.cd_estado
                        join tsg038 in db.tsg038_municipios
                        on tsg002.cd_municipio equals tsg038.cd_municipio
                        join tsg039 in db.tsg039_colonias
                        on tsg002.cd_colonia equals tsg039.cd_colonia
                        where tsg002.cd_estado == estado
                        //on new { tsg002.cd_estado, tsg002.cd_municipio, tsg002.cd_colonia } equals new { tsg039.cd_estado, tsg039.cd_municipio, tsg039.cd_colonia }
                        //on new { X1 = tsg002.cd_estado, X2 = tsg002.cd_municipio} equals new  { X1 = tsg038.cd_estado, X2 = tsg038.cd_municipio}
                        select new
                        {
                            tsg007.nb_mercado,
                            tsg008.nb_corredor,
                            tsg002.nb_parque,
                            tsg002.nb_nave,
                            tsg002.nb_calle,
                            tsg002.nu_direcion,
                            tsg002.nu_cp,
                            tsg037.nb_estado,
                            tsg038.nb_municipio,
                            tsg039.nb_colonia,
                            tsg002.nb_dir_nave,
                            tsg002.cd_estado,
                            tsg002.cd_municipio,
                            tsg002.cd_colonia

                        };

            if (municipio > 0)
            { query = query.Where(t => t.cd_municipio == municipio); }

            if (colonia > 0)
            { query = query.Where(t => t.cd_colonia == colonia); }
            //var query = from c in customers join o in orders on c.ID equals o.ID select new { c.Name, o.Product };


            WebGrid grdGrid = new WebGrid(source: query.ToList(), canPage: false, canSort: false);

            /// nuevo
            //System.Web.UI.WebControls.GridView gv = new System.Web.UI.WebControls.GridView();
            //gv.DataSource = query.ToList();
            //gv.DataBind();


            string gridData = grdGrid.GetHtml(
                columns: grdGrid.Columns(
                    grdGrid.Column("nb_mercado", "Mercado"),
                    grdGrid.Column("nb_corredor", "Corredor"),
                    grdGrid.Column("nb_parque", "Parque"),
                    grdGrid.Column("nb_nave", "Nombre Nave"),
                    grdGrid.Column("nb_calle", "Calle"),
                    grdGrid.Column("nu_direcion", "No."),
                    grdGrid.Column("nu_cp", "CP"),
                    grdGrid.Column("nb_estado", "Estado"),
                    grdGrid.Column("nb_municipio", "Municipio"),
                    grdGrid.Column("nb_colonia", "Colonia"),
                    grdGrid.Column("nb_dir_nave", "Direccion")
                )
             ).ToString();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Naves.xls");
            Response.ContentType = "application/ms-excel";

            // ----- StringWriter sw = new StringWriter();
            // ----- HtmlTextWriter htw = new HtmlTextWriter(sw);        

            //StringWriter sw = new StringWriter();

            //Response.Output.Write(sw.ToString());


            Response.Write(gridData);
            //Response.Flush();
            Response.End();

     
        }
       
        //public ActionResult PostReportPartial(ReportVM model)
        //{

        //    // Validate the Model is correct and contains valid data
        //    // Generate your report output based on the model parameters
        //    // This can be an Excel, PDF, Word file - whatever you need.

        //    // As an example lets assume we've generated an EPPlus ExcelPackage

        //    ExcelPackage workbook = new ExcelPackage();
        //    // Do something to populate your workbook

        //    // Generate a new unique identifier against which the file can be stored
        //    string handle = Guid.NewGuid().ToString();

        //    using (MemoryStream memoryStream = new MemoryStream())
        //    {
        //        workbook.SaveAs(memoryStream);
        //        memoryStream.Position = 0;
        //        TempData[handle] = memoryStream.ToArray();
        //    }

        //    // Note we are returning a filename as well as the handle
        //    return new JsonResult()
        //    {
        //        Data = new { FileGuid = handle, FileName = "TestReportOutput.xlsx" }
        //    };

        //}


        [HttpPost]
        public ActionResult Buscar(int cd_estado, int cd_municipio, int cd_colonia)
        {
            ViewBag.UsuarioActivo = true;
            ViewBag.disabled = true;

            var terrenos = from n in db.tsg002_nave_industrial
                           join g in db.tsg009_ni_dt_gral on n.cd_nave equals g.cd_nave
                           where g.nu_disponibilidad > 0 && n.cd_estado == cd_estado
                           select new { n.nb_posicion, n.cd_estado, n.cd_municipio, n.cd_colonia, n.st_parque_ind };
          
            if (cd_municipio != 0)
            {
                terrenos = terrenos.Where(t => t.cd_estado == cd_estado && t.cd_municipio == cd_municipio);

            }

            if (cd_colonia != 0)
            {
                terrenos = terrenos.Where(t => t.cd_estado == cd_estado && t.cd_municipio == cd_municipio && t.cd_colonia == cd_colonia);
            }
           // terrenos = terrenos.Select("new (nb_posicion)");
            // terrenos = terrenos.Select("new (cd_id, nb_archivo, tx_archivo)");
            return new JsonResult { Data = terrenos, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        private string GetViewToString(ControllerContext context, ViewEngineResult result)
        {
            string viewResult = "";
            ViewDataDictionary viewData = new ViewDataDictionary();
            TempDataDictionary tempData = new TempDataDictionary();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                using (HtmlTextWriter output = new HtmlTextWriter(sw))
                {
                    ViewContext viewContext = new ViewContext(context,
                      result.View, viewData, tempData, output);
                    result.View.Render(viewContext, output);
                }
                viewResult = sb.ToString();
            }
            return viewResult;
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
                  rect.Height - 3, 6
                );
                cb.SetRGBColorStroke(17, 9, 182);
                cb.Stroke();
            }
        }


        public String ObteneLetra(int numero)
        {
            switch (numero)
            {
                case 1:
                    return "A";
                case 2:
                    return "B";
                case 3:
                    return "C";
                case 4:
                    return "D";
                case 5:
                    return "E";
                case 6:
                    return "F";
                case 7:
                    return "G";
                case 8:
                    return "H";
                case 9:
                    return "I";
                case 10:
                    return "J";
                case 11:
                    return "K";
                case 12:
                    return "L";
                case 13:
                    return "M";
                case 14:
                    return "N";
                case 15:
                    return "O";
                case 16:
                    return "P";
                case 17:
                    return "Q";
                case 18:
                    return "R";
                case 19:
                    return "S";
                case 20:
                    return "T";
                
            }
            return "-";
        }

        private void GeneraPowerPoint(int cd_estado, int cd_municipio, int cd_colonia)
        {
            try
            {
                reporte_pp rep_pp = new reporte_pp();
                rep_pp.rep_powerpoint(cd_estado, cd_municipio, cd_colonia);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        private void GeneraPDF(int cd_estado, int cd_municipio, int cd_colonia, string cd_titulo, string cd_idioma)
            {
            var terrenos = from n in db.tsg002_nave_industrial
                           join g in db.tsg009_ni_dt_gral on n.cd_nave equals g.cd_nave
                           where g.nu_disponibilidad > 0 && n.cd_estado == cd_estado
                           select new { n.cd_nave, n.nb_parque, n.nb_nave, n.nb_posicion, n.nb_poligono, n.cd_estado, n.cd_municipio, n.cd_colonia, n.st_parque_ind };

            if (cd_municipio != 0)
            {
                terrenos = terrenos.Where(t => t.cd_estado == cd_estado && t.cd_municipio == cd_municipio);

            }

            if (cd_colonia != 0)
            {
                terrenos = terrenos.Where(t => t.cd_estado == cd_estado && t.cd_municipio == cd_municipio && t.cd_colonia == cd_colonia);
            }
            //ControllerContext context = this.ControllerContext;
            //string partialViewName = "PDF";
            var edo = "";
                var t4 = db.tsg037_estados.First(t => t.cd_estado == cd_estado);
                if (t4 != null)
                {
                    edo = t4.nb_estado;
                }

                Document doc = new Document(PageSize.LETTER.Rotate(),0,0,30,30);
                BaseFont Vn_Helvetica = BaseFont.CreateFont(Server.MapPath("~/fonts/" + "arial.ttf"), "Identity-H", BaseFont.EMBEDDED);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/image/" + "reporte.pdf"), FileMode.Create));
                doc.Open();

                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(Vn_Helvetica, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                PdfContentByte cb = writer.DirectContent;

                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/image/" + "logo_CW.png"));
                logo.ScalePercent(50f);

                iTextSharp.text.Image encabezado = iTextSharp.text.Image.GetInstance(Server.MapPath("~/image/" + "TENANT.png"));
                encabezado.ScalePercent(50f);
                encabezado.SetAbsolutePosition(doc.PageSize.Width - 175f, doc.PageSize.Height - 40F);

                PdfPTable table = new PdfPTable(2);
            
                Paragraph p100 = new Paragraph("          Mapa de Ubicación / Location Map", new iTextSharp.text.Font(Vn_Helvetica, 20f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.RED));
                doc.Add(p100);

                Paragraph p101 = new Paragraph("                " + edo, new iTextSharp.text.Font(Vn_Helvetica, 12f, iTextSharp.text.Font.NORMAL, new BaseColor(63, 131, 204)));
                doc.Add(p101);

                Paragraph p102 = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(3.0F, 100.0F, new BaseColor(14, 103, 161), Element.ALIGN_LEFT, 1)));
                doc.Add(p102);

                Paragraph p103 = new Paragraph("\n", new iTextSharp.text.Font(Vn_Helvetica, 12f, iTextSharp.text.Font.NORMAL, new BaseColor(17, 9, 182)));
                doc.Add(p103);

                doc.Add(encabezado);

                table = new PdfPTable(2);
                //table.DefaultCell.Border = Rectangle.NO_BORDER;
                table.TotalWidth = 200f;
                table.HorizontalAlignment = 50;
                float[] widths = new float[] { 60f, 140f };
                table.SetWidths(widths);
                table.LockedWidth = true;
                int grupo = 1;
                PdfPCell cell = new PdfPCell(new Phrase( "Opciones Grupo " + grupo+ " / \nGroup " + grupo + " Options", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
                cell.BackgroundColor = new BaseColor(3, 3, 92);
                cell.BorderColor = new BaseColor(255, 255, 255);
                cell.BorderWidthBottom = 2f;
                cell.BorderWidthLeft = 30f;
                cell.PaddingLeft = 30f;
                cell.PaddingBottom = 2;
                cell.FixedHeight = 40f;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);


                int i = 1;
                int j = 1;

                string posiciones = null;
                string coordenadas = null;
            string[] arrayCoordenadas = null;
                foreach (var titulos in terrenos)
                {

                    if ((i - 1 > 0) && ((i - 1) % 20) == 0)
                    {
                        grupo = grupo + 1;
                    }

                if ((i % 20) == 0)
                {


                    arrayCoordenadas = coordenadas.Split('|');
                    decimal valor = arrayCoordenadas.Count() / 2;
                    int result = Convert.ToInt16(Math.Truncate(valor));
                    //var estado = db.tsg037_estados.Where(t => t.cd_estado == cd_estado).First();

                    //  string strUrl = "https://maps.googleapis.com/maps/api/staticmap?center=" + estado.nb_estado.Replace(" ", "+") + "&zoom=11&size=640x740&maptype=roadmap&" + posiciones + "&key=AIzaSyDhfzatgC_64higR2LlLGkWldmTBudiRWY";
                    string strUrl = "https://maps.googleapis.com/maps/api/staticmap?center=" + arrayCoordenadas[result - 1] + "&zoom=" + cd_titulo + "&size=640x740&maptype=roadmap&" + posiciones + "&key=AIzaSyDhfzatgC_64higR2LlLGkWldmTBudiRWY";

                    Image ubicaciones = Image.GetInstance(strUrl);
                    ubicaciones.ScaleToFit(600f, 500f);
                    ubicaciones.Alignment = iTextSharp.text.Image.UNDERLYING;
                    // Coloca la imagen en una posición absoluta
                    ubicaciones.SetAbsolutePosition(230, 0);
                    doc.Add(ubicaciones);

                    doc.Add(table); //estaba solo table OANR
                }

                if ((i - 1 > 0) && ((i - 1) % 20) == 0)
                {
                    if (titulos.cd_nave != 0)
                    {
                        doc.NewPage();
                    }

                    doc.Add(p100);

                    doc.Add(p101);

                    doc.Add(p102);

                    doc.Add(p103);

                    doc.Add(encabezado);

                    table = new PdfPTable(2);
                    table.TotalWidth = 200f;
                    table.HorizontalAlignment = 50;
                    table.SetWidths(widths);
                    table.LockedWidth = true;

                    cell = new PdfPCell(new Phrase("Opciones Grupo " + grupo + " / Group " + grupo + " Options", new Font(Vn_Helvetica, 12, Font.NORMAL, BaseColor.WHITE)));
                    cell.BackgroundColor = new BaseColor(3, 3, 92);
                    cell.BorderColor = new BaseColor(255, 255, 255);
                    cell.BorderWidthBottom = 2f;
                    cell.BorderWidthLeft = 30f;
                    cell.PaddingLeft = 30f;
                    cell.PaddingBottom = 2f;
                    cell.FixedHeight = 40f;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.Colspan = 2;
                    table.AddCell(cell);

                }



                //-------------------------------------------------------------------------------

                //PdfPCell cell2 = new PdfPCell(new Phrase(i.ToString(), new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));
                // PdfPCell cell2 = new PdfPCell(new Phrase(ObteneLetra(j) + grupo, new Font(Vn_Helvetica, 10, Font.NORMAL, BaseColor.BLACK)));
                PdfPCell cell2 = new PdfPCell(new Phrase(ObteneLetra(j), new Font(Vn_Helvetica, 10, Font.NORMAL, BaseColor.BLACK)));
                    cell2.BackgroundColor = new BaseColor(222, 222, 222);
                    cell2.BorderColor = new BaseColor(255, 255, 255);
                    cell2.BorderWidthLeft = 30f;
                    cell2.PaddingLeft = 30f;
                    cell.BorderWidthBottom = 2f;
                    cell.PaddingBottom = 2f;
                    cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell2);

                    PdfPCell cell1 = new PdfPCell(new Phrase( titulos.nb_parque + " / " + titulos.nb_nave, new Font(Vn_Helvetica, 10, Font.NORMAL, BaseColor.BLACK)));
                    cell1.BackgroundColor = new BaseColor(222, 222, 222);
                    cell1.BorderColor = new BaseColor(255, 255, 255);
                    cell.BorderWidthBottom = 2f;
                    cell.PaddingBottom = 2f;
                    cell1.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell1);
                    coordenadas = coordenadas + titulos.nb_posicion.Trim()+"|";
                    posiciones = posiciones + "markers=color:red%7Clabel:" + ObteneLetra(j) + "%7C" + titulos.nb_posicion.Trim()+"&";
   
                
                    i++;
                    j++;
                    if (j == 21)
                    {
                        j = 1;
                    }    
                }

                if (i < 20 || ((i - 1) % 20) != 0)
                {

                    var estado = db.tsg037_estados.Where(t => t.cd_estado == cd_estado).First();

                    arrayCoordenadas = coordenadas.Split('|');
                    decimal valor = arrayCoordenadas.Count() / 2;
                    int result = Convert.ToInt16(Math.Truncate(valor));


                    string strUrl = "https://maps.googleapis.com/maps/api/staticmap?center=" + arrayCoordenadas[result - 1] + "&zoom="+ cd_titulo + "&size=640x740&maptype=roadmap&" + posiciones + "&key=AIzaSyDhfzatgC_64higR2LlLGkWldmTBudiRWY";

                    Image ubicaciones = Image.GetInstance(strUrl);
                    ubicaciones.ScaleToFit(600f, 500f);
                    ubicaciones.Alignment = iTextSharp.text.Image.UNDERLYING;
                    // Coloca la imagen en una posición absoluta
                    ubicaciones.SetAbsolutePosition(230, 0);
                    doc.Add(ubicaciones);

                    doc.Add(table); //estaba solo table OANR
                }

                //------------------------------------------------------------------------------------------------------------------


                // pagina para poligonos
                int y = 1;
                int y1 = 1;
                int grupo1 = 1; 
                foreach (var terreno in terrenos)
                {
                    doc.SetMargins(50, 50, 30, 30);
                    doc.NewPage();

                    encabezado.SetAbsolutePosition(doc.PageSize.Width - 175f, doc.PageSize.Height - 40F);
                    doc.Add(encabezado);

                    if ((y - 1 > 0) && ((y - 1) % 20) == 0)
                    {
                        grupo1 = grupo1 + 1;
                        y1 = 1;
                    }

                    Paragraph titulo = new Paragraph("Grupo / Group " + grupo1 + "\nOpción / Option " + ObteneLetra(y1) + ": " + terreno.nb_parque + " / " + terreno.nb_nave, new iTextSharp.text.Font(Vn_Helvetica, 18f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.RED));
                    doc.Add(titulo);
                    Paragraph subtitulo = new Paragraph("Plan Maestro / Master Plan", new iTextSharp.text.Font(Vn_Helvetica, 12f, iTextSharp.text.Font.NORMAL, new BaseColor(63, 131, 204)));
                    doc.Add(subtitulo);

                    Paragraph p1 = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(3.0F, 100.0F, new BaseColor(14, 103, 161), Element.ALIGN_LEFT, 1)));
                    doc.Add(p1);

                    string strUrlPoligono = "https://maps.googleapis.com/maps/api/staticmap?center=" + terreno.nb_posicion + "&zoom=15&size=840x640&maptype=hybrid&path=fillcolor:red|" + terreno.nb_poligono.TrimEnd('|') + " & key=AIzaSyDhfzatgC_64higR2LlLGkWldmTBudiRWY";
                    Image poligono = Image.GetInstance(strUrlPoligono);
                    poligono.ScaleAbsolute(700F, 430F);
                    poligono.SetAbsolutePosition(50, 60);
                    doc.Add(poligono);
                
                    logo.ScalePercent(50f);
                    logo.SetAbsolutePosition(doc.PageSize.Width - 175f, doc.PageSize.Height - 10f - 580f);
                    doc.Add(logo);
                
                    // pagina para poliginos
                    doc.NewPage();
                    encabezado.SetAbsolutePosition(doc.PageSize.Width - 175f, doc.PageSize.Height - 40F);
                    doc.Add(encabezado);
                    doc.Add(titulo);
                    Paragraph subtitulo2 = new Paragraph("Diseño / Layout", new iTextSharp.text.Font(Vn_Helvetica, 12f, iTextSharp.text.Font.NORMAL, new BaseColor(63, 131, 204)));
                    doc.Add(subtitulo2);
                               
                    Paragraph p2 = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(3.0F, 100.0F, new BaseColor(14, 103, 161), Element.ALIGN_LEFT, 1)));
                    p2.SpacingBefore = 1f;
                    p2.SpacingAfter = 3f;
                    doc.Add(p2);

                    PdfPTable table2 = new PdfPTable(3);
                    table2.TotalWidth = 700;
                    table2.SetWidths(new int[] { 250, 250, 200 });                
                    //table2.WidthPercentage = 90f;
                    table2.WriteSelectedRows(5,28,150f,100f, writer.DirectContent);
                    table2.LockedWidth = true;
                    table2.HorizontalAlignment = 150;
                    PdfPCell header2 = new PdfPCell(new Phrase(terreno.nb_nave));
                    header2.Colspan = 3;
                    table.AddCell(header2);
                
                    var datos_gral = db.tsg009_ni_dt_gral.Where(t => t.cd_nave == terreno.cd_nave).First();
                    var precio = db.tsg023_ni_precio.Where(t => t.cd_nave == terreno.cd_nave).First();
                    var servicios = db.tsg020_ni_servicio.Where(t => t.cd_nave == terreno.cd_nave).First();
                    var imagenes = db.tsg045_imagenes_naves.Where(t => t.cd_nave == terreno.cd_nave && t.cd_reporte == 1);
                    // Imagen de Layout
                    string urlImage = "";

                    if (imagenes.Count() > 0)
                    {

                        //if (imagenes.First().tx_archivo.ToString().Contains("image"))
                        //{
                        //    urlImage = imagenes.First().tx_archivo;
                        //}
                        //else
                        //{
                        //    urlImage = descargaBlob(imagenes.First().nb_archivo);
                        //}
                        urlImage = descargaBlob(terreno.cd_nave,imagenes.First().nb_archivo);
                    }

                    Image img;
                    try
                    {
                        if (urlImage != "")
                        {
                            img = Image.GetInstance(urlImage);
                            img.ScaleToFit(500f, 400f);
                            img.Alignment = Element.ALIGN_CENTER; 
                            //img.SetAbsolutePosition(500, 0);
                            PdfPCell bottom2 = new PdfPCell(img);
                            bottom2.BorderColor = new BaseColor(255, 255, 255);
                            bottom2.Colspan = 2;
                            bottom2.Rowspan = 8; 
                            table2.AddCell(bottom2);
                            //doc.Add(table2);
                        }else
                        {
                            PdfPCell bottom2 = new PdfPCell(new Phrase("", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                            bottom2.BorderColor = new BaseColor(255, 255, 255);
                            bottom2.Colspan = 2;
                            bottom2.Rowspan = 8;
                            table2.AddCell(bottom2);
                    }
                }
                    catch (Exception ex)
                    {
                        PdfPCell bottom2 = new PdfPCell(new Phrase("", new iTextSharp.text.Font(Vn_Helvetica, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                        bottom2.BorderColor = new BaseColor(255, 255, 255);
                        bottom2.Colspan = 2;
                        bottom2.Rowspan = 8;
                        table2.AddCell(bottom2);
                }


                Double tp_cmb = 1;
                    var moneda = ""; // por dafault pesos
                    var operador = "*";
                    if (precio.cd_moneda != precio.cd_rep_moneda)
                    {
                        if (precio.cd_moneda != 0) // Se verifica moneda en que fué guardado el registro
                        {
                            var mon = db.tsg044_tipos_monedas.Where(t => t.cd_moneda == precio.cd_moneda).First();
                            if (mon.nb_moneda == "USD") // Dolares
                            {
                                operador = "*";
                               // moneda = 1;
                                if (precio.nu_tipo_cambio != null && precio.nu_tipo_cambio != 0){ 
                                    tp_cmb = (double)precio.nu_tipo_cambio;
                                }
                            }
                            else
                            {                            
                                if (mon.nb_moneda == "MXN") // Pesos
                                {
                                    operador = "/";
                                    //moneda = 2;
                                    if (precio.nu_tipo_cambio != null && precio.nu_tipo_cambio != 0)
                                    {
                                        tp_cmb = (double)precio.nu_tipo_cambio;
                                    }
                                }
                            }
                        }
                    }
                
                    if (precio.cd_rep_moneda != 0)
                    {
                        var mon = db.tsg044_tipos_monedas.Where(t => t.cd_moneda == precio.cd_rep_moneda).First();
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
                    double p = 0;
                    if (precio.im_venta != null)
                    {
                        p = (Double)precio.im_venta;
                    }
                    if (operador == "*")
                    {                    
                        preciofinal =  p * tp_cmb;
                    }else
                    {
                        preciofinal = p / tp_cmb;
                    }
                
                    PdfPTable nested2 = new PdfPTable(1);

                    nested2.DefaultCell.Border = Rectangle.NO_BORDER;

                    var cat_area = datos_gral.cd_area != null ? db.tsg010_area_of.Where(t => t.cd_area == datos_gral.cd_area).First() : null;
                    var cat_piso = datos_gral.cd_carga != null ? db.tsg011_carga_piso.Where(t => t.cd_carga == datos_gral.cd_carga).First() : null;
                    var cat_sist_inc = datos_gral.cd_sist_inc != null ? db.tsg012_sist_incendio.Where(x => x.cd_sist_inc == datos_gral.cd_sist_inc).First() : null;
                    var cat_constr = datos_gral.cd_tp_construccion != null ? db.tsg013_tp_construccion.Where(x => x.cd_tp_construccion == datos_gral.cd_tp_construccion).First() : null ;
                    var cat_entrega = terreno.st_parque_ind != null ? db.tsg017_st_entrega.Where(x => x.cd_st_entrega == terreno.st_parque_ind).First():null;
                    var cat_lamparas = datos_gral.cd_tp_lampara != null ? db.tsg014_tp_lampara.Where(x => x.cd_tp_lampara == datos_gral.cd_tp_lampara).First() : null;
                    var cat_iluminacion = datos_gral.cd_ilum_nat != null ? db.tsg018_ilum_nat.Where(x => x.cd_ilum_nat == datos_gral.cd_ilum_nat).First() : null ;
                    //var cat_codiciones = db.tsg024_cond_arr.Where(x => x.cd_cond_arr == precio.cd_cond_arr).First();

                    nested2.AddCell(new Phrase(" - Superficie Nave / Building Area:" + String.Format("{0:#,##0.00}", datos_gral.nu_superficie) + " M2 / "+ String.Format("{0:#,##0.00}", Convert.ToDouble(datos_gral.nu_superficie) * 10.7639) + " Ft2\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));
                    nested2.AddCell(new Phrase(" - Superficie Terreno / Land Size:" + String.Format("{0:#,##0.00}", datos_gral.nu_bodega) + " M2 / " + String.Format("{0:#,##0.00}", Convert.ToDouble(datos_gral.nu_bodega) * 10.7639) + " Ft2\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));
                    nested2.AddCell(new Phrase(" - Disponibilidad Total / Total Available:" + String.Format("{0:#,##0.00}", datos_gral.nu_disponibilidad) + " M2 / " + String.Format("{0:#,##0.00}", Convert.ToDouble(datos_gral.nu_disponibilidad) * 10.7639) + " Ft2\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));
                    nested2.AddCell(new Phrase(" - Mínimo divisible / Minimum Divisible:" + String.Format("{0:#,##0.00}", datos_gral.nu_min_divisible) + " M2 / " + String.Format("{0:#,##0.00}", Convert.ToDouble(datos_gral.nu_min_divisible) * 10.7639) + " Ft2\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));

                if (cat_area != null)
                {
                    nested2.AddCell(new Phrase(" - Área de Oficina / Office Area:" +  cat_area.nb_area  + "\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));
                }
                else
                {
                    nested2.AddCell(new Phrase(" - Área de Oficina / Office Area:" + "\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));

                }
                if (precio.im_renta > 0 )
                {
                    double im_renta = Convert.ToDouble(precio.im_renta);
                    nested2.AddCell(new Phrase(" - Precio de Renta / Rent Price:"+ im_renta.ToString("C2", CultureInfo.CreateSpecificCulture("en-us")) + "M2/mes – Ft2/month\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));
                }
                if (precio.im_venta > 0) {
                    double im_venta = Convert.ToDouble(precio.im_venta);
                    nested2.AddCell(new Phrase(" - Precio de Venta / Sale Price:" + im_venta.ToString("C2", CultureInfo.CreateSpecificCulture("en-us")) + "M2/mes – Ft2/month\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));
                    }
                    if (precio.cd_cond_arr != null)
                    {
                        var cat_codiciones = db.tsg024_cond_arr.Where(x => x.cd_cond_arr == precio.cd_cond_arr).First();
                        double nu_ma_ac = Convert.ToDouble(precio.nu_ma_ac);
                        nested2.AddCell(new Phrase(" - Cuota de Mantenimiento " + cat_codiciones.nb_cond_arr + " : " + nu_ma_ac.ToString("C2", CultureInfo.CreateSpecificCulture("en-us")) + "\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));
                    }
                    else
                    {
                        double nu_ma_ac = Convert.ToDouble(precio.nu_ma_ac);
                        nested2.AddCell(new Phrase(" - " + " : " + nu_ma_ac.ToString("C2", CultureInfo.CreateSpecificCulture("en-us")) + "\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));
                    }
                    nested2.AddCell(new Phrase(" - Tipo de Cambio / Exchange Rate:" + precio.nu_tipo_cambio + "\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));
                    nested2.AddCell(new Phrase(" - Altura libre / Clear Height:" + String.Format("{0:#,##0.00}", datos_gral.nu_altura) + " M / " + String.Format("{0:#,##0.00}", Convert.ToDouble(datos_gral.nu_altura) * 10.7639) + " Ft\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));
                    nested2.AddCell(new Phrase(" - Puertas Andén / Dock Doors:" + datos_gral.nu_puertas + "\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));
                    nested2.AddCell(new Phrase(" - Rampa / Drive in Door:" + datos_gral.nu_rampas + "\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));

                if (cat_piso != null)
                {
                    nested2.AddCell(new Phrase(" - Capacidad de carga piso / Floor Load Capacity:" + cat_piso.nb_carga + "\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));
                }
                else
                {
                    nested2.AddCell(new Phrase(" - Capacidad de carga piso / Floor Load Capacity:" +  "\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));
                }
                if (cat_sist_inc != null)
                {


                    nested2.AddCell(new Phrase(" - Sistema contra incendios / Fire Protection System:" +  cat_sist_inc.nb_sist_inc  + "\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));
                }
                else
                {
                    nested2.AddCell(new Phrase(" - Sistema contra incendios / Fire Protection System:" +  "\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));

                }
                //if (cat_constr != null)
                //{
                //    nested2.AddCell(new Phrase(" - Tipo construcción / Construction Type:" +  cat_constr.nb_tp_construccion  + "\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));
                //}
                //else
                //{
                //    nested2.AddCell(new Phrase(" - Tipo construcción / Construction Type:"  + "\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));
                //}
                if (cat_constr != null)
                {
                    nested2.AddCell(new Phrase(" - Tipo construcción / Construction Type:" + cat_constr.nb_tp_construccion + "\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));
                }
                else {
                    nested2.AddCell(new Phrase(" - Tipo construcción / Construction Type:" +  "\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));
                }
                if(cat_entrega != null)
                {
                    nested2.AddCell(new Phrase(" - Estatus de entrega / Delivery Status:" + cat_entrega.nb_st_entrega  + "\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));
                }
                else
                {
                    nested2.AddCell(new Phrase(" - Estatus de entrega / Delivery Status:"  + "\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));
                }

                if (cat_lamparas != null)
                {
                    nested2.AddCell(new Phrase(" - Lámparas / Lighting Fixtures:" +  cat_lamparas.nb_tp_lampara + "\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));

                }
                else
                {
                    nested2.AddCell(new Phrase(" - Lámparas / Lighting Fixtures:" + "\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));

                }
                if (cat_iluminacion != null)
                {
                    nested2.AddCell(new Phrase(" - Iluminación natural / Skylights:" + cat_iluminacion.nb_ilum_nat + "\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));

                }
                else
                {
                    nested2.AddCell(new Phrase(" - Iluminación natural / Skylights:" +  "\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));

                }
                //nested2.AddCell(new Phrase(" - Precio/Price: $" + String.Format("{0:#,##0.00}", preciofinal) + " " + moneda + "\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK))); 
                //nested2.AddCell(new Phrase(" - Altura libre/Clear height: " + String.Format("{0:#,##0.00}",datos_gral.nu_altura) + "\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));
                //if (datos_gral.cd_espesor != null)
                //{
                //    var espesor = db.tsg016_espesor.Where(t => t.cd_espesor == datos_gral.cd_espesor).First();
                //    nested2.AddCell(new Phrase(" - Espesor de suelo/Floor Thickness: " + espesor.nb_espesor + "\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));
                //}
                //else
                //{
                //    nested2.AddCell(new Phrase(" - Espesor de suelo/Floor Thickness: " + "\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));
                //}
                //nested2.AddCell(new Phrase(" - Transformador/Transformer: " + servicios.nu_kva + "\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));
                //nested2.AddCell(new Phrase(" - Domos/Skylights: " + datos_gral.n_ilum_nat + "\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));
                //nested2.AddCell(new Phrase(" - Muelle de carga/Loading Docks: " + datos_gral.nb_cajon_est + "\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));
                //nested2.AddCell(new Phrase(" - Conducción de puertas/Drive in doors: " + datos_gral.nb_carga + "\n", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));
                //if (datos_gral.cd_sist_inc != null) {
                //    var sist_inc = db.tsg012_sist_incendio.Where(t => t.cd_sist_inc == datos_gral.cd_sist_inc).First();
                //    nested2.AddCell(new Phrase("- Protección de fuego/Fire Protection: " + sist_inc.nb_sist_inc + "\n ", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));
                //}
                //else
                //{
                //    nested2.AddCell(new Phrase(" - Protección de fuego/Fire Protection: " + "\n ", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));
                //}
                nested2.AddCell(new Phrase(" ", new Font(Vn_Helvetica, 9, Font.NORMAL, BaseColor.BLACK)));
                //nested2.AddCell(new Phrase("- Prepared for Sprinklers: " + datos_gral.nb_espesor, new Font(Vn_Helvetica, 8, Font.NORMAL, BaseColor.BLACK)));

                // Phrase info = new Phrase(nested2, new Font(Vn_Helvetica, 8, Font.NORMAL, BaseColor.WHITE));
                PdfPCell nesthousing2 = new PdfPCell(nested2);
                    //, new Font(Vn_Helvetica, 8, Font.NORMAL, BaseColor.WHITE)
             
                    nesthousing2.Padding = 3f;
                    //nesthousing2.BackgroundColor = new BaseColor(27, 79, 114);
                    //nesthousing2.BorderColor = new BaseColor(27, 79, 114);
                    nesthousing2.Border = PdfPCell.NO_BORDER;
                    nesthousing2.CellEvent = new RoundedBorder();                

                    PdfPCell c1 = new PdfPCell(new Phrase(" ", new Font(Vn_Helvetica, 10, Font.NORMAL, BaseColor.BLACK)));
                    c1.Border = PdfPCell.NO_BORDER;                
                    table2.AddCell(c1);

                    PdfPCell c2 = new PdfPCell(new Phrase(" ", new Font(Vn_Helvetica, 10, Font.NORMAL, BaseColor.BLACK)));
                    c2.Border = PdfPCell.NO_BORDER;
                    table2.AddCell(c2);

                    PdfPCell c3 = new PdfPCell(new Phrase(" ", new Font(Vn_Helvetica, 10, Font.NORMAL, BaseColor.BLACK)));
                    c3.Border = PdfPCell.NO_BORDER;
                    table2.AddCell(c3);

                    PdfPCell c4 = new PdfPCell(new Phrase(" ", new Font(Vn_Helvetica, 10, Font.NORMAL, BaseColor.BLACK)));
                    c4.Border = PdfPCell.NO_BORDER;
                    table2.AddCell(c4);

                    table2.AddCell(nesthousing2);

                    PdfPCell c5 = new PdfPCell(new Phrase(" ", new Font(Vn_Helvetica, 10, Font.NORMAL, BaseColor.BLACK)));
                    c5.Border = PdfPCell.NO_BORDER;
                    table2.AddCell(c5);

                    PdfPCell c6 = new PdfPCell();
                    c6.Border = PdfPCell.NO_BORDER;
                    table2.AddCell(c6);
                    PdfPCell c7 = new PdfPCell();
                    c7.Border = PdfPCell.NO_BORDER;
                    table2.AddCell(c7);
                    PdfPCell c8 = new PdfPCell();
                    c8.Border = PdfPCell.NO_BORDER;
                    table2.AddCell(c8);
                
                    doc.Add(table2);

                    logo.ScalePercent(50f);
                    logo.SetAbsolutePosition(doc.PageSize.Width - 175f, doc.PageSize.Height - 10f - 580f);
                    doc.Add(logo);
                    y++;
                    y1++;
                }
                //AddHTMLText(doc, htmlTextView);
                doc.Close();
                writer.Close();
                uploadfile(Server.MapPath("~/image/" + "reporte.pdf"));
            }
        private void AddHTMLText(iTextSharp.text.Document doc, string html)
        {
            //List & lt;iTextSharp.text.IElement&gt;htmlarraylist = HTMLWorker.ParseToList(new StringReader(html), null);

            //foreach (var item in htmlarraylist)
            //{
            //    doc.Add(item);
            //}
        }
        private static void uploadfile(string ruta)
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
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("reporte.pdf");

            // Create or overwrite the "myblob" blob with contents from a local file.
            using (var fileStream = System.IO.File.OpenRead(ruta))
            {
                blockBlob.UploadFromStream(fileStream);
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
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("reporte.pptx");

            // Create or overwrite the "myblob" blob with contents from a local file.
            using (var fileStream = System.IO.File.OpenRead(ruta))
            {
                blockBlob.UploadFromStream(fileStream);
            }
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
        private static string descargaBlob(int id, String strFileName)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("cushmandocs/imagenes/N"+id);

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
    }
}