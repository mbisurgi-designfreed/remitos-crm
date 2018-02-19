using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REMITOS.model
{
    public class VentaItem
    {
        public Producto producto { get; set; }
        public int cantidad { get; set; }

        public decimal calcularKilosTotales()
        {
            return producto.kilos * cantidad;
        }

        public override string ToString()
        {
            return producto.productoNombre.PadRight(20, ' ') + cantidad.ToString().PadRight(10, ' ') + calcularKilosTotales().ToString().PadRight(15, ' ');
        }
    }
}
