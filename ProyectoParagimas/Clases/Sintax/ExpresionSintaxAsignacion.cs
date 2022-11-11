namespace ProyectoParadigmas.Clases.Sintax
{
    internal class ExpresionSintaxAsignacion : ExpresionSintax
    {
        public ExpresionSintaxAsignacion(Token tokenIdientificador, Token tokenIgualdad, ExpresionSintax expresion)
        {
            TokenIdientificador = tokenIdientificador;
            TokenIgualdad = tokenIgualdad;
            Expresion = expresion;
        }

        public override TiposSintax Tipo => TiposSintax.EXPRESION_ASIGNACION;
        public Token TokenIdientificador { get; }
        public Token TokenIgualdad { get; }
        public ExpresionSintax Expresion { get; }
    }
}
