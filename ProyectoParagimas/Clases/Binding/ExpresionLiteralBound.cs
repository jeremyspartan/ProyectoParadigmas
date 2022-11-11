using System;

namespace ProyectoParadigmas.Clases.Binding
{
    internal class ExpresionLiteralBound : ExpresionBound
    {
        public ExpresionLiteralBound(object valor)
        {
            Valor = valor;
        }

        public override Type Tipo => Valor.GetType();
        public override BoundTipoNodo TipoNodo => BoundTipoNodo.EXPRESION_LITERAL;
        public object Valor { get; }

    }
}
