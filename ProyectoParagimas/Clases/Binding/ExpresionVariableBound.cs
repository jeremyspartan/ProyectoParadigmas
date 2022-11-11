using System;

namespace ProyectoParadigmas.Clases.Binding
{
    internal class ExpresionVariableBound : ExpresionBound
    {
        public ExpresionVariableBound(SimboloVariable variable)
        {
            Variable = variable;
        }


        public override BoundTipoNodo TipoNodo => BoundTipoNodo.EXPRESION_VARIABLE;
        public override Type Tipo => Variable.Tipo;
        public SimboloVariable Variable { get; }
    }
}
