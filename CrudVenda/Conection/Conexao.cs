using MySql.Data.MySqlClient;

namespace CrudVenda.Conection;

public static class Conexao
{

    static MySqlConnection _connection;
    private static string connString = "server=localhost;uid=root;pwd=root;database=vendas_gestao;port=3360";

    public static MySqlConnection Connect()
    {


        try
        {
            _connection = new MySqlConnection(connString);
            _connection.Open();
            Console.WriteLine("Conexão bem sucedida");

        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao conectar com o banco de dadoss" + ex.Message);
        }

        return _connection;
    }

    public static void FecharConexao()
    {
        if (_connection != null)
        {
            _connection.Close();
        }
    }

}
