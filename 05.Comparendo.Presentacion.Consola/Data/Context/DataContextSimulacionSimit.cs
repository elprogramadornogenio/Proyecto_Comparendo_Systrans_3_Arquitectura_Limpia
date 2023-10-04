using _05.Comparendo.Presentacion.Consola.Models;
using Microsoft.EntityFrameworkCore;

namespace _05.Comparendo.Presentacion.Consola.Data.Context
{
    public class DataContextSimulacionSimit: DbContext
    {
        public DataContextSimulacionSimit(DbContextOptions opciones): base(opciones) {}

        public DbSet<CodigoInfraccion>? CodigoInfraccion { get; set; }
        public DbSet<Localidad>? Localidad { get; set; }
        public DbSet<TipoDocumento>? TipoDocumento { get; set; }
        public DbSet<TipoInfractor>? TipoInfractor { get; set; }
        public DbSet<vwAgente>? vwAgente { get; set; }
        public DbSet<vwComparendo>? vwComparendo { get; set; }
        public DbSet<vwDireccion>? vwDireccion { get; set; }
        public DbSet<vwLicenciaConduccion>? vwLicenciaConduccion { get; set; }
        public DbSet<vwPersona>? vwPersona { get; set; }
        
    }
}