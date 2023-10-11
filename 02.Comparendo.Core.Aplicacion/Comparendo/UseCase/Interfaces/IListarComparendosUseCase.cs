using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Query.DTOs;
using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Query.DTOs.Request;
using _02.Comparendo.Core.Aplicacion.Utils;

namespace _02.Comparendo.Core.Aplicacion.Comparendo.UseCase.Interfaces
{
    public interface IListarComparendosUseCase
    {
        Task<Response<IEnumerable<ComparendoEstandarSimitDto>>> listarComparendosValidation (
            FilterComparendoRequestDto filterComparendoRequestDto);
    }
}