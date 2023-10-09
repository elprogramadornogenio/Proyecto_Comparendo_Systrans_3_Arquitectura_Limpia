using _01.Comparendo.Dominio.Comparendos.enums;
using _01.Comparendo.Dominio.Comparendos.Parametricas;

namespace _01.Comparendo.Dominio.Comparendos.Models
{
    public class Comparendos : ComparendoAuditoria
    {
        public Guid Id { get; set; }
        public string Numero { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public DateTime Hora { get; set; }
        public string Direccion { get; set; } = null!;
        public string? Placa { get; set; }
        public string DocumentoInfractor { get; set; } = null!;
        public string? NombreInfractor { get; set; }
        public string? ApellidoInfractor { get; set; }
        public string? LicenciaConduccion { get; set; }
        public string? LicenciaConduccionCategoria { get; set; }
        public DateTime? LicenciaVence { get; set; }
        public string? DireccionInfractor { get; set; }
        public string? TelefonoInfractor { get; set; }
        public int EdadInfractor { get; set; }
        public string? EmailInfractor { get; set; }
        public string? LicenciaTransito { get; set; }
        public string? Observaciones { get; set; }
        public bool Fuga { get; set; }
        public bool Accidente { get; set; }
        public bool Inmobilizacion { get; set; }
        public decimal ValorComparendo { get; set; }
        public decimal ValorAdicional { get; set; }
        public bool Polca { get; set; }
        public string? Localidad { get; set; }
        public eRadioAccion? CodigoRadio { get; set; }
        public eModalidadTransporte? CodigoModalidad { get; set; }
        public eCodigoPasajeros? CodigoPasajeros { get; set; }
        public string? DocumentoPropietario { get; set; }
        public string? NombrePropietario { get; set; }
        public string? ApellidoPropietario { get; set; }
        public string? NombreEmpresa { get; set; }
        public string? NitEmpresa { get; set; }
        public string? TarjetaOperacion { get; set; }
        public string? PatioInmoviliza { get; set; }
        public string? DireccionPatioInmoviliza { get; set; }
        public string? GruaNumero { get; set; }
        public string? GruaPlaca { get; set; }
        public string? ConsecutivoInmovilizacion { get; set; }
        public string? DocumentoTestigo { get; set; }
        public string? NombreTestigo { get; set; }
        public string? ApellidoTestigo { get; set; }
        public string? DireccionTestigo { get; set; }
        public string? TelefonoTestigo { get; set; }
        public bool ComparendoElectronico { get; set; }
        public DateTime? FechaNotificacion { get; set; }
        public int GradoAlcohol { get; set; }
        public eFuenteComparendo? Fuente { get; set; }
        public string? Latitud { get; set; }
        public string? Longitud { get; set; }
        public bool ActivoDB { get; set; }
        //------------------------------ relaciones--------------------------------------
        //----------- relacion de un agente a muchos comparendos-------------------------
        public Guid AgenteTransitoId { get; set; }
        public ComparendoAgenteTransito? AgenteTransito { get; set; }

        // ------relacion de muchos Comparendos con muchos ComparendoTipoInfraccion-------
        // realizar relacion de un comparendo con muchos ComparendoInfraccionComparendo---
        public ICollection<ComparendoInfraccionComparendo>? ComparendoInfraccionComparendos { get; set; }
        //--------- relacion de un ComparendoTipoVehiculo con muchos comparendos----------
        public int TipoVehiculoId { get; set; }
        public ComparendoTipoVehiculo? TipoVehiculo { get; set; }
        //--------- relacion de un ComparendoClaseServicio con muchos comparendos----------
        public int ClaseServicioId { get; set; }
        public ComparendoClaseServicio? ClaseServicio { get; set; }
        // -------- relacion de un ComparendoTipoInfractor con muchos comparendos-----------
        public int? TipoInfractorId { get; set; }
        public ComparendoTipoInfractor? TipoInfractor { get; set; }
        // --------- relación de un InfractorTipoDocumento con muchos comparendos ----------
        public int InfractorTipoDocumentoId { get; set; }
        //---------- relación de una LicenciaConducciónSecretaria con muchos comparendos ---
        public int? LicenciaConduccionSecretariaId { get; set; }
        public SecretariaTransito? LicenciaConduccionSecretaria { get; set; }
        //--------- relacion de una secretaria de transito con muchos comparendos----------
        public int SecretariaId { get; set; }
        public SecretariaTransito? SecretariaTransito { get; set; }
        //--------- relacion de un Comparendo estado con muchos comparendos-----------------
        public int EstadoComparendoId { get; set; }
        public ComparendoEstado? ComparendoEstado { get; set; }

        //--------- relación de una Ciudad con muchos comparendos --------------------------
        public int MunicipioDireccionId { get; set; }
        public Ciudad? Ciudad { get; set; }
        //--------- relacion de una secretaria de matricula a muchos comparendos
        public int? MatriculaSecretariaId { get; set; }
        public SecretariaTransito? SecretariaTransitoMatriculado { get; set; }
        // --------- relacion de una secretaria que exide licencia de transito con muchos comparendos
        public int? LicenciaTransitoSecretariaId { get; set; }
        public SecretariaTransito? SecretariaLicenciaTransito { get; set; }
        // --------- relacion de un Tipo Documento Propietario con muchos comparendos ----------
        public int? TipoDocumentoPropietarioId { get; set; }
        // --------- relacion de una Ciudad del infractor con muchos comparendos ------------
        public int? CiudadInfractorId { get; set; }
        public Ciudad? CiudadDelInfractor { get; set; }
        public Guid ClienteId { get; set; }
    }
}