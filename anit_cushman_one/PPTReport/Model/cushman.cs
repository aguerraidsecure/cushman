namespace PPTReport.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class cushman : DbContext
    {
        public cushman()
            : base("name=cushman")
        {
        }

        public virtual DbSet<Permiso> Permiso { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<tsg001_terreno> tsg001_terreno { get; set; }
        public virtual DbSet<tsg002_nave_industrial> tsg002_nave_industrial { get; set; }
        public virtual DbSet<tsg003_estado> tsg003_estado { get; set; }
        public virtual DbSet<tsg004_ciudad> tsg004_ciudad { get; set; }
        public virtual DbSet<tsg005_municipio> tsg005_municipio { get; set; }
        public virtual DbSet<tsg006_colonia> tsg006_colonia { get; set; }
        public virtual DbSet<tsg007_mercado> tsg007_mercado { get; set; }
        public virtual DbSet<tsg008_corredor_ind> tsg008_corredor_ind { get; set; }
        public virtual DbSet<tsg009_ni_dt_gral> tsg009_ni_dt_gral { get; set; }
        public virtual DbSet<tsg010_area_of> tsg010_area_of { get; set; }
        public virtual DbSet<tsg011_carga_piso> tsg011_carga_piso { get; set; }
        public virtual DbSet<tsg012_sist_incendio> tsg012_sist_incendio { get; set; }
        public virtual DbSet<tsg013_tp_construccion> tsg013_tp_construccion { get; set; }
        public virtual DbSet<tsg014_tp_lampara> tsg014_tp_lampara { get; set; }
        public virtual DbSet<tsg015_hvac> tsg015_hvac { get; set; }
        public virtual DbSet<tsg016_espesor> tsg016_espesor { get; set; }
        public virtual DbSet<tsg017_st_entrega> tsg017_st_entrega { get; set; }
        public virtual DbSet<tsg018_ilum_nat> tsg018_ilum_nat { get; set; }
        public virtual DbSet<tsg019_cajon_est> tsg019_cajon_est { get; set; }
        public virtual DbSet<tsg020_ni_servicio> tsg020_ni_servicio { get; set; }
        public virtual DbSet<tsg021_tp_gas_natural> tsg021_tp_gas_natural { get; set; }
        public virtual DbSet<tsg022_esp_ferr> tsg022_esp_ferr { get; set; }
        public virtual DbSet<tsg023_ni_precio> tsg023_ni_precio { get; set; }
        public virtual DbSet<tsg024_cond_arr> tsg024_cond_arr { get; set; }
        public virtual DbSet<tsg025_ni_contacto> tsg025_ni_contacto { get; set; }
        public virtual DbSet<tsg026_te_dt_gral> tsg026_te_dt_gral { get; set; }
        public virtual DbSet<tsg027_te_servicio> tsg027_te_servicio { get; set; }
        public virtual DbSet<tsg028_te_contacto> tsg028_te_contacto { get; set; }
        public virtual DbSet<tsg029_bitacora_mov> tsg029_bitacora_mov { get; set; }
        public virtual DbSet<tsg030_usuarios> tsg030_usuarios { get; set; }
        public virtual DbSet<tsg031_perfil> tsg031_perfil { get; set; }
        public virtual DbSet<tsg032_perfilpantalla> tsg032_perfilpantalla { get; set; }
        public virtual DbSet<tsg033_menu> tsg033_menu { get; set; }
        public virtual DbSet<tsg034_estados> tsg034_estados { get; set; }
        public virtual DbSet<tsg035_municipios> tsg035_municipios { get; set; }
        public virtual DbSet<tsg036_colonias> tsg036_colonias { get; set; }
        public virtual DbSet<tsg037_estados> tsg037_estados { get; set; }
        public virtual DbSet<tsg038_municipios> tsg038_municipios { get; set; }
        public virtual DbSet<tsg039_colonias> tsg039_colonias { get; set; }
        public virtual DbSet<tsg040_imagenes_terrenos> tsg040_imagenes_terrenos { get; set; }
        public virtual DbSet<tsg041_tp_tech> tsg041_tp_tech { get; set; }
        public virtual DbSet<tsg042_Nivel_Piso> tsg042_Nivel_Piso { get; set; }
        public virtual DbSet<tsg043_telefonia> tsg043_telefonia { get; set; }
        public virtual DbSet<tsg044_tipos_monedas> tsg044_tipos_monedas { get; set; }
        public virtual DbSet<tsg045_imagenes_naves> tsg045_imagenes_naves { get; set; }
        public virtual DbSet<tsg046_tipos_reportes> tsg046_tipos_reportes { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permiso>()
                .Property(e => e.Modulo)
                .IsUnicode(false);

            modelBuilder.Entity<Permiso>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Rol>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Rol>()
                .HasMany(e => e.Usuario)
                .WithOptional(e => e.Rol)
                .HasForeignKey(e => e.Rol_id);

            modelBuilder.Entity<tsg001_terreno>()
                .Property(e => e.nb_comercial)
                .IsUnicode(false);

            modelBuilder.Entity<tsg001_terreno>()
                .Property(e => e.nb_calle)
                .IsUnicode(false);

            modelBuilder.Entity<tsg001_terreno>()
                .Property(e => e.nu_direcion)
                .IsUnicode(false);

            modelBuilder.Entity<tsg001_terreno>()
                .Property(e => e.nu_cp)
                .IsUnicode(false);

            modelBuilder.Entity<tsg002_nave_industrial>()
                .Property(e => e.nb_parque)
                .IsUnicode(false);

            modelBuilder.Entity<tsg002_nave_industrial>()
                .Property(e => e.nb_nave)
                .IsUnicode(false);

            modelBuilder.Entity<tsg002_nave_industrial>()
                .Property(e => e.nb_calle)
                .IsUnicode(false);

            modelBuilder.Entity<tsg002_nave_industrial>()
                .Property(e => e.nu_direcion)
                .IsUnicode(false);

            modelBuilder.Entity<tsg002_nave_industrial>()
                .Property(e => e.nu_cp)
                .IsUnicode(false);

            modelBuilder.Entity<tsg003_estado>()
                .Property(e => e.nb_estado)
                .IsUnicode(false);

            modelBuilder.Entity<tsg004_ciudad>()
                .Property(e => e.nb_ciudad)
                .IsUnicode(false);

            modelBuilder.Entity<tsg005_municipio>()
                .Property(e => e.nb_municipio)
                .IsUnicode(false);

            modelBuilder.Entity<tsg006_colonia>()
                .Property(e => e.nu_cp)
                .IsUnicode(false);

            modelBuilder.Entity<tsg007_mercado>()
                .Property(e => e.nb_mercado)
                .IsUnicode(false);

            modelBuilder.Entity<tsg008_corredor_ind>()
                .Property(e => e.nb_corredor)
                .IsUnicode(false);

            modelBuilder.Entity<tsg009_ni_dt_gral>()
                .Property(e => e.nb_area)
                .IsUnicode(false);

            modelBuilder.Entity<tsg009_ni_dt_gral>()
                .Property(e => e.nb_radio_cob)
                .IsUnicode(false);

            modelBuilder.Entity<tsg009_ni_dt_gral>()
                .Property(e => e.nb_carga)
                .IsUnicode(false);

            modelBuilder.Entity<tsg009_ni_dt_gral>()
                .Property(e => e.nb_sist_inc)
                .IsUnicode(false);

            modelBuilder.Entity<tsg009_ni_dt_gral>()
                .Property(e => e.nb_tp_construccion)
                .IsUnicode(false);

            modelBuilder.Entity<tsg009_ni_dt_gral>()
                .Property(e => e.nb_tp_lampara)
                .IsUnicode(false);

            modelBuilder.Entity<tsg009_ni_dt_gral>()
                .Property(e => e.nb_hvac)
                .IsUnicode(false);

            modelBuilder.Entity<tsg009_ni_dt_gral>()
                .Property(e => e.nb_espesor)
                .IsUnicode(false);

            modelBuilder.Entity<tsg009_ni_dt_gral>()
                .Property(e => e.n_ilum_nat)
                .IsUnicode(false);

            modelBuilder.Entity<tsg009_ni_dt_gral>()
                .Property(e => e.nb_cajon_est)
                .IsUnicode(false);

            modelBuilder.Entity<tsg010_area_of>()
                .Property(e => e.nb_area)
                .IsUnicode(false);

            modelBuilder.Entity<tsg011_carga_piso>()
                .Property(e => e.nb_carga)
                .IsUnicode(false);

            modelBuilder.Entity<tsg012_sist_incendio>()
                .Property(e => e.nb_sist_inc)
                .IsUnicode(false);

            modelBuilder.Entity<tsg014_tp_lampara>()
                .Property(e => e.nb_tp_lampara)
                .IsUnicode(false);

            modelBuilder.Entity<tsg015_hvac>()
                .Property(e => e.nb_hvac)
                .IsUnicode(false);

            modelBuilder.Entity<tsg016_espesor>()
                .Property(e => e.nb_espesor)
                .IsUnicode(false);

            modelBuilder.Entity<tsg017_st_entrega>()
                .HasMany(e => e.tsg001_terreno)
                .WithOptional(e => e.tsg017_st_entrega)
                .HasForeignKey(e => e.tsg017_st_entrega_cd_st_entrega);

            modelBuilder.Entity<tsg018_ilum_nat>()
                .Property(e => e.nb_ilum_nat)
                .IsUnicode(false);

            modelBuilder.Entity<tsg019_cajon_est>()
                .Property(e => e.nb_cajon_est)
                .IsUnicode(false);

            modelBuilder.Entity<tsg020_ni_servicio>()
                .Property(e => e.nb_ser_tel_com)
                .IsUnicode(false);

            modelBuilder.Entity<tsg020_ni_servicio>()
                .Property(e => e.nb_tp_gas_natural)
                .IsUnicode(false);

            modelBuilder.Entity<tsg021_tp_gas_natural>()
                .Property(e => e.nb_tp_gas_natural)
                .IsUnicode(false);

            modelBuilder.Entity<tsg022_esp_ferr>()
                .Property(e => e.nb_esp_ferr)
                .IsUnicode(false);

            modelBuilder.Entity<tsg022_esp_ferr>()
                .HasMany(e => e.tsg027_te_servicio)
                .WithOptional(e => e.tsg022_esp_ferr)
                .HasForeignKey(e => e.cd_esp_fer);

            modelBuilder.Entity<tsg023_ni_precio>()
                .Property(e => e.nb_cond_arr)
                .IsUnicode(false);

            modelBuilder.Entity<tsg024_cond_arr>()
                .Property(e => e.nb_cond_arr)
                .IsUnicode(false);

            modelBuilder.Entity<tsg025_ni_contacto>()
                .Property(e => e.nb_propietario)
                .IsUnicode(false);

            modelBuilder.Entity<tsg025_ni_contacto>()
                .Property(e => e.nu_telefono)
                .IsUnicode(false);

            modelBuilder.Entity<tsg025_ni_contacto>()
                .Property(e => e.nb_email)
                .IsUnicode(false);

            modelBuilder.Entity<tsg025_ni_contacto>()
                .Property(e => e.nu_interior)
                .IsUnicode(false);

            modelBuilder.Entity<tsg025_ni_contacto>()
                .Property(e => e.nu_cp)
                .IsUnicode(false);

            modelBuilder.Entity<tsg026_te_dt_gral>()
                .Property(e => e.nb_radio_cob)
                .IsUnicode(false);

            modelBuilder.Entity<tsg027_te_servicio>()
                .Property(e => e.nb_ser_tel_com)
                .IsUnicode(false);

            modelBuilder.Entity<tsg027_te_servicio>()
                .Property(e => e.nb_tp_gas_natural)
                .IsUnicode(false);

            modelBuilder.Entity<tsg028_te_contacto>()
                .Property(e => e.nb_propietario)
                .IsUnicode(false);

            modelBuilder.Entity<tsg028_te_contacto>()
                .Property(e => e.nu_telefono)
                .IsUnicode(false);

            modelBuilder.Entity<tsg028_te_contacto>()
                .Property(e => e.nb_email)
                .IsUnicode(false);

            modelBuilder.Entity<tsg028_te_contacto>()
                .Property(e => e.nu_interior)
                .IsUnicode(false);

            modelBuilder.Entity<tsg028_te_contacto>()
                .Property(e => e.nu_cp)
                .IsUnicode(false);

            modelBuilder.Entity<tsg029_bitacora_mov>()
                .Property(e => e.nb_id_modif)
                .IsUnicode(false);

            modelBuilder.Entity<tsg030_usuarios>()
                .Property(e => e.nb_nombre)
                .IsUnicode(false);

            modelBuilder.Entity<tsg030_usuarios>()
                .Property(e => e.nb_usuario)
                .IsUnicode(false);

            modelBuilder.Entity<tsg030_usuarios>()
                .Property(e => e.nb_password)
                .IsUnicode(false);

            modelBuilder.Entity<tsg031_perfil>()
                .Property(e => e.nb_perfil)
                .IsUnicode(false);

            modelBuilder.Entity<tsg032_perfilpantalla>()
                .Property(e => e.op_alta)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tsg032_perfilpantalla>()
                .Property(e => e.op_eliminar)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tsg032_perfilpantalla>()
                .Property(e => e.op_editar)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tsg032_perfilpantalla>()
                .Property(e => e.op_consulta)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tsg033_menu>()
                .Property(e => e.cd_cvemenu_padre)
                .IsUnicode(false);

            modelBuilder.Entity<tsg033_menu>()
                .Property(e => e.cd_cvemenu)
                .IsUnicode(false);

            modelBuilder.Entity<tsg033_menu>()
                .Property(e => e.nb_menu)
                .IsUnicode(false);

            modelBuilder.Entity<tsg033_menu>()
                .Property(e => e.tx_url)
                .IsUnicode(false);

            modelBuilder.Entity<tsg034_estados>()
                .HasMany(e => e.tsg036_colonias)
                .WithOptional(e => e.tsg034_estados)
                .HasForeignKey(e => e.tsg034_estados_cd_estado);

            modelBuilder.Entity<tsg035_municipios>()
                .HasMany(e => e.tsg039_colonias)
                .WithOptional(e => e.tsg035_municipios)
                .HasForeignKey(e => e.tsg035_municipios_cd_municipios);

            modelBuilder.Entity<tsg038_municipios>()
                .HasMany(e => e.tsg036_colonias)
                .WithOptional(e => e.tsg038_municipios)
                .HasForeignKey(e => e.tsg038_municipios_cd_municipio);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Correo)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }
    }
}
