namespace ProyectoParadigmas.Clases.Sintax
{
    internal class ParentesisSintax : ExpresionSintax
    {
        public ParentesisSintax(Token parentesisApertura, ExpresionSintax expresion, Token parentesisCierre)
        {
            ParentesisApertura = parentesisApertura;
            Expresion = expresion;
            ParentesisCierre = parentesisCierre;
        }

        public override TiposSintax Tipo => TiposSintax.EXPRESION_PARENTESIS;
        public Token ParentesisApertura { get; }
        public ExpresionSintax Expresion { get; }
        public Token ParentesisCierre { get; }
    }
}
