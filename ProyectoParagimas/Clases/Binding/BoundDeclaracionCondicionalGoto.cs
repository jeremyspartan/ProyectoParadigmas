using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoParadigmas.Clases.Binding
{
    internal class BoundDeclaracionCondicionalGoto : BoundDeclaracion
    {


        public BoundDeclaracionCondicionalGoto(BoundLabel label, ExpresionBound condicion, bool jumpIfTrue = true)
        {
            Label = label;
            Condicion = condicion;
            JumpIfTrue = jumpIfTrue;
        }

        public override BoundTipoNodo TipoNodo => BoundTipoNodo.GOTO_CONDICIONAL_DECLARACION;
        public BoundLabel Label { get; }
        public ExpresionBound Condicion { get; }
        public bool JumpIfTrue { get; }
    }
}
