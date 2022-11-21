using System.Collections.Immutable;
using ProyectoParadigmas.Clases.Simbolos;

namespace ProyectoParadigmas.Clases.Binding
{
    internal class BoundGlobalScope
    {
        public BoundGlobalScope(BoundGlobalScope anterior, ImmutableArray<Diagnostico> diagnosticos, ImmutableArray<SimboloVariable> variables, BoundDeclaracion declaracion)
        {
            Anterior = anterior;
            Diagnosticos = diagnosticos;
            Variables = variables;
            Declaracion = declaracion;
        }

        public BoundGlobalScope Anterior { get; }
        public ImmutableArray<Diagnostico> Diagnosticos { get; }
        public ImmutableArray<SimboloVariable> Variables { get; }
        public BoundDeclaracion Declaracion { get; }
    }
}
