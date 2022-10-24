using System.Collections.Generic;

namespace ProyectoParagimas.Clases.Sintax
{

    internal sealed class Lexer
    {
        private readonly string texto;
        private int pos;
        private List<string> Errores = new List<string>();

        public Lexer(string texto)
        {
            this.texto = texto;
        }

        public IEnumerable<string> errores => Errores;

        private char CharActual => EOF(0);
        private char LookAhead => EOF(1);

        private char EOF(int offset)
        {
            var index = this.pos + offset;
            if (pos >= texto.Length)
                return '\0';
           
            return this.texto[index];
        }

        private void CharSiguiente()
        {
            pos++;
        }

        /*Sirve para identificar y moverse a lo largo de los elementos lexicos dentro del String de entrada y crear los elementos sintacticos que serán procesados en el parser*/
        public Token ElementoSiguiente()
        {

            if (CharActual.Equals('\0'))
            {
                return new Token(TiposSintax.EOF, pos++, "\0", null);
            }

            if (char.IsDigit(CharActual))
            {
                var inicio = pos;

                while (char.IsDigit(CharActual))
                {
                    CharSiguiente();
                }

                var tam = pos - inicio;
                var texto = this.texto.Substring(inicio, tam);

                if (!int.TryParse(texto, out var valor))
                {
                    Errores.Add($"El numero {texto} no es tipo int32 valido");
                }
                return new Token(TiposSintax.NUMERO, inicio, texto, valor);
            }

            if (char.IsWhiteSpace(CharActual))
            {
                var inicio = pos;

                while (char.IsWhiteSpace(CharActual))
                {
                    CharSiguiente();
                }

                var tam = pos - inicio;
                var texto = this.texto.Substring(inicio, tam);
                return new Token(TiposSintax.ESPACIO, inicio, texto, null);
            }

            //vamos a reconocer booleanos
            if(char.IsLetter(CharActual))
            {
                var inicio = pos;

                while (char.IsLetter(CharActual))
                {
                    CharSiguiente();
                }

                var tam = pos - inicio;
                var texto = this.texto.Substring(inicio, tam);
                var tipo = FactsSintax.GetPalabraReservada(texto);
                return new Token(tipo, inicio, texto, null);
            }

            switch (CharActual)//Comienzo a crear los Token de acuerdo a los caracteres analizados
            {
                case '+':
                    return new Token(TiposSintax.SUMA, pos++, "+", null);
                case '-':
                    return new Token(TiposSintax.RESTA, pos++, "-", null);
                case '*':
                    return new Token(TiposSintax.ASTERISCO, pos++, "*", null);
                case '/':
                    return new Token(TiposSintax.SLASH, pos++, "/", null);
                case '(':
                    return new Token(TiposSintax.PARENTESIS_APERTURA, pos++, "(", null);
                case ')':
                    return new Token(TiposSintax.PARENTESIS_CIERRE, pos++, ")", null);
                case '!':
                    return new Token(TiposSintax.EXCLAMACION_CIERRE, pos++, "!", null);
                case '&'://Para el "y"
                {
                    if(LookAhead == '&')
                            return new Token(TiposSintax.DOBLE_AMPERSAND, pos+=2, "&&", null);
                    break;
                }
                case '|'://Para el "o"
                    {
                        if (LookAhead == '|')
                            return new Token(TiposSintax.DOBLE_PALO, pos += 2, "||", null);
                        break;
                    }
                case 'e'://Para el "igual"
                    {
                        if (LookAhead == 'q')
                            return new Token(TiposSintax.IGUALDAD, pos += 2, "eq", null);
                        break;
                    }
                case '¬'://Para el "diferente de"
                    {
                        return new Token(TiposSintax.NO_IGUALDAD, pos += 1, "¬", null);
                    }

            }
            Errores.Add($"Error: caracter incorrecto: '{CharActual}'");
            return new Token(TiposSintax.TIPO_ERRONEO, pos++, texto.Substring(pos - 1, 1), null);
        }
    }
}
