namespace wr_anit_cushman_one.Models
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
        }

        [Key]
        public int cd_mercado { get; set; }

        public string nb_mercado { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? fh_modif { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tsg001_terreno> tsg001_terreno { get; set; }

        
        public virtual ICollection<tsg048_grupos_mercados> tsg048_grupos_mercados { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tsg002_nave_industrial> tsg002_nave_industrial { get; set; }
    }
}
