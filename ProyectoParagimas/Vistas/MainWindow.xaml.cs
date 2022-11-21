using Microsoft.Win32;
using ProyectoParadigmas.Clases;
using ProyectoParadigmas.Clases.Simbolos;
using ProyectoParadigmas.Clases.Sintax;
using ProyectoParadigmas.Clases.Texto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Color = System.Windows.Media.Color;

namespace ProyectoParadigmas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private String content = "";
        private String textoAnterior = "";
        private Dictionary<SimboloVariable, object> variables = new Dictionary<SimboloVariable, object>();
        Compilacion previo = null;
        public MainWindow()
        {
            InitializeComponent();
            Rtb_OutPut.Document.Blocks.Clear();
        }

        /*Sirve para conocer cuando el texto cambia, tambien determina el numero de lineas en el ricTextBox*/
        private void Rtb_textChanged(object sender, TextChangedEventArgs e)
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

        private void Rtb_OutPut_textChanged(object sender, TextChangedEventArgs e)
        {
            
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
            textoAnterior = "";
            if (!diagnosticos.Any())
            {
                previo = compilacion;
                if (!accion)
                {
                   AgreagarDiagnosticos("Compilado sin errores ", false);
                   Trace.WriteLine($"Compilado sin Diagnosticos");
                }
                else
                {
                    AgreagarDiagnosticos($"{resultado.Valor}", false);
                    Trace.WriteLine(resultado.Valor);
                }
                    
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
                    int numLinea;
                    if (indiceLinea == 0)
                         numLinea = indiceLinea+1;
                    else
                        numLinea = indiceLinea;
                    var caracter = diagnostico.TextSpan.Inicio - texto.Lineas[indiceLinea].Inicio + 1;


                    Trace.Write($"(Linea {numLinea}, Columna {caracter}): ");
                    Trace.WriteLine(diagnostico);
                    AgreagarDiagnosticos($"(Linea {numLinea}, Columna {caracter}): {diagnostico}", true);
                    var spanPrefijo = TextoSpan.FromBounds(linea.Inicio, diagnostico.TextSpan.Inicio);
                    var sufijoSpan = TextoSpan.FromBounds(diagnostico.TextSpan.Fin, linea.Final);

                    var prefijo = arbolSintax.Texto.ToString(spanPrefijo);
                    var error = arbolSintax.Texto.ToString(diagnostico.TextSpan);
                    var sufijo = arbolSintax.Texto.ToString(sufijoSpan);
                    Trace.Write(prefijo);
                    Trace.Write($">{error}<");
                    Trace.Write(sufijo);
                    AgreagarDiagnosticos($"{prefijo} >{error}< {sufijo}.", true);
                    Trace.WriteLine("");
                }
                Trace.WriteLine("**************************************************");
            }
        }

        private void AgreagarDiagnosticos(string texto, bool errores)
        {

            //reinicio el rtb para limpiarlo y que no me duplique errores
            Rtb_OutPut.Document.Blocks.Clear();
            Rtb_OutPut.Document.LineHeight = 1;
            Brush brush;
            if (errores)
                brush = new SolidColorBrush(Color.FromRgb(255, 45, 0));
            else
                brush = new SolidColorBrush(Color.FromRgb(255, 86, 156));

            Rtb_OutPut.Foreground = brush;


            if (textoAnterior.Length > 0)
            {
                textoAnterior = String.Concat(textoAnterior, Environment.NewLine, texto);
                Rtb_OutPut.AppendText(textoAnterior);
            }
            else
            {
                textoAnterior = String.Concat(textoAnterior, texto);
                Rtb_OutPut.AppendText(textoAnterior);
            }
               
        }

        /*Sirve para determinar la linea actual de acuerdo a la posicon del caret en el richTextBox*/
        private void Rtb_SelectionChanged(object sender, RoutedEventArgs e)
        {
            lbl_line.Content = "";
            RichTextBox rich = (RichTextBox)sender;
            lbl_line.Content = "Linea: " + (RichPostion(rich.Document.ContentStart, rich.CaretPosition).Item1 + 1);
            lbl_col.Content = "Columna: " + RichPostion(rich.Document.ContentStart, rich.CaretPosition).Item2;
        }

        private void Rtb_OutPut_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}