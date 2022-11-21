using ProyectoParadigmas.Clases.Texto;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace ProyectoParadigmas.Clases.Sintax
{
    internal class Token : NodoSintax
    {
        public Token(TiposSintax tipo, int pos, string texto, object valor)
        {
            this.Tipo = tipo;
            this.Pos = pos;
            this.Texto = texto;
            this.Valor = valor;
        }

        public string Texto { get; }
        public int Pos { get; }
        public object Valor { get; }
        public override TiposSintax Tipo { get; }
        public override TextoSpan Span => new TextoSpan(Pos, Texto?.Length ?? 0);

        public bool IsMissing => Texto == null;

        public bool isMissing(ArbolSintax arbol)
        {
            if(GetUltimoToken(arbol.Raiz.Declaracion).IsMissing)
            {
                return false;
            }

            return true;
        }

        private static Token GetUltimoToken(NodoSintax nodo)
        {
            if (nodo is Token token)
                return token;

            //un nodo sintax debería siempre contener al menos un token
            return GetUltimoToken(nodo.GetChildren().Last());
        }
    }
}
