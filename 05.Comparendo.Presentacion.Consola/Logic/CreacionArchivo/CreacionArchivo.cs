using System.Text;

namespace _05.Comparendo.Presentacion.Consola.Logic.CreacionArchivo
{
    public class CreacionArchivo
    {
        private readonly string _rutaArchivo;
        public CreacionArchivo(string rutaArchivo)
        {
            _rutaArchivo = rutaArchivo;
        }

        public async Task EscribirArchivoSaltoLinea(string textoParaInsertarEnElArchivo)
        {
            try
            {
                var codificacionArchivoISOLatin = Encoding.GetEncoding("ISO-8859-1");
                using (StreamWriter escribirEnElArchivo = new StreamWriter(_rutaArchivo, true, codificacionArchivoISOLatin))
                {
                    await escribirEnElArchivo.WriteLineAsync(textoParaInsertarEnElArchivo);
                    //escribirEnElArchivo.Close();
                }
            }
            catch (Exception ex)
            {
                Console
                .WriteLine($"Ha ocurrido un Error en el momento de escribir un archivo {ex.Message}");
            }
        }
    }
}