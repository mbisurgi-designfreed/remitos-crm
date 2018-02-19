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
    public class VentaItemDao: Conexion
    {
        public List<VentaItem> getVentasTotales(DateTime desde, DateTime hasta)
        {
            using (connection = new SqlConnection(connectionStringCrm))
            {
                List<VentaItem> listado = new List<VentaItem>();

                query = "sp_ConsultarVentasBYfecha";

                command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter paramDesde = new SqlParameter();
                paramDesde.ParameterName = "@Fecha1";
                paramDesde.SqlDbType = SqlDbType.DateTime;
                paramDesde.SqlValue = desde;

                command.Parameters.Add(paramDesde);

                SqlParameter paramHasta = new SqlParameter();
                paramHasta.ParameterName = "@Fecha2";
                paramHasta.SqlDbType = SqlDbType.DateTime;
                paramHasta.SqlValue = hasta;

                command.Parameters.Add(paramHasta);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        VentaItem venta;

                        if (listado.Exists(v => v.producto.productoId == Convert.ToInt32(reader["EnvaseID"])))
                        {
                            venta = listado.FirstOrDefault(v => v.producto.productoId == Convert.ToInt32(reader["EnvaseID"]));
                            venta.cantidad = venta.cantidad + Convert.ToInt32(reader["Cantidad"]);
                        }
                        else
                        {
                            venta = new VentaItem();
                            
                            ProductoDao productoDao = new ProductoDao();

                            venta.producto = productoDao.getProducto(Convert.ToInt32(reader["EnvaseID"]));
                            venta.cantidad = Convert.ToInt32(reader["Cantidad"]);

                            listado.Add(venta);
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

        public List<VentaItem> getRemitosPendientes(DateTime desde, DateTime hasta)
        {
            using (connection = new SqlConnection(connectionStringTango))
            {
                List<VentaItem> listado = new List<VentaItem>();

                query = "sp_ConsultarRemitosPendientesBYfecha";

                command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter paramDesde = new SqlParameter();
                paramDesde.ParameterName = "@Fecha1";
                paramDesde.SqlDbType = SqlDbType.DateTime;
                paramDesde.SqlValue = desde;

                command.Parameters.Add(paramDesde);

                SqlParameter paramHasta = new SqlParameter();
                paramHasta.ParameterName = "@Fecha2";
                paramHasta.SqlDbType = SqlDbType.DateTime;
                paramHasta.SqlValue = hasta;

                command.Parameters.Add(paramHasta);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        VentaItem remitoPendiente;

                        ////CODIGO PARA DIME

                        //int productoCodigo = 0;

                        ////Garrafa 10kg
                        //if (Convert.ToInt32(reader["COD_ARTICU"]) == 110)
                        //{
                        //    productoCodigo = 1001;
                        //}

                        ////Garrafa 15kg
                        //if (Convert.ToInt32(reader["COD_ARTICU"]) == 111)
                        //{
                        //    productoCodigo = 1002;
                        //}

                        ////Garrafa 12kg
                        //if (Convert.ToInt32(reader["COD_ARTICU"]) == 112)
                        //{
                        //    productoCodigo = 1004;
                        //}

                        ////Garrafa 45kg
                        //if (Convert.ToInt32(reader["COD_ARTICU"]) == 120)
                        //{
                        //    productoCodigo = 2002;
                        //}

                        ////Garrafa 15kg Me
                        //if (Convert.ToInt32(reader["COD_ARTICU"]) == 121)
                        //{
                        //    productoCodigo = 1003;
                        //}

                        ////Garrafa 30kg
                        //if (Convert.ToInt32(reader["COD_ARTICU"]) == 122)
                        //{
                        //    productoCodigo = 2001;
                        //}

                        if (listado.Exists(v => v.producto.productoCodigo == Convert.ToInt32(reader["COD_ARTICU"])))
                        //if (listado.Exists(v => v.producto.productoCodigo == productoCodigo))
                        {
                            remitoPendiente = listado.FirstOrDefault(v => v.producto.productoCodigo == Convert.ToInt32(reader["COD_ARTICU"]));
                            //remitoPendiente = listado.FirstOrDefault(v => v.producto.productoCodigo == productoCodigo);
                            remitoPendiente.cantidad = remitoPendiente.cantidad + Convert.ToInt32(reader["CANTIDAD"]);
                        }
                        else
                        {
                            remitoPendiente = new VentaItem();

                            ProductoDao productoDao = new ProductoDao();

                            ////CODIGO PARA DIME

                            ////Garrafa 10kg
                            //if (Convert.ToInt32(reader["COD_ARTICU"]) == 110)
                            //{
                            //    remitoPendiente.producto = productoDao.getProducto(Convert.ToString(1001));
                            //}

                            ////Garrafa 15kg
                            //if (Convert.ToInt32(reader["COD_ARTICU"]) == 111)
                            //{
                            //    remitoPendiente.producto = productoDao.getProducto(Convert.ToString(1002));
                            //}

                            ////Garrafa 12kg
                            //if (Convert.ToInt32(reader["COD_ARTICU"]) == 112)
                            //{
                            //    remitoPendiente.producto = productoDao.getProducto(Convert.ToString(1004));
                            //}

                            ////Garrafa 45kg
                            //if (Convert.ToInt32(reader["COD_ARTICU"]) == 120)
                            //{
                            //    remitoPendiente.producto = productoDao.getProducto(Convert.ToString(2002));
                            //}

                            ////Garrafa 15kg Me
                            //if (Convert.ToInt32(reader["COD_ARTICU"]) == 121)
                            //{
                            //    remitoPendiente.producto = productoDao.getProducto(Convert.ToString(1003));
                            //}

                            ////Garrafa 30kg
                            //if (Convert.ToInt32(reader["COD_ARTICU"]) == 122)
                            //{
                            //    remitoPendiente.producto = productoDao.getProducto(Convert.ToString(2001));
                            //}

                            remitoPendiente.producto = productoDao.getProducto(Convert.ToString(reader["COD_ARTICU"]));
                            remitoPendiente.cantidad = Convert.ToInt32(reader["CANTIDAD"]);

                            listado.Add(remitoPendiente);
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

        public List<VentaItem> getVentaFacturada(DateTime desde, DateTime hasta)
        {
            using (connection = new SqlConnection(connectionStringTango))
            {
                List<VentaItem> listado = new List<VentaItem>();

                query = "sp_ConsultarVentaFacturadaBYfecha";

                command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter paramDesde = new SqlParameter();
                paramDesde.ParameterName = "@Fecha1";
                paramDesde.SqlDbType = SqlDbType.DateTime;
                paramDesde.SqlValue = desde;

                command.Parameters.Add(paramDesde);

                SqlParameter paramHasta = new SqlParameter();
                paramHasta.ParameterName = "@Fecha2";
                paramHasta.SqlDbType = SqlDbType.DateTime;
                paramHasta.SqlValue = hasta;

                command.Parameters.Add(paramHasta);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        VentaItem ventaFacturada;

                        //CODIGO PARA DIME

                        //int productoCodigo = 0;

                        ////Garrafa 10kg
                        //if (Convert.ToInt32(reader["COD_ARTICU"]) == 110)
                        //{
                        //    productoCodigo = 1001;
                        //}

                        ////Garrafa 15kg
                        //if (Convert.ToInt32(reader["COD_ARTICU"]) == 111)
                        //{
                        //    productoCodigo = 1002;
                        //}

                        ////Garrafa 12kg
                        //if (Convert.ToInt32(reader["COD_ARTICU"]) == 112)
                        //{
                        //    productoCodigo = 1004;
                        //}

                        ////Garrafa 45kg
                        //if (Convert.ToInt32(reader["COD_ARTICU"]) == 120)
                        //{
                        //    productoCodigo = 2002;
                        //}

                        ////Garrafa 15kg Me
                        //if (Convert.ToInt32(reader["COD_ARTICU"]) == 121)
                        //{
                        //    productoCodigo = 1003;
                        //}

                        ////Garrafa 30kg
                        //if (Convert.ToInt32(reader["COD_ARTICU"]) == 122)
                        //{
                        //    productoCodigo = 2001;
                        //}

                        if (listado.Exists(v => v.producto.productoCodigo == Convert.ToInt32(reader["COD_ARTICU"])))
                        //if (listado.Exists(v => v.producto.productoCodigo == productoCodigo))
                        {
                            ventaFacturada = listado.FirstOrDefault(v => v.producto.productoCodigo == Convert.ToInt32(reader["COD_ARTICU"]));
                            //ventaFacturada = listado.FirstOrDefault(v => v.producto.productoCodigo == productoCodigo);
                            ventaFacturada.cantidad = ventaFacturada.cantidad + Convert.ToInt32(reader["CANTIDAD"]);
                        }
                        else
                        {
                            ventaFacturada = new VentaItem();

                            ProductoDao productoDao = new ProductoDao();

                            //CODIGO PARA DIME

                            ////Garrafa 10kg
                            //if (Convert.ToInt32(reader["COD_ARTICU"]) == 110)
                            //{
                            //    ventaFacturada.producto = productoDao.getProducto(Convert.ToString(1001));
                            //}

                            ////Garrafa 15kg
                            //if (Convert.ToInt32(reader["COD_ARTICU"]) == 111)
                            //{
                            //    ventaFacturada.producto = productoDao.getProducto(Convert.ToString(1002));
                            //}

                            ////Garrafa 12kg
                            //if (Convert.ToInt32(reader["COD_ARTICU"]) == 112)
                            //{
                            //    ventaFacturada.producto = productoDao.getProducto(Convert.ToString(1004));
                            //}

                            ////Garrafa 45kg
                            //if (Convert.ToInt32(reader["COD_ARTICU"]) == 120)
                            //{
                            //    ventaFacturada.producto = productoDao.getProducto(Convert.ToString(2002));
                            //}

                            ////Garrafa 15kg Me
                            //if (Convert.ToInt32(reader["COD_ARTICU"]) == 121)
                            //{
                            //    ventaFacturada.producto = productoDao.getProducto(Convert.ToString(1003));
                            //}

                            ////Garrafa 30kg
                            //if (Convert.ToInt32(reader["COD_ARTICU"]) == 122)
                            //{
                            //    ventaFacturada.producto = productoDao.getProducto(Convert.ToString(2001));
                            //}

                            ventaFacturada.producto = productoDao.getProducto(Convert.ToString(reader["COD_ARTICU"]));
                            ventaFacturada.cantidad = Convert.ToInt32(reader["CANTIDAD"]);

                            listado.Add(ventaFacturada);
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
    }
}
