namespace PPTReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tsg006_colonia
    {
        [Key]
        public int cd_id { get; set; }

        public int? cd_estado { get; set; }

        public int? cd_municipio { get; set; }

        public int? cd_colonia { get; set; }

        [StringLength(6)]
        public string nu_cp { get; set; }

        public DateTime? fh_modif { get; set; }
    }
}
