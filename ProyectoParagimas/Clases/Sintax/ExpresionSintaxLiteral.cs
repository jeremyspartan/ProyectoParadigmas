namespace ProyectoParadigmas.Clases.Sintax
{
    internal class ExpresionSintaxLiteral : ExpresionSintax
    {
        public Token ElementoLiteral { get; }
        public object Valor { get; }

        public override TiposSintax Tipo => TiposSintax.EXPRESION_LITERAL;

        public ExpresionSintaxLiteral(Token elementoLiteral) : this(elementoLiteral, elementoLiteral.Valor)
        {
        }

        public ExpresionSintaxLiteral(Token elementoLiteral, object valor)
        {
            ElementoLiteral = elementoLiteral;
            Valor = valor;
        }
    }
}
