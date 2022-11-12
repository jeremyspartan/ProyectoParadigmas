using ProyectoParadigmas.Clases.Texto;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Windows.Media;

namespace ProyectoParadigmas.Clases.Sintax
{
    internal class Parser
    {
        private readonly ImmutableArray<Token> _tokes;
        private readonly BagDiagnosticos _diagnosticos = new BagDiagnosticos();
        private readonly TextoFuente _texto;
        private int _posicion;

        public Parser(TextoFuente texto)
        {
            var lexer = new Lexer(texto);
            Token elemento;
            var elementos = new List<Token>();

            do
            {
                elemento = lexer.Lex();//paso al caraceter siguiente
                if (elemento.Tipo != TiposSintax.ESPACIO && elemento.Tipo != TiposSintax.TIPO_ERRONEO)
                {
                    elementos.Add(elemento);
                }

            } while (elemento.Tipo != TiposSintax.EOF);

            _texto = texto;
            _tokes = elementos.ToImmutableArray();
            _diagnosticos.AddRange(lexer.Diagnosticos);
        }

        public BagDiagnosticos Diagnosticos => _diagnosticos;

        private Token TokenActual => CheckEOF(0);

        //sirve para evitar agregar el EOF a la lista de elementos lexicos
        private Token CheckEOF(int offset)
        {
            var index = _posicion + offset;
            if (index >= _tokes.Length)
                return _tokes[_tokes.Length - 1];

            return _tokes[index];
        }

        private Token TokenSiguiente()
        {
            var elementoActual = this.TokenActual;
            _posicion++;
            return elementoActual;
        }

        private Token MatchToken(TiposSintax tipo)
        {
            if (TokenActual.Tipo == tipo)
                return TokenSiguiente();

            _diagnosticos.ReportarTokenInesperado(TokenActual.Span, TokenActual.Tipo, tipo);
            return new Token(tipo, TokenActual.Pos, null, null);
        }

        public UnidadCompilacionSintax ParseUnidadCompilacion()
        {
            var declaracion = ParseDeclaracion();
            var eof = MatchToken(TiposSintax.EOF);
            return new UnidadCompilacionSintax(declaracion, eof);
        }

        private DeclaracionSintax ParseDeclaracion()
        {
            switch (TokenActual.Tipo)
            {
                case TiposSintax.LLAVE_APERTURA:
                    return ParseDeclaracionBloque();
                case TiposSintax.LET_CLAVE:
                case TiposSintax.VAR_CLAVE:
                    return ParseDeclaracionVariable();
                case TiposSintax.IF_CLAVE:
                    return ParseDeclaracionIf();
                case TiposSintax.WHILE_CLAVE:
                    return ParseDeclaracionWhile();
                case TiposSintax.FOR_CLAVE:
                    return ParseDeclaracionFor();
                default:
                    return ParseDeclaracionExpresion();
            }
        }

        private BloqueDeclaracionSintax ParseDeclaracionBloque()
        {
            var declaraciones = ImmutableArray.CreateBuilder<DeclaracionSintax>();
            var llaveAperturaToken = MatchToken(TiposSintax.LLAVE_APERTURA);

            while (TokenActual.Tipo != TiposSintax.EOF && TokenActual.Tipo != TiposSintax.LLAVE_CIERRE)
            {
                var inicioToken = TokenActual;

                var declaracion = ParseDeclaracion();
                declaraciones.Add(declaracion);

                //si ParseDeclaracion no consume ningun token,necesitamos saltar el token actual con el fin de evitar que se encicle
                //No necestimos reportar un error porque ya tratamos de analizar una expresion de declaracion y reportar una
                if (TokenActual == inicioToken)
                    TokenSiguiente();
            }

            var llaveCierreToken = MatchToken(TiposSintax.LLAVE_CIERRE);

            return new BloqueDeclaracionSintax(llaveAperturaToken, declaraciones.ToImmutable(), llaveCierreToken);
        }

        private DeclaracionSintax ParseDeclaracionVariable()
        {
            var esperado = TokenActual.Tipo == TiposSintax.LET_CLAVE ? TiposSintax.LET_CLAVE : TiposSintax.VAR_CLAVE;
            var palabraClave = MatchToken(esperado);
            var identificador = MatchToken(TiposSintax.IDENTIFICADOR);
            var equals = MatchToken(TiposSintax.ASIGNACION);
            var inicializador = ParseExpresion();
            return new DeclaracionVariableSintax(palabraClave, identificador, equals, inicializador);
        }

        private DeclaracionSintax ParseDeclaracionIf()
        {
            var clave = MatchToken(TiposSintax.IF_CLAVE);
            var condicion = ParseExpresion();
            var declaracion = ParseDeclaracion();
            var clausulaElse = ParseClausulaElse();
            return new DeclaracionIfSintax(clave, condicion, declaracion, clausulaElse);
        }

        private ClausulaElseSintax ParseClausulaElse()
        {
            if (TokenActual.Tipo != TiposSintax.ELSE_CLAVE)
                return null;
            var clave = TokenSiguiente();
            var declaracion = ParseDeclaracion();
            return new ClausulaElseSintax(clave, declaracion);
        }

