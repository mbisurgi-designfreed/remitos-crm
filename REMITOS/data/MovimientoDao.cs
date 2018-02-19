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
    public class MovimientoDao: Conexion
    {
        public List<Movimiento> getMovimientos(DateTime desde, DateTime hasta)
        {
            using (connection = new SqlConnection(connectionStringCrm))
            {
                List<Movimiento> listado = new List<Movimiento>();

                query = "Select MovimientoEncID, Fecha, ClienteID From MovimientosEnc Where EstadoMovimientoID = 3 And Fecha Between @Desde And @Hasta And CondicionVentaID = 2 And Remito = 0";

                command = new SqlCommand(query, connection);

                SqlParameter paramDesde = new SqlParameter();
                paramDesde.ParameterName = "@Desde";
                paramDesde.SqlDbType = SqlDbType.DateTime;
                paramDesde.SqlValue = desde;

                command.Parameters.Add(paramDesde);

                SqlParameter paramHasta = new SqlParameter();
                paramHasta.ParameterName = "@Hasta";
                paramHasta.SqlDbType = SqlDbType.DateTime;
                paramHasta.SqlValue = hasta;

                command.Parameters.Add(paramHasta);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Movimiento mov = new Movimiento();

                        ClienteDao clienteDao = new ClienteDao();

                        mov.idMovimiento =  Convert.ToInt32(reader["MovimientoEncID"]);
                        mov.fecha = Convert.ToDateTime(reader["Fecha"]);
                        mov.cliente = clienteDao.getCliente(Convert.ToInt32(reader["ClienteID"]));

                        listado.Add(mov);
                    }

                    return listado;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
