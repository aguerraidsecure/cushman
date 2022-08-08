namespace wr_anit_cushman_one.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    public class tsg036_colonias
    {
        [Key]
        public int cd_colonias { get; set; }
        public int cd_municipios { get; set; }
        public int cd_estados { get; set; }
        public string nb_colonia { get; set; }
        public string nu_cp { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? fh_modif { get; set; }
        public virtual tsg035_municipios tsg035_municipios {get; set;}
        public virtual tsg034_estados tsg034_estados { get; set; }


    }
}