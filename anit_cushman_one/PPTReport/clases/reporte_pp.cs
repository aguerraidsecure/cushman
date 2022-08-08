using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Spire.Presentation;
using Spire.Presentation.Drawing;
using Spire.Presentation.Drawing.Transition;
using PPTReport.Model;
using PPTReport.dao;
using PPTReport.clases;
using System.Globalization;

namespace PPTReport.clases
{
    public static class reporte_pp
    {

        public static void repPpNaves_ind(int? id, int lng, String ruta)
        {
            reporteIndividual(ruta, id);
        }

        public static void repPpTerr_ind(int? id, int lng, String ruta)
        {
            reporteIndividualTerr(ruta, id);
        }

        private static void reporteIndividualTerr(String ruta, int? id)
        {

            clsNavesDao navesdao = new clsNavesDao();

            var terrenos = navesdao.obtenDatosTerrenos(id);

            int diapositiva = 0;
            PPTReport.ModifyInMemory.ActivateMemoryPatching();
            Presentation ppt = new Presentation();
            ISlide slide = ppt.Slides[diapositiva];
            RectangleF titleRect = new RectangleF(10, 10, 700, 25);
            IAutoShape titleShape = slide.Shapes.AppendShape(ShapeType.Rectangle, titleRect);
            titleShape.Fill.FillType = FillFormatType.Solid;
            titleShape.Fill.SolidColor.Color = Color.MidnightBlue;
            titleShape.ShapeStyle.LineColor.Color = Color.MidnightBlue;
            TextParagraph titlePara = titleShape.TextFrame.Paragraphs[0];
            titlePara.Text = terrenos.nb_comercial;
            titlePara.FirstTextRange.FontHeight = 20;
            titlePara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            titlePara.FirstTextRange.Fill.SolidColor.Color = Color.White;
            titlePara.Alignment = TextAlignmentType.Left;

            if (terrenos.cd_estado != null && terrenos.cd_municipio != null && terrenos.cd_colonia != null)
            {
                var municipio = navesdao.obteneMunicipio(terrenos.cd_estado, terrenos.cd_municipio);

                var colonia = navesdao.obtenColonia(terrenos.cd_estado, terrenos.cd_municipio, terrenos.cd_colonia);

                RectangleF dirRect = new RectangleF(10, 40, 234, 50);
                IAutoShape dirShape = slide.Shapes.AppendShape(ShapeType.Rectangle, dirRect);
                dirShape.Fill.FillType = FillFormatType.None;
                dirShape.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph dirPara = dirShape.TextFrame.Paragraphs[0];
                dirPara.Text = terrenos.nb_calle + " No." + terrenos.nu_direcion + "\n" + colonia.nb_colonia + "\n" + municipio.nb_municipio;
                dirPara.FirstTextRange.FontHeight = 10;
                dirPara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                dirPara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                dirPara.FirstTextRange.IsBold = TriState.True;
                dirPara.Alignment = TextAlignmentType.Left;
            }
            else
            {
                RectangleF dirRect = new RectangleF(10, 40, 234, 50);
                IAutoShape dirShape = slide.Shapes.AppendShape(ShapeType.Rectangle, dirRect);
                dirShape.Fill.FillType = FillFormatType.None;
                dirShape.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph dirPara = dirShape.TextFrame.Paragraphs[0];
                dirPara.Text = " ";
                dirPara.FirstTextRange.FontHeight = 10;
                dirPara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                dirPara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                dirPara.FirstTextRange.IsBold = TriState.True;
                dirPara.Alignment = TextAlignmentType.Left;
            }
            RectangleF planoRect = new RectangleF(244, 40, 233, 50);
            IAutoShape planoShape = slide.Shapes.AppendShape(ShapeType.Rectangle, planoRect);
            planoShape.Fill.FillType = FillFormatType.None;
            planoShape.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph planoPara = planoShape.TextFrame.Paragraphs[0];
            planoPara.Text = "Plano de ubicación/Location Map";
            planoPara.FirstTextRange.FontHeight = 10;
            planoPara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            planoPara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            planoPara.FirstTextRange.IsBold = TriState.True;
            planoPara.Alignment = TextAlignmentType.Center;

            RectangleF ComentRect = new RectangleF(477, 40, 233, 50);
            IAutoShape ComentShape = slide.Shapes.AppendShape(ShapeType.Rectangle, ComentRect);
            ComentShape.Fill.FillType = FillFormatType.None;
            ComentShape.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph ComentPara = ComentShape.TextFrame.Paragraphs[0];
            ComentPara.Text = "Comentarios/Comments";
            ComentPara.FirstTextRange.FontHeight = 10;
            ComentPara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            ComentPara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            ComentPara.FirstTextRange.IsBold = TriState.True;
            ComentPara.Alignment = TextAlignmentType.Center;

            RectangleF imagen1Rect = new RectangleF(10, 90, 234, 150);
            IEmbedImage imagen1Shape = slide.Shapes.AppendEmbedImage(ShapeType.Rectangle, ruta + "T_" + id + ".png", imagen1Rect);
            imagen1Shape.Fill.FillType = FillFormatType.Solid;
            imagen1Shape.Fill.SolidColor.Color = Color.White;
            imagen1Shape.Line.Style = TextLineStyle.Single;
            imagen1Shape.Line.Width = 1;
            imagen1Shape.Line.SolidFillColor.Color = Color.Black;

            //imagen1Shape.ShapeStyle.LineColor.Color = Color.Black;
            utils util = new utils();
            string strUrlPoligono = "https://maps.googleapis.com/maps/api/staticmap?center=" + terrenos.nb_posicion + "&zoom=15&size=840x640&maptype=hybrid&path=fillcolor:red|" + (terrenos.nb_poligono != null ? terrenos.nb_poligono.TrimEnd('|') : "") + "&key=AIzaSyDhfzatgC_64higR2LlLGkWldmTBudiRWY";
            string rutaArchivo = util.GeneraImagen(strUrlPoligono, "poligono" + terrenos.cd_terreno, ruta);


            RectangleF imagen2Rect = new RectangleF(244, 90, 234, 150);
            IEmbedImage imagen2Shape = slide.Shapes.AppendEmbedImage(ShapeType.Rectangle, rutaArchivo, imagen2Rect);
            imagen2Shape.Fill.FillType = FillFormatType.Solid;
            imagen2Shape.Fill.SolidColor.Color = Color.White;
            imagen2Shape.Line.Style = TextLineStyle.Single;
            imagen2Shape.Line.Width = 1;
            imagen2Shape.Line.SolidFillColor.Color = Color.Black;

            RectangleF imagen3Rect = new RectangleF(477, 90, 234, 150);
            IAutoShape imagen3Shape = slide.Shapes.AppendShape(ShapeType.Rectangle, imagen3Rect);
            imagen3Shape.Fill.FillType = FillFormatType.Solid;
            imagen3Shape.Fill.SolidColor.Color = Color.White;
            //imagen3Shape.ShapeStyle.LineColor.Color = Color.Black;
            imagen3Shape.Line.Style = TextLineStyle.Single;
            imagen3Shape.Line.Width = 1;
            imagen3Shape.Line.SolidFillColor.Color = Color.Black;

            TextParagraph imagenEspPara = imagen3Shape.TextFrame.Paragraphs[0];
            imagenEspPara.Text = terrenos.nb_comentarios == null ? "" : terrenos.nb_comentarios.Replace("\r\n", " ");
            imagenEspPara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            imagenEspPara.FirstTextRange.FontHeight = 8;
            imagenEspPara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            imagenEspPara.FirstTextRange.IsBold = TriState.False;
            imagenEspPara.Alignment = TextAlignmentType.Justify;

            RectangleF notasTitRect = new RectangleF(477, 250, 234, 25);
            IAutoShape notasTitShape = slide.Shapes.AppendShape(ShapeType.Rectangle, notasTitRect);
            notasTitShape.Fill.FillType = FillFormatType.Solid;
            notasTitShape.Fill.SolidColor.Color = Color.White;
            notasTitShape.Line.Style = TextLineStyle.Single;
            notasTitShape.Line.Width = 1;
            notasTitShape.Line.SolidFillColor.Color = Color.Black;
            TextParagraph notasEspPara = notasTitShape.TextFrame.Paragraphs[0];
            notasEspPara.Text = "Notas Recorrido/Tour Notes: ";
            notasEspPara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            notasEspPara.FirstTextRange.FontHeight = 8;
            notasEspPara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            notasEspPara.FirstTextRange.IsBold = TriState.True;
            notasEspPara.Alignment = TextAlignmentType.Left;

            RectangleF notasRect = new RectangleF(477, 275, 234, 150);
            IAutoShape notasShape = slide.Shapes.AppendShape(ShapeType.Rectangle, notasRect);
            notasShape.Fill.FillType = FillFormatType.Solid;
            notasShape.Fill.SolidColor.Color = Color.White;
            notasShape.Line.Style = TextLineStyle.Single;
            notasShape.Line.Width = 1;
            notasShape.Line.SolidFillColor.Color = Color.Black;

            var precios = navesdao.obtenPreciosTerr(terrenos.cd_terreno);



            double tipo_cambio = precios.nu_tipo_cambio == null ? 0 : (double)precios.nu_tipo_cambio;
            double mantenimiento = precios.nu_ma_ac == null ? 0 : (double)precios.nu_ma_ac;
            double precio_renta = precios.im_renta == null ? 0 : (double)precios.im_renta;
            double precio_venta = precios.im_venta == null ? 0 : (double)precios.im_venta;
            double precio_venta_final = 0;
            double precio_renta_final = 0;
            string strPrecios = "";
            if (precios.cd_moneda != null || precios.cd_rep_moneda != null)
            {
                var tipo_moneda = navesdao.obtenMoneda(precios.cd_moneda);
                var tipo_reporte = navesdao.obtenMoneda(precios.cd_rep_moneda);


                switch (tipo_reporte.nb_moneda)
                {
                    case "USD":
                        if (tipo_reporte.nb_moneda == tipo_moneda.nb_moneda)
                        {
                            precio_venta_final = precio_venta == 0 ? 0 : precio_venta;
                            precio_renta_final = precio_renta == 0 ? 0 : precio_renta;
                        }
                        else
                        {
                            precio_venta_final = precio_venta == 0 ? 0 : precio_venta / tipo_cambio;
                            precio_renta_final = precio_renta == 0 ? 0 : precio_renta / tipo_cambio;
                        }
                        break;
                    case "MXN":
                        if (tipo_reporte.nb_moneda == tipo_moneda.nb_moneda)
                        {
                            precio_venta_final = precio_venta == 0 ? 0 : precio_venta;
                            precio_renta_final = precio_renta == 0 ? 0 : precio_renta;
                        }
                        else
                        {
                            precio_venta_final = precio_venta == 0 ? 0 : precio_venta * tipo_cambio;
                            precio_renta_final = precio_renta == 0 ? 0 : precio_renta * tipo_cambio;
                        }
                        break;
                }



                strPrecios = "Precio de Venta/ Sales Price\t";
                //strPrecios = strPrecios + ": " + precio_venta_final.ToString("C2", CultureInfo.CreateSpecificCulture("en-us")) + " " + tipo_moneda.nb_moneda + " / SqMt\n";
                strPrecios = strPrecios + ": " + precio_venta_final.ToString("C2", CultureInfo.CreateSpecificCulture("en-us")) + " " + tipo_moneda.nb_moneda + "\n";
                strPrecios = strPrecios + "Precio de Renta/Rent Price\t ";
                strPrecios = strPrecios + ": " + precio_renta_final.ToString("C2", CultureInfo.CreateSpecificCulture("en-us")) + " " + tipo_moneda.nb_moneda + "/SqMt/Month\n";
                strPrecios = strPrecios + "Cuota de Mantenimiento/\t ";
                strPrecios = strPrecios + ": " + mantenimiento.ToString("C2", CultureInfo.CreateSpecificCulture("en-us")) + " " + tipo_moneda.nb_moneda + "/SqMt/Month\n";
                strPrecios = strPrecios + "Maintenance Fee ";
            }

            RectangleF preciosRect = new RectangleF(477, 425, 234, 50);
            IAutoShape preciosShape = slide.Shapes.AppendShape(ShapeType.Rectangle, preciosRect);
            preciosShape.Fill.FillType = FillFormatType.Solid;
            preciosShape.Fill.SolidColor.Color = Color.White;
            preciosShape.Line.Style = TextLineStyle.Single;
            preciosShape.Line.Width = 1;
            preciosShape.Line.SolidFillColor.Color = Color.Black;
            TextParagraph preciosPara = preciosShape.TextFrame.Paragraphs[0];
            preciosPara.Text = strPrecios;
            //preciosPara.Text = " Precio de Venta/Sales Price\t : $0.00 USD\n Precio de Renta/Rent Price\t : $5.20 USD\n Cuota de Mantenimiento/Maintenance Fee\t : $0.30 USD";// + ": " + precio_venta.ToString("C2", CultureInfo.CreateSpecificCulture("en-us")) + " " + tipo_moneda.nb_moneda + "\n: ";
            preciosPara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            preciosPara.FirstTextRange.FontHeight = 6;
            preciosPara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            preciosPara.FirstTextRange.IsBold = TriState.True;
            preciosPara.Alignment = TextAlignmentType.Left;

            RectangleF TituloEspRect = new RectangleF(10, 250, 467, 10);
            IAutoShape TituloEspShape = slide.Shapes.AppendShape(ShapeType.Rectangle, TituloEspRect);
            TituloEspShape.Fill.FillType = FillFormatType.None;
            TituloEspShape.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph TituloEspPara = TituloEspShape.TextFrame.Paragraphs[0];
            TituloEspPara.Text = "Información de la propiedad/Property Information";
            TituloEspPara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            TituloEspPara.FirstTextRange.FontHeight = 8;
            TituloEspPara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            TituloEspPara.FirstTextRange.IsBold = TriState.True;
            TituloEspPara.Alignment = TextAlignmentType.Left;

            ///INFORMACION LADO IZQUIERDO
            RectangleF EspRect = new RectangleF(10, 260, 117, 20);
            IAutoShape EspShape = slide.Shapes.AppendShape(ShapeType.Rectangle, EspRect);
            EspShape.Fill.FillType = FillFormatType.None;
            EspShape.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph EspPara = EspShape.TextFrame.Paragraphs[0];
            EspPara.Text = "Corredor/Market";
            EspPara.FirstTextRange.FontHeight = 8;
            EspPara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            EspPara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            EspPara.FirstTextRange.IsBold = TriState.True;
            EspPara.Alignment = TextAlignmentType.Left;

            if (terrenos.cd_mercado != null && terrenos.cd_corredor != null)
            {
                var corredor = navesdao.obtenCorredor(terrenos.cd_mercado, terrenos.cd_corredor);

                RectangleF parafrec0 = new RectangleF(127, 260, 117, 20);
                IAutoShape parafshape0 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec0);
                parafshape0.Fill.FillType = FillFormatType.None;
                parafshape0.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph parafTex0 = parafshape0.TextFrame.Paragraphs[0];
                parafTex0.Text = ": " + corredor.nb_corredor;
                parafTex0.FirstTextRange.FontHeight = 8;
                parafTex0.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                parafTex0.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                parafTex0.FirstTextRange.IsBold = TriState.True;
                parafTex0.Alignment = TextAlignmentType.Left;
            }

