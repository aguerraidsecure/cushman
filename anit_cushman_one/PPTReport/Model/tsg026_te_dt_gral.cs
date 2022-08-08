namespace PPTReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tsg026_te_dt_gral
    {
        [Key]
        public int cd_id { get; set; }

        public int? cd_terreno { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? nu_disponiblidad { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? nu_tam_min { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? nu_tam_max { get; set; }

        public string nb_radio_cob { get; set; }

        public int? cd_st_entrega { get; set; }

        public DateTime? fh_modif { get; set; }

        public virtual tsg001_terreno tsg001_terreno { get; set; }

        public virtual tsg017_st_entrega tsg017_st_entrega { get; set; }
    }
}
