namespace ProyectoParadigmas.Clases.Binding
{
    internal class BoundDeclaracionVariable : BoundDeclaracion
    {
        public BoundDeclaracionVariable(SimboloVariable variable, ExpresionBound inicializador)
        {
            Variable = variable;
            Inicializador = inicializador;
        }

        public override BoundTipoNodo TipoNodo => BoundTipoNodo.VARIABLE_DECLARACION;
        public SimboloVariable Variable { get; }
        public ExpresionBound Inicializador { get; }
    }
}
