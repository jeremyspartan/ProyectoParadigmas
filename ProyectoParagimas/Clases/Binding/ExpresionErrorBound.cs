using ProyectoParadigmas.Clases.Simbolos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoParadigmas.Clases.Binding
{
    internal class ExpresionErrorBound : ExpresionBound
    {
        
        public override BoundTipoNodo TipoNodo => BoundTipoNodo.EXPRESION_ERROR;
        public override TipoSimbolo Tipo => TipoSimbolo.Error;
    }
}
