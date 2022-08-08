namespace wr_anit_cushman_one.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tsg043_telefonia
    {
        [Key]
        public int cd_telefonia { get; set; }
                
        public string nb_telefonia { get; set; }
        
        public DateTime? fh_modif { get; set; }
        
    }
}
