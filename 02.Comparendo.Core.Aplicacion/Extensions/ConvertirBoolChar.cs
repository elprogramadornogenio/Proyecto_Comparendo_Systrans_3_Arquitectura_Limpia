namespace _02.Comparendo.Core.Aplicacion.Extensions
{
    public static class ConvertirBoolChar
    {
        public static char convertirBooleanCadena(this bool datoBool)
        {
            char datoChar = (datoBool) ? 'S': 'N';
            return datoChar;
        }
    }
}