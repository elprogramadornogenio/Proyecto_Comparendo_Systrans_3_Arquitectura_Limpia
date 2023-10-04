using _01.Comparendo.Dominio.Comparendos.enums;
using _01.Comparendo.Dominio.Comparendos.Models;
using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Command.Commands;
using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Query.DTO;
using _04.Comparendo.Infraestructura.Extensions;
using AutoMapper;

namespace _04.Comparendo.Infraestructura.Comparendo.Mapping
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<Comparendos, ComparendoSimitDto>()
                .ForMember(ComparendoEstructuraSimit => 
                    ComparendoEstructuraSimit.ComCodigoRadio,
                    comparendo => comparendo
                    .MapFrom(comparendoOrigen => (int?) comparendoOrigen
                    .CodigoRadio))
                .ForMember(ComparendoEstructuraSimit => 
                    ComparendoEstructuraSimit.ComCodigoModalidad,
                    comparendo => comparendo
                    .MapFrom(comparendoOrigen => (int?) comparendoOrigen
                    .CodigoModalidad))
                .ForMember(ComparendoEstructuraSimit => 
                    ComparendoEstructuraSimit.ComCodigoPasajeros,
                    comparendo => comparendo
                    .MapFrom(comparendoOrigen => (int?) comparendoOrigen
                    .CodigoPasajeros))
                .ForMember(ComparendoEstructuraSimit => 
                    ComparendoEstructuraSimit.FuenteComparendo,
                    comparendo => comparendo
                    .MapFrom(comparendoOrigen => (int?) comparendoOrigen
                    .Fuente));

            CreateMap<CrearAgentePorSoloPlacaCommand, ComparendoAgenteTransito>()
                .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => 
                    new Guid("FDBDD067-861D-424E-69D7-08DA9C1F622A")))
                .ForMember(dest => dest.UsuarioCreadorId, opt => opt.MapFrom(src => 
                    new Guid("7FE04BC7-D2B9-4756-A592-D0501882DDC8")))
                .ForMember(dest => dest.UsuarioActualizadorId, opt => opt.MapFrom(src =>
                    new Guid("7FE04BC7-D2B9-4756-A592-D0501882DDC8")))
                .ForMember(dest => dest.Nombres, opt => opt.MapFrom(src => "Nombre Indeterminado"))
                .ForMember(dest => dest.Apellidos, opt => opt.MapFrom(src => "Apellido Indeterminado"))
                .ForMember(dest => dest.Placa, opt => opt.MapFrom(src => src.Placa))
                .ForMember(dest => dest.Entidad, opt => opt.MapFrom(src => eEntidadAgente.Polca))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => eEstado.AcuerdoDePago))
                .ForMember(dest => dest.ActivoDB, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.FechaActualizacion, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<CrearComparendoCommand, Comparendos>()
                //--------------------DATOS BÁSICOS DEL COMPARENDO -------------------------------//
                .ForMember(dest => dest.Numero, opt => opt.MapFrom(src => src.ComNumero))
                .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => src.ComFecha.convertirCadenaFecha()))
                .ForMember(dest => dest.Hora, opt => opt.MapFrom(src => src.ComHora.convertirCadenaHora()))
                .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.ComDir))
                .ForMember(dest => dest.MunicipioDireccionId, opt => opt.MapFrom(src => src.ComOrganismo))
                .ForMember(dest => dest.Localidad, opt => opt.MapFrom(src => src.ComLocalidadComuna))
                .ForMember(dest => dest.Placa, opt => opt.MapFrom(src => src.ComPlaca))
                .ForMember(dest => dest.MatriculaSecretariaId, opt => opt.MapFrom(src => src.ComDivipoMatri)) // queda en observacion     
                .ForMember(dest => dest.TipoVehiculoId, opt => opt.MapFrom(src => src.ComTipoVehi))
                .ForMember(dest => dest.ClaseServicioId, opt => opt.MapFrom(src => src.ComTipoSer))
                .ForMember(dest => dest.CodigoRadio, opt => opt.MapFrom(src => (src.ComCodigoRadio != null)? (eRadioAccion) src.ComCodigoRadio: (eRadioAccion?)null))
                .ForMember(dest => dest.CodigoModalidad, opt => opt.MapFrom(src => (src.ComCodigoModalidad != null) ? (eModalidadTransporte) src.ComCodigoModalidad: (eModalidadTransporte?)null))
                .ForMember(dest => dest.CodigoPasajeros, opt => opt.MapFrom(src => (src.ComCodigoPasajeros != null) ? (eCodigoPasajeros) src.ComCodigoPasajeros: (eCodigoPasajeros?)null))
                //--------------------DATOS BÁSICOS DEL INFRACTOR -------------------------------//
                .ForMember(dest => dest.InfractorTipoDocumentoId, opt => opt.MapFrom(src => src.ComTipoInfrac))
                .ForMember(dest => dest.DocumentoInfractor, opt => opt.MapFrom(src => src.ComInfractor))
                .ForMember(dest => dest.NombreInfractor, opt => opt.MapFrom(src => src.ComNombre))
                .ForMember(dest => dest.ApellidoInfractor, opt => opt.MapFrom(src => src.ComApellido))
                .ForMember(dest => dest.EdadInfractor, opt => opt.MapFrom(src => src.ComEdadInfractor))
                .ForMember(dest => dest.DireccionInfractor, opt => opt.MapFrom(src => src.ComDirInfractor))
                .ForMember(dest => dest.EmailInfractor, opt => opt.MapFrom(src => src.ComEmail))
                .ForMember(dest => dest.TelefonoInfractor, opt => opt.MapFrom(src => src.ComTeleInfractor))
                .ForMember(dest => dest.CiudadInfractorId, opt => opt.MapFrom(src => src.ComIdCiudad))
                .ForMember(dest => dest.LicenciaConduccion, opt => opt.MapFrom(src => src.ComLicencia))
                .ForMember(dest => dest.LicenciaConduccionCategoria, opt => opt.MapFrom(src => src.ComCategoria))
                .ForMember(dest => dest.LicenciaTransitoSecretariaId, opt => opt.MapFrom(src => src.ComSecreExpide))
                .ForMember(dest => dest.LicenciaVence, opt => opt.MapFrom(src => src.ComFechaVence.convertirCadenaFecha()))
                .ForMember(dest => dest.TipoInfractorId, opt => opt.MapFrom(src => src.ComTipoInfrac))
                .ForMember(dest => dest.LicenciaTransito, opt => opt.MapFrom(src => src.CompLicTransito))
                .ForMember(dest => dest.LicenciaTransitoSecretariaId, opt => opt.MapFrom(src => src.ComDivipoLicen))
                //--------------------DATOS BÁSICOS DEL PROPIETARIO -------------------------------//
                .ForMember(dest => dest.DocumentoPropietario, opt => opt.MapFrom(src => src.ComIdentificacion))
                .ForMember(dest => dest.TipoDocumentoPropietarioId, opt => opt.MapFrom(src => src.ComIdTipoDocProp))
                .ForMember(dest => dest.NombrePropietario, opt => opt.MapFrom(src => src.ComNombreProp))
                //.ForMember(dest => dest.ApellidoPropietario, opt => opt.MapFrom(src => src.ComNombreProp))
                
                //--------------------DATOS BÁSICOS DE LA EMPRESA Y TARJETA OPERACION -------------------------------//
                .ForMember(dest => dest.NombreEmpresa, opt => opt.MapFrom(src => src.ComNombreEmpresa))
                .ForMember(dest => dest.NitEmpresa, opt => opt.MapFrom(src => src.ComNitEmpresa))
                .ForMember(dest => dest.TarjetaOperacion, opt => opt.MapFrom(src => src.ComTarjetaOperacion))

                //--------------------DATOS BÁSICOS DEl AGENTE -------------------------------//
                
                //--------------------DATOS AVANZADOS DEL COMPARENDO -------------------------------//
                .ForMember(dest => dest.Observaciones, opt => opt.MapFrom(src => src.CompObservaciones))
                .ForMember(dest => dest.Fuga, opt => opt.MapFrom(src => (src.ComFuga != null)? src.ComFuga.convertirCadenaBoolean(): false))
                .ForMember(dest => dest.Accidente, opt => opt.MapFrom(src => (src.ComAcci!=null)? src.ComAcci.convertirCadenaBoolean(): false))
                //--------------------DATOS AVANZADOS DEL COMPARENDO INMOVILIZACION -------------------------------//
                .ForMember(dest => dest.Inmobilizacion, opt => opt.MapFrom(src => (src.ComInmov!=null)?src.ComInmov.convertirCadenaBoolean(): false))
                .ForMember(dest => dest.PatioInmoviliza, opt => opt.MapFrom(src => src.ComPatioInmoviliza))
                .ForMember(dest => dest.DireccionPatioInmoviliza, opt => opt.MapFrom(src => src.ComDirPatioInmovi))
                .ForMember(dest => dest.GruaNumero, opt => opt.MapFrom(src => src.ComGruaNumero))
                .ForMember(dest => dest.GruaPlaca, opt => opt.MapFrom(src => src.ComPlacaGrua))
                .ForMember(dest => dest.ConsecutivoInmovilizacion, opt => opt.MapFrom(src => src.ComConsecutInmovi))
                //--------------------DATOS DEL TESTIGO ----------------------------------------------------//
                .ForMember(dest => dest.DocumentoTestigo, opt => opt.MapFrom(src => src.ComIdentificacionTest))
                .ForMember(dest => dest.NombreTestigo, opt => opt.MapFrom(src => src.ComNombreTesti))
                //.ForMember(dest => dest.ApellidoTestigo, opt => opt.MapFrom(src => src.ComNombreTesti))
                .ForMember(dest => dest.DireccionTestigo, opt => opt.MapFrom(src => src.ComDirecResTesti))
                .ForMember(dest => dest.TelefonoTestigo, opt => opt.MapFrom(src => src.ComTeleTestigo))
                //--------------------DATOS DEL DINERO MONEY DEL COMPARENDO ----------------------------------------------------//
                .ForMember(dest => dest.ValorComparendo, opt => opt.MapFrom(src => src.ComValor))
                .ForMember(dest => dest.ValorAdicional, opt => opt.MapFrom(src => src.ComValAd))
                // falta divipo 
                .ForMember(dest => dest.EstadoComparendoId, opt => opt.MapFrom(src => src.ComEstadoCom))
                .ForMember(dest => dest.Polca, opt => opt.MapFrom(src => (src.ComPolca!= null )? src.ComPolca.convertirCadenaBoolean(): false));
       
        }
    }
}