            var gralTerrenos = navesdao.obtenDatosGralTerrenos(terrenos.cd_terreno);

            RectangleF infRec1 = new RectangleF(10, 280, 117, 20);
            IAutoShape infShape1 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec1);
            infShape1.Fill.FillType = FillFormatType.None;
            infShape1.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText1 = infShape1.TextFrame.Paragraphs[0];
            infText1.Text = "Tamaño mínimo de lote / Min. Lot Size";
            infText1.FirstTextRange.FontHeight = 8;
            infText1.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText1.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText1.FirstTextRange.IsBold = TriState.True;
            infText1.Alignment = TextAlignmentType.Left;

            RectangleF parafrec1 = new RectangleF(127, 280, 117, 20);
            IAutoShape parafshape1 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec1);
            parafshape1.Fill.FillType = FillFormatType.None;
            parafshape1.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph parafTex1 = parafshape1.TextFrame.Paragraphs[0];
            parafTex1.Text = ": " + String.Format("{0:#,##0.00}", gralTerrenos.nu_tam_min) + " M2 / " + String.Format("{0:#,##0.00}", Convert.ToDouble(gralTerrenos.nu_tam_min) * 10.7639) + " Ft2\n";
            parafTex1.FirstTextRange.FontHeight = 8;
            parafTex1.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            parafTex1.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            parafTex1.FirstTextRange.IsBold = TriState.True;
            parafTex1.Alignment = TextAlignmentType.Left;

            RectangleF infRec2 = new RectangleF(10, 300, 117, 20);
            IAutoShape infShape2 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec2);
            infShape2.Fill.FillType = FillFormatType.None;
            infShape2.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText2 = infShape2.TextFrame.Paragraphs[0];
            infText2.Text = "Tamaño máximo de lote / Max. Lot Size";
            infText2.FirstTextRange.FontHeight = 8;
            infText2.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText2.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText2.FirstTextRange.IsBold = TriState.True;
            infText2.Alignment = TextAlignmentType.Left;

            //var nivelpiso = navesdao.obtenNivelPiso(gralnaves.cd_Nivel_Piso);

            RectangleF parafrec2 = new RectangleF(127, 300, 117, 20);
            IAutoShape parafshape2 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec2);
            parafshape2.Fill.FillType = FillFormatType.None;
            parafshape2.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph parafTex2 = parafshape2.TextFrame.Paragraphs[0];
            parafTex2.Text = ": " + String.Format("{0:#,##0.00}", gralTerrenos.nu_tam_max) + " M2 / " + String.Format("{0:#,##0.00}", Convert.ToDouble(gralTerrenos.nu_tam_max) * 10.7639) + " Ft2\n";
            parafTex2.FirstTextRange.FontHeight = 8;
            parafTex2.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            parafTex2.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            parafTex2.FirstTextRange.IsBold = TriState.True;
            parafTex2.Alignment = TextAlignmentType.Left;

            RectangleF infRec3 = new RectangleF(10, 320, 117, 20);
            IAutoShape infShape3 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec3);
            infShape3.Fill.FillType = FillFormatType.None;
            infShape3.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText3 = infShape3.TextFrame.Paragraphs[0];
            infText3.Text = "Disponibilidad Total / Total Availability";
            infText3.FirstTextRange.FontHeight = 8;
            infText3.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText3.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText3.FirstTextRange.IsBold = TriState.True;
            infText3.Alignment = TextAlignmentType.Left;

            RectangleF parafrec3 = new RectangleF(127, 320, 117, 20);
            IAutoShape parafshape3 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec3);
            parafshape3.Fill.FillType = FillFormatType.None;
            parafshape3.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph parafTex3 = parafshape3.TextFrame.Paragraphs[0];
            parafTex3.Text = ": " + String.Format("{0:#,##0.00}", gralTerrenos.nu_disponiblidad) + " M2 / " + String.Format("{0:#,##0.00}", Convert.ToDouble(gralTerrenos.nu_disponiblidad) * 10.7639) + " Ft2\n";
            parafTex3.FirstTextRange.FontHeight = 8;
            parafTex3.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            parafTex3.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            parafTex3.FirstTextRange.IsBold = TriState.True;
            parafTex3.Alignment = TextAlignmentType.Left;

            RectangleF infRec4 = new RectangleF(10, 340, 117, 20);
            IAutoShape infShape4 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec4);
            infShape4.Fill.FillType = FillFormatType.None;
            infShape4.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText4 = infShape4.TextFrame.Paragraphs[0];
            infText4.Text = "Cobertura máxima permitida / Max. Allowed Coverage";
            infText4.FirstTextRange.FontHeight = 8;
            infText4.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText4.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText4.FirstTextRange.IsBold = TriState.True;
            infText4.Alignment = TextAlignmentType.Left;

            //var carga = navesdao.obtenCarga(gralnaves.cd_carga);

            RectangleF parafrec4 = new RectangleF(127, 340, 117, 20);
            IAutoShape parafshape4 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec4);
            parafshape4.Fill.FillType = FillFormatType.None;
            parafshape4.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph parafTex4 = parafshape4.TextFrame.Paragraphs[0];
            parafTex4.Text = ": " + gralTerrenos.nb_radio_cob;
            parafTex4.FirstTextRange.FontHeight = 8;
            parafTex4.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            parafTex4.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            parafTex4.FirstTextRange.IsBold = TriState.True;
            parafTex4.Alignment = TextAlignmentType.Left;

            /*RectangleF infRec5 = new RectangleF(10, 360, 117, 20);
            IAutoShape infShape5 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec5);
            infShape5.Fill.FillType = FillFormatType.None;
            infShape5.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText5 = infShape5.TextFrame.Paragraphs[0];
            infText5.Text = "Cajones de Estacionamiento/Parking stalls";
            infText5.FirstTextRange.FontHeight = 8;
            infText5.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText5.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText5.FirstTextRange.IsBold = TriState.True;
            infText5.Alignment = TextAlignmentType.Left;

            var cajon = navesdao.obtenCajonesEst(gralnaves.cd_cajon_est);

            RectangleF parafrec5 = new RectangleF(127, 360, 117, 20);
            IAutoShape parafshape5 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec5);
            parafshape5.Fill.FillType = FillFormatType.None;
            parafshape5.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph parafTex5 = parafshape5.TextFrame.Paragraphs[0];
            parafTex5.Text = ": " + cajon.nb_cajon_est;
            parafTex5.FirstTextRange.FontHeight = 8;
            parafTex5.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            parafTex5.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            parafTex5.FirstTextRange.IsBold = TriState.True;
            parafTex5.Alignment = TextAlignmentType.Left;

            RectangleF infRec6 = new RectangleF(10, 380, 117, 20);
            IAutoShape infShape6 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec6);
            infShape6.Fill.FillType = FillFormatType.None;
            infShape6.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText6 = infShape6.TextFrame.Paragraphs[0];
            infText6.Text = "Techumbre/Roof type";
            infText6.FirstTextRange.FontHeight = 8;
            infText6.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText6.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText6.FirstTextRange.IsBold = TriState.True;
            infText6.Alignment = TextAlignmentType.Left;

            var tp_tech = navesdao.obtenTipoTechumbre(gralnaves.cd_tp_tech);

            RectangleF parafrec6 = new RectangleF(127, 380, 117, 20);
            IAutoShape parafshape6 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec6);
            parafshape6.Fill.FillType = FillFormatType.None;
            parafshape6.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph parafTex6 = parafshape6.TextFrame.Paragraphs[0];
            parafTex6.Text = ": " + tp_tech.nb_tp_tech;
            parafTex6.FirstTextRange.FontHeight = 8;
            parafTex6.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            parafTex6.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            parafTex6.FirstTextRange.IsBold = TriState.True;
            parafTex6.Alignment = TextAlignmentType.Left;

            RectangleF infRec7 = new RectangleF(10, 400, 117, 20);
            IAutoShape infShape7 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec7);
            infShape7.Fill.FillType = FillFormatType.None;
            infShape7.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText7 = infShape7.TextFrame.Paragraphs[0];
            infText7.Text = "Altura Mínima-Máxima/Min-Max Height";
            infText7.FirstTextRange.FontHeight = 8;
            infText7.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText7.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText7.FirstTextRange.IsBold = TriState.True;
            infText7.Alignment = TextAlignmentType.Left;

            RectangleF parafrec7 = new RectangleF(127, 400, 117, 20);
            IAutoShape parafshape7 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec7);
            parafshape7.Fill.FillType = FillFormatType.None;
            parafshape7.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph parafTex7 = parafshape7.TextFrame.Paragraphs[0];
            parafTex7.Text = ": " + gralnaves.nu_altura;
            parafTex7.FirstTextRange.FontHeight = 8;
            parafTex7.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            parafTex7.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            parafTex7.FirstTextRange.IsBold = TriState.True;
            parafTex7.Alignment = TextAlignmentType.Left;

            var serviciosnaves = navesdao.obtenServiciosNaves(naves.cd_nave);

            RectangleF infRec8 = new RectangleF(10, 420, 117, 20);
            IAutoShape infShape8 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec8);
            infShape8.Fill.FillType = FillFormatType.None;
            infShape8.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText8 = infShape8.TextFrame.Paragraphs[0];
            infText8.Text = "Telefonía/Telephone Service";
            infText8.FirstTextRange.FontHeight = 8;
            infText8.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText8.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText8.FirstTextRange.IsBold = TriState.True;
            infText8.Alignment = TextAlignmentType.Left;

            var telefonia = navesdao.obtenTelefonia(serviciosnaves.cd_telefonia);

            RectangleF parafrec8 = new RectangleF(127, 420, 117, 20);
            IAutoShape parafshape8 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec8);
            parafshape8.Fill.FillType = FillFormatType.None;
            parafshape8.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph parafTex8 = parafshape8.TextFrame.Paragraphs[0];
            parafTex8.Text = ": " + telefonia.nb_telefonia;
            parafTex8.FirstTextRange.FontHeight = 8;
            parafTex8.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            parafTex8.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            parafTex8.FirstTextRange.IsBold = TriState.True;
            parafTex8.Alignment = TextAlignmentType.Left;

            RectangleF infRec9 = new RectangleF(10, 440, 117, 20);
            IAutoShape infShape9 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec9);
            infShape9.Fill.FillType = FillFormatType.None;
            infShape9.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText9 = infShape9.TextFrame.Paragraphs[0];
            infText9.Text = "Estacionamiento Trailers/Drop Lots";
            infText9.FirstTextRange.FontHeight = 8;
            infText9.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText9.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText9.FirstTextRange.IsBold = TriState.True;
            infText9.Alignment = TextAlignmentType.Left;

            RectangleF parafrec9 = new RectangleF(127, 440, 117, 20);
            IAutoShape parafshape9 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec9);
            parafshape9.Fill.FillType = FillFormatType.None;
            parafshape9.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph parafTex9 = parafshape9.TextFrame.Paragraphs[0];
            parafTex9.Text = ": " + gralnaves.nu_caj_trailer.ToString();
            parafTex9.FirstTextRange.FontHeight = 8;
            parafTex9.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            parafTex9.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            parafTex9.FirstTextRange.IsBold = TriState.True;
            parafTex9.Alignment = TextAlignmentType.Left;*/

            var servicios = navesdao.obtenServiciosTerrenos(terrenos.cd_terreno);
            RectangleF infRec10 = new RectangleF(244, 260, 117, 20);
            IAutoShape infShape10 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec10);
            infShape10.Fill.FillType = FillFormatType.None;
            infShape10.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText10 = infShape10.TextFrame.Paragraphs[0];
            infText10.Text = "Agua Potable / Drinking Water";
            infText10.FirstTextRange.FontHeight = 8;
            infText10.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText10.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText10.FirstTextRange.IsBold = TriState.True;
            infText10.Alignment = TextAlignmentType.Left;

            // var ventilacion = navesdao.obtenVentilacion(gralnaves.cd_hvac);

            RectangleF parafrec10 = new RectangleF(361, 260, 117, 20);
            IAutoShape parafshape10 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec10);
            parafshape10.Fill.FillType = FillFormatType.None;
            parafshape10.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph parafTex10 = parafshape10.TextFrame.Paragraphs[0];
            parafTex10.Text = ": " + (!servicios.st_aguapozo ? "Servicio Municipal – Municipal Service" : "Pozo – Water well");
            parafTex10.FirstTextRange.FontHeight = 8;
            parafTex10.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            parafTex10.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            parafTex10.FirstTextRange.IsBold = TriState.True;
            parafTex10.Alignment = TextAlignmentType.Left;



            RectangleF infRec11 = new RectangleF(244, 280, 117, 20);
            IAutoShape infShape11 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec11);
            infShape11.Fill.FillType = FillFormatType.None;
            infShape11.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText11 = infShape11.TextFrame.Paragraphs[0];
            infText11.Text = "Telefonía / Telephone Service";
            infText11.FirstTextRange.FontHeight = 8;
            infText11.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText11.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText11.FirstTextRange.IsBold = TriState.True;
            infText11.Alignment = TextAlignmentType.Left;

            if (servicios.cd_telefonia != null)
            {
                var telefonia = navesdao.obtenTelefonia(servicios.cd_telefonia);

                RectangleF parafrec11 = new RectangleF(361, 280, 117, 20);
                IAutoShape parafshape11 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec11);
                parafshape11.Fill.FillType = FillFormatType.None;
                parafshape11.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph parafTex11 = parafshape11.TextFrame.Paragraphs[0];
                parafTex11.Text = ": " + telefonia.nb_telefonia;
                parafTex11.FirstTextRange.FontHeight = 8;
                parafTex11.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                parafTex11.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                parafTex11.FirstTextRange.IsBold = TriState.True;
                parafTex11.Alignment = TextAlignmentType.Left;
            }
            else
            {
                //var telefonia = navesdao.obtenTelefonia(servicios.cd_telefonia);

                RectangleF parafrec11 = new RectangleF(361, 280, 117, 20);
                IAutoShape parafshape11 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec11);
                parafshape11.Fill.FillType = FillFormatType.None;
                parafshape11.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph parafTex11 = parafshape11.TextFrame.Paragraphs[0];
                parafTex11.Text = ": ";
                parafTex11.FirstTextRange.FontHeight = 8;
                parafTex11.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                parafTex11.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                parafTex11.FirstTextRange.IsBold = TriState.True;
                parafTex11.Alignment = TextAlignmentType.Left;
            }

            RectangleF infRec12 = new RectangleF(244, 300, 117, 20);
            IAutoShape infShape12 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec12);
            infShape12.Fill.FillType = FillFormatType.None;
            infShape12.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText12 = infShape12.TextFrame.Paragraphs[0];
            infText12.Text = "Gas Natural / Natural Gas";
            infText12.FirstTextRange.FontHeight = 8;
            infText12.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText12.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText12.FirstTextRange.IsBold = TriState.True;
            infText12.Alignment = TextAlignmentType.Left;

            if (servicios.cd_tp_gas_natural != null)
            {
                var gas = navesdao.obtenTipoGas(servicios.cd_tp_gas_natural);

                RectangleF parafrec12 = new RectangleF(361, 300, 117, 20);
                IAutoShape parafshape12 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec12);
                parafshape12.Fill.FillType = FillFormatType.None;
                parafshape12.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph parafTex12 = parafshape12.TextFrame.Paragraphs[0];
                parafTex12.Text = gas.nb_tp_gas_natural;
                parafTex12.FirstTextRange.FontHeight = 8;
                parafTex12.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                parafTex12.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                parafTex12.FirstTextRange.IsBold = TriState.True;
                parafTex12.Alignment = TextAlignmentType.Left;
            }
            else
            {
                RectangleF parafrec12 = new RectangleF(361, 300, 117, 20);
                IAutoShape parafshape12 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec12);
                parafshape12.Fill.FillType = FillFormatType.None;
                parafshape12.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph parafTex12 = parafshape12.TextFrame.Paragraphs[0];
                parafTex12.Text = "";
                parafTex12.FirstTextRange.FontHeight = 8;
                parafTex12.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                parafTex12.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                parafTex12.FirstTextRange.IsBold = TriState.True;
                parafTex12.Alignment = TextAlignmentType.Left;
            }

            RectangleF infRec13 = new RectangleF(244, 320, 117, 20);
            IAutoShape infShape13 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec13);
            infShape13.Fill.FillType = FillFormatType.None;
            infShape13.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText13 = infShape13.TextFrame.Paragraphs[0];
            infText13.Text = "Espuela de Ferrocarril / Railroad Track";
            infText13.FirstTextRange.FontHeight = 8;
            infText13.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText13.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText13.FirstTextRange.IsBold = TriState.True;
            infText13.Alignment = TextAlignmentType.Left;


            if (servicios.cd_esp_fer != null)
            {
                var esp_ferr = navesdao.obtenEspFerr(servicios.cd_esp_fer);

                RectangleF parafrec13 = new RectangleF(361, 320, 117, 20);
                IAutoShape parafshape13 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec13);
                parafshape13.Fill.FillType = FillFormatType.None;
                parafshape13.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph parafTex13 = parafshape13.TextFrame.Paragraphs[0];
                parafTex13.Text = ": " + esp_ferr.nb_esp_ferr;
                parafTex13.FirstTextRange.FontHeight = 8;
                parafTex13.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                parafTex13.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                parafTex13.FirstTextRange.IsBold = TriState.True;
                parafTex13.Alignment = TextAlignmentType.Left;
            }
            else
            {
                RectangleF parafrec13 = new RectangleF(361, 320, 117, 20);
                IAutoShape parafshape13 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec13);
                parafshape13.Fill.FillType = FillFormatType.None;
                parafshape13.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph parafTex13 = parafshape13.TextFrame.Paragraphs[0];
                parafTex13.Text = ": ";
                parafTex13.FirstTextRange.FontHeight = 8;
                parafTex13.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                parafTex13.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                parafTex13.FirstTextRange.IsBold = TriState.True;
                parafTex13.Alignment = TextAlignmentType.Left;
            }

            RectangleF infRec14 = new RectangleF(244, 340, 117, 20);
            IAutoShape infShape14 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec14);
            infShape14.Fill.FillType = FillFormatType.None;
            infShape14.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText14 = infShape14.TextFrame.Paragraphs[0];
            infText14.Text = "Estatus de Entrega / Delivery status";
            infText14.FirstTextRange.FontHeight = 8;
            infText14.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText14.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText14.FirstTextRange.IsBold = TriState.True;
            infText14.Alignment = TextAlignmentType.Left;

            if (gralTerrenos.cd_st_entrega != null)
            {
                var estatus_entrega = navesdao.obtenEstatusEntrega(gralTerrenos.cd_st_entrega);

                RectangleF parafrec14 = new RectangleF(361, 340, 117, 20);
                IAutoShape parafshape14 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec14);
                parafshape14.Fill.FillType = FillFormatType.None;
                parafshape14.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph parafTex14 = parafshape14.TextFrame.Paragraphs[0];
                parafTex14.Text = ": " + estatus_entrega.nb_st_entrega;
                parafTex14.FirstTextRange.FontHeight = 8;
                parafTex14.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                parafTex14.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                parafTex14.FirstTextRange.IsBold = TriState.True;
                parafTex14.Alignment = TextAlignmentType.Left;

            }
            else
            {
                RectangleF parafrec14 = new RectangleF(361, 340, 117, 20);
                IAutoShape parafshape14 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec14);
                parafshape14.Fill.FillType = FillFormatType.None;
                parafshape14.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph parafTex14 = parafshape14.TextFrame.Paragraphs[0];
                parafTex14.Text = ": ";
                parafTex14.FirstTextRange.FontHeight = 8;
                parafTex14.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                parafTex14.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                parafTex14.FirstTextRange.IsBold = TriState.True;
                parafTex14.Alignment = TextAlignmentType.Left;
            }
            /*RectangleF infRec15 = new RectangleF(244, 360, 117, 20);
            IAutoShape infShape15 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec15);
            infShape15.Fill.FillType = FillFormatType.None;
            infShape15.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText15 = infShape15.TextFrame.Paragraphs[0];
            infText15.Text = "Tipo de Lámparas/Lightning";
            infText15.FirstTextRange.FontHeight = 8;
            infText15.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText15.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText15.FirstTextRange.IsBold = TriState.True;
            infText15.Alignment = TextAlignmentType.Left;

            var tipo_lampara = navesdao.obtenTipoLampara(gralnaves.cd_tp_lampara);

            RectangleF parafrec15 = new RectangleF(361, 360, 117, 20);
            IAutoShape parafshape15 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec15);
            parafshape15.Fill.FillType = FillFormatType.None;
            parafshape15.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph parafTex15 = parafshape15.TextFrame.Paragraphs[0];
            parafTex15.Text = ": " + tipo_lampara.nb_tp_lampara;
            parafTex15.FirstTextRange.FontHeight = 8;
            parafTex15.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            parafTex15.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            parafTex15.FirstTextRange.IsBold = TriState.True;
            parafTex15.Alignment = TextAlignmentType.Left;

            RectangleF infRec16 = new RectangleF(244, 380, 117, 20);
            IAutoShape infShape16 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec16);
            infShape16.Fill.FillType = FillFormatType.None;
            infShape16.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText16 = infShape16.TextFrame.Paragraphs[0];
            infText16.Text = "Energía eléctrica/Power Supply";
            infText16.FirstTextRange.FontHeight = 8;
            infText16.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText16.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText16.FirstTextRange.IsBold = TriState.True;
            infText16.Alignment = TextAlignmentType.Left;

            RectangleF parafrec16 = new RectangleF(361, 380, 117, 20);
            IAutoShape parafshape16 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec16);
            parafshape16.Fill.FillType = FillFormatType.None;
            parafshape16.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph parafTex16 = parafshape16.TextFrame.Paragraphs[0];
            parafTex16.Text = ": " + serviciosnaves.nu_kva + " KvA";
            parafTex16.FirstTextRange.FontHeight = 8;
            parafTex16.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            parafTex16.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            parafTex16.FirstTextRange.IsBold = TriState.True;
            parafTex16.Alignment = TextAlignmentType.Left;

            RectangleF infRec17 = new RectangleF(244, 400, 117, 20);
            IAutoShape infShape17 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec17);
            infShape17.Fill.FillType = FillFormatType.None;
            infShape17.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText17 = infShape17.TextFrame.Paragraphs[0];
            infText17.Text = "Agua Potable/Drinking Water";
            infText17.FirstTextRange.FontHeight = 8;
            infText17.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText17.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText17.FirstTextRange.IsBold = TriState.True;
            infText17.Alignment = TextAlignmentType.Left;

            RectangleF parafrec17 = new RectangleF(361, 400, 117, 20);
            IAutoShape parafshape17 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec17);
            parafshape17.Fill.FillType = FillFormatType.None;
            parafshape17.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph parafTex17 = parafshape17.TextFrame.Paragraphs[0];
            parafTex17.Text = ": " + (!serviciosnaves.st_aguapozo ? "Servicio Municipal – Municipal Service" : "Pozo – Water well");
            parafTex17.FirstTextRange.FontHeight = 8;
            parafTex17.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            parafTex17.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            parafTex17.FirstTextRange.IsBold = TriState.True;
            parafTex17.Alignment = TextAlignmentType.Left;

            RectangleF infRec18 = new RectangleF(244, 420, 117, 20);
            IAutoShape infShape18 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec18);
            infShape18.Fill.FillType = FillFormatType.None;
            infShape18.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText18 = infShape18.TextFrame.Paragraphs[0];
            infText18.Text = "Sistema contra incendio/Fire System";
            infText18.FirstTextRange.FontHeight = 8;
            infText18.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText18.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText18.FirstTextRange.IsBold = TriState.True;
            infText18.Alignment = TextAlignmentType.Left;

            var sistemaincendio = navesdao.obtenSistIncendios(gralnaves.cd_sist_inc);

            RectangleF parafrec18 = new RectangleF(361, 420, 117, 20);
            IAutoShape parafshape18 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec18);
            parafshape18.Fill.FillType = FillFormatType.None;
            parafshape18.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph parafTex18 = parafshape18.TextFrame.Paragraphs[0];
            parafTex18.Text = ": " + sistemaincendio.nb_sist_inc;
            parafTex18.FirstTextRange.FontHeight = 8;
            parafTex18.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            parafTex18.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            parafTex18.FirstTextRange.IsBold = TriState.True;
            parafTex18.Alignment = TextAlignmentType.Left;

            RectangleF infRec19 = new RectangleF(244, 440, 117, 20);
            IAutoShape infShape19 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec19);
            infShape19.Fill.FillType = FillFormatType.None;
            infShape19.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText19 = infShape19.TextFrame.Paragraphs[0];
            infText19.Text = "Tipo de Construcción/Walls Type";
            infText19.FirstTextRange.FontHeight = 8;
            infText19.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText19.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText19.FirstTextRange.IsBold = TriState.True;
            infText19.Alignment = TextAlignmentType.Left;

            var tipo_construccion = navesdao.obtenTipoConstruccion(gralnaves.cd_tp_construccion);

            RectangleF parafrec19 = new RectangleF(361, 440, 117, 20);
            IAutoShape parafshape19 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec19);
            parafshape19.Fill.FillType = FillFormatType.None;
            parafshape19.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph parafTex19 = parafshape19.TextFrame.Paragraphs[0];
            parafTex19.Text = ": " + tipo_construccion.nb_tp_construccion;
            parafTex19.FirstTextRange.FontHeight = 8;
            parafTex19.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            parafTex19.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            parafTex19.FirstTextRange.IsBold = TriState.True;
            parafTex19.Alignment = TextAlignmentType.Left;*/

            IAutoShape shape = slide.Shapes.AppendShape(ShapeType.Line, new RectangleF(10, 480, 700, 0));

            RectangleF footerRec = new RectangleF(10, 480, 700, 40);
            IAutoShape footershape1 = slide.Shapes.AppendShape(ShapeType.Rectangle, footerRec);
            footershape1.Fill.FillType = FillFormatType.None;
            footershape1.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph footerText = footershape1.TextFrame.Paragraphs[0];
            footerText.Text = "This report contains information available to the public and has been relied upon by Cushman & Wakefield on the basis that it is accurate and complete. Cushman & Wakefield accepts no responsibility if this should prove not to be the case. No warranty or representation, express or implied, is made to the accuracy or completeness of the information contained herein, and same is submitted subject to errors, omissions, change of price, rental or other conditions, withdrawal without notice, and to any special listing conditions imposed by our principals.";
            footerText.FirstTextRange.FontHeight = 6;
            footerText.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            footerText.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            footerText.FirstTextRange.IsBold = TriState.True;
            footerText.Alignment = TextAlignmentType.Left;

            RectangleF LogoRect = new RectangleF(600, 510, 100, 30);
            string rutaArchvio = ruta.Replace("repositorio", "image") + "logo_CW.png";
            IEmbedImage LogoShape = slide.Shapes.AppendEmbedImage(ShapeType.Rectangle, rutaArchvio, LogoRect);
            LogoShape.Fill.FillType = FillFormatType.None;
            //LogoShape.ShapeStyle.LineColor.Color = Color.Empty;

            // diapositiva++;
            SizeF pptSize = ppt.SlideSize.Size;
            ppt.SaveToFile(ruta + "terrenos_rpt.pptx", FileFormat.Pptx2010);

        }
        private static void reporteIndividual(String ruta, int? id)
        {

            clsNavesDao navesdao = new clsNavesDao();

            var naves = navesdao.obtenDatosNaves(id);

            int diapositiva = 0;
            PPTReport.ModifyInMemory.ActivateMemoryPatching();
            Presentation ppt = new Presentation();
            ISlide slide = ppt.Slides[diapositiva];
            RectangleF titleRect = new RectangleF(10, 10, 700, 25);
            IAutoShape titleShape = slide.Shapes.AppendShape(ShapeType.Rectangle, titleRect);
            titleShape.Fill.FillType = FillFormatType.Solid;
            titleShape.Fill.SolidColor.Color = Color.MidnightBlue;
            titleShape.ShapeStyle.LineColor.Color = Color.MidnightBlue;
            TextParagraph titlePara = titleShape.TextFrame.Paragraphs[0];
            titlePara.Text = naves.nb_parque + "/" + naves.nb_nave;
            titlePara.FirstTextRange.FontHeight = 20;
            titlePara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            titlePara.FirstTextRange.Fill.SolidColor.Color = Color.White;
            titlePara.Alignment = TextAlignmentType.Left;

            var municipio = navesdao.obteneMunicipio(naves.cd_estado, naves.cd_municipio);
            var colonia = navesdao.obtenColonia(naves.cd_estado, naves.cd_municipio, naves.cd_colonia);

            RectangleF dirRect = new RectangleF(10, 40, 234, 50);
            IAutoShape dirShape = slide.Shapes.AppendShape(ShapeType.Rectangle, dirRect);
            dirShape.Fill.FillType = FillFormatType.None;
            dirShape.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph dirPara = dirShape.TextFrame.Paragraphs[0];
            dirPara.Text = naves.nb_calle + " No." + naves.nu_direcion + "\n" + colonia.nb_colonia + "\n" + municipio.nb_municipio;
            dirPara.FirstTextRange.FontHeight = 10;
            dirPara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            dirPara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            dirPara.FirstTextRange.IsBold = TriState.True;
            dirPara.Alignment = TextAlignmentType.Left;

            RectangleF planoRect = new RectangleF(244, 40, 233, 50);
            IAutoShape planoShape = slide.Shapes.AppendShape(ShapeType.Rectangle, planoRect);
            planoShape.Fill.FillType = FillFormatType.None;
            planoShape.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph planoPara = planoShape.TextFrame.Paragraphs[0];
            planoPara.Text = "Plano de ubicación/Location Map";
            planoPara.FirstTextRange.FontHeight = 10;
            planoPara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            planoPara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            planoPara.FirstTextRange.IsBold = TriState.True;
            planoPara.Alignment = TextAlignmentType.Center;

            RectangleF ComentRect = new RectangleF(477, 40, 233, 50);
            IAutoShape ComentShape = slide.Shapes.AppendShape(ShapeType.Rectangle, ComentRect);
            ComentShape.Fill.FillType = FillFormatType.None;
            ComentShape.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph ComentPara = ComentShape.TextFrame.Paragraphs[0];
            ComentPara.Text = "Comentarios/Comments";
            ComentPara.FirstTextRange.FontHeight = 10;
            ComentPara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            ComentPara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            ComentPara.FirstTextRange.IsBold = TriState.True;
            ComentPara.Alignment = TextAlignmentType.Center;
            if (System.IO.File.Exists(ruta + "N_" + id + ".png"))
            {
                RectangleF imagen1Rect = new RectangleF(10, 90, 234, 150);
                IEmbedImage imagen1Shape = slide.Shapes.AppendEmbedImage(ShapeType.Rectangle, ruta + "N_" + id + ".png", imagen1Rect);
                imagen1Shape.Fill.FillType = FillFormatType.Solid;
                imagen1Shape.Fill.SolidColor.Color = Color.White;
                imagen1Shape.Line.Style = TextLineStyle.Single;
                imagen1Shape.Line.Width = 1;
                imagen1Shape.Line.SolidFillColor.Color = Color.Black;
            }
            try { 
                //imagen1Shape.ShapeStyle.LineColor.Color = Color.Black;
                utils util = new utils();
                string strUrlPoligono = "https://maps.googleapis.com/maps/api/staticmap?center=" + naves.nb_posicion + "&zoom=15&size=840x640&maptype=hybrid&path=fillcolor:red|" + naves.nb_poligono.TrimEnd('|') + "&key=AIzaSyDhfzatgC_64higR2LlLGkWldmTBudiRWY";
                string rutaArchivo = util.GeneraImagen(strUrlPoligono, "poligono" + naves.cd_nave, ruta);

                if (rutaArchivo != "")
                {
                    RectangleF imagen2Rect = new RectangleF(244, 90, 234, 150);
                    IEmbedImage imagen2Shape = slide.Shapes.AppendEmbedImage(ShapeType.Rectangle, rutaArchivo, imagen2Rect);
                    imagen2Shape.Fill.FillType = FillFormatType.Solid;
                    imagen2Shape.Fill.SolidColor.Color = Color.White;
                    imagen2Shape.Line.Style = TextLineStyle.Single;
                    imagen2Shape.Line.Width = 1;
                    imagen2Shape.Line.SolidFillColor.Color = Color.Black;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
            }

            RectangleF imagen3Rect = new RectangleF(477, 90, 234, 150);
            IAutoShape imagen3Shape = slide.Shapes.AppendShape(ShapeType.Rectangle, imagen3Rect);
            imagen3Shape.Fill.FillType = FillFormatType.Solid;
            imagen3Shape.Fill.SolidColor.Color = Color.White;
            //imagen3Shape.ShapeStyle.LineColor.Color = Color.Black;
            imagen3Shape.Line.Style = TextLineStyle.Single;
            imagen3Shape.Line.Width = 1;
            imagen3Shape.Line.SolidFillColor.Color = Color.Black;
            
            TextParagraph imagenEspPara = imagen3Shape.TextFrame.Paragraphs[0];
            imagenEspPara.Text = naves.nb_comentarios == null ? "":naves.nb_comentarios ;
            imagenEspPara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            imagenEspPara.FirstTextRange.FontHeight = 8;
            imagenEspPara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            imagenEspPara.FirstTextRange.IsBold = TriState.False;
            imagenEspPara.Alignment = TextAlignmentType.Justify;
            

            RectangleF notasTitRect = new RectangleF(477, 250, 234, 25);
            IAutoShape notasTitShape = slide.Shapes.AppendShape(ShapeType.Rectangle, notasTitRect);
            notasTitShape.Fill.FillType = FillFormatType.Solid;
            notasTitShape.Fill.SolidColor.Color = Color.White;
            notasTitShape.Line.Style = TextLineStyle.Single;
            notasTitShape.Line.Width = 1;
            notasTitShape.Line.SolidFillColor.Color = Color.Black;
            TextParagraph notasEspPara = notasTitShape.TextFrame.Paragraphs[0];
            notasEspPara.Text ="Notas Recorrido/Tour Notes: ";
            notasEspPara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            notasEspPara.FirstTextRange.FontHeight = 8;
            notasEspPara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            notasEspPara.FirstTextRange.IsBold = TriState.True;
            notasEspPara.Alignment = TextAlignmentType.Left;

            RectangleF notasRect = new RectangleF(477, 275, 234, 150);
            IAutoShape notasShape = slide.Shapes.AppendShape(ShapeType.Rectangle, notasRect);
            notasShape.Fill.FillType = FillFormatType.Solid;
            notasShape.Fill.SolidColor.Color = Color.White;
            notasShape.Line.Style = TextLineStyle.Single;
            notasShape.Line.Width = 1;
            notasShape.Line.SolidFillColor.Color = Color.Black;

            var precios = navesdao.obtenPrecios(naves.cd_nave);
            var tipo_moneda = navesdao.obtenMoneda(precios.cd_moneda);
            var tipo_reporte = navesdao.obtenMoneda(precios.cd_rep_moneda);
            double tipo_cambio = precios.nu_tipo_cambio==null?0:(double)precios.nu_tipo_cambio;
            double mantenimiento = precios.nu_ma_ac == null ? 0 : (double)precios.nu_ma_ac;
            double precio_renta = precios.im_renta == null ? 0 : (double)precios.im_renta;
            double precio_venta = precios.im_venta == null ? 0 : (double)precios.im_venta;
            double precio_venta_final = 0;
            double precio_renta_final = 0;

            switch (tipo_reporte.nb_moneda)
            {
                case "USD":
                    if (tipo_reporte.nb_moneda == tipo_moneda.nb_moneda)
                    {
                        precio_venta_final = precio_venta == 0 ? 0 : precio_venta;
                        precio_renta_final = precio_renta == 0 ? 0 : precio_renta;
                    }
                    else
                    {
                        precio_venta_final = precio_venta == 0 ? 0 : precio_venta / tipo_cambio;
                        precio_renta_final = precio_renta == 0 ? 0 : precio_renta / tipo_cambio;
                    }
                    break;
                case "MXN":
                    if (tipo_reporte.nb_moneda == tipo_moneda.nb_moneda)
                    {
                        precio_venta_final = precio_venta == 0 ? 0 : precio_venta;
                        precio_renta_final = precio_renta == 0 ? 0 : precio_renta;
                    }
                    else
                    {
                        precio_venta_final = precio_venta == 0 ? 0 : precio_venta * tipo_cambio;
                        precio_renta_final = precio_renta == 0 ? 0 : precio_renta * tipo_cambio;
                    }
                    break;
            }



            string strPrecios = "Precio de Venta/ Sales Price\t";
            //strPrecios = strPrecios + ": " + precio_venta_final.ToString("C2", CultureInfo.CreateSpecificCulture("en-us")) + " " + tipo_moneda.nb_moneda + " / SqMt\n";
            strPrecios = strPrecios + ": " + precio_venta_final.ToString("C2", CultureInfo.CreateSpecificCulture("en-us")) + " " + tipo_moneda.nb_moneda + "\n";
            strPrecios = strPrecios + "Precio de Renta/Rent Price\t ";
            strPrecios = strPrecios + ": " + precio_renta_final.ToString("C2", CultureInfo.CreateSpecificCulture("en-us")) + " " + tipo_moneda.nb_moneda + "/SqMt/Month\n";
            strPrecios = strPrecios + "Cuota de Mantenimiento/\t ";
            strPrecios = strPrecios + ": " + mantenimiento.ToString("C2", CultureInfo.CreateSpecificCulture("en-us")) + " " + tipo_moneda.nb_moneda + "/SqMt/Month\n";
            strPrecios = strPrecios + "Maintenance Fee ";

            RectangleF preciosRect = new RectangleF(477, 425, 234, 50);
            IAutoShape preciosShape = slide.Shapes.AppendShape(ShapeType.Rectangle, preciosRect);
            preciosShape.Fill.FillType = FillFormatType.Solid;
            preciosShape.Fill.SolidColor.Color = Color.White;
            preciosShape.Line.Style = TextLineStyle.Single;
            preciosShape.Line.Width = 1;
            preciosShape.Line.SolidFillColor.Color = Color.Black;
            TextParagraph preciosPara = preciosShape.TextFrame.Paragraphs[0];
            preciosPara.Text = strPrecios;
            //preciosPara.Text = " Precio de Venta/Sales Price\t : $0.00 USD\n Precio de Renta/Rent Price\t : $5.20 USD\n Cuota de Mantenimiento/Maintenance Fee\t : $0.30 USD";// + ": " + precio_venta.ToString("C2", CultureInfo.CreateSpecificCulture("en-us")) + " " + tipo_moneda.nb_moneda + "\n: ";
            preciosPara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            preciosPara.FirstTextRange.FontHeight = 6;
            preciosPara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            preciosPara.FirstTextRange.IsBold = TriState.True;
            preciosPara.Alignment = TextAlignmentType.Left;

            RectangleF TituloEspRect = new RectangleF(10, 250, 467, 10);
            IAutoShape TituloEspShape = slide.Shapes.AppendShape(ShapeType.Rectangle, TituloEspRect);
            TituloEspShape.Fill.FillType = FillFormatType.None;
            TituloEspShape.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph TituloEspPara = TituloEspShape.TextFrame.Paragraphs[0];
            TituloEspPara.Text = "Información de la propiedad/Property Information";
            TituloEspPara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            TituloEspPara.FirstTextRange.FontHeight = 8;
            TituloEspPara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            TituloEspPara.FirstTextRange.IsBold = TriState.True;
            TituloEspPara.Alignment = TextAlignmentType.Left;

            ///INFORMACION LADO IZQUIERDO
            RectangleF EspRect = new RectangleF(10, 260, 117, 20);
            IAutoShape EspShape = slide.Shapes.AppendShape(ShapeType.Rectangle, EspRect);
            EspShape.Fill.FillType = FillFormatType.None;
            EspShape.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph EspPara = EspShape.TextFrame.Paragraphs[0];
            EspPara.Text = "Corredor/Market";
            EspPara.FirstTextRange.FontHeight = 8;
            EspPara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            EspPara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            EspPara.FirstTextRange.IsBold = TriState.True;
            EspPara.Alignment = TextAlignmentType.Left;


            if (naves.cd_mercado != null && naves.cd_corredor != null)
            {
                var corredor = navesdao.obtenCorredor(naves.cd_mercado, naves.cd_corredor);

                RectangleF parafrec0 = new RectangleF(127, 260, 117, 20);
                IAutoShape parafshape0 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec0);
                parafshape0.Fill.FillType = FillFormatType.None;
                parafshape0.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph parafTex0 = parafshape0.TextFrame.Paragraphs[0];
                parafTex0.Text = ": " + corredor.nb_corredor;
                parafTex0.FirstTextRange.FontHeight = 8;
                parafTex0.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                parafTex0.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                parafTex0.FirstTextRange.IsBold = TriState.True;
                parafTex0.Alignment = TextAlignmentType.Left;
            }
            else
            {
                RectangleF parafrec0 = new RectangleF(127, 260, 117, 20);
                IAutoShape parafshape0 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec0);
                parafshape0.Fill.FillType = FillFormatType.None;
                parafshape0.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph parafTex0 = parafshape0.TextFrame.Paragraphs[0];
                parafTex0.Text = ": ";
                parafTex0.FirstTextRange.FontHeight = 8;
                parafTex0.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                parafTex0.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                parafTex0.FirstTextRange.IsBold = TriState.True;
                parafTex0.Alignment = TextAlignmentType.Left;
            }

            var gralnaves = navesdao.obtenDatosGralNaves(naves.cd_nave);

            RectangleF infRec1 = new RectangleF(10, 280, 117, 20);
            IAutoShape infShape1 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec1);
            infShape1.Fill.FillType = FillFormatType.None;
            infShape1.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText1 = infShape1.TextFrame.Paragraphs[0];
            infText1.Text = "Año de construccion/Date Built";
            infText1.FirstTextRange.FontHeight = 8;
            infText1.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText1.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText1.FirstTextRange.IsBold = TriState.True;
            infText1.Alignment = TextAlignmentType.Left;

            RectangleF parafrec1 = new RectangleF(127, 280, 117, 20);
            IAutoShape parafshape1 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec1);
            parafshape1.Fill.FillType = FillFormatType.None;
            parafshape1.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph parafTex1 = parafshape1.TextFrame.Paragraphs[0];
            parafTex1.Text = ": " + gralnaves.nu_anio_cons;
            parafTex1.FirstTextRange.FontHeight = 8;
            parafTex1.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            parafTex1.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            parafTex1.FirstTextRange.IsBold = TriState.True;
            parafTex1.Alignment = TextAlignmentType.Left;

            RectangleF infRec2 = new RectangleF(10, 300, 117, 20);
            IAutoShape infShape2 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec2);
            infShape2.Fill.FillType = FillFormatType.None;
            infShape2.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText2 = infShape2.TextFrame.Paragraphs[0];
            infText2.Text = "Nivel de piso Nave/Grade Level Doors";
            infText2.FirstTextRange.FontHeight = 8;
            infText2.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText2.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText2.FirstTextRange.IsBold = TriState.True;
            infText2.Alignment = TextAlignmentType.Left;


            if (gralnaves.cd_Nivel_Piso != null)
            {
                var nivelpiso = navesdao.obtenNivelPiso(gralnaves.cd_Nivel_Piso);

                RectangleF parafrec2 = new RectangleF(127, 300, 117, 20);
                IAutoShape parafshape2 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec2);
                parafshape2.Fill.FillType = FillFormatType.None;
                parafshape2.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph parafTex2 = parafshape2.TextFrame.Paragraphs[0];
                parafTex2.Text = ": " + nivelpiso.nb_nivel_piso;
                parafTex2.FirstTextRange.FontHeight = 8;
                parafTex2.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                parafTex2.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                parafTex2.FirstTextRange.IsBold = TriState.True;
                parafTex2.Alignment = TextAlignmentType.Left;
            }
            else
            {
                RectangleF parafrec2 = new RectangleF(127, 300, 117, 20);
                IAutoShape parafshape2 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec2);
                parafshape2.Fill.FillType = FillFormatType.None;
                parafshape2.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph parafTex2 = parafshape2.TextFrame.Paragraphs[0];
                parafTex2.Text = ": ";
                parafTex2.FirstTextRange.FontHeight = 8;
                parafTex2.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                parafTex2.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                parafTex2.FirstTextRange.IsBold = TriState.True;
                parafTex2.Alignment = TextAlignmentType.Left;
            }

            RectangleF infRec3 = new RectangleF(10, 320, 117, 20);
            IAutoShape infShape3 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec3);
            infShape3.Fill.FillType = FillFormatType.None;
            infShape3.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText3 = infShape3.TextFrame.Paragraphs[0];
            infText3.Text = "Rampas de Acceso/Ramps";
            infText3.FirstTextRange.FontHeight = 8;
            infText3.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText3.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText3.FirstTextRange.IsBold = TriState.True;
            infText3.Alignment = TextAlignmentType.Left;

            RectangleF parafrec3 = new RectangleF(127, 320, 117, 20);
            IAutoShape parafshape3 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec3);
            parafshape3.Fill.FillType = FillFormatType.None;
            parafshape3.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph parafTex3 = parafshape3.TextFrame.Paragraphs[0];
            parafTex3.Text = ": " + gralnaves.nu_rampas;
            parafTex3.FirstTextRange.FontHeight = 8;
            parafTex3.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            parafTex3.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            parafTex3.FirstTextRange.IsBold = TriState.True;
            parafTex3.Alignment = TextAlignmentType.Left;

            RectangleF infRec4 = new RectangleF(10, 340, 117, 20);
            IAutoShape infShape4 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec4);
            infShape4.Fill.FillType = FillFormatType.None;
            infShape4.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText4 = infShape4.TextFrame.Paragraphs[0];
            infText4.Text = "Capacidad de carga Piso/Floor Load Capacity";
            infText4.FirstTextRange.FontHeight = 8;
            infText4.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText4.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText4.FirstTextRange.IsBold = TriState.True;
            infText4.Alignment = TextAlignmentType.Left;

            if (gralnaves.cd_carga != null)
            {

                var carga = navesdao.obtenCarga(gralnaves.cd_carga);


                RectangleF parafrec4 = new RectangleF(127, 340, 117, 20);
                IAutoShape parafshape4 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec4);
                parafshape4.Fill.FillType = FillFormatType.None;
                parafshape4.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph parafTex4 = parafshape4.TextFrame.Paragraphs[0];
                parafTex4.Text = ": " + carga.nb_carga;
                parafTex4.FirstTextRange.FontHeight = 8;
                parafTex4.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                parafTex4.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                parafTex4.FirstTextRange.IsBold = TriState.True;
                parafTex4.Alignment = TextAlignmentType.Left;
            }
            else
            {
                RectangleF parafrec4 = new RectangleF(127, 340, 117, 20);
                IAutoShape parafshape4 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec4);
                parafshape4.Fill.FillType = FillFormatType.None;
                parafshape4.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph parafTex4 = parafshape4.TextFrame.Paragraphs[0];
                parafTex4.Text = ": ";
                parafTex4.FirstTextRange.FontHeight = 8;
                parafTex4.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                parafTex4.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                parafTex4.FirstTextRange.IsBold = TriState.True;
                parafTex4.Alignment = TextAlignmentType.Left;
            }

            RectangleF infRec5 = new RectangleF(10, 360, 117, 20);
            IAutoShape infShape5 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec5);
            infShape5.Fill.FillType = FillFormatType.None;
            infShape5.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText5 = infShape5.TextFrame.Paragraphs[0];
            infText5.Text = "Cajones de Estacionamiento/Parking stalls";
            infText5.FirstTextRange.FontHeight = 8;
            infText5.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText5.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText5.FirstTextRange.IsBold = TriState.True;
            infText5.Alignment = TextAlignmentType.Left;

            if (gralnaves.cd_cajon_est != null)
            {
                var cajon = navesdao.obtenCajonesEst(gralnaves.cd_cajon_est);

                RectangleF parafrec5 = new RectangleF(127, 360, 117, 20);
                IAutoShape parafshape5 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec5);
                parafshape5.Fill.FillType = FillFormatType.None;
                parafshape5.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph parafTex5 = parafshape5.TextFrame.Paragraphs[0];
                parafTex5.Text = ": " + cajon.nb_cajon_est;
                parafTex5.FirstTextRange.FontHeight = 8;
                parafTex5.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                parafTex5.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                parafTex5.FirstTextRange.IsBold = TriState.True;
                parafTex5.Alignment = TextAlignmentType.Left;

            }
            else
            {
                RectangleF parafrec5 = new RectangleF(127, 360, 117, 20);
                IAutoShape parafshape5 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec5);
                parafshape5.Fill.FillType = FillFormatType.None;
                parafshape5.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph parafTex5 = parafshape5.TextFrame.Paragraphs[0];
                parafTex5.Text = ": ";
                parafTex5.FirstTextRange.FontHeight = 8;
                parafTex5.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                parafTex5.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                parafTex5.FirstTextRange.IsBold = TriState.True;
                parafTex5.Alignment = TextAlignmentType.Left;
            }

            RectangleF infRec6 = new RectangleF(10, 380, 117, 20);
            IAutoShape infShape6 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec6);
            infShape6.Fill.FillType = FillFormatType.None;
            infShape6.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText6 = infShape6.TextFrame.Paragraphs[0];
            infText6.Text = "Techumbre/Roof type";
            infText6.FirstTextRange.FontHeight = 8;
            infText6.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText6.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText6.FirstTextRange.IsBold = TriState.True;
            infText6.Alignment = TextAlignmentType.Left;

            if (gralnaves.cd_tp_tech != null)
            {

                var tp_tech = navesdao.obtenTipoTechumbre(gralnaves.cd_tp_tech);

                RectangleF parafrec6 = new RectangleF(127, 380, 117, 20);
                IAutoShape parafshape6 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec6);
                parafshape6.Fill.FillType = FillFormatType.None;
                parafshape6.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph parafTex6 = parafshape6.TextFrame.Paragraphs[0];
                parafTex6.Text = ": " + tp_tech.nb_tp_tech;
                parafTex6.FirstTextRange.FontHeight = 8;
                parafTex6.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                parafTex6.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                parafTex6.FirstTextRange.IsBold = TriState.True;
                parafTex6.Alignment = TextAlignmentType.Left;
            }
            else {
                RectangleF parafrec6 = new RectangleF(127, 380, 117, 20);
                IAutoShape parafshape6 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec6);
                parafshape6.Fill.FillType = FillFormatType.None;
                parafshape6.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph parafTex6 = parafshape6.TextFrame.Paragraphs[0];
                parafTex6.Text = ": ";
                parafTex6.FirstTextRange.FontHeight = 8;
                parafTex6.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                parafTex6.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                parafTex6.FirstTextRange.IsBold = TriState.True;
                parafTex6.Alignment = TextAlignmentType.Left;
            }

            RectangleF infRec7 = new RectangleF(10, 400, 117, 20);
            IAutoShape infShape7 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec7);
            infShape7.Fill.FillType = FillFormatType.None;
            infShape7.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText7 = infShape7.TextFrame.Paragraphs[0];
            infText7.Text = "Altura Mínima-Máxima/Min-Max Height";
            infText7.FirstTextRange.FontHeight = 8;
            infText7.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText7.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText7.FirstTextRange.IsBold = TriState.True;
            infText7.Alignment = TextAlignmentType.Left;

            RectangleF parafrec7 = new RectangleF(127, 400, 117, 20);
            IAutoShape parafshape7 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec7);
            parafshape7.Fill.FillType = FillFormatType.None;
            parafshape7.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph parafTex7 = parafshape7.TextFrame.Paragraphs[0];
            parafTex7.Text = ": " + gralnaves.nu_altura +" M/"+  String.Format("{0:#,##0.00}", Convert.ToDouble(gralnaves.nu_altura) * 3.28084) + " ft";
            parafTex7.FirstTextRange.FontHeight = 8;
            parafTex7.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            parafTex7.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            parafTex7.FirstTextRange.IsBold = TriState.True;
            parafTex7.Alignment = TextAlignmentType.Left;

            var serviciosnaves = navesdao.obtenServiciosNaves(naves.cd_nave);

            RectangleF infRec8 = new RectangleF(10, 420, 117, 20);
            IAutoShape infShape8 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec8);
            infShape8.Fill.FillType = FillFormatType.None;
            infShape8.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText8 = infShape8.TextFrame.Paragraphs[0];
            infText8.Text = "Telefonía/Telephone Service";
            infText8.FirstTextRange.FontHeight = 8;
            infText8.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText8.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText8.FirstTextRange.IsBold = TriState.True;
            infText8.Alignment = TextAlignmentType.Left;

            if (serviciosnaves.cd_telefonia != null)
            {
                var telefonia = navesdao.obtenTelefonia(serviciosnaves.cd_telefonia);

                RectangleF parafrec8 = new RectangleF(127, 420, 117, 20);
                IAutoShape parafshape8 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec8);
                parafshape8.Fill.FillType = FillFormatType.None;
                parafshape8.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph parafTex8 = parafshape8.TextFrame.Paragraphs[0];
                parafTex8.Text = ": " + telefonia.nb_telefonia;
                parafTex8.FirstTextRange.FontHeight = 8;
                parafTex8.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                parafTex8.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                parafTex8.FirstTextRange.IsBold = TriState.True;
                parafTex8.Alignment = TextAlignmentType.Left;
            }
            else
            {
                RectangleF parafrec8 = new RectangleF(127, 420, 117, 20);
                IAutoShape parafshape8 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec8);
                parafshape8.Fill.FillType = FillFormatType.None;
                parafshape8.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph parafTex8 = parafshape8.TextFrame.Paragraphs[0];
                parafTex8.Text = ": ";
                parafTex8.FirstTextRange.FontHeight = 8;
                parafTex8.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                parafTex8.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                parafTex8.FirstTextRange.IsBold = TriState.True;
                parafTex8.Alignment = TextAlignmentType.Left;
            }

            RectangleF infRec9 = new RectangleF(10, 440, 117, 20);
            IAutoShape infShape9 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec9);
            infShape9.Fill.FillType = FillFormatType.None;
            infShape9.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText9 = infShape9.TextFrame.Paragraphs[0];
            infText9.Text = "Estacionamiento Trailers/Drop Lots";
            infText9.FirstTextRange.FontHeight = 8;
            infText9.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText9.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText9.FirstTextRange.IsBold = TriState.True;
            infText9.Alignment = TextAlignmentType.Left;

            RectangleF parafrec9 = new RectangleF(127, 440, 117, 20);
            IAutoShape parafshape9 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec9);
            parafshape9.Fill.FillType = FillFormatType.None;
            parafshape9.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph parafTex9 = parafshape9.TextFrame.Paragraphs[0];
            parafTex9.Text = ": " + gralnaves.nu_caj_trailer.ToString();
            parafTex9.FirstTextRange.FontHeight = 8;
            parafTex9.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            parafTex9.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            parafTex9.FirstTextRange.IsBold = TriState.True;
            parafTex9.Alignment = TextAlignmentType.Left;


            RectangleF infRec10 = new RectangleF(244, 260, 117, 20);
            IAutoShape infShape10 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec10);
            infShape10.Fill.FillType = FillFormatType.None;
            infShape10.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText10 = infShape10.TextFrame.Paragraphs[0];
            infText10.Text = "Sistema de Ventilación/Ventilation System";
            infText10.FirstTextRange.FontHeight = 8;
            infText10.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText10.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText10.FirstTextRange.IsBold = TriState.True;
            infText10.Alignment = TextAlignmentType.Left;

            if (gralnaves.cd_hvac != null)
            {
                var ventilacion = navesdao.obtenVentilacion(gralnaves.cd_hvac);

                RectangleF parafrec10 = new RectangleF(361, 260, 117, 20);
                IAutoShape parafshape10 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec10);
                parafshape10.Fill.FillType = FillFormatType.None;
                parafshape10.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph parafTex10 = parafshape10.TextFrame.Paragraphs[0];
                parafTex10.Text = ": " + ventilacion.nb_hvac;
                parafTex10.FirstTextRange.FontHeight = 8;
                parafTex10.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                parafTex10.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                parafTex10.FirstTextRange.IsBold = TriState.True;
                parafTex10.Alignment = TextAlignmentType.Left;
            }
            else
            {
                RectangleF parafrec10 = new RectangleF(361, 260, 117, 20);
                IAutoShape parafshape10 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec10);
                parafshape10.Fill.FillType = FillFormatType.None;
                parafshape10.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph parafTex10 = parafshape10.TextFrame.Paragraphs[0];
                parafTex10.Text = ": ";
                parafTex10.FirstTextRange.FontHeight = 8;
                parafTex10.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                parafTex10.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                parafTex10.FirstTextRange.IsBold = TriState.True;
                parafTex10.Alignment = TextAlignmentType.Left;
            }

            RectangleF infRec11 = new RectangleF(244, 280, 117, 20);
            IAutoShape infShape11 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec11);
            infShape11.Fill.FillType = FillFormatType.None;
            infShape11.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText11 = infShape11.TextFrame.Paragraphs[0];
            infText11.Text = "Puertas de Anden/Dock High Doors";
            infText11.FirstTextRange.FontHeight = 8;
            infText11.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText11.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText11.FirstTextRange.IsBold = TriState.True;
            infText11.Alignment = TextAlignmentType.Left;

            RectangleF parafrec11 = new RectangleF(361, 280, 117, 20);
            IAutoShape parafshape11 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec11);
            parafshape11.Fill.FillType = FillFormatType.None;
            parafshape11.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph parafTex11 = parafshape11.TextFrame.Paragraphs[0];
            parafTex11.Text = ": " + gralnaves.nu_puertas;
            parafTex11.FirstTextRange.FontHeight = 8;
            parafTex11.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            parafTex11.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            parafTex11.FirstTextRange.IsBold = TriState.True;
            parafTex11.Alignment = TextAlignmentType.Left;

            RectangleF infRec12 = new RectangleF(244, 300, 117, 20);
            IAutoShape infShape12 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec12);
            infShape12.Fill.FillType = FillFormatType.None;
            infShape12.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText12 = infShape12.TextFrame.Paragraphs[0];
            infText12.Text = "Superficie de Nave/Warehouse Rentable:";
            infText12.FirstTextRange.FontHeight = 8;
            infText12.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText12.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText12.FirstTextRange.IsBold = TriState.True;
            infText12.Alignment = TextAlignmentType.Left;

            RectangleF parafrec12 = new RectangleF(361, 300, 117, 20);
            IAutoShape parafshape12 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec12);
            parafshape12.Fill.FillType = FillFormatType.None;
            parafshape12.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph parafTex12 = parafshape12.TextFrame.Paragraphs[0];
            parafTex12.Text = ": " + String.Format("{0:#,##0.00}", gralnaves.nu_superficie) + " M2 / " + String.Format("{0:#,##0.00}", Convert.ToDouble(gralnaves.nu_superficie) * 10.7639) + " Ft2\n";
            parafTex12.FirstTextRange.FontHeight = 8;
            parafTex12.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            parafTex12.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            parafTex12.FirstTextRange.IsBold = TriState.True;
            parafTex12.Alignment = TextAlignmentType.Left;

            RectangleF infRec13 = new RectangleF(244, 320, 117, 20);
            IAutoShape infShape13 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec13);
            infShape13.Fill.FillType = FillFormatType.None;
            infShape13.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText13 = infShape13.TextFrame.Paragraphs[0];
            infText13.Text = "Superficie Terreno/Land Size";
            infText13.FirstTextRange.FontHeight = 8;
            infText13.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText13.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText13.FirstTextRange.IsBold = TriState.True;
            infText13.Alignment = TextAlignmentType.Left;

            RectangleF parafrec13 = new RectangleF(361, 320, 117, 20);
            IAutoShape parafshape13 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec13);
            parafshape13.Fill.FillType = FillFormatType.None;
            parafshape13.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph parafTex13 = parafshape13.TextFrame.Paragraphs[0];
            parafTex13.Text = ": " + String.Format("{0:#,##0.00}", gralnaves.nu_bodega) + " M2 / " + String.Format("{0:#,##0.00}", Convert.ToDouble(gralnaves.nu_bodega) * 10.7639) + " Ft2\n";
            parafTex13.FirstTextRange.FontHeight = 8;
            parafTex13.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            parafTex13.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            parafTex13.FirstTextRange.IsBold = TriState.True;
            parafTex13.Alignment = TextAlignmentType.Left;

            RectangleF infRec14 = new RectangleF(244, 340, 117, 20);
            IAutoShape infShape14 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec14);
            infShape14.Fill.FillType = FillFormatType.None;
            infShape14.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText14 = infShape14.TextFrame.Paragraphs[0];
            infText14.Text = "Disponibilidad Total/Total Availability";
            infText14.FirstTextRange.FontHeight = 8;
            infText14.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText14.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText14.FirstTextRange.IsBold = TriState.True;
            infText14.Alignment = TextAlignmentType.Left;

            RectangleF parafrec14 = new RectangleF(361, 340, 117, 20);
            IAutoShape parafshape14 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec14);
            parafshape14.Fill.FillType = FillFormatType.None;
            parafshape14.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph parafTex14 = parafshape14.TextFrame.Paragraphs[0];
            parafTex14.Text = ": " + String.Format("{0:#,##0.00}", gralnaves.nu_disponibilidad) + " M2 / " + String.Format("{0:#,##0.00}", Convert.ToDouble(gralnaves.nu_disponibilidad) * 10.7639) + " Ft2\n";
            parafTex14.FirstTextRange.FontHeight = 8;
            parafTex14.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            parafTex14.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            parafTex14.FirstTextRange.IsBold = TriState.True;
            parafTex14.Alignment = TextAlignmentType.Left;

            RectangleF infRec15 = new RectangleF(244, 360, 117, 20);
            IAutoShape infShape15 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec15);
            infShape15.Fill.FillType = FillFormatType.None;
            infShape15.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText15 = infShape15.TextFrame.Paragraphs[0];
            infText15.Text = "Tipo de Lámparas/Lightning";
            infText15.FirstTextRange.FontHeight = 8;
            infText15.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText15.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText15.FirstTextRange.IsBold = TriState.True;
            infText15.Alignment = TextAlignmentType.Left;

            if (gralnaves.cd_tp_lampara != null)
            {
                var tipo_lampara = navesdao.obtenTipoLampara(gralnaves.cd_tp_lampara);

                RectangleF parafrec15 = new RectangleF(361, 360, 117, 20);
                IAutoShape parafshape15 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec15);
                parafshape15.Fill.FillType = FillFormatType.None;
                parafshape15.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph parafTex15 = parafshape15.TextFrame.Paragraphs[0];
                parafTex15.Text = ": " + tipo_lampara.nb_tp_lampara;
                parafTex15.FirstTextRange.FontHeight = 8;
                parafTex15.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                parafTex15.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                parafTex15.FirstTextRange.IsBold = TriState.True;
                parafTex15.Alignment = TextAlignmentType.Left;
            }
            else
            {
                RectangleF parafrec15 = new RectangleF(361, 360, 117, 20);
                IAutoShape parafshape15 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec15);
                parafshape15.Fill.FillType = FillFormatType.None;
                parafshape15.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph parafTex15 = parafshape15.TextFrame.Paragraphs[0];
                parafTex15.Text = ": ";
                parafTex15.FirstTextRange.FontHeight = 8;
                parafTex15.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                parafTex15.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                parafTex15.FirstTextRange.IsBold = TriState.True;
                parafTex15.Alignment = TextAlignmentType.Left;
            }

            RectangleF infRec16 = new RectangleF(244, 380, 117, 20);
            IAutoShape infShape16 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec16);
            infShape16.Fill.FillType = FillFormatType.None;
            infShape16.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText16 = infShape16.TextFrame.Paragraphs[0];
            infText16.Text = "Energía eléctrica/Power Supply";
            infText16.FirstTextRange.FontHeight = 8;
            infText16.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText16.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText16.FirstTextRange.IsBold = TriState.True;
            infText16.Alignment = TextAlignmentType.Left;

            RectangleF parafrec16 = new RectangleF(361, 380, 117, 20);
            IAutoShape parafshape16 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec16);
            parafshape16.Fill.FillType = FillFormatType.None;
            parafshape16.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph parafTex16 = parafshape16.TextFrame.Paragraphs[0];
            parafTex16.Text = ": " + serviciosnaves.nu_kva + " KvA";
            parafTex16.FirstTextRange.FontHeight = 8;
            parafTex16.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            parafTex16.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            parafTex16.FirstTextRange.IsBold = TriState.True;
            parafTex16.Alignment = TextAlignmentType.Left;

            RectangleF infRec17 = new RectangleF(244, 400, 117, 20);
            IAutoShape infShape17 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec17);
            infShape17.Fill.FillType = FillFormatType.None;
            infShape17.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText17 = infShape17.TextFrame.Paragraphs[0];
            infText17.Text = "Agua Potable/Drinking Water";
            infText17.FirstTextRange.FontHeight = 8;
            infText17.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText17.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText17.FirstTextRange.IsBold = TriState.True;
            infText17.Alignment = TextAlignmentType.Left;

            RectangleF parafrec17 = new RectangleF(361, 400, 117, 20);
            IAutoShape parafshape17 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec17);
            parafshape17.Fill.FillType = FillFormatType.None;
            parafshape17.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph parafTex17 = parafshape17.TextFrame.Paragraphs[0];
            parafTex17.Text = ": " + (!serviciosnaves.st_aguapozo ? "Servicio Municipal – Municipal Service" : "Pozo – Water well");
            parafTex17.FirstTextRange.FontHeight = 8;
            parafTex17.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            parafTex17.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            parafTex17.FirstTextRange.IsBold = TriState.True;
            parafTex17.Alignment = TextAlignmentType.Left;

            RectangleF infRec18 = new RectangleF(244, 420, 117, 20);
            IAutoShape infShape18 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec18);
            infShape18.Fill.FillType = FillFormatType.None;
            infShape18.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText18 = infShape18.TextFrame.Paragraphs[0];
            infText18.Text = "Sistema contra incendio/Fire System";
            infText18.FirstTextRange.FontHeight = 8;
            infText18.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText18.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText18.FirstTextRange.IsBold = TriState.True;
            infText18.Alignment = TextAlignmentType.Left;

            if (gralnaves.cd_sist_inc != null)
            {
                var sistemaincendio = navesdao.obtenSistIncendios(gralnaves.cd_sist_inc);

                RectangleF parafrec18 = new RectangleF(361, 420, 117, 20);
                IAutoShape parafshape18 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec18);
                parafshape18.Fill.FillType = FillFormatType.None;
                parafshape18.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph parafTex18 = parafshape18.TextFrame.Paragraphs[0];
                parafTex18.Text = ": " + sistemaincendio.nb_sist_inc;
                parafTex18.FirstTextRange.FontHeight = 8;
                parafTex18.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                parafTex18.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                parafTex18.FirstTextRange.IsBold = TriState.True;
                parafTex18.Alignment = TextAlignmentType.Left;
            }
            else
            {
                RectangleF parafrec18 = new RectangleF(361, 420, 117, 20);
                IAutoShape parafshape18 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec18);
                parafshape18.Fill.FillType = FillFormatType.None;
                parafshape18.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph parafTex18 = parafshape18.TextFrame.Paragraphs[0];
                parafTex18.Text = ": ";
                parafTex18.FirstTextRange.FontHeight = 8;
                parafTex18.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                parafTex18.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                parafTex18.FirstTextRange.IsBold = TriState.True;
                parafTex18.Alignment = TextAlignmentType.Left;
            }

            RectangleF infRec19 = new RectangleF(244, 440, 117, 20);
            IAutoShape infShape19 = slide.Shapes.AppendShape(ShapeType.Rectangle, infRec19);
            infShape19.Fill.FillType = FillFormatType.None;
            infShape19.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph infText19 = infShape19.TextFrame.Paragraphs[0];
            infText19.Text = "Tipo de Construcción/Walls Type";
            infText19.FirstTextRange.FontHeight = 8;
            infText19.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            infText19.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            infText19.FirstTextRange.IsBold = TriState.True;
            infText19.Alignment = TextAlignmentType.Left;

            if (gralnaves.cd_tp_construccion != null)
            {
                var tipo_construccion = navesdao.obtenTipoConstruccion(gralnaves.cd_tp_construccion);

                RectangleF parafrec19 = new RectangleF(361, 440, 117, 20);
                IAutoShape parafshape19 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec19);
                parafshape19.Fill.FillType = FillFormatType.None;
                parafshape19.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph parafTex19 = parafshape19.TextFrame.Paragraphs[0];
                parafTex19.Text = ": " + tipo_construccion.nb_tp_construccion;
                parafTex19.FirstTextRange.FontHeight = 8;
                parafTex19.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                parafTex19.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                parafTex19.FirstTextRange.IsBold = TriState.True;
                parafTex19.Alignment = TextAlignmentType.Left;
            }
            else
            {
                RectangleF parafrec19 = new RectangleF(361, 440, 117, 20);
                IAutoShape parafshape19 = slide.Shapes.AppendShape(ShapeType.Rectangle, parafrec19);
                parafshape19.Fill.FillType = FillFormatType.None;
                parafshape19.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph parafTex19 = parafshape19.TextFrame.Paragraphs[0];
                parafTex19.Text = ": ";
                parafTex19.FirstTextRange.FontHeight = 8;
                parafTex19.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                parafTex19.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                parafTex19.FirstTextRange.IsBold = TriState.True;
                parafTex19.Alignment = TextAlignmentType.Left;
            }

            IAutoShape shape = slide.Shapes.AppendShape(ShapeType.Line, new RectangleF(10, 480, 700,0));

            RectangleF footerRec = new RectangleF(10, 480, 700, 40);
            IAutoShape footershape1 = slide.Shapes.AppendShape(ShapeType.Rectangle, footerRec);
            footershape1.Fill.FillType = FillFormatType.None;
            footershape1.ShapeStyle.LineColor.Color = Color.Empty;
            TextParagraph footerText = footershape1.TextFrame.Paragraphs[0];
            footerText.Text = "This report contains information available to the public and has been relied upon by Cushman & Wakefield on the basis that it is accurate and complete. Cushman & Wakefield accepts no responsibility if this should prove not to be the case. No warranty or representation, express or implied, is made to the accuracy or completeness of the information contained herein, and same is submitted subject to errors, omissions, change of price, rental or other conditions, withdrawal without notice, and to any special listing conditions imposed by our principals.";
            footerText.FirstTextRange.FontHeight = 6;
            footerText.FirstTextRange.Fill.FillType = FillFormatType.Solid;
            footerText.FirstTextRange.Fill.SolidColor.Color = Color.Black;
            footerText.FirstTextRange.IsBold = TriState.True;
            footerText.Alignment = TextAlignmentType.Left;

            RectangleF LogoRect = new RectangleF(600, 510, 100, 30);
            string rutaArchvio = ruta.Replace("repositorio", "image") + "logo_CW.png";
            IEmbedImage LogoShape = slide.Shapes.AppendEmbedImage(ShapeType.Rectangle, rutaArchvio, LogoRect);
            LogoShape.Fill.FillType = FillFormatType.None;
            //LogoShape.ShapeStyle.LineColor.Color = Color.Empty;

            // diapositiva++;
            SizeF pptSize = ppt.SlideSize.Size;
            ppt.SaveToFile(ruta + "Naves_rpt.pptx", FileFormat.Pptx2010);

        }
        public static void rep_powerpoint(int cd_estado, int cd_municipio, int cd_colonia, string ruta)
        {
            String rutaArchivo = "";
            int diapositiva = 0;// Contador de diapositivas.
            //cushman db = new cushman();
            PPTReport.ModifyInMemory.ActivateMemoryPatching();
            Presentation ppt = new Presentation();
            ISlide slide = ppt.Slides[diapositiva];
           // diapositiva++;
            SizeF pptSize = ppt.SlideSize.Size;

            daoNaves dNaves = new daoNaves();
            List<tsg002_nave_industrial> lnaves = dNaves.listNaves(cd_estado, cd_municipio, cd_colonia);

            tsg037_estados estado = dNaves.obtenestadoid(cd_estado);

            utils util = new utils();

            

            int i = 1;
            string letra;
            int grupo = 1;
            int objeto = 1;
            string coordenadas = null;
            string[] arrayCoordenadas = null;
            string posiciones = null;

            int tRegistros = lnaves.Count();
            int iRows = 0;
            ITable table = null;
            foreach (var nave in lnaves)
            {
                int res = dNaves.existeNave(nave.cd_nave);
                if (res != 0) { 
                //Encabezado primera diapositiva.
                if (i == 1)
                {
                    //Add title
                    RectangleF titleRect = new RectangleF(4, 2, 400, 50);
                    IAutoShape titleShape = slide.Shapes.AppendShape(ShapeType.Rectangle, titleRect);
                    titleShape.Fill.FillType = FillFormatType.None;
                    titleShape.ShapeStyle.LineColor.Color = Color.Empty;
                    TextParagraph titlePara = titleShape.TextFrame.Paragraphs[0];
                    titlePara.Text = "Mapa de Ubicacion / Location Map";
                    titlePara.FirstTextRange.FontHeight = 20;
                    titlePara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                    titlePara.FirstTextRange.Fill.SolidColor.Color = Color.Blue;
                    titlePara.Alignment = TextAlignmentType.Left;

                    //Add subtitle
                    RectangleF subtitleRect = new RectangleF(4, 15, 400, 50);
                    IAutoShape subtitleShape = slide.Shapes.AppendShape(ShapeType.Rectangle, subtitleRect);
                    subtitleShape.Fill.FillType = FillFormatType.None;
                    subtitleShape.ShapeStyle.LineColor.Color = Color.Empty;
                    TextParagraph subtitlePara = subtitleShape.TextFrame.Paragraphs[0];
                    subtitlePara.Text = estado.nb_estado;
                    subtitlePara.FirstTextRange.FontHeight = 12;
                    subtitlePara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                    subtitlePara.FirstTextRange.Fill.SolidColor.Color = Color.Red;
                    subtitlePara.Alignment = TextAlignmentType.Left;

                    double[] widths = { 20, 160 };
                    double[] heights = { 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8 };

                    table = slide.Shapes.AppendTable(4, 50, widths, heights);
                    table[0, 0].TextFrame.Text = "Opciones Grupo " + grupo + " / Group " + grupo + " Options";
                    table.MergeCells(table[0, 0], table[1, 0], false);
                    grupo++;
                    i++;
                }

                if (i <= 20)
                {
                    letra = util.ObteneLetra(i - 1);
                    table[0, i - 1].TextFrame.Text = letra;
                    table[1, i - 1].TextFrame.Text = nave.nb_parque + " / " + nave.nb_nave;

                    //
                    
                    //obtengo coordenadas.
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

                    arrayCoordenadas = coordenadas.Split('|');
                    decimal valor = arrayCoordenadas.Count() / 2;
                    int result = Convert.ToInt16(Math.Truncate(valor));
                    string imagemapa = "https://maps.googleapis.com/maps/api/staticmap?center=" + arrayCoordenadas[result - 1] + "&zoom =14&size=640x840&&maptype=roadmap&" + posiciones + "&key=AIzaSyDhfzatgC_64higR2LlLGkWldmTBudiRWY";

                    rutaArchivo = util.GeneraImagen(imagemapa, "mapa_" + grupo, ruta);
                    RectangleF logoRect = new RectangleF(210, 50, 500, 480);
                    IEmbedImage image = slide.Shapes.AppendEmbedImage(ShapeType.Rectangle, rutaArchivo, logoRect);
                    image.Line.FillType = FillFormatType.None;

                    foreach (TableRow row in table.TableRows)
                    {
                        foreach (Cell cell in row)
                        {
                            cell.TextFrame.TextRange.FontHeight = 8;
                        }
                    }

                    i = 0;
                    objeto = 1;
                    coordenadas = null;
                    posiciones = null;
                    diapositiva++;
                    ppt.Slides.Append();
                    slide = ppt.Slides[diapositiva];
                }
                i++;
             }
            }

            foreach(TableRow row in table.TableRows)
            {
                foreach(Cell cell in row)
                {
                    cell.TextFrame.TextRange.FontHeight = 8;
                }
            }

            if (i > 1)
            {
                arrayCoordenadas = coordenadas.Split('|');
                decimal valor = arrayCoordenadas.Count() / 2;
                int result = Convert.ToInt16(Math.Truncate(valor));
                string imagemapa = "https://maps.googleapis.com/maps/api/staticmap?center=" + arrayCoordenadas[result - 1] + "&zoom =14&size=640x840&&maptype=roadmap&" + posiciones + "&key=AIzaSyDhfzatgC_64higR2LlLGkWldmTBudiRWY";

                rutaArchivo = util.GeneraImagen(imagemapa, "mapa_" + grupo, ruta);
                RectangleF logoRect = new RectangleF(210, 50,500, 480);
                IEmbedImage image = slide.Shapes.AppendEmbedImage(ShapeType.Rectangle, rutaArchivo, logoRect);
                image.Line.FillType = FillFormatType.None;

                i = 0;
                objeto = 1;
                coordenadas = null;
                posiciones = null;
                diapositiva++;
                ppt.Slides.Append();
                slide = ppt.Slides[diapositiva];

            }
            i = 1;
            grupo = 1;
            objeto = 1;
            foreach (var nave in lnaves)
            {
                int res = dNaves.existeNave(nave.cd_nave);
                if (res != 0) { 
                letra = util.ObteneLetra(i);
                //Add title
                RectangleF titleRect = new RectangleF(4, 2, 700, 50);
                    
                IAutoShape titleShape = slide.Shapes.AppendShape(ShapeType.Rectangle, titleRect);
                titleShape.Fill.FillType = FillFormatType.None;
                titleShape.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph titlePara = titleShape.TextFrame.Paragraphs[0];
                titlePara.Text = "Grupo" + grupo + " / Group " + grupo + "\n" + "Opción / Option " + letra + ": " + nave.nb_parque + "/" + nave.nb_nave;
                titlePara.FirstTextRange.FontHeight = 20;
                titlePara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                titlePara.FirstTextRange.Fill.SolidColor.Color = Color.Red;
                titlePara.Alignment = TextAlignmentType.Left;


                RectangleF titleRectS = new RectangleF(4, 30, 700, 50);

                IAutoShape titleShapeS = slide.Shapes.AppendShape(ShapeType.Rectangle, titleRectS);
                titleShapeS.Fill.FillType = FillFormatType.None;
                titleShapeS.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph titleParaS = titleShapeS.TextFrame.Paragraphs[0];
                titleParaS.Text = "Plan Maestro / Master Plan";
                titleParaS.FirstTextRange.FontHeight = 12;
                titleParaS.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                titleParaS.FirstTextRange.Fill.SolidColor.Color = Color.Blue;
                titleParaS.Alignment = TextAlignmentType.Left;


                if (nave.nb_posicion != null || nave.nb_poligono != null)
                {
                    string strUrlPoligono = "https://maps.googleapis.com/maps/api/staticmap?center=" + nave.nb_posicion + "&zoom=15&size=840x640&maptype=hybrid&path=fillcolor:red|" + nave.nb_poligono.TrimEnd('|') + "&key=AIzaSyDhfzatgC_64higR2LlLGkWldmTBudiRWY";
                    rutaArchivo = util.GeneraImagen(strUrlPoligono, "poligono" + nave.cd_nave, ruta);
                    RectangleF logoRect = new RectangleF(4, 70, 700, 450);
                    IEmbedImage image = slide.Shapes.AppendEmbedImage(ShapeType.Rectangle, rutaArchivo, logoRect);
                    image.Line.FillType = FillFormatType.None;
                }

                // RectangleF LogoRect = new RectangleF(600, 510, 100, 30);
                //string rutaArchvio = ruta.Replace("repositorio", "image") + "logo_CW.png";
                //IEmbedImage LogoShape = slide.Shapes.AppendEmbedImage(ShapeType.Rectangle, rutaArchvio, LogoRect);
                //LogoShape.Fill.FillType = FillFormatType.None;

                diapositiva++;
                ppt.Slides.Append();
                slide = ppt.Slides[diapositiva];


                /************empieza la parte del plano y detalle*///
                RectangleF titleRect2 = new RectangleF(4, 2, 700, 50);
                IAutoShape titleShape2 = slide.Shapes.AppendShape(ShapeType.RoundCornerRectangle, titleRect2);
                titleShape2.Fill.FillType = FillFormatType.None;
                titleShape2.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph titlePara2 = titleShape2.TextFrame.Paragraphs[0];
                titlePara2.Text = "Grupo" + grupo + " / Group " + grupo + "\n" + "Opción / Option " + letra + ": " + nave.nb_parque + "/" + nave.nb_nave;
                titlePara2.FirstTextRange.FontHeight = 20;
                titlePara2.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                titlePara2.FirstTextRange.Fill.SolidColor.Color = Color.Red;
                titlePara2.Alignment = TextAlignmentType.Left;

                RectangleF titleRect3 = new RectangleF(4, 50, 700, 25);
                IAutoShape titleShape3 = slide.Shapes.AppendShape(ShapeType.RoundCornerRectangle, titleRect3);
                    titleShape3.Fill.FillType = FillFormatType.None;
                    titleShape3.ShapeStyle.LineColor.Color = Color.Empty;
                TextParagraph titlePara3 = titleShape3.TextFrame.Paragraphs[0];
                    titlePara3.Text = "Diseño / Layout";
                    titlePara3.FirstTextRange.FontHeight = 10;
                    titlePara3.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                    titlePara3.FirstTextRange.Fill.SolidColor.Color = Color.Blue;
                    titlePara3.Alignment = TextAlignmentType.Left;

                    tsg045_imagenes_naves imagenNave = dNaves.obteneImagenesNaves(nave.cd_nave);

                clsStorage cs = new clsStorage();

                    if (System.IO.File.Exists(ruta + "N_" + nave.cd_nave + ".png"))
                    {

                        // String strUrlImagen = cs.descargaBlob(nave.cd_nave, imagenNave.nb_archivo);
                        //rutaArchivo = util.GeneraImagen(strUrlImagen, imagenNave.nb_archivo, ruta);
                        RectangleF logoRect2 = new RectangleF(4, 70, 400, 300);
                        IEmbedImage image2 = slide.Shapes.AppendEmbedImage(ShapeType.Rectangle, ruta + "N_" + nave.cd_nave + ".png", logoRect2);
                        image2.Line.FillType = FillFormatType.None;

                    }
                
                RectangleF textRect = new RectangleF(450, 70, 250, 400);

                
                IAutoShape textShape = slide.Shapes.AppendShape(ShapeType.Rectangle, textRect);
                //Content format
                string text = dNaves.obtenDetalle(nave.cd_nave);
                textShape.AppendTextFrame(text);

                TextParagraph contentPara = textShape.TextFrame.Paragraphs[0];
                contentPara.FirstTextRange.Fill.FillType = FillFormatType.Solid;
                contentPara.FirstTextRange.Fill.SolidColor.Color = Color.Black;
                contentPara.FirstTextRange.FontHeight = 10;
                contentPara.Alignment = TextAlignmentType.Left;

                    RectangleF LogoRect2 = new RectangleF(600, 510, 100, 30);
                    string rutaArchvio2 = ruta.Replace("repositorio", "image") + "logo_CW.png";
                    IEmbedImage LogoShape2 = slide.Shapes.AppendEmbedImage(ShapeType.Rectangle, rutaArchvio2, LogoRect2);
                    LogoShape2.Fill.FillType = FillFormatType.None;

                    //TextRange textrange = textShape.TextFrame.TextRange;

                    //textrange.FontHeight = 12;


                    //Shape format
                    textShape.ShapeStyle.LineColor.Color = Color.Black;
                textShape.Fill.FillType = FillFormatType.None;

                //textShape.Fill.Gradient.GradientStops.Append(0, Color.LightBlue);
                //textShape.Fill.Gradient.GradientStops.Append(1, Color.LightGreen);


                diapositiva++;
                ppt.Slides.Append();
                slide = ppt.Slides[diapositiva];
                i++;
                }

            }
            ppt.SaveToFile(ruta + "reporte.pptx", FileFormat.Pptx2010);
            util.EliminaArchivos(ruta);
        }

    }
}
