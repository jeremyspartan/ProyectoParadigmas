using System.Collections.Generic;
using System.Linq;
namespace ProyectoParagimas.Clases.Sintax
{
    internal class ArbolSintax
    {
        public IReadOnlyList<string> errores { get; }
        public ExpresionSintax raiz { get; }
        public Token eof { get; }

        public ArbolSintax(IEnumerable<string> errores, ExpresionSintax raiz, Token eof)
        {
            this.errores = errores.ToArray();
            this.raiz = raiz;
            this.eof = eof;
        }

        public static ArbolSintax Parse(string texto)
        {
            var parser = new Parser(texto);
            return parser.Parse();
        }
    }
}
