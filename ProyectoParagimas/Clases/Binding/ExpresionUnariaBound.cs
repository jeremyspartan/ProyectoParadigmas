using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoParagimas.Clases.Binding
{
    internal class ExpresionUnariaBound : ExpresionBound
    {
        public ExpresionUnariaBound(BoundOperadorUnario operador, ExpresionBound operando)
        {
            Operador = operador;
            Operando = operando;
        }

        public override BoundTipoNodo Tipo => BoundTipoNodo.EXPRESION_UNARIA;
        public override Type Type => Operador.Tipo;
        public BoundOperadorUnario Operador { get; }
        public ExpresionBound Operando { get; }

    }
}
