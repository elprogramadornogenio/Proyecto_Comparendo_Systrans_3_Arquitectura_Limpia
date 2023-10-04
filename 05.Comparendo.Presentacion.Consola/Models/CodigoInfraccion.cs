namespace _05.Comparendo.Presentacion.Consola.Models
{
    public class CodigoInfraccion
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string? Descripcion { get; set; }
        public int Salarios { get; set; }
        public int DiasVencimiento { get; set; }
        public decimal PorcentajeDescuento { get; set; }
        public decimal PporcentajeRecargo { get; set; }
        public int TipoComparendos { get; set; }
        public string? Inmovilizacion { get; set; }
        public int Estado { get; set; }
    }
}