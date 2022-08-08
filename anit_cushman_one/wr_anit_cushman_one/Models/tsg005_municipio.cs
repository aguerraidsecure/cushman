namespace wr_anit_cushman_one.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tsg005_municipio
    {
        [Key]
        public int cd_id { get; set; }

        public int? cd_estado { get; set; }

        public int? cd_municipio { get; set; }

        public string nb_municipio { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? fh_modif { get; set; }
    }
}
