namespace PPTReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Permiso")]
    public partial class Permiso
    {
        public int PermisoID { get; set; }

        [StringLength(20)]
        public string Modulo { get; set; }

        [StringLength(100)]
        public string Descripcion { get; set; }
    }
}
