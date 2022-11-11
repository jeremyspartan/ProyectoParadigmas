using ProyectoParadigmas.AnalisisDeCodigo.Binding;
using ProyectoParadigmas.Clases.Binding;
using System;
using System.Collections.Generic;

namespace ProyectoParadigmas.Clases
{
    internal class Evaluador
    {
        private readonly BoundDeclaracion Raiz;
        private readonly Dictionary<SimboloVariable, object> _variables;
        private object _ultimoValor;

        public Evaluador(BoundDeclaracion raiz, Dictionary<SimboloVariable, object> variables)
        {
            Raiz = raiz;
            _variables = variables;
        }

        public object Evaluar()
        {
            EvaluarDeclaracion(this.Raiz);
            return _ultimoValor;
        }

        private void EvaluarDeclaracion(BoundDeclaracion nodo)
        {
            switch (nodo.TipoNodo)
            {
                case BoundTipoNodo.BLOQUE_DECLARACION:
                    EvaluarBloqueDeclaracion((BoundBloqueDeclaracion)nodo);
                    break;
                case BoundTipoNodo.EXPRESION_DECLARACION:
                    EvaluarExpresionDeclaracion((ExpresionDeclaracionBound)nodo);
                    break;
                case BoundTipoNodo.VARIABLE_DECLARACION:
                    EvaluarDeclaracionVariable((BoundDeclaracionVariable)nodo);
                    break;
                default:
                    throw new Exception($"Nodo inesperado {nodo.TipoNodo}");
            }
            static object EvaluarExpresionLiteral(ExpresionLiteralBound n)
            {
                return n.Valor;
            }
        }

        private void EvaluarDeclaracionVariable(BoundDeclaracionVariable nodo)
        {
            var valor = EvaluarExpresion(nodo.Inicializador);
            _variables[nodo.Variable] = valor;
            _ultimoValor = valor;
        }

        private void EvaluarBloqueDeclaracion(BoundBloqueDeclaracion nodo)
        {
            foreach (var declaracion in nodo.Declaraciones)
                EvaluarDeclaracion(declaracion);
        }

        private void EvaluarExpresionDeclaracion(ExpresionDeclaracionBound nodo)
        {
            _ultimoValor = EvaluarExpresion(nodo.Expresion);
        }

        private object EvaluarExpresion(ExpresionBound nodo)
        {
            switch (nodo.TipoNodo)
            {
                case BoundTipoNodo.EXPRESION_LITERAL:
                    return EvaluarExpresionLiteral((ExpresionLiteralBound)nodo);
                case BoundTipoNodo.EXPRESION_VARIABLE:
                    return EvaluarExpresionVariable((ExpresionVariableBound)nodo);
                case BoundTipoNodo.EXPRESION_ASIGNACION:
                    return EvaluarExpresionAsignacion((ExpresionAsignacionBound)nodo);
                case BoundTipoNodo.EXPRESION_UNARIA:
                    return EvaluarExpresionUnaria((ExpresionUnariaBound)nodo);
                case BoundTipoNodo.EXPRESION_BINARIA:
                    return EvaluarExpresionBinaria((ExpresionBinariaBound)nodo);
                default:
                    throw new Exception($"Nodo inesperado {nodo.TipoNodo}");
            }

            static object EvaluarExpresionLiteral(ExpresionLiteralBound n)
            {
                return n.Valor;
            }
        }

        private object EvaluarExpresionVariable(ExpresionVariableBound v)
        {
            return _variables[v.Variable];
        }
        private object EvaluarExpresionAsignacion(ExpresionAsignacionBound a)
        {
            var valor = EvaluarExpresion(a.Expresion);
            _variables[a.Variable] = valor;
            return valor;
        }

        private object EvaluarExpresionUnaria(ExpresionUnariaBound expUni)
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
                    throw new Exception($"OperadorToken unario inesperado {expUni.Operador}");
            }
        }

        private object EvaluarExpresionBinaria(ExpresionBinariaBound expBin)
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
                    throw new Exception($"Declaracion binaria inesperada {expBin.Operador}");
            }
        }
    }
}
