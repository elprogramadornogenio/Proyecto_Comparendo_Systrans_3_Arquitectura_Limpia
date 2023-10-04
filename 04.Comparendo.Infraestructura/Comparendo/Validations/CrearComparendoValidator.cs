using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Command.Commands;
using _04.Comparendo.Infraestructura.Extensions;
using FluentValidation;

namespace _04.Comparendo.Infraestructura.Comparendo.Validations
{
    public class CrearComparendoValidator: 
        AbstractValidator<CrearComparendoCommand>
    {
        public string Errores { get; set; } = string.Empty;
        public CrearComparendoValidator()
        {
            RuleFor(comparendo => comparendo.ComNumero)
                .NotNull().NotEmpty()
                .WithMessage("Numero es Obligatorio");
            RuleFor(comparendo => comparendo.ComFecha)
                .Must(ConvertirStringDate.esValidaLaCadenaFecha)
                .WithMessage("Fecha es Obligatoria");
            RuleFor(comparendo => comparendo.ComHora)
                .Must(ConvertirStringDate.esValidaLaCadenaHora)
                .WithMessage("Hora es Obligatoria");
            RuleFor(comparendo => comparendo.ComDir)
                .NotNull().NotEmpty()
                .WithMessage("Dirección Comparendo es Obligatorio");
            RuleFor(comparendo => comparendo.ComDivipoMuni)
                .NotNull().NotEmpty()
                .WithMessage("Codigo Divipo Municipio es Obligatorio");
            RuleFor(comparendo => comparendo.ComTipoVehi)
                .NotNull().NotEmpty()
                .WithMessage("Definir Tipo de Vehiculo es Obligatorio");
            RuleFor(comparendo => comparendo.ComTipoSer)
                .NotNull().NotEmpty()
                .WithMessage("Definir Tipo de Servicio es Obligatorio");
            RuleFor(comparendo => comparendo.ComInfractor)
                .NotNull().NotEmpty()
                .WithMessage("Definir Numero Documento del Infractor es Obligatorio");
            RuleFor(comparendo => comparendo.ComTipoDoc)
                .NotNull().NotEmpty()
                .WithMessage("Definir Tipo Documento del Infractor es Obligatorio");
            RuleFor(comparendo => comparendo.ComEdadInfractor)
                .NotNull().NotEmpty()
                .WithMessage("Definir la edad del Infractor es Obligatorio");
            RuleFor(comparendo => comparendo.CompPlacaAgente)
                .NotNull().NotEmpty()
                .WithMessage("La placa del agente es obligatoria");
            RuleFor(comparendo => comparendo.ComFuga)
                .NotNull().NotEmpty()
                .WithMessage("Definir la fuga es obligatoria");
            RuleFor(comparendo => comparendo.ComAcci)
                .NotNull().NotEmpty()
                .WithMessage("Definir la Accidentalidad es obligatoria");
            RuleFor(comparendo => comparendo.ComInmov)
                .NotNull().NotEmpty()
                .WithMessage("Definir la Inmovilizacion es obligatoria");
            RuleFor(comparendo => comparendo.ComOrganismo)
                .NotNull().NotEmpty()
                .WithMessage("Definir el organismo es obligatorio");
            RuleFor(comparendo => comparendo.ComEstadoCom)
                .NotNull().NotEmpty()
                .WithMessage("Definir el estado del comparendo es obligatorio");
            RuleFor(comparendo => comparendo.ComInfraccion)
                .NotNull().NotEmpty()
                .WithMessage("Definir la infraccion es obligatorio");
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