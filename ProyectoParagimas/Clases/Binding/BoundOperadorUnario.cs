using ProyectoParadigmas.Clases.Sintax;
using System;

namespace ProyectoParadigmas.Clases.Binding
{
    internal class BoundOperadorUnario
    {
        private BoundOperadorUnario(TiposSintax tipoSintax, BoundTipoOperadorUnario tipoOperador, Type type) : this(tipoSintax, tipoOperador, type, type) { }

        private BoundOperadorUnario(TiposSintax tipoSintax, BoundTipoOperadorUnario tipoOperador, Type type, Type resultType)
        {
            TipoSintax = tipoSintax;
            TipoOperador = tipoOperador;
            Type = type;
            Tipo = resultType;
        }

        public TiposSintax TipoSintax { get; }
        public BoundTipoOperadorUnario TipoOperador { get; }
        public Type Type { get; }
        public Type Tipo { get; }

        private static BoundOperadorUnario[] operadores =
        {
            new BoundOperadorUnario(TiposSintax.EXCLAMACION_CIERRE, BoundTipoOperadorUnario.NEGACION_LOGICA, typeof(bool)),
            new BoundOperadorUnario(TiposSintax.SUMA, BoundTipoOperadorUnario.IDENTIDAD, typeof(int)),
            new BoundOperadorUnario(TiposSintax.RESTA, BoundTipoOperadorUnario.NEGACION, typeof(int))

        };

        public static BoundOperadorUnario Bind(TiposSintax tipoSintax, Type tipoOperando)
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
