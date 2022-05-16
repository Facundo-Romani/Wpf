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

            MuestraDetalles();
        }


        // Método para mostrar datos de la Tabla.
        private void MuestraClientes ()
        {
            try
            {
                string consulta = "SELECT * FROM cliente";

                SqlDataAdapter miAdaptadorSql = new SqlDataAdapter(consulta, conexion);

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
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }
            
        }


        // Método Ver pedidos en listBox.
        private void MuestraPedidos()
        {
            try
            {
                string consulta = "SELECT * FROM Pedido P INNER JOIN CLIENTE C ON  C.Id = P.cCliente" + " WHERE C.Id = @clienteId";

                SqlCommand sqlComando = new SqlCommand(consulta, conexion);

                SqlDataAdapter miAdaptadorSql = new SqlDataAdapter(sqlComando);

                using (miAdaptadorSql)
                {
                    sqlComando.Parameters.AddWithValue("@clienteId", listaClientes.SelectedValue);

                    DataTable pedidos = new DataTable();

                    // Fill método para rellenar la Tabla.
                    miAdaptadorSql.Fill(pedidos);

                    pedidosClientes.DisplayMemberPath = "fechaPedido";
                    pedidosClientes.SelectedValuePath = "Id";
                    pedidosClientes.ItemsSource = pedidos.DefaultView;
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }
               
        }


        // Método para ver detalles de pedidos.
        private void MuestraDetalles()
        {
            try
            {
                string consulta = "SELECT * , CONCAT (cCliente , ' ' , fechaPedido , ' ' , formaPago ) AS INFOCOMPLETA FROM PEDIDO ";

                SqlDataAdapter miAdaptadorSql = new SqlDataAdapter(consulta, conexion);

                using (miAdaptadorSql)
                {
                    DataTable pedidosDetalle = new DataTable();

                    // Fill método para rellenar la Tabla. 
                    miAdaptadorSql.Fill(pedidosDetalle);

                    verPedidos.DisplayMemberPath = "INFOCOMPLETA";
                    verPedidos.SelectedValuePath = "Id";
                    verPedidos.ItemsSource = pedidosDetalle.DefaultView;
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }
          
        }
         

        // Eventos.
        //private void listaClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    MuestraPedidos();
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string consulta = "DELETE FROM PEDIDO WHERE Id = @PedidoId";

            SqlCommand miSqlCommand = new SqlCommand(consulta , conexion);

            conexion.Open();

            miSqlCommand.Parameters.AddWithValue("@PedidoId", verPedidos.SelectedValue);

            miSqlCommand.ExecuteNonQuery();

            conexion.Close();

            MuestraPedidos();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string insertar = "INSERT INTO Cliente(nombre) VALUES (@nombre)" ;

            SqlCommand miSqlCommand = new SqlCommand(insertar, conexion);

            conexion.Open();

            miSqlCommand.Parameters.AddWithValue("@nombre", insertaCliente.Text);

            miSqlCommand.ExecuteNonQuery();

            conexion.Close();

            MuestraClientes();

            insertaCliente.Text = "";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string consulta = "DELETE FROM CLIENTE WHERE Id = @clienteId";

            SqlCommand miSqlCommand = new SqlCommand(consulta, conexion);

            conexion.Open();

            miSqlCommand.Parameters.AddWithValue("@clienteId", listaClientes.SelectedValue);

            miSqlCommand.ExecuteNonQuery();

            conexion.Close();

            MuestraClientes();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Actualiza ventanaActualizar = new Actualiza((int)listaClientes.SelectedValue);

            try
            {
                string consulta = "SELECT nombre FROM cliente WHERE Id = @clId";

                SqlCommand miSqlCommand = new SqlCommand(consulta, conexion);

                SqlDataAdapter miAdaptadorSql = new SqlDataAdapter(miSqlCommand);

                using (miAdaptadorSql)
                {
                    miSqlCommand.Parameters.AddWithValue("@clId", listaClientes.SelectedValue);

                    DataTable clientesTabla = new DataTable();

                    miAdaptadorSql.Fill(clientesTabla);

                    ventanaActualizar.cuadroAct.Text = clientesTabla.Rows[0]["nombre"].ToString();
                }
            }
            catch (Exception e2)
            {

                MessageBox.Show(e2.ToString());
            }

            ventanaActualizar.ShowDialog();

            MuestraClientes();
        }

        private void listaClientes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MuestraPedidos();
        }

        
    }
}
 