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
            //var query = from tsg002 in db.tsg002_nave_industrial
            //            join tsg009 in db.tsg009_ni_dt_gral on tsg002.cd_nave equals tsg009.cd_nave
            //            join tsg008 in db.tsg008_corredor_ind on tsg002.cd_corredor equals tsg008.cd_corredor into tmpT8
            //            join tsg007 in db.tsg007_mercado on tsg002.cd_mercado equals tsg007.cd_mercado into tmpT7
            //            join tsg037 in db.tsg037_estados on tsg002.cd_estado equals tsg037.cd_estado into tmpT37
            //            join tsg038 in db.tsg038_municipios on tsg002.cd_municipio equals tsg038.cd_municipio into tmpT38
            //            join tsg039 in db.tsg039_colonias on tsg002.cd_colonia equals tsg039.cd_colonia into tmpT39
            //            join tsg010 in db.tsg010_area_of on tsg009.cd_area equals tsg010.cd_area into tmpT10
            //            join tsg023 in db.tsg023_ni_precio on tsg002.cd_nave equals tsg023.cd_nave into tmpT23
            //            join tsg011 in db.tsg011_carga_piso on tsg009.cd_carga equals tsg011.cd_carga into tmpT11
            //            join tsg012 in db.tsg012_sist_incendio on tsg009.cd_sist_inc equals tsg012.cd_sist_inc into tmpT12
            //            join tsg013 in db.tsg013_tp_construccion on tsg009.cd_tp_construccion equals tsg013.cd_tp_construccion into tmpT13
            //            join tsg017 in db.tsg017_st_entrega on tsg002.st_parque_ind equals tsg017.cd_st_entrega into tmpT17
            //            join tsg014 in db.tsg014_tp_lampara on tsg009.cd_tp_lampara equals tsg014.cd_tp_lampara into tmpT14
            //            join tsg018 in db.tsg018_ilum_nat on tsg009.cd_ilum_nat equals tsg018.cd_ilum_nat into tmpT18
            //            from tsg008 in tmpT8.DefaultIfEmpty()
            //            from tsg007 in tmpT7.DefaultIfEmpty()
            //            from tsg037 in tmpT37.DefaultIfEmpty()
            //            from tsg038 in tmpT38.DefaultIfEmpty()
            //            from tsg039 in tmpT39.DefaultIfEmpty()
            //            from tsg010 in tmpT10.DefaultIfEmpty()
            //            from tsg023 in tmpT23.DefaultIfEmpty()
            //            from tsg011 in tmpT11.DefaultIfEmpty()
            //            from tsg012 in tmpT12.DefaultIfEmpty()
            //            from tsg013 in tmpT13.DefaultIfEmpty()
            //            from tsg017 in tmpT17.DefaultIfEmpty()
            //            from tsg014 in tmpT14.DefaultIfEmpty()
            //            from tsg018 in tmpT18.DefaultIfEmpty()
            //            where (tsg002.cd_estado == estado && tsg009.nu_disponibilidad > 0)

            //            select new
            //            {
            //                nb_mercado = tsg007 != null ? tsg007.nb_mercado : "N/A",
            //                nb_corredor = tsg008 != null ? tsg008.nb_corredor : "N/A",
            //                nb_parque = tsg002 != null ? tsg002.nb_parque : "N/A",
            //                nb_nave = tsg002 != null ? tsg002.nb_nave : "N/A",
            //                nb_calle = tsg002 != null ? tsg002.nb_calle : "N/A",
            //                nu_direcion = tsg002 != null ? tsg002.nu_direcion : "N/A",
            //                nu_cp = tsg002 != null ? tsg002.nu_cp : "N/A",
            //                nb_estado = tsg037 != null ? tsg037.nb_estado : "N/A",
            //                nb_municipio = tsg038 != null ? tsg038.nb_municipio : "N/A",
            //                nb_colonia = tsg039 != null ? tsg039.nb_colonia : "N/A",
            //                nb_dir_nave = tsg002 != null ? tsg002.nb_dir_nave : "N/A",
            //                cd_estado = tsg002 != null ? tsg002.cd_estado : 0,
            //                cd_municipio = tsg002 != null ? tsg002.cd_municipio : 0,
            //                cd_colonia = tsg002 != null ? tsg002.cd_colonia : 0,
            //                nu_superficie = tsg009 != null ? tsg009.nu_superficie : 0,
            //                nu_bodega = tsg009 != null ? tsg009.nu_bodega : 0,
            //                nu_disponibilidad = tsg009 != null ? tsg009.nu_disponibilidad : 0,
            //                nu_min_divisible = tsg009 != null ? tsg009.nu_min_divisible : 0,
            //                nb_area = tsg010 != null ? tsg010.nb_area : "N/A",

            //                im_renta = tsg023 != null ? tsg023.im_renta : 0,
            //                im_venta = tsg023 != null ? tsg023.im_venta : 0,
            //                nu_tipo_cambio = tsg023 != null ? tsg023.nu_tipo_cambio : 0,

            //                nu_altura = tsg009 != null ? tsg009.nu_altura : 0,
            //                nu_puertas = tsg009 != null ? tsg009.nu_puertas : 0,
            //                nu_rampas = tsg009 != null ? tsg009.nu_rampas : 0,
            //                nb_carga = tsg011 != null ? tsg011.nb_carga : "N/A",
            //                nb_sist_inc = tsg012 != null ? tsg012.nb_sist_inc : "N/A",
            //                nb_tp_construccion = tsg013 != null ? tsg013.nb_tp_construccion : "N/A",
            //                nb_st_entrega = tsg017 != null ? tsg017.nb_st_entrega : "N/A",
            //                nb_tp_lampara = tsg014 != null ? tsg014.nb_tp_lampara : "N/A",
            //                nb_ilum_nat = tsg018 != null ? tsg018.nb_ilum_nat : "N/A",
            //                nb_comentarios = tsg002 != null ? tsg002.nb_comentarios : "N/A"

            //            };
            //var query = from n in db.tsg002_nave_industrial
            //            join g in db.tsg009_ni_dt_gral on n.cd_nave equals g.cd_nave
            //            join p in db.tsg023_ni_precio on n.cd_nave equals p.cd_nave into tmpP
            //            join a in db.tsg010_area_of on g.cd_area equals a.cd_area into tmpA
            //            join s in db.tsg012_sist_incendio on g.cd_sist_inc equals s.cd_sist_inc into tmpS
            //            from p in tmpP.DefaultIfEmpty()
            //            from a in tmpA.DefaultIfEmpty()
            //            from s in tmpS.DefaultIfEmpty()
            //            where n.cd_nave != 1 && g.nu_disponibilidad > 0 && n.cd_estado == estado
            //            select new
            //            {    //nb_mercado = tsg007 != null ? tsg007.nb_mercado : "N/A",
            //                cd_nave     = n != null ? n.cd_nave : 0,
            //                nb_parque   = n != null ? n.nb_parque : " ",
            //                nb_nave     = n != null ? n.nb_nave : " ",
            //                nu_disponibilidad   = g != null ? g.nu_disponibilidad : 0,
            //                nu_min_divisible    = g != null ? g.nu_min_divisible : 0,
            //                im_renta            = p != null ? p.im_renta : 0,
            //                nb_area             = a != null ? a.nb_area : " ",
            //                nu_altura           = g != null ? g.nu_altura : 0,
            //                //nb_tp_lampara       = g != null ? g.nb_tp_lampara : " ",
            //                cd_ilum_nat = g != null ? g.nb_tp_lampara : " ",
            //                nb_sist_inc         = s != null ? s.nb_sist_inc : " ",
            //                cd_estado           = n != null ? n.cd_estado : 0,
            //                cd_municipio        = n != null ? n.cd_municipio : 0,
            //                cd_colonia          = n != null ? n.cd_colonia : 0,
            //                timecomen = ""
            //            };
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
                            cd_nave = n != null ? n.cd_nave : 0,
                            nb_parque = n != null ? n.nb_parque : " ",
                            nb_nave = n != null ? n.nb_nave : " ",

                            nb_ilum_nat = w != null ? (g.cd_ilum_nat == 8 ? g.n_ilum_nat : w.nb_ilum_nat) : " ",
                            nb_area = g != null ? (g.cd_area == 8 ? g.nb_area:a.nb_area ):" ",
                            nb_sist_inc = s != null ? (g.cd_ilum_nat == 5 ? g.nb_sist_inc : s.nb_sist_inc) : " ",


                            nu_disponibilidad = g != null ? g.nu_disponibilidad : 0,
                            nu_min_divisible = g != null ? g.nu_min_divisible : 0,
                            im_renta = p != null ? p.im_renta : 0,                            
                            nu_altura = g != null ? g.nu_altura : 0,
                            
                            cd_estado = n != null ? n.cd_estado : 0,
                            cd_municipio = n != null ? n.cd_municipio : 0,
                            cd_colonia = n != null ? n.cd_colonia : 0,
                            cd_ilum_nat = g != null ? g.cd_ilum_nat : 0,
                            cd_area = a != null ? a.cd_area : 0,
                            cd_sist_inc = s != null ? s.cd_sist_inc : 0,
                            timecomen = ""
                        };
            if (municipio > 0)
            { query = query.Where(t => t.cd_municipio == municipio); }

            if (colonia > 0)
            { query = query.Where(t => t.cd_colonia == colonia); }

            
            //var query = from c in customers join o in orders on c.ID equals o.ID select new { c.Name, o.Product };


            WebGrid grdGrid = new WebGrid(source: query.ToList(), canPage: false, canSort: false);

            //string gridData = grdGrid.GetHtml(
            //    columns: grdGrid.Columns(
            //        grdGrid.Column("nb_mercado", "Mercado"),
            //        grdGrid.Column("nb_corredor", "Corredor"),
            //        grdGrid.Column("nb_parque", "Parque"),
            //        grdGrid.Column("nb_nave", "Nombre Nave"),
            //        grdGrid.Column("nb_calle", "Calle"),
            //        grdGrid.Column("nu_direcion", "No."),
            //        grdGrid.Column("nu_cp", "CP"),
            //        grdGrid.Column("nb_estado", "Estado"),
            //        grdGrid.Column("nb_municipio", "Municipio"),
            //        grdGrid.Column("nb_colonia", "Colonia"),
            //        grdGrid.Column("nb_dir_nave", "Direccion"),
            //        grdGrid.Column("nu_superficie", "Superficie Nave"),
            //        grdGrid.Column("nu_bodega", "Superficie Terreno"),
            //        grdGrid.Column("nu_disponibilidad", "Disponibilidad Total"),
            //        grdGrid.Column("nu_min_divisible", "Mínimo divisible"),
            //        grdGrid.Column("nb_area", "Área de Oficina"),
            //        grdGrid.Column("im_renta", "Precio de Renta"),
            //        grdGrid.Column("im_venta", "Precio de Venta"),
            //        grdGrid.Column("nu_tipo_cambio", "Tipo de Cambio"),
            //        grdGrid.Column("nu_altura", "Altura libre"),
            //        grdGrid.Column("nu_puertas", "Puertas Andén"),
            //        grdGrid.Column("nu_rampas", "Rampa"),
            //        grdGrid.Column("nb_carga", "Capacidad de carga piso"),
            //        grdGrid.Column("nb_sist_inc", "Sistema contra incendios"),
            //        grdGrid.Column("nb_tp_construccion", "Tipo construcción"),
            //        grdGrid.Column("nb_st_entrega", "Estatus de entrega"),
            //        grdGrid.Column("nb_tp_lampara", "Lámparas"),
            //        grdGrid.Column("nb_ilum_nat", "Iluminación natural"),
            //        grdGrid.Column("nb_comentarios", "Comentarios")
            //    )
            // ).ToString();
            string gridData = grdGrid.GetHtml(
               columns: grdGrid.Columns(
                   grdGrid.Column("cd_nave", ""),
                   grdGrid.Column("nb_parque", "Parque"),
                   grdGrid.Column("nb_nave", "Nombre Nave"),
                   grdGrid.Column("nu_disponibilidad", "Disponibilidad Total(sqMt)(SqFt)"),
                   grdGrid.Column("nu_min_divisible", "Minimo divisible m2"),
                   grdGrid.Column("im_renta", "Precio de Renta y Mantenimien to Areas Comunes(USD)($/ SqFt / year)"),                   
                   grdGrid.Column("nb_area", "Area Oficina (% del AreaTotal)"),
                   grdGrid.Column("nu_altura", "Altura Libre(Mt)(Ft))"),
                   grdGrid.Column("nb_ilum_nat", "Iluminación Natural(% ofroof)"),
                   grdGrid.Column("nb_sist_inc", "Sistemas contra incendio"),
                   grdGrid.Column("timecomen",  "Tiempos / Comentarios")
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
            //var query = from tsg002 in db.tsg002_nave_industrial
            //            join tsg009 in db.tsg009_ni_dt_gral on tsg002.cd_nave equals tsg009.cd_nave
            //            join tsg008 in db.tsg008_corredor_ind on tsg002.cd_corredor equals tsg008.cd_corredor into tmpT8
            //            join tsg007 in db.tsg007_mercado on tsg002.cd_mercado equals tsg007.cd_mercado into tmpT7
            //            join tsg037 in db.tsg037_estados on tsg002.cd_estado equals tsg037.cd_estado into tmpT37
            //            join tsg038 in db.tsg038_municipios on tsg002.cd_municipio equals tsg038.cd_municipio into tmpT38
            //            join tsg039 in db.tsg039_colonias on tsg002.cd_colonia equals tsg039.cd_colonia into tmpT39
            //            join tsg010 in db.tsg010_area_of on tsg009.cd_area equals tsg010.cd_area into tmpT10
            //            join tsg023 in db.tsg023_ni_precio on tsg002.cd_nave equals tsg023.cd_nave into tmpT23
            //            join tsg011 in db.tsg011_carga_piso on tsg009.cd_carga equals tsg011.cd_carga into tmpT11
            //            join tsg012 in db.tsg012_sist_incendio on tsg009.cd_sist_inc equals tsg012.cd_sist_inc into tmpT12
            //            join tsg013 in db.tsg013_tp_construccion on tsg009.cd_tp_construccion equals tsg013.cd_tp_construccion into tmpT13
            //            join tsg017 in db.tsg017_st_entrega on tsg002.st_parque_ind equals tsg017.cd_st_entrega into tmpT17
            //            join tsg014 in db.tsg014_tp_lampara on tsg009.cd_tp_lampara equals tsg014.cd_tp_lampara into tmpT14
            //            join tsg018 in db.tsg018_ilum_nat on tsg009.cd_ilum_nat equals tsg018.cd_ilum_nat into tmpT18
            //            from tsg008 in tmpT8.DefaultIfEmpty()
            //            from tsg007 in tmpT7.DefaultIfEmpty()
            //            from tsg037 in tmpT37.DefaultIfEmpty()
            //            from tsg038 in tmpT38.DefaultIfEmpty()
            //            from tsg039 in tmpT39.DefaultIfEmpty()
            //            from tsg010 in tmpT10.DefaultIfEmpty()
            //            from tsg023 in tmpT23.DefaultIfEmpty()
            //            from tsg011 in tmpT11.DefaultIfEmpty()
            //            from tsg012 in tmpT12.DefaultIfEmpty()
            //            from tsg013 in tmpT13.DefaultIfEmpty()
            //            from tsg017 in tmpT17.DefaultIfEmpty()
            //            from tsg014 in tmpT14.DefaultIfEmpty()
            //            from tsg018 in tmpT18.DefaultIfEmpty()
            //            where (tsg002.cd_estado == estado && tsg009.nu_disponibilidad > 0)

            //            select new
            //            {
            //                nb_mercado      =   tsg007 != null ? tsg007.nb_mercado : "N/A",
            //                nb_corredor     =   tsg008 != null ? tsg008.nb_corredor: "N/A",
            //                nb_parque       =   tsg002 != null ? tsg002.nb_parque : "N/A" ,
            //                nb_nave = tsg002 != null ? tsg002.nb_nave : "N/A",
            //                nb_calle = tsg002 != null ? tsg002.nb_calle : "N/A",
            //                nu_direcion = tsg002 != null ? tsg002.nu_direcion : "N/A",
            //                nu_cp = tsg002 != null ? tsg002.nu_cp : "N/A",
            //                nb_estado = tsg037 != null ? tsg037.nb_estado : "N/A",
            //                nb_municipio = tsg038 != null ? tsg038.nb_municipio : "N/A",
            //                nb_colonia = tsg039 != null ? tsg039.nb_colonia : "N/A",
            //                nb_dir_nave = tsg002 != null ? tsg002.nb_dir_nave : "N/A",
            //                cd_estado = tsg002 != null ? tsg002.cd_estado : 0,
            //                cd_municipio    =   tsg002 != null ? tsg002.cd_municipio:0,
            //                cd_colonia      =   tsg002 != null ? tsg002.cd_colonia:0,
            //                nu_superficie = tsg009 != null ? tsg009.nu_superficie : 0,
            //                nu_bodega = tsg009 != null ? tsg009.nu_bodega : 0,
            //                nu_disponibilidad = tsg009 != null ? tsg009.nu_disponibilidad : 0,
            //                nu_min_divisible = tsg009 != null ? tsg009.nu_min_divisible : 0,
            //                nb_area = tsg010 != null ? tsg010.nb_area : "N/A",

            //                im_renta = tsg023 != null ? tsg023.im_renta : 0,
            //                im_venta = tsg023 != null ? tsg023.im_venta : 0,
            //                nu_tipo_cambio = tsg023 != null ? tsg023.nu_tipo_cambio : 0,

            //                nu_altura = tsg009 != null ? tsg009.nu_altura : 0,
            //                nu_puertas = tsg009 != null ? tsg009.nu_puertas : 0,
            //                nu_rampas = tsg009 != null ? tsg009.nu_rampas : 0,
            //                nb_carga = tsg011 != null ? tsg011.nb_carga : "N/A",
            //                nb_sist_inc = tsg012 != null ? tsg012.nb_sist_inc : "N/A",
            //                nb_tp_construccion = tsg013 != null ? tsg013.nb_tp_construccion : "N/A",
            //                nb_st_entrega = tsg017 != null ? tsg017.nb_st_entrega : "N/A",
            //                nb_ilum_nat = tsg018 != null ? tsg018.nb_ilum_nat : "N/A",
            //                nb_comentarios = tsg002 != null ? tsg002.nb_comentarios : "N/A"

            //            };
            //var query = from n in db.tsg002_nave_industrial
            //            join g in db.tsg009_ni_dt_gral on n.cd_nave equals g.cd_nave
            //            join p in db.tsg023_ni_precio on n.cd_nave equals p.cd_nave into tmpP
            //            join a in db.tsg010_area_of on g.cd_area equals a.cd_area into tmpA
            //            join s in db.tsg012_sist_incendio on g.cd_sist_inc equals s.cd_sist_inc into tmpS
            //            from p in tmpP.DefaultIfEmpty()
            //            from a in tmpA.DefaultIfEmpty()
            //            from s in tmpS.DefaultIfEmpty()
            //            where n.cd_nave != 1 && g.nu_disponibilidad > 0 && n.cd_estado == estado
            //            select new
            //            {    //nb_mercado = tsg007 != null ? tsg007.nb_mercado : "N/A",
            //                cd_nave = n != null ? n.cd_nave : 0,
            //                nb_parque = n != null ? n.nb_parque : " ",
            //                nb_nave = n != null ? n.nb_nave : " ",
            //                nu_disponibilidad = g != null ? g.nu_disponibilidad : 0,
            //                nu_min_divisible = g != null ? g.nu_min_divisible : 0,
            //                im_renta = p != null ? p.im_renta : 0,
            //                nb_area = a != null ? a.nb_area : " ",
            //                nu_altura = g != null ? g.nu_altura : 0,
            //                nb_tp_lampara = g != null ? g.nb_tp_lampara : " ",
            //                nb_sist_inc = s != null ? s.nb_sist_inc : " ",
            //                cd_estado = n != null ? n.cd_estado : 0,
            //                cd_municipio = n != null ? n.cd_municipio : 0,
            //                cd_colonia = n != null ? n.cd_colonia : 0
            //            };
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
                            cd_nave = n != null ? n.cd_nave : 0,
                            nb_parque = n != null ? n.nb_parque : " ",
                            nb_nave = n != null ? n.nb_nave : " ",

                            nb_ilum_nat = w != null ? (g.cd_ilum_nat == 8 ? g.n_ilum_nat : w.nb_ilum_nat) : " ",
                            nb_area = g != null ? (g.cd_area == 8 ? g.nb_area : a.nb_area) : " ",
                            nb_sist_inc = s != null ? (g.cd_ilum_nat == 5 ? g.nb_sist_inc : s.nb_sist_inc) : " ",


                            nu_disponibilidad = g != null ? g.nu_disponibilidad : 0,
                            nu_min_divisible = g != null ? g.nu_min_divisible : 0,
                            im_renta = p != null ? p.im_renta : 0,
                            nu_altura = g != null ? g.nu_altura : 0,

                            cd_estado = n != null ? n.cd_estado : 0,
                            cd_municipio = n != null ? n.cd_municipio : 0,
                            cd_colonia = n != null ? n.cd_colonia : 0,
                            cd_ilum_nat = g != null ? g.cd_ilum_nat : 0,
                            cd_area = a != null ? a.cd_area : 0,
                            cd_sist_inc = s != null ? s.cd_sist_inc : 0,
                        };
            if (municipio>0)
            { query = query.Where(t => t.cd_municipio == municipio).DefaultIfEmpty(); }

            if (colonia > 0)
            { query = query.Where(t => t.cd_colonia == colonia).DefaultIfEmpty(); }
            //var query = from c in customers join o in orders on c.ID equals o.ID select new { c.Name, o.Product };

            query.ToList();
            //WebGrid grdGrid = new WebGrid(source: query.ToList(), canPage:false,canSort:false);

            //string gridData = grdGrid.GetHtml(                
            //    columns: grdGrid.Columns(
            //        grdGrid.Column("nb_mercado", "Mercado", null,null,true),
            //        grdGrid.Column("nb_corredor", "Corredor", null, null, true),
            //        grdGrid.Column("nb_parque", "Parque", null, null, true),
            //        grdGrid.Column("nb_nave", "Nombre Nave", null, null, true),
            //        grdGrid.Column("nb_calle", "Calle", null, null, true),
            //        grdGrid.Column("nu_direcion", "No.", null, null, true),
            //        grdGrid.Column("nu_cp", "CP", null, null, true),
            //        grdGrid.Column("nb_estado", "Estado", null, null, true),
            //        grdGrid.Column("nb_municipio", "Municipio", null, null, true),
            //        grdGrid.Column("nb_colonia", "Colonia", null, null, true),
            //        grdGrid.Column("nb_dir_nave", "Direccion", null, null, true)
            //    )
            // ).ToString();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Naves.xls");
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "";

            /////Response.Write(gridData);
            Response.Flush();
            Response.End();

            //return RedirectToAction("Index");
        }
    }
}