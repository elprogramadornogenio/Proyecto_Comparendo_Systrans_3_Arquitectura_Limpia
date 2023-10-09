namespace _01.Comparendo.Dominio.Comparendos.Models
{
    public class SecretariaTransito
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string? Descripcion { get; set; }
        public int? CiudadId { get; set; }
        // relación de una secretaria Licencias Conduccion con muchos comparendos
        public List<Comparendos> ComparendosLicenciaConduccion { get; set; } = new();
        // relacion de una secretaria de transito a muchos comparendos
        public List<Comparendos> ComparendosSecretaria { get; set; } = new();
        // relacion de una secretaria Licencias de Transito a muchos comparendos
        public List<Comparendos> ComparendosLicenciaTransito { get; set; } = new();
        // relacion de una secretaria Matricula a muchos comparendos
        public List<Comparendos> ComparendosMatricula { get; set; } = new();
    }
}