using System;
using CrudVenda.Control;

namespace CrudVenda.Entities;

public class Venda : IMenuItem
{
    public bool IsMarked { get; set; } = false;

    public int Numero { get; set; }
    public int Quantidade { get; set; }
    public double Valor { get; set; }

    public void RenderTitle()
    {
        System.Console.WriteLine("Numero | Valor | Quantidade");
    }

    public override string ToString()
    {
        return $"{Numero} | {Valor} | {Quantidade}";
    }
}
