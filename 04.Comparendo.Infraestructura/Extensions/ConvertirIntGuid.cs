namespace _04.Comparendo.Infraestructura.Extensions
{
    public static class ConvertirIntGuid
    {
        public static Guid trasformarEnteroGuid(this int numeroEntero)
        {
            byte[] convetirClavePrincipalIntaByte = new byte[16];
            BitConverter.GetBytes(numeroEntero)
                .CopyTo(convetirClavePrincipalIntaByte, 0);
            var convertirClavePrimariaByteGuid = new Guid(
                convetirClavePrincipalIntaByte);
            return convertirClavePrimariaByteGuid;
        }
    }
}