using System;
using System.Collections.Generic;
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

namespace WpfApp4
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<Poblaciones> listaPob = new List<Poblaciones>();

            listaPob.Add(new Poblaciones() { Poblacion1 = "Madrid", Poblacion2 = "Barcelona", Temperatura1 = 25, Temperatura2 = 15 , DiferenciaTemp = 10 });
            listaPob.Add(new Poblaciones() { Poblacion1 = "Valencia", Poblacion2 = "Alicante", Temperatura1 = 30, Temperatura2 = 10, DiferenciaTemp = 20 });
            listaPob.Add(new Poblaciones() { Poblacion1 = "Málaga", Poblacion2 = "Bilbao", Temperatura1 = 25, Temperatura2 = 15, DiferenciaTemp = 10 });
            listaPob.Add(new Poblaciones() { Poblacion1 = "Sevilla", Poblacion2 = "Coruña", Temperatura1 = 5, Temperatura2 = 10, DiferenciaTemp = 5 });
            listaPoblaciones.ItemsSource = listaPob;
        }

        // Evento click.
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (listaPoblaciones.SelectedItem != null) 
            { 
                MessageBox.Show(((Poblaciones)(listaPoblaciones.SelectedItem)).Poblacion1 + " " +
                           ((Poblaciones)(listaPoblaciones.SelectedItem)).Temperatura1 + " ºC " +
                           ((Poblaciones)(listaPoblaciones.SelectedItem)).Poblacion2 + " " +
                           ((Poblaciones)(listaPoblaciones.SelectedItem)).Temperatura2 + " ºC ");
            } 
            else
            {
                MessageBox.Show("Debes seleccionar un elemento.");
            }
        }

        // El cuarto elemento da la acción.
        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (listaPoblaciones.SelectedItem != null)
            { 
                MessageBox.Show(((Poblaciones)(listaPoblaciones.SelectedItem)).Poblacion1 + " " +
                           ((Poblaciones)(listaPoblaciones.SelectedItem)).Temperatura1 + " ºC " +
                           ((Poblaciones)(listaPoblaciones.SelectedItem)).Poblacion2 + " " +
                           ((Poblaciones)(listaPoblaciones.SelectedItem)).Temperatura2 + " ºC ");
            }
            else
            {
                MessageBox.Show("Debes seleccionar un elemento.");
            }
        }
    }

    public class Poblaciones
    {
        public string Poblacion1 { get; set; }

        public int Temperatura1 { get; set; }

        public string Poblacion2 { get; set; }

        public int Temperatura2 { get; set; }

        public int DiferenciaTemp { get; set; }
    }
}
