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
    internal class ServicoDAO
    {

        public static void Insert(Servico servico)
        {

            try
            {

                string sql = "INSERT INTO servico (valor, descricao, tempo) values (@valor, @descricao, @tempo)";

                MySqlCommand command = new(sql, Conexao.Connect());

                command.Parameters.AddWithValue("@valor", servico.Valor);
                command.Parameters.AddWithValue("@descricao", servico.Descricao);
                command.Parameters.AddWithValue("@tempo", servico.Tempo);
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

        public static List<Servico> Read()
        {

            var list = new List<Servico>();
            try
            {

                string sql = "select * from servico";

                MySqlCommand command = new(sql, Conexao.Connect());


                using (var reader = command.ExecuteReader())
                {



                    while (reader.Read())
                    {
                        var servico = new Servico();

                        servico.Descricao = reader.GetString("descricao");
                        servico.Tempo = reader.GetString("Tempo");
                        servico.Valor = reader.GetDouble("valor");
                        list.Add(servico);
                    }

                }

            }
            catch (Exception ex) { }
            finally
            {
                Conexao.FecharConexao();
            }
            return list;
        }
    }

}
