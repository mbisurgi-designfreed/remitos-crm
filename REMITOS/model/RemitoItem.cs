using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REMITOS.model
{
    public class RemitoItem
    {
        public Producto producto { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
    }
}
