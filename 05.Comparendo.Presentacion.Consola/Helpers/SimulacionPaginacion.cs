namespace _05.Comparendo.Presentacion.Consola.Helpers
{
    public static class SimulacionPaginacion
    {
        public static List<Rango> obtenerRangos(
            int numeroTotalComparendos, int numeroComparendosObtener)
        {
            var rangos = new List<Rango>();
            var definirNumeroRangoComparendo = 
                (numeroTotalComparendos / numeroComparendosObtener);
            var residuo = numeroTotalComparendos % numeroComparendosObtener;

            for (int i = 0; i < definirNumeroRangoComparendo; i++)
            {
                rangos.Add(new Rango 
                {
                    Inicio = i*numeroComparendosObtener, 
                    Fin = (i+1)*numeroComparendosObtener
                });
            }
            if( residuo > 0)
            {
                rangos.Add(new Rango 
                { 
                    Inicio = definirNumeroRangoComparendo * numeroComparendosObtener, 
                    Fin = numeroTotalComparendos 
                });
            }

            return rangos;
        }
    }
}