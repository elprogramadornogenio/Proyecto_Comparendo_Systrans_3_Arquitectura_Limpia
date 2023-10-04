namespace _02.Comparendo.Core.Aplicacion.Comparendo.CQRS.Query.DTO
{
    public class ComparendoSimitDto
    {
        //--------------------DATOS BÁSICOS DEL COMPARENDO -------------------------------//
        // ComConsecutivo DEBE SER MENOR O IGUAL A 19999
        public int ComConsecutivo { get; set; } // OBLIGATORIO generado tiempo ejecución (*)
        // ComNumero DEBE SER NÚMERICO Y NO CARACTERES ESPECIALES
        public string ComNumero { get; set; } = null!; // OBLIGATORIO Numero { get; set; } (*)
        // ComFecha DEBE SER EN FORMATO dd/mm/yyyy = 27/01/1999
        public string ComFecha { get; set; } = null!; // OBLIGATORIO DateTime Fecha { get; set; } (*) 
        // ComHora DEBE SER EN FORMATO hhmm por ejemplo las 10:05 -> 1005
        public string? ComHora { get; set; } // no obligatorio DateTime Hora { get; set; }
        // ComDir DEBE SER UNA CADENA MAXIMO DE 80 CARACTERES
        public string? ComDir { get; set; } // no obligatorio public string Direccion { get; set; }
        // ComDivipoMuni DEBE SER UNA CADENA DE MAXIMO 8 CARACTERES
        public int? ComDivipoMuni { get; set; } // no obligatorio ¿ MunicipioDireccionId? { get; set; }
        // ComLocalidadComuna DEBE SER UNA CADENA DE MAXIMO 30 CARACTERES
        public string? ComLocalidadComuna { get; set; } // no obligatorio Localidad { get; set; }
        // ComPlaca DEBE SER UNA CADENA ALFANUMERICA MAXIMO 8 CARACTERES
        public string? ComPlaca { get; set; } // no obligatorio Placa { get; set; }
        // ComDivipoMatri DEBE SER UNA CADENA MAXIMO 8 CARACTERES
        public int? ComDivipoMatri { get; set; } // no obligatorio MatriculaSecretariaId { get; set; }
        // ComTipoVehi DEBE SER UN NUMERO DE MÁXIMO 2 CIFRAS MENOR QUE 100
        public int? ComTipoVehi { get; set; } // no obligatorio TipoVehiculoId { get; set; }
        // ComTipoSer DEBE SER UN NUMERO DE MÁXIMO 1 CIFRAS MENOR QUE 10
        public int? ComTipoSer { get; set; } // no obligatorio ClaseServicioId { get; set; }
        // ComCodigoRadio DEBE SER UN NUMERO DE MÁXIMO 1 CIFRAS MENOR QUE 10
        public int? ComCodigoRadio { get; set; } // no obligatorio CodigoRadio { get; set; }
        // ComCodigoModalidad DEBE SER UN NUMERO DE MÁXIMO 1 CIFRAS MENOR QUE 10
        public int? ComCodigoModalidad { get; set; } // no obligatorio CodigoModalidad { get; set; }
        // ComCodigoModalidad DEBE SER UN NUMERO DE MÁXIMO 1 CIFRAS MENOR QUE 10
        public int? ComCodigoPasajeros { get; set; } // no obligatorio CodigoPasajeros { get; set; }
        /*-------------------------------------------------------------------------------------*/
        
        //--------------------DATOS BÁSICOS DEL INFRACTOR -------------------------------//
        // ComInfractor DEBE SER UNA CADENA DE MAXIMO 15 CARACTERES
        public string ComInfractor { get; set; } = null!; // OBLIGATORIO DocumentoInfractor { get; set; } (*)
        // ComTipoDoc DEBE SER NUMERO DE 1 CIFRA ES DECIR MENOR 10
        public int ComTipoDoc { get; set; } // OBLIGATORIO InfractorTipoDocumentoId { get; set; } (*)
        // ComNombre DEBE SER UNA CADENA DE MAXIMO 18 CARACTERES
        public string? ComNombre { get; set; } // no obligatorio NombreInfractor { get; set; }
        // ComApellido DEBE SER UNA CADENA DE MAXIMO 20 CARACTERES
        public string? ComApellido { get; set; } // no obligatorio ApellidoInfractor { get; set; }
        // ComEdadInfractor DEBE SER NUMERO DE MAXIMO 2 CIFRAS ES DECIR MENOR 100
        public int? ComEdadInfractor { get; set; } // no obligatorio EdadInfractor { get; set; }
        // ComDirInfractor DEBE SER UNA CADENA DE MAXIMO 40 CARACTERES
        public string? ComDirInfractor { get; set; } // no obligatorio DireccionInfractor { get; set; }
        // ComEmail DEBE SER UNA CADENA DE MAXIMO 40 CARACTERES
        public string? ComEmail { get; set; } // no obligatorio EmailInfractor { get; set; }
        // ComTeleInfractor DEBE SER UNA CADENA DE MAXIMO 15 CARACTERES
        public string? ComTeleInfractor { get; set; } // no obligatorio TelefonoInfractor { get; set; }
        // ComIdCiudad DEBE SER NUMERO DE MAXIMO 8 CIFRAS
        public int? ComIdCiudad { get; set; } // no obligatorio CiudadInfractorId { get; set; }
        // ComLicencia DEBE SER UNA CADENA DE MAXIMO 14 CARACTERES
        public string? ComLicencia { get; set; } // no obligatorio LicenciaConduccion { get; set; }
        // ComLicencia DEBE SER UNA CADENA DE MAXIMO 2 CARACTERES
        public string? ComCategoria { get; set; } // no obligatorio LicenciaConduccionCategoria { get; set; }
        // ComSecreExpide DEBE SER NUMERO DE MAXIMO 8 CIFRAS
        public int? ComSecreExpide { get; set; } // no obligatorio LicenciaConduccionSecretariaId { get; set; }
        // ComFechaVence DEBE SER UNA CADENA CON EL FORMATO dd/mm/yyyy ejemplo 20/07/1998
        public string? ComFechaVence { get; set; } // no obligatorio DateTime? LicenciaVence { get; set; }
        // ComTipoInfrac DEBE SER NUMERO DE MAXIMO 1 CIFRA
        public int? ComTipoInfrac { get; set; } // no obligatorio TipoInfractorId { get; set; }
        // CompLicTransito DEBE SER UNA CADENA DE MAXIMO 15 CARACTERES
        public string? CompLicTransito { get; set; } // no obligatorio LicenciaTransito { get; set; }
        // ComDivipoLicen DEBE SER UN NUMERO DE MAXIMO 8 CIFRAS
        public int? ComDivipoLicen { get; set; } // no obligatorio LicenciaTransitoSecretariaId { get; set; }
        
        /*--------------------------------------------------------------------------------------*/
        //--------------------DATOS BÁSICOS DEL PROPIETARIO -------------------------------//
        // ComIdentificacion DEBE SER UN NUMERO DE MAXIMO 15 CARACTERES
        public string? ComIdentificacion { get; set; } // no obligatorio DocumentoPropietario { get; set; }
        // ComDivipoLicen DEBE SER UN NUMERO DE MAXIMO 1 CIFRA
        public int? ComIdTipoDocProp { get; set; } // no obligatorio TipoDocumentoPropietarioId { get; set; }
        // ComNombreProp DEBE SER UN NUMERO DE MAXIMO 50 CARACTERES
        public string? ComNombreProp { get; set; } // no obligatorio NombrePropietario { get; set; } y  
        // ApellidoPropietario { get; set; }
        /*--------------------------------------------------------------------------------------*/
        
        /*--------------------------------------------------------------------------------------*/
        //--------------------DATOS BÁSICOS DE LA EMPRESA Y TARJETA OPERACION -------------------------------//
        // ComNombreEmpresa DEBE SER UN NUMERO DE MAXIMO 30 CARACTERES
        public string? ComNombreEmpresa { get; set; } // no obligatorio NombreEmpresa { get; set; }
        // ComNitEmpresa DEBE SER UN NUMERO DE MAXIMO 15 CARACTERES
        public string? ComNitEmpresa { get; set; } // no obligatorio NitEmpresa { get; set; }
        // ComTarjetaOperacion DEBE SER UN NUMERO DE MAXIMO 10 CARACTERES
        public string? ComTarjetaOperacion { get; set; } // no obligatorio TarjetaOperacion { get; set; }

        /*--------------------------------------------------------------------------------------*/

        /*--------------------------------------------------------------------------------------*/
        //--------------------DATOS BÁSICOS DEl AGENTE -------------------------------//
        // ComNombreEmpresa DEBE SER UN NUMERO DE MAXIMO 30 CARACTERES
        public string? CompPlacaAgente { get; set; } // OBLIGATORIO CONDICIONAL si el comparendo es POLCA AgenteTransitoId { get; set; }
        
        /*--------------------------------------------------------------------------------------*/

        //--------------------DATOS AVANZADOS DEL COMPARENDO -------------------------------//
        // CompObservaciones DEBE SER UN NUMERO DE MAXIMO 200 CARACTERES
        public string? CompObservaciones { get; set; } // no obligatorio Observaciones { get; set; }
        public char? ComFuga { get; set; } // no obligatorio bool Fuga { get; set; }
        public char? ComAcci { get; set; } // no obligatorio bool Accidente { get; set; }
        /*--------------------------------------------------------------------------------------*/

        //--------------------DATOS AVANZADOS DEL COMPARENDO INMOVILIZACION -------------------------------//
        public char? ComInmov { get; set; } // no obligatorio bool Inmobilizacion { get; set; }
        // ComPatioInmoviliza DEBE SER UN NUMERO DE MAXIMO 30 CARACTERES
        public string? ComPatioInmoviliza { get; set; } // no obligatorio PatioInmoviliza { get; set; }
        // ComDirPatioInmovi DEBE SER UN NUMERO DE MAXIMO 30 CARACTERES
        public string? ComDirPatioInmovi { get; set; } // no obligatorio DireccionPatioInmoviliza { get; set; }
        // ComGruaNumero DEBE SER UN NUMERO DE MAXIMO 10 CARACTERES
        public string? ComGruaNumero { get; set; } // no obligatorio GruaNumero { get; set; }
        // ComPlacaGrua DEBE SER UN NUMERO DE MAXIMO 6 CARACTERES
        public string? ComPlacaGrua { get; set; } // no obligatorio GruaPlaca { get; set; }
        // ComConsecutInmovi DEBE SER UN NUMERO DE MAXIMO 15 CARACTERES
        public string? ComConsecutInmovi { get; set; } // no obligatorio ConsecutivoInmovilizacion { get; set; }
        /*--------------------------------------------------------------------------------------*/

        //--------------------DATOS DEL TESTIGO ----------------------------------------------------//
        // ComIdentificacionTest DEBE SER UN NUMERO DE MAXIMO 15 CARACTERES
        public string? ComIdentificacionTest { get; set; } // no obligatorio DocumentoTestigo { get; set; }
        // ComNombreTesti DEBE SER UN NUMERO DE MAXIMO 50 CARACTERES
        public string? ComNombreTesti { get; set; } // no obligatorio NombreTestigo { get; set; }
        // ApellidoTestigo { get; set; }
        // ComDirecResTesti DEBE SER UN NUMERO DE MAXIMO 40 CARACTERES
        public string? ComDirecResTesti { get; set; } // no obligatorio DireccionTestigo { get; set; }
        // ComTeleTestigo DEBE SER UN NUMERO DE MAXIMO 15 CARACTERES
        public string? ComTeleTestigo { get; set; } // no obligatorio TelefonoTestigo { get; set; }

        /*--------------------------------------------------------------------------------------*/

        //--------------------DATOS DEL DINERO MONEY DEL COMPARENDO ----------------------------------------------------//
        // ComValor DEBE SER UN NUMERO DE MAXIMO 8 CIFRAS
        public decimal ComValor { get; set; } // OBLIGATORIO ValorComparendo { get; set; } (*)
        // ComValAd DEBE SER UN NUMERO DE MAXIMO 8 CIFRAS
        public decimal ComValAd { get; set; } // OBLIGATORIO ValorAdicional { get; set; } (*)
        // ComOrganismo DEBE SER UN NUMERO DE MAXIMO 8 CIFRAS
        public int ComOrganismo { get; set; } // OBLIGATORIO SecretariaId { get; set; } (*)
        // ComEstadoCom DEBE SER UN NUMERO DE MAXIMO 2 CIFRAS
        public int ComEstadoCom { get; set; }// OBLIGATORIO EstadoComparendoId { get; set; } (*)
        /*--------------------------------------------------------------------------------------*/
        
        //----------------------------DATOS EXTRA DEL COMPARENDO------------------------------------------
        // ComPolca DEBE SER UN NUMERO DE MAXIMO 1 CIFRAS (S O N)
        public char? ComPolca { get; set; } // OBLIGATORIO bool Polca { get; set; }
        // ComInfraccion DEBE SER UN NUMERO DE MAXIMO 5 CARACTERES
        public string ComInfraccion { get; set; } = null!; // OBLIGATORIO CodigoComparendo { get; set; } (*)
        // ComValInfra DEBE SER UN NUMERO DE MAXIMO 5 CIFRAS
        public decimal ComValInfra { get; set; } // ComValInfra = ComValor + ComValAd OBLIGATORIO (*)
        /*--------------------------------------------------------------------------------------*/

        //----------------------------DATOS DEL TUTOR------------------------------------------
        // Id_Tipo_Doc_Tutor DEBE SER UN NUMERO DE MAXIMO 2 CIFRAS 
        public int? Id_Tipo_Doc_Tutor { get; set; } // OBLIGATORIO SI EL INFRACTOR ES MENOR DE EDAD
        // Nro_Doc_Tutor DEBE SER UN NUMERO DE MAXIMO 15 CARACTERES
        public string? Nro_Doc_Tutor { get; set; } // OBLIGATORIO SI EL INFRACTOR ES MENOR DE EDAD
        // Nombre_Tutor DEBE SER UN NUMERO DE MAXIMO 20 CARACTERES
        public string? Nombre_Tutor { get; set; } // OBLIGATORIO SI EL INFRACTOR ES MENOR DE EDAD
        // Apellido_Tutor DEBE SER UN NUMERO DE MAXIMO 20 CARACTERES
        public string? Apellido_Tutor { get; set; } // OBLIGATORIO SI EL INFRACTOR ES MENOR DE EDAD
        /*--------------------------------------------------------------------------------------*/
        
        //----------------------------DATOS COMPARENDO ELECTRÓNICO------------------------------------------
        public char? FotoMulta { get; set; } // no obligatorio bool ComparendoElectronico { get; set; }
        public string? FechaNotificacion { get; set; } // OBLIGATORIO DateTime? FechaNotificacion { get; set; }
        public int? FuenteComparendo { get; set; } // no obligatorio Fuente { get; set; }
        public string? LatitudComparendo { get; set; } // no obligatorio Latitud { get; set; }
        public string? LongitudComparendo { get; set; } // no obligatorio Longitud { get; set; }
        /*--------------------------------------------------------------------------------------*/
    }
}