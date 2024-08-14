using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudVenda.Entities
{
    internal class Servico
    {
        public int Numero { get; set; }
        public double Valor { get; set; }

        public string Descricao { get; set; }

        public  string Tempo { get; set; }

        public override string ToString()
        {
            return $"{Numero} | {Valor} | {Descricao} | {Tempo}";
        }
    }

}
