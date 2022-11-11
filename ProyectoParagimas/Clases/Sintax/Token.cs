using ProyectoParadigmas.Clases.Texto;

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
    }
}
