
using Microsoft.EntityFrameworkCore;
using NSoft.Models;
using NSoft.Models.Presupuesto;

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

        //PRESUPUESTO
        public DbSet<Presupuesto> Presupuestos { get; set; }
        public DbSet<Descompuesto> Descompuestos { get; set; }
        public DbSet<DetalleDescompuesto> DetalleDescompuestos { get; set; }
        public DbSet<ManoDeObra> ManoDeObras { get; set; }
        public DbSet<TipoManoObra> TipoManoObras { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Rol>()
                .Property(r => r.Estado)
                .HasDefaultValue(true);

            modelBuilder.Entity<Usuario>()
                .Property(r => r.Estado)
                .HasDefaultValue(true);

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.Property(p => p.Estado)
                .HasDefaultValue(true);

                entity.HasMany(p => p.Contactos)
                .WithOne(c => c.Proveedor)
                .HasForeignKey(c => c.ProveedorCifId);
            }
            );

            modelBuilder.Entity<ProveedorMarca>(
                entity =>
                {
                    entity.HasKey(pm => new { pm.ProveedorCifId, pm.MarcaId });
                    
                    entity.Property(pm => pm.Estado)
                    .HasDefaultValue(true);

                    entity.HasOne(pm => pm.Proveedor)
                    .WithMany(p => p.ProveedoresMarcas)
                    .HasForeignKey(pm => pm.ProveedorCifId);

                    entity.HasOne(pm => pm.Marca)
                    .WithMany(m => m.ProveedoresMarcas)
                    .HasForeignKey(pm => pm.MarcaId);
                }
                );

            modelBuilder.Entity<PrecioTarifa>(
                entity =>
                {
                    entity.HasKey(pt => new { pt.MaterialId, pt.ProveedorCifId, pt.MarcaId });

                    entity.Property(pt => pt.Estado).HasDefaultValue(true);

                    entity.HasOne(pt => pt.Material)
                    .WithMany(m => m.precioTarifas)
                    .HasForeignKey(pt => pt.MaterialId);

                    entity.HasOne(pt => pt.ProveedorMarca)
                    .WithMany(pm => pm.PrecioTarifa)
                    .HasForeignKey(pt => new { pt.ProveedorCifId, pt.MarcaId })
                    .OnDelete(DeleteBehavior.Restrict);
                }
                );

            modelBuilder.Entity<CategoriaMaterial>()
                .HasMany(cm => cm.Materiales)
                .WithOne(m => m.CategoriasMaterial)
                .HasForeignKey(m => m.CategoriaId);

            modelBuilder.Entity<RolModulo>(entity =>
            {
                entity.HasKey(rm => new { rm.RolId, rm.ModuloId, rm.TipoPermisoId });

                entity.Property(rm => rm.Estado)
                .HasDefaultValue(true);
            });

            //PRESUPUESTO
            modelBuilder.Entity<Descompuesto>()
                .HasIndex(d => new { d.IsPlantilla, d.Titulo })
                .HasDatabaseName("IX_Descompuestos_Plantilla_Nombre");

            // Especificar la precisión para las propiedades decimales.
            modelBuilder.Entity<Descompuesto>()
                .Property(d => d.Precio).HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<Descompuesto>()
                .Property(d => d.ManoObra).HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<Descompuesto>()
                .Property(d => d.Beneficio).HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<Descompuesto>()
                .Property(d => d.GastoAdministrativo).HasColumnType("decimal(18, 2)");

            // Presupuesto (1) -> Descompuesto (N)
            modelBuilder.Entity<Presupuesto>()
                .HasMany(p => p.Descompuestos)
                .WithOne(d => d.Presupuesto)
                .HasForeignKey(d => d.PresupuestoId)
                // Evitamos el borrado en cascada para no tener problemas con las plantillas (cuyo PresupuestoId es nulo).
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Descompuesto (1) -> DetallaDescompuesto (N)
            modelBuilder.Entity<Descompuesto>()
                .HasMany(d => d.DetalleDescompuestos)
                .WithOne(dd => dd.Descompuesto)
                .HasForeignKey(dd => dd.DescompuestoId)
                // Si se borra un Descompuesto, sus detalles se borran con él.
                .OnDelete(DeleteBehavior.Cascade);

            // Descompuesto (1) -> ManoDeObra (N)
            modelBuilder.Entity<Descompuesto>()
                .HasMany(d => d.ManoDeObras)
                .WithOne(m => m.Descompuesto)
                .HasForeignKey(m => m.DescompuestoId)
                // Si se borra un Descompuesto, su mano de obra asociada también.
                .OnDelete(DeleteBehavior.Cascade);

            // TipoManoObra (1) -> ManoDeObra (N)
            modelBuilder.Entity<TipoManoObra>()
                .HasMany(t => t.ManoDeObras)
                .WithOne(m => m.TipoManoObra)
                .HasForeignKey(m => m.TipoManoObraId)
                // Evita que se borre un tipo si está en uso.
                .OnDelete(DeleteBehavior.Restrict);


            // --- Configuración de otras entidades (tipos decimales) ---

            modelBuilder.Entity<DetalleDescompuesto>()
                .Property(d => d.Precio).HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<DetalleDescompuesto>()
                .Property(d => d.Descuento).HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<ManoDeObra>()
                .Property(m => m.Precio).HasColumnType("decimal(18, 2)");

        }
    }
}
