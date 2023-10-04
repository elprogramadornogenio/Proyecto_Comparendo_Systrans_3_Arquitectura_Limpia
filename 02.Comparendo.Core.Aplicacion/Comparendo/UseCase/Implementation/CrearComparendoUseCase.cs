using System.Globalization;
using _01.Comparendo.Dominio.Comparendos.Models;
using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Command.Commands;
using _02.Comparendo.Core.Aplicacion.Comparendo.Repositorio;
using _02.Comparendo.Core.Aplicacion.Comparendo.UseCase.Interfaces;
using _02.Comparendo.Core.Aplicacion.Comparendo.Utils;
using AutoMapper;

namespace _02.Comparendo.Core.Aplicacion.Comparendo.UseCase.Implementation
{
    
    public class CrearComparendoUseCase : ICrearComparendoUseCase
    {
        private readonly IComparendoRepository _comparendoRepository;
        private readonly IComparendoTipoInfraccionRepository _comparendoTipoInfraccionRespository;
        private readonly IMapper _mapper;
        private CrearComparendoCommand _comparendoTransito;

        public CrearComparendoUseCase(
            IComparendoRepository comparendoRepository,
            IComparendoTipoInfraccionRepository comparendoTipoInfraccionRepository,
            IMapper mapper
            )
        {
            _comparendoRepository = comparendoRepository;
            _comparendoTipoInfraccionRespository = comparendoTipoInfraccionRepository;
            _mapper = mapper;
            _comparendoTransito = new CrearComparendoCommand();
        }
        public async Task<Response<Guid>> crearComparendoValidation(CrearComparendoCommand datosCrearComparendoRequest)
        {
            _comparendoTransito = datosCrearComparendoRequest;

            try
            {
                esNullComparendo();
                /*
                    validar campos obligatorios para insertar comparendo en Systrans
                    si hay campos que no son obligatorios en Simit pero son obligatorios
                    en Systrans los autocompleta.
                */
                await existeNumeroComparendo(); 
                // numero comparendo, documento infractor y codigo de infraccion
                await existeCodigoInfraccion();
                await existeTipoVehiculo();
                await existeClaseServicio();
                existeTipoDocumentoInfractor(); // esta funcion debe consultar a base datos
                validarDocumentoInfractor(); // de acuerdo con la consulta tipo documento validar reglas de documento identidad
                existeCodigoSecretaria(); // validar codigo de secretaria en base de datos
                await existeEstadoComparendo();
                validarDireccionMunicipio(); // validar direccion del municipio
                validarFechaComparendo(); // puede ir en la capa 04 validations
                validarHoraComparendo(); // puede ir en la capa 04 validations
                existeGradoAlcohol(); // validar los grados de alcoholemia si estan registrados en una tabla de base de datos 
                existeFuenteComparendo(); // validar desde base de datos si existe fuente comparendo
                return new Response<Guid>(
                    await _comparendoRepository.agregarComparendoConDependencias(
                        _mapper.Map<Comparendos>(_comparendoTransito),
                        _comparendoTransito.CompPlacaAgente!,
                        _comparendoTransito.ComInfraccion!,
                        _comparendoTransito.ComValInfra
                        ), "Comparendo Creado Correctamente");
            }
            catch (Exception ex)
            {
                return new Response<Guid>(ex.Message);
            }
        }

        private void esNullComparendo()
        {
            if(_comparendoTransito == null)
            {
                throw new Exception("El comparendo es null");
            }
            
        }

        private async Task existeNumeroComparendo()
        {
            if(await _comparendoRepository
                .existeComparendoPorNumero(_comparendoTransito.ComNumero!))
            {
                throw new Exception("El numero de comparendo ya existe");
            }
                 // no guardar comparendo
        }

        private async Task existeCodigoInfraccion()
        {
            if(!await _comparendoTipoInfraccionRespository
                .existeInfraccion(_comparendoTransito.ComInfraccion!))
                throw new Exception("El tipo de infraccion de comparendo No existe");
        }

        private async Task existeTipoVehiculo()
        {
            if(! await _comparendoRepository
                .existeTipoVehiculoPorId(_comparendoTransito.ComTipoVehi))
            {
                _comparendoTransito.ComTipoVehi = 99;
            }
        }

        private async Task existeClaseServicio()
        {
            if(! await _comparendoRepository
                .existeClaseServicioPorId(_comparendoTransito.ComTipoSer))
            {
                _comparendoTransito.ComTipoSer = 9;
            }
        }

        private void existeTipoDocumentoInfractor()
        {
            // esta funcion debe consultar directamente a base de datos
            if(_comparendoTransito.ComTipoInfrac < 0
                || _comparendoTransito.ComTipoInfrac > 10)
            {
                _comparendoTransito.ComTipoInfrac = 1;
            }
                
        }

        private void validarDocumentoInfractor()
        {
            // de acuerdo con el tipo de documento validar las reglas
            if(string.IsNullOrEmpty(_comparendoTransito.ComInfractor))
                _comparendoTransito.ComInfractor = "Documento No definido";
        }

        private void existeCodigoSecretaria()
        {
            // esta funcion debe consultar directamente a base de datos
            if(_comparendoTransito.ComOrganismo < 0)
                throw new Exception("El Codigo del organismo no existe");
        }
        
        private async Task existeEstadoComparendo()
        {
            if(!await _comparendoRepository
                .existeComparendoEstadoPorId(_comparendoTransito.ComEstadoCom))
                throw new Exception("El estado comparendo no existe");
        }

        private void validarDireccionMunicipio()
        {
            if(_comparendoTransito.ComDivipoMuni < 0
                || _comparendoTransito.ComDivipoMuni > 99999999)
                _comparendoTransito.ComDivipoMuni = 0; // se almacena la direccion 0
        }

        private void validarFechaComparendo()
        {
            DateTime fecha;
            if(!DateTime.TryParse(_comparendoTransito.ComFecha, out fecha) && 
                fecha > DateTime.UtcNow)
                throw new Exception("La fecha no es valida");
        }

        private void validarHoraComparendo()
        {
            DateTime hora;
            if(!DateTime.TryParseExact(_comparendoTransito.ComHora,
                "HHmm", CultureInfo.InvariantCulture, DateTimeStyles.None, out hora))
                throw new Exception("La Hora no es valida");
        }

        private void existeGradoAlcohol()
        {
            // validar grados de alcohol base de datos
            if(_comparendoTransito.GradoAlcohol < 0
                || _comparendoTransito.GradoAlcohol > 4)
                _comparendoTransito.GradoAlcohol = 0;
        }

        private void existeFuenteComparendo()
        {
            // validar fuente comparendo base de datos
            if(_comparendoTransito.FuenteComparendo == null ||
                _comparendoTransito.FuenteComparendo < 0 || 
                _comparendoTransito.FuenteComparendo > 2)
                _comparendoTransito.FuenteComparendo = 0;
        }
    }
}