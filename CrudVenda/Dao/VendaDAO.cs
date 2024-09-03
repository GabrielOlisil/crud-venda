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
using CrudVenda.Helpers;

namespace CrudVenda.Dao
{
    internal class VendaDAO
    {
        /// <summary>
        /// Atualiza uma venda no banco de dados
        /// </summary>
        /// <param name="venda"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public static void Update(Venda venda)
        {
            const string query = "UPDATE vendas SET data_venda = @dataVenda, valor_total = @valorTotal, hora = @hora, total_parcelas = @totalParcelas, desconto = @desconto, valor_final = @valorFinal, tipo = @tipo, fk_cliente = @idCliente  WHERE id_venda = @idVenda";

            try
            {
                using var command = new MySqlCommand(query, Conexao.Connect());
                command.Parameters.AddWithValue("@dataVenda", venda.DataVenda);
                command.Parameters.AddWithValue("@valorTotal", venda.ValorTotal);
                command.Parameters.AddWithValue("@idVenda", venda.Id);
                command.Parameters.AddWithValue("@hora", venda.Hora);
                command.Parameters.AddWithValue("@totalParcelas", venda.TotalParcelas);
                command.Parameters.AddWithValue("@desconto", venda.Desconto);
                command.Parameters.AddWithValue("@valorFinal", venda.ValorFinal);
                command.Parameters.AddWithValue("@tipo", venda.Tipo);
                command.Parameters.AddWithValue("@idCliente", venda.Cliente?.Id);

                int affectedRows = command.ExecuteNonQuery();

                if (affectedRows <= 0)
                {
                    throw new InvalidOperationException("Não foi possível atualizar o registro");
                }
            }
            finally
            {
                Conexao.FecharConexao();
            }
        }

        /// <summary>
        /// Exclui uma venda do banco de dados
        /// </summary>
        /// <param name="venda"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public static void Delete(Venda venda)
        {
            const string sql = "DELETE FROM vendas WHERE id_venda = @idvenda ";

            try
            {
                using var comando = new MySqlCommand(sql, Conexao.Connect());
                comando.Parameters.AddWithValue("@idvenda", venda.Id);
                var affectedRows = comando.ExecuteNonQuery();

                if (affectedRows <= 0)
                {
                    throw new InvalidOperationException("Não foi possível excluir o registro");
                }
            }
            finally
            {
                Conexao.FecharConexao();
            }
        }


        /// <summary>
        /// Insere uma venda no Banco de Dados
        /// </summary>
        /// <param name="venda"></param>
        public static void Insert(Venda venda)
        {
            const string sql = "INSERT INTO vendas (data_venda, hora, valor_total, desconto, valor_final, tipo, total_parcelas , fk_cliente) values (@dataVenda, @hora, @valorTotal, @desconto, @valorFinal , @tipo, @totalParcelas, @clienteId); select last_insert_id();";



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
                command.Parameters.AddWithValue("@valorFinal", venda.ValorFinal);
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
                            throw new InvalidOperationException("Erro ao Inserir recebimento");
                        }
                    }
                }
                command.Transaction.Commit();

            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                Conexao.FecharConexao();
            }
        }



        /// <summary>
        /// Lista as vendas no banco de dados
        /// </summary>
        /// <returns></returns>
        public static List<Venda> List()
        {
            const string query = "select * from vendas v left join clientes c on c.id_cliente = v.fk_cliente";
            var list = new List<Venda>();
            try
            {
                var command = new MySqlCommand(query, Conexao.Connect());

                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var venda = new Venda
                    {
                        Id = reader.GetNullableUInt64("id_venda"),
                        DataVenda = reader.GetNullableDateTime("data_venda"),
                        Hora = reader.GetNullableTimeSpan("hora").ToString(),
                        ValorTotal = reader.GetNullableDouble("valor_total"),
                        ValorFinal = reader.GetNullableDouble("valor_final"),
                        Desconto = reader.GetNullableDouble("desconto"),
                        Tipo = reader.GetNullableString("tipo"),
                        TotalParcelas = reader.GetNullableInt32("total_parcelas"),

                        Cliente = new Cliente
                        {
                            Id = reader.GetNullableUInt64("id_cliente"),
                            Nome = reader.GetNullableString("nome"),
                            Cpf = reader.GetNullableString("cpf"),
                            Email = reader.GetNullableString("email"),
                            DataNascimento = reader.GetNullableDateTime("data_nascimento"),
                            Telefone = reader.GetNullableString("telefone")
                        }
                    };
                    list.Add(venda);
                }
            }
            finally
            {
                Conexao.FecharConexao();
            }
            return list;
        }
    }

}
