using CrudVenda.Conection;
using CrudVenda.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudVenda.Dao
{
    internal class VendaDAO
    {
        public static void Update(Venda venda)
        {
            const string query = "UPDATE venda SET data_venda = @dataVenda, valor_total = @valorTotal WHERE id_venda = @idVenda";

            try
            {
                using var command = new MySqlCommand(query, Conexao.Connect());
                command.Parameters.AddWithValue("@idCliente", venda.Id);
                command.Parameters.AddWithValue("@dataVenda", venda.DataVenda);
                command.Parameters.AddWithValue("@valorTotal", venda.ValorTotal);
                command.Parameters.AddWithValue("@idVenda", venda.Id);

                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows > 0)
                {
                    Console.WriteLine("Venda atualizada com sucesso.");
                }
                else
                {
                    Console.WriteLine("Nenhuma venda encontrada com o ID fornecido.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao atualizar venda: " + ex.Message);
            }
            finally
            {
                Conexao.FecharConexao();
            }
        }

        public static void Delete(Venda venda)
        {
            try
            {
                string sql = "DELETE FROM venda WHERE id_venda = @idvenda ";
                MySqlCommand comando = new MySqlCommand(sql, Conexao.Connect());
                comando.Parameters.AddWithValue("@idvenda", venda.Id);
                comando.ExecuteNonQuery();
                Console.WriteLine("Cliente excluído com sucesso!");
                Conexao.FecharConexao();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir a venda {ex.Message}");
            }
        }



        public static void Insert(Venda venda)
        {
            const string sql = "INSERT INTO venda (data_venda, hora, valor_total, desconto, tipo, total_parcelas , fk_cliente) values (@dataVenda, @hora, @valorTotal, @desconto, @tipo, @totalParcelas, @clienteId); select last_insert_id();";



            MySqlTransaction? transaction = null;

            try
            {
                var conn = Conexao.Connect();

                using var command = new MySqlCommand(sql, conn);

                transaction = conn.BeginTransaction();

                command.Transaction = transaction;

                string? dataVenda = venda.DataVenda?.ToString("yyyy-MM-dd");


                command.Parameters.AddWithValue("@dataVenda", dataVenda);
                command.Parameters.AddWithValue("@hora", venda.Hora);
                command.Parameters.AddWithValue("@valorTotal", venda.ValorTotal);
                command.Parameters.AddWithValue("@desconto", venda.Desconto);
                command.Parameters.AddWithValue("@tipo", venda.Tipo);
                command.Parameters.AddWithValue("@totalParcelas", venda.TotalParcelas);
                command.Parameters.AddWithValue("@clienteId", venda.Cliente?.Id);


                var idVendaInserida = (ulong)command.ExecuteScalar();

                if (idVendaInserida != 0 && venda.Recebimentos is not null)
                {
                    foreach (var recebimento in venda.Recebimentos)
                    {
                        recebimento.Venda = new Venda()
                        {
                            Id = idVendaInserida
                        };

                        if (!RecebimentoDAO.Insert(recebimento, conn))
                        {
                            command.Transaction.Rollback();
                            return;
                        }
                    }
                }
                command.Transaction.Commit();

            }
            catch (Exception ex)
            {
                transaction?.Rollback();
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
                        Id = reader.GetUInt64("id_venda"),
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
