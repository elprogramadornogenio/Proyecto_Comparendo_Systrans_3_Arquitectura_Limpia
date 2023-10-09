namespace _05.Comparendo.Presentacion.Consola.Models
{
    public class ControlComparendo
    {
        public int Id { get; set; }
        public DateTime FechaComparendo { get; set; }
        public string? NroComparendo { get; set; }
        public string? Placa { get; set; }
        public string? Cedula { get; set; }
        public string? Infraccion { get; set; }
        public int Inmovilizado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int Reportado { get; set; }
        public DateTime? FechaReporte { get; set; }
        public int Origen { get; set; }
        public int Estado { get; set; }
        public int? Grado { get; set; }
        public string? Agente { get; set; }
        public int? Naturaleza { get; set; } // comparendo electronico o no
        public int? Fuga { get; set; }
        public int? Polca { get; set; }
        public DateTime? FechaNotificacion { get; set; }
    }
}