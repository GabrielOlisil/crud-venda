using System;
using CrudVenda.Menu;
using static CrudVenda.Helpers.PadHelper;

namespace CrudVenda.Entities;

public class Recebimento : IMenuItem
{
    public bool IsMarked { get; set; } = false;

    public ulong? Id { get; set; }
    public double? Valor { get; set; }

    public DateTime? DataVencimento { get; set; }

    public DateTime? DataPagamento { get; set; }

    public string? Status { get; set; }

    public Venda? Venda { get; set; }

    public void RenderTitle()
    {
        Console.WriteLine($"{NumberPad("Id")} | {NumberPad("Valor")} | {ShortPad("Data Vencimento")} | {ShortPad("Data Pagamneto")} | {ShortPad("Status")}");
        Console.WriteLine($"{NumberPad("", '_')} | {NumberPad("", '_')} | {ShortPad("", '_')} | {ShortPad("", '_')} | {ShortPad("", '_')}");

    }

    public override string ToString()
    {
        return $"{NumberPad(Id.ToString() ?? "")} | {NumberPad(Valor.ToString() ?? "")} | {ShortPad(DataVencimento?.ToString("dd/MM/yyyy") ?? "")} | {ShortPad(DataPagamento?.ToString("dd/MM/yyyy") ?? "")} | {ShortPad(Status ?? "")}";
    }
}
