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

            using var transaccionCrearComparendoCompleto = _contexto.Database.BeginTransaction();
            try
            {
                // solo para obtener datos
                var datosAgenteTransito = await _comparendoAgenteTransitoRespository
                        .existeAgenteTransitoPorPlaca(placaAgente);

                var datosTipoInfraccion = await _comparendoTipoInfraccionRepository
                            .traerInfraccionPorCodigo(codigoInfraccion);

                if (datosAgenteTransito == null)
                {
                    var agenteIdentificadoPorSoloPlaca = _mapper
                        .Map<ComparendoAgenteTransito>(new CrearAgentePorSoloPlacaCommand { Placa = placaAgente });
                    datosAgenteTransito = await _comparendoAgenteTransitoRespository
                        .crearAgenteIndeterminadoConPlaca(agenteIdentificadoPorSoloPlaca);
                }
                var datosComparendoCreado = await crearComparendo(datosComparendo);
                // crear relacion de un agente a muchos comparendos
                datosAgenteTransito.Comparendos!.Add(datosComparendoCreado);

                var datosRelacionComparendoTipoInfraccion =
                    await _comparendoInfraccionComparendoRepository
                        .crearRelacionComparendoTipoInfraccion(
                            datosComparendoCreado,
                            datosTipoInfraccion,
                            valorInfraccion
                        );

                await consultaCompletada();
                await transaccionCrearComparendoCompleto.CommitAsync();
                return datosComparendoCreado.Id;
            }
            catch (Exception ex)
            {
                await transaccionCrearComparendoCompleto.RollbackAsync();
                if (ex.InnerException != null)
                {
                    throw new Exception($"El comparendo no se ha podido crear debido a un error interno: {ex.InnerException.Message}");
                }
                else
                {
                    throw new Exception($"El comparendo no se ha podido crear por {ex.Message}");
                }
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
            return (existeClaseServicio != null) ? true : false;
        }

        public async Task<bool> existeComparendoEstadoPorId(int? id)
        {
            var existeEstadoComparendo = await _contexto
                .ComparendoEstado!.FindAsync(id);
            return (existeEstadoComparendo != null) ? true : false;
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
            return (existeTipoVehiculo != null) ? true : false;
        }

        public async Task<SecretariaTransito> traerSecretariaTransito(
            string codigoSecretariaTransito)
        {
            var secretariaTransito = await _contexto.SecretariaTransito!
                .FirstOrDefaultAsync(secretariaTransito => 
                    secretariaTransito.Codigo!.Equals(codigoSecretariaTransito));
            return secretariaTransito!;
        }

        public async Task<Ciudad> traerCiudadComparendo(string codigoCiudad)
        {
            var ciudad = await _contexto.Ciudad!
                .FirstOrDefaultAsync(ciudad => ciudad.Codigo!.Equals(codigoCiudad));
            return ciudad!;
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

        public async Task<Comparendos> traerComparendoPorNumeroYCodigoInfraccion(
            int tipoDocumentoInfractor,
            string documentoInfractor,
            string numeroComparendo,
            string codigoInfraccion)
        {
            var comparendo = await _contexto.Comparendo!
                .Where(comparendo =>
                    comparendo.InfractorTipoDocumentoId == tipoDocumentoInfractor &&
                    comparendo.DocumentoInfractor.Equals(documentoInfractor) &&
                    comparendo.Numero.Equals(numeroComparendo) &&
                    comparendo.ComparendoInfraccionComparendos!
                    .Any(comparendoInfraccionComparendo => comparendoInfraccionComparendo
                        .ComparendoTipoInfraccion.Codigo.Equals(codigoInfraccion))
                )
                .Include(comparendo => comparendo.ComparendoInfraccionComparendos!)
                .ThenInclude(comparendoInfraccionComparendo =>
                    comparendoInfraccionComparendo.ComparendoTipoInfraccion)
                .FirstOrDefaultAsync();
            return comparendo!;
        }

        
    }
}