using System.Collections.Immutable;

namespace ProyectoParadigmas.Clases.Sintax
{
    internal class BloqueDeclaracionSintax : DeclaracionSintax
    {
        public BloqueDeclaracionSintax(Token llaveApertura, ImmutableArray<DeclaracionSintax> declaraciones, Token llaveCierre)
        {
            LlaveApertura = llaveApertura;
            Declaraciones = declaraciones;
            LlaveCierre = llaveCierre;
        }

        public override TiposSintax Tipo => TiposSintax.BLOQUE_DECLARACION;
        public Token LlaveApertura { get; }
        public ImmutableArray<DeclaracionSintax> Declaraciones { get; }
        public Token LlaveCierre { get; }
    }
}
