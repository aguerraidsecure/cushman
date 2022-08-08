using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wr_anit_cushman_one.Models;

namespace wr_anit_cushman_one.Models
{
    public class clsNaves
    {
        public tsg002_nave_industrial tsg002_nave_industrial { get; set; }
        public tsg009_ni_dt_gral tsg009_ni_dt_gral { get; set; }
        public tsg020_ni_servicio tsg020_ni_servicio { get; set; }
        public tsg023_ni_precio tsg023_ni_precio { get; set; }
        public tsg025_ni_contacto tsg025_ni_contacto_P { get; set; }
        public tsg025_ni_contacto tsg025_ni_contacto_A { get; set; }
        public tsg025_ni_contacto tsg025_ni_contacto_C { get; set; }
        public tsg045_imagenes_naves tsg045_imagenes_naves { get; set; }


    }
}