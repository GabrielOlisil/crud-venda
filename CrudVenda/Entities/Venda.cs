using System;
using CrudVenda.Control;
using CrudVenda.Helpers;

namespace CrudVenda.Entities;

public class Venda : IMenuItem
{
    public bool IsMarked { get; set; } = false;

    public int Id { get; set; }
    public int? TotalParcelas { get; set; }
    public DateTime? DataVenda { get; set; }
    public double? Desconto { get; set; }
    public double? ValorTotal { get; set; }
    public string? Hora { get; set; }
    public string? Tipo { get; set; }

    public Cliente? Cliente { get; set; }




    public void RenderTitle()
    {
        Console.WriteLine($"{PadHelper.ShortPad("ID")} | {PadHelper.ShortPad("Data")} | {PadHelper.ShortPad("Desconto")} | {PadHelper.ShortPad("Valor Total")} | {PadHelper.ShortPad("Hora")} | {PadHelper.ShortPad("Tipo")} | {PadHelper.MediumPad("Cliente")} ");


        Console.WriteLine($"{PadHelper.ShortPad("", '_')} | {PadHelper.ShortPad("", '_')} | {PadHelper.ShortPad("", '_')} | {PadHelper.ShortPad("", '_')} | {PadHelper.ShortPad("", '_')} | {PadHelper.ShortPad("", '_')} | {PadHelper.MediumPad("", '_')}");

    }

    public override string ToString()
    {
        return $"{PadHelper.ShortPad(Id.ToString())} | {PadHelper.ShortPad(DataVenda?.ToString("yyyy/MM/dd") ?? "")} | {PadHelper.ShortPad(Desconto.ToString() ?? "")} | {PadHelper.ShortPad(ValorTotal.ToString() ?? "")} | {PadHelper.ShortPad(Hora ?? "")} | {PadHelper.ShortPad(Tipo ?? "")} | {PadHelper.MediumPad(Cliente?.Nome ?? "")} ";

    }
}
