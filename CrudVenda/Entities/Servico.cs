using CrudVenda.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CrudVenda.Helpers.PadHelper;

namespace CrudVenda.Entities
{
    internal class Servico : IMenuItem
    {
        public int Numero { get; set; }
        public double Valor { get; set; }

        public string Descricao { get; set; }

        public string Tempo { get; set; }
        public bool IsMarked { get; set; } = false;

        public void RenderTitle()
        {
            Console.WriteLine($"{ShortPad("Numero")} | {ShortPad("Valor")} | {ShortPad("Descricao")} | {ShortPad("Tempo")}");
            Console.WriteLine($"{ShortPad("", '_')} | {ShortPad("", '_')} | {MediumPad("", '_')} | {ShortPad("", '_')}");

        }

        public override string ToString()
        {
            return $"{ShortPad(Numero.ToString())} | {ShortPad(Valor.ToString())} | {MediumPad(Descricao)} | {ShortPad(Tempo)}";
        }
    }

}
