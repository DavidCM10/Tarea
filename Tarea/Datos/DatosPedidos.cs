using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
    public class DatosPedidos
    {
        public List<Pedidos> Listar()
        {
            List<Pedidos> lista = new List<Pedidos>();
            using (SqlConnection conexiones = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("Select IdPedido,Fecha,NombreCliente,MontoPedido,Distrito from PEDIDO");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexiones);
                    cmd.CommandType = CommandType.Text;

                    conexiones.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Pedidos()
                            {
                                IdPedido = Convert.ToInt32(dr["IdPedido"]),
                                Fecha = Convert.ToDateTime(dr["Fecha"]),
                                NombreCliente = dr["NombreCliente"].ToString(),
                                MontoPedido = Convert.ToInt32(dr["MontoPedido"]),
                                Distrito = dr["Distrito"].ToString(),
                            });

                        }
                    }

                }
                catch (Exception ex)
                {
                    lista = new List<Pedidos>();
                }
            }
            return lista;
        }
        public int Registrar(Pedidos obj, out string Mensaje)
        {
            int IdPedidoGen = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection conexiones = new SqlConnection(Conexion.cadena))
                {



                    SqlCommand cmd = new SqlCommand("SP_RegistrarPedido", conexiones);
                    cmd.Parameters.AddWithValue("Fecha", obj.Fecha);
                    cmd.Parameters.AddWithValue("NombreCliente", obj.NombreCliente);
                    cmd.Parameters.AddWithValue("MontoPedido", obj.MontoPedido);
                    cmd.Parameters.AddWithValue("Distrito", obj.Distrito);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conexiones.Open();
                    cmd.ExecuteNonQuery();
                    IdPedidoGen = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                IdPedidoGen = 0;
                Mensaje = ex.Message;
            }



            return IdPedidoGen;
        }

    }
}

