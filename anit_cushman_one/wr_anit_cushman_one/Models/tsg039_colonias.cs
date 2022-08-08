namespace wr_anit_cushman_one.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class tsg039_colonias
    {
        [Key]
        public int cd_colonia { get; set; }
        public int cd_municipio { get; set; }
        public int cd_estado { get; set; }
        public string nb_colonia { get; set; }
        public string nu_cp { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? fh_modif { get; set; }
        public virtual tsg038_municipios tsg038_municipios { get; set; }
        
    }
}