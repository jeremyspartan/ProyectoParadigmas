using ProyectoParadigmas.Clases.Binding;
using ProyectoParadigmas.Clases.Sintax;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

namespace ProyectoParadigmas.Clases
{
    internal partial class Compilacion
    {
        private BoundGlobalScope _globalScope;

        public Compilacion(ArbolSintax arbolSintax) : this(null, arbolSintax) { }

        private Compilacion(Compilacion previo, ArbolSintax arbolSintax)
        {
            Previo = previo;
            ArbolSintax = arbolSintax;
        }

        public Compilacion Previo { get; }
        public ArbolSintax ArbolSintax { get; }

        internal BoundGlobalScope GlobalScope
        {
            get
            {
                if (_globalScope == null)
                {
                    var globalScope = Binder.BindGlobalScope(Previo?.GlobalScope, ArbolSintax.Raiz);
                    Interlocked.CompareExchange(ref _globalScope, globalScope, null);
                }

                return _globalScope;
            }
        }

        public Compilacion ContinuarCon(ArbolSintax arbolSintax)
        {
            return new Compilacion(this, arbolSintax);
        }

        public ResultadoEvaluacion Evaluar(bool ejecutar, Dictionary<SimboloVariable, object> variables)
        {
            var diagnosticos = ArbolSintax.Diagnosticos.Concat(GlobalScope.Diagnosticos).ToImmutableArray();
            if (diagnosticos.Any())
                return new ResultadoEvaluacion(diagnosticos.ToImmutableArray(), null);

            var evaluador = new Evaluador(GlobalScope.Declaracion, variables);
            if (ejecutar)
            {
                var valor = evaluador.Evaluar();
                return new ResultadoEvaluacion(ImmutableArray<Diagnostico>.Empty, valor);
            }
            else
            {
                return new ResultadoEvaluacion(ImmutableArray<Diagnostico>.Empty, null);
            }
        }
    }
}
