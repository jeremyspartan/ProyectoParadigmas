namespace ProyectoParadigmas.Clases.Sintax
{
    internal class ExpresionSintaxNombre : ExpresionSintax
    {
        public ExpresionSintaxNombre(Token tokenIdientificador)
        {
            TokenIdentificador = tokenIdientificador;
        }

        public override TiposSintax Tipo => TiposSintax.EXPRESION_NOMBRE;
        public Token TokenIdentificador { get; }
    }
}
