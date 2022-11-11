using ProyectoParadigmas.Clases.Texto;

namespace ProyectoParadigmas.Clases
{
    public class Diagnostico
    {

        public Diagnostico(TextoSpan textSpan, string mensaje)
        {
            TextSpan = textSpan;
            Mensaje = mensaje;
        }

        public TextoSpan TextSpan { get; }
        public string Mensaje { get; }

        public override string ToString() => Mensaje;
    }
}
