using _02.Comparendo.Core.Aplicacion.Utils;
using _05.Comparendo.Presentacion.Consola.Extension;
using _05.Comparendo.Presentacion.Consola.Helpers;
using _05.Comparendo.Presentacion.Consola.Logic.Comparendo;
using _05.Comparendo.Presentacion.Consola.Logic.LecturaArchivo;
using _05.Comparendo.Presentacion.Consola.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;


/*
#region DefinirSimitSimulador

try
{
    var coleccionServicios = new ServiceCollection();
    coleccionServicios.CrearInfraestructuraServicios();
    coleccionServicios.crearServiciosBasesDatos();

    var servicioControlador = coleccionServicios
    .BuildServiceProvider().GetService<IComparendoController>();

    var comparendosSimulacionSimit = coleccionServicios
        .BuildServiceProvider().GetRequiredService<IComparendosSimulacionSimitRespository>();


    if (comparendosSimulacionSimit != null && servicioControlador != null)
    {
        string ubicacionArchivo = "Data/errores.txt";
        try
        {
            var numeroComparendo = 1;
            var numeroComparendosObtener = 1000;

            var numeroTotalComparendosSimulacionSimit = await comparendosSimulacionSimit
                .obtenerNumeroComparendosTotales();

            var rangosComparendos = SimulacionPaginacion.obtenerRangos(
                numeroTotalComparendosSimulacionSimit, numeroComparendosObtener);

            using (StreamWriter escribirArchivo = new StreamWriter(ubicacionArchivo))
            {
                foreach (var rango in rangosComparendos)
                {
                    numeroComparendosObtener = rango.Fin - rango.Inicio;
                    var resultadoConsultaSimulacionSimit = await comparendosSimulacionSimit
                    .obtenerRangoListaComparendos(rango.Inicio, numeroComparendosObtener);
                    foreach (var comparendo in resultadoConsultaSimulacionSimit)
                    {
                        var respuesta = new Response<Guid>();
                        if (comparendo != null)
                            respuesta = await servicioControlador.agregarComparendo(comparendo);
                        Console.WriteLine($"||#{numeroComparendo} || id: {respuesta.Data} || mensaje: {respuesta.Message} || ¿Fue exitoso?: {respuesta.Success} ||");
                        if(!respuesta.Success)
                            await escribirArchivo
                                .WriteLineAsync($"#{numeroComparendo}, {comparendo?.ComNumero}, {comparendo?.ComEstadoCom}, {comparendo?.ComInfraccion}, {comparendo?.ComPolca}, {comparendo?.CompPlacaAgente}, {respuesta.Message}");
                        numeroComparendo++;
                    }
                }
            }
            
            Console.WriteLine("Migración Completada");
        }
        catch (IOException e)
        {
            Console.WriteLine("Ocurrió un error al crear el archivo: " + e.Message);
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"{ex.Message}");
}

#endregion
*/

#region DefinirControllador
var coleccionServicios = new ServiceCollection();
    coleccionServicios.CrearInfraestructuraServicios();
    coleccionServicios.crearServiciosBasesDatos();


var servicioControlador = coleccionServicios
    .BuildServiceProvider().GetService<IComparendoController>();

if(servicioControlador != null)
{
    var lecturaArchivo = new LecturaArchivo("Data/25754000comp.txt");
    var comparendos = await lecturaArchivo.VerContenidoArchivo();
    
    var numeroComparendo = 1;
    var respuesta = new Response<Guid>();
    
    foreach (var comparendo in comparendos)
    { 
        if(comparendo != null) 
        {
            respuesta = await servicioControlador.agregarComparendo(comparendo);
            Console.WriteLine($"||#{numeroComparendo} || id: {respuesta.Data} || mensaje: {respuesta.Message} || ¿Fue exitoso?: {respuesta.Success} ||");
            numeroComparendo++; 
        }
    }
    
}
#endregion





