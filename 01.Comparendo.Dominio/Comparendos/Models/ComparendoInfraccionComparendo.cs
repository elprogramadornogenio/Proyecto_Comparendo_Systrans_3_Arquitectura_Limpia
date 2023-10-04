namespace _01.Comparendo.Dominio.Comparendos.Models
{
    public class ComparendoInfraccionComparendo
    {
        public Guid Id { get; set; }

        // relacion de un ComparendoInfraccionComparendo con muchos Comparendos
        public Guid ComaprendoId { get; set; }
        public Comparendos Comparendo { get; set; } = null!;
        // relacion de un ComparendoInfraccionComparendo con muchos ComparendoTipoInfraccion
        public Guid ComparendoTipoInfraccionId { get; set; }
        public ComparendoTipoInfraccion ComparendoTipoInfraccion { get; set; } = null!;
        public decimal ValorInfraccion { get; set; }
    }
}