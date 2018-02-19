using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REMITOS.model
{
    public class RemitoTango
    {
        //Codigo de cliente
        public string COD_PRO_CL { get; set; } 
        //Estado de movimiento (P = Pendiente, F = Facturado)
        public string ESTADO_MOV { get; set; }
        //Fecha de movimiento
        public DateTime FECHA_MOV { get; set; }
        //Moneda corriente
        public bool MON_CTE { get; set; }
        //Numero de comprobante (Formato R000000000000)
        public string N_COMP { get; set; }
        //Numero de remito (Formato R000000000000)
        public string N_REMITO { get; set; }
        //Numero de comprobante interno
        public string NCOMP_IN_S { get; set; }
        //Numero de sucursal
        public int NRO_SUCURS { get; set; }
        //Tipo de comprobante (REM = Remito)
        public string T_COMP { get; set; }
        //Talonario
        public int TALONARIO { get; set; }
        //Tipo de comprobante segun tango (RE = Remito)
        public string TCOMP_IN_S { get; set; }
        //Usuario (TANGO-SICO)
        public string USUARIO { get; set; }
        //Transporte (1)
        public string COD_TRANSP { get; set; }
        public List<RemitoTangoItem> items { get; set; }
    }
}
