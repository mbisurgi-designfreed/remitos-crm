using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REMITOS.model
{
    public class RemitoTangoItem
    {
        //Cantidad equivalencia
        public decimal CAN_EQUI_V { get; set; }
        //Cantidad pendiente
        public decimal CANT_PEND { get; set; }
        //Cantidad
        public decimal CANTIDAD { get; set; }
        //Producto
        public string COD_ARTICU { get; set; }
        //Deposito
        public string COD_DEPOSI { get; set; }
        //Equivalencia
        public float EQUIVALENC { get; set; }
        //Fecha de movimiento 
        public DateTime FECHA_MOV { get; set; }
        //Numero de renglon
        public int N_RENGL_S { get; set; }
        //Numero de comprobante interno
        public string NCOMP_IN_S { get; set; }
        //Precio unitario
        public decimal PRECIO { get; set; }
        //Precio total remito
        public decimal PRECIO_REM { get; set; }
        //Tipo de comprobante segun tango (RE = Remito)
        public string TCOMP_IN_S { get; set; }
        //Tipo de movimiento (S = Salida)
        public string TIPO_MOV { get; set; }
    }
}
