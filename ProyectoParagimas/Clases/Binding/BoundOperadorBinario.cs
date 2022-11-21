using ProyectoParadigmas.AnalisisDeCodigo.Binding;
using ProyectoParadigmas.Clases.Simbolos;
using ProyectoParadigmas.Clases.Sintax;
using System;

namespace ProyectoParadigmas.Clases.Binding
{
    internal class BoundOperadorBinario
    {
        private BoundOperadorBinario(TiposSintax tipoSintax, BoundTipoOperadorBinario tipoOperador, TipoSimbolo type) : this(tipoSintax, tipoOperador, type, type, type) { }

        private BoundOperadorBinario(TiposSintax tipoSintax, BoundTipoOperadorBinario tipoOperador, TipoSimbolo tipoOperando, TipoSimbolo resultType) :
            this(tipoSintax, tipoOperador, tipoOperando, tipoOperando, resultType)
        { }

        private BoundOperadorBinario(TiposSintax tipoSintax, BoundTipoOperadorBinario tipoOperador, TipoSimbolo tipoIzq, TipoSimbolo tipoDer, TipoSimbolo resultType)
        {
            TipoSintax = tipoSintax;
            TipoOperador = tipoOperador;
            TipoIzq = tipoIzq;
            TipoDer = tipoDer;
            Tipo = resultType;
        }

        public TiposSintax TipoSintax { get; }
        public BoundTipoOperadorBinario TipoOperador { get; }
        public TipoSimbolo TipoIzq { get; }
        public TipoSimbolo TipoDer { get; }
        public TipoSimbolo Tipo { get; }

        private static BoundOperadorBinario[] operadores =
        {
            new BoundOperadorBinario(TiposSintax.SUMA, BoundTipoOperadorBinario.ADICION, TipoSimbolo.Int),
            new BoundOperadorBinario(TiposSintax.RESTA, BoundTipoOperadorBinario.SUSTRACCION, TipoSimbolo.Int),
            new BoundOperadorBinario(TiposSintax.ASTERISCO, BoundTipoOperadorBinario.MULTIPLICACION, TipoSimbolo.Int),
            new BoundOperadorBinario(TiposSintax.SLASH, BoundTipoOperadorBinario.DIVISION, TipoSimbolo.Int),
            new BoundOperadorBinario(TiposSintax.AMPERSAND, BoundTipoOperadorBinario.Y_BITWISE, TipoSimbolo.Int),
            new BoundOperadorBinario(TiposSintax.PALO, BoundTipoOperadorBinario.O_BITWISE, TipoSimbolo.Int),
            new BoundOperadorBinario(TiposSintax.SOMBRERO, BoundTipoOperadorBinario.XOR_BITWISE, TipoSimbolo.Int),

            new BoundOperadorBinario(TiposSintax.AMPERSAND, BoundTipoOperadorBinario.Y_BITWISE, TipoSimbolo.Int),
            new BoundOperadorBinario(TiposSintax.PALO, BoundTipoOperadorBinario.O_BITWISE, TipoSimbolo.Int),
            new BoundOperadorBinario(TiposSintax.SOMBRERO, BoundTipoOperadorBinario.XOR_BITWISE, TipoSimbolo.Bool),
            new BoundOperadorBinario(TiposSintax.DOBLE_AMPERSAND, BoundTipoOperadorBinario.Y_LOGICO, TipoSimbolo.Bool),
            new BoundOperadorBinario(TiposSintax.DOBLE_PALO, BoundTipoOperadorBinario.O_LOGICO, TipoSimbolo.Bool),

            new BoundOperadorBinario(TiposSintax.IGUALDAD, BoundTipoOperadorBinario.IGUAL_A, TipoSimbolo.Int, TipoSimbolo.Bool),
            new BoundOperadorBinario(TiposSintax.NO_IGUALDAD, BoundTipoOperadorBinario.DIFERENTE_DE, TipoSimbolo.Int, TipoSimbolo.Bool),
            new BoundOperadorBinario(TiposSintax.MENOR, BoundTipoOperadorBinario.MENOR_QUE, TipoSimbolo.Int, TipoSimbolo.Bool),
            new BoundOperadorBinario(TiposSintax.MENOR_IGUAL, BoundTipoOperadorBinario.MENOR_IGUAL_QUE, TipoSimbolo.Int, TipoSimbolo.Bool),
            new BoundOperadorBinario(TiposSintax.MAYOR, BoundTipoOperadorBinario.MAYOR_QUE, TipoSimbolo.Int, TipoSimbolo.Bool),
            new BoundOperadorBinario(TiposSintax.MAYOR_IGUAL, BoundTipoOperadorBinario.MAYOR_IGUAL_QUE, TipoSimbolo.Int, TipoSimbolo.Bool),

            //tambien se puede definir los tipos de desigualdad e igualdad sobre objetos de tipo bool
            new BoundOperadorBinario(TiposSintax.IGUALDAD, BoundTipoOperadorBinario.IGUAL_A, TipoSimbolo.Bool),
            new BoundOperadorBinario(TiposSintax.NO_IGUALDAD, BoundTipoOperadorBinario.DIFERENTE_DE, TipoSimbolo.Bool),

            new BoundOperadorBinario(TiposSintax.SUMA, BoundTipoOperadorBinario.ADICION, TipoSimbolo.String)

        };

        public static BoundOperadorBinario Bind(TiposSintax tipoSintax, TipoSimbolo izq, TipoSimbolo der)
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

