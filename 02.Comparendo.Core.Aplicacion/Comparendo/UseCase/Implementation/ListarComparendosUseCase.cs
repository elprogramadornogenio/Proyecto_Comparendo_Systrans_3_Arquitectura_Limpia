using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Query.DTOs;
using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Query.DTOs.Request;
using _02.Comparendo.Core.Aplicacion.Comparendo.Repositorio;
using _02.Comparendo.Core.Aplicacion.Comparendo.UseCase.Interfaces;
using _02.Comparendo.Core.Aplicacion.Utils;

namespace _02.Comparendo.Core.Aplicacion.Comparendo.UseCase.Implementation
{
    public class ListarComparendosUseCase : IListarComparendosUseCase
    {
        private readonly IComparendoRepository _comparendoRepository;
        private FilterComparendoRequestDto? filterComparendoRequestDto;
        private const int _cantidadMaximaComparendosAdmitidosPorSimit = 20000;

        public ListarComparendosUseCase(
            IComparendoRepository comparendoRepository
        )
        {
            _comparendoRepository = comparendoRepository;
        }
        public async Task<Response<IEnumerable<ComparendoEstandarSimitDto>>> listarComparendosValidation(
            FilterComparendoRequestDto resquest)
        {
            filterComparendoRequestDto = resquest;
            esNullFiltroComparendo();
            validarCantidadClavesPrimariasConsultarComparendos();
            await verificarExistenciaClavesPrimariasComparendo();
            await verificarExistenciaCodigosInfraccion();
            throw new NotImplementedException();
        }

        private void esNullFiltroComparendo() {
            if(
                filterComparendoRequestDto == null || 
                filterComparendoRequestDto.IdentificadoresUnicosComparendos == null ||
                filterComparendoRequestDto.IdentificadoresUnicosComparendos.Count == 0
                )
                throw new Exception("Enviar al menos un comparendo para poder generar plano SIMIT");
            
        }

        private void validarCantidadClavesPrimariasConsultarComparendos()
        {
            var cantidadClavesPrimariasComparendos = filterComparendoRequestDto!
                .IdentificadoresUnicosComparendos!.Count;
            if(cantidadClavesPrimariasComparendos > _cantidadMaximaComparendosAdmitidosPorSimit)
                throw new Exception($"No se puede enviar más de {_cantidadMaximaComparendosAdmitidosPorSimit} registros para generar plano SIMIT");
        }

        private async Task verificarExistenciaClavesPrimariasComparendo()
        {
            var comparendosRequestDtos = filterComparendoRequestDto!
                .IdentificadoresUnicosComparendos!;
            foreach (var comparendoRequest in comparendosRequestDtos)
            {
                if(! await _comparendoRepository.existeComparendoPorId(comparendoRequest.Id))
                    throw new Exception($"La información proporcionada no es válida");
            }
        }

        private async Task verificarExistenciaCodigosInfraccion()
        {
            
        }
    }
}