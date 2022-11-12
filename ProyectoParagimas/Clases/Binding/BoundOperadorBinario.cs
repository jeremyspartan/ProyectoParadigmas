using ProyectoParadigmas.AnalisisDeCodigo.Binding;
using ProyectoParadigmas.Clases.Sintax;
using System;

namespace ProyectoParadigmas.Clases.Binding
{
    internal class BoundOperadorBinario
    {
        private BoundOperadorBinario(TiposSintax tipoSintax, BoundTipoOperadorBinario tipoOperador, Type type) : this(tipoSintax, tipoOperador, type, type, type) { }

        private BoundOperadorBinario(TiposSintax tipoSintax, BoundTipoOperadorBinario tipoOperador, Type tipoOperando, Type resultType) :
            this(tipoSintax, tipoOperador, tipoOperando, tipoOperando, resultType)
        { }

        private BoundOperadorBinario(TiposSintax tipoSintax, BoundTipoOperadorBinario tipoOperador, Type tipoIzq, Type tipoDer, Type resultType)
        {
            TipoSintax = tipoSintax;
            TipoOperador = tipoOperador;
            TipoIzq = tipoIzq;
            TipoDer = tipoDer;
            Tipo = resultType;
        }

        public TiposSintax TipoSintax { get; }
        public BoundTipoOperadorBinario TipoOperador { get; }
        public Type TipoIzq { get; }
        public Type TipoDer { get; }
        public Type Tipo { get; }

        private static BoundOperadorBinario[] operadores =
        {
            new BoundOperadorBinario(TiposSintax.SUMA, BoundTipoOperadorBinario.ADICION, typeof(int)),
            new BoundOperadorBinario(TiposSintax.RESTA, BoundTipoOperadorBinario.SUSTRACCION, typeof(int)),
            new BoundOperadorBinario(TiposSintax.ASTERISCO, BoundTipoOperadorBinario.MULTIPLICACION, typeof(int)),
            new BoundOperadorBinario(TiposSintax.SLASH, BoundTipoOperadorBinario.DIVISION, typeof(int)),
            new BoundOperadorBinario(TiposSintax.DOBLE_AMPERSAND, BoundTipoOperadorBinario.Y_LOGICO, typeof(bool)),
            new BoundOperadorBinario(TiposSintax.DOBLE_PALO, BoundTipoOperadorBinario.O_LOGICO, typeof(bool)),
            new BoundOperadorBinario(TiposSintax.IGUALDAD, BoundTipoOperadorBinario.IGUAL_A, typeof(int),typeof(bool)),
            new BoundOperadorBinario(TiposSintax.NO_IGUALDAD, BoundTipoOperadorBinario.DIFERENTE_DE, typeof(int),typeof(bool)),
            new BoundOperadorBinario(TiposSintax.MENOR, BoundTipoOperadorBinario.MENOR_QUE, typeof(int),typeof(bool)),
            new BoundOperadorBinario(TiposSintax.MENOR_IGUAL, BoundTipoOperadorBinario.MENOR_IGUAL_QUE, typeof(int),typeof(bool)),
            new BoundOperadorBinario(TiposSintax.MAYOR, BoundTipoOperadorBinario.MAYOR_QUE, typeof(int),typeof(bool)),
            new BoundOperadorBinario(TiposSintax.MAYOR_IGUAL, BoundTipoOperadorBinario.MAYOR_IGUAL_QUE, typeof(int),typeof(bool)),
            //tambien se puede definir los tipos de desigualdad e igualdad sobre objetos de tipo bool
             new BoundOperadorBinario(TiposSintax.IGUALDAD, BoundTipoOperadorBinario.IGUAL_A, typeof(bool)),
            new BoundOperadorBinario(TiposSintax.NO_IGUALDAD, BoundTipoOperadorBinario.DIFERENTE_DE, typeof(bool))

        };

        public static BoundOperadorBinario Bind(TiposSintax tipoSintax, Type izq, Type der)
        {
            foreach (var operador in operadores)
            {
                if (operador.TipoSintax == tipoSintax && operador.TipoIzq == izq && operador.TipoDer == der)
                {
                    return operador;
                }
            }
            return null;
        }
    }
}

