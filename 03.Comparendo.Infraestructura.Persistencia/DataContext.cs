using _01.Comparendo.Dominio.Comparendos.Models;
using Microsoft.EntityFrameworkCore;

namespace _03.Comparendo.Infraestructura.Persistencia
{
    public class DataContext: DbContext
    {
        public DbSet<Ciudad>? Ciudad { get; set; }
        public DbSet<Comparendos>? Comparendo { get; set; }
        public DbSet<ComparendoAgenteTransito>? ComparendoAgenteTransito { get; set; }
        public DbSet<ComparendoInfraccionComparendo>? ComparendoInfraccionComparendo { get; set; }
        public DbSet<ComparendoTipoInfraccion>? ComparendoTipoInfraccion { get; set; }
        public DbSet<ComparendoTipoInfraccionReglas>? ComparendoTipoInfraccionReglas { get; set; }
        public DbSet<TipoInfraccionReglas>? TipoInfraccionReglas { get; set; }
        public DbSet<ComparendoTipoVehiculo>? ComparendoTipoVehiculo { get; set; }
        public DbSet<ComparendoClaseServicio>? ComparendoClaseServicio { get; set; }
        public DbSet<ComparendoEstado>? ComparendoEstado {get; set;}
        public DbSet<SecretariaTransito>? SecretariaTransito { get; set; }
        public DbSet<ComparendoTipoInfractor>? ComparendoTipoInfractor { get; set; }

