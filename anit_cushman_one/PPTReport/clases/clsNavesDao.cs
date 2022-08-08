using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPTReport.Model;

namespace PPTReport.clases
{
    public class clsNavesDao
    {
        cushman db = new cushman();
        public tsg002_nave_industrial obtenDatosNaves(int? id)
        {
            var naves =  db.tsg002_nave_industrial.Where(x => x.cd_nave == id).Single();
            return naves;
        }

        public tsg001_terreno obtenDatosTerrenos(int? id)
        {
            var terrenos = db.tsg001_terreno.Where(x => x.cd_terreno == id).Single();
            return terrenos;
        }
        public tsg038_municipios obteneMunicipio(int? cd_estado, int? cd_municipio)
        {
            var municipios = db.tsg038_municipios.Where(x => x.cd_estado == cd_estado && x.cd_municipio == cd_municipio).Single();
            return municipios;

        }

        public tsg008_corredor_ind obtenCorredor(int? cd_mercado, int? cd_corredor)
        {
            var corredor = db.tsg008_corredor_ind.Where(x => x.cd_mercado == cd_mercado && x.cd_corredor == cd_corredor).Single();
            return corredor;
        }
        public tsg039_colonias obtenColonia(int? cd_estado, int? cd_municipio, int? cd_colonia)
        {
            var colonia =db.tsg039_colonias.Where(x => x.cd_estado == cd_estado && x.cd_municipio == cd_municipio && x.cd_colonia == cd_colonia).Single();
            return colonia;
        }

        public tsg009_ni_dt_gral obtenDatosGralNaves(int? id)
        {
            var gralnaves = db.tsg009_ni_dt_gral.Where(x => x.cd_nave == id).Single();
            return gralnaves;
        }

        public tsg026_te_dt_gral obtenDatosGralTerrenos(int? id)
        {
            var gralterrenos = db.tsg026_te_dt_gral.Where(x => x.cd_terreno == id).Single();
            return gralterrenos;
        }

        public tsg042_Nivel_Piso obtenNivelPiso(int? id_nivel)
        {
            var nivelpiso = db.tsg042_Nivel_Piso.Where(x => x.cd_nivel_piso == id_nivel).Single();
            return nivelpiso;
        }

        public tsg011_carga_piso obtenCarga(int? id_carga)
        {
            var carga = db.tsg011_carga_piso.Where(x => x.cd_carga == id_carga).Single();
            return carga;
        }

        public tsg019_cajon_est obtenCajonesEst(int? id_cajon)
        {
            var cajon = db.tsg019_cajon_est.Where(x => x.cd_cajon_est == id_cajon).Single();
            return cajon;
        }

        public tsg041_tp_tech obtenTipoTechumbre(int? id_tpTech)
        {
            var tp_tech = db.tsg041_tp_tech.Where(x => x.cd_tp_tech == id_tpTech).Single();
            return tp_tech;
        }

        public tsg020_ni_servicio obtenServiciosNaves(int? id_nave)
        {
            var servnaves = db.tsg020_ni_servicio.Where(x => x.cd_nave == id_nave).Single();
            return servnaves;

        }

        public tsg027_te_servicio obtenServiciosTerrenos(int? id_terrenos)
        {
            var servterrenos = db.tsg027_te_servicio.Where(x => x.cd_terreno == id_terrenos).Single();
            return servterrenos;

        }

        public tsg043_telefonia obtenTelefonia(int? id_telefonia)
        {
            var telefonia = db.tsg043_telefonia.Where(x => x.cd_telefonia == id_telefonia).Single();
            return telefonia;
        }

        public tsg021_tp_gas_natural obtenTipoGas(int? id_gas)
        {
            var tipo_gas = db.tsg021_tp_gas_natural.Where(x => x.cd_tp_gas_natural == id_gas).Single();
            return tipo_gas;
        }

        public tsg022_esp_ferr obtenEspFerr(int? id_esp)
        {
            var esp_fer = db.tsg022_esp_ferr.Where(x => x.cd_esp_ferr == id_esp).Single();
            return esp_fer;
        }

        public tsg017_st_entrega obtenEstatusEntrega(int? id_estatus)
        {
            var estatus_entrega = db.tsg017_st_entrega.Where(x => x.cd_st_entrega == id_estatus).Single();
            return estatus_entrega;
        }

        public tsg015_hvac obtenVentilacion(int? id_vent)
        {
            var ventilacion = db.tsg015_hvac.Where(x => x.cd_hvac == id_vent).Single();
            return ventilacion;
        }
        public tsg014_tp_lampara obtenTipoLampara(int? id_lampa)
        {
            var tipo_lampara = db.tsg014_tp_lampara.Where(x => x.cd_tp_lampara == id_lampa).Single();
            return tipo_lampara;
        }
        public tsg012_sist_incendio obtenSistIncendios(int? id_sist_inc)
        {
            var sistemaincendio = db.tsg012_sist_incendio.Where(x => x.cd_sist_inc == id_sist_inc).Single();
            return sistemaincendio;
        }
        public tsg013_tp_construccion obtenTipoConstruccion(int? id_tipo_const)
        {
            var tipo_construccion = db.tsg013_tp_construccion.Where(x => x.cd_tp_construccion == id_tipo_const).Single();
            return tipo_construccion;
        }

        public tsg023_ni_precio obtenPrecios(int? id_nava)
        {
            var precios = db.tsg023_ni_precio.Where(x => x.cd_nave == id_nava).Single();
            return precios;
        }
        public tsg023_ni_precio obtenPreciosTerr(int? id_terreno)
        {
            var precios = db.tsg023_ni_precio.Where(x => x.cd_terreno == id_terreno).Single();
            return precios;
        }
        public tsg044_tipos_monedas obtenMoneda(int? id_moneda)
        {
            if (id_moneda != 0)
            {
                var tipo_moneda = db.tsg044_tipos_monedas.Where(x => x.cd_moneda == id_moneda).Single();
                return tipo_moneda;
            }
            else
            {
                tsg044_tipos_monedas tipos_monedas = new tsg044_tipos_monedas();
                tipos_monedas.cd_moneda = 0;
                tipos_monedas.nb_moneda = "";
                return tipos_monedas;
            }

           
        }
          
    }
}
