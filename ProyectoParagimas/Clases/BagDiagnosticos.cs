using ProyectoParadigmas.Clases.Simbolos;
using ProyectoParadigmas.Clases.Sintax;
using ProyectoParadigmas.Clases.Texto;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ProyectoParadigmas.Clases
{
    internal class BagDiagnosticos : IEnumerable<Diagnostico>
    {
        private readonly List<Diagnostico> _diagnosticos = new List<Diagnostico>();
        public void Reporte(TextoSpan textSpan, string mensaje)
        {
            var diagnostico = new Diagnostico(textSpan, mensaje);
            _diagnosticos.Add(diagnostico);
        }

        public IEnumerator<Diagnostico> GetEnumerator() => _diagnosticos.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void AddRange(BagDiagnosticos diganosticos)
        {
            _diagnosticos.AddRange(diganosticos._diagnosticos);
        }

        public void ReportarNumeroInvalido(TextoSpan textSpan, string texto, TipoSimbolo type)
        {
            var mensaje = $"El numero {texto} no es TipoNodo {type} valido.";
            Reporte(textSpan, mensaje);
        }

        public void CaracterIncorrecto(int pos, char caracter)
        {
            var mensaje = $"Caracter incorrecto: '{caracter}'";

            Reporte(new TextoSpan(pos, 1), mensaje);
        }

        public void ReportarTokenInesperado(TextoSpan textSpan, TiposSintax tipoActual, TiposSintax tipoEsperado)
        {
            var mensaje = $"Token inesperado: <{tipoActual}>, se esperaba {tipoEsperado}.";
            Reporte(textSpan, mensaje);
        }

        public void ReportarOperadorUnarioIndefinido(TextoSpan textSpan, string textoOperador, TipoSimbolo tipoOperador)
        {
            var mensaje = $"Token operador unario '{textoOperador}' no está definido para el TipoNodo {tipoOperador}.";
            Reporte(textSpan, mensaje);
        }

        internal void ReportarOperadorBinarioIndefinido(TextoSpan textSpan, string textoOperador, TipoSimbolo izqTipo, TipoSimbolo derTipo)
        {
            var mensaje = $"Token operador binario '{textoOperador}' no está definido para los tipos {izqTipo} y {derTipo}.";
            Reporte(textSpan, mensaje);
        }

        public void ReportarNombreIndefinido(TextoSpan textSpan, string nombre)
        {
            var mensaje = $"La variable '{nombre}' no existe";
            Reporte(textSpan, mensaje);
        }

        public void ReportarNoSePuedeConvertir(TextoSpan textSpan, TipoSimbolo fromTipo, TipoSimbolo toTipo)
        {
            var mensaje = $"No se puede convertir el tipo '{fromTipo}' a tipo '{toTipo}'.";
            Reporte(textSpan, mensaje);
        }

        public void ReportarVariableYaDeclarada(TextoSpan textSpan, string nombre)
        {
            var mensaje = $"La variable '{nombre}' ya fue declarada.";
            Reporte(textSpan, mensaje);
        }

        public void ReportarNoSePuedeAsignar(TextoSpan textSpan, string nombre)
        {
            var mensaje = $"La variable '{nombre}' es de solo lectura y no puede ser asignada.";
            Reporte(textSpan, mensaje);
        }

        public void ReportartringSinTerminar(TextoSpan textSpan)
        {
            var mensaje = "String sin terminar.";
            Reporte(textSpan, mensaje);
        }
    }
}
