using System.Collections.Immutable;

namespace ProyectoParadigmas.Clases
{
    public class ResultadoEvaluacion
    {
        public ResultadoEvaluacion(ImmutableArray<Diagnostico> diagnosticos, object valor)
        {
            Diagnosticos = diagnosticos;
            Valor = valor;
        }

        public ImmutableArray<Diagnostico> Diagnosticos { get; }
        public object Valor { get; }
    }
}