namespace wr_anit_cushman_one.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public class tsg047_brokers
    {
        [Key]
        public int cd_broker { get; set; }

        public string nb_broker { get; set; }
        public string nb_puesto { get; set; }
        public string nu_telefono { get; set; }
        public DateTime? fh_modif { get; set; }
    }
}