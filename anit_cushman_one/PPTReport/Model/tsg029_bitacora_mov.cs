namespace PPTReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tsg029_bitacora_mov
    {
        [Key]
        public int cd_bitacora { get; set; }

        public int? cd_usuario { get; set; }

        public string nb_tabla { get; set; }

        public string nb_id_modif { get; set; }

        public DateTime? fh_modif_reg { get; set; }

        public DateTime? fh_modif { get; set; }
    }
}
