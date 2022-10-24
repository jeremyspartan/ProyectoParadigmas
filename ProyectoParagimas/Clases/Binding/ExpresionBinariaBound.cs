using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoParagimas.Clases.Binding
{
    internal class ExpresionBinariaBound : ExpresionBound
    {
        public ExpresionBinariaBound(ExpresionBound izq, BoundOperadorBinario operador, ExpresionBound der)
        {
            Izq = izq;
            Operador = operador;
            Der = der;
        }

        public override BoundTipoNodo Tipo => BoundTipoNodo.EXPRESION_BINARIA;
        public override Type Type => Operador.Tipo;
        public ExpresionBound Izq { get; }
        public BoundOperadorBinario Operador { get; }
        public ExpresionBound Der { get; }
    }
}
