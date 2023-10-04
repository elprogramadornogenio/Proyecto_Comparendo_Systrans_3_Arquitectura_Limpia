namespace _05.Comparendo.Presentacion.Consola.Extension
{
    public static class ConversorDatos
    {
        public static decimal conversionStringDecimal(this string? numeroDecimalCadena)
        {
            if( numeroDecimalCadena != null 
                && decimal.TryParse(numeroDecimalCadena, out decimal numeroDecimal)) 
                return numeroDecimal;
            return 0;
        }

        public static int conversionStringInt(this string? numeroEnteroCadena)
        {
            if( numeroEnteroCadena != null 
                && int.TryParse(numeroEnteroCadena, out int numeroEntero)) 
                return numeroEntero;
            return 0;
        }

        public static char conversionStringChar(this string? cadena)
        {
            if( cadena != null && cadena.Equals("S")) return 'S';
            return 'N';
        }

        public static string conversionTimeSpanStringHHmm(this TimeSpan tiempoHoras)
        {
            return $"{tiempoHoras.Hours:D2}{tiempoHoras.Minutes:D2}";
        }
    }
}