using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;


namespace wr_anit_cushman_one.Models
{
    [NotMapped]
    public class Terreno
    {
        public tsg001_terreno tsg001_terreno { get; set; }
        public tsg023_ni_precio tsg023_ni_precio { get; set; }
        public tsg026_te_dt_gral tsg026_te_dt_gral { get; set; }
        public tsg027_te_servicio tsg027_te_servicio { get; set; }
        public tsg028_te_contacto tsg028_te_contacto_Prop { get; set; }
        public tsg028_te_contacto tsg028_te_contacto_Adm { get; set; }
        public tsg028_te_contacto tsg028_te_contacto_Corr { get; set; }
        public tsg017_st_entrega tsg017_st_entrega { get; set; }
        public tsg022_esp_ferr tsg022_esp_ferr { get; set; }
        public tsg040_imagenes_terrenos tsg040_imagenes_terrenos { get; set; }



    }
}