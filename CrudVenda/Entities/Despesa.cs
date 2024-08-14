using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudVenda.Entities
{
    internal class Despesa
    {
        public int Numero { get; set; }
        public double Valor { get; set; }

        public DateTime DataVencimento   {  get; set; }

        public DateTime DataPagamento { get; set; }

        public int NumeroPagamento { get; private set; }

        public string StatusDespesa { get; set; }

        public override string ToString()
        {
            return $"{Numero} | {Valor} | {DataVencimento.ToShortDateString()} | {DataPagamento.ToShortDateString()} | {StatusDespesa}";
        }
    }
}
