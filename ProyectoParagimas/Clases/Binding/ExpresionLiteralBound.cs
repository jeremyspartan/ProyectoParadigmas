using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoParagimas.Clases.Binding
{
    internal class ExpresionLiteralBound : ExpresionBound
    {
        public ExpresionLiteralBound(Object valor)
        {
            Valor = valor;
        }

        public override Type Type => Valor.GetType();
        public override BoundTipoNodo Tipo => BoundTipoNodo.EXPRESION_LITERAL;
        public object Valor { get; }

    }
}
