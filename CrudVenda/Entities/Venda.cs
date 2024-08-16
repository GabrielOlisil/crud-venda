using System;
using CrudVenda.Control;
using CrudVenda.Helpers;

namespace CrudVenda.Entities;

public class Venda : IMenuItem
{
    public bool IsMarked { get; set; } = false;

    public int Numero { get; set; }
    public int Quantidade { get; set; }
    public double Valor { get; set; }

    public void RenderTitle()
    {
        Console.WriteLine($"{PadHelper.ShortPad("Numero")} | {PadHelper.ShortPad("Valor")} | {PadHelper.ShortPad("Quantidade")}");
        Console.WriteLine($"{PadHelper.ShortPad("", '_')} | {PadHelper.ShortPad("", '_')} | {PadHelper.ShortPad("", '_')}");
    }

    public override string ToString()
    {
        return $"{PadHelper.ShortPad(Numero.ToString())} | {PadHelper.ShortPad(Valor.ToString())} | {PadHelper.ShortPad(Quantidade.ToString())}";
    }
}
