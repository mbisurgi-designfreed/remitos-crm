using REMITOS.data;
using REMITOS.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REMITOS
{
    class Program
    {
        public Program()
        {

        }

        static void Main(string[] args)
        {
            Program program = new Program();

            Console.WindowWidth = 150;

            program.Menu();
        }

        private void Menu()
        {
            int opcion;
            
            Console.WriteLine("SINCRONIZACION REMITOS Y CONTROL FACTURACION");
            Console.WriteLine("1 - SINCRONIZAR REMITOS");
            Console.WriteLine("2 - CONTROL FACTURACION PENDIENTE");
            Console.WriteLine("3 - ACTUALIZAR ESTADO REMITOS");
            Console.WriteLine("4 - MODIFICAR CLIENTE REMITOS");
            Console.WriteLine("5 - MOVIMIENTOS PENDIENTES DE REMITIR");
            opcion = Convert.ToInt32(Console.ReadLine());

            if (opcion == 1)
            {
                Console.WriteLine("\n");
                SincronizarRemitos();
            }

            if (opcion == 2)
            {
                Console.WriteLine("\n");
                ControlFacturacion();
            }

            if (opcion == 3)
            {
                Console.WriteLine("\n");
                ActualizarEstadoRemitos();
            }

            if (opcion == 4)
            {
                Console.WriteLine("\n");
                ModificarClienteRemitos();
            }

            if (opcion == 5)
            {
                Console.WriteLine("\n");
                MovimientosPendientesDeRemitir();
            } 
        }

        private void SincronizarRemitos()
        {
            Console.WriteLine("SINCRONIZAR REMITOS TANGO-CRM\n");
            Console.WriteLine("Iniciando proceso...\n");

            try
            {
                Console.WriteLine("Remitos procesados:");

                RemitoDao remitoDao = new RemitoDao();
                RemitoTangoDao remitoTangoDao = new RemitoTangoDao();
                List<Remito> remitos = remitoDao.getRemitos(false);

                int proxNumeroInterno = remitoTangoDao.ObtenerUltimoNroInterno();

                proxNumeroInterno++;

                foreach (Remito rem in remitos)
                {
                    RemitoTango remTgo = new RemitoTango();

                    remTgo.COD_PRO_CL = Convert.ToString(rem.cliente.clienteId);
                    remTgo.ESTADO_MOV = "P";
                    remTgo.FECHA_MOV = rem.fecha;
                    remTgo.MON_CTE = true;
                    remTgo.N_COMP = FormatoNroRemito(Convert.ToString(rem.nroRemito));
                    remTgo.N_REMITO = FormatoNroRemito(Convert.ToString(rem.nroRemito));
                    remTgo.NCOMP_IN_S = ObtenerProximoNroInterno(proxNumeroInterno);
                    remTgo.NRO_SUCURS = ObtenerNumeroSucursal(rem.nroRemito);
                    remTgo.T_COMP = "REM";
                    remTgo.TALONARIO = 2;
                    remTgo.TCOMP_IN_S = "RE";
                    remTgo.USUARIO = "TANGO-SICO";
                    remTgo.COD_TRANSP = "1";
                    remTgo.items = new List<RemitoTangoItem>();

                    int nroRenglon = 1;

                    foreach (RemitoItem item in rem.items)
                    {
                        RemitoTangoItem itemTgo = new RemitoTangoItem();

                        itemTgo.CAN_EQUI_V = 1;
                        itemTgo.CANT_PEND = item.cantidad;
                        itemTgo.CANTIDAD = item.cantidad;
                        //CODIGO PARA DIME
                        //itemTgo.COD_ARTICU = ObtenerCodigoArticulo(Convert.ToString(item.producto.productoCodigo));
                        itemTgo.COD_ARTICU = Convert.ToString(item.producto.productoCodigo);
                        itemTgo.COD_DEPOSI = "1";
                        itemTgo.EQUIVALENC = 1;
                        itemTgo.FECHA_MOV = rem.fecha;
                        itemTgo.N_RENGL_S = nroRenglon;
                        itemTgo.NCOMP_IN_S = ObtenerProximoNroInterno(proxNumeroInterno);
                        itemTgo.PRECIO = item.precio;
                        itemTgo.PRECIO_REM = rem.calcularTotal();
                        itemTgo.TCOMP_IN_S = "RE";
                        itemTgo.TIPO_MOV = "S";

                        remTgo.items.Add(itemTgo);

                        nroRenglon++;
                    }

                    Console.WriteLine("Nro".PadRight(14, ' ') + "Razon Social".PadRight(50, ' ') + "Fecha".PadRight(11, ' ') + "Total".PadLeft(20, ' '));
                    Console.WriteLine(rem.ToString());

                    remitoTangoDao.AddRemitoTango(Convert.ToString(rem.nroRemito), remTgo);

                    proxNumeroInterno++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }

            Console.WriteLine("\n\n");
            Menu();
        }

        private void ControlFacturacion()
        {
            Console.WriteLine("CONTROL DE FACTURACION PENDIENTE\n");
            Console.WriteLine("Fecha Desde");
            DateTime desde = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Fecha Hasta");
            DateTime hasta = Convert.ToDateTime(Console.ReadLine());

            try
            {
                VentaItemDao ventaItemDao = new VentaItemDao();
                List<VentaItem> ventasTotales = ventaItemDao.getVentasTotales(desde, hasta);
                List<VentaItem> remitosPendientes = ventaItemDao.getRemitosPendientes(desde, hasta);
                List<VentaItem> ventasFacturadas = ventaItemDao.getVentaFacturada(desde, hasta);

                Console.WriteLine("\n");
                Console.WriteLine("Ventas Totales\n");
                Console.WriteLine("Producto".PadRight(20, ' ') + "Cantidad".PadRight(10, ' ') + "Kilos".PadRight(15, ' '));

                foreach (VentaItem ventaTotal in ventasTotales)
                {
                    Console.WriteLine(ventaTotal.ToString());
                }

                Console.WriteLine("\n");
                Console.WriteLine("Venta Facturada\n");
                Console.WriteLine("Producto".PadRight(20, ' ') + "Cantidad".PadRight(10, ' ') + "Kilos".PadRight(15, ' '));

                foreach (VentaItem ventaFacturada in ventasFacturadas)
                {
                    Console.WriteLine(ventaFacturada.ToString());
                }

                Console.WriteLine("\n");
                Console.WriteLine("Remitos Pendientes de Facturar\n");
                Console.WriteLine("Producto".PadRight(20, ' ') + "Cantidad".PadRight(10, ' ') + "Kilos".PadRight(15, ' '));

                foreach (VentaItem remitoPendiente in remitosPendientes)
                {
                    Console.WriteLine(remitoPendiente.ToString());
                }

                Console.WriteLine("\n");
                Console.WriteLine("Venta Pendiente de Facturar\n");
                Console.WriteLine("Producto".PadRight(20, ' ') + "Cantidad".PadRight(10, ' ') + "Kilos".PadRight(15, ' '));

                foreach (VentaItem ventaPendiente in ObtenerVentasPendienteFacturacion(ventasTotales, ventasFacturadas, remitosPendientes))
                {
                    Console.WriteLine(ventaPendiente.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }

            Console.WriteLine("\n\n");
            Menu();
        }

        private void ActualizarEstadoRemitos()
        {
            Console.WriteLine("ACTUALIZAR ESTADO REMITOS TANGO-CRM\n");
            Console.WriteLine("Iniciando proceso...\n");

            try
            {
                Console.WriteLine("Procesando remitos");

                RemitoDao remitoDao = new RemitoDao();
                RemitoTangoDao remitoTangoDao = new RemitoTangoDao();
                List<Remito> remitosCrm = remitoDao.getRemitos(false);
                List<string> remitosTango = remitoTangoDao.getNumerosRemitosTango();

                foreach (Remito rem in remitosCrm)
                {
                    if (remitosTango.Contains(FormatoNroRemito(rem.nroRemito)))
                    {
                        rem.sincronizado = true;
                    }
                }

                remitoDao.actualizarEstado(remitosCrm);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }

            Console.WriteLine("\n\n");
            Menu();
        }

        private void ModificarClienteRemitos()
        {
            Console.WriteLine("MODIFICAR CLIENTE REMITOS\n");
            try
            {
                Console.WriteLine("Nro Remito");
                String nroRemito = FormatoNroRemito(Console.ReadLine());

                ClienteDao clienteDao = new ClienteDao();
                RemitoTangoDao remitoTangoDao = new RemitoTangoDao();

                Cliente viejoCliente = null;

                string viejoClienteId = remitoTangoDao.getClienteId(nroRemito);

                if (viejoClienteId != null)
                {
                    viejoCliente = clienteDao.getCliente(Convert.ToInt32(viejoClienteId));
                }

                Console.WriteLine("Cliente actual remito nro: " + nroRemito);
                Console.WriteLine("Codigo".PadRight(10, ' ') + "Razon Social".PadRight(50, ' '));
                Console.WriteLine(viejoCliente.ToString());

                Console.WriteLine("\n");
                Console.WriteLine("Cliente Nuevo");
                string nuevoClienteId = Console.ReadLine();

                Cliente nuevoCliente = clienteDao.getCliente(Convert.ToInt32(nuevoClienteId));

                Console.WriteLine("Cliente nuevo asignado a remito nro: " + nroRemito);
                Console.WriteLine("Codigo".PadRight(10, ' ') + "Razon Social".PadRight(50, ' '));
                Console.WriteLine(nuevoCliente.ToString());

                Console.WriteLine("\n");
                Console.WriteLine("¿Desea asignar el remito nro: " + nroRemito + " al nuevo cliente? S - N");
                string opcion = Console.ReadLine().ToUpper();

                if (opcion.Equals("S"))
                {
                    remitoTangoDao.UpdateClienteRemito(nroRemito, nuevoClienteId);
                    Menu();
                }
                else
                {
                    Menu();
                }

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
            
        }

        private void MovimientosPendientesDeRemitir()
        {
            Console.WriteLine("MOVIMIENTOS PENDIENTES DE REMITIR\n");
            Console.WriteLine("Fecha Desde");
            DateTime desde = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Fecha Hasta");
            DateTime hasta = Convert.ToDateTime(Console.ReadLine());

            try
            {
                MovimientoDao movimientoDao = new MovimientoDao();
                List<Movimiento> movimientos = movimientoDao.getMovimientos(desde, hasta);

                Console.WriteLine("\n");
                Console.WriteLine("Movimientos Pendientes\n");
                Console.WriteLine("Cliente".PadRight(10, ' ') + "Razon Social".PadRight(50, ' ') + "Movimiento".PadRight(15, ' ') + "Fecha".PadRight(15, ' '));

                foreach (Movimiento mov in movimientos)
                {
                    Console.WriteLine(mov.ToString());
                }
            }
            catch (Exception ex)
            {
               Console.WriteLine("ERROR: " + ex.Message);
            }

            Console.WriteLine("\n\n");
            Menu();
        }

        private int ObtenerNumeroSucursal(String nroRemito) {
            String nroSucural = nroRemito.Substring(0, 4);

            return Int32.Parse(nroSucural);
        }

        //CODIGO PARA DIME

        private string ObtenerCodigoArticulo(string productoCodigo)
        {
            string producto = "";

            if (productoCodigo.Equals("1001"))
            {
                producto = "110";
            }

            if (productoCodigo.Equals("1002"))
            {
                producto = "111";
            }

            if (productoCodigo.Equals("1004"))
            {
                producto = "112";
            }

            if (productoCodigo.Equals("2002"))
            {
                producto = "120";
            }

            if (productoCodigo.Equals("1003"))
            {
                producto = "121";
            }

            if (productoCodigo.Equals("2001"))
            {
                producto = "122";
            }

            return producto;
        }

        private String FormatoNroRemito(String nroRemito)
        {
            String nroSucursal = nroRemito.Substring(0, 4);
            String nroComprobante = nroRemito.Substring(5, 8);

            return "R" + nroSucursal + nroComprobante;
        }

        private String ObtenerProximoNroInterno(int anteNumeroInterno) {
            String proximoNumero = Convert.ToString(anteNumeroInterno).PadLeft(8, '0');

            return proximoNumero;
        }

        private List<VentaItem> ObtenerVentasPendienteFacturacion(List<VentaItem> ventasTotales, List<VentaItem> ventasFacturadas, List<VentaItem> remitosPendientes)
        {
            ProductoDao productoDao = new ProductoDao();
            List<VentaItem> ventasPendientes = new List<VentaItem>();
            List<Producto> productos = productoDao.getProductos();

            foreach (Producto pro in productos)
            {
                VentaItem ventaPendiente = new VentaItem();

                ventaPendiente.producto = pro;
                ventaPendiente.cantidad = 0;

                foreach (VentaItem ventaTotal in ventasTotales)
                {
                    if (ventaTotal.producto.productoId == ventaPendiente.producto.productoId)
                    {
                        ventaPendiente.cantidad = ventaPendiente.cantidad + ventaTotal.cantidad;
                    }
                }

                foreach (VentaItem ventaFacturada in ventasFacturadas)
                {
                    if (ventaFacturada.producto.productoId == ventaPendiente.producto.productoId)
                    {
                        ventaPendiente.cantidad = ventaPendiente.cantidad - ventaFacturada.cantidad;
                    }
                }

                foreach (VentaItem remitoPendiente in remitosPendientes)
                {
                    if (remitoPendiente.producto.productoId == ventaPendiente.producto.productoId)
                    {
                        ventaPendiente.cantidad = ventaPendiente.cantidad - remitoPendiente.cantidad;
                    }
                }

                ventasPendientes.Add(ventaPendiente);
            }

            return ventasPendientes;
        }
    }
}
