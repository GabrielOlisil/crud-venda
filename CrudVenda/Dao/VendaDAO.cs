using CrudVenda.Conection;
using CrudVenda.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudVenda.Dao
{
    internal class VendaDAO
    {

        public static void Insert(Venda venda)
        {
            const string sql = "INSERT INTO venda (data_venda, hora, valor_total, desconto, tipo, total_parcelas , fk_cliente) values (@dataVenda, @hora, @valorTotal, @desconto, @tipo,, @totalParcelas, @clienteId)";

            try
            {
                MySqlCommand command = new(sql, Conexao.Connect());

                string? dataVenda = venda.DataVenda?.ToString("yyyy-MM-dd");


                command.Parameters.AddWithValue("@dataVenda", dataVenda);
                command.Parameters.AddWithValue("@hora", venda.Hora);
                command.Parameters.AddWithValue("@valorTotal", venda.ValorTotal);
                command.Parameters.AddWithValue("@desconto", venda.Desconto);
                command.Parameters.AddWithValue("@tipo", venda.Tipo);
                command.Parameters.AddWithValue("@totalParcelas", venda.TotalParcelas);
                command.Parameters.AddWithValue("@clienteId", venda.Cliente?.Id);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Conexao.FecharConexao();
            }
        }

        public static List<Venda> List()
        {
            const string query = "select * from venda v left join cliente c on c.id_cliente = v.fk_cliente";

            var list = new List<Venda>();

            try
            {
                var command = new MySqlCommand(query, Conexao.Connect());

                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var venda = new Venda
                    {
                        Id = reader.GetInt32("id_venda"),
                        DataVenda = reader.GetDateTime("data_venda"),
                        Hora = reader.GetTimeSpan("hora").ToString(),
                        ValorTotal = reader.GetDouble("valor_total"),
                        Desconto = reader.GetDouble("desconto"),
                        Tipo = reader.GetString("tipo"),
                        TotalParcelas = reader.GetInt32("total_parcelas"),
                        Cliente = new Cliente
                        {
                            Id = reader.GetInt32("id_cliente"),
                            Nome = reader.GetString("nome"),
                            Cpf = reader.GetString("cpf"),
                            Email = reader.GetString("email"),
                            DataNascimento = reader.GetDateTime("data_nascimento"),
                            Telefone = reader.GetString("telefone"),
                        }
                    };

                    list.Add(venda);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Conexao.FecharConexao();
            }
            return list;
        }
    }

}
