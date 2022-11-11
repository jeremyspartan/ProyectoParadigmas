using ProyectoParadigmas.Clases.Texto;

namespace ProyectoParadigmas.Clases.Sintax
{

    internal sealed class Lexer
    {

        private readonly TextoFuente _texto;
        private readonly BagDiagnosticos _diagnosticos = new BagDiagnosticos();
        private int _posicion;
        private int _inicio;
        private TiposSintax _tipo;
        private object _valor;

        public Lexer(TextoFuente texto)
        {
            _texto = texto;
        }

        public BagDiagnosticos Diagnosticos => _diagnosticos;

        private char CharActual => EOF(0);
        private char LookAhead => EOF(1);

        private char EOF(int offset)
        {
            var index = _posicion + offset;
            if (_posicion >= _texto.Tamannio)
                return '\0';

            return this._texto[index];
        }

        /*Sirve para identificar y moverse a lo largo de los elementos lexicos dentro del String de entrada y crear los elementos sintacticos que serán procesados en el parser*/
        public Token Lex()
        {

            _inicio = _posicion;
            _tipo = TiposSintax.TIPO_ERRONEO;
            _valor = null;
            switch (CharActual)//Comienzo a crear los Token de acuerdo a los caracteres analizados
            {
                case '\0':
                    _tipo = TiposSintax.EOF;
                    break;
                case '+':
                    _tipo = TiposSintax.SUMA;
                    _posicion++;
                    break;
                case '-':
                    _tipo = TiposSintax.RESTA;
                    _posicion++;
                    break;
                case '*':
                    _tipo = TiposSintax.ASTERISCO;
                    _posicion++;
                    break;
                case '/':
                    _tipo = TiposSintax.SLASH;
                    _posicion++;
                    break;
                case '(':
                    _tipo = TiposSintax.PARENTESIS_APERTURA;
                    _posicion++;
                    break;
                case ')':
                    _tipo = TiposSintax.PARENTESIS_CIERRE;
                    _posicion++;
                    break;
                case '{':
                    _tipo = TiposSintax.LLAVE_APERTURA;
                    _posicion++;
                    break;
                case '}':
                    _tipo = TiposSintax.LLAVE_CIERRE;
                    _posicion++;
                    break;
                case '!':
                    _tipo = TiposSintax.EXCLAMACION_CIERRE;
                    _posicion++;
                    break;
                //la pos se corre para ignorar los operadores logicos
                case '&'://Para el "y"
                    {
                        if (LookAhead == '&')
                        {
                            _posicion += 2;
                            _tipo = TiposSintax.DOBLE_AMPERSAND;
                        }
                        break;
                    }
                case '|'://Para el "o"
                    {
                        if (LookAhead == '|')
                        {
                            _posicion += 2;
                            _tipo = TiposSintax.DOBLE_PALO;
                        }
                        break;
                    }
                case 'e'://Para el "igual"
                    {
                        if (LookAhead == 'q')
                        {
                            _posicion += 2;
                            _tipo = TiposSintax.IGUALDAD;
                        }
                        break;
                    }
                case '='://Para la asignacion
                    {
                        _posicion += 1;
                        _tipo = TiposSintax.ASIGNACION;
                        break;
                    }
                case '¬'://Para el "diferente de"
                    {
                        _posicion += 1;
                        _tipo = TiposSintax.NO_IGUALDAD;
                        break;
                    }
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    LeerNumero();
                    break;
                case ' ':
                case '\t':
                case '\n':
                case '\r':
                    LeerEspacio();
                    break;
                default:
                    if (char.IsLetter(CharActual))//vamos a reconocer booleanos
                    {
                        LeerIdentificadorOPalabraRerservada();
                    }
                    else if (char.IsWhiteSpace(CharActual))
                    {
                        LeerEspacio();
                    }
                    else
                    {
                        _diagnosticos.CaracterIncorrecto(_posicion, CharActual);
                        _posicion++;
                    }
                    break;
            }

            var tamannio = _posicion - _inicio;
            var texto = FactsSintax.GetTexto(_tipo);
            if (texto == null)
                texto = _texto.ToString(_inicio, tamannio);

            return new Token(_tipo, _inicio, texto, _valor);
        }

        private void LeerNumero()
        {
            while (char.IsDigit(CharActual))
                _posicion++;

            var tam = _posicion - _inicio;
            var texto = this._texto.ToString(_inicio, tam);

            if (!int.TryParse(texto, out var valor))
            {
                _diagnosticos.ReportarNumeroInvalido(new TextoSpan(_inicio, tam), texto, typeof(int));
            }

            _valor = valor;
            _tipo = TiposSintax.NUMERO;
        }

        private void LeerEspacio()
        {
            while (char.IsWhiteSpace(CharActual))
                _posicion++;

            _tipo = TiposSintax.ESPACIO;
        }

        private void LeerIdentificadorOPalabraRerservada()
        {
            while (char.IsLetter(CharActual))
                _posicion++;

            var tam = _posicion - _inicio;
            var texto = _texto.ToString(_inicio, tam);
            _tipo = FactsSintax.GetPalabraReservada(texto);
        }
    }
}
