namespace PPTReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tsg035_municipios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tsg035_municipios()
        {
            tsg036_colonias = new HashSet<tsg036_colonias>();
            tsg039_colonias = new HashSet<tsg039_colonias>();
        }

        [Key]
        public int cd_municipios { get; set; }

        public int cd_estado { get; set; }

        public string nb_municipio { get; set; }

        public DateTime? fh_modif { get; set; }

        public virtual tsg034_estados tsg034_estados { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tsg036_colonias> tsg036_colonias { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tsg039_colonias> tsg039_colonias { get; set; }
    }
}
