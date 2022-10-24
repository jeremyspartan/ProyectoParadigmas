using System.Collections.Generic;

namespace ProyectoParagimas.Clases.Sintax
{
    internal class Parser
    {
        private Token[] Elementos;
        private int pos;
        private Token ElementoActual => CheckEOF(0);
        private List<string> Errores = new List<string>();

        public Parser(string texto)
        {
            var lexer = new Lexer(texto);
            Token elemento;
            var elementos = new List<Token>();

            do
            {
                elemento = lexer.ElementoSiguiente();//paso al caraceter siguiente
                if (elemento.tipo != TiposSintax.ESPACIO && elemento.tipo != TiposSintax.TIPO_ERRONEO)
                {
                    elementos.Add(elemento);
                }

            } while (elemento.tipo != TiposSintax.EOF);

            this.Elementos = elementos.ToArray();
            Errores.AddRange(lexer.errores);
        }

        public IEnumerable<string> Errorres => Errores;

        //sirve para evitar agregar el EOF a la lista de elementos lexicos
        private Token CheckEOF(int offset)
        {
            var index = pos + offset;
            if (index >= Elementos.Length)
                return Elementos[Elementos.Length - 1];

            return Elementos[index];
        }

        private Token ElementoSiguiente()
        {
            var elementoActual = this.ElementoActual;
            pos++;
            return elementoActual;
        }

        private Token Match(TiposSintax tipo)
        {
            if (ElementoActual.tipo == tipo)
                return ElementoSiguiente();

            Errores.Add($"Error: elemento inesperado: <{ElementoActual.tipo}>, se esperaba {tipo}.");
            return new Token(tipo, ElementoActual.pos, null, null);
        }


        public ArbolSintax Parse()
        {
            var expresion = ParseExpresion();
            var eof = Match(TiposSintax.EOF);
            return new ArbolSintax(Errorres, expresion, eof);
        }

        private ExpresionSintax ParseExpresion(int parentPrecedence = 0)
        {
            ExpresionSintax izq;
            //operadores unarios como -(1+2) o +(3*1)
            var operadorUnarioPre = ElementoActual.tipo.GetOperadorUnarioPrecendece();
            if (operadorUnarioPre != 0 && operadorUnarioPre >= parentPrecedence)
            {
                var operador = ElementoSiguiente();
                var operando = ParseExpresion(operadorUnarioPre);
                izq = new ExpresionUnaria(operador, operando);
            }
            else
            {
                izq = ParseExpresionPrimaria();
            }

            while (true)
            {
                var precedence = ElementoActual.tipo.GetOperadorBinarioPrecendece();
                if (precedence == 0 || precedence <= parentPrecedence)
                    break;
                var op = ElementoSiguiente();
                var der = ParseExpresion(precedence);
                izq = new ExpresionBinaria(izq, op, der);
            }
            return izq;
        }


        private ExpresionSintax ParseExpresionPrimaria()
        {
            switch (ElementoActual.tipo)
            {
                case TiposSintax.PARENTESIS_APERTURA:
                {
                    var izq = ElementoSiguiente();
                    var expresion = ParseExpresion();
                    var der = Match(TiposSintax.PARENTESIS_CIERRE);
                    return new ParentesisSintax(izq, expresion, der);
                }

                case TiposSintax.VERDADERO:
                case TiposSintax.FALSO:
                {
                    var palabraReservada = ElementoSiguiente();
                    var valor = palabraReservada.tipo == TiposSintax.VERDADERO;
                    return new ExpresionLiteral(palabraReservada, valor);
                }
                default:
                {
                    var elementoNumerico = Match(TiposSintax.NUMERO);
                    return new ExpresionLiteral(elementoNumerico);
                 }
            }
        }
    }
}
