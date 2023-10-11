using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Command.Commands;
using _05.Comparendo.Presentacion.Consola.Data.Context;
using _05.Comparendo.Presentacion.Consola.Extension;
using _05.Comparendo.Presentacion.Consola.Models;
using _05.Comparendo.Presentacion.Consola.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace _05.Comparendo.Presentacion.Consola.Repository.Implementations
{
    public class ComparendosSimulacionSimitRespository : IComparendosSimulacionSimitRespository
    {
        private readonly DataContextSimulacionSimit _contexto;
        private readonly decimal _SALARIOMINIMO;
        public ComparendosSimulacionSimitRespository(DataContextSimulacionSimit contexto)
        {
            _contexto = contexto;
            _SALARIOMINIMO = 1160000;
        }
        public async Task<IEnumerable<CrearComparendoCommand>> listarComparendos()
        {
            var comparendoCompletoSimulacionSimit = 
                await (
                //--------------------DATOS BÁSICOS DEL COMPARENDO -------------------------------//
                from comparendo in _contexto.vwComparendo!
                join direccionComparendo in _contexto.vwDireccion! on comparendo.DireccionId
                equals direccionComparendo.Id into grupoDireccionComparendo 
                from direccionComparendo in grupoDireccionComparendo.DefaultIfEmpty()
                join localidadComparendo in _contexto.Localidad! on comparendo.LocalidadId
                equals localidadComparendo.Id into grupoLocalidadComparendo
                from localidadComparendo in grupoLocalidadComparendo.DefaultIfEmpty()

                //--------------------DATOS BÁSICOS DEL INFRACTOR -------------------------------//
                join infractor in _contexto.vwPersona! on comparendo.InfractorId equals infractor.Id
                into grupoInfractor from infractor in grupoInfractor.DefaultIfEmpty()
                join tipoDocumentoInfractor in _contexto.TipoDocumento! on infractor.TipoDoc 
                equals tipoDocumentoInfractor.Nombre into grupoTipoDocumentoInfractor 
                from tipoDocumentoInfractor in grupoTipoDocumentoInfractor.DefaultIfEmpty()
                join direccionInfractor in _contexto.vwDireccion! on comparendo.InfractorDireccionId
                equals direccionInfractor.Id into grupoDireccionInfractor 
                from direccionInfractor in grupoDireccionInfractor.DefaultIfEmpty()
                join licenciaConduccionInfractor in _contexto.vwLicenciaConduccion! on comparendo.InfractorLicenciaConduccionId
                equals licenciaConduccionInfractor.Id into grupoLicenciaConduccionInfractor
                from licenciaConduccionInfractor in grupoLicenciaConduccionInfractor.DefaultIfEmpty()
                join tipoInfractor in _contexto.TipoInfractor! on comparendo.TipoInfractorId
                equals tipoInfractor.Id into grupoTipoInfractor
                from tipoInfractor in grupoTipoInfractor.DefaultIfEmpty()

                //--------------------DATOS BÁSICOS DEL PROPIETARIO -------------------------------//
                join propietario in _contexto.vwPersona! on comparendo.PropietarioId equals propietario.Id
                into grupoPropietario from propietario in grupoPropietario.DefaultIfEmpty()
                join tipoDocumentoPropietario in _contexto.TipoDocumento! on propietario.TipoDoc
                equals tipoDocumentoPropietario.Nombre into grupoTipoDocumentoPropietario
                from tipoDocumentoPropietario in grupoTipoDocumentoPropietario.DefaultIfEmpty()

                //--------------------DATOS BÁSICOS DEl AGENTE -------------------------------//
                join agente in _contexto.vwAgente! on comparendo.AgenteId equals agente.Id 
                into grupoAgente from agente in grupoAgente.DefaultIfEmpty()

                //--------------------DATOS AVANZADOS DEL COMPARENDO -------------------------------//
                join controlComparendo in _contexto.ControlComparendo! on
                comparendo.Numero equals controlComparendo.NroComparendo into grupoControlComparendo
                from controlComparendo in grupoControlComparendo.DefaultIfEmpty()

                //--------------------DATOS BÁSICOS DEL TESTIGO -------------------------------//
                join testigo in _contexto.vwPersona! on comparendo.TestigoId equals testigo.Id
                into grupoTestigo from testigo in grupoTestigo.DefaultIfEmpty()
                join tipoDocumentoTestigo in _contexto.TipoDocumento! on testigo.TipoDoc
                equals tipoDocumentoTestigo.Nombre into grupoTipoDocumentoTestigo
                from tipoDocumentoTestigo in grupoTipoDocumentoTestigo.DefaultIfEmpty()
                join direccionTestigo in _contexto.vwDireccion! on comparendo.DireccionTestigoId
                equals direccionTestigo.Id into grupoDireccionTestigo 
                from direccionTestigo in grupoDireccionTestigo.DefaultIfEmpty()

                //--------------------DATOS DEL DINERO MONEY DEL COMPARENDO ----------------------------------------------------//
                join codigoInfraccion in _contexto.CodigoInfraccion! on comparendo.CodigoInfraccionId
                equals codigoInfraccion.Id into grupoInfraccion
                from codigoInfraccion in grupoInfraccion.DefaultIfEmpty()
                //----------------------------DATOS EXTRA DEL COMPARENDO------------------------------------------


                select new CrearComparendoCommand
                {
                    //--------------------DATOS BÁSICOS DEL COMPARENDO -------------------------------//
                    ComNumero = comparendo.Numero, // OBLIGATORIO SYSTRANS
                    ComFecha = comparendo.Fecha.HasValue ? comparendo.Fecha!.Value.ToString("dd/MM/yyyy"): string.Empty, // OBLIGATORIO SYSTRANS
                    ComHora = comparendo.Hora.HasValue ? comparendo.Hora!.Value.conversionTimeSpanStringHHmm(): string.Empty, // OBLIGATORIO SYSTRANS
                    ComDir = $"{direccionComparendo.ViaPrinTipo} {direccionComparendo.ViaPrinNumero} {direccionComparendo.ViaSecTipo} {direccionComparendo.ViaSecNumero}", // OBLIGATORIO SYSTRANS
                    ComDivipoMuni = direccionComparendo.CodigoRUNT.IsNullOrEmpty() ? 0 : (int?)int.Parse(direccionComparendo.CodigoRUNT!), // OBLIGATORIO SYSTRANS
                    ComLocalidadComuna = localidadComparendo.Nombre,
                    ComPlaca = comparendo.Placa,
                    ComDivipoMatri = comparendo.DivipoMatricula.IsNullOrEmpty() ? null : (int?)int.Parse(comparendo.DivipoMatricula!),
                    ComTipoVehi = comparendo.TipoVehiculoId, // OBLIGATORIO SYSTRANS
                    ComTipoSer = comparendo.ClaseServicioId, // OBLIGATORIO SYSTRANS
                    ComCodigoRadio = comparendo.RadioAccionId,
                    ComCodigoModalidad = comparendo.ModalidaTrasporteId,
                    ComCodigoPasajeros = comparendo.TransportePasajerosId,
                    /*-------------------------------------------------------------------------------------*/
                    //--------------------DATOS BÁSICOS DEL INFRACTOR -------------------------------//
                    ComInfractor = infractor.NumeroDocumento, // OBLIGATORIO SYSTRANS
                    ComTipoDoc = tipoDocumentoInfractor.SimitId.IsNullOrEmpty() ? null : (int?)int.Parse(tipoDocumentoInfractor.SimitId!), // OBLIGATORIO SYSTRANS
                    ComNombre = $"{infractor.NombrePrincipal} {infractor.NombreSecundario}",
                    ComApellido = $"{infractor.ApellidoPrincipal} {infractor.ApellidoSecundario}", 
                    ComEdadInfractor = (comparendo.InfractorEdad != null) ? comparendo.InfractorEdad : 0, // OBLIGATORIO SYSTRANS
                    ComDirInfractor = $"{direccionInfractor.ViaPrinTipo} {direccionInfractor.ViaPrinNumero} {direccionInfractor.ViaSecTipo} {direccionInfractor.ViaSecNumero}",
                    ComEmail = infractor.Correo,
                    ComTeleInfractor = infractor.Telefono,
                    ComIdCiudad = 1, // no se encuentra en systrans 2.0
                    ComLicencia = licenciaConduccionInfractor.Numero,
                    ComCategoria = licenciaConduccionInfractor.Categoria,
                    ComSecreExpide = comparendo.MatriculadoId,
                    ComFechaVence = licenciaConduccionInfractor.Vencimiento.HasValue ? licenciaConduccionInfractor.Vencimiento!.Value.ToString("dd/MM/yyyy"): string.Empty,
                    ComTipoInfrac = tipoInfractor.SimitId.IsNullOrEmpty() ? null: (int?)int.Parse(tipoInfractor.SimitId!),
                    CompLicTransito = comparendo.NumeroLicenciaTransito,
                    ComDivipoLicen = comparendo.DivipoMatricula.IsNullOrEmpty() ? null: (int?) int.Parse(comparendo.DivipoMatricula!),
                    //--------------------DATOS BÁSICOS DEL PROPIETARIO -------------------------------//
                    ComIdentificacion = propietario.NumeroDocumento,
                    ComIdTipoDocProp = tipoDocumentoPropietario.SimitId.IsNullOrEmpty() ? null : (int?)int.Parse(tipoDocumentoInfractor.SimitId!),
                    ComNombreProp = $"{propietario.NombrePrincipal} {propietario.NombreSecundario} {propietario.ApellidoPrincipal} {propietario.ApellidoSecundario}",
                    //--------------------DATOS BÁSICOS DE LA EMPRESA Y TARJETA OPERACION -------------------------------//
                    ComNombreEmpresa = comparendo.NombreEmpresa,
                    ComNitEmpresa = comparendo.Nit,
                    ComTarjetaOperacion = comparendo.TarjetaNumero,
                    //--------------------DATOS BÁSICOS DEl AGENTE -------------------------------//
                    CompPlacaAgente = agente.Placa, // OBLIGATORIO SYSTRANS
                    //--------------------DATOS AVANZADOS DEL COMPARENDO -------------------------------//
                    CompObservaciones = comparendo.Observaciones,
                    ComFuga = controlComparendo.Fuga.convertirIntChar(), // no está definido en base de datos systrans 2 // OBLIGATORIO SYSTRANS
                    ComAcci = 'N', // no está definido en base de datos systrans 2 // OBLIGATORIO SYSTRANS
                    GradoAlcohol = (controlComparendo.Grado != null) ? controlComparendo.Grado: 0, // OBLIGATORIO
                    //--------------------DATOS AVANZADOS DEL COMPARENDO INMOVILIZACION -------------------------------//
                    ComInmov = controlComparendo.Inmovilizado.convertirIntChar(), // no está definido en base de datos systrans 2 // OBLIGATORIO SYSTRANS
                    ComPatioInmoviliza = comparendo.Patio,
                    ComDirPatioInmovi = comparendo.PatioDireccion,
                    ComGruaNumero = comparendo.Grua,
                    ComPlacaGrua = comparendo.GruaPlaca,
                    ComConsecutInmovi = comparendo.Consecutivo,
                    //--------------------DATOS DEL TESTIGO ----------------------------------------------------//
                    ComIdentificacionTest = testigo.NumeroDocumento,
                    ComNombreTesti = $"{testigo.NombrePrincipal} {testigo.NombreSecundario} {testigo.ApellidoPrincipal} {testigo.ApellidoSecundario}",
                    ComDirecResTesti = $"{direccionTestigo.ViaPrinTipo} {direccionTestigo.ViaPrinNumero} {direccionTestigo.ViaSecTipo} {direccionTestigo.ViaSecNumero}",
                    ComTeleTestigo = testigo.Telefono,
                    //--------------------DATOS DEL DINERO MONEY DEL COMPARENDO ----------------------------------------------------//
                    ComValor = (codigoInfraccion != null) ? _SALARIOMINIMO * codigoInfraccion.Salarios : 0,
                    ComValAd = 0,
                    ComOrganismo = 25754000, // OBLIGATORIO SYSTRANS
                    ComEstadoCom = comparendo.EsatdoId != null ? comparendo.EsatdoId: 1, // OBLIGATORIO SYSTRANS
                    //----------------------------DATOS EXTRA DEL COMPARENDO------------------------------------------
                    ComPolca =  controlComparendo.Polca.convertirIntChar(),
                    ComInfraccion = (codigoInfraccion.Codigo != null)? codigoInfraccion.Codigo: string.Empty, // OBLIGATORIO SYSTRANS
                    ComValInfra = (codigoInfraccion != null) ? _SALARIOMINIMO * codigoInfraccion.Salarios : 0
                })
                .Skip(11)
                .Take(10)
                .AsNoTracking()
                .ToListAsync(); 
            return comparendoCompletoSimulacionSimit;
        }

        public async Task<CrearComparendoCommand> obtenerComparendoPorNumero(string numeroComparendo)
        {
            var comparendoCompletoSimulacionSimit = 
                await (
                //--------------------DATOS BÁSICOS DEL COMPARENDO -------------------------------//
                from comparendo in _contexto.vwComparendo!
                join direccionComparendo in _contexto.vwDireccion! on comparendo.DireccionId
                equals direccionComparendo.Id into grupoDireccionComparendo 
                from direccionComparendo in grupoDireccionComparendo.DefaultIfEmpty()
                join localidadComparendo in _contexto.Localidad! on comparendo.LocalidadId
                equals localidadComparendo.Id into grupoLocalidadComparendo
                from localidadComparendo in grupoLocalidadComparendo.DefaultIfEmpty()

                //--------------------DATOS BÁSICOS DEL INFRACTOR -------------------------------//
                join infractor in _contexto.vwPersona! on comparendo.InfractorId equals infractor.Id
                into grupoInfractor from infractor in grupoInfractor.DefaultIfEmpty()
                join tipoDocumentoInfractor in _contexto.TipoDocumento! on infractor.TipoDoc 
                equals tipoDocumentoInfractor.Nombre into grupoTipoDocumentoInfractor 
                from tipoDocumentoInfractor in grupoTipoDocumentoInfractor.DefaultIfEmpty()
                join direccionInfractor in _contexto.vwDireccion! on comparendo.InfractorDireccionId
                equals direccionInfractor.Id into grupoDireccionInfractor 
                from direccionInfractor in grupoDireccionInfractor.DefaultIfEmpty()
                join licenciaConduccionInfractor in _contexto.vwLicenciaConduccion! on comparendo.InfractorLicenciaConduccionId
                equals licenciaConduccionInfractor.Id into grupoLicenciaConduccionInfractor
                from licenciaConduccionInfractor in grupoLicenciaConduccionInfractor.DefaultIfEmpty()
                join tipoInfractor in _contexto.TipoInfractor! on comparendo.TipoInfractorId
                equals tipoInfractor.Id into grupoTipoInfractor
                from tipoInfractor in grupoTipoInfractor.DefaultIfEmpty()

                //--------------------DATOS BÁSICOS DEL PROPIETARIO -------------------------------//
                join propietario in _contexto.vwPersona! on comparendo.PropietarioId equals propietario.Id
                into grupoPropietario from propietario in grupoPropietario.DefaultIfEmpty()
                join tipoDocumentoPropietario in _contexto.TipoDocumento! on propietario.TipoDoc
                equals tipoDocumentoPropietario.Nombre into grupoTipoDocumentoPropietario
                from tipoDocumentoPropietario in grupoTipoDocumentoPropietario.DefaultIfEmpty()

                //--------------------DATOS BÁSICOS DEl AGENTE -------------------------------//
                join agente in _contexto.vwAgente! on comparendo.AgenteId equals agente.Id 
                into grupoAgente from agente in grupoAgente.DefaultIfEmpty()

                //--------------------DATOS AVANZADOS DEL COMPARENDO -------------------------------//
                join controlComparendo in _contexto.ControlComparendo! on
                comparendo.Numero equals controlComparendo.NroComparendo into grupoControlComparendo
                from controlComparendo in grupoControlComparendo.DefaultIfEmpty()

                //--------------------DATOS BÁSICOS DEL TESTIGO -------------------------------//
                join testigo in _contexto.vwPersona! on comparendo.TestigoId equals testigo.Id
                into grupoTestigo from testigo in grupoTestigo.DefaultIfEmpty()
                join tipoDocumentoTestigo in _contexto.TipoDocumento! on testigo.TipoDoc
                equals tipoDocumentoTestigo.Nombre into grupoTipoDocumentoTestigo
                from tipoDocumentoTestigo in grupoTipoDocumentoTestigo.DefaultIfEmpty()
                join direccionTestigo in _contexto.vwDireccion! on comparendo.DireccionTestigoId
                equals direccionTestigo.Id into grupoDireccionTestigo 
                from direccionTestigo in grupoDireccionTestigo.DefaultIfEmpty()

                //--------------------DATOS DEL DINERO MONEY DEL COMPARENDO ----------------------------------------------------//
                join codigoInfraccion in _contexto.CodigoInfraccion! on comparendo.CodigoInfraccionId
                equals codigoInfraccion.Id into grupoInfraccion
                from codigoInfraccion in grupoInfraccion.DefaultIfEmpty()
                //----------------------------DATOS EXTRA DEL COMPARENDO------------------------------------------
                where comparendo.Numero!.Equals(numeroComparendo)

                select new CrearComparendoCommand
                {
                    //--------------------DATOS BÁSICOS DEL COMPARENDO -------------------------------//
                    ComNumero = comparendo.Numero, // OBLIGATORIO SYSTRANS
                    ComFecha = comparendo.Fecha.HasValue ? comparendo.Fecha!.Value.ToString("dd/MM/yyyy"): string.Empty, // OBLIGATORIO SYSTRANS
                    ComHora = comparendo.Hora.HasValue ? comparendo.Hora!.Value.conversionTimeSpanStringHHmm(): string.Empty, // OBLIGATORIO SYSTRANS
                    ComDir = $"{direccionComparendo.ViaPrinTipo} {direccionComparendo.ViaPrinNumero} {direccionComparendo.ViaSecTipo} {direccionComparendo.ViaSecNumero}", // OBLIGATORIO SYSTRANS
                    ComDivipoMuni = direccionComparendo.CodigoRUNT.IsNullOrEmpty() ? 0 : (int?)int.Parse(direccionComparendo.CodigoRUNT!), // OBLIGATORIO SYSTRANS
                    ComLocalidadComuna = localidadComparendo.Nombre,
                    ComPlaca = comparendo.Placa,
                    ComDivipoMatri = comparendo.DivipoMatricula.IsNullOrEmpty() ? null : (int?)int.Parse(comparendo.DivipoMatricula!),
                    ComTipoVehi = comparendo.TipoVehiculoId, // OBLIGATORIO SYSTRANS
                    ComTipoSer = comparendo.ClaseServicioId, // OBLIGATORIO SYSTRANS
                    ComCodigoRadio = comparendo.RadioAccionId,
                    ComCodigoModalidad = comparendo.ModalidaTrasporteId,
                    ComCodigoPasajeros = comparendo.TransportePasajerosId,
                    /*-------------------------------------------------------------------------------------*/
                    //--------------------DATOS BÁSICOS DEL INFRACTOR -------------------------------//
                    ComInfractor = infractor.NumeroDocumento, // OBLIGATORIO SYSTRANS
                    ComTipoDoc = tipoDocumentoInfractor.SimitId.IsNullOrEmpty() ? null : (int?)int.Parse(tipoDocumentoInfractor.SimitId!), // OBLIGATORIO SYSTRANS
                    ComNombre = $"{infractor.NombrePrincipal} {infractor.NombreSecundario}",
                    ComApellido = $"{infractor.ApellidoPrincipal} {infractor.ApellidoSecundario}", 
                    ComEdadInfractor = (comparendo.InfractorEdad != null) ? comparendo.InfractorEdad : 0, // OBLIGATORIO SYSTRANS
                    ComDirInfractor = $"{direccionInfractor.ViaPrinTipo} {direccionInfractor.ViaPrinNumero} {direccionInfractor.ViaSecTipo} {direccionInfractor.ViaSecNumero}",
                    ComEmail = infractor.Correo,
                    ComTeleInfractor = infractor.Telefono,
                    ComIdCiudad = 1, // no se encuentra en systrans 2.0
                    ComLicencia = licenciaConduccionInfractor.Numero,
                    ComCategoria = licenciaConduccionInfractor.Categoria,
                    ComSecreExpide = comparendo.MatriculadoId,
                    ComFechaVence = licenciaConduccionInfractor.Vencimiento.HasValue ? licenciaConduccionInfractor.Vencimiento!.Value.ToString("dd/MM/yyyy"): string.Empty,
                    ComTipoInfrac = tipoInfractor.SimitId.IsNullOrEmpty() ? null: (int?)int.Parse(tipoInfractor.SimitId!),
                    CompLicTransito = comparendo.NumeroLicenciaTransito,
                    ComDivipoLicen = comparendo.DivipoMatricula.IsNullOrEmpty() ? null: (int?) int.Parse(comparendo.DivipoMatricula!),
                    //--------------------DATOS BÁSICOS DEL PROPIETARIO -------------------------------//
                    ComIdentificacion = propietario.NumeroDocumento,
                    ComIdTipoDocProp = tipoDocumentoPropietario.SimitId.IsNullOrEmpty() ? null : (int?)int.Parse(tipoDocumentoInfractor.SimitId!),
                    ComNombreProp = $"{propietario.NombrePrincipal} {propietario.NombreSecundario} {propietario.ApellidoPrincipal} {propietario.ApellidoSecundario}",
                    //--------------------DATOS BÁSICOS DE LA EMPRESA Y TARJETA OPERACION -------------------------------//
                    ComNombreEmpresa = comparendo.NombreEmpresa,
                    ComNitEmpresa = comparendo.Nit,
                    ComTarjetaOperacion = comparendo.TarjetaNumero,
                    //--------------------DATOS BÁSICOS DEl AGENTE -------------------------------//
                    CompPlacaAgente = agente.Placa, // OBLIGATORIO SYSTRANS
                    //--------------------DATOS AVANZADOS DEL COMPARENDO -------------------------------//
                    CompObservaciones = comparendo.Observaciones,
                    ComFuga = controlComparendo.Fuga.convertirIntChar(), // no está definido en base de datos systrans 2 // OBLIGATORIO SYSTRANS
                    ComAcci = 'N', // no está definido en base de datos systrans 2 // OBLIGATORIO SYSTRANS
                    GradoAlcohol = (controlComparendo.Grado != null) ? controlComparendo.Grado: 0, // OBLIGATORIO
                    //--------------------DATOS AVANZADOS DEL COMPARENDO INMOVILIZACION -------------------------------//
                    ComInmov = controlComparendo.Inmovilizado.convertirIntChar(), // no está definido en base de datos systrans 2 // OBLIGATORIO SYSTRANS
                    ComPatioInmoviliza = comparendo.Patio,
                    ComDirPatioInmovi = comparendo.PatioDireccion,
                    ComGruaNumero = comparendo.Grua,
                    ComPlacaGrua = comparendo.GruaPlaca,
                    ComConsecutInmovi = comparendo.Consecutivo,
                    //--------------------DATOS DEL TESTIGO ----------------------------------------------------//
                    ComIdentificacionTest = testigo.NumeroDocumento,
                    ComNombreTesti = $"{testigo.NombrePrincipal} {testigo.NombreSecundario} {testigo.ApellidoPrincipal} {testigo.ApellidoSecundario}",
                    ComDirecResTesti = $"{direccionTestigo.ViaPrinTipo} {direccionTestigo.ViaPrinNumero} {direccionTestigo.ViaSecTipo} {direccionTestigo.ViaSecNumero}",
                    ComTeleTestigo = testigo.Telefono,
                    //--------------------DATOS DEL DINERO MONEY DEL COMPARENDO ----------------------------------------------------//
                    ComValor = (codigoInfraccion != null) ? _SALARIOMINIMO * codigoInfraccion.Salarios : 0,
                    ComValAd = 0,
                    ComOrganismo = 25754000, // OBLIGATORIO SYSTRANS
                    ComEstadoCom = comparendo.EsatdoId != null ? comparendo.EsatdoId: 1, // OBLIGATORIO SYSTRANS
                    //----------------------------DATOS EXTRA DEL COMPARENDO------------------------------------------
                    ComPolca =  controlComparendo.Polca.convertirIntChar(),
                    ComInfraccion = (codigoInfraccion.Codigo != null)? codigoInfraccion.Codigo: string.Empty, // OBLIGATORIO SYSTRANS
                    ComValInfra = (codigoInfraccion != null) ? _SALARIOMINIMO * codigoInfraccion.Salarios : 0
                })
                .FirstOrDefaultAsync();
            return comparendoCompletoSimulacionSimit!;
        }

        public async Task<int> obtenerNumeroComparendosTotales()
        {
            return await _contexto.vwComparendo!.CountAsync();
        }

        public async Task<IEnumerable<CrearComparendoCommand>> obtenerRangoListaComparendos(
            int numeroComparendosOmitir, int numeroComparendosParaObtener)
        {
            try
            {
                var comparendoCompletoSimulacionSimit = 
                await (
                //--------------------DATOS BÁSICOS DEL COMPARENDO -------------------------------//
                from comparendo in _contexto.vwComparendo!
                join direccionComparendo in _contexto.vwDireccion! on comparendo.DireccionId
                equals direccionComparendo.Id into grupoDireccionComparendo 
                from direccionComparendo in grupoDireccionComparendo.DefaultIfEmpty()
                join localidadComparendo in _contexto.Localidad! on comparendo.LocalidadId
                equals localidadComparendo.Id into grupoLocalidadComparendo
                from localidadComparendo in grupoLocalidadComparendo.DefaultIfEmpty()

                //--------------------DATOS BÁSICOS DEL INFRACTOR -------------------------------//
                join infractor in _contexto.vwPersona! on comparendo.InfractorId equals infractor.Id
                into grupoInfractor from infractor in grupoInfractor.DefaultIfEmpty()
                join tipoDocumentoInfractor in _contexto.TipoDocumento! on infractor.TipoDoc 
                equals tipoDocumentoInfractor.Nombre into grupoTipoDocumentoInfractor 
                from tipoDocumentoInfractor in grupoTipoDocumentoInfractor.DefaultIfEmpty()
                join direccionInfractor in _contexto.vwDireccion! on comparendo.InfractorDireccionId
                equals direccionInfractor.Id into grupoDireccionInfractor 
                from direccionInfractor in grupoDireccionInfractor.DefaultIfEmpty()
                join licenciaConduccionInfractor in _contexto.vwLicenciaConduccion! on comparendo.InfractorLicenciaConduccionId
                equals licenciaConduccionInfractor.Id into grupoLicenciaConduccionInfractor
                from licenciaConduccionInfractor in grupoLicenciaConduccionInfractor.DefaultIfEmpty()
                join tipoInfractor in _contexto.TipoInfractor! on comparendo.TipoInfractorId
                equals tipoInfractor.Id into grupoTipoInfractor
                from tipoInfractor in grupoTipoInfractor.DefaultIfEmpty()

                //--------------------DATOS BÁSICOS DEL PROPIETARIO -------------------------------//
                join propietario in _contexto.vwPersona! on comparendo.PropietarioId equals propietario.Id
                into grupoPropietario from propietario in grupoPropietario.DefaultIfEmpty()
                join tipoDocumentoPropietario in _contexto.TipoDocumento! on propietario.TipoDoc
                equals tipoDocumentoPropietario.Nombre into grupoTipoDocumentoPropietario
                from tipoDocumentoPropietario in grupoTipoDocumentoPropietario.DefaultIfEmpty()

                //--------------------DATOS BÁSICOS DEl AGENTE -------------------------------//
                join agente in _contexto.vwAgente! on comparendo.AgenteId equals agente.Id 
                into grupoAgente from agente in grupoAgente.DefaultIfEmpty()

                //--------------------DATOS AVANZADOS DEL COMPARENDO -------------------------------//
                join controlComparendo in _contexto.ControlComparendo! on
                comparendo.Numero equals controlComparendo.NroComparendo into grupoControlComparendo
                from controlComparendo in grupoControlComparendo.DefaultIfEmpty()

                //--------------------DATOS BÁSICOS DEL TESTIGO -------------------------------//
                join testigo in _contexto.vwPersona! on comparendo.TestigoId equals testigo.Id
                into grupoTestigo from testigo in grupoTestigo.DefaultIfEmpty()
                join tipoDocumentoTestigo in _contexto.TipoDocumento! on testigo.TipoDoc
                equals tipoDocumentoTestigo.Nombre into grupoTipoDocumentoTestigo
                from tipoDocumentoTestigo in grupoTipoDocumentoTestigo.DefaultIfEmpty()
                join direccionTestigo in _contexto.vwDireccion! on comparendo.DireccionTestigoId
                equals direccionTestigo.Id into grupoDireccionTestigo 
                from direccionTestigo in grupoDireccionTestigo.DefaultIfEmpty()

                //--------------------DATOS DEL DINERO MONEY DEL COMPARENDO ----------------------------------------------------//
                join codigoInfraccion in _contexto.CodigoInfraccion! on comparendo.CodigoInfraccionId
                equals codigoInfraccion.Id into grupoInfraccion
                from codigoInfraccion in grupoInfraccion.DefaultIfEmpty()
                //----------------------------DATOS EXTRA DEL COMPARENDO------------------------------------------


                select new CrearComparendoCommand
                {
                    //--------------------DATOS BÁSICOS DEL COMPARENDO -------------------------------//
                    ComNumero = comparendo.Numero, // OBLIGATORIO SYSTRANS
                    ComFecha = comparendo.Fecha.HasValue ? comparendo.Fecha!.Value.ToString("dd/MM/yyyy"): string.Empty, // OBLIGATORIO SYSTRANS
                    ComHora = comparendo.Hora.HasValue ? comparendo.Hora!.Value.conversionTimeSpanStringHHmm(): string.Empty, // OBLIGATORIO SYSTRANS
                    ComDir = $"{direccionComparendo.ViaPrinTipo} {direccionComparendo.ViaPrinNumero} {direccionComparendo.ViaSecTipo} {direccionComparendo.ViaSecNumero}", // OBLIGATORIO SYSTRANS
                    ComDivipoMuni = direccionComparendo.CodigoRUNT.IsNullOrEmpty() ? 0 : (int?)int.Parse(direccionComparendo.CodigoRUNT!), // OBLIGATORIO SYSTRANS
                    ComLocalidadComuna = localidadComparendo.Nombre,
                    ComPlaca = comparendo.Placa,
                    ComDivipoMatri = comparendo.DivipoMatricula.IsNullOrEmpty() ? null : (int?)int.Parse(comparendo.DivipoMatricula!),
                    ComTipoVehi = comparendo.TipoVehiculoId, // OBLIGATORIO SYSTRANS
                    ComTipoSer = comparendo.ClaseServicioId, // OBLIGATORIO SYSTRANS
                    ComCodigoRadio = comparendo.RadioAccionId,
                    ComCodigoModalidad = comparendo.ModalidaTrasporteId,
                    ComCodigoPasajeros = comparendo.TransportePasajerosId,
                    /*-------------------------------------------------------------------------------------*/
                    //--------------------DATOS BÁSICOS DEL INFRACTOR -------------------------------//
                    ComInfractor = infractor.NumeroDocumento, // OBLIGATORIO SYSTRANS
                    ComTipoDoc = tipoDocumentoInfractor.SimitId.IsNullOrEmpty() ? null : (int?)int.Parse(tipoDocumentoInfractor.SimitId!), // OBLIGATORIO SYSTRANS
                    ComNombre = $"{infractor.NombrePrincipal} {infractor.NombreSecundario}",
                    ComApellido = $"{infractor.ApellidoPrincipal} {infractor.ApellidoSecundario}", 
                    ComEdadInfractor = (comparendo.InfractorEdad != null) ? comparendo.InfractorEdad : 0, // OBLIGATORIO SYSTRANS
                    ComDirInfractor = $"{direccionInfractor.ViaPrinTipo} {direccionInfractor.ViaPrinNumero} {direccionInfractor.ViaSecTipo} {direccionInfractor.ViaSecNumero}",
                    ComEmail = infractor.Correo,
                    ComTeleInfractor = infractor.Telefono,
                    ComIdCiudad = 1, // no se encuentra en systrans 2.0
                    ComLicencia = licenciaConduccionInfractor.Numero,
                    ComCategoria = licenciaConduccionInfractor.Categoria,
                    ComSecreExpide = comparendo.MatriculadoId,
                    ComFechaVence = licenciaConduccionInfractor.Vencimiento.HasValue ? licenciaConduccionInfractor.Vencimiento!.Value.ToString("dd/MM/yyyy"): string.Empty,
                    ComTipoInfrac = tipoInfractor.SimitId.IsNullOrEmpty() ? null: (int?)int.Parse(tipoInfractor.SimitId!),
                    CompLicTransito = comparendo.NumeroLicenciaTransito,
                    ComDivipoLicen = comparendo.DivipoMatricula.IsNullOrEmpty() ? null: (int?) int.Parse(comparendo.DivipoMatricula!),
                    //--------------------DATOS BÁSICOS DEL PROPIETARIO -------------------------------//
                    ComIdentificacion = propietario.NumeroDocumento,
                    ComIdTipoDocProp = tipoDocumentoPropietario.SimitId.IsNullOrEmpty() ? null : (int?)int.Parse(tipoDocumentoInfractor.SimitId!),
                    ComNombreProp = $"{propietario.NombrePrincipal} {propietario.NombreSecundario} {propietario.ApellidoPrincipal} {propietario.ApellidoSecundario}",
                    //--------------------DATOS BÁSICOS DE LA EMPRESA Y TARJETA OPERACION -------------------------------//
                    ComNombreEmpresa = comparendo.NombreEmpresa,
                    ComNitEmpresa = comparendo.Nit,
                    ComTarjetaOperacion = comparendo.TarjetaNumero,
                    //--------------------DATOS BÁSICOS DEl AGENTE -------------------------------//
                    CompPlacaAgente = agente.Placa, // OBLIGATORIO SYSTRANS
                    //--------------------DATOS AVANZADOS DEL COMPARENDO -------------------------------//
                    CompObservaciones = comparendo.Observaciones,
                    ComFuga = controlComparendo != null? controlComparendo.Fuga.convertirIntChar(): 'N', // no está definido en base de datos systrans 2 // OBLIGATORIO SYSTRANS
                    ComAcci = 'N', // no está definido en base de datos systrans 2 // OBLIGATORIO SYSTRANS
                    GradoAlcohol = controlComparendo != null? ((controlComparendo.Grado != null) ? controlComparendo.Grado: 0) :0, // OBLIGATORIO
                    //--------------------DATOS AVANZADOS DEL COMPARENDO INMOVILIZACION -------------------------------//
                    ComInmov = controlComparendo != null? controlComparendo.Inmovilizado.convertirIntChar(): 'N', // no está definido en base de datos systrans 2 // OBLIGATORIO SYSTRANS
                    ComPatioInmoviliza = comparendo.Patio,
                    ComDirPatioInmovi = comparendo.PatioDireccion,
                    ComGruaNumero = comparendo.Grua,
                    ComPlacaGrua = comparendo.GruaPlaca,
                    ComConsecutInmovi = comparendo.Consecutivo,
                    //--------------------DATOS DEL TESTIGO ----------------------------------------------------//
                    ComIdentificacionTest = testigo.NumeroDocumento,
                    ComNombreTesti = $"{testigo.NombrePrincipal} {testigo.NombreSecundario} {testigo.ApellidoPrincipal} {testigo.ApellidoSecundario}",
                    ComDirecResTesti = $"{direccionTestigo.ViaPrinTipo} {direccionTestigo.ViaPrinNumero} {direccionTestigo.ViaSecTipo} {direccionTestigo.ViaSecNumero}",
                    ComTeleTestigo = testigo.Telefono,
                    //--------------------DATOS DEL DINERO MONEY DEL COMPARENDO ----------------------------------------------------//
                    ComValor = (codigoInfraccion != null) ? _SALARIOMINIMO * codigoInfraccion.Salarios : 0,
                    ComValAd = 0,
                    ComOrganismo = 25754000, // OBLIGATORIO SYSTRANS
                    ComEstadoCom = comparendo.EsatdoId != null ? comparendo.EsatdoId: 1, // OBLIGATORIO SYSTRANS
                    //----------------------------DATOS EXTRA DEL COMPARENDO------------------------------------------
                    ComPolca = (controlComparendo != null) ?  controlComparendo.Polca.convertirIntChar(): 'N',
                    ComInfraccion = (codigoInfraccion.Codigo != null)? codigoInfraccion.Codigo: string.Empty, // OBLIGATORIO SYSTRANS
                    ComValInfra = (codigoInfraccion != null) ? _SALARIOMINIMO * codigoInfraccion.Salarios : 0
                })
                .Skip(numeroComparendosOmitir)
                .Take(numeroComparendosParaObtener)
                .AsNoTracking()
                .ToListAsync(); 

                return comparendoCompletoSimulacionSimit;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw new Exception($"Error: {ex.Message}");
            }
            
        }
    }
}