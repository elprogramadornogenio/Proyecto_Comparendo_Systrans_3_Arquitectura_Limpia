using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Command.Commands;
using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Query.DTO;
using _02.Comparendo.Core.Aplicacion.Comparendo.Repositorio;
using _02.Comparendo.Core.Aplicacion.Utils;
using _04.Comparendo.Infraestructura.Comparendo.Validations;
using _05.Comparendo.Presentacion.Consola.Logic.Comparendo;
using MediatR;


namespace _05.Comparendo.Presentacion.Consola.logic.Comparendo
{
    public class ComparendoController: IComparendoController
    {
        private IMediator _mediator; // enviar datos
        private IComparendoRepository _comparendoRepository; // consultar datos

        public ComparendoController(
            IMediator mediator, 
            IComparendoRepository comparendoRepository)
        {
            _mediator = mediator;
            _comparendoRepository = comparendoRepository;
        }

        public async Task<Response<Guid>> agregarComparendo(CrearComparendoCommand comparendo)
        {
            try
            {
                var validacionComparendo = new CrearComparendoValidator();
                if(! await validacionComparendo.ValidarComparendoAsync(comparendo))
                    return new Response<Guid>(validacionComparendo.Errores);
                var respuestaComparendo = await _mediator.Send(comparendo);
                return respuestaComparendo;
            }
            catch (Exception ex)
            {
                return new Response<Guid>(ex.Message);
            }
        }

        public async Task<List<ComparendoSimitDto>> listarRangoComparendos(
            FilterComparendoDTO filterComparendoDTO)
        {
            var listaComparendos = await _comparendoRepository.getFilter(
                filterComparendoDTO.numeroComparendoOmitir,
                filterComparendoDTO.numeroComparendosConsultar
                );
            return listaComparendos;
        }
    }
}