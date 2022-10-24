using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoParagimas.Clases.Sintax
{
    internal class Token : NodoSintax
    {
        public string texto { get; }
        public int pos { get; }
        public object valor { get; }
        public override TiposSintax tipo { get; }

        public Token(TiposSintax tipo, int pos, string texto, object valor)
        {
            this.tipo = tipo;
            this.pos = pos;
            this.texto = texto;
            this.valor = valor;
        }


        public override IEnumerable<NodoSintax> GetChildren()
        {
            return Enumerable.Empty<NodoSintax>();
        }
    }
}
