namespace ProyectoParadigmas.Clases.Sintax
{
    internal class UnidadCompilacionSintax : NodoSintax
    {
        public UnidadCompilacionSintax(DeclaracionSintax declaracion, Token eofToken)
        {
            Declaracion = declaracion;
            EofToken = eofToken;
        }

        public override TiposSintax Tipo => TiposSintax.UNIDAD_COMPILACION;
        public DeclaracionSintax Declaracion { get; }
        public Token EofToken { get; }
    }
}
