using System;
using ProyectoParadigmas.Clases.Simbolos;

namespace ProyectoParadigmas.Clases.Binding
{
    internal class ExpresionAsignacionBound : ExpresionBound
    {
        public ExpresionAsignacionBound(SimboloVariable variable, ExpresionBound expresion)
        {
            Variable = variable;
            Expresion = expresion;
        }

        public override BoundTipoNodo TipoNodo => BoundTipoNodo.EXPRESION_ASIGNACION;
        public override TipoSimbolo Tipo => Expresion.Tipo;

        public SimboloVariable Variable { get; }
        public ExpresionBound Expresion { get; }
    }
}
