using _01.Comparendo.Dominio.Comparendos.Models;
using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Command.Commands;
using _02.Comparendo.Core.Aplicacion.Comparendo.Utils;

namespace _02.Comparendo.Core.Aplicacion.Comparendo.UseCase.Interfaces
{
    public interface ICrearComparendoUseCase
    {
        Task<Response<Guid>> crearComparendoValidation(CrearComparendoCommand datosCrearComparendoRequest);
    }
}