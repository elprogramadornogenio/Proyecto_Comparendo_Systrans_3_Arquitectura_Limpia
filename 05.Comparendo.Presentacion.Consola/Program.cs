
using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Query.DTOs.Request;
using _02.Comparendo.Core.Aplicacion.Utils;
using _05.Comparendo.Presentacion.Consola.Extension;
using _05.Comparendo.Presentacion.Consola.Helpers;
using _05.Comparendo.Presentacion.Consola.Logic.Comparendo;
using _05.Comparendo.Presentacion.Consola.Logic.LecturaArchivo;
using _05.Comparendo.Presentacion.Consola.Models;
using _05.Comparendo.Presentacion.Consola.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;


var coleccionServicios = new ServiceCollection();
coleccionServicios.CrearInfraestructuraServicios();
coleccionServicios.crearServiciosBasesDatos();
var servicioControlador = coleccionServicios
    .BuildServiceProvider().GetService<IComparendoController>();
var comparendosSimulacionSimit = coleccionServicios
    .BuildServiceProvider().GetRequiredService<IComparendosSimulacionSimitRespository>();


try
{
    bool salirDelPrograma = true;
    do
    {
        int opcionEscogidaOperacionComparendos = 2;
        /*
        do
        {
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("Ingresa el número de opción para realizar una tarea");
            Console.WriteLine("1. Migrar archivos planos de Simit a Systrans");
            Console.WriteLine("2. Exportar comparendos de systrans a archivos planos en Estandar Simit");
            Console.WriteLine("Digita cualquier numero diferente a 1 y 2 para salir del programa");
            Console.Write("Ingresa el número: ");
        } while (!int.TryParse(Console.ReadLine(), out opcionEscogidaOperacionComparendos));*/

        switch ((OpcionEscogida)opcionEscogidaOperacionComparendos)
        {
            case OpcionEscogida.MigrarComparendosDeSimitASystrans:
                /*
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
                */
                Console.WriteLine("Datos de Simit Migrados a Systrans");
                break;

            case OpcionEscogida.GenerarArchivoPlanoExportarComparendosSimit:
                if(servicioControlador != null) 
                {
                    try
                    {
                        var comparendosGenerarArchivoPlano = new FilterComparendoRequestDto();
                        comparendosGenerarArchivoPlano.IdentificadoresUnicosComparendos!
                            .Add(new ComparendoRequestDto {
                                Id = new Guid("CD8F5ACA-F8F6-4A84-20EF-08DBC8E4E774"),
                                CodigoInfraccion = "C14"
                            });
                        comparendosGenerarArchivoPlano.IdentificadoresUnicosComparendos!
                            .Add(new ComparendoRequestDto {
                                Id = new Guid("FE7F963F-3C0E-4DDC-00E2-08DBC8F3355D"),
                                CodigoInfraccion = "C02"
                            });
                        await servicioControlador
                        .listarComparendosPorIdyCodigoInfraccion(comparendosGenerarArchivoPlano);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                Console.WriteLine("Se ha generado Archivos Planos de Simit");
                break;

            default:
                Console.WriteLine("Has Salido del programa");
                salirDelPrograma = true;
                break;
        }

    } while (!salirDelPrograma);
}
catch (Exception ex)
{
    Console.WriteLine($"Ha ocurrido un error {ex.Message}");
}





/*
#region DefinirControllador

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
*/

