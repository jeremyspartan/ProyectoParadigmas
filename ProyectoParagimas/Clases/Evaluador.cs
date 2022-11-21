using ProyectoParadigmas.AnalisisDeCodigo.Binding;
using ProyectoParadigmas.Clases.Binding;
using ProyectoParadigmas.Clases.Simbolos;
using System;
using System.CodeDom;
using System.Collections.Generic;

namespace ProyectoParadigmas.Clases
{
    internal class Evaluador
    {
        private readonly BoundBloqueDeclaracion Raiz;
        private readonly Dictionary<SimboloVariable, object> _variables;
        private object _ultimoValor;

        public Evaluador(BoundBloqueDeclaracion raiz, Dictionary<SimboloVariable, object> variables)
        {
            Raiz = raiz;
            _variables = variables;
        }

        public object Evaluar()
        {
            var labelToIndex = new Dictionary<BoundLabel, int>();

            for (var i = 0; i < Raiz.Declaraciones.Length; i++)
            {
                if (Raiz.Declaraciones[i] is BoundDeclaracionLabel l)
                    labelToIndex.Add(l.Label, i + 1);
            }

            var index = 0;
            while (index < Raiz.Declaraciones.Length)
            {
                var s = Raiz.Declaraciones[index];
                switch (s.TipoNodo)
                {
                    case BoundTipoNodo.VARIABLE_DECLARACION:
                        EvaluarVariableDeclaracion((BoundDeclaracionVariable)s);
                        index++;
                        break;
                    case BoundTipoNodo.EXPRESION_DECLARACION:
                        EvaluarExpresionDeclaracion((ExpresionDeclaracionBound)s);
                        index++;
                        break;
                    case BoundTipoNodo.GOTO_DECLARACION:
                        var dg = (BoundDeclaracionGotTo)s;
                        index = labelToIndex[dg.Label];
                        break;
                    case BoundTipoNodo.GOTO_CONDICIONAL_DECLARACION:
                        var dcg= (BoundDeclaracionCondicionalGoto)s;
                        var condicion = (bool)EvaluarExpresion(dcg.Condicion);
                        if(condicion == dcg.JumpIfTrue)
                            index = labelToIndex[dcg.Label];
                        else
                            index++;
                        break;
                    case BoundTipoNodo.LABEL_DECLRARACION:
                        index++;
                        break;
                    default:
                        throw new Exception($"Nodo inesperado {s.TipoNodo}");
                }
            }
            
            return _ultimoValor;
        }

        private void EvaluarVariableDeclaracion(BoundDeclaracionVariable nodo)
        {
            var valor = EvaluarExpresion(nodo.Inicializador);
            _variables[nodo.Variable] = valor;
            _ultimoValor = valor;
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
                case BoundTipoOperadorUnario.RESTA:
                    return -(int)operando;
                case BoundTipoOperadorUnario.NEGACION_LOGICA:
                    return !(bool)operando;
                case BoundTipoOperadorUnario.COMPLEMENTO:
                    return ~(int)operando;
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
                    if(expBin.Tipo == TipoSimbolo.Int)
                        return (int)izq + (int)der;
                    else
                        return (string)izq + (string)der;
                case BoundTipoOperadorBinario.SUSTRACCION:
                    return (int)izq - (int)der;
                case BoundTipoOperadorBinario.MULTIPLICACION:
                    return (int)izq * (int)der;
                case BoundTipoOperadorBinario.DIVISION:
                    return (int)izq / (int)der;
                case BoundTipoOperadorBinario.Y_BITWISE:
                    if (expBin.Tipo == TipoSimbolo.Int)
                        return (int)izq & (int)der;
                    else
                        return (bool)izq & (bool)der;
                case BoundTipoOperadorBinario.O_BITWISE:
                    if (expBin.Tipo == TipoSimbolo.Int)
                        return (int)izq | (int)der;
                    else
                        return (bool)izq | (bool)der;
                case BoundTipoOperadorBinario.XOR_BITWISE:
                    if (expBin.Tipo == TipoSimbolo.Int)
                        return (int)izq ^ (int)der;
                    else
                        return (bool)izq ^ (bool)der;
                case BoundTipoOperadorBinario.Y_LOGICO:
                    return (bool)izq && (bool)der;
                case BoundTipoOperadorBinario.O_LOGICO:
                    return (bool)izq || (bool)der;
                case BoundTipoOperadorBinario.IGUAL_A:
                    return Equals(izq, der);
                case BoundTipoOperadorBinario.DIFERENTE_DE:
                    return !Equals(izq, der);
                case BoundTipoOperadorBinario.MENOR_QUE:
                    return (int)izq < (int)der;
                case BoundTipoOperadorBinario.MAYOR_QUE:
                    return (int)izq > (int)der;
                case BoundTipoOperadorBinario.MENOR_IGUAL_QUE:
                    return (int)izq <= (int)der;
                case BoundTipoOperadorBinario.MAYOR_IGUAL_QUE:
                    return (int)izq >= (int)der;
                default:
                    throw new Exception($"ThenDeclaracion binaria inesperada {expBin.Operador}");
            }
        }
    }
}
