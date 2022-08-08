namespace PPTReport.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tsg027_te_servicio
    {
        [Key]
        public int cd_servicio { get; set; }

        public int? cd_terreno { get; set; }

        public int? nu_agua { get; set; }

        public string nb_ser_tel_com { get; set; }

        public int? cd_tp_gas_natural { get; set; }

        public string nb_tp_gas_natural { get; set; }

        public int? cd_esp_fer { get; set; }

        public DateTime? fh_modif { get; set; }

        public bool st_aguamunicipal { get; set; }

        public bool st_aguapozo { get; set; }

        public int? cd_telefonia { get; set; }

        public virtual tsg001_terreno tsg001_terreno { get; set; }

        public virtual tsg021_tp_gas_natural tsg021_tp_gas_natural { get; set; }

        public virtual tsg022_esp_ferr tsg022_esp_ferr { get; set; }

        public virtual tsg043_telefonia tsg043_telefonia { get; set; }
    }
}
