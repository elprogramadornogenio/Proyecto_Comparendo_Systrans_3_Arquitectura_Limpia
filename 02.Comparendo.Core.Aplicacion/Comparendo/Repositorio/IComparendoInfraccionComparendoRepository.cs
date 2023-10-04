using _01.Comparendo.Dominio.Comparendos.Models;

namespace _02.Comparendo.Core.Aplicacion.Comparendo.Repositorio
{
    public interface IComparendoInfraccionComparendoRepository
    {
        Task<ComparendoInfraccionComparendo> crearRelacionComparendoTipoInfraccion(
            Comparendos datosComparendos,
            ComparendoTipoInfraccion datosInfraccion,
            decimal valorInfraccion
        );        
    }
}