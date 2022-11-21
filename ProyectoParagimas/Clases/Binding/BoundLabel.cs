using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoParadigmas.Clases.Binding
{
    internal class BoundLabel
    {
        public BoundLabel(string nombre)
        {
            Nombre = nombre;
        }

        public string Nombre { get; }
    }
}
