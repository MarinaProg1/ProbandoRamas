using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;

namespace WindowsFromAdoNet
{
    public class ProductoDB
    {
        private string connectionString = "Data Source=.;Initial Catalog=Productos2;" +
            "Persist Security Info=True;Integrated security= true";
        public bool OkConn()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

            }
            catch
            {
                return false;
            }

            return true;
        }

        public List<Producto> DevuelveLista()
        {

            List<Producto> LisProductos1 = new List<Producto>();

            string queryLis = "select Id,Nombre,Cantidad from Producto";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(queryLis, conn);
                try
                {
                    conn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        Producto producto2 = new Producto();
                        producto2.Id = reader.GetInt32(0);
                        producto2.Nombre = reader.GetString(1);
                        producto2.Cantidad = reader.GetInt32(2);
                        //producto2.Precio = reader.GetFloat(3);  
                        LisProductos1.Add(producto2);
                    }
                    reader.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error muy grande" + ex.Message);
                }

            }

            return LisProductos1;
        }
        public void Add(string nombre, int cantidad)
        {


            string queryLis = "insert into Producto(Nombre, Cantidad) values" +
                "(@nombre, @cantidad)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(queryLis, conn);
                sqlCommand.Parameters.AddWithValue("@nombre", nombre);
                sqlCommand.Parameters.AddWithValue("@cantidad", cantidad);
                try
                {
                    conn.Open();
                    sqlCommand.ExecuteNonQuery();

                    conn.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error muy grande" + ex.Message);
                }

            }


        }

    }
}
