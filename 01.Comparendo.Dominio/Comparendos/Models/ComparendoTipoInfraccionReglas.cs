namespace _01.Comparendo.Dominio.Comparendos.Models
{
    public class ComparendoTipoInfraccionReglas: ComparendoAuditoria
    {
        public Guid Id { get; set; }
        public string Condiciones { get; set; } = null!;
        public int Salarios { get; set; }
        public int TiempoSuspencion { get; set; }
        public int DiasRetenido { get; set; }
        public int ServicioComunitario { get; set; }
        public bool ActivoDB { get; set; }
        // --------------------------Relacion Comparendo Tipo Infraccion--------------------
        // relacion de muchos ComparendoTipoInfracciónReglas a un ComparendoTipoInfraccion
        public Guid ComparendoTipoInfraccionId { get; set; }
        public ComparendoTipoInfraccion comparendoTipoInfraccion { get; set; } = null!;
        public Guid ClienteId { get; set; }
    }
}