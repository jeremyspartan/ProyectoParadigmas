namespace ProyectoParadigmas.Clases.Texto
{
    public struct TextoSpan
    {
        public TextoSpan(int inicio, int tamannio)
        {
            Inicio = inicio;
            Tamannio = tamannio;
        }

        public int Inicio { get; }
        public int Tamannio { get; }
        public int Fin => Inicio + Tamannio;

        public static TextoSpan FromBounds(int inicio, int fin)
        {
            var tamannio = fin - inicio;
            return new TextoSpan(inicio, tamannio);
        }

        public override string ToString() => $"{Inicio}..{Fin}";
    }
}
