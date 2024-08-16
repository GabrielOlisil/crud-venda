using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudVenda.Entities
{
    internal class Despesa
    {
        public bool IsMarked { get; set; } = false;

        public int Numero { get; set; }
        public double Valor { get; set; }

        public string DataVencimento { get; set; }

        public string DataPagamento { get; set; }

        public int NumeroPagamento { get; private set; }

        public string StatusDespesa { get; set; }

        public Caixa CaixaD { get; set; }

        public override string ToString()
        {
            return $"{Numero} | {Valor} | {DataVencimento} | {DataPagamento} | {StatusDespesa}";
        }
    }
}
