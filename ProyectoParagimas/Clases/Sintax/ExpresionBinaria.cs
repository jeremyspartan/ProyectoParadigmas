using System.Collections.Generic;

namespace ProyectoParagimas.Clases.Sintax
{
    internal class ExpresionBinaria : ExpresionSintax
    {
        public ExpresionSintax Izq { get; }
        public ExpresionSintax Der { get; }
        public Token Operador { get; }
        public override TiposSintax tipo => TiposSintax.EXPRESION_BINARIA;

        public ExpresionBinaria(ExpresionSintax izq, Token operador, ExpresionSintax der)
        {
            Izq = izq;
            Operador = operador;
            Der = der;
        }

        public override IEnumerable<NodoSintax> GetChildren()
        {
            yield return Izq;
            yield return Operador;
            yield return Der;
        }
    }
}
