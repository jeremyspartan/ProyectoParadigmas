using ProyectoParadigmas.Clases.Simbolos;
using System;

namespace ProyectoParadigmas.Clases.Binding
{
    internal abstract class ExpresionBound : NodoBound
    {
        public abstract TipoSimbolo Tipo { get; }
    }
}
