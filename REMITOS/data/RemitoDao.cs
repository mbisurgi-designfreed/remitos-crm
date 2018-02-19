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
    public class RemitoDao : Conexion
    {
        public List<Remito> getRemitos(bool sincronizado)
        {
            using (connection = new SqlConnection(connectionStringCrm))
            {
                List<Remito> listado = new List<Remito>();

                query = "sp_remitos_crm_tango";

                command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter paramSincronizado = new SqlParameter();
                paramSincronizado.ParameterName = "@Sincronizado";
                paramSincronizado.SqlDbType = SqlDbType.Bit;
                paramSincronizado.SqlValue = sincronizado;

                command.Parameters.Add(paramSincronizado);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Remito rem;

                        if (listado.Exists(r => r.nroRemito.Equals(reader["NroRemito"])))
                        {
                            if (!listado.Exists(r => r.movimientoEncId == Convert.ToInt32(reader["MovimientoEncId"])))
                            {
                                rem = listado.FirstOrDefault(r => r.nroRemito.Equals(reader["NroRemito"]));
                                rem.movimientoEncId = Convert.ToInt32(reader["MovimientoEncId"]);

                                List<RemitoItem> items = getItems(Convert.ToInt32(reader["MovimientoEncID"]));

                                //rem.total = 0;

                                foreach (RemitoItem item in items)
                                {
                                    rem.agregarItem(item);
                                   //rem.total = rem.total + (item.cantidad * item.precio);
                                }
                            }                          
                        }
                        else
                        {
                            rem = new Remito();

                            ClienteDao clienteDao = new ClienteDao();

                            rem.remitoId = Convert.ToInt32(reader["RemitoID"]);
                            rem.movimientoEncId = Convert.ToInt32(reader["MovimientoEncId"]);
                            rem.fecha = Convert.ToDateTime(reader["Fecha"]);
                            rem.nroRemito = Convert.ToString(reader["NroRemito"]);
                            rem.cliente = clienteDao.getCliente(Convert.ToInt32(reader["ClienteID"]));
                            rem.items = getItems(Convert.ToInt32(reader["MovimientoEncID"]));
                            
                            foreach (RemitoItem item in rem.items)
                            {
                                //rem.total = rem.total + (item.cantidad * item.precio);
                            }

                            listado.Add(rem);
                        }       
                    }

                    return listado;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<RemitoItem> getItems(int movimientoEncId)
        {
            using (connection = new SqlConnection(connectionStringCrm))
            {
                List<RemitoItem> listado = new List<RemitoItem>();

                query = "Select EnvaseID, Cantidad, Monto From MovimientosDet Where MovimientoEncID = @MovimientoEncID";

                command = new SqlCommand(query, connection);

                SqlParameter paramMovimientoEncId = new SqlParameter();
                paramMovimientoEncId.ParameterName = "@MovimientoEncID";
                paramMovimientoEncId.SqlDbType = SqlDbType.Int;
                paramMovimientoEncId.SqlValue = movimientoEncId;

                command.Parameters.Add(paramMovimientoEncId);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        RemitoItem item = new RemitoItem();

                        ProductoDao productoDao = new ProductoDao();

                        item.producto = productoDao.getProducto(Convert.ToInt32(reader["EnvaseID"]));
                        item.cantidad = Convert.ToInt32(reader["Cantidad"]);
                        item.precio = Convert.ToInt32(reader["Monto"]) / item.cantidad;

                        listado.Add(item);
                    }

                    return listado;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void actualizarEstado(string nroRemito, SqlTransaction sqlTransaction)
        {
            using (connection = new SqlConnection(connectionStringCrm))
            {
                query = "Update Remitos Set Sincronizado = 'true' Where NroRemito = @NroRemito";

                command = new SqlCommand(query, connection);
                //command.Transaction = sqlTransaction;

                SqlParameter paramNroRemito = new SqlParameter();
                paramNroRemito.ParameterName = "@NroRemito";
                paramNroRemito.SqlDbType = SqlDbType.NVarChar;
                paramNroRemito.Size = 50;
                paramNroRemito.SqlValue = nroRemito;

                command.Parameters.Add(paramNroRemito);

                try
                {
                    connection.Open();

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    sqlTransaction.Rollback();
                    throw ex;
                }
            }
        }

        public void actualizarEstado(List<Remito> remitos)
        {
            using (connection = new SqlConnection(connectionStringCrm))
            {
                query = "Update Remitos Set Sincronizado = @Sincronizado Where RemitoID = @RemitoID";

                SqlTransaction sqlTransaction;

                connection.Open();
                sqlTransaction = connection.BeginTransaction();

                command = new SqlCommand(query, connection);
                command.Transaction = sqlTransaction;

                command.Parameters.Add(new SqlParameter("RemitoID", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("Sincronizado", SqlDbType.Bit));

                try
                {
                    foreach (Remito rem in remitos)
                    {
                        command.Parameters["RemitoID"].Value = rem.remitoId;
                        command.Parameters["Sincronizado"].Value = rem.sincronizado;

                        command.ExecuteNonQuery();
                    }

                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    sqlTransaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}
