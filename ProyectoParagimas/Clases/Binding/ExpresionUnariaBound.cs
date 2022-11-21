using ProyectoParadigmas.Clases.Simbolos;
using System;

namespace ProyectoParadigmas.Clases.Binding
{
    internal class ExpresionUnariaBound : ExpresionBound
    {
        public ExpresionUnariaBound(BoundOperadorUnario operador, ExpresionBound operando)
        {
            Operador = operador;
            Operando = operando;
        }

        public override BoundTipoNodo TipoNodo => BoundTipoNodo.EXPRESION_UNARIA;
        public override TipoSimbolo Tipo => Operador.Tipo;
        public BoundOperadorUnario Operador { get; }
        public ExpresionBound Operando { get; }

    }
}
