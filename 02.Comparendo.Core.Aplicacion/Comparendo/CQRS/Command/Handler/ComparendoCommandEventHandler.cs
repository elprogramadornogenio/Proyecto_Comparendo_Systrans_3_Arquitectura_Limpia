using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Command.Commands;
using _02.Comparendo.Core.Aplicacion.Comparendo.UseCase.Interfaces;
using _02.Comparendo.Core.Aplicacion.Utils;
using MediatR;

namespace _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Command.Handler
{
    public class ComparendoCommandEventHandler : 
        IRequestHandler<CrearComparendoCommand, Response<Guid>>
    {
        private readonly ICrearComparendoUseCase _crearComparendoUseCase;
        public ComparendoCommandEventHandler(
            ICrearComparendoUseCase crearComparendoUseCase
        )
        {
            _crearComparendoUseCase = crearComparendoUseCase;
        }
        public async Task<Response<Guid>> Handle(
            CrearComparendoCommand request, CancellationToken cancellationToken)
        {
            var resultadoComparendoCreado = await _crearComparendoUseCase
                .crearComparendoValidation(request);
            return resultadoComparendoCreado!;
        }
    }
}