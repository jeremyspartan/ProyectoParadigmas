namespace ProyectoParadigmas.Clases.Sintax
{
    internal class ExpresionSintaxUnaria : ExpresionSintax
    {
        public Token OperadorToken { get; }
        public ExpresionSintax Operando { get; }
        public override TiposSintax Tipo => TiposSintax.EXPRESION_UNARIA;

        public ExpresionSintaxUnaria(Token operadorToken, ExpresionSintax operando)
        {
            OperadorToken = operadorToken;
            Operando = operando;
        }
    }
}
