using ProyectoParadigmas.Clases;
using ProyectoParadigmas.Clases.Sintax;
using ProyectoParadigmas.Clases.Texto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace ProyectoParadigmas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private String content = "";
        private Dictionary<SimboloVariable, object> variables = new Dictionary<SimboloVariable, object>();
        Compilacion previo = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        /*Sirve para conocer cuando el texto cambia, tambien determina el numero de lineas en el ricTextBox*/
        private void rtb_textChanged(object sender, TextChangedEventArgs e)
        {
            lbl_lines.Content = "";
            lbl_line.Content = "";
            RichTextBox rich = (RichTextBox)sender;
            lbl_lines.Content = "lineas: " + (RichPostion(rich.Document.ContentStart, rich.Document.ContentEnd).Item1);
            lbl_line.Content = "Linea: " + (RichPostion(rich.Document.ContentStart, rich.CaretPosition).Item1 + 1);
            lbl_col.Content = "Columna: " + RichPostion(rich.Document.ContentStart, rich.CaretPosition).Item2;
            TextRange textRange = new(
               //puntero al inicio del texto
               rich.Document.ContentStart,
            //puntero al final de la posicion actual de caret
                rich.Document.ContentEnd
           );
            content = textRange.Text;
        }

        private static (int, int) RichPostion(TextPointer startPoint, TextPointer endPoint)
        {
            //seleccionar el texto solo de una linea
            TextRange textRange = new(
                //puntero al inicio del texto
                startPoint,
                //puntero al final de la posicion actual de caret
                endPoint
            );
            int flag = textRange.Text.Length;
            int x = 0, y = 0, col = 1;
            while (x < flag)
            {
                if (textRange.Text.ElementAt(x).Equals('\n'))
                {
                    y++;
                    col = 0;
                }
                x++;
                col++;
            }
            return (y, col);
        }

        private void btn_compilar_click(object sender, RoutedEventArgs e)
        {
            Accion(false);

        }

        private void PrettyPrint(NodoSintax nodo, String indent = "", bool isLast = true)
        {
            var marker = isLast ? "└──" : "├──";
            Trace.Write(indent);
            Trace.Write(marker);
            Trace.Write(nodo.Tipo);
            if (nodo is Token t && t.Valor != null)
            {
                Trace.Write("");
                Trace.Write(t.Valor);
            }
            Trace.WriteLine("");
            indent += isLast ? "    " : "│   ";

            var lastChild = nodo.GetChildren().LastOrDefault();

            foreach (var child in nodo.GetChildren())
                PrettyPrint(child, indent, child == lastChild);
        }

        private void btn_ejecutar_Click(object sender, RoutedEventArgs e)
        {
            Accion(true);
        }

        private void Accion(bool accion)
        {
            Trace.WriteLine(content);
            var arbolSintax = ArbolSintax.Parse(content);
            var compilacion = previo == null ? new Compilacion(arbolSintax) : previo.ContinuarCon(arbolSintax);
            var resultado = compilacion.Evaluar(accion, variables);

            var diagnosticos = resultado.Diagnosticos;

            PrettyPrint(arbolSintax.Raiz);

            if (!diagnosticos.Any())
            {
                previo = compilacion;
                if (!accion)
                    Trace.WriteLine($"Compilado sin Diagnosticos");
                else
                    Trace.WriteLine(resultado.Valor);
            }
            else
            {
                var texto = arbolSintax.Texto;
                Trace.WriteLine($"Compilado con Diagnosticos");
                Trace.WriteLine("Lista de Diagnosticos:");
                Trace.WriteLine("**************************************************");
                foreach (var diagnostico in diagnosticos)
                {
                    var indiceLinea = texto.GetIndiceLinea(diagnostico.TextSpan.Inicio);
                    var linea = arbolSintax.Texto.Lineas[indiceLinea];
                    var numLinea = indiceLinea + 1;
                    var caracter = diagnostico.TextSpan.Inicio - texto.Lineas[indiceLinea].Inicio + 1;


                    Trace.Write($"(Linea {numLinea}, Columna {caracter}): ");
                    Trace.WriteLine(diagnostico);
                    var spanPrefijo = TextoSpan.FromBounds(linea.Inicio, diagnostico.TextSpan.Inicio);
                    var sufijoSpan = TextoSpan.FromBounds(diagnostico.TextSpan.Fin, linea.Final);

                    var prefijo = arbolSintax.Texto.ToString(spanPrefijo);
                    var error = arbolSintax.Texto.ToString(diagnostico.TextSpan);
                    var sufijo = arbolSintax.Texto.ToString(sufijoSpan);
                    Trace.Write(prefijo);
                    Trace.Write($">{error}<");
                    Trace.Write(sufijo);
                    Trace.WriteLine("");
                }
                Trace.WriteLine("**************************************************");
            }
        }
    }
}