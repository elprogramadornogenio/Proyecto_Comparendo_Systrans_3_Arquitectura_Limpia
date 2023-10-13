using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Query.DTOs.Request;
using FluentValidation;

namespace _04.Comparendo.Infraestructura.Comparendo.Validations
{
    public class ListarComparendoValidator: AbstractValidator<ComparendoRequestDto>
    {
        public string Errores { get; set; } = string.Empty;

        public ListarComparendoValidator()
        {
            //ID comparendo: obligatorio
            RuleFor(comparendoRequestDto => comparendoRequestDto.Id)
                .NotNull().NotEmpty()
                .WithMessage("Id Comparendo es Obligatorio");
            //ComFecha: obligatorio en Simit
            RuleFor(comparendoRequestDto => comparendoRequestDto.CodigoInfraccion)
                .NotNull().NotEmpty()
                .WithMessage("El Codigo Infraccion es Obligatoria");
        }

        public async Task<bool> ValidarComparendoRequestAsync(
                ComparendoRequestDto comparendoRequestDto)
        {
            var resultadoValidacionComparendoRequestDto = await 
                ValidateAsync(comparendoRequestDto);
            if(!resultadoValidacionComparendoRequestDto.IsValid) 
                Errores = string.Join(", ", resultadoValidacionComparendoRequestDto.Errors);
            return resultadoValidacionComparendoRequestDto.IsValid;
        }
    }
}