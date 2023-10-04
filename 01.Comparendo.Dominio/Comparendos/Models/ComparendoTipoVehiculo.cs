namespace _01.Comparendo.Dominio.Comparendos.Models
{
    public class ComparendoTipoVehiculo
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = null!;

        //-------------------relacion de un ComparendoTipoVehiculo a muchos comparendos------
        public ICollection<Comparendos>? Comparendos { get; set; }
    }
}