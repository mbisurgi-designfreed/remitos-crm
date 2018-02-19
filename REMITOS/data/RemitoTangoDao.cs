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
    public class RemitoTangoDao: Conexion
    {
        public List<string> getNumerosRemitosTango()
        {
            using (connection = new SqlConnection(connectionStringTango))
            {
                List<string> listado = new List<string>();
                
                query = "Select N_COMP From STA14";

                command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        listado.Add(Convert.ToString(reader["N_COMP"]));
                    }

                    return listado;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public string getClienteId(String nroRemito)
        {
            using (connection = new SqlConnection(connectionStringTango))
            {
                String clienteId;

                query = "Select COD_PRO_CL From STA14 Where N_COMP = @N_COMP";

                command = new SqlCommand(query, connection);

                SqlParameter paramN_COMP = new SqlParameter();
                paramN_COMP.ParameterName = "@N_COMP";
                paramN_COMP.SqlDbType = SqlDbType.NVarChar;
                paramN_COMP.Size = 50;
                paramN_COMP.SqlValue = nroRemito;

                command.Parameters.Add(paramN_COMP);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        clienteId = Convert.ToString(reader["COD_PRO_CL"]);
                    }
                    else
                    {
                        clienteId = null;
                    }

                    return clienteId;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void AddRemitoTango(String nroRemito, RemitoTango remitoTango)
        {
            using (connection = new SqlConnection(connectionStringTango))
            {
                query = "Insert Into STA14(COD_PRO_CL, ESTADO_MOV, FECHA_MOV, MON_CTE, N_COMP, N_REMITO, NCOMP_IN_S, NRO_SUCURS, T_COMP, TALONARIO, TCOMP_IN_S, USUARIO, COD_TRANSP) Values(@COD_PRO_CL, @ESTADO_MOV, @FECHA_MOV, @MON_CTE, @N_COMP, @N_REMITO, @NCOMP_IN_S, @NRO_SUCURS, @T_COMP, @TALONARIO, @TCOMP_IN_S, @USUARIO, @COD_TRANSP)";

                SqlTransaction sqlTransaction;

                connection.Open();
                sqlTransaction = connection.BeginTransaction();

                command = new SqlCommand(query, connection);
                command.Transaction = sqlTransaction;
               
                SqlParameter paramCOD_PRO_CL = new SqlParameter();
                paramCOD_PRO_CL.ParameterName = "@COD_PRO_CL";
                paramCOD_PRO_CL.SqlDbType = SqlDbType.NVarChar;
                paramCOD_PRO_CL.Size = 50;
                paramCOD_PRO_CL.SqlValue = remitoTango.COD_PRO_CL;

                command.Parameters.Add(paramCOD_PRO_CL);

                SqlParameter paramESTADO_MOV = new SqlParameter();
                paramESTADO_MOV.ParameterName = "@ESTADO_MOV";
                paramESTADO_MOV.SqlDbType = SqlDbType.NVarChar;
                paramESTADO_MOV.Size = 50;
                paramESTADO_MOV.SqlValue = remitoTango.ESTADO_MOV;

                command.Parameters.Add(paramESTADO_MOV);

                SqlParameter paramFECHA_MOV = new SqlParameter();
                paramFECHA_MOV.ParameterName = "@FECHA_MOV";
                paramFECHA_MOV.SqlDbType = SqlDbType.DateTime;
                paramFECHA_MOV.Size = 50;
                paramFECHA_MOV.SqlValue = remitoTango.FECHA_MOV;

                command.Parameters.Add(paramFECHA_MOV);

                SqlParameter paramMON_CTE = new SqlParameter();
                paramMON_CTE.ParameterName = "@MON_CTE";
                paramMON_CTE.SqlDbType = SqlDbType.Bit;
                paramMON_CTE.SqlValue = remitoTango.MON_CTE;

                command.Parameters.Add(paramMON_CTE);

                SqlParameter paramN_COMP = new SqlParameter();
                paramN_COMP.ParameterName = "@N_COMP";
                paramN_COMP.SqlDbType = SqlDbType.NVarChar;
                paramN_COMP.Size = 50;
                paramN_COMP.SqlValue = remitoTango.N_COMP;

                command.Parameters.Add(paramN_COMP);

                SqlParameter paramN_REMITO = new SqlParameter();
                paramN_REMITO.ParameterName = "@N_REMITO";
                paramN_REMITO.SqlDbType = SqlDbType.NVarChar;
                paramN_REMITO.Size = 50;
                paramN_REMITO.SqlValue = remitoTango.N_REMITO;

                command.Parameters.Add(paramN_REMITO);

                SqlParameter paramNCOMP_IN_S = new SqlParameter();
                paramNCOMP_IN_S.ParameterName = "@NCOMP_IN_S";
                paramNCOMP_IN_S.SqlDbType = SqlDbType.NVarChar;
                paramNCOMP_IN_S.Size = 50;
                paramNCOMP_IN_S.SqlValue = remitoTango.NCOMP_IN_S;

                command.Parameters.Add(paramNCOMP_IN_S);

                SqlParameter paramNRO_SUCURS = new SqlParameter();
                paramNRO_SUCURS.ParameterName = "@NRO_SUCURS";
                paramNRO_SUCURS.SqlDbType = SqlDbType.Int;
                paramNRO_SUCURS.SqlValue = remitoTango.NRO_SUCURS;

                command.Parameters.Add(paramNRO_SUCURS);

                SqlParameter paramT_COMP = new SqlParameter();
                paramT_COMP.ParameterName = "@T_COMP";
                paramT_COMP.SqlDbType = SqlDbType.NVarChar;
                paramT_COMP.Size = 50;
                paramT_COMP.SqlValue = remitoTango.T_COMP;

                command.Parameters.Add(paramT_COMP);

                SqlParameter paramTALONARIO = new SqlParameter();
                paramTALONARIO.ParameterName = "@TALONARIO";
                paramTALONARIO.SqlDbType = SqlDbType.Int;
                paramTALONARIO.SqlValue = remitoTango.TALONARIO;

                command.Parameters.Add(paramTALONARIO);

                SqlParameter paramTCOMP_IN_S = new SqlParameter();
                paramTCOMP_IN_S.ParameterName = "@TCOMP_IN_S";
                paramTCOMP_IN_S.SqlDbType = SqlDbType.NVarChar;
                paramTCOMP_IN_S.Size = 50;
                paramTCOMP_IN_S.SqlValue = remitoTango.TCOMP_IN_S;

                command.Parameters.Add(paramTCOMP_IN_S);

                SqlParameter paramUSUARIO = new SqlParameter();
                paramUSUARIO.ParameterName = "@USUARIO";
                paramUSUARIO.SqlDbType = SqlDbType.NVarChar;
                paramUSUARIO.Size = 50;
                paramUSUARIO.SqlValue = remitoTango.USUARIO;

                command.Parameters.Add(paramUSUARIO);

                SqlParameter paramCOD_TRANSP = new SqlParameter();
                paramCOD_TRANSP.ParameterName = "@COD_TRANSP";
                paramCOD_TRANSP.SqlDbType = SqlDbType.NVarChar;
                paramCOD_TRANSP.Size = 50;
                paramCOD_TRANSP.SqlValue = remitoTango.COD_TRANSP;

                command.Parameters.Add(paramCOD_TRANSP);

                try
                {
                    command.ExecuteNonQuery();

                    AddRemitoTangoItem(remitoTango, connection, sqlTransaction);

                    RemitoDao remitoDao = new RemitoDao();
                    remitoDao.actualizarEstado(nroRemito, sqlTransaction);

                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    sqlTransaction.Rollback();
                    throw ex;
                }
            }
        }

        public void AddRemitoTangoItem(RemitoTango remitoTango, SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            query = "Insert Into STA20(CAN_EQUI_V, CANT_PEND, CANTIDAD, COD_ARTICU, COD_DEPOSI, EQUIVALENC, FECHA_MOV, N_RENGL_S, NCOMP_IN_S, PRECIO, PRECIO_REM, TCOMP_IN_S, TIPO_MOV) Values(@CAN_EQUI_V, @CANT_PEND, @CANTIDAD, @COD_ARTICU, @COD_DEPOSI, @EQUIVALENC, @FECHA_MOV, @N_RENGL_S, @NCOMP_IN_S, @PRECIO, @PRECIO_REM, @TCOMP_IN_S, @TIPO_MOV)";

            command = new SqlCommand(query, connection);
            command.Transaction = sqlTransaction;

            command.Parameters.Add(new SqlParameter("CAN_EQUI_V", SqlDbType.Decimal));
            command.Parameters.Add(new SqlParameter("CANT_PEND", SqlDbType.Decimal));
            command.Parameters.Add(new SqlParameter("CANTIDAD", SqlDbType.Decimal));
            command.Parameters.Add(new SqlParameter("COD_ARTICU", SqlDbType.NVarChar));
            command.Parameters.Add(new SqlParameter("COD_DEPOSI", SqlDbType.NVarChar));
            command.Parameters.Add(new SqlParameter("EQUIVALENC", SqlDbType.Decimal));
            command.Parameters.Add(new SqlParameter("FECHA_MOV", SqlDbType.DateTime));
            command.Parameters.Add(new SqlParameter("N_RENGL_S", SqlDbType.NVarChar));
            command.Parameters.Add(new SqlParameter("NCOMP_IN_S", SqlDbType.NVarChar));
            command.Parameters.Add(new SqlParameter("PRECIO", SqlDbType.Decimal));
            command.Parameters.Add(new SqlParameter("PRECIO_REM", SqlDbType.Decimal));
            command.Parameters.Add(new SqlParameter("TCOMP_IN_S", SqlDbType.NVarChar));
            command.Parameters.Add(new SqlParameter("TIPO_MOV", SqlDbType.NVarChar));

            try
            {
                foreach (RemitoTangoItem item in remitoTango.items)
                {
                    command.Parameters["CAN_EQUI_V"].Value = item.CAN_EQUI_V;
                    command.Parameters["CANT_PEND"].Value = item.CANT_PEND;
                    command.Parameters["CANTIDAD"].Value = item.CANTIDAD;
                    command.Parameters["COD_ARTICU"].Value = item.COD_ARTICU;
                    command.Parameters["COD_DEPOSI"].Value = item.COD_DEPOSI;
                    command.Parameters["EQUIVALENC"].Value = item.EQUIVALENC;
                    command.Parameters["FECHA_MOV"].Value = item.FECHA_MOV;
                    command.Parameters["N_RENGL_S"].Value = item.N_RENGL_S;
                    command.Parameters["NCOMP_IN_S"].Value = item.NCOMP_IN_S;
                    command.Parameters["PRECIO"].Value = item.PRECIO;
                    command.Parameters["PRECIO_REM"].Value = item.PRECIO_REM;
                    command.Parameters["TCOMP_IN_S"].Value = item.TCOMP_IN_S;
                    command.Parameters["TIPO_MOV"].Value = item.TIPO_MOV;

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                sqlTransaction.Rollback();
                throw ex;
            }  
        }

        public void UpdateClienteRemito(string nroRemito, string clienteId)
        {
            using (connection = new SqlConnection(connectionStringTango))
            {
                query = "Update STA14 Set COD_PRO_CL = @COD_PRO_CL Where N_COMP = @N_COMP";

                command = new SqlCommand(query, connection);

                SqlParameter paramN_COMP = new SqlParameter();
                paramN_COMP.ParameterName = "@N_COMP";
                paramN_COMP.SqlDbType = SqlDbType.NVarChar;
                paramN_COMP.Size = 50;
                paramN_COMP.SqlValue = nroRemito;

                command.Parameters.Add(paramN_COMP);

                SqlParameter paramCOD_PRO_CL = new SqlParameter();
                paramCOD_PRO_CL.ParameterName = "@COD_PRO_CL";
                paramCOD_PRO_CL.SqlDbType = SqlDbType.NVarChar;
                paramCOD_PRO_CL.Size = 50;
                paramCOD_PRO_CL.SqlValue = Convert.ToString(clienteId);

                command.Parameters.Add(paramCOD_PRO_CL);

                try
                {
                    connection.Open();

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {                    
                    throw ex;
                }
            }
        }

        public int ObtenerUltimoNroInterno()
        {
            using (connection = new SqlConnection(connectionStringTango))
            {
                query = "Select Top 1 NCOMP_IN_S From STA14 Order By NCOMP_IN_S Desc";

                command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        return Convert.ToInt32(reader["NCOMP_IN_S"]);
                    }

                    return 0;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
