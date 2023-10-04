using System.Reflection;
using _02.Comparendo.Core.Aplicacion.Comparendo.Repositorio;
using _02.Comparendo.Core.Aplicacion.Comparendo.UseCase.Implementation;
using _02.Comparendo.Core.Aplicacion.Comparendo.UseCase.Interfaces;
using _03.Comparendo.Infraestructura.Persistencia.Comparendo;
using _05.Comparendo.Presentacion.Consola.logic.Comparendo;
using _05.Comparendo.Presentacion.Consola.Logic.Comparendo;
using _05.Comparendo.Presentacion.Consola.Repository.Implementations;
using _05.Comparendo.Presentacion.Consola.Repository.Interfaces;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace _05.Comparendo.Presentacion.Consola.Extension
{
    public static class InfraestructureRegistrationService
    {
        public static IServiceCollection CrearInfraestructuraServicios(
            this IServiceCollection servicios)
        {
            
            // registrar ciclos de vida de dependencias
            servicios
            .AddScoped<IComparendoRepository, ComparendoRepository>();
            servicios
            .AddScoped<IComparendoAgenteTransitoRepository, ComparendoAgenteTransitoRepository>();
            servicios
            .AddScoped<IComparendoTipoInfraccionRepository, ComparendoTipoInfraccionRepository>();
            servicios
            .AddScoped<IComparendoInfraccionComparendoRepository, ComparendoInfraccionComparendoRepository>();


            // registrar casos de uso en ciclos de vida
            servicios
            .AddScoped<ICrearComparendoUseCase, CrearComparendoUseCase>();

            // registrar ejemplos de controladores
            servicios
            .AddScoped<IComparendoController, ComparendoController>();


            // registrar Comparendos para simular SIMIT
            servicios
            .AddScoped<IComparendosSimulacionSimitRespository, ComparendosSimulacionSimitRespository>();

            // configurar el patrón mediator
            /*
            // registrar un handler especifico para inyectar
            servicios.AddMediatR(config => config
                .RegisterServicesFromAssembly(typeof(ComparendoEventHandler).Assembly));
            */
            /*
            // registrar los handlers de un mismo proyecto
            servicios
            .AddMediatR(
                cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            */
            // registrar los handles especificos de otro proyecto
            servicios
                .AddMediatR(cfg => { cfg
                .RegisterServicesFromAssemblies(Assembly.Load("02.Comparendo.Core.Aplicacion")); 
                });
            
            
            
            
            
            // configuracion de mappers
            // esto configura todos los dominios para encontrar el automapper
            //servicios.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            // este configura para buscar la funcion de configuracion del automapper en un ensamblado
            servicios.AddAutoMapper(Assembly.Load("04.Comparendo.Infraestructura"));
            
            return servicios;
        }
    }
}