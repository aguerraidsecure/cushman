namespace PPTReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tsg007_mercado
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tsg007_mercado()
        {
            tsg001_terreno = new HashSet<tsg001_terreno>();
            tsg002_nave_industrial = new HashSet<tsg002_nave_industrial>();
            tsg008_corredor_ind = new HashSet<tsg008_corredor_ind>();
        }

        [Key]
        public int cd_mercado { get; set; }

        public string nb_mercado { get; set; }

        public DateTime? fh_modif { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tsg001_terreno> tsg001_terreno { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tsg002_nave_industrial> tsg002_nave_industrial { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tsg008_corredor_ind> tsg008_corredor_ind { get; set; }
    }
}
