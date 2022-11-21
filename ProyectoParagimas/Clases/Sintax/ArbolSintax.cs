using ProyectoParadigmas.Clases.Texto;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace ProyectoParadigmas.Clases.Sintax
{
    internal class ArbolSintax
    {
        public TextoFuente Texto { get; }
        public ImmutableArray<Diagnostico> Diagnosticos { get; }
        public UnidadCompilacionSintax Raiz { get; }

        private ArbolSintax(TextoFuente texto)
        {
            var parser = new Parser(texto);
            var raiz = parser.ParseUnidadCompilacion();
            Diagnosticos = parser.Diagnosticos.ToImmutableArray();
            Texto = texto;
            Raiz = raiz;
        }

        public static ArbolSintax Parse(string texto)
        {
            var textoFuente = TextoFuente.From(texto);
            return Parse(textoFuente);
        }

        public static ArbolSintax Parse(TextoFuente texto)
        {
            return new ArbolSintax(texto);
        }

        public static IEnumerable<Token> ParseTokens(string texto)
        {
            var textoFuente = TextoFuente.From(texto);
            return ParseTokens(textoFuente);
        }

        public static IEnumerable<Token> ParseTokens(TextoFuente texto)
        {
            var lexer = new Lexer(texto);
            while (true)
            {
                var token = lexer.Lex();
                if (token.Tipo == TiposSintax.EOF)
                    break;

                yield return token;
            }
        }
    }
}
