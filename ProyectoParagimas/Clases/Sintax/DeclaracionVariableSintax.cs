namespace ProyectoParadigmas.Clases.Sintax
{
    internal class DeclaracionVariableSintax : DeclaracionSintax
    {
        public DeclaracionVariableSintax(Token palabraReservada, Token identificador, Token equalsToken, ExpresionSintax inicializador)
        {
            PalabraReservada = palabraReservada;
            Identificador = identificador;
            EqualsToken = equalsToken;
            Inicializador = inicializador;
        }

        public override TiposSintax Tipo => TiposSintax.VARIABLE_DECLARACION;

        public Token PalabraReservada { get; }
        public Token Identificador { get; }
        public Token EqualsToken { get; }
        public ExpresionSintax Inicializador { get; }
    }
}
