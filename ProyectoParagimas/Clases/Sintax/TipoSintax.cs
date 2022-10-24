using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoParagimas.Clases.Sintax
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

        //Expresiones
        EXPRESION_BINARIA,
        EXPRESION_PARENTESIS,
        EXPRESION_LITERAL,
        EXPRESION_UNARIA,

        //Palabras reservadas
        VERDADERO,
        FALSO
    }
}
