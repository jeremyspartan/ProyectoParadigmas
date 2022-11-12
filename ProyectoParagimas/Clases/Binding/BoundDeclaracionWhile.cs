using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoParadigmas.Clases.Binding
{
    internal class BoundDeclaracionWhile : BoundDeclaracion
    {
        public BoundDeclaracionWhile(ExpresionBound condicion, BoundDeclaracion cuerpo)
        {
            Condicion = condicion;
            Cuerpo = cuerpo;
        }

        public override BoundTipoNodo TipoNodo => BoundTipoNodo.WHILE_DECLRACION;
        public ExpresionBound Condicion { get; }
        public BoundDeclaracion Cuerpo { get; }
    }
}
