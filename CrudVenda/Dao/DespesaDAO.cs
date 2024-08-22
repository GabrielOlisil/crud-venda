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
                command.Parameters.AddWithValue("@fk_caixa", despesa.Caixa);
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

