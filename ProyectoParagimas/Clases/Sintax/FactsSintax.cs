using System;
using System.Collections.Generic;

namespace ProyectoParadigmas.Clases.Sintax

{
    internal static class FactsSintax
    {
        /*Esta clase sirve para asignarle una jerarquia a los elementos sintacticos*/
        public static int GetOperadorBinarioPrecendece(this TiposSintax tipo)
        {
            switch (tipo)
            {
                case TiposSintax.SLASH:
                case TiposSintax.ASTERISCO:
                    return 5;

                case TiposSintax.SUMA:
                case TiposSintax.RESTA:
                    return 4;


                case TiposSintax.IGUALDAD:
                case TiposSintax.NO_IGUALDAD:
                case TiposSintax.MENOR:
                case TiposSintax.MAYOR:
                case TiposSintax.MENOR_IGUAL:
                case TiposSintax.MAYOR_IGUAL:
                    return 3;

                case TiposSintax.DOBLE_AMPERSAND:
                case TiposSintax.AMPERSAND:
                    return 2;

                case TiposSintax.DOBLE_PALO:
                case TiposSintax.PALO:
                case TiposSintax.SOMBRERO:
                    return 1;

                default:
                    return 0;
            }
        }

        public static int GetOperadorUnarioPrecendece(this TiposSintax tipo)
        {
            switch (tipo)
            {
                case TiposSintax.SUMA:
                case TiposSintax.RESTA:
                case TiposSintax.EXCLAMACION_CIERRE:
                case TiposSintax.NEGACION:
                    return 6;

                default:
                    return 0;
            }
        }

        public static TiposSintax GetPalabraReservada(string texto)
        {
            switch (texto)
            {
                case "true":
                    return TiposSintax.VERDADERO;
                case "false":
                    return TiposSintax.FALSO;
                case "if":
                    return TiposSintax.IF_CLAVE;
                case "else":
                    return TiposSintax.ELSE_CLAVE;
                case "let":
                    return TiposSintax.LET_CLAVE;
                case "var":
                    return TiposSintax.VAR_CLAVE;
                case "while":
                    return TiposSintax.WHILE_CLAVE;
                case "for":
                    return TiposSintax.FOR_CLAVE;
                case "to":
                    return TiposSintax.TO_CLAVE;
                default:
                    return TiposSintax.IDENTIFICADOR;
            }
        }

        public static IEnumerable<TiposSintax> GetTiposOperadoresUnarios()
        {
            var tipos = ((TiposSintax[])Enum.GetValues(typeof(TiposSintax)));
            foreach (var tipo in tipos)
            {
                if (GetOperadorUnarioPrecendece(tipo) > 0)
                    yield return tipo;
            }
        }

        public static IEnumerable<TiposSintax> GetTiposOperadoresBinarios()
        {
            var tipos = ((TiposSintax[])Enum.GetValues(typeof(TiposSintax)));
            foreach (var tipo in tipos)
            {
                if (GetOperadorBinarioPrecendece(tipo) > 0)
                    yield return tipo;
            }
        }

        public static string GetTexto(TiposSintax tipo)
        {
            switch (tipo)
            {
                case TiposSintax.SUMA:
                    return "+";
                case TiposSintax.RESTA:
                    return "-";
                case TiposSintax.ASTERISCO:
                    return "*";
                case TiposSintax.SLASH:
                    return "/";
                case TiposSintax.EXCLAMACION_CIERRE:
                    return "!";
                case TiposSintax.MENOR:
                    return "<";
                case TiposSintax.MAYOR:
                    return ">";
                case TiposSintax.MENOR_IGUAL:
                    return "<=";
                case TiposSintax.MAYOR_IGUAL:
                    return ">=";
                case TiposSintax.ASIGNACION:
                    return "=";
                case TiposSintax.DOBLE_AMPERSAND:
                    return "&&";
                case TiposSintax.AMPERSAND:
                    return "&";
                case TiposSintax.DOBLE_PALO:
                    return "||";
                case TiposSintax.PALO:
                    return "|";
                case TiposSintax.SOMBRERO:
                    return "^";
                case TiposSintax.NEGACION:
                    return "~";
                case TiposSintax.IGUALDAD:
                    return "==";
                case TiposSintax.NO_IGUALDAD:
                    return "¬";
                case TiposSintax.PARENTESIS_APERTURA:
                    return "(";
                case TiposSintax.PARENTESIS_CIERRE:
                    return ")";
                case TiposSintax.LLAVE_APERTURA:
                    return "{";
                case TiposSintax.LLAVE_CIERRE:
                    return "}";
                case TiposSintax.FALSO:
                    return "false";
                case TiposSintax.VERDADERO:
                    return "true";
                case TiposSintax.LET_CLAVE:
                    return "let";
                case TiposSintax.VAR_CLAVE:
                    return "var";
                case TiposSintax.IF_CLAVE:
                    return "if";
                case TiposSintax.ELSE_CLAVE:
                    return "else";
                case TiposSintax.FOR_CLAVE:
                    return "for";
                case TiposSintax.WHILE_CLAVE:
                    return "while";
                case TiposSintax.TO_CLAVE:
                    return "to";
                default:
                    return null;
            }
        }
    }
}
