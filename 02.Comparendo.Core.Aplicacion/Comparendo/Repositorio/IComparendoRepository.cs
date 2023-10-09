using _01.Comparendo.Dominio.Comparendos.Models;
using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Query.DTO;

namespace _02.Comparendo.Core.Aplicacion.Comparendo.Repositorio
{
    public interface IComparendoRepository
    {
        Task<Guid> agregarComparendoConDependencias(
            Comparendos comparendo,
            string codigoInfraccion,
            string placaAgente,
            decimal valorInfraccion);
        Task<Comparendos> traerComparendoPorNumeroYCodigoInfraccion(
            int tipoDocumentoInfractor,
            string documentoInfractor,
            string numeroComparendo, 
            string codigoInfraccion);
            
        Task<bool> existeComparendoPorNumero(string numeroComparendo);
        Task<bool> existeTipoVehiculoPorId(int? id);
        Task<bool> existeClaseServicioPorId(int? id);
        Task<bool> existeComparendoEstadoPorId(int? id);
        Task<SecretariaTransito> traerSecretariaTransito(string codigoSecretariaTransito);
        Task<Ciudad> traerCiudadComparendo(string codigoCiudad);
        Task<List<ComparendoSimitDto>> getAll();
        Task<List<ComparendoSimitDto>> getFilter(
            int numeroComparendoOmitir, 
            int numeroComparendosConsultar);
    }
}