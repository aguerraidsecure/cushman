using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPTReport.Model;
using System.Globalization;

namespace PPTReport.dao
{
    public class daoNaves
    {
        cushman db = new cushman();

        public List<tsg002_nave_industrial> listNaves(int cd_estado, int cd_municipio, int cd_colonia)
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

        public int existeNave(int cd_nava)
        {
            int result = 0;

            result = db.tsg009_ni_dt_gral.Where(t => t.cd_nave == cd_nava && t.nu_disponibilidad > 0).Count();
            return result;
        }
    
        public tsg037_estados obtenestadoid(int cd_estado)
        {

            tsg037_estados estados = db.tsg037_estados.First(t => t.cd_estado == cd_estado);
            return estados;
        }

        public tsg045_imagenes_naves obteneImagenesNaves(int cd_nave)
        {
            var imagenesNaves = db.tsg045_imagenes_naves.Where(t => t.cd_nave == cd_nave && t.cd_reporte == 1);

            tsg045_imagenes_naves imgNaves = null;

            foreach (var a in imagenesNaves)
            {
                imgNaves = a;
            }

            return imgNaves;
        }
        public String obtenDetalle(int cd_nave)
        {
            String result = "";

            result = obtenDatosGeneralNave(cd_nave);
            result = result + ObtenDatosPrecios(cd_nave);
            result = result + ObtenDatosGeneralNave2(cd_nave);


            return result;
        }

