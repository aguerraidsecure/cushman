namespace wr_anit_cushman_one.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tsg023_ni_precio
    {
        [Key]
        public int cd_ni_precio { get; set; }

        public int? cd_nave { get; set; }

        public int? cd_terreno { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? im_renta { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? im_venta { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? im_total { get; set; }

        public int? cd_cond_arr { get; set; }

        public string nb_cond_arr { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? nu_ma_ac { get; set; }

        public DateTime? fh_modif { get; set; }

        
        public int cd_moneda { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? nu_tipo_cambio { get; set; }

        public int cd_rep_moneda { get; set; }
    
        public virtual tsg001_terreno tsg001_terreno { get; set; }

        public virtual tsg002_nave_industrial tsg002_nave_industrial { get; set; }

        public virtual tsg024_cond_arr tsg024_cond_arr { get; set; }
    }
}
