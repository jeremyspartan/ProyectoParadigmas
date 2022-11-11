namespace ProyectoParadigmas.Clases.Sintax
{
    internal class ExpresionSintaxDeclaracion : DeclaracionSintax
    {
        public ExpresionSintaxDeclaracion(ExpresionSintax expresion)
        {
            Expresion = expresion;
        }

        public override TiposSintax Tipo => TiposSintax.EXPRESION_DECLARACION;
        public ExpresionSintax Expresion { get; }
    }
}
