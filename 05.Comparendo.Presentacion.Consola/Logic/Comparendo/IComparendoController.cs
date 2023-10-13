using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Command.Commands;
using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Query.DTOs.Request;
using _02.Comparendo.Core.Aplicacion.Utils;

namespace _05.Comparendo.Presentacion.Consola.Logic.Comparendo
{
    public interface IComparendoController
    {
        Task<Response<Guid>> agregarComparendo(CrearComparendoCommand comparendo);
        Task listarComparendosPorIdyCodigoInfraccion(FilterComparendoRequestDto filterComparendoRequestDto);
    }
}