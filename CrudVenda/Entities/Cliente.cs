using System;
using CrudVenda.Menu;
using CrudVenda.Helpers;

namespace CrudVenda.Entities;

public class Cliente : IMenuItem
{
    public ulong? Id { get; set; }
    public string? Nome { get; set; }
    public string? Cpf { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public DateTime? DataNascimento { get; set; }
    public bool IsMarked { get; set; } = false;



    public override string ToString()
    {
        return $"{PadHelper.ShortPad(Id.ToString())} | {PadHelper.MediumPad(Nome ?? "")} | {PadHelper.MediumPad(Cpf ?? "")} | {PadHelper.MediumPad(Email ?? "")} | {PadHelper.MediumPad(Telefone ?? "")} | {PadHelper.MediumPad(DataNascimento?.ToString("dd-MM-yyyy") ?? "")}";
    }

    public void RenderTitle()
    {
        Console.WriteLine($"{PadHelper.ShortPad("ID")} | {PadHelper.MediumPad("Nome")} | {PadHelper.MediumPad("CPF")} | {PadHelper.MediumPad("Email")} | {PadHelper.MediumPad("telefone")} | {PadHelper.MediumPad("Data de Nascimento")}");

        Console.WriteLine($"{PadHelper.ShortPad("", '_')} | {PadHelper.MediumPad("", '_')} | {PadHelper.MediumPad("", '_')} | {PadHelper.MediumPad("", '_')} | {PadHelper.MediumPad("", '_')} | {PadHelper.MediumPad("", '_')}");
    }
}
