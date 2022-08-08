namespace PPTReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tsg002_nave_industrial
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tsg002_nave_industrial()
        {
            tsg009_ni_dt_gral = new HashSet<tsg009_ni_dt_gral>();
            tsg020_ni_servicio = new HashSet<tsg020_ni_servicio>();
            tsg023_ni_precio = new HashSet<tsg023_ni_precio>();
            tsg025_ni_contacto = new HashSet<tsg025_ni_contacto>();
        }

        [Key]
        public int cd_nave { get; set; }

        [Required]
        public string nb_parque { get; set; }

        [Required]
        public string nb_nave { get; set; }

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

        public string nb_posicion { get; set; }

        public string nb_poligono { get; set; }

        public string nb_dir_nave { get; set; }

        public string nb_comentarios { get; set; }

        public virtual tsg007_mercado tsg007_mercado { get; set; }

        public virtual tsg008_corredor_ind tsg008_corredor_ind { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tsg009_ni_dt_gral> tsg009_ni_dt_gral { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tsg020_ni_servicio> tsg020_ni_servicio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tsg023_ni_precio> tsg023_ni_precio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tsg025_ni_contacto> tsg025_ni_contacto { get; set; }
    }
}
