using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Command.Commands;
using _02.Comparendo.Core.Aplicacion.Comparendo.Utils;

namespace _05.Comparendo.Presentacion.Consola.Logic.Comparendo
{
    public interface IComparendoController
    {
        Task<Response<Guid>> agregarComparendo(CrearComparendoCommand comparendo);
    }
}