using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        /*Sirve para conocer cuando el texto cambia, tambien determina el numero de lineas en el ricTextBox*/
        private void rtb_textChanged(object sender, TextChangedEventArgs e)
        {
            lbl_lines.Content = "";
            RichTextBox rich = (RichTextBox)sender;
            lbl_lines.Content = "lineas: " + RichPostion(rich.Document.ContentStart, rich.Document.ContentEnd,0);
        }



        private void HolaMundo(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("hola mundo");
        }

        /*Sirve para determinar la linea actual de acuerdo a la posicon del caret en el richTextBox*/
        private void rtb_SelectionChanged(object sender, RoutedEventArgs e)
        {
            lbl_line.Content = "";
            RichTextBox rich = (RichTextBox)sender;
            lbl_line.Content = "Linea actual: " + RichPostion(rich.Document.ContentStart, rich.CaretPosition,1);
        }

        private static int RichPostion(TextPointer startPoint,TextPointer endPoint,int index)
        {
            TextRange textRange1 = new TextRange(
                //puntero al inicio del texto
                startPoint,
                //puntero al final de la posicion actual de caret
                endPoint
            );
            int flag = textRange1.Text.Length;
            int x = 0, y = index;
            while (x < flag)
            {
                if (textRange1.Text.ElementAt(x).Equals('\n'))
                {
                    y++;
                }
                x++;
            }
            return y;
        }
    }
}