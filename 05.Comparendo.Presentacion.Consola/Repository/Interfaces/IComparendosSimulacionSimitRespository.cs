using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Command.Commands;

namespace _05.Comparendo.Presentacion.Consola.Repository.Interfaces
{
    public interface IComparendosSimulacionSimitRespository
    {
        Task<IEnumerable<CrearComparendoCommand>> listarComparendos();
        Task<IEnumerable<CrearComparendoCommand>> obtenerRangoListaComparendos(
            int numeroComparendosOmitir, int numeroComparendosParaObtener);
        Task<int> obtenerNumeroComparendosTotales();
    }
}