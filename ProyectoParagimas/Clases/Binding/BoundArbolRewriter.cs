using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoParadigmas.Clases.Binding
{
    internal class BoundArbolRewriter
    {
        public virtual ExpresionBound RewriteExpresion(ExpresionBound nodo)
        {
            switch(nodo.TipoNodo)
            {
                case BoundTipoNodo.EXPRESION_ERROR:
                    return  RewriteExpresionError((ExpresionErrorBound) nodo);
                case BoundTipoNodo.EXPRESION_LITERAL:
                    return RewriteExpresionLiteral((ExpresionLiteralBound)nodo);
                case BoundTipoNodo.EXPRESION_VARIABLE:
                    return RewriteExpresionVariable((ExpresionVariableBound) nodo);
                case BoundTipoNodo.EXPRESION_ASIGNACION:
                    return RewriteExpresionAsignacion((ExpresionAsignacionBound) nodo);
                case BoundTipoNodo.EXPRESION_UNARIA:
                    return RewriteExpresionUnaria((ExpresionUnariaBound) nodo);
                case BoundTipoNodo.EXPRESION_BINARIA:
                    return RewriteExpresionBinaria((ExpresionBinariaBound) nodo);
                default:
                    throw new Exception($"Nodo inesperado {nodo.TipoNodo}");
            }
        }

        protected virtual ExpresionBound RewriteExpresionError(ExpresionErrorBound nodo)
        {
            return nodo;
        }

        protected virtual ExpresionBound RewriteExpresionLiteral(ExpresionLiteralBound nodo)
        {
            return nodo;
        }

        protected virtual ExpresionBound RewriteExpresionVariable(ExpresionVariableBound nodo)
        {
            return nodo;
        }

        protected virtual ExpresionBound RewriteExpresionAsignacion(ExpresionAsignacionBound nodo)
        {
            var expresion = RewriteExpresion(nodo.Expresion);
            if (expresion == nodo.Expresion)
                return nodo;

            return new ExpresionAsignacionBound(nodo.Variable, expresion);
        }

        protected virtual ExpresionBound RewriteExpresionUnaria(ExpresionUnariaBound nodo)
        {
            var operando = RewriteExpresion(nodo.Operando);
            if (operando == nodo.Operando)
                return nodo;

            return new ExpresionUnariaBound(nodo.Operador, operando);
        }

        protected virtual ExpresionBound RewriteExpresionBinaria(ExpresionBinariaBound nodo)
        {
            var izq = RewriteExpresion(nodo.Izq);
            var der = RewriteExpresion(nodo.Der);
            if(izq == nodo.Izq && der == nodo.Der)
                return nodo;

            return new ExpresionBinariaBound(izq, nodo.Operador,der);
        }

        public virtual BoundDeclaracion RewriteDeclaracion(BoundDeclaracion nodo)
        {
            switch (nodo.TipoNodo)
            {
                case BoundTipoNodo.BLOQUE_DECLARACION:
                    return RewriteBloqueDeclaracion((BoundBloqueDeclaracion)nodo);
                case BoundTipoNodo.VARIABLE_DECLARACION:
                    return RewriteVariableDeclaracion((BoundDeclaracionVariable)nodo);
                case BoundTipoNodo.IF_DECLARACION:
                    return RewriteDeclaracionIf((BoundDeclaracionIf)nodo);
                case BoundTipoNodo.WHILE_DECLRACION:
                    return RewriteDeclaracionWhile((BoundDeclaracionWhile)nodo);
                case BoundTipoNodo.FOR_DECLRARACION:
                    return RewriteDeclaracionFor((BoundDeclaracionFor)nodo);
                case BoundTipoNodo.LABEL_DECLRARACION:
                    return RewriteDeclaracionLabel((BoundDeclaracionLabel)nodo);
                case BoundTipoNodo.GOTO_DECLARACION:
                    return RewriteDeclaracionGoTo((BoundDeclaracionGotTo)nodo);
                case BoundTipoNodo.GOTO_CONDICIONAL_DECLARACION:
                    return RewriteDeclaracionGoToCondicional((BoundDeclaracionCondicionalGoto)nodo);
                case BoundTipoNodo.EXPRESION_DECLARACION:
                    return RewriteExpresionDeclaracion((ExpresionDeclaracionBound)nodo);
                default:
                    throw new Exception($"Nodo inesperado {nodo.TipoNodo}");
            }
        }

        protected virtual BoundDeclaracion RewriteBloqueDeclaracion(BoundBloqueDeclaracion nodo)
        {
            ImmutableArray<BoundDeclaracion>.Builder builder = null;
            for(var i = 0; i < nodo.Declaraciones.Length; i++)
            {
                var delcracionAnterior = nodo.Declaraciones[i];
                var nuevaDelcracion = RewriteDeclaracion(delcracionAnterior);
                if (nuevaDelcracion != delcracionAnterior)
                {
                    if(builder == null)
                    {   
                        builder = ImmutableArray.CreateBuilder<BoundDeclaracion>(nodo.Declaraciones.Length);
                        for (var x = 0; x < i; x++)
                        {
                            builder.Add(nodo.Declaraciones[x]);
                        }
                    }
                }
                if(builder != null)
                    builder.Add(nuevaDelcracion);
                    
            }
            if (builder == null)
                return nodo;

            return new BoundBloqueDeclaracion(builder.MoveToImmutable());
        }

        protected virtual BoundDeclaracion RewriteVariableDeclaracion(BoundDeclaracionVariable nodo)
        {
            var inicializador = RewriteExpresion(nodo.Inicializador);
            if (inicializador == nodo.Inicializador)
                return nodo;

            return new BoundDeclaracionVariable(nodo.Variable, inicializador);
        }

        protected virtual BoundDeclaracion RewriteDeclaracionIf(BoundDeclaracionIf nodo)
        {
            var condicion = RewriteExpresion(nodo.Condicion);
            var thenDeclaracion = RewriteDeclaracion(nodo.ThenDeclaracion);
            var elseDeclaracion = nodo.ElseDeclaracion == null ? null : RewriteDeclaracion(nodo.ElseDeclaracion);
            if (condicion == nodo.Condicion && thenDeclaracion == nodo.ThenDeclaracion && elseDeclaracion == nodo.ElseDeclaracion)
                return nodo;

            return new BoundDeclaracionIf(condicion, thenDeclaracion, elseDeclaracion);
        }

        protected virtual BoundDeclaracion RewriteDeclaracionWhile(BoundDeclaracionWhile nodo)
        {
            var condicion = RewriteExpresion(nodo.Condicion);
            var cuerpo = RewriteDeclaracion(nodo.Cuerpo);
            if (condicion == nodo.Condicion && cuerpo == nodo.Cuerpo)
                return nodo;

            return new BoundDeclaracionWhile(condicion, cuerpo);
        }

        protected virtual BoundDeclaracion RewriteDeclaracionFor(BoundDeclaracionFor nodo)
        {
            var lowerBound = RewriteExpresion(nodo.LowerBound);
            var upperBound = RewriteExpresion(nodo.UpperBound);
            var cuerpo = RewriteDeclaracion(nodo.Cuerpo);
            if (lowerBound == nodo.LowerBound && upperBound == nodo.UpperBound && cuerpo == nodo.Cuerpo)
                return nodo;

            return new BoundDeclaracionFor(nodo.Variable, lowerBound, upperBound, cuerpo);

        }

        protected virtual BoundDeclaracion RewriteExpresionDeclaracion(ExpresionDeclaracionBound nodo)
        {
            var expresion = RewriteExpresion(nodo.Expresion);
            if (expresion == nodo.Expresion)
                return nodo;

            return new ExpresionDeclaracionBound(expresion);
        }

        protected virtual BoundDeclaracion RewriteDeclaracionLabel(BoundDeclaracionLabel nodo)
        {
            return nodo;
        }

        protected virtual BoundDeclaracion RewriteDeclaracionGoTo(BoundDeclaracionGotTo nodo)
        {
            return nodo;
        }

        protected virtual BoundDeclaracion RewriteDeclaracionGoToCondicional(BoundDeclaracionCondicionalGoto nodo)
        {
            var condicion = RewriteExpresion(nodo.Condicion);
            if (condicion == nodo.Condicion)
                return nodo;

            return new BoundDeclaracionCondicionalGoto(nodo.Label, condicion, nodo.JumpIfTrue);
        }
    }
}
