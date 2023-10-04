using _01.Comparendo.Dominio.Comparendos.enums;

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
        public int SecretariaId { get; set; }
        //--------- relacion de un ComparendoClaseServicio con muchos comparendos----------
        public int ClaseServicioId { get; set; }
        public ComparendoClaseServicio? ClaseServicio { get; set; }
        //--------- relacion de un Comparendo estado con muchos comparendos-----------------
        public int EstadoComparendoId { get; set; }
        public ComparendoEstado? ComparendoEstado { get; set; }
        public int MunicipioDireccionId { get; set; }
        public int InfractorTipoDocumentoId { get; set; }
        public int? LicenciaConduccionSecretariaId { get; set; }
        public int? TipoInfractorId { get; set; }
        public int? MatriculaSecretariaId { get; set; }
        public int? LicenciaTransitoSecretariaId { get; set; }
        public int? TipoDocumentoPropietarioId { get; set; }
        public int? CiudadInfractorId { get; set; }
        public Guid ClienteId { get; set; }
    }
}