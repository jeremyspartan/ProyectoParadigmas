using ProyectoParadigmas.Clases.Binding;
using ProyectoParadigmas.Clases.Simbolos;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoParadigmas.Clases.Lowering
{
    internal class Lowerer : BoundArbolRewriter
    {
        private int _labelContandor;

        private Lowerer()
        {

        }

        private BoundLabel GenerarLabel()
        {
            var nombre = $"Label {++_labelContandor}";
            return new BoundLabel(nombre);

        }

        public static BoundBloqueDeclaracion Lower(BoundDeclaracion declaracion)
        {
            var lowerer = new Lowerer();
            var resultado = lowerer.RewriteDeclaracion(declaracion);
            return Flatten(resultado);
        }

        private static BoundBloqueDeclaracion Flatten(BoundDeclaracion declaracion)
        {
            var builder = ImmutableArray.CreateBuilder<BoundDeclaracion>();
            var stack = new Stack<BoundDeclaracion>();
            stack.Push(declaracion);

            while (stack.Count > 0)
            {
                var actual = stack.Pop();

                if (actual is BoundBloqueDeclaracion bloque)
                {
                    foreach (var s in bloque.Declaraciones.Reverse())
                        stack.Push(s);
                }
                else
                    builder.Add(actual);
            }

            return new BoundBloqueDeclaracion(builder.ToImmutable());
        }

        protected override BoundDeclaracion RewriteDeclaracionIf(BoundDeclaracionIf nodo)
        {
            if (nodo.ElseDeclaracion == null)
            {
                /* queremos pasar de 
                * if <condicion> 
                *       <then>
                * 
                * a
                * 
                * gotoFalse <condicon> end
                *   <the>  
                * end:*/
                var endLabel = GenerarLabel();
                var gotoFalse = new BoundDeclaracionCondicionalGoto(endLabel, nodo.Condicion, false);
                var endLabelDelcaracion = new BoundDeclaracionLabel(endLabel);
                var resultado = new BoundBloqueDeclaracion(ImmutableArray.Create<BoundDeclaracion>(gotoFalse, nodo.ThenDeclaracion, endLabelDelcaracion));
                return RewriteDeclaracion(resultado);
            }
            else
            {
                /* 
                * y para las condicones con else de 
                * 
                * if <condicon> 
                *      <then>
                * else
                *      <else>
                *      
                * a
                * 
                * gotoFalse <condicion> else
                *      <then>
                * goto end
                * else:
                *      <else>
                * end:
                */
                var elseLabel = GenerarLabel();
                var endLabel = GenerarLabel();

                var gotoFalse = new BoundDeclaracionCondicionalGoto(elseLabel, nodo.Condicion, false);
                var gotoEndlDelcaracion = new BoundDeclaracionGotTo(endLabel);
                var elseLabelDelcaracion = new BoundDeclaracionLabel(elseLabel);
                var endLabelDeclaracion = new BoundDeclaracionLabel(endLabel);
                var resultado = new BoundBloqueDeclaracion(ImmutableArray.Create<BoundDeclaracion>(
                    gotoFalse,
                    nodo.ThenDeclaracion,
                    gotoEndlDelcaracion,
                    elseLabelDelcaracion,
                    nodo.ElseDeclaracion,
                    endLabelDeclaracion
                 ));
                return RewriteDeclaracion(resultado);
            }
        }

        protected override BoundDeclaracion RewriteDeclaracionWhile(BoundDeclaracionWhile nodo)
        {
            /*
             * while <condicion> 
             *      <cuerpo>
             *      
             * a
             * 
             * goto chek
             * continue:
             * <cuerpo>
             * check:
             *      gotoTrue <condicion> continue
             * 
             * end:
             */
            var continueLabel = GenerarLabel();
            var chekLabel = GenerarLabel();
            var endLabel = GenerarLabel();

            var gotoCheck = new BoundDeclaracionGotTo(chekLabel);
            var continueLabelDeclaracion = new BoundDeclaracionLabel(continueLabel);
            var chekLabelDeclaracion = new BoundDeclaracionLabel(chekLabel);
            var gotoTrue = new BoundDeclaracionCondicionalGoto(continueLabel, nodo.Condicion);
            var endLabelDeclaracion = new BoundDeclaracionLabel(endLabel);

            var resultado = new BoundBloqueDeclaracion(ImmutableArray.Create<BoundDeclaracion>(
                gotoCheck,
                continueLabelDeclaracion,
                nodo.Cuerpo,
                chekLabelDeclaracion,
                gotoTrue,
                endLabelDeclaracion
             ));
            return RewriteDeclaracion(resultado);
        }

        protected override BoundDeclaracion RewriteDeclaracionFor(BoundDeclaracionFor nodo)
        {
            /* queremos pasar de 
             * for <var> = <lower> to <upper>
             *       <cuerpo>
             * 
             * a
             * 
             * {
             *      var <var> = <lower>
             *      while(<var> <= <upper>)
             *      {
             *          <cuerpo>
             *          <var> = <var> +1
             *      }
             * }
             */
            var declaracionVariable = new BoundDeclaracionVariable(nodo.Variable, nodo.LowerBound);
            var expresionVariable = new ExpresionVariableBound(nodo.Variable);
            var simboloUppper = new SimboloVariable("upperBound", true, TipoSimbolo.Int);
            var declaracionUpper = new BoundDeclaracionVariable(simboloUppper, nodo.UpperBound);
            var condicion = new ExpresionBinariaBound(expresionVariable,
            BoundOperadorBinario.Bind(Sintax.TiposSintax.MENOR_IGUAL, TipoSimbolo.Int, TipoSimbolo.Int),
                new ExpresionVariableBound(simboloUppper)
            );

            var incremento = new ExpresionDeclaracionBound(new ExpresionAsignacionBound(nodo.Variable,
                new ExpresionBinariaBound(expresionVariable,
                BoundOperadorBinario.Bind(Sintax.TiposSintax.SUMA, TipoSimbolo.Int, TipoSimbolo.Int),
                new ExpresionLiteralBound(1))
                )
            );

            var cuerpoWhile = new BoundBloqueDeclaracion(ImmutableArray.Create(nodo.Cuerpo, incremento));
            var declaracionWhile = new BoundDeclaracionWhile(condicion, cuerpoWhile);
            var resultado = new BoundBloqueDeclaracion(ImmutableArray.Create<BoundDeclaracion>(declaracionVariable,declaracionUpper, declaracionWhile));
            return RewriteDeclaracion(resultado);
        }
    }
}
