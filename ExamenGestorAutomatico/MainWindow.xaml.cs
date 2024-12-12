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
using System.Text.Json;
using System.Drawing;

namespace ActividadSeguimiento
{
    // DECLARACIÓN DE LA CLASE DEL OBJETO QUE SE SERIALIZARÁ/DESERIALIZARÁ

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public class Coche
        {
            public int COLOR { get; set; }
            public string VELOCIDADMAXIMA { get; set; }
            public string TIEMPOCEROACIEN { get; set; }
            public string POTENCIA { get; set; }
            public bool ELECTRICO { get; set; }
        }

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
            dialog.Filter = "Fichero de coche en modo JSON (*.carj)|*.carj";
            dialog.FilterIndex = 0;

            if(dialog.ShowDialog().GetValueOrDefault())
            {
                // CODIGO DE CARGA

                //ojo que en la carga recuerda que es distinto

                //Personaje
                Coche coche = new Coche();

                //opciones de serializacion
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true //lo tenia en false
                };

                string JSONCargar = File.ReadAllText(dialog.FileName);
                
                coche = JsonSerializer.Deserialize<Coche>(JSONCargar, options);

                Color.SelectedIndex = coche.COLOR;
                VelocidadMaxima.Text = coche.VELOCIDADMAXIMA;
                TiempoCeroCien.Text = coche.TIEMPOCEROACIEN;
                Potencia.Text = coche.POTENCIA;
                Electrico.IsChecked = coche.ELECTRICO;

                WriteOutput("Cargado nuevo coche");
            }

        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.AddExtension = true;
            dialog.DefaultExt = "*.carj";
            dialog.Filter = "Fichero de coche en modo JSON (*.carj)|*.carj";
            dialog.FilterIndex = 0;

            if (dialog.ShowDialog().GetValueOrDefault())
            {
                // CODIGO DE GUARDADO

                //Personaje
                //que contiene el personaje

                Coche coche = new Coche();
                coche.COLOR = Color.SelectedIndex;
                coche.VELOCIDADMAXIMA = VelocidadMaxima.Text;
                coche.TIEMPOCEROACIEN = TiempoCeroCien.Text;
                coche.POTENCIA = Potencia.Text;
                coche.ELECTRICO = Electrico.IsChecked.GetValueOrDefault();

                //opciones de serializacion Creo que era si el writeindented pero la documentacion no dice lo mismo
                //var options = new JsonSerializerOptions
                //{
                //    WriteIndented = false
                //};

                var options = new JsonSerializerOptions 
                { 
                    WriteIndented = true 
                };

                string JSONGuardar = JsonSerializer.Serialize(coche, options);
                File.WriteAllText(dialog.FileName, JSONGuardar);


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