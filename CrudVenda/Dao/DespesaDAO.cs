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
    internal class DespesaDAO
    {

        public static void Insert(Despesa despesa)
        {

            try
            {

                string sql = "INSERT INTO despesa (valor, data_vencimento, data_pagamento, status_despesa, fk_caixa) values (@valor, @data_vencimento, @data_pagamento, @status_despesa, @fk_caixa)";

                MySqlCommand command = new(sql, Conexao.Connect());

                command.Parameters.AddWithValue("@valor", despesa.Valor);
                command.Parameters.AddWithValue("@data_vencimento", despesa.DataVencimento);
                command.Parameters.AddWithValue("@data_pagamento", despesa.DataPagamento);
                command.Parameters.AddWithValue("@status_despesa", despesa.StatusDespesa);
                command.Parameters.AddWithValue("@fk_caixa", 1);
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

        public void Delete(Despesa despesa)
        {
            try
            {
                string sql = "DELETE FROM despesa WHERE id_despensa = @id_despensa";
                MySqlCommand comando = new MySqlCommand(sql, Conexao.Connect());
                comando.Parameters.AddWithValue("@id_despensa", despesa.Id);
                comando.ExecuteNonQuery();
                Console.WriteLine("Despesa excluida com sucesso!");
                Conexao.FecharConexao();

            }
            catch (Exception ex)
            {
                throw new Exception($"erro ao deletar o cliente {ex}");
            }
        }
        public static List<Despesa> FindById()
        {

            var list = new List<Despesa>();
            try
            {

                string sql = "select * from despesa";

                MySqlCommand command = new(sql, Conexao.Connect());


                using (var reader = command.ExecuteReader())
                {



                    while (reader.Read())
                    {
                        var despesa = new Despesa();

                        despesa.StatusDespesa = reader.GetString("descricao");
                        despesa.DataPagamento = reader.GetString("Data Pagamento");
                        despesa.DataVencimento = reader.GetString("Data Vencimento");
                        despesa.Valor = reader.GetDouble("valor");
                        despesa.StatusDespesa = reader.GetString("Status Despesa");


                        list.Add(despesa);
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

