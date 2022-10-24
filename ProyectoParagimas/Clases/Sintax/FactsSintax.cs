using System;

namespace ProyectoParagimas.Clases.Sintax

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
                    return 3;

                case TiposSintax.DOBLE_AMPERSAND:
                    return 2;

                case TiposSintax.DOBLE_PALO:
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
                    return 6;

                default:
                    return 0;
            }
        }

        public static TiposSintax GetPalabraReservada(string texto)
        {
            switch(texto)
            {
                case "true":
                    return TiposSintax.VERDADERO;
                case "false":
                    return TiposSintax.FALSO;
                case "eq":
                    return TiposSintax.IGUALDAD;
                case "¬":
                    return TiposSintax.NO_IGUALDAD;
                default:
                    return TiposSintax.IDENTIFICADOR;
            }
        }
    }
}
