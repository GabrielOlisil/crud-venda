using System;
using CrudVenda.Conection;
using CrudVenda.Entities;
using MySql.Data.MySqlClient;

namespace CrudVenda.Dao;

public class RecebimentoDAO
{
    public static bool Insert(Recebimento recebimento, MySqlConnection connection)
    {

        const string sql = "INSERT INTO recebimento (valor, data_vencimento, data_pagamento, status_recebimento, fk_caixa, fk_venda) values (@valor, @dataVencimento, @dataPagamento, @statusDespesa, @fkCaixa, @fkVenda)";

        try
        {

            using MySqlCommand command = new(sql, connection);

            command.Parameters.AddWithValue("@valor", recebimento.Valor);
            command.Parameters.AddWithValue("@dataVencimento", recebimento.DataVencimento?.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@dataPagamento", recebimento.DataPagamento?.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@statusDespesa", recebimento.Status);
            command.Parameters.AddWithValue("@fkCaixa", 1);
            command.Parameters.AddWithValue("@fkVenda", recebimento.Venda?.Id);

            if (command.ExecuteNonQuery() > 0)
            {
                return true;
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }



        return false;

    }
}
