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

        public int Id { get; set; }
        public double Valor { get; set; }

        public string DataVencimento { get; set; }

        public string DataPagamento { get; set; }

        public string StatusDespesa { get; set; }

        public Caixa Caixa { get; set; }

        public override string ToString()
        {
            return $"{Id} | {Valor} | {DataVencimento} | {DataPagamento} | {StatusDespesa}";
        }
    }
}
