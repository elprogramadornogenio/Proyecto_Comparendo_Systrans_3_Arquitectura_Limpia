namespace _01.Comparendo.Dominio.Comparendos.Models
{
    public class ComparendoTipoInfractor
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; }
        // relacion de un ComparendoTipoInfractor a muchos Comparendos
        public List<Comparendos>? Comparendos { get; set; }
    }
}