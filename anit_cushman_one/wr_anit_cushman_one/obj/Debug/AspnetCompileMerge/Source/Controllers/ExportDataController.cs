using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI;
using wr_anit_cushman_one.Models;

namespace wr_anit_cushman_one.Controllers
{
    public class ExportDataController : Controller
    {
        private CushmanContext db = new CushmanContext();
        // GET: ExportData
        //public ActionResult Index()
        //{
        //    var nave = db.tsg002_nave_industrial.Include(t => t.tsg023_ni_precio)
        //        .Include(t => t.tsg009_ni_dt_gral)
        //        .Include(t => t.tsg020_ni_servicio)
        //        .Include(t => t.tsg025_ni_contacto)
        //        .Include(t => t.tsg007_mercado)
        //        .Include(t => t.tsg008_corredor_ind);
        //        //.Select("new (nb_mercado,nb_corredor,nb_parque, nb_nave,nb_calle,nu_direcion,nu_cp,cd_estado,cd_ciudad,cd_municipio,cd_colonia,nb_dir_nave)");

        //    return View( nave.ToList());
        //}
        public void Index(int estado, int municipio, int colonia)
        {            
            var query = from tsg002 in db.tsg002_nave_industrial
                        join tsg009 in db.tsg009_ni_dt_gral                      
                        on tsg002.cd_nave equals tsg009.cd_nave
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
                        join tsg010 in db.tsg010_area_of
                        on tsg009.cd_area equals tsg010.cd_area
                        join tsg023 in db.tsg023_ni_precio
                        on tsg002.cd_nave equals tsg023.cd_nave
                        join tsg011 in db.tsg011_carga_piso
                        on tsg009.cd_carga equals tsg011.cd_carga
                        join tsg012 in db.tsg012_sist_incendio
                        on tsg009.cd_sist_inc equals tsg012.cd_sist_inc
                        join tsg013 in db.tsg013_tp_construccion
                        on tsg009.cd_tp_construccion equals tsg013.cd_tp_construccion
                        join tsg017 in db.tsg017_st_entrega
                        on tsg002.st_parque_ind equals tsg017.cd_st_entrega                        
                        join tsg014 in db.tsg014_tp_lampara
                        on tsg009.cd_tp_lampara equals tsg014.cd_tp_lampara
                        join tsg018 in db.tsg018_ilum_nat
                        on tsg009.cd_ilum_nat equals tsg018.cd_ilum_nat
                        where ( tsg002.cd_estado == estado && tsg009.nu_disponibilidad >0)
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
                            tsg002.cd_colonia,
                            tsg009.nu_superficie,
                            tsg009.nu_bodega,
                            tsg009.nu_disponibilidad,
                            tsg009.nu_min_divisible,
                            tsg010.nb_area,
                            tsg023.im_renta,
                            tsg023.im_venta,
                            tsg023.nu_tipo_cambio,
                            tsg009.nu_altura,
                            tsg009.nu_puertas,
                            tsg009.nu_rampas,
                            tsg011.nb_carga,
                            tsg012.nb_sist_inc,
                            tsg013.nb_tp_construccion,
                            tsg017.nb_st_entrega,
                            tsg014.nb_tp_lampara,
                            tsg018.nb_ilum_nat,
                            tsg002.nb_comentarios

                        };

            if (municipio > 0)
            { query = query.Where(t => t.cd_municipio == municipio); }

            if (colonia > 0)
            { query = query.Where(t => t.cd_colonia == colonia); }

            
            //var query = from c in customers join o in orders on c.ID equals o.ID select new { c.Name, o.Product };


