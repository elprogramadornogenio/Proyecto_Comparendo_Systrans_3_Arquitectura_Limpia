using System.ComponentModel.DataAnnotations;

namespace _01.Comparendo.Dominio.Comparendos.enums
{
    public enum eFuenteComparendo
    {
        Desconocida = 0,
        [Display(Name = "Comparendera Simit")]
        ComparenderaSimit = 1,
        [Display(Name = "Comparendera O.T.")]
        ComparenderaOT = 2
    }
}