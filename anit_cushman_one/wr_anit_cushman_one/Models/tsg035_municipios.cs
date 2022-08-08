namespace wr_anit_cushman_one.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    public class tsg035_municipios
    {
        [Key]
        public int cd_municipios { get; set; }
        public int cd_estado { get; set; }
        public string nb_municipio { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? fh_modif { get; set; }

        public virtual tsg034_estados tsg034_estados { get; set; }

        public virtual ICollection<tsg039_colonias> tsg039_colonias { get; set; }
    }
}