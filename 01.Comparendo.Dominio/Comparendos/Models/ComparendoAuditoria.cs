namespace _01.Comparendo.Dominio.Comparendos.Models
{
    public class ComparendoAuditoria
    {
        public Guid UsuarioCreadorId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public Guid UsuarioActualizadorId { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}