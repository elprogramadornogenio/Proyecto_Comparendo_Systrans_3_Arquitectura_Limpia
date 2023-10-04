namespace _04.Comparendo.Infraestructura.Extensions
{
    public static class ConvertirCharBool
    {
        public static bool convertirCadenaBoolean(this char? datoChar)
        {
            bool datoBool = (datoChar == 'S') ? true: false;
            return datoBool;
        }
    }
}