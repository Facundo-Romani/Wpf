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
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ConexionGestionPedidos
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        SqlConnection conexion;
        public MainWindow()
        {
            InitializeComponent();

            string miConexion = ConfigurationManager.ConnectionStrings["ConexionGestionPedidos.Properties.Settings.GestionPedidosConnectionString"].ConnectionString;
            
            conexion = new SqlConnection(miConexion);

            MuestraClientes();
        }

        // Método para mostrar datos de la Tabla.
        private void MuestraClientes ()
        {
            string consulta = "SELECT * FROM cliente";

            SqlDataAdapter miAdaptadorSql = new SqlDataAdapter(consulta , conexion);

            using (miAdaptadorSql)
            {
                DataTable clientesTabla = new DataTable();
                
                // Fill método para rellenar la Tabla.
                miAdaptadorSql.Fill(clientesTabla);

                listaClientes.DisplayMemberPath = "nombre";
                listaClientes.SelectedValuePath = "Id";
                listaClientes.ItemsSource = clientesTabla.DefaultView;
            }
        }

        // Método Ver pedidos en listBox.
        private void MuestraPedidos()
        {
            string consulta = "SELECT * FROM Pedido P INNER JOIN CLIENTE C ON  C.Id = P.cCliente" + " WHERE C.Id = @ClienteId";

            SqlCommand sqlComando = new SqlCommand(consulta, conexion);

            SqlDataAdapter miAdaptadorSql = new SqlDataAdapter(sqlComando);

            using (miAdaptadorSql)
            {
                sqlComando.Parameters.AddWithValue("@ClienteId" , listaClientes.SelectedValue);

                DataTable pedidos = new DataTable();

                // Fill método para rellenar la Tabla.
                miAdaptadorSql.Fill(pedidos);

                pedidosClientes.DisplayMemberPath = "fechaPedido";
                pedidosClientes.SelectedValuePath = "Id";
                pedidosClientes.ItemsSource = pedidos.DefaultView;
            }
        }

        private void listaClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MuestraPedidos();
        }
    }
}
 