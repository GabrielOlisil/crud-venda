using System;
using CrudVenda.Conection;
using CrudVenda.Entities;
using MySql.Data.MySqlClient;

namespace CrudVenda.Dao;

public class RecebimentoDAO
{
    public static void Insert(Recebimento recebimento)
    {

        const string sql = "INSERT INTO recebimento (valor, data_vencimento, data_pagamento, status_recebimento, fk_caixa, fk_venda) values (@valor, @dataVencimento, @dataPagamento, @statusDespesa, @fkCaixa, @fkVenda)";

        try
        {

            MySqlCommand command = new(sql, Conexao.Connect());

            command.Parameters.AddWithValue("@valor", recebimento.Valor);
            command.Parameters.AddWithValue("@dataVencimento", recebimento.DataVencimento?.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@dataPagamento", recebimento.DataPagamento?.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@statusDespesa", recebimento.Status);
            command.Parameters.AddWithValue("@fkCaixa", 1);
            command.Parameters.AddWithValue("@fkVenda", recebimento.Venda?.Id);
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
}
