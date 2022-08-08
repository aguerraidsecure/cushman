namespace PPTReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tsg030_usuarios
    {
        [Key]
        [Column("cd_usuario ")]
        public int cd_usuario_ { get; set; }

        public string nb_nombre { get; set; }

        public int? cd_jerarquia { get; set; }

        public int? st_usuario { get; set; }

        public string nb_usuario { get; set; }

        public string nb_password { get; set; }

        public int? cd_perfil { get; set; }

        public DateTime? fh_modif { get; set; }

        public virtual tsg031_perfil tsg031_perfil { get; set; }
    }
}
