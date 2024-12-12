using Microsoft.Win32;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace ActividadSeguimiento
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            Color.SelectedIndex = 0;
            VelocidadMaxima.Text = "340";
            TiempoCeroCien.Text = "2.71";
            Potencia.Text = "3000";
            Electrico.IsChecked = false;

            WriteOutput("Iniciado nuevo coche");

        }

        public static void WriteOutput(string text)
        {
            MainWindow window = ((MainWindow)App.Current.MainWindow);
            window.OutputText.Text += text + "\n";

            window.OutputText.ScrollToEnd();

        }

        private void Nuevo_Click(object sender, RoutedEventArgs e)
        {
            Color.SelectedIndex = 0;
            VelocidadMaxima.Text = "340";
            TiempoCeroCien.Text = "2.71";
            Potencia.Text = "3000";
            Electrico.IsChecked = false;

            WriteOutput("Iniciado nuevo coche");

        }

        private void Abrir_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Fichero de coche en modo texto (*.cart)|*.cart";
            dialog.FilterIndex = 0;

            if(dialog.ShowDialog().GetValueOrDefault())
            {
                // CODIGO DE CARGA
                StreamReader reader = new StreamReader(dialog.FileName);

                Color.SelectedIndex = Int32.Parse(reader.ReadLine());
                VelocidadMaxima.Text = reader.ReadLine();
                TiempoCeroCien.Text = reader.ReadLine();
                Potencia.Text = reader.ReadLine();
                Electrico.IsChecked = bool.Parse(reader.ReadLine());

                reader.Close();
                WriteOutput("Cargado nuevo coche");
            }

        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.AddExtension = true;
            dialog.DefaultExt = "*.cart";
            dialog.Filter = "Fichero de coche en modo texto (*.cart)|*.cart";
            dialog.FilterIndex = 0;

            if (dialog.ShowDialog().GetValueOrDefault())
            {
                // CODIGO DE GUARDADO

                StreamWriter writer = new StreamWriter(dialog.FileName, false, Encoding.UTF8);

                writer.WriteLine(Color.SelectedIndex);
                writer.WriteLine(VelocidadMaxima.Text);
                writer.WriteLine(TiempoCeroCien.Text);
                writer.WriteLine(Potencia.Text);
                writer.WriteLine(Electrico.IsChecked);

                writer.Close();

                WriteOutput("Guardado coche");
            }
        }

        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Color_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Imagen1.Visibility = Visibility.Hidden;
            Imagen2.Visibility = Visibility.Hidden;
            Imagen3.Visibility = Visibility.Hidden;
            if (Color.SelectedIndex == 0) { Imagen1.Visibility = Visibility.Visible; }
            else if (Color.SelectedIndex == 1) { Imagen2.Visibility = Visibility.Visible; }
            else // Color.SelectedIndex == 2
            { Imagen3.Visibility = Visibility.Visible; }
        }
    }
}