        public DataContext(DbContextOptions<DataContext> opciones): base(opciones) {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            
            // 1. relacion de un agente transito (comparendo agente) a muchos comparendos
            modelBuilder.Entity<ComparendoAgenteTransito>()
                .HasMany(tieneMuchos => tieneMuchos.Comparendos)
                .WithOne(conUn => conUn.AgenteTransito)
                .HasForeignKey(relacionadoCon => relacionadoCon.AgenteTransitoId)
                .OnDelete(DeleteBehavior.Restrict);
            /*
                DeleteBehavior.Restrict = si se elimina un agente de tránsito, no se
                elimina el comparendo
                si se elimina el comparendo no se va a eliminar el agente de tránsito
            */
            
            // 2. Relacion de un ComparendoTipoVehiculo a muchos Comparendos
            modelBuilder.Entity<ComparendoTipoVehiculo>()
                .HasMany(tieneMuchos => tieneMuchos.Comparendos)
                .WithOne(conUn => conUn.TipoVehiculo)
                .HasForeignKey(relacionadoCon => relacionadoCon.TipoVehiculoId)
                .OnDelete(DeleteBehavior.Restrict);

            // 3. Relacion de un ComparendoClaseServicio a muchos Comparendos
            modelBuilder.Entity<ComparendoClaseServicio>()
                .HasMany(tieneMuchos => tieneMuchos.Comparendos)
                .WithOne(conUn => conUn.ClaseServicio)
                .HasForeignKey(relacionadoCon => relacionadoCon.ClaseServicioId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // 4. Relacion de un ComparendoTipoInfractor a muchos Comparendos (null)
            modelBuilder.Entity<ComparendoTipoInfractor>()
                .HasMany(tieneMuchos => tieneMuchos.Comparendos)
                .WithOne(conUn => conUn.TipoInfractor)
                .HasForeignKey(relacionadoCon => relacionadoCon.TipoInfractorId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // 5. Relación de una SecretariaLicenciaConducción con muchos Comparendos (null)
            modelBuilder.Entity<SecretariaTransito>()
                .HasMany(tieneMuchos => tieneMuchos.ComparendosLicenciaConduccion)
                .WithOne(conUn => conUn.LicenciaConduccionSecretaria)
                .HasForeignKey(relacionadoCon => relacionadoCon.LicenciaConduccionSecretariaId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // 6. Relacion de una Secretaria que reporta comparendos con muchos Comparendos
            modelBuilder.Entity<SecretariaTransito>()
                .HasMany(tieneMuchos => tieneMuchos.ComparendosSecretaria)
                .WithOne(conUn => conUn.SecretariaTransito)
                .HasForeignKey(relacionadoCon => relacionadoCon.SecretariaId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // 7. Relacion de un Estado Comparendo con muchos Comparendos
            modelBuilder.Entity<ComparendoEstado>()
                .HasMany(tieneMuchos => tieneMuchos.Comparendos)
                .WithOne(conUn => conUn.ComparendoEstado)
                .HasForeignKey(relacionadoCon => relacionadoCon.EstadoComparendoId)
                .OnDelete(DeleteBehavior.Restrict);

            // 8. Relacion de una Secretaria matricula de transito con muchos Comparendos
            modelBuilder.Entity<SecretariaTransito>()
                .HasMany(tieneMuchos => tieneMuchos.ComparendosMatricula)
                .WithOne(conUn => conUn.SecretariaTransitoMatriculado)
                .HasForeignKey(relacionadoCon => relacionadoCon.MatriculaSecretariaId)
                .OnDelete(DeleteBehavior.Restrict);

            // 9. Relacion de una Ciudad con muchos Comparendos
            modelBuilder.Entity<Ciudad>()
                .HasMany(tieneMuchos => tieneMuchos.ComparendosDireccion)
                .WithOne(conUn => conUn.Ciudad)
                .HasForeignKey(relacionadoCon => relacionadoCon.MunicipioDireccionId)
                .OnDelete(DeleteBehavior.Restrict);

            // 10. Relación de un Secretaria Licencia de Transito con muchos Comparendos
            modelBuilder.Entity<SecretariaTransito>()
                .HasMany(tieneMuchos => tieneMuchos.ComparendosLicenciaTransito)
                .WithOne(conUn => conUn.SecretariaLicenciaTransito)
                .HasForeignKey(relacionadoCon => relacionadoCon.LicenciaTransitoSecretariaId)
                .OnDelete(DeleteBehavior.Restrict);

            // 11. Relacion de una ciudad del infractor con muchos comparendos
            modelBuilder.Entity<Ciudad>()
                .HasMany(tieneMuchos => tieneMuchos.ComparendosDireccionInfractor)
                .WithOne(conUn => conUn.CiudadDelInfractor)
                .HasForeignKey(relacionadoCon => relacionadoCon.CiudadInfractorId)
                .OnDelete(DeleteBehavior.Restrict);

            // 12. relacion de un Comparendo Tipo Infracción a muchos Comparendos Tipo Infracción reglas
            modelBuilder.Entity<ComparendoTipoInfraccion>()
                .HasMany(tieneMuchos => tieneMuchos.ComparendoTipoInfraccionReglas)
                .WithOne(conUn => conUn.comparendoTipoInfraccion)
                .HasForeignKey(relacionadoCon => relacionadoCon.ComparendoTipoInfraccionId)
                .OnDelete(DeleteBehavior.Cascade);
            
            /*
                DeleteBehavior.Cascade = si  se elimina un comparendo tipo infracción se
                eliminan todos los comparendos tipo de infraccion Reglas.
            */

            // relacion de muchos comparendos a muchos comparendos tipo infracción
            /*
                1. Para realizar esta relación se requiere la tabla intermediaria
                "ComparendoInfraccionComparendo".
                2. Realizar la relacion de un comparendo a muchos 
                    ComparendoInfraccionComparendo y si se elimina un comparendo borrar en
                    cascada los elementos relacionados con ComparendoInfraccionComparendo
                3. Realizar la relacion de un comparendoTipoInfraccion a muchos
                    ComparendoInfraccionComparendo y si se elimina un comparendoTipoInfraccion
                    borrar en cascada los elementos relacionados con 
                    ComparendoInfraccionComparendo
            */

            // 1. primer punto de la tabla intermediaria está cumplido ir a la base de datos
            // 2. Relacion de un Comparendo a muchos ComparendoInfraccionComparendo

            modelBuilder.Entity<Comparendos>()
                .HasMany(tieneMuchos => tieneMuchos.ComparendoInfraccionComparendos)
                .WithOne(conUn => conUn.Comparendo)
                .HasForeignKey(relacionadoCon => relacionadoCon.ComaprendoId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // 3. Relacion de un ComparendoTipoInfraccion a muchos ComparendoInfraccionComparendo

            modelBuilder.Entity<ComparendoTipoInfraccion>()
                .HasMany(tieneMuchos => tieneMuchos.ComparendoInfraccionComparendos)
                .WithOne(conUn => conUn.ComparendoTipoInfraccion)
                .HasForeignKey(relacionadoCon => relacionadoCon.ComparendoTipoInfraccionId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}