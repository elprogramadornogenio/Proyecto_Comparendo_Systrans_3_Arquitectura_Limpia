using System.Text;
namespace _05.Comparendo.Presentacion.Consola.Extension
{
    public static class ConversorComparendoEstandarSimitASCIIEntero
    {
        public static long convertirLineaStringASCII(this string? cadena)
        {
            if(cadena == null)
                return 0;
            char[] cadenaTransformadaChar = cadena.ToCharArray();
            long sumaTotalCadenaASCII = 0;

            foreach (var letra in cadenaTransformadaChar)
            {
                sumaTotalCadenaASCII += Convert.ToInt64(letra);
            }
            return sumaTotalCadenaASCII;
        }
    }
}