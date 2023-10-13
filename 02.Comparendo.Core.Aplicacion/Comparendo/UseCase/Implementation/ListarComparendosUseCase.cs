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
        private readonly IComparendoTipoInfraccionRepository _tipoInfraccionRepository;
        private ComparendoRequestDto? comparendoRequestDto;

        public ListarComparendosUseCase(
            IComparendoRepository comparendoRepository,
            IComparendoTipoInfraccionRepository tipoInfraccionRepository
        )
        {
            _comparendoRepository = comparendoRepository;
            _tipoInfraccionRepository = tipoInfraccionRepository;
        }
        public async Task<Response<ComparendoEstandarSimitDto>> listarComparendosValidation(
            ComparendoRequestDto resquest)
        {
            comparendoRequestDto = resquest;
            try
            {
                esNullFiltroComparendo();
                await verificarExistenciaClavePrimaria();
                await verificarExistenciaCodigosInfraccion();
                return new Response<ComparendoEstandarSimitDto>(
                    await _comparendoRepository
                        .traerComparendoEstandarSimitPorId(
                            comparendoRequestDto!.Id,
                            comparendoRequestDto!.CodigoInfraccion
                    ), "Comparendo Listado Correctamente");
            }
            catch (Exception ex)
            {
                return new Response<ComparendoEstandarSimitDto>(ex.Message);
            }
            
            throw new NotImplementedException();
        }

        private void esNullFiltroComparendo() {
            if(comparendoRequestDto == null)
                throw new Exception("Comparendo No cumple los requisitos para ser incluido en Simit");
            
        }

        private async Task verificarExistenciaClavePrimaria()
        {
            if(! await _comparendoRepository.existeComparendoPorId(comparendoRequestDto!.Id))
                throw new Exception($"No existe comparendo");
            
        }

        private async Task verificarExistenciaCodigosInfraccion()
        {
            if(comparendoRequestDto!.CodigoInfraccion == null)
                throw new Exception($"Comparendo NO cumple los requisitos para generar plano"); 
            if(! await _tipoInfraccionRepository
                .existeInfraccion(comparendoRequestDto!.CodigoInfraccion))
                throw new Exception($"No existe infracción del comparendo");
        }
    }
}