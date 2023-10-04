using System.Globalization;

namespace _04.Comparendo.Infraestructura.Extensions
{
    public static class ConvertirStringDate
    {
        public static DateTime? convertirCadenaFecha(this string? fechaCadena)
        {
            DateTime fecha;
            if(DateTime.TryParse(fechaCadena, out fecha))
                return fecha;
            return null;
        }

        public static DateTime? convertirCadenaHora(this string? horaCadena)
        {
            DateTime hora;
            if(DateTime.TryParseExact(horaCadena,
                "HHmm", CultureInfo.InvariantCulture, DateTimeStyles.None, out hora))
                return hora;
            return null;
        }

        public static bool esValidaLaCadenaFecha(string? fechaCadena)
        {
            return DateTime.TryParse(fechaCadena, out _);   
        }

        public static bool esValidaLaCadenaHora(string? horaCadena)
        {
            return DateTime.TryParseExact(horaCadena, "HHmm", 
                CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }
    }
}