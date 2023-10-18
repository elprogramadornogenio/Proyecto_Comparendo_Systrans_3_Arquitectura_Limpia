using System.ComponentModel.DataAnnotations;
using _01.Comparendo.Dominio.Comparendos.Models;
using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Command.Commands;
using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Query.DTOs;
using _02.Comparendo.Core.Aplicacion.Comparendo.Repositorio;
using _02.Comparendo.Core.Aplicacion.Extensions;
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
                        .Map<ComparendoAgenteTransito>(new CrearAgentePorSoloPlacaCommand 
                        { Placa = placaAgente });
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
        // otro respositorio
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
        // otro repositorio
        public async Task<bool> existeTipoVehiculoPorId(int? id)
        {
            var existeTipoVehiculo = await _contexto.ComparendoTipoVehiculo!.FindAsync(id);
            return (existeTipoVehiculo != null) ? true : false;
        }
        // otro repositorio
        public async Task<SecretariaTransito> traerSecretariaTransito(
            string codigoSecretariaTransito)
        {
            var secretariaTransito = await _contexto.SecretariaTransito!
                .FirstOrDefaultAsync(secretariaTransito => 
                    secretariaTransito.Codigo!.Equals(codigoSecretariaTransito));
            return secretariaTransito!;
        }
        // otro repositorio
        public async Task<Ciudad> traerCiudadComparendo(string codigoCiudad)
        {
            var ciudad = await _contexto.Ciudad!
                .FirstOrDefaultAsync(ciudad => ciudad.Codigo!.Equals(codigoCiudad));
            return ciudad!;
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

        public async Task<bool> existeComparendoPorId(Guid? idComparendo)
        {
            return await _contexto.Comparendo!.FindAsync(idComparendo) != null;
        }

        public Task<IEnumerable<ComparendoEstandarSimitDto>> traerComparendosEstandarSimit(
            List<Guid> llavesPrimariasComparendos)
        {
            throw new Exception();
        }

        public async Task<ComparendoEstandarSimitDto> traerComparendoEstandarSimitPorId(
            Guid? idComparendo, 
            string? codigoInfraccion
            )
        {
            var comparendoEstandarSimit = await _contexto.Comparendo!
                .Where(comparendo => comparendo.Id == idComparendo &&
                    comparendo.ComparendoInfraccionComparendos!
                    .Any(comparendoInfraccionComparendo => comparendoInfraccionComparendo
                        .ComparendoTipoInfraccion.Codigo.Equals(codigoInfraccion)
                ))
                .Include(comparendo => comparendo.AgenteTransito) // incluimos en agente transito
                .Include(comparendo => comparendo.ComparendoInfraccionComparendos!)
                .ThenInclude(comparendoInfraccionComparendo => 
                    comparendoInfraccionComparendo.ComparendoTipoInfraccion) // infracción
                .Include(comparendo => comparendo.LicenciaConduccionSecretaria) //secretaria licencia conduccion
                .Include(comparendo => comparendo.SecretariaTransito)
                .Include(comparendo => comparendo.Ciudad) // municipioDireccionId
                .Include(comparendo => comparendo.SecretariaTransitoMatriculado) // secretaria de matricula
                .Include(comparendo => comparendo.SecretariaLicenciaTransito) // secretaria licencia transito
                .Include(comparendo => comparendo.CiudadDelInfractor)
                // Esto lo comento es por fines educativos como tal este select es remplazado
                // por ProjectTo<ComparendoEstandarSimitDto>(_mapper.ConfigurationProvider)
                /*
                .Select(comparendo => new ComparendoEstandarSimitDto 
                {
                    //--------------------DATOS BÁSICOS DEL COMPARENDO ---------------------------//
                    ComNumero = comparendo.Numero,
                    ComFecha = comparendo.Fecha.convertirFechaCadena(),
                    ComHora = comparendo.Hora.convertirHoraCadena(),
                    ComDir = comparendo.Direccion,
                    ComDivipoMuni = (comparendo.Ciudad != null) ? 
                        comparendo.Ciudad!.Codigo.convertirCadenaEntero() : 0,
                    ComLocalidadComuna = comparendo.Localidad,
                    ComPlaca = comparendo.Placa,
                    ComDivipoMatri = (comparendo.SecretariaTransitoMatriculado != null) ?  
                        comparendo.SecretariaTransitoMatriculado!.Codigo.convertirCadenaEntero(): 0,
                    ComTipoSer = comparendo.ClaseServicioId,
                    ComCodigoRadio = (comparendo.CodigoRadio != null) ? 
                        (int?)comparendo.CodigoRadio : null,
                    ComCodigoModalidad = (comparendo.CodigoModalidad != null) ? 
                        (int?)comparendo.CodigoModalidad: null,
                    ComCodigoPasajeros = (comparendo.CodigoPasajeros != null) ? 
                        (int?) comparendo.CodigoPasajeros: null,
                    //--------------------DATOS BÁSICOS DEL INFRACTOR ----------------------------//
                    ComInfraccion = comparendo.DocumentoInfractor,
                    ComTipoDoc = comparendo.InfractorTipoDocumentoId,
                    ComNombre = comparendo.NombreInfractor,
                    ComApellido = comparendo.ApellidoInfractor,
                    ComEdadInfractor = (comparendo.EdadInfractor != 0) ? comparendo.EdadInfractor: null,
                    ComDirInfractor = comparendo.DireccionInfractor,
                    ComEmail = comparendo.EmailInfractor,
                    ComTeleInfractor = comparendo.TelefonoInfractor,
                    ComIdCiudad = (comparendo.CiudadDelInfractor != null) ? 
                        comparendo.CiudadDelInfractor.Codigo.convertirCadenaEntero(): null,
                    ComLicencia = comparendo.LicenciaConduccion,
                    ComCategoria = comparendo.LicenciaConduccionCategoria,
                    ComSecreExpide = (comparendo.LicenciaConduccionSecretaria != null) ? 
                        comparendo.LicenciaConduccionSecretaria.Codigo.convertirCadenaEntero() : null,
                    ComFechaVence = comparendo.LicenciaVence.convertirFechaCadena(),
                    ComTipoInfrac = comparendo.TipoInfractorId,
                    CompLicTransito = comparendo.LicenciaTransito,
                    ComDivipoLicen = (comparendo.SecretariaLicenciaTransito != null)? 
                        comparendo.SecretariaLicenciaTransito.Codigo.convertirCadenaEntero(): null,
                    //--------------------DATOS BÁSICOS DEL PROPIETARIO ----------------------------//
                    ComIdentificacion = comparendo.DocumentoPropietario,
                    ComIdTipoDocProp = comparendo.TipoDocumentoPropietarioId,
                    ComNombreProp =  $"{comparendo.NombrePropietario} {comparendo.ApellidoPropietario}",
                    //--------------------DATOS BÁSICOS DE LA EMPRESA Y TARJETA OPERACION ----------//
                    ComNombreEmpresa = comparendo.NombreEmpresa,
                    ComNitEmpresa = comparendo.NitEmpresa,
                    ComTarjetaOperacion = comparendo.TarjetaOperacion,
                    //--------------------DATOS BÁSICOS DEl AGENTE ---------------------------------//
                    CompPlacaAgente = (comparendo.AgenteTransito != null) ? 
                        comparendo.AgenteTransito.Placa : null,
                    //--------------------DATOS AVANZADOS DEL COMPARENDO ---------------------------//
                    CompObservaciones = comparendo.Observaciones,
                    ComFuga = comparendo.Fuga.convertirBooleanCadena(),
                    ComAcci = comparendo.Accidente.convertirBooleanCadena(),
                    //--------------------DATOS AVANZADOS DEL COMPARENDO INMOVILIZACION ------------//
                    ComInmov = comparendo.Inmobilizacion.convertirBooleanCadena(),
                    ComPatioInmoviliza = comparendo.PatioInmoviliza,
                    ComDirPatioInmovi = comparendo.DireccionPatioInmoviliza,
                    ComGruaNumero = comparendo.GruaNumero,
                    ComPlacaGrua = comparendo.GruaPlaca,
                    ComConsecutInmovi = comparendo.ConsecutivoInmovilizacion,
                    //--------------------DATOS DEL TESTIGO -----------------------------------------//
                    ComIdentificacionTest = comparendo.DocumentoTestigo,
                    ComNombreTesti = $"{comparendo.NombreTestigo} {comparendo.ApellidoTestigo}",
                    ComDirecResTesti = comparendo.DireccionTestigo,
                    ComTeleTestigo = comparendo.TelefonoTestigo,
                    //--------------------DATOS DEL DINERO MONEY DEL COMPARENDO --------------------//
                    ComValor = comparendo.ValorComparendo,
                    ComValAd = comparendo.ValorAdicional,
                    ComOrganismo = (comparendo.SecretariaTransito != null)? 
                        comparendo.SecretariaTransito.Codigo.convertirCadenaEntero(): 0,
                    ComEstadoCom = comparendo.EstadoComparendoId,
                    //----------------------------DATOS EXTRA DEL COMPARENDO--------------------------//
                    ComPolca = comparendo.Polca.convertirBooleanCadena(),
                    ComInfractor = comparendo.ComparendoInfraccionComparendos!.FirstOrDefault()!
                        .ComparendoTipoInfraccion.Codigo,
                    ComValInfra = comparendo.ComparendoInfraccionComparendos!.FirstOrDefault()!
                        .ValorInfraccion,
                    //----------------------------DATOS DEL TUTOR-------------------------------------//
                    Id_Tipo_Doc_Tutor = null,
                    Nro_Doc_Tutor = null,
                    Nombre_Tutor = null,
                    Apellido_Tutor = null,
                    //----------------------------DATOS COMPARENDO ELECTRÓNICO------------------------//
                    FotoMulta = comparendo.ComparendoElectronico.convertirBooleanCadena(),
                    FechaNotificacion = (comparendo.FechaNotificacion != null) ? 
                        comparendo.FechaNotificacion.convertirFechaCadena(): null,
                    FuenteComparendo = (comparendo.Fuente != null)? (int?) comparendo.Fuente: null,
                    LatitudComparendo = comparendo.Latitud,
                    LongitudComparendo = comparendo.Longitud
                })
                */
                .AsNoTracking()
                .ProjectTo<ComparendoEstandarSimitDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return comparendoEstandarSimit!;
        }
    }
}