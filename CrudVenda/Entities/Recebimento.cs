using System;

namespace CrudVenda.Entities;

public class Recebimento
{
    public bool IsMarked { get; set; } = false;

    public int? Id { get; set; }
    public double? Valor { get; set; }

    public DateTime? DataVencimento { get; set; }

    public DateTime? DataPagamento { get; set; }

    public string? Status { get; set; }

    public Venda? Venda { get; set; }

    public override string ToString()
    {
        return $"{Id} | {Valor} | {DataVencimento} | {DataPagamento} | {Status}";
    }
}
