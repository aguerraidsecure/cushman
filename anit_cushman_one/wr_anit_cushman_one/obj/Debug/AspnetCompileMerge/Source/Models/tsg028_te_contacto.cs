namespace wr_anit_cushman_one.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tsg028_te_contacto
    {
        [Key]
        public int cd_contacto { get; set; }

        public int? cd_terreno { get; set; }

        public string nb_propietario { get; set; }

        public string nb_apellido { get; set; }

        public string nb_empresa { get; set; }

        public string nu_telefono { get; set; }

        public string nb_email { get; set; }

        public string nb_calle { get; set; }

        [StringLength(10)]
        public string nu_interior { get; set; }

        public int? cd_estado { get; set; }

        public int? cd_ciudad { get; set; }

        public int? cd_municipio { get; set; }

        public int? cd_colonia { get; set; }

        [StringLength(6)]
        public string nu_cp { get; set; }

        public int? st_contacto { get; set; }

        public DateTime? fh_modif { get; set; }

        public virtual tsg001_terreno tsg001_terreno { get; set; }
    }
}
