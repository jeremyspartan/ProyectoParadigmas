using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoParadigmas.Clases.Binding
{
    internal class BoundDeclaracionIf : BoundDeclaracion
    {
        public BoundDeclaracionIf(ExpresionBound condicion, BoundDeclaracion thenDeclaracion, BoundDeclaracion elseDeclaracion)
        {
            Condicion = condicion;
            ThenDeclaracion = thenDeclaracion;
            ElseDeclaracion = elseDeclaracion;
        }

        public override BoundTipoNodo TipoNodo => BoundTipoNodo.IF_DECLARACION;

        public ExpresionBound Condicion { get; }
        public BoundDeclaracion ThenDeclaracion { get; }
        public BoundDeclaracion ElseDeclaracion { get; }
    }
}
