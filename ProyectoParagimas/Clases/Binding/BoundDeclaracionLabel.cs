using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoParadigmas.Clases.Binding
{
    internal class BoundDeclaracionLabel : BoundDeclaracion
    {
        public BoundDeclaracionLabel(BoundLabel label)
        {
            Label = label;
        }

        public BoundLabel Label { get; }

        public override BoundTipoNodo TipoNodo => BoundTipoNodo.LABEL_DECLRARACION;
    }
}
