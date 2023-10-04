using _01.Comparendo.Dominio.Comparendos.Models;

namespace _02.Comparendo.Core.Aplicacion.Comparendo.Repositorio
{
    public interface IComparendoTipoInfraccionRepository
    {
        Task<bool> existeInfraccion(string codigoInfraccion);
        Task<ComparendoTipoInfraccion> traerInfraccionPorCodigo(string codigoInfraccion);
    }
}