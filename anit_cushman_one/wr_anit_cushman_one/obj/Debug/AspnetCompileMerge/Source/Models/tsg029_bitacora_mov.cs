using Helper;
using Model;

namespace wr_anit_cushman_one.Models
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

        public void GuardaBitacora(string tabla, string cd_campo , int id, string movimiento)
        {
            try
            {
                using (var context = new CushmanContext())
                {
                    var usuario = new Usuario().Obtener(SessionHelper.GetUser());
                    int idUsuario = usuario.id;
                    tsg029_bitacora_mov bitacora_mov = new tsg029_bitacora_mov();
                    bitacora_mov.cd_usuario = idUsuario;
                    bitacora_mov.nb_tabla = "tabla: " + tabla + " id: " + id + " mov: " + movimiento;
                    bitacora_mov.nb_id_modif = cd_campo;
                    bitacora_mov.fh_modif = DateTime.Now;
                    bitacora_mov.fh_modif_reg = DateTime.Now;
                    context.tsg029_bitacora_mov.Add(bitacora_mov);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                { }
                throw;
            }
            
        }
    }
}
