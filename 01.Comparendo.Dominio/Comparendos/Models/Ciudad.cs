namespace _01.Comparendo.Dominio.Comparendos.Models
{
    public class Ciudad
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string? Descripcion { get; set; }
        public int? DepartamentoId { get; set; }
        // relación una ciudad con muchos comparendos
        public List<Comparendos> ComparendosDireccion { get; set; } = new();
        // relación una ciudad del infractor con muchos comparendos
        public List<Comparendos> ComparendosDireccionInfractor { get; set; } = new();
    }
}