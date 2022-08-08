namespace wr_anit_cushman_one.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tsg021_tp_gas_natural
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tsg021_tp_gas_natural()
        {
            tsg020_ni_servicio = new HashSet<tsg020_ni_servicio>();
            tsg027_te_servicio = new HashSet<tsg027_te_servicio>();
        }

        [Key]
        public int cd_tp_gas_natural { get; set; }

        public string nb_tp_gas_natural { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? fh_modif { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tsg020_ni_servicio> tsg020_ni_servicio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tsg027_te_servicio> tsg027_te_servicio { get; set; }
    }
}
