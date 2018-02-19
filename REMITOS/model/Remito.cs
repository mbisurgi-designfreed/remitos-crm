using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REMITOS.model
{
    public class Remito
    {
        public int remitoId { get; set; }
        public int movimientoEncId { get; set; }
        public DateTime fecha { get; set; }
        public string nroRemito { get; set; }
        public Cliente cliente { get; set; }
        public List<RemitoItem> items { get; set; }
        //public decimal total { get; set; }
        public bool sincronizado { get; set; }

        public void agregarItem(RemitoItem item)
        {
            items.Add(item);
        }

        public decimal calcularTotal()
        {
            decimal total = 0;

            foreach (RemitoItem item in items)
            {
                total = total + (item.cantidad * item.precio);
            }

            return total;
        }

        public override string ToString()
        {
            return nroRemito.PadRight(14, ' ') + cliente.razonSocial.PadRight(50, ' ') + fecha.Date.ToShortDateString().PadRight(11, ' ') + calcularTotal().ToString().PadLeft(20, ' ');
        }
    }
}
