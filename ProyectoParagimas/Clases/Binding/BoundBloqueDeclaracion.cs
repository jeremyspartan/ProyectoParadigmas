using System.Collections.Immutable;

namespace ProyectoParadigmas.Clases.Binding
{
    internal class BoundBloqueDeclaracion : BoundDeclaracion
    {
        public BoundBloqueDeclaracion(ImmutableArray<BoundDeclaracion> declaraciones)
        {
            Declaraciones = declaraciones;
        }

        public override BoundTipoNodo TipoNodo => BoundTipoNodo.BLOQUE_DECLARACION;
        public ImmutableArray<BoundDeclaracion> Declaraciones { get; }

    }
}
