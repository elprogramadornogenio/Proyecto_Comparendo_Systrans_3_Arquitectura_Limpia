using System.ComponentModel.DataAnnotations;

namespace _01.Comparendo.Dominio.Comparendos.enums
{
    public enum eEstado
    {
        Pendiente = 1, 
        Pagado = 2,
        Cese = 3,
        Resolucion = 4,
        [Display(Name = "Pendiente de Fallo")]
        PendienteDeFallo = 5,
        [Display(Name = "Acuerdo de Pago")]
        AcuerdoDePago = 6,
        Exonerado = 7
    }
}