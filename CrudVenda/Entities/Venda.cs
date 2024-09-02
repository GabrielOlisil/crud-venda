using System;
using CrudVenda.Menu;
using CrudVenda.Helpers;

namespace CrudVenda.Entities;

public class Venda : IMenuItem
{
    public bool IsMarked { get; set; } = false;

    public ulong? Id { get; set; }
    public int? TotalParcelas { get; set; }
    public DateTime? DataVenda { get; set; }
    public double? Desconto { get; set; }
    public double? ValorTotal { get; set; }
    public double? ValorFinal { get; set; }
    public string? Hora { get; set; }
    public string? Tipo { get; set; }

    public Cliente? Cliente { get; set; }

    public List<Recebimento>? Recebimentos { get; set; }




    public void RenderTitle()
    {
        Console.WriteLine($"{PadHelper.NumberPad("ID")} | {PadHelper.ShortPad("Data")} | {PadHelper.ShortPad("Valor Total")} | {PadHelper.NumberPad("Desconto")} | {PadHelper.ShortPad("Valor Final")} | {PadHelper.ShortPad("Hora")} | {PadHelper.ShortPad("Tipo")} | {PadHelper.NumberPad("Parcelas")} | {PadHelper.MediumPad("Cliente")} ");


        Console.WriteLine($"{PadHelper.NumberPad("", '_')} | {PadHelper.ShortPad("", '_')} | {PadHelper.ShortPad("", '_')} | {PadHelper.NumberPad("", '_')} | {PadHelper.ShortPad("", '_')} | {PadHelper.ShortPad("", '_')} | {PadHelper.ShortPad("", '_')} | {PadHelper.NumberPad("", '_')} | {PadHelper.MediumPad("", '_')}");

    }

    public override string ToString()
    {
        return $"{PadHelper.NumberPad(Id?.ToString() ?? "")} | {PadHelper.ShortPad(DataVenda?.ToString("yyyy/MM/dd") ?? "")} | {PadHelper.ShortPad(ValorTotal.ToString() ?? "")} | {PadHelper.NumberPad(Desconto.ToString() ?? "")} | {PadHelper.ShortPad(ValorFinal.ToString() ?? "")} | {PadHelper.ShortPad(Hora ?? "")} | {PadHelper.ShortPad(Tipo ?? "")} | {PadHelper.NumberPad(TotalParcelas.ToString() ?? "")} | {PadHelper.MediumPad(Cliente?.Nome ?? "")} ";

    }
}
