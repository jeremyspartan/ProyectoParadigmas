using System;

namespace ProyectoParadigmas.Clases
{
    internal class SimboloVariable
    {
        public SimboloVariable(string nombre, bool isReadOnly, Type tipo)
        {
            Nombre = nombre;
            IsReadOnly = isReadOnly;
            Tipo = tipo;
        }

        public string Nombre { get; }
        public bool IsReadOnly { get; }
        public Type Tipo { get; }
    }
}
