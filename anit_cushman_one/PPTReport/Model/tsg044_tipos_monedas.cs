namespace PPTReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tsg044_tipos_monedas
    {
        [Key]
        public int cd_moneda { get; set; }

        public string nb_moneda { get; set; }

        public DateTime? fh_modif { get; set; }
    }
}
