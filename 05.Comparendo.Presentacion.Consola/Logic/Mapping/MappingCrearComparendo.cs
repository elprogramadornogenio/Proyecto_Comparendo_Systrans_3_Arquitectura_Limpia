using _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Command.Commands;
using _05.Comparendo.Presentacion.Consola.Extension;

namespace _05.Comparendo.Presentacion.Consola.Logic.Mapping
{
    public class MappingCrearComparendo
    {
        public MappingCrearComparendo() {}
        public CrearComparendoCommand? mappearCrearComparendoCommand(string[] comparendo)
        {
            if(comparendo.Length <= 4) return null;

            var crearComparendoCommand = new CrearComparendoCommand{};
            //--------------------DATOS BÁSICOS DEL COMPARENDO -------------------------------//
            crearComparendoCommand.ComNumero = comparendo[1];
            crearComparendoCommand.ComFecha = comparendo[2];
            crearComparendoCommand.ComHora = comparendo[3];
            crearComparendoCommand.ComDir = comparendo[4];
            crearComparendoCommand.ComDivipoMuni = comparendo[5].conversionStringInt();
            crearComparendoCommand.ComLocalidadComuna = comparendo[6];
            crearComparendoCommand.ComPlaca = comparendo[7];
            crearComparendoCommand.ComDivipoMatri = comparendo[8].conversionStringInt();
            crearComparendoCommand.ComTipoVehi = comparendo[9].conversionStringInt();
            crearComparendoCommand.ComTipoSer = comparendo[10].conversionStringInt();
            crearComparendoCommand.ComCodigoRadio = comparendo[11].conversionStringInt();
            crearComparendoCommand.ComCodigoModalidad = comparendo[12].conversionStringInt();
            crearComparendoCommand.ComCodigoPasajeros = comparendo[13].conversionStringInt();
            //--------------------DATOS BÁSICOS DEL INFRACTOR -------------------------------//
            crearComparendoCommand.ComInfractor = comparendo[14];
            crearComparendoCommand.ComTipoDoc = comparendo[15].conversionStringInt();
            crearComparendoCommand.ComNombre = comparendo[16];
            crearComparendoCommand.ComApellido = comparendo[17];
            crearComparendoCommand.ComEdadInfractor = comparendo[18].conversionStringInt();
            crearComparendoCommand.ComDirInfractor = comparendo[19];
            crearComparendoCommand.ComEmail = comparendo[20];
            crearComparendoCommand.ComTeleInfractor = comparendo[21];
            crearComparendoCommand.ComIdCiudad = comparendo[22].conversionStringInt();
            crearComparendoCommand.ComLicencia = comparendo[23];
            crearComparendoCommand.ComCategoria = comparendo[24];
            crearComparendoCommand.ComSecreExpide = comparendo[25].conversionStringInt();
            crearComparendoCommand.ComFechaVence = comparendo[26];
            crearComparendoCommand.ComTipoInfrac = comparendo[27].conversionStringInt();
            crearComparendoCommand.CompLicTransito = comparendo[28];
            crearComparendoCommand.ComDivipoLicen = comparendo[29].conversionStringInt();
            //--------------------DATOS BÁSICOS DEL PROPIETARIO -------------------------------//
            crearComparendoCommand.ComIdentificacion = comparendo[30];
            crearComparendoCommand.ComIdTipoDocProp = comparendo[31].conversionStringInt();
            crearComparendoCommand.ComNombreProp = comparendo[32];
            //--------------------DATOS BÁSICOS DE LA EMPRESA Y TARJETA OPERACION --------------//
            crearComparendoCommand.ComNombreEmpresa = comparendo[33];
            crearComparendoCommand.ComNitEmpresa = comparendo[34];
            crearComparendoCommand.ComTarjetaOperacion = comparendo[35];
            //--------------------DATOS BÁSICOS DEl AGENTE -------------------------------//
            crearComparendoCommand.CompPlacaAgente = comparendo[36];
            //--------------------DATOS AVANZADOS DEL COMPARENDO -------------------------------//
            crearComparendoCommand.CompObservaciones = comparendo[37];
            crearComparendoCommand.ComFuga = comparendo[38].conversionStringChar();
            crearComparendoCommand.ComAcci = comparendo[39].conversionStringChar();
            //--------------------DATOS AVANZADOS DEL COMPARENDO INMOVILIZACION -------------------------------//
            crearComparendoCommand.ComInmov = comparendo[38].conversionStringChar();
            crearComparendoCommand.ComPatioInmoviliza = comparendo[41];
            crearComparendoCommand.ComDirPatioInmovi = comparendo[42];
            crearComparendoCommand.ComGruaNumero = comparendo[43];
            crearComparendoCommand.ComPlacaGrua = comparendo[44];
            crearComparendoCommand.ComConsecutInmovi = comparendo[45];
            //--------------------DATOS DEL TESTIGO ---------------------------------------//
            crearComparendoCommand.ComIdentificacionTest = comparendo[46];
            crearComparendoCommand.ComNombreTesti = comparendo[47];
            crearComparendoCommand.ComDirecResTesti = comparendo[48];
            crearComparendoCommand.ComTeleTestigo = comparendo[49];
            //--------------------DATOS DEL DINERO MONEY DEL COMPARENDO --------------------------------//
            crearComparendoCommand.ComValor = comparendo[50].conversionStringDecimal();
            crearComparendoCommand.ComValAd = comparendo[51].conversionStringDecimal();
            crearComparendoCommand.ComOrganismo = comparendo[52].conversionStringInt();
            crearComparendoCommand.ComEstadoCom = comparendo[53].conversionStringInt();
            //----------------------------DATOS EXTRA DEL COMPARENDO---------------------------------
            crearComparendoCommand.ComPolca = comparendo[54].conversionStringChar();
            crearComparendoCommand.ComInfraccion = comparendo[55];
            crearComparendoCommand.ComValInfra = comparendo[56].conversionStringDecimal();
            /*
            //----------------------------DATOS DEL TUTOR------------------------------------------
            crearComparendoCommand.Id_Tipo_Doc_Tutor = comparendo[57].conversionStringInt();
            crearComparendoCommand.Nro_Doc_Tutor = comparendo[58];
            crearComparendoCommand.Nombre_Tutor = comparendo[59];
            crearComparendoCommand.Apellido_Tutor = comparendo[60];
            //----------------------------DATOS COMPARENDO ELECTRÓNICO------------------------------------------
            crearComparendoCommand.FotoMulta = comparendo[61].conversionStringChar();
            crearComparendoCommand.FechaNotificacion = comparendo[62];
            crearComparendoCommand.FuenteComparendo = comparendo[63].conversionStringInt();
            crearComparendoCommand.LatitudComparendo = comparendo[64];
            crearComparendoCommand.LongitudComparendo = comparendo[65];
            */
            return crearComparendoCommand;
        }
    }
}