        private DeclaracionSintax ParseDeclaracionWhile()
        {
            var clave = MatchToken(TiposSintax.WHILE_CLAVE);
            var condicion = ParseExpresion();
            var cuerpo = ParseDeclaracion();
            return new DeclaracionWhileSintax(clave, condicion, cuerpo);
        }

        private DeclaracionSintax ParseDeclaracionFor()
        {
            var clave = MatchToken(TiposSintax.FOR_CLAVE);
            var identificador = MatchToken(TiposSintax.IDENTIFICADOR);
            var igualToken = MatchToken(TiposSintax.ASIGNACION);
            var lowerBound = ParseExpresion();
            var toClave = MatchToken(TiposSintax.TO_CLAVE);
            var upperBound = ParseExpresion();
            var cuerpo = ParseDeclaracion();
            return new DeclaracionForSintax(clave, identificador, igualToken, lowerBound, toClave, upperBound, cuerpo);

        }


        private ExpresionSintaxDeclaracion ParseDeclaracionExpresion()
        {
            var expresion = ParseExpresion();
            return new ExpresionSintaxDeclaracion(expresion);
        }

        private ExpresionSintax ParseExpresion()
        {
            return ParseExpresionAsignacion();
        }

        private ExpresionSintax ParseExpresionAsignacion()
        {
            /* 
            a + b + 5
            arbol normal
                    +
                    / \
                    +   5
                    / \
                a   b
            Normalmente la evaluacion inicia desde el nodo más a la  izquierda y termina con el nodo más a la derecha

            Si se tiene la expresion de asignaciones a = b = 5 y se aplica la evaluacion anterior el arbol queda de la siguiente manera:
                    =
                    / \
                    =   5
                    / \
                a   b
            Lo cual es incorrecto, porque tenemos que a=b y luego que a y b = 5, la manera correcta es que b=5 y luego que a = b=5
            por lo que el arbol deberia quedar:
                    =
                    / \
                    a   =
                        / \
                    b   5
            */
            if (CheckEOF(0).Tipo == TiposSintax.IDENTIFICADOR && CheckEOF(1).Tipo == TiposSintax.ASIGNACION)
            {
                var tokenIdentificador = TokenSiguiente();
                var operadorToken = TokenSiguiente();
                var der = ParseExpresionAsignacion();
                return new ExpresionSintaxAsignacion(tokenIdentificador, operadorToken, der);
            }

            return ParseExpresionBinaria();
        }

        private ExpresionSintax ParseExpresionBinaria(int parentPrecedence = 0)
        {
            ExpresionSintax izq;
            //operadores unarios como -(1+2) o +(3*1)
            var operadorUnarioPre = TokenActual.Tipo.GetOperadorUnarioPrecendece();
            if (operadorUnarioPre != 0 && operadorUnarioPre >= parentPrecedence)
            {
                var operador = TokenSiguiente();
                var operando = ParseExpresionBinaria(operadorUnarioPre);
                izq = new ExpresionSintaxUnaria(operador, operando);
            }
            else
            {
                izq = ParseExpresionPrimaria();
            }

            while (true)
            {
                var precedence = TokenActual.Tipo.GetOperadorBinarioPrecendece();
                if (precedence == 0 || precedence <= parentPrecedence)
                    break;
                var op = TokenSiguiente();
                var der = ParseExpresionBinaria(precedence);
                izq = new ExpresionSintaxBinaria(izq, op, der);
            }
            return izq;
        }


        private ExpresionSintax ParseExpresionPrimaria()
        {
            switch (TokenActual.Tipo)
            {
                case TiposSintax.PARENTESIS_APERTURA:
                    return ParseExpresionParentesis();
                case TiposSintax.VERDADERO:
                case TiposSintax.FALSO:
                    return ParseLiteralBoolenao();
                case TiposSintax.NUMERO:
                    return ParseNumeroLiteral();
                case TiposSintax.IDENTIFICADOR:
                default:
                    return ParseExpresionNombre();
            }
        }

        private ExpresionSintax ParseExpresionParentesis()
        {
            var izq = MatchToken(TiposSintax.PARENTESIS_APERTURA);
            var expresion = ParseExpresion();
            var der = MatchToken(TiposSintax.PARENTESIS_CIERRE);
            return new ParentesisSintax(izq, expresion, der);
        }

        private ExpresionSintax ParseLiteralBoolenao()
        {
            var isTrue = TokenActual.Tipo == TiposSintax.VERDADERO;
            var palabraReservada = isTrue ? MatchToken(TiposSintax.VERDADERO) : MatchToken(TiposSintax.FALSO);
            return new ExpresionSintaxLiteral(palabraReservada, isTrue);
        }

        private ExpresionSintax ParseNumeroLiteral()
        {
            var elementoNumerico = MatchToken(TiposSintax.NUMERO);
            return new ExpresionSintaxLiteral(elementoNumerico);
        }

        private ExpresionSintax ParseExpresionNombre()
        {
            var tokenIdentificador = MatchToken(TiposSintax.IDENTIFICADOR);
            return new ExpresionSintaxNombre(tokenIdentificador);
        }
    }
}
