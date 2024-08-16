using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudVenda.Entities
{

    public class Caixa
    {
        
        public int IdCaixa { get; set; }  
        public double SaldoInicial { get; set; }  
        public double TotalEntrado { get; set; }  
        public double TotalSaida { get; set; }  
        public string StatusCaixa { get; set; }  


        public Caixa() { }
        public Caixa(int idCaixa, double saldoInicial, double totalEntrado, double totalSaida, string statusCaixa)
        {
            IdCaixa = idCaixa;
            SaldoInicial = saldoInicial;
            TotalEntrado = totalEntrado;
            TotalSaida = totalSaida;
            StatusCaixa = statusCaixa;
        }

        // Método para calcular o saldo atual baseado nas entradas e saídas
        public double CalcularSaldoAtual()
        {
            return SaldoInicial + TotalEntrado - TotalSaida;
        }

        // Método override para exibir os detalhes da classe como uma string
        public override string ToString()
        {
            return $"Caixa:\n" +
                   $"ID: {IdCaixa}\n" +
                   $"Saldo Inicial: {SaldoInicial.ToString("F2")}\n" +
                   $"Total Entrado: {TotalEntrado.ToString("F2")}\n" +
                   $"Total Saída: {TotalSaida.ToString("F2")}\n" +
                   $"Status: {StatusCaixa}\n" +
                   $"Saldo Atual: {CalcularSaldoAtual().ToString("F2")}";
        }
    }

}
