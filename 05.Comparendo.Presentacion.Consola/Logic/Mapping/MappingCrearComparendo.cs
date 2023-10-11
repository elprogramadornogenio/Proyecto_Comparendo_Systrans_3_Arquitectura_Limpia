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
            crearComparendoCommand.ComNumero = (comparendo.Length > 1)? comparendo[1]: null;
            crearComparendoCommand.ComFecha = (comparendo.Length > 2)? comparendo[2]: null;
            crearComparendoCommand.ComHora = (comparendo.Length > 3)? comparendo[3]: null;
            crearComparendoCommand.ComDir = (comparendo.Length > 4)? comparendo[4]: null;
            crearComparendoCommand.ComDivipoMuni = (comparendo.Length > 5)? comparendo[5].conversionStringInt(): null;
            crearComparendoCommand.ComLocalidadComuna = (comparendo.Length > 6)? comparendo[6]: null;
            crearComparendoCommand.ComPlaca = (comparendo.Length > 7)? comparendo[7]: null;
            crearComparendoCommand.ComDivipoMatri = (comparendo.Length > 8)? comparendo[8].conversionStringInt(): null;
            crearComparendoCommand.ComTipoVehi = (comparendo.Length > 9)? comparendo[9].conversionStringInt(): null;
            crearComparendoCommand.ComTipoSer = (comparendo.Length > 10)? comparendo[10].conversionStringInt(): null;
            crearComparendoCommand.ComCodigoRadio = (comparendo.Length > 11)? comparendo[11].conversionStringInt(): null;
            crearComparendoCommand.ComCodigoModalidad = (comparendo.Length > 12)? comparendo[12].conversionStringInt(): null;
            crearComparendoCommand.ComCodigoPasajeros = (comparendo.Length > 13)? comparendo[13].conversionStringInt(): null;
            //--------------------DATOS BÁSICOS DEL INFRACTOR -------------------------------//
            crearComparendoCommand.ComInfractor = (comparendo.Length > 14)? comparendo[14]: null;
            crearComparendoCommand.ComTipoDoc = (comparendo.Length > 15)? comparendo[15].conversionStringInt(): null;
            crearComparendoCommand.ComNombre = (comparendo.Length > 16)? comparendo[16]: null;
            crearComparendoCommand.ComApellido = (comparendo.Length > 17)? comparendo[17]: null;
            crearComparendoCommand.ComEdadInfractor = (comparendo.Length > 18)? comparendo[18].conversionStringInt(): null;
            crearComparendoCommand.ComDirInfractor = (comparendo.Length > 19)? comparendo[19]: null;
            crearComparendoCommand.ComEmail = (comparendo.Length > 20)? comparendo[20]: null;
            crearComparendoCommand.ComTeleInfractor = (comparendo.Length > 21)?  comparendo[21]: null;
            crearComparendoCommand.ComIdCiudad = (comparendo.Length > 22)? comparendo[22].conversionStringInt(): null;
            crearComparendoCommand.ComLicencia = (comparendo.Length > 23)? comparendo[23]: null;
            crearComparendoCommand.ComCategoria = (comparendo.Length > 24)? comparendo[24]: null;
            crearComparendoCommand.ComSecreExpide = (comparendo.Length > 25)? comparendo[25].conversionStringInt(): null;
            crearComparendoCommand.ComFechaVence = (comparendo.Length > 26)? comparendo[26]: null;
            crearComparendoCommand.ComTipoInfrac = (comparendo.Length > 27)? comparendo[27].conversionStringInt(): null;
            crearComparendoCommand.CompLicTransito = (comparendo.Length > 28)? comparendo[28]: null;
            crearComparendoCommand.ComDivipoLicen = (comparendo.Length > 29)? comparendo[29].conversionStringInt(): null;
            //--------------------DATOS BÁSICOS DEL PROPIETARIO -------------------------------//
            crearComparendoCommand.ComIdentificacion = (comparendo.Length > 30)? comparendo[30]: null;
            crearComparendoCommand.ComIdTipoDocProp = (comparendo.Length > 31)? comparendo[31].conversionStringInt(): null;
            crearComparendoCommand.ComNombreProp = (comparendo.Length > 32)? comparendo[32]: null;
            //--------------------DATOS BÁSICOS DE LA EMPRESA Y TARJETA OPERACION --------------//
            crearComparendoCommand.ComNombreEmpresa = (comparendo.Length > 33)? comparendo[33]: null;
            crearComparendoCommand.ComNitEmpresa = (comparendo.Length > 34)? comparendo[34]: null;
            crearComparendoCommand.ComTarjetaOperacion = (comparendo.Length > 35)? comparendo[35]: null;
            //--------------------DATOS BÁSICOS DEl AGENTE -------------------------------//
            crearComparendoCommand.CompPlacaAgente = (comparendo.Length > 36)? comparendo[36]: null;
            //--------------------DATOS AVANZADOS DEL COMPARENDO -------------------------------//
            crearComparendoCommand.CompObservaciones = (comparendo.Length > 37)? comparendo[37]: null;
            crearComparendoCommand.ComFuga = (comparendo.Length > 38)? comparendo[38].conversionStringChar(): null;
            crearComparendoCommand.ComAcci = (comparendo.Length > 39)? comparendo[39].conversionStringChar(): null;
            //--------------------DATOS AVANZADOS DEL COMPARENDO INMOVILIZACION -------------------------------//
            crearComparendoCommand.ComInmov = (comparendo.Length > 40)? comparendo[40].conversionStringChar(): null;
            crearComparendoCommand.ComPatioInmoviliza = (comparendo.Length > 41)? comparendo[41]: null;
            crearComparendoCommand.ComDirPatioInmovi = (comparendo.Length > 42)? comparendo[42]: null;
            crearComparendoCommand.ComGruaNumero = (comparendo.Length > 43)? comparendo[43]: null;
            crearComparendoCommand.ComPlacaGrua = (comparendo.Length > 44)? comparendo[44]: null;
            crearComparendoCommand.ComConsecutInmovi = (comparendo.Length > 45)? comparendo[45]: null;
            //--------------------DATOS DEL TESTIGO ---------------------------------------//
            crearComparendoCommand.ComIdentificacionTest = (comparendo.Length > 46)? comparendo[46]: null;
            crearComparendoCommand.ComNombreTesti = (comparendo.Length > 47)? comparendo[47]: null;
            crearComparendoCommand.ComDirecResTesti = (comparendo.Length > 48)? comparendo[48]: null;
            crearComparendoCommand.ComTeleTestigo = (comparendo.Length > 49)? comparendo[49]: null;
            //--------------------DATOS DEL DINERO MONEY DEL COMPARENDO --------------------------------//
            crearComparendoCommand.ComValor = (comparendo.Length > 50)? comparendo[50].conversionStringDecimal(): null;
            crearComparendoCommand.ComValAd = (comparendo.Length > 51)? comparendo[51].conversionStringDecimal(): null;
            crearComparendoCommand.ComOrganismo = (comparendo.Length > 52)? comparendo[52].conversionStringInt(): null;
            crearComparendoCommand.ComEstadoCom = (comparendo.Length > 53)? comparendo[53].conversionStringInt(): null;
            //----------------------------DATOS EXTRA DEL COMPARENDO---------------------------------
            crearComparendoCommand.ComPolca = (comparendo.Length > 54)? comparendo[54].conversionStringChar(): null;
            crearComparendoCommand.ComInfraccion = (comparendo.Length > 55)? comparendo[55]: null;
            crearComparendoCommand.ComValInfra = (comparendo.Length > 56)? comparendo[56].conversionStringDecimal(): 0;

            if (comparendo.Length > 57)
            {
                if (comparendo[57].conversionStringInt() != 0)
                {
                    //----------------------------DATOS DEL TUTOR------------------------------------------//
                    crearComparendoCommand.Id_Tipo_Doc_Tutor = (comparendo.Length > 57)? comparendo[57]
                        .conversionStringInt(): null;
                    crearComparendoCommand.Nro_Doc_Tutor = (comparendo.Length > 58)? comparendo[58] : null;
                    crearComparendoCommand.Nombre_Tutor = (comparendo.Length > 59)? comparendo[59] : null;
                    crearComparendoCommand.Apellido_Tutor = (comparendo.Length > 60)? comparendo[60] : null;
                }
                else {
                    //----------------------------DATOS COMPARENDO ELECTRÓNICO------------------------------//
                    crearComparendoCommand.FotoMulta = (comparendo.Length > 57)? comparendo[57].conversionStringChar(): 'N';
                    crearComparendoCommand.FechaNotificacion = (comparendo.Length > 58)? comparendo[58] : null;
                    crearComparendoCommand.FuenteComparendo = (comparendo.Length > 59)? comparendo[59].conversionStringInt(): 0;
                    crearComparendoCommand.LatitudComparendo = (comparendo.Length > 60)? comparendo[60] : null;
                    crearComparendoCommand.LongitudComparendo = (comparendo.Length > 61)? comparendo[61] : null;
                    return crearComparendoCommand;
                }
            }
            //----------------------------DATOS COMPARENDO ELECTRÓNICO------------------------------------------
            crearComparendoCommand.FotoMulta = (comparendo.Length > 61) ? comparendo[61].conversionStringChar(): 'N';
            crearComparendoCommand.FechaNotificacion = (comparendo.Length > 62)? comparendo[62] : null;
            crearComparendoCommand.FuenteComparendo = (comparendo.Length > 63)? comparendo[59].conversionStringInt(): 0;
            crearComparendoCommand.LatitudComparendo = (comparendo.Length > 64)? comparendo[64] : null;
            crearComparendoCommand.LongitudComparendo = (comparendo.Length > 66)? comparendo[65] : null;
            
            return crearComparendoCommand;
        }
    }
}