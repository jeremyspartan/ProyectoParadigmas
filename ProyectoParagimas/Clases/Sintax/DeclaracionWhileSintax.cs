using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoParadigmas.Clases.Sintax
{
    internal class DeclaracionWhileSintax : DeclaracionSintax
    {
        public DeclaracionWhileSintax(Token claveWhile, ExpresionSintax condicion, DeclaracionSintax cuerpo)
        {
            ClaveWhile = claveWhile;
            Condicion = condicion;
            Cuerpo = cuerpo;
        }

        public override TiposSintax Tipo => TiposSintax.WHILE_DECLRARACION;
        public Token ClaveWhile { get; }
        public ExpresionSintax Condicion { get; }
        public DeclaracionSintax Cuerpo { get; }
    }
}
