using System;
using ProyectoParagimas.Clases.Binding;

namespace ProyectoParagimas.Clases
{
    internal class Evaluador
    {
        private readonly ExpresionBound Raiz;

        public Evaluador(ExpresionBound raiz)
        {
            this.Raiz = raiz;
        }

        public object Evaluar()
        {
            return EvaluarExpresion(this.Raiz);
        }

        private object EvaluarExpresion(ExpresionBound nodo)
        {
            if (nodo is ExpresionLiteralBound expNum)
            {
                return expNum.Valor;
            }

            if (nodo is ExpresionUnariaBound expUni)
            {
                var operando = EvaluarExpresion(expUni.Operando);

                switch (expUni.Operador.TipoOperador)
                {
                    case BoundTipoOperadorUnario.IDENTIDAD:
                        return (int)operando;
                    case BoundTipoOperadorUnario.NEGACION:
                        return -(int)operando;
                    case BoundTipoOperadorUnario.NEGACION_LOGICA:
                        return !(bool)operando;
                    default:
                        throw new Exception($"Operador unario inesperado {expUni.Operador}");
                }
            }

            if (nodo is ExpresionBinariaBound expBin)
            {
                var izq = EvaluarExpresion(expBin.Izq);
                var der = EvaluarExpresion(expBin.Der);

                switch (expBin.Operador.TipoOperador)
                {
                    case BoundTipoOperadorBinario.ADICION:
                        return (int)izq + (int)der;
                    case BoundTipoOperadorBinario.SUSTRACCION:
                        return (int)izq - (int)der;
                    case BoundTipoOperadorBinario.MULTIPLICACION:
                        return (int)izq * (int)der;
                    case BoundTipoOperadorBinario.DIVISION:
                        return (int)izq / (int)der;
                    case BoundTipoOperadorBinario.Y_LOGICO:
                        return (bool)izq && (bool)der;
                    case BoundTipoOperadorBinario.O_LOGICO:
                        return (bool)izq || (bool)der;
                    case BoundTipoOperadorBinario.IGUAL_A:
                        return Equals(izq, der);
                    case BoundTipoOperadorBinario.DIFERENTE_DE:
                        return !Equals(izq, der);
                    default:
                        throw new Exception($"Expresion binaria inesperada {expBin.Operador}");
                }
            }
            throw new Exception($"Nodo inesperado {nodo.Tipo}");
        }
    }
}
