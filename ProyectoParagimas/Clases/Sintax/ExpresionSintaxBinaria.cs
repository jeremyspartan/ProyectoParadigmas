namespace ProyectoParadigmas.Clases.Sintax
{
    internal class ExpresionSintaxBinaria : ExpresionSintax
    {
        public ExpresionSintax Izq { get; }
        public ExpresionSintax Der { get; }
        public Token OperadorToken { get; }
        public override TiposSintax Tipo => TiposSintax.EXPRESION_BINARIA;

        public ExpresionSintaxBinaria(ExpresionSintax izq, Token operadorToken, ExpresionSintax der)
        {
            Izq = izq;
            OperadorToken = operadorToken;
            Der = der;
        }
    }
}
