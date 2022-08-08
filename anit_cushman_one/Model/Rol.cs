using System.Data.Entity;
using System.Data.SqlClient;

namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Rol")]
    public partial class Rol
    {
        public Rol()
        {
            Usuario = new List<Usuario>();
            Permiso = new List<Permiso>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        public virtual ICollection<Usuario> Usuario { get; set; }

        public virtual ICollection<Permiso> Permiso { get; set; }
        public void Guardar()
        {
            try
            {
                using (var context = new CushmanContext())
                {
                    if (this.id == 0)
                    {
                        context.Entry(this).State = EntityState.Added;
                    }
                    else
                    {
                        context.Database.ExecuteSqlCommand(
                            "DELETE FROM PermisoDenegadoPorRol WHERE RolID = @id",
                            new SqlParameter("id", this.id)
                        );

                        var PermisoBK = this.Permiso;

                        this.Permiso = null;
                        context.Entry(this).State = EntityState.Modified;
                        this.Permiso = PermisoBK;
                    }

                    foreach (var c in this.Permiso)
                        context.Entry(c).State = EntityState.Unchanged;

                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
