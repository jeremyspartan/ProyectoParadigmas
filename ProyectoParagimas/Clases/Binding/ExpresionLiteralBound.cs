using ProyectoParadigmas.Clases.Simbolos;
using System;

namespace ProyectoParadigmas.Clases.Binding
{
    internal class ExpresionLiteralBound : ExpresionBound
    {
        public ExpresionLiteralBound(object valor)
        {
            Valor = valor;

            if (valor is bool)
                Tipo = TipoSimbolo.Bool;
            else if (valor is int)
                Tipo = TipoSimbolo.Int;
            else if (valor is string)
                Tipo = TipoSimbolo.String;
            else
                throw new Exception($"Literal inesperado '{valor}' de tipo {valor.GetType()}.");
        }

        public override TipoSimbolo Tipo { get; }
        public override BoundTipoNodo TipoNodo => BoundTipoNodo.EXPRESION_LITERAL;
        public object Valor { get; }

    }
}
