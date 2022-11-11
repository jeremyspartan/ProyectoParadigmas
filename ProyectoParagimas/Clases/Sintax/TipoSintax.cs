namespace ProyectoParadigmas.Clases.Sintax
{
    public enum TiposSintax
    {
        //Elementos
        NUMERO,
        ESPACIO,
        PARENTESIS_CIERRE,
        PARENTESIS_APERTURA,
        SUMA,
        RESTA,
        ASTERISCO,
        SLASH,
        TIPO_ERRONEO,
        EOF,
        IDENTIFICADOR,
        EXLAMACION_APERTURA,
        EXCLAMACION_CIERRE,
        AMPERSAND,
        DOBLE_AMPERSAND,
        PALO,
        DOBLE_PALO,
        IGUALDAD,
        NO_IGUALDAD,
        ASIGNACION,
        LLAVE_APERTURA,
        LLAVE_CIERRE,

        //Nodos
        UNIDAD_COMPILACION,

        //Declaraciones
        BLOQUE_DECLARACION,
        VARIABLE_DECLARACION,

        //Expresiones
        EXPRESION_BINARIA,
        EXPRESION_PARENTESIS,
        EXPRESION_LITERAL,
        EXPRESION_UNARIA,
        EXPRESION_NOMBRE,
        EXPRESION_ASIGNACION,
        EXPRESION_DECLARACION,

        //Palabras reservadas
        VERDADERO,
        FALSO,
        LET_CLAVE,
        VAR_CLAVE
    }
}
