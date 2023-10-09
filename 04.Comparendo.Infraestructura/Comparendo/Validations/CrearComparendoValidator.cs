using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Command.Commands;
using _02.Comparendo.Core.Aplicacion.Extensions;
using FluentValidation;

namespace _04.Comparendo.Infraestructura.Comparendo.Validations
{
    public class CrearComparendoValidator: 
        AbstractValidator<CrearComparendoCommand>
    {
        public string Errores { get; set; } = string.Empty;
        public CrearComparendoValidator()
        {
            //ComNumero: obligatorio en Simit
            RuleFor(comparendo => comparendo.ComNumero)
                .NotNull().NotEmpty()
                .WithMessage("Numero es Obligatorio");
            //ComFecha: obligatorio en Simit
            RuleFor(comparendo => comparendo.ComFecha)
                .Must(ConvertirStringDate.esValidaLaCadenaFecha)
                .WithMessage("Fecha es Obligatoria");
            //ComInfractor: obligatorio en Simit
            RuleFor(comparendo => comparendo.ComInfractor)
                .NotNull().NotEmpty()
                .WithMessage("Definir Numero Documento del Infractor es Obligatorio");
            //ComTipoDoc: obligatorio en Simit
            RuleFor(comparendo => comparendo.ComTipoDoc)
                .NotNull().NotEmpty()
                .WithMessage("Definir Tipo Documento del Infractor es Obligatorio");
            //ComValor: obligatorio en Simit
            RuleFor(comparendo => comparendo.ComValor)
                .NotNull().NotEmpty()
                .WithMessage("El valor del comparendo es obligatorio");
            //ComValAd: obligatorio en Simit
            RuleFor(comparendo => comparendo.ComValAd)
                .NotNull().NotEmpty()
                .WithMessage("El valor adicional del comparendo es obligatorio");
            //ComOrganismo: obligatorio en Simit
            RuleFor(comparendo => comparendo.ComOrganismo)
                .NotNull().NotEmpty()
                .WithMessage("Definir el organismo es obligatorio");
            //ComEstadoCom: obligatorio en Simit
            RuleFor(comparendo => comparendo.ComEstadoCom)
                .NotNull().NotEmpty()
                .WithMessage("Definir el estado del comparendo es obligatorio");
            //ComPolca: obligatorio en Simit
            RuleFor(comparendo => comparendo.ComPolca)
                .NotNull().NotEmpty()
                .WithMessage("Definir si el comparendo es POLCA es obligatorio");
            //ComInfraccion: obligatorio en Simit
            RuleFor(comparendo => comparendo.ComInfraccion)
                .NotNull().NotEmpty()
                .WithMessage("Definir la infraccion es obligatorio");
            //ComValInfra: obligatorio en Simit
            RuleFor(comparendo => comparendo.ComValInfra)
                .NotNull().NotEmpty()
                .WithMessage("El valor total de la infracción es obligatorio");
        }

        public async Task<bool> ValidarComparendoAsync(CrearComparendoCommand comparendo)
        {
            var resultadoValidacionComparendo = await ValidateAsync(comparendo);
            if(!resultadoValidacionComparendo.IsValid) 
                Errores = string.Join(", ", resultadoValidacionComparendo.Errors);
            return resultadoValidacionComparendo.IsValid;
        }
    }

    
}