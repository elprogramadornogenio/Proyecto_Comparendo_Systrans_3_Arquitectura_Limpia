using _02.Comparendo.Core.Aplicacion.Utils;
using MediatR;

namespace _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Query.DTOs.Request
{
    public class FilterComparendoRequestDto: 
        IRequest<Response<IEnumerable<ComparendoEstandarSimitDto>>>
    {
        public List<ComparendoRequestDto>? IdentificadoresUnicosComparendos { get; set; }
    }
}