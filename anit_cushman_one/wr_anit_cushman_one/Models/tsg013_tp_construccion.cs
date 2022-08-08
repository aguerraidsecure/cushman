namespace wr_anit_cushman_one.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tsg013_tp_construccion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tsg013_tp_construccion()
        {
            tsg009_ni_dt_gral = new HashSet<tsg009_ni_dt_gral>();
        }

        [Key]
        public int cd_tp_construccion { get; set; }

        public String nb_tp_construccion { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? fh_modif { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tsg009_ni_dt_gral> tsg009_ni_dt_gral { get; set; }
    }
}
