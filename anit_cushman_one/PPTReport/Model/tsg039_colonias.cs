namespace PPTReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tsg039_colonias
    {
        [Key]
        public int cd_colonia { get; set; }

        public int cd_municipio { get; set; }

        public int cd_estado { get; set; }

        public string nb_colonia { get; set; }

        public string nu_cp { get; set; }

        public DateTime? fh_modif { get; set; }

        public int? tsg035_municipios_cd_municipios { get; set; }

        public virtual tsg035_municipios tsg035_municipios { get; set; }

        public virtual tsg038_municipios tsg038_municipios { get; set; }
    }
}
