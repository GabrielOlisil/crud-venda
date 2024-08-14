using CrudVenda.Helpers;

namespace CrudVenda.Control;



class MenuItem : IMenuItem
{
    public bool IsMarked { get; set; } = false;
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Id { get; set; }


    public override string ToString()
    {
        return $"{PadHelper.ShortPad(Id.ToString() ?? "")} | {PadHelper.MediumPad(Description ?? "")} | {PadHelper.ShortPad(Name ?? "")} ";
    }

    public void RenderTitle()
    {
        Console.WriteLine(PadHelper.ShortPad("Id") + " | " + PadHelper.MediumPad("Descrição") + " | " + PadHelper.ShortPad("Nome"));
        Console.WriteLine(PadHelper.ShortPad("", '_') + " | " + PadHelper.MediumPad("", '_') + " | " + PadHelper.ShortPad("", '_'));
    }

}