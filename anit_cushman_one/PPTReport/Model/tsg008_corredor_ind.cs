namespace PPTReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tsg008_corredor_ind
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tsg008_corredor_ind()
        {
            tsg001_terreno = new HashSet<tsg001_terreno>();
            tsg002_nave_industrial = new HashSet<tsg002_nave_industrial>();
        }

        [Key]
        public int cd_corredor { get; set; }

        public string nb_corredor { get; set; }

        public DateTime? fh_modif { get; set; }

        public int cd_mercado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tsg001_terreno> tsg001_terreno { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tsg002_nave_industrial> tsg002_nave_industrial { get; set; }

        public virtual tsg007_mercado tsg007_mercado { get; set; }
    }
}
