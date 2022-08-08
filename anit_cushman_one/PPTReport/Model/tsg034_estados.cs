namespace PPTReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tsg034_estados
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tsg034_estados()
        {
            tsg035_municipios = new HashSet<tsg035_municipios>();
            tsg036_colonias = new HashSet<tsg036_colonias>();
            tsg038_municipios = new HashSet<tsg038_municipios>();
        }

        [Key]
        public int cd_estado { get; set; }

        [Required]
        public string nb_estado { get; set; }

        public DateTime fh_modif { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tsg035_municipios> tsg035_municipios { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tsg036_colonias> tsg036_colonias { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tsg038_municipios> tsg038_municipios { get; set; }
    }
}
