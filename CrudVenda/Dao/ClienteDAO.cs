using System;
using CrudVenda.Conection;
using CrudVenda.Entities;
using CrudVenda.Helpers;
using MySql.Data.MySqlClient;

namespace CrudVenda.Dao;

public class ClienteDAO
{
    public static List<Cliente> List()
    {
        var list = new List<Cliente>();
        const string query = "select * from clientes";
        try
        {
            using var command = new MySqlCommand(query, Conexao.Connect());

            using var reader = command.ExecuteReader();

            string oi = "aa";

            while (reader.Read())
            {
                var cliente = new Cliente
                {
                    Id = reader.GetNullableUInt64("id_cliente"),
                    Nome = reader.GetNullableString("nome"),
                    Cpf = reader.GetNullableString("cpf"),
                    Email = reader.GetNullableString("email"),
                    DataNascimento = reader.GetNullableDateTime("data_nascimento"),
                    Telefone = reader.GetNullableString("telefone"),
                };

                list.Add(cliente);
            }
        }
        finally
        {
            Conexao.FecharConexao();
        }
        return list;
    }

    public static Cliente? FindById(int id)
    {
        const string query = $"select * from clientes where id_cliente = @id";

        try
        {
            var command = new MySqlCommand(query, Conexao.Connect());

            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();

            if (!reader.Read())
            {
                return null;
            }


            var cliente = new Cliente
            {
                Id = reader.GetNullableUInt64("id_cliente"),
                Nome = reader.GetNullableString("nome"),
                Cpf = reader.GetNullableString("cpf"),
                Email = reader.GetNullableString("email"),
                DataNascimento = reader.GetNullableDateTime("data_nascimento"),
                Telefone = reader.GetNullableString("telefone"),
            };

            return cliente;

        }
        finally
        {
            Conexao.FecharConexao();
        }
    }
}
