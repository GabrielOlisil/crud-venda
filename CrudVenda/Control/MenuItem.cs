using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudVenda.Control;

class MenuItem
{
    public bool IsMarked { get; set; } = false;
    public string? Name { get; set; }
    public string? Description { get; set; }




    public virtual void SelfRender()
    {
        Console.ResetColor();
        if (IsMarked)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            Console.WriteLine($"{MediumPad(Description)} | {ShortPad(Name)} ");
            Console.ResetColor();
            return;
        }
        Console.WriteLine($"{MediumPad(Description)} | {ShortPad(Name)} ");

        return;
    }

    public virtual void RenderTitle()
    {
        Console.WriteLine(MediumPad("Descrição") + " | " + ShortPad("Nome"));
        Console.WriteLine(MediumPad("", '_') + " | " + ShortPad("", '_'));
    }


    private static string MediumPad(string value)
    {
        return (value.Length > 20 ? value.Substring(0, 17) + "..." : value).PadRight(20);
    }

    private static string MediumPad(string value, char character)
    {
        return (value.Length > 20 ? value.Substring(0, 17) + "..." : value).PadRight(20, character);
    }



    private static string ShortPad(string value)
    {
        return (value.Length > 13 ? value.Substring(0, 10) + "..." : value).PadRight(13);
    }

    private static string ShortPad(string value, char character)
    {
        return (value.Length > 13 ? value.Substring(0, 10) + "..." : value).PadRight(13, character);
    }


}