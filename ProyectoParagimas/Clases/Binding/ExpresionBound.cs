using System;

namespace ProyectoParadigmas.Clases.Binding
{
    internal abstract class ExpresionBound : NodoBound
    {
        public abstract Type Tipo { get; }
    }
}
