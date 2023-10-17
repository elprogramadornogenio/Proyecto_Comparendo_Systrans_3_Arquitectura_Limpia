using _01.Comparendo.Dominio.Comparendos.Models;

namespace _02.Comparendo.Core.Aplicacion.Extensions
{
    public static class ConcatenarNombres
    {
        public static string concatenarNombresComparendo(string? _nombres, string? _apellidos)
        {
            var nombres = !string.IsNullOrWhiteSpace(_nombres) ? 
                _nombres : string.Empty;
            var apellidos = !string.IsNullOrWhiteSpace(_apellidos) ? 
                _apellidos : string.Empty;
            var nombreCompleto = nombres + " " + apellidos;
            return nombreCompleto.Trim();
        }
    }
}