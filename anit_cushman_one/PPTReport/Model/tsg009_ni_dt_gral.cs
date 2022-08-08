namespace PPTReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tsg009_ni_dt_gral
    {
        [Key]
        public int cd_id { get; set; }

        public int? cd_nave { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? nu_superficie { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? nu_bodega { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? nu_disponibilidad { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? nu_min_divisible { get; set; }

        public int? cd_area { get; set; }

        public string nb_area { get; set; }

        public int? nu_anio_cons { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? nu_altura { get; set; }

        public int? nu_puertas { get; set; }

        public int? nu_rampas { get; set; }

        public string nb_radio_cob { get; set; }

        public int? cd_carga { get; set; }

        public string nb_carga { get; set; }

        public int? cd_sist_inc { get; set; }

        public string nb_sist_inc { get; set; }

        public int? cd_tp_construccion { get; set; }

        public string nb_tp_construccion { get; set; }

        public int? cd_tp_lampara { get; set; }

        public string nb_tp_lampara { get; set; }

        public int? cd_hvac { get; set; }

        public string nb_hvac { get; set; }

        public int? cd_espesor { get; set; }

        public string nb_espesor { get; set; }

        public int? cd_st_entrega { get; set; }

        public DateTime? fh_entrega { get; set; }

        public int? cd_ilum_nat { get; set; }

        public string n_ilum_nat { get; set; }

        public int? cd_cajon_est { get; set; }

        public string nb_cajon_est { get; set; }

        public DateTime? fh_modif { get; set; }

        public int? cd_tp_tech { get; set; }

        public int? cd_Nivel_Piso { get; set; }

        public DateTime? fh_desde { get; set; }

        public DateTime? fh_hasta { get; set; }

        public int? nu_caj_trailer { get; set; }

        public virtual tsg002_nave_industrial tsg002_nave_industrial { get; set; }

        public virtual tsg041_tp_tech tsg041_tp_tech { get; set; }

        public virtual tsg042_Nivel_Piso tsg042_Nivel_Piso { get; set; }

        public virtual tsg010_area_of tsg010_area_of { get; set; }

        public virtual tsg011_carga_piso tsg011_carga_piso { get; set; }

        public virtual tsg012_sist_incendio tsg012_sist_incendio { get; set; }

        public virtual tsg013_tp_construccion tsg013_tp_construccion { get; set; }

        public virtual tsg014_tp_lampara tsg014_tp_lampara { get; set; }

        public virtual tsg015_hvac tsg015_hvac { get; set; }

        public virtual tsg016_espesor tsg016_espesor { get; set; }

        public virtual tsg017_st_entrega tsg017_st_entrega { get; set; }

        public virtual tsg018_ilum_nat tsg018_ilum_nat { get; set; }

        public virtual tsg019_cajon_est tsg019_cajon_est { get; set; }
    }
}
