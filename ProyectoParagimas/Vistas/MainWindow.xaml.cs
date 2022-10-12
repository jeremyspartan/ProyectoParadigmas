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
            lbl_line.Content = "Linea: " + (RichPostion(rich.Document.ContentStart, rich.CaretPosition).Item1+1);
            lbl_col.Content = "Columna: " + RichPostion(rich.Document.ContentStart, rich.CaretPosition).Item2;
        }

        private static (int,int) RichPostion(TextPointer startPoint,TextPointer endPoint)
        {
            //seleccionar el texto solo de una linea
            TextRange textRange = new TextRange(
                //puntero al inicio del texto
                startPoint,
                //puntero al final de la posicion actual de caret
                endPoint
            );
            int flag = textRange.Text.Length;
            int x = 0, y = 0, col=1;
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
            return (y,col);
        }

        private void btn_compilar_click(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine(content);
        }
    }
}