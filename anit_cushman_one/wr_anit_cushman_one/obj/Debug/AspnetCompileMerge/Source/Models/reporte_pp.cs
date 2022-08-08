using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Web.Mvc;
using System.Web.UI;
using System.Web;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
using wr_anit_cushman_one.Models;
using wr_anit_cushman_one.Controllers;
using Microsoft.Office.Interop.PowerPoint;
using System.Drawing;
namespace wr_anit_cushman_one.Models
{
    public class reporte_pp
    {
        CushmanContext db = new CushmanContext();
        public void rep_powerpoint(int cd_estado, int cd_municipio, int cd_colonia)
        {
            try
            {

                ReportNavesController reporController = new ReportNavesController();
                Application pptAplication = new Application();
                //pptAplication.Visible = Microsoft.Office.Core.MsoTriState.
                Presentation pptpresentation = pptAplication.Presentations.Add(Microsoft.Office.Core.MsoTriState.msoFalse);
                //pptpresentation.Application.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;

                Microsoft.Office.Interop.PowerPoint.Slides slides;
                Microsoft.Office.Interop.PowerPoint._Slide slide = null;
                Microsoft.Office.Interop.PowerPoint.TextFrame objFrame;
                Microsoft.Office.Interop.PowerPoint.TextRange objText;
                Microsoft.Office.Interop.PowerPoint.LineFormat objLine;
                Microsoft.Office.Interop.PowerPoint.PictureFormat objFiguras;


                Microsoft.Office.Interop.PowerPoint.CustomLayout custLayout =
                        pptpresentation.SlideMaster.CustomLayouts[Microsoft.Office.Interop.PowerPoint.PpSlideLayout.ppLayoutText];

                slides = pptpresentation.Slides;


                List<tsg002_nave_industrial> lnaves = listNaves(cd_estado, cd_municipio, cd_colonia);

                tsg037_estados estados = obtenestadoid(cd_estado);//obtengo el estado que se busco.

                Microsoft.Office.Interop.PowerPoint.Shape oShape = null;




                int i = 1;
                string letra;
                int grupo = 1;
                int objeto = 1;
                string coordenadas = null;
                string[] arrayCoordenadas = null;
                string posiciones = null;
                foreach (var nave in lnaves)
                {


                    if (i == 1)
                    {
                        slide = slides.AddSlide(grupo, custLayout);

                        slide.Shapes[objeto].Left = 4;  //posicionar el frame a la izquierda          
                        slide.Shapes[objeto].Top = 4;  // posicionar el farme en la parte superior
                        slide.Shapes[objeto].Height = 20; //Tamaño del frame

                        objFrame = slide.Shapes[objeto].TextFrame;


                        objText = objFrame.TextRange;

                        objText.Text = "Mapa de Ubicacion / Location Map";
                        objText.Font.Name = "Arial";
                        objText.Font.Size = 20;
                        objText.Font.Color.RGB = Color.Red.ToArgb();

                        objeto++;
                        slide.Shapes[objeto].Left = 12;
                        slide.Shapes[objeto].Top = 20;
                        slide.Shapes[objeto].Height = 10;



                        objFrame = slide.Shapes[objeto].TextFrame;
                        objText = objFrame.TextRange;
                        objText.Text = estados.nb_estado;
                        objText.Font.Name = "Arial";
                        objText.Font.Size = 12;
                        objText.Font.Color.RGB = Color.Blue.ToArgb();


                        int iRows = 20 + 1;
                        int iColumns = 1;



                        oShape = slide.Shapes.AddTable(iRows, iColumns, 4, 35, 200, 50);
                        oShape.Table.Cell(i, 1).Shape.TextFrame.TextRange.Text = "Opciones Grupo " + grupo + " / Group " + grupo + " Options";
                        oShape.Table.Cell(i, 1).Shape.TextFrame.TextRange.Font.Name = "Arial";
                        oShape.Table.Cell(i, 1).Shape.TextFrame.TextRange.Font.Size = 10;
                        oShape.Table.Cell(i, 1).Shape.TextFrame.TextRange.Font.Bold = Microsoft.Office.Core.MsoTriState.msoTrue;
                        grupo++;
                    }

                    if (i <= 20)
                    {
                        letra = reporController.ObteneLetra(i);
                        oShape.Table.Cell(i + 1, 1).Shape.TextFrame.TextRange.Text = letra + " - " + nave.nb_parque + " / " + nave.nb_nave;
                        oShape.Table.Cell(i + 1, 1).Shape.TextFrame.TextRange.Font.Name = "Arial";
                        oShape.Table.Cell(i + 1, 1).Shape.TextFrame.TextRange.Font.Size = 8;
                        if (nave.nb_posicion == null)
                        {
                            coordenadas = coordenadas + "";
                        }
                        else
                        {
                            coordenadas = coordenadas + nave.nb_posicion.Trim() + "|";

                            posiciones = posiciones + "markers=color:red%7Clabel:" + letra + "%7C" + nave.nb_posicion.Trim() + "&";
                        }
                    }
                    if (i == 20)
                    {
                        Microsoft.Office.Interop.PowerPoint.Shape shape = slide.Shapes[objeto];
                        arrayCoordenadas = coordenadas.Split('|');
                        decimal valor = arrayCoordenadas.Count() / 2;
                        int result = Convert.ToInt16(Math.Truncate(valor));
                        string imagemapa = "https://maps.googleapis.com/maps/api/staticmap?center=" + arrayCoordenadas[result - 1] + "&zoom =14&size=400x400&&maptype=roadmap&" + posiciones + "key=AIzaSyDhfzatgC_64higR2LlLGkWldmTBudiRWY";

                        slide.Shapes.AddPicture(imagemapa, Microsoft.Office.Core.MsoTriState.msoFalse,
                        Microsoft.Office.Core.MsoTriState.msoTrue, 350, 35, 600, 500);
                        i = 0;
                        objeto = 1;
                        coordenadas = null;
                        posiciones = null;

                    }
                    i++;

                }

                if (i > 1)
                {
                    Microsoft.Office.Interop.PowerPoint.Shape shape = slide.Shapes[objeto];
                    arrayCoordenadas = coordenadas.Split('|');
                    decimal valor = arrayCoordenadas.Count() / 2;
                    int result = Convert.ToInt16(Math.Truncate(valor));
                    string imagemapa = "https://maps.googleapis.com/maps/api/staticmap?center=" + arrayCoordenadas[result - 1] + "&zoom =14&size=400x400&&maptype=roadmap&" + posiciones + "key=AIzaSyDhfzatgC_64higR2LlLGkWldmTBudiRWY";

                    slide.Shapes.AddPicture(imagemapa, Microsoft.Office.Core.MsoTriState.msoFalse,
                    Microsoft.Office.Core.MsoTriState.msoTrue, 350, 35, 600, 500);
                    i = 0;
                    objeto = 1;
                    coordenadas = null;
                    posiciones = null;
                }

                i = 1;
                // grupo = 1;
                objeto = 1;

                //Detalle del reporte
                foreach (var nave in lnaves)
                {

                    letra = reporController.ObteneLetra(i);

                    slide = slides.AddSlide(grupo, custLayout);

                    slide.Shapes[objeto].Left = 4;  //posicionar el frame a la izquierda          
                    slide.Shapes[objeto].Top = 4;  // posicionar el farme en la parte superior
                    slide.Shapes[objeto].Height = 20; //Tamaño del frame

                    objFrame = slide.Shapes[objeto].TextFrame;


                    objText = objFrame.TextRange;

                    objText.Text = "Grupo"+ grupo +" / Group "+ i + "\n" + "Opción / Option " + letra + ";"+ nave.nb_nave  + "/" + nave.nb_parque;
                    objText.Font.Name = "Arial";
                    objText.Font.Size = 20;
                    objText.Font.Color.RGB = Color.Red.ToArgb();

                    /*objeto++;
                    slide.Shapes[objeto].Left = 12;
                    slide.Shapes[objeto].Top = 20;
                    slide.Shapes[objeto].Height = 10;



                    objFrame = slide.Shapes[objeto].TextFrame;
                    objText = objFrame.TextRange;
                    objText.Text = "Plan Maestro / Master Plan";
                    objText.Font.Name = "Arial";
                    objText.Font.Size = 12;
                    objText.Font.Color.RGB = Color.Blue.ToArgb();*/

                    Microsoft.Office.Interop.PowerPoint.Shape shape = slide.Shapes[objeto];
                    //arrayCoordenadas = coordenadas.Split('|');
                    //decimal valor = arrayCoordenadas.Count() / 2;
                    //int result = Convert.ToInt16(Math.Truncate(valor));
                    string strUrlPoligono = "https://maps.googleapis.com/maps/api/staticmap?center=" + nave.nb_posicion + "&zoom=15&size=840x640&maptype=hybrid&path=fillcolor:red|" + nave.nb_poligono.TrimEnd('|') + " & key=AIzaSyDhfzatgC_64higR2LlLGkWldmTBudiRWY";

                    slide.Shapes.AddPicture(strUrlPoligono, Microsoft.Office.Core.MsoTriState.msoFalse,
                    Microsoft.Office.Core.MsoTriState.msoTrue, 4, 40, 950, 500);




                    grupo++;
                    objeto = 1;
                    i = 0;

                    i++;


                    /************empieza la parte del plano y detalle*///
                    
                    slide = slides.AddSlide(grupo, custLayout);

                    slide.Shapes[objeto].Left = 4;  //posicionar el frame a la izquierda          
                    slide.Shapes[objeto].Top = 4;  // posicionar el farme en la parte superior
                    slide.Shapes[objeto].Height = 20; //Tamaño del frame

                    objFrame = slide.Shapes[objeto].TextFrame;


                    objText = objFrame.TextRange;

                    objText.Text = "Grupo"+ grupo +" / Group "+ i + "\n" + "Opción / Option " + letra + ";"+ nave.nb_nave  + "/" + nave.nb_parque;
                    objText.Font.Name = "Arial";
                    objText.Font.Size = 20;
                    objText.Font.Color.RGB = Color.Red.ToArgb();

                    /*objeto++;
                    slide.Shapes[objeto].Left = 12;
                    slide.Shapes[objeto].Top = 20;
                    slide.Shapes[objeto].Height = 10;



                    objFrame = slide.Shapes[objeto].TextFrame;
                    objText = objFrame.TextRange;
                    objText.Text = "Plan Maestro / Master Plan";
                    objText.Font.Name = "Arial";
                    objText.Font.Size = 12;
                    objText.Font.Color.RGB = Color.Blue.ToArgb();*/

                    Microsoft.Office.Interop.PowerPoint.Shape shape3 = slide.Shapes[objeto];
                    //arrayCoordenadas = coordenadas.Split('|');
                    //decimal valor = arrayCoordenadas.Count() / 2;
                    //int result = Convert.ToInt16(Math.Truncate(valor));

                    tsg045_imagenes_naves imagenNave = obteneImagenesNaves(nave.cd_nave);

                    

                    //string strUrlImagen = reporController.descargaBlob(nave.cd_nave, imagenNave.nb_archivo);
                    //Uri urlImageNave = new Uri(strUrlImagen,UriKind.Absolute);
                    /*slide.Shapes.AddPicture(strUrlImagen.Trim('"'), Microsoft.Office.Core.MsoTriState.msoTrue,
                    Microsoft.Office.Core.MsoTriState.msoTrue, 4, 40, 700, 500);*/


                    /// Genera el rectangulo
                    float[,] points = new float[,] { { 100, 100 }, { 172, 100 }, { 172, 172 }, { 100, 172 }, { 100, 100 } };
                    //var x = slide.Shapes.AddPolyline(points).TextFrame.TextRange.Text = "x";
                    var x = slide.Shapes.AddPolyline(points);
                    x.TextFrame.TextRange.Text = "x";
                    x.TextFrame.TextRange.Font.Name = "Arial";
                    x.TextFrame.TextRange.Font.Size = 20;


                    grupo++;
                    objeto = 1;
                    i = 0;

                    i++;

                }



                //slide = slides.AddSlide(2, custLayout);



                pptpresentation.SaveAs(System.Web.HttpContext.Current.Server.MapPath("~/image/" + "reporte.pptx"),
                    Microsoft.Office.Interop.PowerPoint.PpSaveAsFileType.ppSaveAsDefault,
                    Microsoft.Office.Core.MsoTriState.msoTrue);

                pptpresentation.Close();
                pptAplication.Quit();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private tsg045_imagenes_naves obteneImagenesNaves(int cd_nave)
        {
            var imagenesNaves =  db.tsg045_imagenes_naves.Where(t => t.cd_nave == cd_nave && t.cd_reporte == 1);

            tsg045_imagenes_naves imgNaves = null;

            foreach( var a in imagenesNaves)
            {
                imgNaves = a;
            }

            return imgNaves;
        }
        private tsg037_estados obtenestadoid(int cd_estado)
        {

            tsg037_estados estados = db.tsg037_estados.First(t => t.cd_estado == cd_estado);
            return estados;
        }

        private List<tsg002_nave_industrial> listNaves(int cd_estado, int cd_municipio, int cd_colonia)
        {
            List<tsg002_nave_industrial> lnaves = db.tsg002_nave_industrial.Where(t => t.cd_estado == cd_estado).ToList();

            if (cd_municipio != 0)
            {
                lnaves = lnaves.Where(t => t.cd_estado == cd_estado && t.cd_municipio == cd_municipio).ToList();

            }

            if (cd_colonia != 0)
            {
                lnaves = lnaves.Where(t => t.cd_estado == cd_estado && t.cd_municipio == cd_municipio && t.cd_colonia == cd_colonia).ToList();
            }

            return lnaves;
        }
    }
}