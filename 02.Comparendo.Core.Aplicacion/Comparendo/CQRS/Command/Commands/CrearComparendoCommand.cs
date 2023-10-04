using _02.Comparendo.Core.Aplicacion.Comparendo.Utils;
using MediatR;

namespace _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Command.Commands
{
    public class CrearComparendoCommand: IRequest<Response<Guid>>
    {
        //--------------------DATOS BÁSICOS DEL COMPARENDO -------------------------------//
        public string? ComNumero { get; set; } = null!; // OBLIGATORIO
        public string? ComFecha { get; set; } = null!; // OBLIGATORIO
        public string? ComHora { get; set; } // OBLIGATORIO
        public string? ComDir { get; set; } // OBLIGATORIO
        public int? ComDivipoMuni { get; set; } // OBLIGATORIO
        public string? ComLocalidadComuna { get; set; } 
        public string? ComPlaca { get; set; } 
        public int? ComDivipoMatri { get; set; } 
        public int? ComTipoVehi { get; set; } // OBLIGATORIO
        public int? ComTipoSer { get; set; } // OBLIGATORIO
        public int? ComCodigoRadio { get; set; } 
        public int? ComCodigoModalidad { get; set; } 
        public int? ComCodigoPasajeros { get; set; }
        /*-------------------------------------------------------------------------------------*/
        
        //--------------------DATOS BÁSICOS DEL INFRACTOR -------------------------------//
        // ComInfractor DEBE SER UNA CADENA DE MAXIMO 15 CARACTERES
        public string? ComInfractor { get; set; } = null!; // OBLIGATORIO
        public int? ComTipoDoc { get; set; } // OBLIGATORIO
        public string? ComNombre { get; set; } 
        public string? ComApellido { get; set; } 
        public int? ComEdadInfractor { get; set; } // OBLIGATORIO
        public string? ComDirInfractor { get; set; } 
        public string? ComEmail { get; set; } 
        public string? ComTeleInfractor { get; set; } 
        public int? ComIdCiudad { get; set; } 
        public string? ComLicencia { get; set; } 
        public string? ComCategoria { get; set; } 
        public int? ComSecreExpide { get; set; } 
        public string? ComFechaVence { get; set; } 
        public int? ComTipoInfrac { get; set; } 
        public string? CompLicTransito { get; set; } 
        public int? ComDivipoLicen { get; set; }
        
        /*--------------------------------------------------------------------------------------*/
        //--------------------DATOS BÁSICOS DEL PROPIETARIO -------------------------------//
        // ComIdentificacion DEBE SER UN NUMERO DE MAXIMO 15 CARACTERES
        public string? ComIdentificacion { get; set; } 
        public int? ComIdTipoDocProp { get; set; } 
        public string? ComNombreProp { get; set; }
        /*--------------------------------------------------------------------------------------*/
        
        /*--------------------------------------------------------------------------------------*/
        //--------------------DATOS BÁSICOS DE LA EMPRESA Y TARJETA OPERACION -------------------------------//
        // ComNombreEmpresa DEBE SER UN NUMERO DE MAXIMO 30 CARACTERES
        public string? ComNombreEmpresa { get; set; }
        public string? ComNitEmpresa { get; set; } 
        public string? ComTarjetaOperacion { get; set; }

        /*--------------------------------------------------------------------------------------*/

        /*--------------------------------------------------------------------------------------*/
        //--------------------DATOS BÁSICOS DEl AGENTE -------------------------------//
        // ComNombreEmpresa DEBE SER UN NUMERO DE MAXIMO 30 CARACTERES
        public string? CompPlacaAgente { get; set; } // OBLIGATORIO
        
        /*--------------------------------------------------------------------------------------*/

        //--------------------DATOS AVANZADOS DEL COMPARENDO -------------------------------//
        public string? CompObservaciones { get; set; } 
        public char? ComFuga { get; set; } // OBLIGATORIO
        public char? ComAcci { get; set; } // OBLIGATORIO
        public int? GradoAlcohol { get; set; } = 0; // OBLIGATORIO NO SE ENCUENTRA EN SIMIT 
        /*--------------------------------------------------------------------------------------*/

        //--------------------DATOS AVANZADOS DEL COMPARENDO INMOVILIZACION -------------------------------//
        public char? ComInmov { get; set; } // OBLIGATORIO
        public string? ComPatioInmoviliza { get; set; } 
        public string? ComDirPatioInmovi { get; set; } 
        public string? ComGruaNumero { get; set; } 
        public string? ComPlacaGrua { get; set; } 
        public string? ComConsecutInmovi { get; set; }
        /*--------------------------------------------------------------------------------------*/

        //--------------------DATOS DEL TESTIGO ----------------------------------------------------//
        public string? ComIdentificacionTest { get; set; } 
        public string? ComNombreTesti { get; set; } 
        public string? ComDirecResTesti { get; set; } 
        public string? ComTeleTestigo { get; set; }

        /*--------------------------------------------------------------------------------------*/

        //--------------------DATOS DEL DINERO MONEY DEL COMPARENDO ----------------------------------------------------//
        public decimal? ComValor { get; set; } = 0;
        public decimal? ComValAd { get; set; }
        public int? ComOrganismo { get; set; } // OBLIGATORIO
        public int? ComEstadoCom { get; set; } // OBLIGATORIO
        /*--------------------------------------------------------------------------------------*/
        
        //----------------------------DATOS EXTRA DEL COMPARENDO------------------------------------------
        public char? ComPolca { get; set; } 
        public string? ComInfraccion { get; set; } = null!; // OBLIGATORIO
        public decimal ComValInfra { get; set; } = 0;
        /*--------------------------------------------------------------------------------------*/

        //----------------------------DATOS DEL TUTOR------------------------------------------
        public int? Id_Tipo_Doc_Tutor { get; set; }
        public string? Nro_Doc_Tutor { get; set; }
        public string? Nombre_Tutor { get; set; }
        public string? Apellido_Tutor { get; set; }
        /*--------------------------------------------------------------------------------------*/
        
        //----------------------------DATOS COMPARENDO ELECTRÓNICO------------------------------------------
        public char? FotoMulta { get; set; } = 'N';
        public string? FechaNotificacion { get; set; } 
        public int? FuenteComparendo { get; set; } = 0; // OBLIGATORIO
        public string? LatitudComparendo { get; set; }
        public string? LongitudComparendo { get; set; }
        /*--------------------------------------------------------------------------------------*/
    }
}