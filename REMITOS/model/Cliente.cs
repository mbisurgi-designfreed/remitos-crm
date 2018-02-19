using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REMITOS.model
{
    public class Cliente
    {
        public int clienteId { get; set; }
        public string razonSocial { get; set; }

        public override string ToString()
        {
            return Convert.ToString(clienteId).PadRight(10, ' ') + razonSocial.PadRight(50, ' ');
        }
    }
}
