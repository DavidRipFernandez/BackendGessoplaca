
using Microsoft.EntityFrameworkCore;
using NSoft.Models;

namespace NSoft.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol>Roles { get; set; }
        public DbSet<Modulo> Modulos { get; set; }  
        public DbSet<TipoPermiso> TiposPermiso  { get; set; }   
        public DbSet<RolModulo> RolesModulos { get; set; }
        //PresupuestoMaterial
        public DbSet<CategoriaMaterial> CategoriasMateriales { get; set; }
        public DbSet<Material> Materiales { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Contacto> Contactos { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<ProveedorMarca> ProveedoresMarcas { get; set; }
        public DbSet<PrecioTarifa> PreciosTarifas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Proveedor>()
                .HasMany(p => p.Contactos)
                .WithOne(c => c.Proveedor)
                .HasForeignKey(c => c.ProveedorCifId);

            modelBuilder.Entity<ProveedorMarca>()
                .HasKey(pm => new { pm.ProveedorCifId, pm.MarcaId });

            modelBuilder.Entity<ProveedorMarca>()
                .HasOne(pm => pm.Proveedor)
                .WithMany(p => p.ProveedoresMarcas)
                .HasForeignKey(pm => pm.ProveedorCifId);

            modelBuilder.Entity<ProveedorMarca>()
                .HasOne(pm => pm.Marca)
                .WithMany(m => m.ProveedoresMarcas)
                .HasForeignKey(pm => pm.MarcaId);

            modelBuilder.Entity<RolModulo>().HasKey(rm => new { rm.RolId, rm.ModuloId, rm.TipoPermisoId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
