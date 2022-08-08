using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using wr_anit_cushman_one.Models;

namespace wr_anit_cushman_one.Controllers
{
    public class UtilsController : Controller
    {
        // GET: Utils
        private CushmanContext db = new CushmanContext();
        //[System.Web.Services.WebMethod]
        [HttpPost]
        public JsonResult GetCdEstado()
        {
            return Json(new SelectList(db.tsg037_estados.OrderBy(x => x.nb_estado).ToList(),"cd_estado", "nb_estado"));
        }
        [HttpPost]
        public JsonResult GetMercados()
        {
            return Json(new SelectList(db.tsg007_mercado.ToList(), "cd_mercado", "nb_mercado"));
        }
        [HttpPost]
        public JsonResult GetCorredor()
        {
            return Json(new SelectList(db.tsg008_corredor_ind.ToList(), "cd_corredor", "nb_corredor"));
        }
        [HttpPost]        
        public JsonResult GetEstatus()
        {
            return Json(new SelectList(db.tsg017_st_entrega.ToList(), "cd_st_entrega", "nb_st_entrega"));
        }
        [HttpPost]
        public JsonResult GetEspuelas()
        {
            return Json(new SelectList(db.tsg022_esp_ferr.ToList(), "cd_esp_ferr", "nb_esp_ferr"));
        }
        [HttpPost]
        public JsonResult GetAreas()
        {
            return Json(new SelectList(db.tsg010_area_of.ToList(), "cd_area", "nb_area"));
        }
        [HttpPost]
        public JsonResult GetCargas()
        {
            return Json(new SelectList(db.tsg011_carga_piso.ToList(), "cd_carga", "nb_carga"));
        }
        [HttpPost]
        public JsonResult GetSistInc()
        {
            return Json(new SelectList(db.tsg012_sist_incendio.ToList(), "cd_sist_inc", "nb_sist_inc"));
        }
        [HttpPost]
        public JsonResult GetConstruccion()
        {
            return Json(new SelectList(db.tsg013_tp_construccion.ToList(), "cd_tp_construccion", "nb_tp_construccion"));
        }
        [HttpPost]
        public JsonResult GetLamparas()
        {
            return Json(new SelectList(db.tsg014_tp_lampara.ToList(), "cd_tp_lampara", "nb_tp_lampara"));
        }

        [HttpPost]
        public JsonResult GetHVAC()
        {
            return Json(new SelectList(db.tsg015_hvac.ToList(), "cd_hvac", "nb_hvac"));
        }

        [HttpPost]
        public JsonResult GetEspesor()
        {
            return Json(new SelectList(db.tsg016_espesor.ToList(), "cd_espesor", "nb_espesor"));
        }
        [HttpPost]
        public JsonResult GetIluminacion()
        {
            return Json(new SelectList(db.tsg018_ilum_nat.ToList(), "cd_ilum_nat", "nb_ilum_nat"));
        }
        [HttpPost]
        public JsonResult GetCajones()
        {
            return Json(new SelectList(db.tsg019_cajon_est.ToList(), "cd_cajon_est", "nb_cajon_est"));
        }
        [HttpPost]
        public JsonResult GetGas()
        {
            return Json(new SelectList(db.tsg021_tp_gas_natural.ToList(), "cd_tp_gas_natural", "nb_tp_gas_natural"));
        }
        [HttpPost]
        public JsonResult GetspFerr()
        {
            return Json(new SelectList(db.tsg022_esp_ferr.ToList(), "cd_esp_ferr", "nb_esp_ferr"));
        }
        [HttpPost]
        public JsonResult GetCondArrend()
        {
            return Json(new SelectList(db.tsg024_cond_arr.ToList(), "cd_cond_arr", "nb_cond_arr"));
        }

        [HttpPost]
        public JsonResult GettpTech()
        {
            return Json(new SelectList(db.tsg041_tp_tech.ToList(), "cd_tp_tech", "nb_tp_tech"));
        }
        [HttpPost]
        public JsonResult GetNivelPiso()
        {
            return Json(new SelectList(db.tsg042_Nivel_Piso.ToList(), "cd_Nivel_Piso", "nb_Nivel_Piso"));
        }

        [HttpPost]
        public JsonResult GetTelefonia()
        {
            return Json(new SelectList(db.tsg043_telefonia.ToList(), "cd_telefonia", "nb_telefonia"));
        }

        [HttpPost]
        public JsonResult GetTipoMoneda()
        {
            return Json(new SelectList(db.tsg044_tipos_monedas.ToList(), "cd_moneda", "nb_moneda"));
        }
        [HttpPost]
        public JsonResult GetTipoReporte()
        {
            return Json(new SelectList(db.tsg046_tipos_reportes.ToList(), "cd_reporte", "nb_reporte"));
        }

        [HttpPost]
        public JsonResult GetCorredorByCdMercado(int idAbuelo)
        {
            return Json(new SelectList(db.tsg008_corredor_ind.Where(x => x.cd_mercado == idAbuelo).ToList(), "cd_corredor", "nb_corredor"));
        }


        [HttpPost]
        
        public JsonResult GetMunicipioByCdEstado(int idAbuelo)
        {
            return Json(new SelectList(db.tsg038_municipios.Where(x => x.cd_estado == idAbuelo).OrderBy(x => x.nb_municipio).ToList(), "cd_municipio", "nb_municipio"));
        }
        [HttpPost]
        public JsonResult GetColoniaByMunicipio(int idAbuelo, int idPadre)
        {

            var tsg039_colonias = db.tsg039_colonias.Where(x => x.cd_estado == idAbuelo);
            tsg039_colonias = tsg039_colonias.Where(x => x.cd_municipio == idPadre);

            return Json(new SelectList(tsg039_colonias.OrderBy(x => x.nb_colonia).ToList(), "cd_colonia", "nb_colonia"));
        }
        /*[HttpPost]
        public JsonResult GetTpProdBySubFamilia(int idAbuelo, int idPadre, int idHijo)
        {

            var tsi019_tipo_producto = db.tsi019_tipo_producto.Where(x => x.cd_gpo_familia == idAbuelo);
            tsi019_tipo_producto = tsi019_tipo_producto.Where(x => x.cd_familia == idPadre);
            tsi019_tipo_producto = tsi019_tipo_producto.Where(x => x.cd_sub_familia == idHijo);

            return Json(new SelectList(tsi019_tipo_producto.ToList(), "cd_tipo_prod", "nb_tipo_producto"));
        }*/
    }

}