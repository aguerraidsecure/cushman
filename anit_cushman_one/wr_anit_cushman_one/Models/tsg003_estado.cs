namespace wr_anit_cushman_one.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tsg003_estado
    {
        [Key]
        public int cd_estado { get; set; }

        [Required]
        public string nb_estado { get; set; }

        public DateTime fh_modif { get; set; }

        public string st_registro { get; set; }

    }
}
