namespace CrudVenda.Control;

class MenuItem : IMenuItem
{
    public bool IsMarked { get; set; } = false;
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Id { get; set; }





    public override string ToString()
    {
        return $"{ShortPad(Id.ToString() ?? "")} | {MediumPad(Description ?? "")} | {ShortPad(Name ?? "")} ";
    }

    public void RenderTitle()
    {
        Console.WriteLine(ShortPad("Id") + " | " + MediumPad("Descrição") + " | " + ShortPad("Nome"));
        Console.WriteLine(ShortPad("", '_') + " | " + MediumPad("", '_') + " | " + ShortPad("", '_'));
    }


    private string MediumPad(string value)
    {
        return (value.Length > 20 ? value[..17] + "..." : value).PadRight(20);
    }

    private string MediumPad(string value, char character)
    {
        return (value.Length > 20 ? value[..17] + "..." : value).PadRight(20, character);
    }



    private string ShortPad(string value)
    {
        return (value.Length > 13 ? value[..10] + "..." : value).PadRight(13);
    }

    private string ShortPad(string value, char character)
    {
        return (value.Length > 13 ? value[..10] + "..." : value).PadRight(13, character);
    }


}