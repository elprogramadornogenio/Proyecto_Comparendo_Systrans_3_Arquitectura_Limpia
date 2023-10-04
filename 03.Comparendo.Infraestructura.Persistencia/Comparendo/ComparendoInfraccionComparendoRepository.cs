using _01.Comparendo.Dominio.Comparendos.Models;
using _02.Comparendo.Core.Aplicacion.Comparendo.Repositorio;

namespace _03.Comparendo.Infraestructura.Persistencia.Comparendo
{
    public class ComparendoInfraccionComparendoRepository : IComparendoInfraccionComparendoRepository
    {
        private readonly DataContext _contexto;
        public ComparendoInfraccionComparendoRepository(DataContext context)
        {
            _contexto = context;
        }
        public async Task<ComparendoInfraccionComparendo> crearRelacionComparendoTipoInfraccion(
            Comparendos datosComparendos, 
            ComparendoTipoInfraccion datosInfraccion,
            decimal valorInfraccion
            )
        {
            var relacionComparendoTipoInfraccion = new ComparendoInfraccionComparendo
            {
                Comparendo = datosComparendos,
                ComparendoTipoInfraccion = datosInfraccion,
                ValorInfraccion = valorInfraccion
            };

            await _contexto.ComparendoInfraccionComparendo!
                .AddAsync(relacionComparendoTipoInfraccion);
            
            return relacionComparendoTipoInfraccion;
        }
    }
}