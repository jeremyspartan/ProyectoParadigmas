using ProyectoParadigmas.Clases.Texto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace ProyectoParadigmas.Clases.Sintax
{
    /*
     se tiene la entrada 1+2+3, se busca generar un arbol sintactico de la siguiente forma
                    +
                  /   \
                 +     3
               /   \
              1     2
     */
    internal abstract class NodoSintax
    {
        public abstract TiposSintax Tipo { get; }
        public virtual TextoSpan Span
        {
            get
            {
                var primero = GetChildren().First().Span;
                var ultimo = GetChildren().Last().Span;
                return TextoSpan.FromBounds(primero.Inicio, ultimo.Fin);
            }
        }

        public IEnumerable<NodoSintax> GetChildren()
        {
            var propiedades = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var propiedad in propiedades)//se van recorriendo las propiedades y preguntando...
            {
                if (typeof(NodoSintax).IsAssignableFrom(propiedad.PropertyType))//...si puedo tomar alguna de propiedad que pueda convertir a nodo?
                {
                    var hijo = (NodoSintax)propiedad.GetValue(this);
                    if(hijo != null)
                        yield return hijo;
                }
                else if (typeof(IEnumerable<NodoSintax>).IsAssignableFrom(propiedad.PropertyType))
                {
                    var hijos = (IEnumerable<NodoSintax>)propiedad.GetValue(this);
                    foreach (var hijo in hijos)
                    {
                        if (hijo != null)
                        yield return hijo;
                    }
                        
                }
            }
        }

        private void PrettyPrint(NodoSintax nodo, String indent = "", bool isLast = true)
        {
            var marker = isLast ? "└──" : "├──";
            Trace.Write(indent);
            Trace.Write(marker);
            Trace.Write(nodo.Tipo);
            if (nodo is Token t && t.Valor != null)
            {
                Trace.Write("");
                Trace.Write(t.Valor);
            }
            Trace.WriteLine("");
            indent += isLast ? "    " : "│   ";

            var lastChild = nodo.GetChildren().LastOrDefault();

            foreach (var child in nodo.GetChildren())
                PrettyPrint(child, indent, child == lastChild);
        }
    }
}
