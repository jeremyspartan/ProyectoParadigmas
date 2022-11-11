using System.Collections.Immutable;

namespace ProyectoParadigmas.Clases.Texto
{
    internal class TextoFuente
    {
        private readonly string _texto;
        public ImmutableArray<TextoLinea> Lineas { get; }

        private TextoFuente(string texto)
        {
            Lineas = ParseLineas(texto, this);
            _texto = texto;
        }

        public char this[int index] => _texto[index];

        public int Tamannio => _texto.Length;

        public int GetIndiceLinea(int posicion)
        {
            var lower = 0;
            var upper = Lineas.Length - 1;

            while (lower <= upper)
            {
                var index = lower + (upper - lower) / 2;
                var inicio = Lineas[index].Inicio;

                if (posicion == inicio)
                    return index;
                if (inicio > posicion)
                {
                    upper = index - 1;
                }
                else
                {
                    lower = index + 1;
                }
            }
            return lower - 1;
        }

        private ImmutableArray<TextoLinea> ParseLineas(string texto, TextoFuente textoFuente)
        {
            var posicion = 0;
            var inicioLinea = 0;
            var resultado = ImmutableArray.CreateBuilder<TextoLinea>();

            while (posicion < texto.Length)
            {
                var saltoLineaWidth = GetLineaSaltoWidth(texto, posicion);
                if (saltoLineaWidth == 0)
                {
                    posicion++;
                }
                else
                {
                    AgregarLinea(resultado, textoFuente, posicion, inicioLinea, saltoLineaWidth);
                    posicion += saltoLineaWidth;
                    inicioLinea = posicion;
                }
            }
            if (posicion >= inicioLinea)
                AgregarLinea(resultado, textoFuente, posicion, inicioLinea, 0);


            return resultado.ToImmutable();
        }

        private static void AgregarLinea(ImmutableArray<TextoLinea>.Builder resultado, TextoFuente textoFuente, int posicion, int inicioLinea, int saltoLineaWidth)
        {
            var tamannioLinea = posicion - inicioLinea;
            var tammanioHastaSaltoLinea = tamannioLinea + saltoLineaWidth;
            var linea = new TextoLinea(textoFuente, inicioLinea, tamannioLinea, tammanioHastaSaltoLinea);
            resultado.Add(linea);
        }

        private static int GetLineaSaltoWidth(string texto, int posicion)
        {
            var caracter = texto[posicion];
            var l = posicion + 1 >= texto.Length ? '\0' : texto[posicion + 1];

            if (caracter == '\r' && l == '\n')
                return 2;
            if (caracter == '\r' || caracter == '\n')
                return 1;

            return 0;
        }

        public static TextoFuente From(string texto)
        {
            return new TextoFuente(texto);
        }

        public override string ToString() => _texto;

        public string ToString(int inicio, int tamannio) => _texto.Substring(inicio, tamannio);

        public string ToString(TextoSpan span) => ToString(span.Inicio, span.Tamannio);
    }
}
