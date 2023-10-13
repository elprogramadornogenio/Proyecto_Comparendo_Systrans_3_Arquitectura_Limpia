using _02.Comparendo.Core.Aplicacion.Utils;
using MediatR;

namespace _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Query.DTOs.Request
{
    public class ComparendoRequestDto : IRequest<Response<ComparendoEstandarSimitDto>>
    {
        public Guid Id { get; set; }
        public string? CodigoInfraccion { get; set; }
    }
}