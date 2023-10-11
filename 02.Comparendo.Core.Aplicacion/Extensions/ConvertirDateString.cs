namespace _02.Comparendo.Core.Aplicacion.Extensions
{
    public static class ConvertirDateString
    {
        public static string convertirFechaCadena(this DateTime fecha)
        {
            return fecha.ToString("dd/MM/yyyy");
        }

        public static string? convertirFechaCadena(this DateTime? fecha)
        {
            if(fecha == null)
                return null;
            return fecha.Value.ToString("dd/MM/yyyy");
        }

        public static string convertirHoraCadena(this DateTime hora)
        {
            return hora.ToString("HHmm");
        }

    }
}