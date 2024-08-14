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

            Console.WriteLine($"{(Description.Length > 13 ? Description.Substring(0, 10) + "..." : Description).PadRight(13)} | {Name?.PadRight(10)} ");
            Console.ResetColor();
            return;
        }
        Console.WriteLine($"{(Description.Length > 13 ? Description.Substring(0, 10) + "..." : Description).PadRight(13)} | {Name?.PadRight(10)} ");

        return;
    }

    public virtual void RenderTitle()
    {
        Console.WriteLine("Nome");
    }


}