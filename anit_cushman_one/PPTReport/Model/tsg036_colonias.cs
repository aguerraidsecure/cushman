namespace PPTReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tsg036_colonias
    {
        [Key]
        public int cd_colonias { get; set; }

        public int cd_municipios { get; set; }

        public int cd_estados { get; set; }

        public string nb_colonia { get; set; }

        public string nu_cp { get; set; }

        public DateTime? fh_modif { get; set; }

        public int? tsg034_estados_cd_estado { get; set; }

        public int? tsg038_municipios_cd_municipio { get; set; }

        public virtual tsg034_estados tsg034_estados { get; set; }

        public virtual tsg035_municipios tsg035_municipios { get; set; }

        public virtual tsg038_municipios tsg038_municipios { get; set; }
    }
}
