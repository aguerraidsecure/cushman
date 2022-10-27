
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace wr_anit_cushman_one.Models
{
    
    public class tsg048_grupos_mercados
    {
        [Key]
        public int cd_grupo { get; set; }
        
        public int? cd_mercado { get; set; }
        
        public int? cd_estado { get; set; }
        
        public int? cd_municipio { get; set; }
        
        public DateTime? fh_modif { get; set; }
        public virtual tsg007_mercado tsg007_mercado { get; set; }
        public virtual tsg038_municipios tsg038_municipios { get; set; }
    }
}