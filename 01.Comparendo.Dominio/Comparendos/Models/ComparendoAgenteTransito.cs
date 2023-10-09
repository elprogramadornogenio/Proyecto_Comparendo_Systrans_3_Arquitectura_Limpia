using _01.Comparendo.Dominio.Comparendos.enums;

namespace _01.Comparendo.Dominio.Comparendos.Models
{
    public class ComparendoAgenteTransito: ComparendoAuditoria
    {
        public Guid Id { get; set; }
        public int? TipoDocumentoId { get; set; }
        public string? Documento { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Placa { get; set; } = null!;
        public eEntidadAgente Entidad { get; set; }
        public eEstado Estado { get; set; }
        public Guid ClienteId { get; set; }
        public bool ActivoDB { get; set; }
        public bool Activo { get; set; }
        // relacion de muchos comparendos
        public List<Comparendos>? Comparendos { get; set; } = new();
    }
}