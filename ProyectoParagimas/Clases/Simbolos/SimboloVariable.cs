using System;

namespace ProyectoParadigmas.Clases.Simbolos
{
    internal class SimboloVariable : Simbolo
    {
        public SimboloVariable(string nombre, bool isReadOnly, TipoSimbolo tipo) : base(nombre)
        {
            IsReadOnly = isReadOnly;
            Tipo = tipo;
        }

        public override ClaseSimbolo Clase => ClaseSimbolo.VARIABLE;
        public bool IsReadOnly { get; }
        public TipoSimbolo Tipo { get; }
    }
}
