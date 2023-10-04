using System.ComponentModel.DataAnnotations;

namespace _01.Comparendo.Dominio.Comparendos.enums
{
    public enum eCodigoPasajeros
    {
        Colectivo = 1,
        Individual = 2,
        Masivo = 3,
        [Display(Name = "Especial escolar")]
        EspecialEscolar = 4,
        [Display(Name = "Especial asalariado")]
        EspecialAsalariado = 5,
        [Display(Name = "Especial turismo")]
        EspecialTurismo = 6,
        [Display(Name = "Especial ocasional")]
        EspecialOcasional = 7
    }
}