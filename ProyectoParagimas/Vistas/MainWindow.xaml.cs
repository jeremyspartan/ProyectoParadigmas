using ProyectoParagimas.Clases;
using ProyectoParagimas.Clases.Binding;
using ProyectoParagimas.Clases.Sintax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProyectoParagimas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private String content = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        /*Sirve para conocer cuando el texto cambia, tambien determina el numero de lineas en el ricTextBox*/
        private void rtb_textChanged(object sender, TextChangedEventArgs e)
        {
            lbl_lines.Content = "";
            RichTextBox rich = (RichTextBox)sender;
            lbl_lines.Content = "lineas: " + (RichPostion(rich.Document.ContentStart, rich.Document.ContentEnd).Item1);
            TextRange textRange = new TextRange(
               //puntero al inicio del texto
               rich.Document.ContentStart,
            //puntero al final de la posicion actual de caret
                rich.Document.ContentEnd
           );
            content = textRange.Text;
        }

        /*Sirve para determinar la linea actual de acuerdo a la posicon del caret en el richTextBox*/
        private void rtb_SelectionChanged(object sender, RoutedEventArgs e)
        {
            lbl_line.Content = "";
            RichTextBox rich = (RichTextBox)sender;
            lbl_line.Content = "Linea: " + (RichPostion(rich.Document.ContentStart, rich.CaretPosition).Item1 + 1);
            lbl_col.Content = "Columna: " + RichPostion(rich.Document.ContentStart, rich.CaretPosition).Item2;
        }

        private static (int, int) RichPostion(TextPointer startPoint, TextPointer endPoint)
        {
            //seleccionar el texto solo de una linea
            TextRange textRange = new TextRange(
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

            Trace.WriteLine(content);
            var arbolSintax = ArbolSintax.Parse(content);
            var binder = new Binder();
            var expresionBound = binder.ExpresionBind(arbolSintax.raiz);

            var errores = arbolSintax.errores.Concat(binder.errores).ToArray();

            PrettyPrint(arbolSintax.raiz);

            if (errores.Any())
            {
                foreach(var error in errores)
                    Trace.WriteLine(error);
            }
            else
            {
                var evaluador = new Evaluador(expresionBound);
                var resultado = evaluador.Evaluar();
                Trace.WriteLine(resultado);
            }
            //while (true)
            //{
            //    var token = lexer.ElementoSiguiente();
            //    if (token.tipo == TipoSintax.Tipos.EOF)
            //        break;

                

                //Trace.Write("Tipo:" + token.tipo + "| Texto:" + token.texto);
                //if (token.valor != null)
                //{
                //    Trace.WriteLine("| Valor:" + token.valor);
                //}
                //Trace.WriteLine("");
            //}

        }

        private void PrettyPrint( NodoSintax nodo, String indent = "", bool isLast = true)
        {
            var marker = isLast ? "└──" : "├──";
            Trace.Write(indent);
            Trace.Write(marker);
            Trace.Write(nodo.tipo);
            if(nodo is Token t && t.valor != null )
            {
                Trace.Write("");
                Trace.Write(t.valor);
            }
            Trace.WriteLine("");
            indent += isLast ? "    " : "│   ";

            var lastChild = nodo.GetChildren().LastOrDefault();

            foreach (var child in nodo.GetChildren())
                PrettyPrint(child, indent, child==lastChild);
        }
    }
}