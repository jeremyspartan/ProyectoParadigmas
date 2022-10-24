using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoParagimas.Clases.Sintax
{
    internal class ExpresionLiteral : ExpresionSintax
    {
        public Token ElementoLiteral { get; }
        public object Valor { get; }

        public override TiposSintax tipo => TiposSintax.EXPRESION_LITERAL;

        public ExpresionLiteral(Token elementoLiteral):this(elementoLiteral, elementoLiteral.valor)
        {
        }

        public ExpresionLiteral(Token elementoLiteral, object valor)
        {
            ElementoLiteral = elementoLiteral;
            Valor = valor;
        }

        public override IEnumerable<NodoSintax> GetChildren()
        {
            yield return ElementoLiteral;
        }
    }
}
