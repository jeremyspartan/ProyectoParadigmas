using ProyectoParadigmas.Clases.Simbolos;
using System;

namespace ProyectoParadigmas.Clases.Binding
{
    internal class ExpresionBinariaBound : ExpresionBound
    {
        public ExpresionBinariaBound(ExpresionBound izq, BoundOperadorBinario operador, ExpresionBound der)
        {
            Izq = izq;
            Operador = operador;
            Der = der;
        }

        public override BoundTipoNodo TipoNodo => BoundTipoNodo.EXPRESION_BINARIA;
        public override TipoSimbolo Tipo => Operador.Tipo;
        public ExpresionBound Izq { get; }
        public BoundOperadorBinario Operador { get; }
        public ExpresionBound Der { get; }
    }
}
