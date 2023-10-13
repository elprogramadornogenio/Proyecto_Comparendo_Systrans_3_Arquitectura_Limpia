namespace _05.Comparendo.Presentacion.Consola.Logic.CreacionArchivo
{
    public class CreacionArchivo
    {
        private readonly string _rutaArchivo;
        public CreacionArchivo(string rutaArchivo)
        {
            _rutaArchivo = rutaArchivo;
        }

        public void EscribirArchivoSaltoLinea(string textoParaInsertarEnElArchivo)
        {
            try
            {
                using (StreamWriter escribirEnElArchivo = new StreamWriter(_rutaArchivo))
                {
                    escribirEnElArchivo.WriteLineAsync(textoParaInsertarEnElArchivo);
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