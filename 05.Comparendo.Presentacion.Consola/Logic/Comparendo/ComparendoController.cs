using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Command.Commands;
using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Query.DTOs.Request;
using _02.Comparendo.Core.Aplicacion.Comparendo.Repositorio;
using _02.Comparendo.Core.Aplicacion.Utils;
using _04.Comparendo.Infraestructura.Comparendo.Validations;
using _05.Comparendo.Presentacion.Consola.Extension;
using _05.Comparendo.Presentacion.Consola.Logic.Comparendo;
using _05.Comparendo.Presentacion.Consola.Logic.CreacionArchivo;
using MediatR;


namespace _05.Comparendo.Presentacion.Consola.logic.Comparendo
{
    public class ComparendoController: IComparendoController
    {
        private IMediator _mediator; // enviar datos
        private int _codigoDeNuestraDivipo;

        public ComparendoController(
            IMediator mediator, 
            IComparendoRepository comparendoRepository)
        {
            _mediator = mediator;
            _codigoDeNuestraDivipo = 25754000;
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

        public async Task listarComparendosPorIdyCodigoInfraccion(
            FilterComparendoRequestDto filterComparendoRequestDto)
        {
            try
            {
                var listaComparendosConsultar = filterComparendoRequestDto
                    .IdentificadoresUnicosComparendos;
                if( listaComparendosConsultar == null 
                    || listaComparendosConsultar.Count == 0)
                    throw new Exception("Debe insertar datos");
                var validacionComparendoRequestDto = new ListarComparendoValidator();
                var crearArchivoComparendosEstandarSimit = new CreacionArchivo(
                    $"../../Data/{_codigoDeNuestraDivipo}comp.txt");

                foreach (var comparendoRequestDto in listaComparendosConsultar)
                {
                    if(! await validacionComparendoRequestDto
                            .ValidarComparendoRequestAsync(comparendoRequestDto))
                        throw new Exception("Datos no validos");
                    var respuestaComparendo = await _mediator
                        .Send(comparendoRequestDto);
                    if(respuestaComparendo.Data != null)
                        crearArchivoComparendosEstandarSimit.
                            EscribirArchivoSaltoLinea(respuestaComparendo.Data
                                .generarLineaTextoComparendoEstandarSimit());   
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception($"No se ha podido consultar {ex}");
            }
        }

        /*
        public async Task<List<ComparendoSimitDto>> listarRangoComparendos(
            FilterComparendoDTO filterComparendoDTO)
        {
            var listaComparendos = await _comparendoRepository.getFilter(
                filterComparendoDTO.numeroComparendoOmitir,
                filterComparendoDTO.numeroComparendosConsultar
                );
            return listaComparendos;
        }*/
    }
}