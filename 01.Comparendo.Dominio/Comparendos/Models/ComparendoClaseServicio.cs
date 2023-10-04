namespace _01.Comparendo.Dominio.Comparendos.Models
{
    public class ComparendoClaseServicio
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; }

        // relacion de un ComparendoClaseServicio con muchos Comparendos
        public ICollection<Comparendos>? Comparendos {get; set;}
    }
}