using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Query.DTOs;
using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Query.DTOs.Request;
using _02.Comparendo.Core.Aplicacion.Comparendo.UseCase.Interfaces;
using _02.Comparendo.Core.Aplicacion.Utils;
using MediatR;

namespace _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Query.Handler
{
    public class ComparendoQueryEventHandler :
        IRequestHandler<ComparendoRequestDto,
            Response<ComparendoEstandarSimitDto>>
    {
        private readonly IListarComparendosUseCase _listarComparendosUseCase;

        public ComparendoQueryEventHandler(IListarComparendosUseCase listarComparendosUseCase)
        {
            _listarComparendosUseCase = listarComparendosUseCase;
        }
        public async Task<Response<ComparendoEstandarSimitDto>> Handle(
            ComparendoRequestDto request, CancellationToken cancellationToken)
        {
            var resultadoListadoComparendos = await _listarComparendosUseCase
                .listarComparendosValidation(request);
            return resultadoListadoComparendos!;
        }
    }
}