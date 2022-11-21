using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoParadigmas.Clases.Binding
{
    internal class BoundDeclaracionGotTo : BoundDeclaracion
    {
        public BoundDeclaracionGotTo(BoundLabel label)
        {
            Label = label;
        }

        public override BoundTipoNodo TipoNodo => BoundTipoNodo.GOTO_DECLARACION;

        public BoundLabel Label { get; }
    }
}
