namespace wr_anit_cushman_one.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public partial class tsg042_Nivel_Piso
    {
        [Key]
        public int cd_nivel_piso { get; set; }

        public string nb_nivel_piso { get; set; }

        public DateTime? fh_modif { get; set; }


    }
}