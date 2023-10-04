using _01.Comparendo.Dominio.Comparendos.Models;
using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Command.Commands;
using _02.Comparendo.Core.Aplicacion.Comparendo.Repositorio;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace _03.Comparendo.Infraestructura.Persistencia.Comparendo
{
    public class ComparendoAgenteTransitoRepository : IComparendoAgenteTransitoRepository
    {
        private readonly DataContext _contexto;
        public ComparendoAgenteTransitoRepository(DataContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<ComparendoAgenteTransito> crearAgenteIndeterminadoConPlaca(
            ComparendoAgenteTransito agenteTransitoIndeterminadoIdentificadoPorPlaca
        )
        {
            await _contexto.ComparendoAgenteTransito!
                .AddAsync(agenteTransitoIndeterminadoIdentificadoPorPlaca);
            return agenteTransitoIndeterminadoIdentificadoPorPlaca;
        }

        public async Task<ComparendoAgenteTransito> existeAgenteTransitoPorPlaca(string placa)
        {
            var existeAgenteTransito = await _contexto.ComparendoAgenteTransito!
                .SingleOrDefaultAsync(agenteTransito => agenteTransito.Placa.Equals(placa));
            return existeAgenteTransito!;
        }
    }
}