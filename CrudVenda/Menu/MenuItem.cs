using CrudVenda.Helpers;

namespace CrudVenda.Menu;



class MenuItem : IMenuItem
{
    public bool IsMarked { get; set; } = false;
    public string? Name { get; set; }
    public int Id { get; set; }


    public override string ToString()
    {
        return $"{PadHelper.ShortPad(Id.ToString() ?? "")} | {PadHelper.MediumPad(Name ?? "")} ";
    }

    public void RenderTitle()
    {
        Console.WriteLine(PadHelper.ShortPad("Numero") + " | " + PadHelper.MediumPad("Opção"));
        Console.WriteLine(PadHelper.ShortPad("", '_') + " | " + PadHelper.MediumPad("", '_'));
    }

}