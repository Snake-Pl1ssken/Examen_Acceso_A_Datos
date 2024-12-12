using Microsoft.Win32;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.IO.Pipes;
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
            dialog.Filter = "Fichero de coche en modo binario (*.carb)|*.carb";
            dialog.FilterIndex = 0;

            if(dialog.ShowDialog().GetValueOrDefault())
            {
                // CODIGO DE CARGA
                FileStream fichero = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read);
                byte[] bytes;

                int CO;
                int VM;
                float TC;
                int PO;
                bool ELEC;

                bytes = new byte[sizeof(int)];
                fichero.Read(bytes, 0, bytes.Length);
                CO = BitConverter.ToInt32(bytes);
                Color.SelectedIndex = CO;

                bytes = new byte[sizeof(int)];
                fichero.Read(bytes, 0, bytes.Length);
                VM = BitConverter.ToInt32(bytes);
                VelocidadMaxima.Text = VM.ToString();

                bytes = new byte[sizeof(float)];
                fichero.Read(bytes, 0, bytes.Length);
                TC = BitConverter.ToSingle(bytes);
                TiempoCeroCien.Text = TC.ToString();

                bytes = new byte[sizeof(int)];
                fichero.Read(bytes, 0, bytes.Length);
                PO = BitConverter.ToInt32(bytes);
                Potencia.Text = PO.ToString();

                bytes = new byte[sizeof(bool)];
                fichero.Read(bytes, 0, bytes.Length);
                ELEC = BitConverter.ToBoolean(bytes);
                Electrico.IsChecked = ELEC;
                
                fichero.Close();
                WriteOutput("Cargado nuevo coche");
            }

        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.AddExtension = true;
            dialog.DefaultExt = "*.carb";
            dialog.Filter = "Fichero de coche en modo binario (*.carb)|*.carb";
            dialog.FilterIndex = 0;

            if (dialog.ShowDialog().GetValueOrDefault())
            {
                // CODIGO DE GUARDADO
                FileStream fichero = new FileStream(dialog.FileName, FileMode.Create, FileAccess.Write);

                byte[] bytes;

                int CO = Color.SelectedIndex;
                int VM = Int32.Parse(VelocidadMaxima.Text, CultureInfo.InvariantCulture);
                float TC = Single.Parse(TiempoCeroCien.Text, CultureInfo.InvariantCulture);
                int PO = Int32.Parse(Potencia.Text, CultureInfo.InvariantCulture);
                bool ELEC = Electrico.IsChecked.GetValueOrDefault();

                bytes = BitConverter.GetBytes(CO);
                fichero.Write(bytes);

                bytes = BitConverter.GetBytes(VM);
                fichero.Write(bytes);

                bytes = BitConverter.GetBytes(TC);
                fichero.Write(bytes);

                bytes = BitConverter.GetBytes(PO);
                fichero.Write(bytes);

                bytes = BitConverter.GetBytes(ELEC);
                fichero.Write(bytes);

                fichero.Close();

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