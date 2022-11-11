namespace ProyectoParadigmas.Clases.Binding
{
    internal class ExpresionDeclaracionBound : BoundDeclaracion
    {
        public ExpresionDeclaracionBound(ExpresionBound expresion)
        {
            Expresion = expresion;
        }


        public override BoundTipoNodo TipoNodo => BoundTipoNodo.EXPRESION_DECLARACION;
        public ExpresionBound Expresion { get; }
    }
}
