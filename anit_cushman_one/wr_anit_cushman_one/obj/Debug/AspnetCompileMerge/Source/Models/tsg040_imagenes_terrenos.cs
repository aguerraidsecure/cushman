namespace wr_anit_cushman_one.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public partial class tsg040_imagenes_terrenos
    {
        [Key]
        public int cd_id { get; set; }

        public int? cd_terreno { get; set; }
        public string nb_archivo { get; set; }
        public string tx_archivo { get; set; }
        public int? cd_reporte { get; set; }
        public DateTime? fh_modif { get; set; }
      
        //public virtual tsg001_terreno tsg001_terreno { get; set; }

    }
}