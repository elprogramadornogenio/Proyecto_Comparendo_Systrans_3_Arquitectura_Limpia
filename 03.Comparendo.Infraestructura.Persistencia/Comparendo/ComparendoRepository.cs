using System.Transactions;
using _01.Comparendo.Dominio.Comparendos.Models;
using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Command.Commands;
using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Query.DTO;
using _02.Comparendo.Core.Aplicacion.Comparendo.Repositorio;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace _03.Comparendo.Infraestructura.Persistencia.Comparendo
{
    public class ComparendoRepository : IComparendoRepository
    {
        private readonly DataContext _contexto;
        private readonly IMapper _mapper;
        private readonly IComparendoAgenteTransitoRepository _comparendoAgenteTransitoRespository;
        private readonly IComparendoTipoInfraccionRepository _comparendoTipoInfraccionRepository;
        private readonly IComparendoInfraccionComparendoRepository _comparendoInfraccionComparendoRepository;
        public ComparendoRepository(
            DataContext contexto, 
            IMapper mapper,
            IComparendoAgenteTransitoRepository comparendoAgenteTransitoRespository,
            IComparendoTipoInfraccionRepository comparendoTipoInfraccionRepository,
            IComparendoInfraccionComparendoRepository comparendoInfraccionComparendoRepository
            )
        {
            _contexto = contexto;
            _mapper = mapper;
            _comparendoAgenteTransitoRespository = comparendoAgenteTransitoRespository;
            _comparendoTipoInfraccionRepository = comparendoTipoInfraccionRepository;
            _comparendoInfraccionComparendoRepository = comparendoInfraccionComparendoRepository;
        }

        public async Task<Guid> agregarComparendoConDependencias(
            Comparendos datosComparendo, 
            string placaAgente,
            string codigoInfraccion,
            decimal valorInfraccion
            )
        {
            
            try
            {
                // solo para obtener datos
                var datosAgenteTransito = await _comparendoAgenteTransitoRespository
                        .existeAgenteTransitoPorPlaca(placaAgente);
                var datosTipoInfraccion = await _comparendoTipoInfraccionRepository
                            .traerInfraccionPorCodigo(codigoInfraccion);
                // inicia la transacción
                using(var transaccionGuardarComparendoDependecias = new TransactionScope())
                {
                    if(datosAgenteTransito == null)
                    {
                        var agenteIdentificadoPorSoloPlaca = _mapper
                            .Map<ComparendoAgenteTransito>(new CrearAgentePorSoloPlacaCommand{Placa = placaAgente});
                        datosAgenteTransito = await _comparendoAgenteTransitoRespository
                            .crearAgenteIndeterminadoConPlaca(agenteIdentificadoPorSoloPlaca);
                    }
                    var datosComparendoCreado = await crearComparendo(datosComparendo);
                    var datosRelacionComparendoTipoInfraccion = 
                        await _comparendoInfraccionComparendoRepository
                            .crearRelacionComparendoTipoInfraccion(
                                datosComparendoCreado,
                                datosTipoInfraccion,
                                valorInfraccion
                            );
                    
                    //await consultaCompletada();
                    //transaccionGuardarComparendoDependecias.Complete();
                    return datosComparendoCreado.Id;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"El comparendo no se ha podido crear por {ex.Message}");
            }
        }

        private async Task<Comparendos> crearComparendo(Comparendos datosComparendo)
        {
            await _contexto.Comparendo!.AddAsync(datosComparendo);
            return datosComparendo;
        }

        private async Task consultaCompletada()
        {
            await _contexto.SaveChangesAsync();
        }

        public async Task<bool> existeClaseServicioPorId(int? id)
        {
            var existeClaseServicio = await _contexto
                .ComparendoClaseServicio!.FindAsync(id);
            return (existeClaseServicio != null)? true: false;
        }

        public async Task<bool> existeComparendoEstadoPorId(int? id)
        {
            var existeEstadoComparendo = await _contexto
                .ComparendoEstado!.FindAsync(id);
            return (existeEstadoComparendo != null)? true: false;
        }

        public Task<bool> existeComparendoPorNumero(string numeroComparendo)
        {
            var existeComparendo = _contexto.Comparendo!
                .AnyAsync(comparendo => comparendo.Numero.Equals(numeroComparendo));
            return existeComparendo;
        }

        public async Task<bool> existeTipoVehiculoPorId(int? id)
        {
            var existeTipoVehiculo = await _contexto.ComparendoTipoVehiculo!.FindAsync(id);
            return (existeTipoVehiculo != null)? true: false;
        }


        // consultas de queries
        public async Task<List<ComparendoSimitDto>> getAll()
        {
            return await _contexto.Comparendo!
                .AsNoTracking()
                .ProjectTo<ComparendoSimitDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<ComparendoSimitDto>> getFilter(
            int numeroComparendoOmitir, int numeroComparendosConsultar)
        {
            return await _contexto.Comparendo!
                .Skip(numeroComparendoOmitir)
                .Take(numeroComparendosConsultar)
                .AsNoTracking()
                .ProjectTo<ComparendoSimitDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}