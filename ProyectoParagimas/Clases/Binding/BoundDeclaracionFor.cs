namespace ProyectoParadigmas.Clases.Binding
{
    internal class BoundDeclaracionFor : BoundDeclaracion
    {
        

        public BoundDeclaracionFor(SimboloVariable variable, ExpresionBound lowerBound, ExpresionBound upperBound, BoundDeclaracion cuerpo)
        {
            Variable = variable;
            LowerBound = lowerBound;
            UpperBound = upperBound;
            Cuerpo = cuerpo;
        }

        public override BoundTipoNodo TipoNodo => BoundTipoNodo.FOR_DECLRARACION;
        public SimboloVariable Variable { get; }
        public ExpresionBound LowerBound { get; }
        public ExpresionBound UpperBound { get; }
        public BoundDeclaracion Cuerpo { get; }
    }
}