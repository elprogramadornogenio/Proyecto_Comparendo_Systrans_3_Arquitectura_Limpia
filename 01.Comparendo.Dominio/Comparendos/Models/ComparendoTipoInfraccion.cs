namespace _01.Comparendo.Dominio.Comparendos.Models
{
    public class ComparendoTipoInfraccion: ComparendoAuditoria
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public bool Activo { get; set; }
        public bool Especial { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public bool InfraccionMunicipal { get; set; }
        public bool PagoRequerido { get; set; }
        public bool Pedagogica { get; set; }
        public Guid ClienteId { get; set; }
        public bool ActivoDB { get; set; }
        // relaciones de un ComparendoTipoInfracción a muchos ComparendosTipoInfracciónReglas
        public ICollection<ComparendoTipoInfraccionReglas>? ComparendoTipoInfraccionReglas { get; set; }
        
        //----- relacion de muchos Comparendos Tipo Infraccion con muchos Comparendos---------
        // realizar la relacion de un Comparendo Tipo Infraccion con muchos ComparendoInfraccionComparendo
        public ICollection<ComparendoInfraccionComparendo>? ComparendoInfraccionComparendos { get; set; }
    
    }
}