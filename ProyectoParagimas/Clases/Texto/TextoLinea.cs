namespace ProyectoParadigmas.Clases.Texto
{
    internal class TextoLinea
    {
        public TextoLinea(TextoFuente texto, int inicio, int tamannio, int tammanioHastaSaltoLinea)
        {
            Texto = texto;
            Inicio = inicio;
            Tamannio = tamannio;
            TammanioHastaSaltoLinea = tammanioHastaSaltoLinea;
        }

        public TextoFuente Texto { get; }
        public int Inicio { get; }
        public int Tamannio { get; }
        public int Final => Inicio + Tamannio;
        public int TammanioHastaSaltoLinea { get; }
        public TextoSpan Span => new TextoSpan(Inicio, Tamannio);
        public TextoSpan SpanHastaSaltoLinea => new TextoSpan(Inicio, TammanioHastaSaltoLinea);
        public override string ToString() => Texto.ToString(Span);
    }
}
