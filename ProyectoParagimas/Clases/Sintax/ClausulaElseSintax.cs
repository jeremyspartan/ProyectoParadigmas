using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoParadigmas.Clases.Sintax
{
    internal class ClausulaElseSintax : NodoSintax
    {
        public ClausulaElseSintax(Token claveElse, DeclaracionSintax elseDeclaracion)
        {
            ClaveElse = claveElse;
            ElseDeclaracion = elseDeclaracion;
        }

        public override TiposSintax Tipo => TiposSintax.CLAUSULA_ELSE;
        public Token ClaveElse { get; }
        public DeclaracionSintax ElseDeclaracion { get; }
    }
}
