using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Command.Commands;
using _02.Comparendo.Core.Aplicacion.Utils;

namespace _02.Comparendo.Core.Aplicacion.Comparendo.UseCase.Interfaces
{
    public interface ICrearComparendoUseCase
    {
        Task<Response<Guid>> crearComparendoValidation(
            CrearComparendoCommand datosCrearComparendoRequest);
    }
}