            WebGrid grdGrid = new WebGrid(source: query.ToList(), canPage: false, canSort: false);

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
                    grdGrid.Column("nb_dir_nave", "Direccion"),
                    grdGrid.Column("nu_superficie", "Superficie Nave"),
                    grdGrid.Column("nu_bodega", "Superficie Terreno"),
                    grdGrid.Column("nu_disponibilidad", "Disponibilidad Total"),
                    grdGrid.Column("nu_min_divisible", "Mínimo divisible"),
                    grdGrid.Column("nb_area", "Área de Oficina"),
                    grdGrid.Column("im_renta", "Precio de Renta"),
                    grdGrid.Column("im_venta", "Precio de Venta"),
                    grdGrid.Column("nu_tipo_cambio", "Tipo de Cambio"),
                    grdGrid.Column("nu_altura", "Altura libre"),
                    grdGrid.Column("nu_puertas", "Puertas Andén"),
                    grdGrid.Column("nu_rampas", "Rampa"),
                    grdGrid.Column("nb_carga", "Capacidad de carga piso"),
                    grdGrid.Column("nb_sist_inc", "Sistema contra incendios"),
                    grdGrid.Column("nb_tp_construccion", "Tipo construcción"),
                    grdGrid.Column("nb_st_entrega", "Estatus de entrega"),
                    grdGrid.Column("nb_tp_lampara", "Lámparas"),
                    grdGrid.Column("nb_ilum_nat", "Iluminación natural"),
                    grdGrid.Column("nb_comentarios", "Comentarios")
                )
             ).ToString();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Naves.xls");
            Response.ContentType = "application/ms-excel";           
            Response.Write(gridData);           
            Response.End();            
        }
        public void GetExcel(int ?estado, int ?municipio, int ?colonia)
        {
            //var tsg002_nave_industrial = db.tsg002_nave_industrial.Include(t => t.tsg023_ni_precio)
            //    .Include(t => t.tsg009_ni_dt_gral)
            //    .Include(t => t.tsg020_ni_servicio)
            //    .Include(t => t.tsg025_ni_contacto)
            //    .Include(t => t.tsg007_mercado)
            //    .Include(t => t.tsg008_corredor_ind).ToList();

            var query = from tsg002 in db.tsg002_nave_industrial
                        join tsg009 in db.tsg009_ni_dt_gral
                        on tsg002.cd_nave equals tsg009.cd_nave
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
                        join tsg010 in db.tsg010_area_of
                        on tsg009.cd_area equals tsg010.cd_area
                        join tsg023 in db.tsg023_ni_precio
                        on tsg002.cd_nave equals tsg023.cd_nave
                        join tsg011 in db.tsg011_carga_piso
                        on tsg009.cd_carga equals tsg011.cd_carga
                        join tsg012 in db.tsg012_sist_incendio
                        on tsg009.cd_sist_inc equals tsg012.cd_sist_inc
                        join tsg013 in db.tsg013_tp_construccion
                        on tsg009.cd_tp_construccion equals tsg013.cd_tp_construccion
                        join tsg017 in db.tsg017_st_entrega
                        on tsg002.st_parque_ind equals tsg017.cd_st_entrega
                        join tsg014 in db.tsg014_tp_lampara
                        on tsg009.cd_tp_lampara equals tsg014.cd_tp_lampara
                        join tsg018 in db.tsg018_ilum_nat
                        on tsg009.cd_ilum_nat equals tsg018.cd_ilum_nat
                        where (tsg002.cd_estado == estado && tsg009.nu_disponibilidad > 0)
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
                            tsg002.cd_colonia,

                            tsg009.nu_superficie,
                            tsg009.nu_bodega,
                            tsg009.nu_disponibilidad,
                            tsg009.nu_min_divisible,
                            tsg010.nb_area,
                            tsg023.im_renta,
                            tsg023.im_venta,
                            tsg023.nu_tipo_cambio,
                            tsg009.nu_altura,
                            tsg009.nu_puertas,
                            tsg009.nu_rampas,
                            tsg011.nb_carga,
                            tsg012.nb_sist_inc,
                            tsg013.nb_tp_construccion,
                            tsg017.nb_st_entrega,
                            tsg018.nb_ilum_nat,
                            tsg002.nb_comentarios

                        };

            if (municipio>0)
            { query = query.Where(t => t.cd_municipio == municipio); }

            if (colonia > 0)
            { query = query.Where(t => t.cd_colonia == colonia); }
            //var query = from c in customers join o in orders on c.ID equals o.ID select new { c.Name, o.Product };


            WebGrid grdGrid = new WebGrid(source: query.ToList(), canPage:false,canSort:false);

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

            //return RedirectToAction("Index");
        }
    }
}