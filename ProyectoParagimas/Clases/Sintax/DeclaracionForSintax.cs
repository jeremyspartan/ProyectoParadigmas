using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoParadigmas.Clases.Sintax
{
    //declaraciones tipo for i = 1 to 10
    //{
    // *hacer algo*
    //}
    internal class DeclaracionForSintax : DeclaracionSintax
    {
        public DeclaracionForSintax(Token clave, Token identificador, Token igualdToken, ExpresionSintax lowerBound, Token toToken, ExpresionSintax upperBound, DeclaracionSintax cuerpo)
        {
            Clave = clave;
            Identificador = identificador;
            IgualToken = igualdToken;
            LowerBound = lowerBound;
            ToToken = toToken;
            UpperBound = upperBound;
            Cuerpo = cuerpo;
        }

        public override TiposSintax Tipo => TiposSintax.FOR_DECLRARACION;
        public Token Clave { get; }
        public Token Identificador { get; }
        public Token IgualToken { get; }
        public ExpresionSintax LowerBound { get; }
        public Token ToToken { get; }
        public ExpresionSintax UpperBound { get; }
        public DeclaracionSintax Cuerpo { get; }
    }
}
