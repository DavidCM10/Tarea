using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class NegocioPedidos
    {
        private DatosPedidos dPedidos = new DatosPedidos();
        public List<Pedidos> Listar()
        {
            return dPedidos.Listar();
        }
        public int Registrar(Pedidos obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.NombreCliente == "")
            {
                Mensaje += "Nombre completo del Cliente\n";
            }
            if (obj.MontoPedido == 0)
            {
                Mensaje += "Monto superior a 0\n";
            }
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return dPedidos.Registrar(obj, out Mensaje);
            }
        }
    }
}
