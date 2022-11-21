using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoParadigmas.Clases.Simbolos
{
    public abstract class Simbolo
    {
        public Simbolo(string nombre)
        {
            Nombre = nombre;
        }

        public abstract ClaseSimbolo Clase { get; }
        public string Nombre { get; }
        public override string ToString() => Nombre;
    }
}
