using _03.Comparendo.Infraestructura.Persistencia;
using _05.Comparendo.Presentacion.Consola.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace _05.Comparendo.Presentacion.Consola.Extension
{
    public static class InfraestructureRegistrationDatabaseService
    {
        public static IServiceCollection crearServiciosBasesDatos(
            this IServiceCollection servicios)
        {
            var serviceProvider = servicios
            .AddDbContext<DataContext>(opciones => opciones
                .UseSqlServer("Data source=192.168.20.39;initial catalog=Systrans3;user id=sa;password=Creati_451789;TrustServerCertificate=True;"))
                .BuildServiceProvider();

            var serviceImportarComparendo = servicios
            .AddDbContext<DataContextSimulacionSimit>(opciones => opciones
                .UseSqlServer("Data source=192.168.20.39;initial catalog=SystransNegocio28072023PruebasRunt;user id=sa;password=Creati_451789;TrustServerCertificate=True;"))
                .BuildServiceProvider();
            
            return servicios;
        }
    }
}