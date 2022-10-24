using System;
using System.Collections.Generic;

namespace ProyectoParagimas.Clases.Sintax
{
    /*
     se tiene la entrada 1+2+3, se busca generar un arbol sintactico de la siguiente forma
                    +
                  /   \
                 +     3
               /   \
              1     2
     */
    internal abstract class NodoSintax
    {
        public abstract TiposSintax tipo { get; }

        public abstract IEnumerable<NodoSintax> GetChildren();
    }
}
