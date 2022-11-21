using ProyectoParadigmas.Clases.Sintax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace ProyectoParadigmas.Clases.Binding
{
    internal abstract class NodoBound
    {
        public abstract BoundTipoNodo TipoNodo { get; }

        public IEnumerable<NodoBound> GetChildren()
        {
            var propiedades = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var propiedad in propiedades)//se van recorriendo las propiedades y preguntando...
            {
                if (typeof(NodoSintax).IsAssignableFrom(propiedad.PropertyType))//...si puedo tomar alguna de propiedad que pueda convertir a nodo?
                {
                    var hijo = (NodoBound)propiedad.GetValue(this);
                    if (hijo != null)
                        yield return hijo;
                }
                else if (typeof(IEnumerable<NodoBound>).IsAssignableFrom(propiedad.PropertyType))
                {
                    var hijos = (IEnumerable<NodoBound>)propiedad.GetValue(this);
                    foreach (var hijo in hijos)
                    {
                        if (hijo != null)
                            yield return hijo;
                    }

                }
            }
        }
    }
}
