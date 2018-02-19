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
    public class ClienteDao: Conexion
    {
        public Cliente getCliente(int clienteId)
        {
            using (connection = new SqlConnection(connectionStringCrm))
            {
                Cliente cli = new Cliente();

                query = "Select ClienteID, RazonSocial From Clientes Where ClienteID = @ClienteID";

                command = new SqlCommand(query, connection);

                SqlParameter paramClienteId = new SqlParameter();
                paramClienteId.ParameterName = "@ClienteID";
                paramClienteId.SqlDbType = SqlDbType.Int;
                paramClienteId.SqlValue = clienteId;

                command.Parameters.Add(paramClienteId);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        cli.clienteId = Convert.ToInt32(reader["ClienteID"]);
                        cli.razonSocial = Convert.ToString(reader["RazonSocial"]);
                    }
                    else
                    {
                        cli = null;
                    }

                    return cli;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }                    
        }
    }
}
