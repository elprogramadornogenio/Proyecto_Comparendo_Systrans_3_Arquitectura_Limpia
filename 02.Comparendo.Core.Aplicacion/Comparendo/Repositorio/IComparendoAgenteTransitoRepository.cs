using _01.Comparendo.Dominio.Comparendos.Models;
using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Command.Commands;

namespace _02.Comparendo.Core.Aplicacion.Comparendo.Repositorio
{
    public interface IComparendoAgenteTransitoRepository
    {
        Task<ComparendoAgenteTransito> existeAgenteTransitoPorPlaca(string placa);
        Task<ComparendoAgenteTransito> crearAgenteIndeterminadoConPlaca(
            ComparendoAgenteTransito agenteTransitoIdentificadoConPlaca);
    }
}