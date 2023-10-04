namespace _01.Comparendo.Dominio.Comparendos.Models
{
    public class ComparendoEstado
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; }
        
        // relacion de un ComparendoEstado con muchos comparendos
        public ICollection<Comparendos>? Comparendos {get; set;}
    }
}