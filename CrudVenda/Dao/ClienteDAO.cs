using System;
using CrudVenda.Conection;
using CrudVenda.Entities;
using MySql.Data.MySqlClient;

namespace CrudVenda.Dao;

public class ClienteDAO
{
    public static List<Cliente> List()
    {
        var list = new List<Cliente>();
        const string query = "select * from cliente";
        try
        {
            var command = new MySqlCommand(query, Conexao.Connect());

            using var reader = command.ExecuteReader();


            while (reader.Read())
            {
                var cliente = new Cliente
                {
                    Id = reader.GetInt32("id_cliente"),
                    Nome = reader.GetString("nome"),
                    Cpf = reader.GetString("cpf"),
                    Email = reader.GetString("email"),
                    DataNascimento = reader.GetDateTime("data_nascimento"),
                    Telefone = reader.GetString("telefone"),
                };

                list.Add(cliente);
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

    public static Cliente? FindById(int id)
    {
        const string query = $"select * from cliente where id_cliente = @id";

        try
        {
            var command = new MySqlCommand(query, Conexao.Connect());

            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();

            reader.Read();


            var cliente = new Cliente
            {
                Id = reader.GetInt32("id_cliente"),
                Nome = reader.GetString("nome"),
                Cpf = reader.GetString("cpf"),
                Email = reader.GetString("email"),
                DataNascimento = reader.GetDateTime("data_nascimento"),
                Telefone = reader.GetString("telefone"),
            };

            return cliente;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            Conexao.FecharConexao();
        }



        return null;
    }
}
