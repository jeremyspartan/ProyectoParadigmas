using ProyectoParagimas.Clases.Sintax;
using System;
using System.Collections.Generic;

namespace ProyectoParagimas.Clases.Binding
{
    internal class Binder
    {
        private List<String> Errores = new List<String>();  

        public IEnumerable<String> errores => Errores;

        public ExpresionBound ExpresionBind(ExpresionSintax sintax)
        {
            switch(sintax.tipo)
            {
                case TiposSintax.EXPRESION_LITERAL:
                    return BindExpresionLiteral((ExpresionLiteral) sintax);

                case TiposSintax.EXPRESION_UNARIA:
                    return BindExpresionUnaria((ExpresionUnaria) sintax);

                case TiposSintax.EXPRESION_BINARIA:
                    return BindExpresionBinaria((ExpresionBinaria) sintax);
               
                case TiposSintax.EXPRESION_PARENTESIS:
                    return ExpresionBind(((ParentesisSintax)sintax).Expresion);

                default:
                    throw new Exception($"sintaxis inesperada {sintax.tipo}");
            }
        }

        private ExpresionBound BindExpresionLiteral(ExpresionLiteral sintax)
        {
            var valor = sintax.Valor  ?? 0;
            return new ExpresionLiteralBound(valor);
        }

        private ExpresionBound BindExpresionUnaria(ExpresionUnaria sintax)
        {
            var operandoBound = ExpresionBind(sintax.Operando);
            var operadorBound = BoundOperadorUnario.Bind(sintax.Operador.tipo, operandoBound.Type);
            if(operadorBound == null)
            {
                Errores.Add($"Operador unario '{sintax.Operador.texto}' no está definido para el tipo {operandoBound.Type}.");
                return operandoBound;
            }
            return new ExpresionUnariaBound(operadorBound , operandoBound);
        }

        private ExpresionBound BindExpresionBinaria(ExpresionBinaria sintax)
        {
            var izqBound = ExpresionBind(sintax.Izq); 
            var derBound = ExpresionBind(sintax.Der);
            var operadorBound = BoundOperadorBinario.Bind(sintax.Operador.tipo,izqBound.Type, derBound.Type);
            if (operadorBound == null)
            {
                Errores.Add($"Operador binario '{sintax.Operador.texto}' no está definido para los tipos {izqBound.Type} y {derBound.Type}.");
                return izqBound;
            }
            return new ExpresionBinariaBound(izqBound, operadorBound, derBound);
        }
       
    }
}
