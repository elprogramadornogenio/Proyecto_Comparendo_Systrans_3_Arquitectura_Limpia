using _01.Comparendo.Dominio.Comparendos.Models;
using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Command.Commands;
using _02.Comparendo.Core.Aplicacion.Comparendo.Repositorio;
using _02.Comparendo.Core.Aplicacion.Comparendo.UseCase.Interfaces;
using _02.Comparendo.Core.Aplicacion.Extensions;
using _02.Comparendo.Core.Aplicacion.Utils;
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
        public async Task<Response<Guid>> crearComparendoValidation(
            CrearComparendoCommand datosCrearComparendoRequest)
        {
            _comparendoTransito = datosCrearComparendoRequest;

            try
            {
                esNullComparendo();
                
                /*
                    1. Validar los datos obligatorios en simit y systrans 
                    (estos datos no pueden ser generados y si no se cumplen
                    lanzará una excepción)
                */

                /*
                    la funcion existeTipoDocumentoInfractor debe consultar a base datos 
                    si existe documento en las tablas
                */
                existeTipoDocumentoInfractor(); // falta información en base de datos
                /*
                    de acuerdo al tipo de documento definir reglas de acuerdo a simit
                    y para esto cada tipo de documento en base de datos debe tener sus reglas
                    definidas para esto se define la funcion validarDocumentoInfractor para
                    hacer cumplir esas reglas de lo contrario lanza una excepción
                */
                validarDocumentoInfractor();
                await existeCodigoInfraccion();
                /*
                    De acuerdo a que las secretarias de tránsito pueden reutilizar numero
                    de comparendo para que se produzca un comparendo Repetido se tienen
                    presente los siguientes campos:
                    Tipo de documento Infractor
                    número documento Infractor
                    Numero de comparendo
                    Tipo de Infracción
                    Si todos los campos se repiten el comparendo se repite y por lo tanto
                    lanza una excepción
                */
                await existeComparendoRepetido();
                await existeCodigoSecretariaQueReportaComparendo(); 
                await existeEstadoComparendo();
                /*
                    2. Validar los datos obligatorios en Systrans pero NO en Simit,
                    como no son obligatorios en Simit en el momento de importarlos
                    deben ser autogenerados para colocar información en los campos
                    obligatorios en Systrans no se puede generar una excepción porque
                    deben ser guardados.
                */
                await existeTipoVehiculo();
                await existeClaseServicio(); // falta información en base de datos
                await validarYGenerarDivipoMuni(); // falta información en base de datos
                validarYGenerarHoraComparendo();
                validarYGenerarDireccionComparendo();
                /*
                    es necesario consultar base de datos si existe el
                    divipo Municipio de lo contrario de lo contrario
                    colocar el valor por defecto
                */
                
                validarYGenerarEdadInfractor();
                validarYDefinirPlacaAgente();
                validarYDefinirFuga();
                validarYDefinirAccidente();
                validarYDefinirInmovilizacion();
                existeFuenteComparendo();
                existeGradoAlcohol();


                /*
                    3. Validar datos que no son obligatorios pero que tiene relaciones
                    con otras tablas
                
                */
                await validarYGenerarSecretariaDondeSeEncuentraMatriculadoVehiculo();
                await validarYGenerarSecretariaQueExpideLicenciaConduccion();
                await validarYGenerarSecretariaQueExpideLicenciaTransito();
                await validarYGenerarCiudadDelInfractor();


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
                throw new Exception("El comparendo es null");
            
        }

        private void existeTipoDocumentoInfractor()
        {
            // esta funcion debe consultar directamente a base de datos
            if(_comparendoTransito.ComTipoDoc < 0
                || _comparendoTransito.ComTipoDoc > 10)
                throw new Exception("El tipo de Documento NO definido");
                
        }

        private void validarDocumentoInfractor()
        {
            if(string.IsNullOrEmpty(_comparendoTransito.ComInfractor))
                throw new Exception("Documento NO definido");
        }

        private async Task existeCodigoInfraccion()
        {
            if(!await _comparendoTipoInfraccionRespository
                .existeInfraccion(_comparendoTransito.ComInfraccion!))
                throw new Exception("El tipo de infraccion de comparendo No existe");
        }

        private async Task existeComparendoRepetido()
        {
            var comparendo = await _comparendoRepository
                .traerComparendoPorNumeroYCodigoInfraccion(
                    (int)_comparendoTransito.ComTipoDoc!,
                    _comparendoTransito.ComInfractor!,
                    _comparendoTransito.ComNumero!, 
                    _comparendoTransito.ComInfraccion!);

            if(comparendo != null)
                throw new Exception("El comparendo con numero e infracción ya existe");
                 // no guardar comparendo
        }

        private async Task existeCodigoSecretariaQueReportaComparendo()
        {
            //  secretariaTransto NO REPORTADA tiene código 99999999 con Id 162
            if(_comparendoTransito.ComOrganismo == null)
            {
                _comparendoTransito.ComOrganismo = 162;
                return;
            }
            var secretariaTransito = await _comparendoRepository
                .traerSecretariaTransito(_comparendoTransito.ComOrganismo.ToString()!);
            // id de secretariaTransito NO EXISTE
            if(secretariaTransito == null)
                _comparendoTransito.ComOrganismo = 162;
            else
                _comparendoTransito.ComOrganismo = secretariaTransito.Id;
        }

        private async Task existeEstadoComparendo()
        {
            if(!await _comparendoRepository
                .existeComparendoEstadoPorId(_comparendoTransito.ComEstadoCom))
                throw new Exception("El estado comparendo no existe");
        }

        private async Task existeTipoVehiculo()
        {
            if(! await _comparendoRepository
                .existeTipoVehiculoPorId(_comparendoTransito.ComTipoVehi))
            {
                _comparendoTransito.ComTipoVehi = 99;
                // 99 significa que tipo de vehículo no fue reportado
            }
        }

        private async Task existeClaseServicio()
        {
            if(! await _comparendoRepository
                .existeClaseServicioPorId(_comparendoTransito.ComTipoSer))
            {
                //_comparendoTransito.ComTipoSer = 9;
                // hay que reportar los tipos de vehiculo 9 para decir no reportado
                _comparendoTransito.ComTipoSer = 3;
                // he dejado por defecto 3 (servicio particular) puesto que 9
                // no está reportado en bases de datos.
            }
        }

        private async Task validarYGenerarDivipoMuni()
        {
            // como aparece como NO REPORTADA tiene código 99999999 con Id 162
            if(_comparendoTransito.ComDivipoMuni == null)
            {
                _comparendoTransito.ComDivipoMuni = 162;
                return;
            }
            var direccionDivipoMuni = await _comparendoRepository
                .traerCiudadComparendo(_comparendoTransito.ComDivipoMuni.ToString()!);
            // direccion divipo transito NO EXISTE
            if(direccionDivipoMuni == null)
                _comparendoTransito.ComDivipoMuni = 162;
            else
                _comparendoTransito.ComDivipoMuni = direccionDivipoMuni.Id;
        }

        private void validarYGenerarHoraComparendo()
        {
            if(_comparendoTransito.ComHora.convertirCadenaHora() == null)
                _comparendoTransito.ComHora = "0000"; // representa la hora por default en formato simit 00:00
                
        }

        private void validarYGenerarDireccionComparendo()
        {
            if(_comparendoTransito.ComDir == null)
                _comparendoTransito.ComDir = "";
        }

        

        private void validarYGenerarEdadInfractor()
        {
            if(_comparendoTransito.ComEdadInfractor == null)
                _comparendoTransito.ComEdadInfractor = 0;
        }

        private void validarYDefinirPlacaAgente()
        {
            /*
                Como en Simit el valor de la placa es obligatoria si
                y solo si el comparendo es Polca hay que validar si la placa es
                obligatoria y lanzará una excepción, de lo contrario
                si el comparendo no es polca simit puede o no enviar la placa del agente
                y como en Systrans es obligatorio insertar la placa del agente
                va a generar una placa por default que es 123
            */
            if(_comparendoTransito.ComPolca.convertirCadenaBoolean()) 
            {
                if(_comparendoTransito.CompPlacaAgente == null)
                    throw new Exception("La placa del agente es obligatoria");
            }
            
            if(_comparendoTransito.CompPlacaAgente == null)
                _comparendoTransito.CompPlacaAgente = "999";
                
        }

        private void validarYDefinirFuga()
        {
            if(_comparendoTransito.ComFuga == null)
                _comparendoTransito.ComFuga = 'N';
        }

        private void validarYDefinirAccidente()
        {
            if(_comparendoTransito.ComAcci == null)
                _comparendoTransito.ComAcci = 'N';
        }

        private void validarYDefinirInmovilizacion()
        {
            if(_comparendoTransito.ComInmov == null)
                _comparendoTransito.ComInmov = 'N';
        }

        private void existeFuenteComparendo()
        {
            // validar fuente comparendo base de datos
            if(_comparendoTransito.FuenteComparendo == null ||
                _comparendoTransito.FuenteComparendo < 0 || 
                _comparendoTransito.FuenteComparendo > 2)
                _comparendoTransito.FuenteComparendo = 0;
        }


        private void existeGradoAlcohol()
        {
            // validar grados de alcohol base de datos
            if(_comparendoTransito.GradoAlcohol < 0
                || _comparendoTransito.GradoAlcohol > 4)
                _comparendoTransito.GradoAlcohol = 0;
        }

        private async Task validarYGenerarSecretariaDondeSeEncuentraMatriculadoVehiculo()
        {
            if(_comparendoTransito.ComDivipoMatri == null)
            {
                return;
            }
            var direccionSecretariaMatriculaVehiculo = await _comparendoRepository
                .traerSecretariaTransito(_comparendoTransito.ComDivipoMatri.ToString()!);
            // direccion divipo transito NO EXISTE la reemplaza por soacha que tiene codigo 25754000 y Id 155
            if(direccionSecretariaMatriculaVehiculo == null)
                _comparendoTransito.ComDivipoMatri = 155;
            else
                _comparendoTransito.ComDivipoMatri = direccionSecretariaMatriculaVehiculo.Id;
        }

        private async Task validarYGenerarSecretariaQueExpideLicenciaConduccion()
        {
            // como no aparece un registro de NO REPORTADA se coloca soacha por defecto
            if(_comparendoTransito.ComSecreExpide == null)
            {
                return;
            }
            var direccionSecretariaExpideLicenciaConduccion = await _comparendoRepository
                .traerSecretariaTransito(_comparendoTransito.ComSecreExpide.ToString()!);
            // direccion divipo transito NO EXISTE la reemplaza por soacha que tiene codigo 25754000 y Id 155
            if(direccionSecretariaExpideLicenciaConduccion == null)
                _comparendoTransito.ComSecreExpide = 155;
            else
                _comparendoTransito.ComSecreExpide = direccionSecretariaExpideLicenciaConduccion.Id;
        }

        private async Task validarYGenerarSecretariaQueExpideLicenciaTransito()
        {
            // como no aparece un registro de NO REPORTADA se coloca soacha por defecto
            if(_comparendoTransito.ComDivipoLicen == null)
            {
                return;
            }
            var direccionSecretariaExpideLicenciaTransito = await _comparendoRepository
                .traerSecretariaTransito(_comparendoTransito.ComDivipoLicen.ToString()!);
            // direccion divipo transito NO EXISTE la reemplaza por soacha que tiene codigo 25754000 y Id 155
            if(direccionSecretariaExpideLicenciaTransito == null)
                _comparendoTransito.ComDivipoLicen = 155;
            else
                _comparendoTransito.ComDivipoLicen = direccionSecretariaExpideLicenciaTransito.Id;
        }

        private async Task validarYGenerarCiudadDelInfractor()
        {
            // como no aparece un registro de NO REPORTADA se coloca soacha por defecto
            if(_comparendoTransito.ComIdCiudad == null)
            {
                return;
            }
            var direccionCiudadInfractor = await _comparendoRepository
                .traerCiudadComparendo(_comparendoTransito.ComIdCiudad.ToString()!);
            // direccion divipo transito NO EXISTE la reemplaza por soacha que tiene codigo 25754000 y Id 2
            if(direccionCiudadInfractor == null)
                _comparendoTransito.ComIdCiudad = 2;
            else
                _comparendoTransito.ComIdCiudad = direccionCiudadInfractor.Id;
        }
    }
}