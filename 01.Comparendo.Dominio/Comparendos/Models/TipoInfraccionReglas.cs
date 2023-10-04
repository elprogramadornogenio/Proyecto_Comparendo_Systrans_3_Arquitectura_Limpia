namespace _01.Comparendo.Dominio.Comparendos.Models
{
    public class TipoInfraccionReglas
    {
        public Guid Id { get; set; }
        public int Salarios { get; set; }
        public int TiempoSuspencion { get; set; }
        public int DiasRetenido { get; set; }
        public int ServicioComunitario { get; set; }
        public bool PagoRequerido { get; set; }
        public bool Pedagogica { get; set; }
        public int GradoAlcohol { get; set; }
        public int TipoReincidencia { get; set; }
        public int ClaseServicioId { get; set; }
        public Guid ComparendoTipoInfraccionId { get; set; }
        public Guid ClienteId { get; set; }
        public bool ActivoDB { get; set; }
    }
}