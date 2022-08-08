namespace PPTReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tsg004_ciudad
    {
        [Key]
        public int cd_id { get; set; }

        public int? cd_estado { get; set; }

        public int? cd_ciudad { get; set; }

        public string nb_ciudad { get; set; }

        public DateTime? fh_modif { get; set; }
    }
}
