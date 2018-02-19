using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace REMITOS.data
{
    public abstract class Conexion
    {
        protected string connectionStringCrm = ConfigurationManager.ConnectionStrings["CRM"].ConnectionString;
        protected string connectionStringTango = ConfigurationManager.ConnectionStrings["Tango"].ConnectionString;
        protected string query;
        protected SqlConnection connection;
        protected SqlCommand command;
    }
}