        private String ObtenDatosGeneralNave2(int cd_nave)
        {
            String result = "";
            var datosNaves = db.tsg009_ni_dt_gral.Where(t => t.cd_nave == cd_nave);
            foreach (var datos_gral in datosNaves)
            {
                result = "- Altura libre / Clear Height:" + String.Format("{0:#,##0.00}", datos_gral.nu_altura) + " M / " + String.Format("{0:#,##0.00}", Convert.ToDouble(datos_gral.nu_altura) * 3.28084) + " Ft\n";
                result = result + "- Puertas Andén / Dock Doors:" + datos_gral.nu_puertas + "\n";
                result = result + "- Rampa / Drive in Door:" + datos_gral.nu_rampas + "\n";
                var cat_piso = datos_gral.cd_carga != null ? db.tsg011_carga_piso.Where(t => t.cd_carga == datos_gral.cd_carga).First() : null;
                if (cat_piso != null)
                {
                    result = result + "- Capacidad de carga piso / Floor Load Capacity:" + cat_piso.nb_carga + "\n";
                }
                else
                {
                    result = result + "- Capacidad de carga piso / Floor Load Capacity:" + "\n";
                }

                var cat_sist_inc = datos_gral.cd_sist_inc != null ? db.tsg012_sist_incendio.Where(x => x.cd_sist_inc == datos_gral.cd_sist_inc).First() : null;
                if (cat_sist_inc != null)
                {
                    result = result + "- Sistema contra incendios / Fire Protection System:" + cat_sist_inc.nb_sist_inc + "\n";
                }
                else
                {
                    result = result + "- Sistema contra incendios / Fire Protection System:" + "\n";
                }

                var cat_constr = datos_gral.cd_tp_construccion != null ? db.tsg013_tp_construccion.Where(x => x.cd_tp_construccion == datos_gral.cd_tp_construccion).First() : null;
                if (cat_constr != null)
                {
                    result = result + "- Tipo construcción / Construction Type:" + cat_constr.nb_tp_construccion + "\n";
                }
                else
                {
                    result = result + " - Tipo construcción / Construction Type:" + "\n";
                }

                var naves = db.tsg002_nave_industrial.Where(t => t.cd_nave == cd_nave).First();
                var cat_entrega = naves.st_parque_ind != null ? db.tsg017_st_entrega.Where(x => x.cd_st_entrega == naves.st_parque_ind).First() : null;
                if (cat_entrega != null)
                {
                    result = result + "- Estatus de entrega / Delivery Status:" + cat_entrega.nb_st_entrega + "\n";
                }
                else
                {
                    result = result + " - Estatus de entrega / Delivery Status:" + "\n";
                }

                var cat_lamparas = datos_gral.cd_tp_lampara != null ? db.tsg014_tp_lampara.Where(x => x.cd_tp_lampara == datos_gral.cd_tp_lampara).First() : null;
                if (cat_lamparas != null)
                {
                    result = result + "- Lámparas / Lighting Fixtures:" + cat_lamparas.nb_tp_lampara + "\n";
                }
                else
                {
                    result = result + " - Lámparas / Lighting Fixtures:" + "\n";
                }

                var cat_iluminacion = datos_gral.cd_ilum_nat != null ? db.tsg018_ilum_nat.Where(x => x.cd_ilum_nat == datos_gral.cd_ilum_nat).First() : null;
                if (cat_iluminacion != null)
                {
                    result = result + " - Iluminación natural / Skylights:" + cat_iluminacion.nb_ilum_nat + "\n";
                }
                else
                {
                    result = result + " - Iluminación natural / Skylights:" + "\n";
                }
            }
            return result;
        }
        private String ObtenDatosPrecios(int cd_nave)
        {
            String result = "";

            var datosPrecio = db.tsg023_ni_precio.Where(t => t.cd_nave == cd_nave);
            foreach (var datos_precios in datosPrecio)
            {
                if (datos_precios.im_renta > 0)
                {
                    double im_renta = Convert.ToDouble(datos_precios.im_renta);
                    result = "- Precio de Renta / Rent Price: " + im_renta.ToString("C2", CultureInfo.CreateSpecificCulture("en-us")) + "M2/mes - Ft2/month\n";
                }

                if (datos_precios.im_venta > 0)
                {
                    double im_venta = Convert.ToDouble(datos_precios.im_venta);
                    result = result + "- Precio de Venta / Sale Price: " + im_venta.ToString("C2", CultureInfo.CreateSpecificCulture("en-us")) + "M2/mes - Ft2/month\n";

                }
                double nu_ma_ac;
                if (datos_precios.cd_cond_arr != null)
                {
                    var cat_codiciones = db.tsg024_cond_arr.Where(x => x.cd_cond_arr == datos_precios.cd_cond_arr).First();
                    nu_ma_ac = Convert.ToDouble(datos_precios.nu_ma_ac);
                    result = result + " - Cuota de Mantenimiento " + cat_codiciones.nb_cond_arr + " : " + nu_ma_ac.ToString("C2", CultureInfo.CreateSpecificCulture("en-us")) + "\n";
                }
                else
                {
                    nu_ma_ac = Convert.ToDouble(datos_precios.nu_ma_ac);
                    result = result + " - " + " : " + nu_ma_ac.ToString("C2", CultureInfo.CreateSpecificCulture("en-us")) + "\n";
                }

                double nu_tipo_cambio = Convert.ToDouble(datos_precios.nu_tipo_cambio);

                result = result + " - Tipo de Cambio / Exchange Rate:" + nu_tipo_cambio.ToString("C2", CultureInfo.CreateSpecificCulture("en-us")) + "\n";

            }

            return result;
        }
        private String obtenDatosGeneralNave(int cd_nave)
        {
            String result = "";


            var datosNaves = db.tsg009_ni_dt_gral.Where(t => t.cd_nave == cd_nave);
            foreach (var datos_gral in datosNaves)
            {
                result = "- Superficie Nave / Building Area:" + String.Format("{0:#,##0.00}", datos_gral.nu_superficie) + "M2 / " + String.Format("{0:#,##0.00}", Convert.ToDouble(datos_gral.nu_superficie) * 10.7639) + " Ft2\n";
                result = result + "- Superficie Terreno / Land Size:" + String.Format("{0:#,##0.00}", datos_gral.nu_bodega) + " M2 / " + String.Format("{0:#,##0.00}", Convert.ToDouble(datos_gral.nu_bodega) * 10.7639) + " Ft2\n";
                result = result + " - Disponibilidad Total / Total Available:" + String.Format("{0:#,##0.00}", datos_gral.nu_disponibilidad) + " M2 / " + String.Format("{0:#,##0.00}", Convert.ToDouble(datos_gral.nu_disponibilidad) * 10.7639) + " Ft2\n";
                result = result + " - Mínimo divisible / Minimum Divisible:" + String.Format("{0:#,##0.00}", datos_gral.nu_min_divisible) + " M2 / " + String.Format("{0:#,##0.00}", Convert.ToDouble(datos_gral.nu_min_divisible) * 10.7639) + " Ft2\n";
                if (datos_gral.cd_area != null)
                {
                    result = result + obtenCatalogoArea(datos_gral.cd_area);
                }
                else
                {
                    result = result + " - Área de Oficina / Office Area:" + "\n";
                }
            }
            return result;
        }

        private String obtenCatalogoArea(int? cd_area)
        {
            String result = "";

            var cat_area = db.tsg010_area_of.Where(t => t.cd_area == cd_area).First();
            if (cat_area != null)
            {
                result = " - Área de Oficina / Office Area:" + cat_area.nb_area + "\n";

            }
            else
            {
                result = " - Área de Oficina / Office Area:" + "\n";
            }
            return result;
        }
    }
}
