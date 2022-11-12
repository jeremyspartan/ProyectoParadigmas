using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoParadigmas.Clases.Sintax
{
    internal class DeclaracionIfSintax :  DeclaracionSintax
    {
        public DeclaracionIfSintax(Token claveIf, ExpresionSintax condicion, DeclaracionSintax thenDeclaracion,ClausulaElseSintax clausulaElse)
        {
            ClaveIf = claveIf;
            Condicion = condicion;
            ThenDeclaracion = thenDeclaracion;
            ClausulaElse = clausulaElse;
        }

        public override TiposSintax Tipo => TiposSintax.IF_DECLARACION;
        public Token ClaveIf { get; }
        public ExpresionSintax Condicion { get; }
        public DeclaracionSintax ThenDeclaracion { get; }
        public ClausulaElseSintax ClausulaElse { get; }
    }
}
