using System.IO;
using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Command.Commands;
using _05.Comparendo.Presentacion.Consola.Logic.Mapping;
namespace _05.Comparendo.Presentacion.Consola.Logic.LecturaArchivo
{
    public class LecturaArchivo
    {
        private readonly string _rutaArchivo;
        public LecturaArchivo(string rutaArchivo)
        {
            _rutaArchivo = rutaArchivo;
        }

        public async Task<List<CrearComparendoCommand>> VerContenidoArchivo()
        {
            List<CrearComparendoCommand> comparendos = new();
            try
            {
                using (var contenido = new StreamReader(_rutaArchivo))
                {
                    while (!contenido.EndOfStream)
                    {
                        var linea = await contenido.ReadLineAsync();
                        if(linea != null)
                        {
                            var comparendoLineaString = linea.Split(",");
                            var mappingCrearComparendo = new MappingCrearComparendo();
                            var crearComparendoCommand = mappingCrearComparendo
                                .mappearCrearComparendoCommand(comparendoLineaString);
                            if(crearComparendoCommand != null) 
                                comparendos.Add(crearComparendoCommand);
                        } 
                    }
                    return comparendos;
                }
                
            }
            catch (IOException e)
            {
                Console.WriteLine("Este archivo no se puede leer:");
                Console.WriteLine(e.Message);
                return comparendos;
            }
        }
    }
}