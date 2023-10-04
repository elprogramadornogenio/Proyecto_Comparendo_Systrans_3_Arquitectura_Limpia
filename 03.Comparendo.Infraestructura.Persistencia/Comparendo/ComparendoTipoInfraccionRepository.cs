using _01.Comparendo.Dominio.Comparendos.Models;
using _02.Comparendo.Core.Aplicacion.Comparendo.Repositorio;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace _03.Comparendo.Infraestructura.Persistencia.Comparendo
{
    public class ComparendoTipoInfraccionRepository : IComparendoTipoInfraccionRepository
    {
        private readonly DataContext _contexto;
        public ComparendoTipoInfraccionRepository(DataContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> existeInfraccion(string codigoInfraccion)
        {
            var existeTipoInfraccion = await _contexto.ComparendoTipoInfraccion!
                .AnyAsync(tipoInfraccion => tipoInfraccion.Codigo.Equals(codigoInfraccion));
            return existeTipoInfraccion;
        }

        public async Task<ComparendoTipoInfraccion> traerInfraccionPorCodigo(string codigoInfraccion)
        {
            var datosTipoInfraccion = await _contexto.ComparendoTipoInfraccion!
                .FirstOrDefaultAsync(tipoInfraccion => tipoInfraccion.Codigo
                    .Equals(codigoInfraccion));
            return datosTipoInfraccion!;
        }
    }
}