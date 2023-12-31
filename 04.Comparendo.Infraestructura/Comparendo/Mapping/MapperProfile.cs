using _01.Comparendo.Dominio.Comparendos.enums;
using _01.Comparendo.Dominio.Comparendos.Models;
using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Command.Commands;
using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Query.DTOs;
using _02.Comparendo.Core.Aplicacion.Extensions;
using AutoMapper;

namespace _04.Comparendo.Infraestructura.Comparendo.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
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
                //-----------------DATOS DE AUDITORIA---------------------------------------
                .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src =>
                    new Guid("FDBDD067-861D-424E-69D7-08DA9C1F622A")))
                .ForMember(dest => dest.UsuarioCreadorId, opt => opt.MapFrom(src =>
                    new Guid("7FE04BC7-D2B9-4756-A592-D0501882DDC8")))
                .ForMember(dest => dest.UsuarioActualizadorId, opt => opt.MapFrom(src =>
                    new Guid("7FE04BC7-D2B9-4756-A592-D0501882DDC8")))
                .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.FechaActualizacion, opt => opt.MapFrom(src => DateTime.UtcNow))
                //--------------------DATOS PARA ACTIVAR O DESACTIVAR COMPARENDO-------------------//
                .ForMember(dest => dest.ActivoDB, opt => opt.MapFrom(src => true))
                //--------------------DATOS BÁSICOS DEL COMPARENDO -------------------------------//
                .ForMember(dest => dest.Numero, opt => opt.MapFrom(src => src.ComNumero))
                .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => src.ComFecha.convertirCadenaFecha()))
                .ForMember(dest => dest.Hora, opt => opt.MapFrom(src => src.ComHora.convertirCadenaHora()))
                .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.ComDir))
                .ForMember(dest => dest.MunicipioDireccionId, opt => opt.MapFrom(src => src.ComDivipoMuni))
                .ForMember(dest => dest.Localidad, opt => opt.MapFrom(src => src.ComLocalidadComuna))
                .ForMember(dest => dest.Placa, opt => opt.MapFrom(src => src.ComPlaca))
                .ForMember(dest => dest.MatriculaSecretariaId, opt => opt.MapFrom(src => src.ComDivipoMatri)) // queda en observacion     
                .ForMember(dest => dest.TipoVehiculoId, opt => opt.MapFrom(src => src.ComTipoVehi))
                .ForMember(dest => dest.ClaseServicioId, opt => opt.MapFrom(src => src.ComTipoSer))
                .ForMember(dest => dest.CodigoRadio, opt => opt.MapFrom(src => (src.ComCodigoRadio != null) ? (eRadioAccion)src.ComCodigoRadio : (eRadioAccion?)null))
                .ForMember(dest => dest.CodigoModalidad, opt => opt.MapFrom(src => (src.ComCodigoModalidad != null) ? (eModalidadTransporte)src.ComCodigoModalidad : (eModalidadTransporte?)null))
                .ForMember(dest => dest.CodigoPasajeros, opt => opt.MapFrom(src => (src.ComCodigoPasajeros != null) ? (eCodigoPasajeros)src.ComCodigoPasajeros : (eCodigoPasajeros?)null))
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
                .ForMember(dest => dest.LicenciaConduccionSecretariaId, opt => opt.MapFrom(src => src.ComSecreExpide))
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
                .ForMember(dest => dest.Fuga, opt => opt.MapFrom(src => (src.ComFuga != null) ? src.ComFuga.convertirCadenaBoolean() : false))
                .ForMember(dest => dest.Accidente, opt => opt.MapFrom(src => (src.ComAcci != null) ? src.ComAcci.convertirCadenaBoolean() : false))
                //--------------------DATOS AVANZADOS DEL COMPARENDO INMOVILIZACION -------------------------------//
                .ForMember(dest => dest.Inmobilizacion, opt => opt.MapFrom(src => (src.ComInmov != null) ? src.ComInmov.convertirCadenaBoolean() : false))
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
                .ForMember(dest => dest.SecretariaId, opt => opt.MapFrom(src => src.ComOrganismo))
                .ForMember(dest => dest.EstadoComparendoId, opt => opt.MapFrom(src => src.ComEstadoCom))
                .ForMember(dest => dest.Polca, opt => opt.MapFrom(src => (src.ComPolca != null) ? src.ComPolca.convertirCadenaBoolean() : false))
                //----------------------------DATOS DEL TUTOR------------------------------------------//
                /*
                .ForMember(_ => _, opt => opt.MapFrom(src => src.Id_Tipo_Doc_Tutor))
                .ForMember(_ => _, opt => opt.MapFrom(src => src.Nro_Doc_Tutor))
                .ForMember(_ => _, opt => opt.MapFrom(src => src.Nombre_Tutor))
                .ForMember(_ => _, opt => opt.MapFrom(src => src.Apellido_Tutor))
                */
                //----------------------------DATOS COMPARENDO ELECTRÓNICO-----------------------------//
                .ForMember(dest => dest.ComparendoElectronico, opt => opt.MapFrom(src => src.FotoMulta.convertirCadenaBoolean()))
                .ForMember(dest => dest.FechaNotificacion, opt => opt.MapFrom(src => src.FechaNotificacion.convertirCadenaFecha()))
                .ForMember(dest => dest.Fuente, opt => opt.MapFrom(src => (src.FuenteComparendo != null) ? (eFuenteComparendo)src.FuenteComparendo : (eFuenteComparendo?)null))
                .ForMember(dest => dest.Latitud, opt => opt.MapFrom(src => src.LatitudComparendo))
                .ForMember(dest => dest.Longitud, opt => opt.MapFrom(src => src.LongitudComparendo));

            CreateMap<Comparendos, ComparendoEstandarSimitDto>()
                //--------------------DATOS BÁSICOS DEL COMPARENDO ---------------------------//
                .ForMember(dest => dest.ComNumero, opt => opt.MapFrom(src => src.Numero))
                .ForMember(dest => dest.ComFecha, opt => opt.MapFrom(src => src.Fecha.convertirFechaCadena()))
                .ForMember(dest => dest.ComHora, opt => opt.MapFrom(src => src.Hora.convertirHoraCadena()))
                .ForMember(dest => dest.ComDir, opt => opt.MapFrom(src => src.Direccion))
                .ForMember(dest => dest.ComDivipoMuni, opt => opt.MapFrom(src => 
                    (src.Ciudad != null) ? src.Ciudad!.Codigo.convertirCadenaEntero() : 0))
                .ForMember(dest => dest.ComLocalidadComuna, opt => opt.MapFrom(src => src.Localidad))
                .ForMember(dest => dest.ComPlaca, opt => opt.MapFrom(src => src.Placa))
                .ForMember(dest => dest.ComDivipoMatri, opt => opt.MapFrom(src => 
                    (src.SecretariaTransitoMatriculado != null) ?  
                        src.SecretariaTransitoMatriculado!.Codigo.convertirCadenaEntero(): 0))
                .ForMember(dest => dest.ComTipoSer, opt => opt.MapFrom(src => src.ClaseServicioId))
                .ForMember(dest => dest.ComCodigoRadio, opt => opt.MapFrom(src =>
                    (src.CodigoRadio != null) ? (int?)src.CodigoRadio : null))
                .ForMember(dest => dest.ComCodigoModalidad, opt => opt.MapFrom(src =>
                    (src.CodigoModalidad != null) ? (int?)src.CodigoModalidad: null))
                .ForMember(dest => dest.ComCodigoModalidad, opt => opt.MapFrom(src =>
                    (src.CodigoPasajeros != null) ? (int?) src.CodigoPasajeros: null))
                //--------------------DATOS BÁSICOS DEL INFRACTOR ----------------------------//
                .ForMember(dest => dest.ComInfraccion, opt => opt.MapFrom(src => src.DocumentoInfractor))
                .ForMember(dest => dest.ComTipoDoc, opt => opt.MapFrom(src => src.InfractorTipoDocumentoId))
                .ForMember(dest => dest.ComNombre, opt => opt.MapFrom(src => src.NombreInfractor))
                .ForMember(dest => dest.ComApellido, opt => opt.MapFrom(src => src.ApellidoInfractor))
                .ForMember(dest => dest.ComEdadInfractor, opt => opt.MapFrom(src => (src.EdadInfractor != 0) ? 
                    (int?) src.EdadInfractor: null))
                .ForMember(dest => dest.ComDirInfractor, opt => opt.MapFrom(src => src.DireccionInfractor))
                .ForMember(dest => dest.ComEmail, opt => opt.MapFrom(src => src.EmailInfractor))
                .ForMember(dest => dest.ComTeleInfractor, opt => opt.MapFrom(src => src.TelefonoInfractor))
                .ForMember(dest => dest.ComIdCiudad, opt => opt.MapFrom(src =>
                    (src.CiudadDelInfractor != null) ? 
                        (int?) src.CiudadDelInfractor.Codigo.convertirCadenaEntero(): null))
                .ForMember(dest => dest.ComLicencia, opt => opt.MapFrom(src => src.LicenciaConduccion))
                .ForMember(dest => dest.ComCategoria, opt => opt.MapFrom(src => src.LicenciaConduccionCategoria))
                .ForMember(dest => dest.ComSecreExpide, opt => opt.MapFrom(src => 
                    (src.LicenciaConduccionSecretaria != null) ? 
                        (int?) src.LicenciaConduccionSecretaria.Codigo.convertirCadenaEntero() : null))
                .ForMember(dest => dest.ComFechaVence, opt => opt.MapFrom(src => src.LicenciaVence.convertirFechaCadena()))
                .ForMember(dest => dest.ComTipoInfrac, opt => opt.MapFrom(src => src.TipoInfractorId))
                .ForMember(dest => dest.CompLicTransito, opt => opt.MapFrom(src => src.LicenciaTransito))
                .ForMember(dest => dest.ComDivipoLicen, opt => opt.MapFrom(src =>
                    (src.SecretariaLicenciaTransito != null)? 
                        (int?) src.SecretariaLicenciaTransito.Codigo.convertirCadenaEntero(): null))
                //--------------------DATOS BÁSICOS DEL PROPIETARIO ----------------------------//
                .ForMember(dest => dest.ComIdentificacion, opt => opt.MapFrom(src => src.DocumentoPropietario))
                .ForMember(dest => dest.ComIdentificacion, opt => opt.MapFrom(src => src.TipoDocumentoPropietarioId))
                .ForMember(dest => dest.ComNombreProp, opt => opt.MapFrom(src => 
                    ConcatenarNombres.
                        concatenarNombresComparendo(src.NombrePropietario, src.ApellidoPropietario)))
                //--------------------DATOS BÁSICOS DE LA EMPRESA Y TARJETA OPERACION ----------//
                .ForMember(dest => dest.ComNombreEmpresa, opt => opt.MapFrom(src => src.NombreEmpresa))
                .ForMember(dest => dest.ComNitEmpresa, opt => opt.MapFrom(src => src.NitEmpresa))
                .ForMember(dest => dest.ComTarjetaOperacion, opt => opt.MapFrom(src => src.TarjetaOperacion))
                //--------------------DATOS BÁSICOS DEl AGENTE ---------------------------------//
                .ForMember(dest => dest.CompPlacaAgente, opt => opt.MapFrom(src => 
                    (src.AgenteTransito != null) ? 
                        src.AgenteTransito.Placa : null))
                //--------------------DATOS AVANZADOS DEL COMPARENDO ---------------------------//
                .ForMember(dest => dest.CompObservaciones, opt => opt.MapFrom(src => src.Observaciones))
                .ForMember(dest => dest.ComFuga, opt => opt.MapFrom(src => 
                    src.Fuga.convertirBooleanCadena()))
                .ForMember(dest => dest.ComAcci, opt => opt.MapFrom(src => 
                    src.Accidente.convertirBooleanCadena()))
                //--------------------DATOS AVANZADOS DEL COMPARENDO INMOVILIZACION ------------//
                .ForMember(dest => dest.ComInmov, opt => opt.MapFrom(src =>
                    src.Inmobilizacion.convertirBooleanCadena()))
                .ForMember(dest => dest.ComPatioInmoviliza, opt => opt.MapFrom(src =>
                    src.PatioInmoviliza))
                .ForMember(dest => dest.ComDirPatioInmovi, opt => opt.MapFrom(src =>
                    src.DireccionPatioInmoviliza))
                .ForMember(dest => dest.ComGruaNumero, opt => opt.MapFrom(src => src.GruaNumero))
                .ForMember(dest => dest.ComPlacaGrua, opt => opt.MapFrom(src => src.GruaPlaca))
                .ForMember(dest => dest.ComConsecutInmovi, opt => opt.MapFrom(src => src.ConsecutivoInmovilizacion))
                //--------------------DATOS DEL TESTIGO -----------------------------------------//
                .ForMember(dest => dest.ComIdentificacionTest, opt => opt.MapFrom(src => src.DocumentoTestigo))
                .ForMember(dest => dest.ComNombreTesti, opt => opt.MapFrom(src => 
                    ConcatenarNombres.concatenarNombresComparendo(src.NombreTestigo, src.ApellidoTestigo)))
                .ForMember(dest => dest.ComDirecResTesti, opt => opt.MapFrom(src => src.DireccionTestigo))
                .ForMember(dest => dest.ComTeleTestigo, opt => opt.MapFrom(src => src.TelefonoTestigo))
                //--------------------DATOS DEL DINERO MONEY DEL COMPARENDO --------------------//
                .ForMember(dest => dest.ComValor, opt => opt.MapFrom(src => src.ValorComparendo))
                .ForMember(dest => dest.ComValAd, opt => opt.MapFrom(src => src.ValorAdicional))
                .ForMember(dest => dest.ComOrganismo, opt => opt.MapFrom(src => 
                    (src.SecretariaTransito != null)? 
                        src.SecretariaTransito.Codigo.convertirCadenaEntero(): 0))
                .ForMember(dest => dest.ComEstadoCom, opt => opt.MapFrom(src => src.EstadoComparendoId))
                //----------------------------DATOS EXTRA DEL COMPARENDO--------------------------//
                .ForMember(dest => dest.ComPolca, opt => opt.MapFrom(src => 
                    src.Polca.convertirBooleanCadena()))
                .ForMember(dest => dest.ComInfractor, opt => opt.MapFrom(src => 
                    src.ComparendoInfraccionComparendos!.FirstOrDefault()!
                        .ComparendoTipoInfraccion.Codigo))
                .ForMember(dest => dest.ComValInfra, opt => opt.MapFrom(src =>
                    src.ComparendoInfraccionComparendos!.FirstOrDefault()!
                        .ValorInfraccion))
                //----------------------------DATOS DEL TUTOR-------------------------------------//
                .ForMember(dest => dest.Id_Tipo_Doc_Tutor, opt => opt.MapFrom(src => (int?) null))
                .ForMember(dest => dest.Nro_Doc_Tutor, opt => opt.MapFrom(src => (string?) null))
                .ForMember(dest => dest.Nombre_Tutor, opt => opt.MapFrom(src => (string?) null))
                .ForMember(dest => dest.Apellido_Tutor, opt => opt.MapFrom(src => (string?) null))
                //----------------------------DATOS COMPARENDO ELECTRÓNICO------------------------//
                .ForMember(dest => dest.FotoMulta, opt => opt.MapFrom(src => src.
                    ComparendoElectronico.convertirBooleanCadena()))
                .ForMember(dest => dest.FechaNotificacion, opt => opt.MapFrom(src =>
                    (src.FechaNotificacion != null) ? 
                        src.FechaNotificacion.convertirFechaCadena(): null))
                .ForMember(dest => dest.FuenteComparendo, opt => opt.MapFrom(src =>
                    (src.Fuente != null)? (int?) src.Fuente: null))
                .ForMember(dest => dest.LatitudComparendo, opt => opt.MapFrom(src => src.Latitud))
                .ForMember(dest => dest.LongitudComparendo, opt => opt.MapFrom(src => src.Longitud));
        }
    }
}
