using _02.Comparendo.Core.Aplicacion.Comparendo.Utils;
using _05.Comparendo.Presentacion.Consola.Extension;
using _05.Comparendo.Presentacion.Consola.Logic.Comparendo;
using _05.Comparendo.Presentacion.Consola.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;



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
        var numeroComparendo = 0;
        var numeroComparendosObtener = 1000;

        var numeroTotalComparendosSimulacionSimit = await comparendosSimulacionSimit
            .obtenerNumeroComparendosTotales();
        for (int numeroComparendosOmitir = 0;
            numeroComparendosOmitir < numeroTotalComparendosSimulacionSimit;
            numeroComparendosOmitir = numeroComparendosOmitir + numeroComparendosObtener)
        {
            var resultadoConsultaSimulacionSimit = await comparendosSimulacionSimit
            .obtenerRangoListaComparendos(numeroComparendosOmitir, numeroComparendosObtener);
            // listar comparendos simit
            foreach (var comparendo in resultadoConsultaSimulacionSimit)
            {
                var respuesta = new Response<Guid>();
                if (comparendo != null)
                    respuesta = await servicioControlador.agregarComparendo(comparendo);
                Console.WriteLine($"{numeroComparendo} id: {respuesta.Data} mensaje: {respuesta.Message} fue exitoso: {respuesta.Success}");
                numeroComparendo++;
            }
        }
        Console.WriteLine("Migración Completada");
        /*
        var resultadoConsultaSimulacionSimit = await comparendosSimulacionSimit
            .obtenerRangoListaComparendos(42000, numeroComparendosObtener);*/
        
        // hay un error entre los datos en los rangos 42000 y 43000 (42061-42062)
    }
}
catch (Exception ex)
{
    Console.WriteLine($"{ex.Message}");
}


/*
if(comparendosSimulacionSimit != null && servicioControlador != null)
{
    var numeroTotalComparendosSimulacionSimit = await comparendosSimulacionSimit
        .obtenerNumeroComparendosTotales();
    var resultadoConsultaSimulacionSimit = await comparendosSimulacionSimit
        .obtenerRangoListaComparendos(42001, 1);
    foreach (var comparendo in resultadoConsultaSimulacionSimit)
        {
            var respuesta = new Response<Guid>();
            if(comparendo != null)
                respuesta = await servicioControlador.agregarComparendo(comparendo);
        }
}*/

#endregion

#region DefinirControllador
/*

var servicioControlador = coleccionServicios
    .BuildServiceProvider().GetService<IComparendoController>();

if(servicioControlador != null)
{
    var lecturaArchivo = new LecturaArchivo("Data/25754000comp.txt");
    var comparendos = await lecturaArchivo.VerContenidoArchivo();
    
    
    var respuesta = new Response<Guid>();
    
    foreach (var comparendo in comparendos)
    { 
        if(comparendo != null)
            respuesta = await servicioControlador.agregarComparendo(comparendo);
    }
    
}
*/
#endregion





