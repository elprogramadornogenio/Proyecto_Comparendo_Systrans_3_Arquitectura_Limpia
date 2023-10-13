using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Query.DTOs;

namespace _05.Comparendo.Presentacion.Consola.Extension
{
    public static class ConversionComparendoEstandarSimitEscrituraArchivo
    {
        public static string generarLineaTextoComparendoEstandarSimit(
            this ComparendoEstandarSimitDto comparendoEstandarSimit) 
        {
            var lineaComparendo =
                //--------------------DATOS BÁSICOS DEL COMPARENDO ---------------------------//
                $"{comparendoEstandarSimit.ComConsecutivo},"+ 
                $"{comparendoEstandarSimit.ComNumero},"+
                $"{comparendoEstandarSimit.ComFecha},"+
                $"{comparendoEstandarSimit.ComHora ?? string.Empty},"+
                $"{comparendoEstandarSimit.ComDir ?? string.Empty},"+
                $"{((comparendoEstandarSimit.ComDivipoMuni.HasValue) ? 
                    comparendoEstandarSimit.ComDivipoMuni: string.Empty)},"+
                $"{comparendoEstandarSimit.ComLocalidadComuna ?? string.Empty},"+
                $"{comparendoEstandarSimit.ComPlaca ?? string.Empty},"+
                $"{((comparendoEstandarSimit.ComDivipoMatri.HasValue) ?
                    comparendoEstandarSimit.ComDivipoMatri: string.Empty)},"+
                $"{((comparendoEstandarSimit.ComTipoVehi.HasValue) ?
                    comparendoEstandarSimit.ComTipoVehi: string.Empty)},"+
                $"{((comparendoEstandarSimit.ComTipoVehi.HasValue) ?
                    comparendoEstandarSimit.ComTipoSer: string.Empty)},"+
                $"{((comparendoEstandarSimit.ComCodigoRadio.HasValue) ?
                    comparendoEstandarSimit.ComCodigoRadio: string.Empty)},"+
                $"{((comparendoEstandarSimit.ComCodigoModalidad.HasValue) ?
                    comparendoEstandarSimit.ComCodigoModalidad: string.Empty)},"+
                $"{((comparendoEstandarSimit.ComCodigoPasajeros.HasValue) ?
                    comparendoEstandarSimit.ComCodigoPasajeros: string.Empty)},"+
                //--------------------DATOS BÁSICOS DEL INFRACTOR ----------------------------//
                $"{comparendoEstandarSimit.ComInfraccion},"+
                $"{comparendoEstandarSimit.ComTipoDoc},"+
                $"{((comparendoEstandarSimit.ComEdadInfractor.HasValue) ?
                    comparendoEstandarSimit.ComEdadInfractor: string.Empty)},"+
                $"{comparendoEstandarSimit.ComDirInfractor ?? string.Empty},"+
                $"{comparendoEstandarSimit.ComEmail ?? string.Empty},"+
                $"{comparendoEstandarSimit.ComTeleInfractor ?? string.Empty},"+
                $"{((comparendoEstandarSimit.ComIdCiudad.HasValue) ?
                    comparendoEstandarSimit.ComIdCiudad : string.Empty)},"+
                $"{comparendoEstandarSimit.ComLicencia ?? string.Empty},"+
                $"{comparendoEstandarSimit.ComCategoria ?? string.Empty},"+
                $"{((comparendoEstandarSimit.ComSecreExpide.HasValue) ?
                    comparendoEstandarSimit.ComSecreExpide: string.Empty)},"+
                $"{comparendoEstandarSimit.ComFechaVence ?? string.Empty},"+
                $"{((comparendoEstandarSimit.ComTipoInfrac.HasValue) ? 
                    comparendoEstandarSimit.ComTipoInfrac: string.Empty)},"+
                $"{comparendoEstandarSimit.CompLicTransito ?? string.Empty},"+
                $"{((comparendoEstandarSimit.ComDivipoLicen.HasValue) ?
                    comparendoEstandarSimit.ComDivipoLicen: string.Empty)},"+
                //--------------------DATOS BÁSICOS DEL PROPIETARIO ----------------------------//
                $"{comparendoEstandarSimit.ComIdentificacion ?? string.Empty},"+
                $"{((comparendoEstandarSimit.ComIdTipoDocProp.HasValue) ?
                    comparendoEstandarSimit.ComIdTipoDocProp: string.Empty)},"+
                $"{comparendoEstandarSimit.ComNombreProp ?? string.Empty},"+
                //--------------------DATOS BÁSICOS DE LA EMPRESA Y TARJETA OPERACION ----------//
                $"{comparendoEstandarSimit.ComNombreEmpresa ?? string.Empty},"+
                $"{comparendoEstandarSimit.ComNitEmpresa ?? string.Empty},"+
                $"{comparendoEstandarSimit.ComTarjetaOperacion ?? string.Empty},"+
                //--------------------DATOS BÁSICOS DEl AGENTE ---------------------------------//
                $"{comparendoEstandarSimit.CompPlacaAgente ?? string.Empty},"+
                //--------------------DATOS AVANZADOS DEL COMPARENDO ---------------------------//
                $"{comparendoEstandarSimit.CompObservaciones ?? string.Empty},"+
                $"{((comparendoEstandarSimit.ComFuga.HasValue) ?
                    comparendoEstandarSimit.ComFuga: string.Empty)},"+
                $"{((comparendoEstandarSimit.ComAcci.HasValue) ?
                    comparendoEstandarSimit.ComAcci: string.Empty)},"+
                //--------------------DATOS AVANZADOS DEL COMPARENDO INMOVILIZACION ------------//
                $"{((comparendoEstandarSimit.ComInmov.HasValue) ?
                    comparendoEstandarSimit.ComInmov: string.Empty)},"+
                $"{comparendoEstandarSimit.ComPatioInmoviliza ?? string.Empty},"+
                $"{comparendoEstandarSimit.ComDirPatioInmovi ?? string.Empty},"+
                $"{comparendoEstandarSimit.ComGruaNumero ?? string.Empty},"+
                $"{comparendoEstandarSimit.ComPlacaGrua ?? string.Empty},"+
                $"{comparendoEstandarSimit.ComConsecutInmovi ?? string.Empty},"+
                //--------------------DATOS DEL TESTIGO -----------------------------------------//
                $"{comparendoEstandarSimit.ComIdentificacionTest ?? string.Empty},"+
                $"{comparendoEstandarSimit.ComNombreTesti ?? string.Empty},"+
                $"{comparendoEstandarSimit.ComDirecResTesti ?? string.Empty},"+
                $"{comparendoEstandarSimit.ComTeleTestigo ?? string.Empty},"+
                //--------------------DATOS DEL DINERO MONEY DEL COMPARENDO --------------------//
                $"{comparendoEstandarSimit.ComValor},"+
                $"{comparendoEstandarSimit.ComValAd},"+
                $"{comparendoEstandarSimit.ComOrganismo},"+
                $"{comparendoEstandarSimit.ComEstadoCom},"+
                //----------------------------DATOS EXTRA DEL COMPARENDO--------------------------//
                $"{((comparendoEstandarSimit.ComPolca.HasValue) ?
                    comparendoEstandarSimit.ComPolca: string.Empty)},"+
                $"{comparendoEstandarSimit.ComInfraccion},"+
                $"{comparendoEstandarSimit.ComValInfra},"+
                //----------------------------DATOS DEL TUTOR-------------------------------------//
                $"{((comparendoEstandarSimit.Id_Tipo_Doc_Tutor.HasValue) ?
                    comparendoEstandarSimit.Id_Tipo_Doc_Tutor: string.Empty)},"+
                $"{comparendoEstandarSimit.Nro_Doc_Tutor ?? string.Empty},"+
                $"{comparendoEstandarSimit.Nombre_Tutor ?? string.Empty},"+
                $"{comparendoEstandarSimit.Apellido_Tutor ?? string.Empty}"+
                //----------------------------DATOS COMPARENDO ELECTRÓNICO------------------------//
                $"{((comparendoEstandarSimit.FotoMulta.HasValue) ?
                    comparendoEstandarSimit.FotoMulta: string.Empty)},"+
                $"{comparendoEstandarSimit.FechaNotificacion ?? string.Empty},"+
                $"{((comparendoEstandarSimit.FuenteComparendo.HasValue) ?
                    comparendoEstandarSimit.FuenteComparendo: string.Empty)},"+
                $"{comparendoEstandarSimit.LatitudComparendo ?? string.Empty},"+
                $"{comparendoEstandarSimit.LongitudComparendo ?? string.Empty}"
                ;
            return lineaComparendo;
        }
    }
}