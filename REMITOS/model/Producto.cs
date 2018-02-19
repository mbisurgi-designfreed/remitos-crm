using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REMITOS.model
{
    public class Producto
    {
        public int productoId { get; set; }
        public int productoCodigo { get; set; }
        public string productoNombre { get; set; }
        public decimal kilos { get; set; }
    }
}
