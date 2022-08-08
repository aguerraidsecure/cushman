namespace PPTReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tsg022_esp_ferr
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tsg022_esp_ferr()
        {
            tsg020_ni_servicio = new HashSet<tsg020_ni_servicio>();
            tsg027_te_servicio = new HashSet<tsg027_te_servicio>();
        }

        [Key]
        public int cd_esp_ferr { get; set; }

        public string nb_esp_ferr { get; set; }

        public DateTime? fh_modif { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tsg020_ni_servicio> tsg020_ni_servicio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tsg027_te_servicio> tsg027_te_servicio { get; set; }
    }
}
