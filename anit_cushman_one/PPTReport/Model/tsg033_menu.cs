namespace PPTReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tsg033_menu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tsg033_menu()
        {
            tsg032_perfilpantalla = new HashSet<tsg032_perfilpantalla>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int cd_menu { get; set; }

        public int? cd_posicion { get; set; }

        public string cd_cvemenu_padre { get; set; }

        public string cd_cvemenu { get; set; }

        public string nb_menu { get; set; }

        public string tx_url { get; set; }

        public int? st_opcion_menu { get; set; }

        public DateTime? fh_modif { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tsg032_perfilpantalla> tsg032_perfilpantalla { get; set; }
    }
}
