namespace PPTReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tsg045_imagenes_naves
    {
        [Key]
        public int cd_id { get; set; }

        public int? cd_nave { get; set; }

        public string nb_archivo { get; set; }

        public string tx_archivo { get; set; }

        public DateTime? fh_modif { get; set; }

        public int? cd_reporte { get; set; }
    }
}
