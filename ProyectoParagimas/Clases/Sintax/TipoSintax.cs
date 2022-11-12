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
        MENOR,
        MENOR_IGUAL,
        MAYOR,
        MAYOR_IGUAL,

        //Nodos
        UNIDAD_COMPILACION,
        CLAUSULA_ELSE,

        //Declaraciones
        BLOQUE_DECLARACION,
        VARIABLE_DECLARACION,
        IF_DECLARACION,
        WHILE_DECLRARACION,
        FOR_DECLRARACION,
        EXPRESION_DECLARACION,

        //Expresiones
        EXPRESION_BINARIA,
        EXPRESION_PARENTESIS,
        EXPRESION_LITERAL,
        EXPRESION_UNARIA,
        EXPRESION_NOMBRE,
        EXPRESION_ASIGNACION,

        //Palabras reservadas
        VERDADERO,
        FALSO,
        LET_CLAVE,
        VAR_CLAVE,
        IF_CLAVE,
        ELSE_CLAVE,
        WHILE_CLAVE,
        FOR_CLAVE,
        TO_CLAVE
    }
}
