using ProyectoParadigmas.Clases.Simbolos;
using ProyectoParadigmas.Clases.Sintax;
using System;

namespace ProyectoParadigmas.Clases.Binding
{
    internal class BoundOperadorUnario
    {
        private BoundOperadorUnario(TiposSintax tipoSintax, BoundTipoOperadorUnario tipoOperador, TipoSimbolo type) : this(tipoSintax, tipoOperador, type, type) { }

        private BoundOperadorUnario(TiposSintax tipoSintax, BoundTipoOperadorUnario tipoOperador, TipoSimbolo type, TipoSimbolo resultType)
        {
            TipoSintax = tipoSintax;
            TipoOperador = tipoOperador;
            Type = type;
            Tipo = resultType;
        }

        public TiposSintax TipoSintax { get; }
        public BoundTipoOperadorUnario TipoOperador { get; }
        public TipoSimbolo Type { get; }
        public TipoSimbolo Tipo { get; }

        private static BoundOperadorUnario[] operadores =
        {
            new BoundOperadorUnario(TiposSintax.EXCLAMACION_CIERRE, BoundTipoOperadorUnario.NEGACION_LOGICA, TipoSimbolo.Bool),
            new BoundOperadorUnario(TiposSintax.SUMA, BoundTipoOperadorUnario.IDENTIDAD, TipoSimbolo.Int),
            new BoundOperadorUnario(TiposSintax.RESTA, BoundTipoOperadorUnario.RESTA, TipoSimbolo.Int),
            new BoundOperadorUnario(TiposSintax.NEGACION, BoundTipoOperadorUnario.COMPLEMENTO, TipoSimbolo.Int),


        };

        public static BoundOperadorUnario Bind(TiposSintax tipoSintax, TipoSimbolo tipoOperando)
        {
            foreach (var operador in operadores)
            {
                if (operador.TipoSintax == tipoSintax && operador.Type == tipoOperando)
                {
                    return operador;
                }
            }
            return null;
        }
    }
}
