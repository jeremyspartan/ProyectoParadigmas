using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoParagimas.Clases.Sintax
{
    internal class ParentesisSintax : ExpresionSintax
    {
        public ParentesisSintax(Token parentesisApertura, ExpresionSintax expresion, Token parentesisCierre)
        {
            ParentesisApertura = parentesisApertura;
            Expresion = expresion;
            ParentesisCierre = parentesisCierre;
        }

        public override TiposSintax tipo => TiposSintax.EXPRESION_PARENTESIS;
        public Token ParentesisApertura { get; }
        public ExpresionSintax Expresion { get; }
        public Token ParentesisCierre { get; }

        public override IEnumerable<NodoSintax> GetChildren()
        {
            yield return ParentesisApertura;
            yield return Expresion;
            yield return ParentesisCierre;

        }
    }
}
