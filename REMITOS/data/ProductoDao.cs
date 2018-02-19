using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using REMITOS.model;

namespace REMITOS.data
{
    public class ProductoDao : Conexion
    {
        public List<Producto> getProductos()
        {
            using (connection = new SqlConnection(connectionStringCrm))
            {
                List<Producto> listado = new List<Producto>();

                query = "Select EnvaseID, EnvaseCodigo, EnvaseNombre, Kilos From Envases Where TipoEnvaseID = 1 or TipoEnvaseId = 2";

                command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Producto pro = new Producto();

                        pro.productoId = Convert.ToInt32(reader["EnvaseID"]);
                        pro.productoCodigo = Convert.ToInt32(reader["EnvaseCodigo"]);
                        pro.productoNombre = Convert.ToString(reader["EnvaseNombre"]);
                        pro.kilos = Convert.ToDecimal(reader["Kilos"]);

                        listado.Add(pro);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return listado;
            }
        }

        public Producto getProducto(int productoId)
        {
            using (connection = new SqlConnection(connectionStringCrm))
            {
                Producto pro = new Producto();

                query = "Select EnvaseID, EnvaseCodigo, EnvaseNombre, Kilos From Envases Where EnvaseID = @EnvaseID";

                command = new SqlCommand(query, connection);

                SqlParameter paramProductoId = new SqlParameter();
                paramProductoId.ParameterName = "@EnvaseID";
                paramProductoId.SqlDbType = SqlDbType.Int;
                paramProductoId.SqlValue = productoId;

                command.Parameters.Add(paramProductoId);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        pro.productoId = Convert.ToInt32(reader["EnvaseID"]);
                        pro.productoCodigo = Convert.ToInt32(reader["EnvaseCodigo"]);
                        pro.productoNombre = Convert.ToString(reader["EnvaseNombre"]);
                        pro.kilos = Convert.ToDecimal(reader["Kilos"]);
                    }
                    else
                    {
                        pro = null;
                    }

                    return pro;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Producto getProducto(string productoCodigo)
        {
            using (connection = new SqlConnection(connectionStringCrm))
            {
                Producto pro = new Producto();

                query = "Select EnvaseID, EnvaseCodigo, EnvaseNombre, Kilos From Envases Where EnvaseCodigo = @EnvaseCodigo";

                command = new SqlCommand(query, connection);

                SqlParameter paramProductoCodigo = new SqlParameter();
                paramProductoCodigo.ParameterName = "@EnvaseCodigo";
                paramProductoCodigo.SqlDbType = SqlDbType.Int;
                paramProductoCodigo.SqlValue = Convert.ToInt32(productoCodigo);

                command.Parameters.Add(paramProductoCodigo);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        pro.productoId = Convert.ToInt32(reader["EnvaseID"]);
                        pro.productoCodigo = Convert.ToInt32(reader["EnvaseCodigo"]);
                        pro.productoNombre = Convert.ToString(reader["EnvaseNombre"]);
                        pro.kilos = Convert.ToDecimal(reader["Kilos"]);
                    }
                    else
                    {
                        pro = null;
                    }

                    return pro;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
