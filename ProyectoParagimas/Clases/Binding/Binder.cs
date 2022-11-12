using ProyectoParadigmas.Clases.Sintax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace ProyectoParadigmas.Clases.Binding
{
    internal class Binder
    {
        private BagDiagnosticos _diagnosticos = new BagDiagnosticos();
        private BoundScope _scope;

        public Binder(BoundScope padre)
        {
            _scope = new BoundScope(padre);
        }

        public static BoundGlobalScope BindGlobalScope(BoundGlobalScope previo, UnidadCompilacionSintax sintax)
        {
            var padreScope = CrearPadreScope(previo);
            var binder = new Binder(padreScope);
            var expresion = binder.DeclaracionBind(sintax.Declaracion);
            var variables = binder._scope.GetVariablesDeclaradas();
            var diagnosticos = binder.Diagnosticos.ToImmutableArray();

            if (previo != null)
                diagnosticos = diagnosticos.InsertRange(0, previo.Diagnosticos);

            return new BoundGlobalScope(previo, diagnosticos, variables, expresion);
        }

        private static BoundScope CrearPadreScope(BoundGlobalScope previo)
        {
            var stack = new Stack<BoundGlobalScope>();
            while (previo != null)
            {
                stack.Push(previo);
                previo = previo.Anterior;
            }

            BoundScope padre = null;
            while (stack.Count > 0)
            {
                previo = stack.Pop();
                var scope = new BoundScope(padre);
                foreach (var v in previo.Variables)
                    scope.TryDeclare(v);

                padre = scope;
            }

            return padre;
        }

        public BagDiagnosticos Diagnosticos => _diagnosticos;

        private BoundDeclaracion DeclaracionBind(DeclaracionSintax sintax)
        {
            switch (sintax.Tipo)
            {
                case TiposSintax.BLOQUE_DECLARACION:
                    return BloqueDeclaracionBind((BloqueDeclaracionSintax)sintax);
                case TiposSintax.VARIABLE_DECLARACION:
                    return VariableDeclaracionBind((DeclaracionVariableSintax)sintax);
                case TiposSintax.IF_DECLARACION:
                    return IfDeclaracionBind((DeclaracionIfSintax)sintax);
                case TiposSintax.EXPRESION_DECLARACION:
                    return ExpresionDeclaracionBind((ExpresionSintaxDeclaracion)sintax);
                default:
                    throw new Exception($"sintaxis inesperada {sintax.Tipo}");
            }
        }

        private BoundDeclaracion BloqueDeclaracionBind(BloqueDeclaracionSintax sintax)
        {
            var declaraciones = ImmutableArray.CreateBuilder<BoundDeclaracion>();
            _scope = new BoundScope(_scope);
            foreach (var sintaxDeclaracion in sintax.Declaraciones)
            {
                var declaracion = DeclaracionBind(sintaxDeclaracion);
                declaraciones.Add(declaracion);
            }

            _scope = _scope.Padre;
            return new BoundBloqueDeclaracion(declaraciones.ToImmutableArray());
        }

        private BoundDeclaracion VariableDeclaracionBind(DeclaracionVariableSintax sintax)
        {
            var nombre = sintax.Identificador.Texto;
            var isReadOnly = sintax.PalabraReservada.Tipo == TiposSintax.LET_CLAVE;
            var inicializador = ExpresionBind(sintax.Inicializador);
            var variable = new SimboloVariable(nombre, isReadOnly, inicializador.Tipo);

            if (!_scope.TryDeclare(variable))
                _diagnosticos.ReportarVariableYaDeclarada(sintax.Identificador.Span, nombre);

            return new BoundDeclaracionVariable(variable, inicializador);
        }

        private BoundDeclaracion IfDeclaracionBind(DeclaracionIfSintax sintax)
        {
            var condicion = ExpresionBind(sintax.Condicion, typeof(bool));
            var thenDeclaracion = DeclaracionBind(sintax.ThenDeclaracion);
            var elseDeclaracion = sintax.ClausulaElse == null ? null : DeclaracionBind(sintax.ClausulaElse.ElseDeclaracion);
            return new BoundDeclaracionIf(condicion, thenDeclaracion, elseDeclaracion);
        }

        private BoundDeclaracion ExpresionDeclaracionBind(ExpresionSintaxDeclaracion sintax)
        {
            var expresion = ExpresionBind(sintax.Expresion);
            return new ExpresionDeclaracionBound(expresion);
        }

        private ExpresionBound ExpresionBind(ExpresionSintax sintax, Type tipoTarget)
        {
            var resultado = ExpresionBind(sintax);
            if (resultado.Tipo != tipoTarget)
                _diagnosticos.ReportarNoSePuedeConvertir(sintax.Span, resultado.Tipo, tipoTarget);
            return resultado;
        }

        private ExpresionBound ExpresionBind(ExpresionSintax sintax)
        {
            switch (sintax.Tipo)
            {
                case TiposSintax.EXPRESION_PARENTESIS:
                    return ExpresionParentesisBind((ParentesisSintax)sintax);

                case TiposSintax.EXPRESION_LITERAL:
                    return BindExpresionLiteral((ExpresionSintaxLiteral)sintax);

                case TiposSintax.EXPRESION_NOMBRE:
                    return ExpresionNombrenBind((ExpresionSintaxNombre)sintax);

                case TiposSintax.EXPRESION_ASIGNACION:
                    return ExpresionAsignacionBind((ExpresionSintaxAsignacion)sintax);

                case TiposSintax.EXPRESION_UNARIA:
                    return BindExpresionUnaria((ExpresionSintaxUnaria)sintax);

                case TiposSintax.EXPRESION_BINARIA:
                    return BindExpresionBinaria((ExpresionSintaxBinaria)sintax);

                default:
                    throw new Exception($"sintaxis inesperada {sintax.Tipo}");
            }
        }

        private ExpresionBound ExpresionParentesisBind(ParentesisSintax sintax)//permite binder las expresiones con parentesis
        {
            return ExpresionBind(sintax.Expresion);
        }

        private ExpresionBound BindExpresionLiteral(ExpresionSintaxLiteral sintax)
        {
            var valor = sintax.Valor ?? 0;
            return new ExpresionLiteralBound(valor);
        }

        private ExpresionBound ExpresionNombrenBind(ExpresionSintaxNombre sintax)
        {
            var nombre = sintax.TokenIdentificador.Texto;

            if (!_scope.TryLookUp(nombre, out var variable))
            {
                _diagnosticos.ReportarNombreIndefinido(sintax.TokenIdentificador.Span, nombre);
                return new ExpresionLiteralBound(0);
            }

            return new ExpresionVariableBound(variable);
        }

        private ExpresionBound ExpresionAsignacionBind(ExpresionSintaxAsignacion sintax)
        {
            var nombre = sintax.TokenIdientificador.Texto;
            var expresionBound = ExpresionBind(sintax.Expresion);

            if (!_scope.TryLookUp(nombre, out var variable))
            {
                _diagnosticos.ReportarNombreIndefinido(sintax.TokenIdientificador.Span, nombre);
                return expresionBound;
            }

            if (variable.IsReadOnly)
                _diagnosticos.ReportarNoSePuedeAsignar(sintax.TokenIgualdad.Span, nombre);

            if (expresionBound.Tipo != variable.Tipo)
            {
                _diagnosticos.ReportarNoSePuedeConvertir(sintax.Expresion.Span, expresionBound.Tipo, variable.Tipo);
                return expresionBound;
            }

            return new ExpresionAsignacionBound(variable, expresionBound);
        }

        private ExpresionBound BindExpresionUnaria(ExpresionSintaxUnaria sintax)
        {
            var operandoBound = ExpresionBind(sintax.Operando);
            var operadorBound = BoundOperadorUnario.Bind(sintax.OperadorToken.Tipo, operandoBound.Tipo);
            if (operadorBound == null)
            {
                _diagnosticos.ReportarOperadorUnarioIndefinido(sintax.OperadorToken.Span, sintax.OperadorToken.Texto, operandoBound.Tipo);
                return operandoBound;
            }
            return new ExpresionUnariaBound(operadorBound, operandoBound);
        }

        private ExpresionBound BindExpresionBinaria(ExpresionSintaxBinaria sintax)
        {
            var izqBound = ExpresionBind(sintax.Izq);
            var derBound = ExpresionBind(sintax.Der);
            var operadorBound = BoundOperadorBinario.Bind(sintax.OperadorToken.Tipo, izqBound.Tipo, derBound.Tipo);
            if (operadorBound == null)
            {
                _diagnosticos.ReportarOperadorBinarioIndefinido(sintax.OperadorToken.Span, sintax.OperadorToken.Texto, izqBound.Tipo, derBound.Tipo);
                return izqBound;
            }
            return new ExpresionBinariaBound(izqBound, operadorBound, derBound);
        }

    }
}
