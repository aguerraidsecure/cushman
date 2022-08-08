namespace wr_anit_cushman_one.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tsg001_terreno
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tsg001_terreno()
        {
            tsg023_ni_precio = new HashSet<tsg023_ni_precio>();
            tsg026_te_dt_gral = new HashSet<tsg026_te_dt_gral>();
            tsg027_te_servicio = new HashSet<tsg027_te_servicio>();
            tsg028_te_contacto = new HashSet<tsg028_te_contacto>();
        }

        [Key]
        public int cd_terreno { get; set; }

        [Required]
        public string nb_comercial { get; set; }

        public string nb_nombre { get; set; }
        public string nb_apellido { get; set; }

        [Required]
        public string nb_calle { get; set; }

        [Required]
        public string nu_direcion { get; set; }

        [Required]
        public string nu_cp { get; set; }

        public int? cd_estado { get; set; }

        public int? cd_ciudad { get; set; }

        public int? cd_municipio { get; set; }

        public int? cd_colonia { get; set; }

        public int? cd_mercado { get; set; }

        public int? cd_corredor { get; set; }

        public int? st_parque_ind { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? nu_tamano { get; set; }

        public DateTime fh_modif { get; set; }

        public string nb_posicion  { get; set; }

        public string nb_poligono { get; set; }

        public string nb_dir_terr { get; set; }

        public string nb_comentarios { get; set; }
        public virtual tsg007_mercado tsg007_mercado { get; set; }

        public virtual tsg008_corredor_ind tsg008_corredor_ind { get; set; }

        public virtual tsg017_st_entrega tsg017_st_entrega { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tsg023_ni_precio> tsg023_ni_precio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tsg026_te_dt_gral> tsg026_te_dt_gral { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tsg027_te_servicio> tsg027_te_servicio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tsg028_te_contacto> tsg028_te_contacto { get; set; }
    }
}
