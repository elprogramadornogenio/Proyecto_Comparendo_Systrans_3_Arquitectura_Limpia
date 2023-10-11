namespace _02.Comparendo.Core.Aplicacion.Extensions
{
    public static class ConvertirStringInt
    {
        public static int convertirCadenaEntero(this string? cadenaTextoNumerico)
        {
            if(int.TryParse(cadenaTextoNumerico, out int numero))
                return numero;
            return 0;
        }
    }
}