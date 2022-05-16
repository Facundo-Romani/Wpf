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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Configuration;

namespace ConexionGestionPedidos
{
    /// <summary>
    /// Lógica de interacción para Actualiza.xaml
    /// </summary>
    public partial class Actualiza : Window
    {
        SqlConnection conexion;

        private int z ; // Variable para obtener el Id del cleinte en ventana actualizar

        public Actualiza(int elId)
        {
            InitializeComponent();

            z = elId;

            string miConexion = ConfigurationManager.ConnectionStrings["ConexionGestionPedidos.Properties.Settings.GestionPedidosConnectionString"].ConnectionString;

            conexion = new SqlConnection(miConexion);

        }


        // Btn Actualizar Ventana Actualizar.
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string insertar = "UPDATE CLIENTE SET nombre=@nombre WHERE Id = " + z;

            SqlCommand miSqlCommand = new SqlCommand(insertar, conexion);

            conexion.Open();

            miSqlCommand.Parameters.AddWithValue("@nombre", cuadroAct.Text);

            miSqlCommand.ExecuteNonQuery();

            conexion.Close();

        }
    }
}
