using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoParagimas.Clases.Sintax
{
    internal class ExpresionUnaria : ExpresionSintax
    {
        public Token Operador { get; }
        public ExpresionSintax Operando { get; }
        public override TiposSintax tipo => TiposSintax.EXPRESION_UNARIA;

        public ExpresionUnaria(Token operador, ExpresionSintax operando)
        {
            Operador = operador;
            Operando = operando;
        }

        public override IEnumerable<NodoSintax> GetChildren()
        {
            yield return Operador;
            yield return Operando;
        }
    }
}
