namespace wr_anit_cushman_one.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public partial class tsg046_tipos_reportes
    {
        [Key]
        public int cd_reporte { get; set; }

        public string nb_reporte { get; set; }

        public DateTime? fh_modif { get; set; }

    }
}