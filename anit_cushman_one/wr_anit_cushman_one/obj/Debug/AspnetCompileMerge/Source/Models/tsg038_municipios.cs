namespace wr_anit_cushman_one.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    public class tsg038_municipios
    {
        [Key]
        public int cd_municipio { get; set; }
        public int cd_estado { get; set; }
        public string nb_municipio { get; set; }
        public DateTime fh_modif { get; set; }

        public virtual tsg037_estados tsg037_estados { get; set; }

        public virtual ICollection<tsg036_colonias> tsg036_colonias { get; set; }
    }
}