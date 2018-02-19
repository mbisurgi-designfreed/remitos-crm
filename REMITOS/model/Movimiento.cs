using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REMITOS.model
{
    public class Movimiento
    {
        public int idMovimiento { get; set; }
        public DateTime fecha { get; set; }
        public Cliente cliente { get; set; }

        public override string ToString()
        {
            return Convert.ToString(cliente.clienteId).PadRight(10, ' ') + cliente.razonSocial.PadRight(50, ' ') + idMovimiento.ToString().PadRight(15, ' ') + fecha.ToShortDateString().PadRight(15, ' ');
        }
    }
}
