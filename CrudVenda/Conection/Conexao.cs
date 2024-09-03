using MySql.Data.MySqlClient;

namespace CrudVenda.Conection;

public static class Conexao
{

    static MySqlConnection _connection;
    private static string connString = "server=localhost;uid=root;pwd=example;database=vendas_gestao1;port=3306";

    public static MySqlConnection Connect()
    {


        try
        {
            _connection = new MySqlConnection(connString);
            _connection.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao conectar com o banco de dadoss" + ex.Message);
        }

        return _connection;
    }

    public static void FecharConexao()
    {
        _connection?.Close();
        _connection?.Dispose();
    }

}
