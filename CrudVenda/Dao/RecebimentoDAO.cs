using System;
using CrudVenda.Conection;
using CrudVenda.Entities;
using CrudVenda.Helpers;
using MySql.Data.MySqlClient;

namespace CrudVenda.Dao;

public class RecebimentoDAO
{
    public static bool Insert(Recebimento recebimento, MySqlConnection connection)
    {

        const string sql = "INSERT INTO recebimentos (valor, data_vencimento, data_pagamento, status_recebimento, fk_caixa, fk_venda) values (@valor, @dataVencimento, @dataPagamento, @statusDespesa, @fkCaixa, @fkVenda)";

        try
        {

            using MySqlCommand command = new(sql, connection);

            command.Parameters.AddWithValue("@valor", recebimento.Valor);
            command.Parameters.AddWithValue("@dataVencimento", recebimento.DataVencimento?.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@dataPagamento", recebimento.DataPagamento?.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@statusDespesa", recebimento.Status);
            command.Parameters.AddWithValue("@fkCaixa", 1);
            command.Parameters.AddWithValue("@fkVenda", recebimento.Venda?.Id);

            if (command.ExecuteNonQuery() <= 0)
            {
                throw new InvalidOperationException("O recebimento não pode ser inserido no banco de dados");
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro Ao Inserir Recebimento: " + ex.Message);
            return false;
        }

    }


    public static List<Recebimento> List(Venda venda)
    {
        const string sql = "select * from recebimentos r where r.fk_venda = @idVenda;";

        List<Recebimento> lista = [];


        if (venda is null || venda.Id is null)
        {
            throw new ArgumentException("Informe uma venda válida");
        }

        try
        {
            using var command = new MySqlCommand(sql, Conexao.Connect());

            command.Parameters.AddWithValue("idVenda", venda.Id);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var recebimento = new Recebimento()
                {
                    Id = reader.GetNullableUInt64("id_recebimento"),
                    DataPagamento = reader.GetNullableDateTime("data_pagamento"),
                    Valor = reader.GetNullableDouble("valor"),
                    DataVencimento = reader.GetNullableDateTime("data_vencimento"),
                    Status = reader.GetNullableString("status_recebimento")
                };
                lista.Add(recebimento);
            }

        }
        finally
        {
            Conexao.FecharConexao();
        }

        return lista;
    }
}
