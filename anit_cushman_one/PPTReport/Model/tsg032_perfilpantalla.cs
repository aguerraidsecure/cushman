namespace PPTReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tsg032_perfilpantalla
    {
        [Key]
        public int cd_perfilpantalla { get; set; }

        public int? cd_perfil { get; set; }

        public int? cd_menu { get; set; }

        public int? bl_padre { get; set; }

        [StringLength(1)]
        public string op_alta { get; set; }

        [StringLength(1)]
        public string op_eliminar { get; set; }

        [StringLength(1)]
        public string op_editar { get; set; }

        [StringLength(1)]
        public string op_consulta { get; set; }

        public virtual tsg031_perfil tsg031_perfil { get; set; }

        public virtual tsg033_menu tsg033_menu { get; set; }
    }
}
