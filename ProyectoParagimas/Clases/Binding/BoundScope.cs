using System.Collections.Generic;
using System.Collections.Immutable;
using ProyectoParadigmas.Clases.Simbolos;

namespace ProyectoParadigmas.Clases.Binding
{
    internal class BoundScope
    {
        private Dictionary<string, SimboloVariable> _variables = new Dictionary<string, SimboloVariable>();
        public BoundScope Padre { get; }

        public BoundScope(BoundScope padre)
        {
            Padre = padre;
        }

        public bool TryDeclare(SimboloVariable variable)
        {
            if (_variables.ContainsKey(variable.Nombre))
                return false;

            _variables.Add(variable.Nombre, variable);
            return true;
        }

        public bool TryLookUp(string nombre, out SimboloVariable variable)
        {
            if (_variables.TryGetValue(nombre, out variable))
                return true;

            if (Padre == null)
                return false;

            return Padre.TryLookUp(nombre, out variable);
        }

        public ImmutableArray<SimboloVariable> GetVariablesDeclaradas()
        {
            return _variables.Values.ToImmutableArray();
        }
    }
}
