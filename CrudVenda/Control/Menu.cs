namespace CrudVenda.Control;

class Menu
{
    private readonly IMenuItem[] _menuItems;
    private int _indexMarked = 0;


    public Menu(IMenuItem[] menuItems)
    {
        _menuItems = menuItems;
        _menuItems[0].IsMarked = true;
    }

    private void Render()
    {
        var first = true;
        Console.Clear();


        foreach (var i in _menuItems)
        {

            if (first)
            {
                i.RenderTitle();
                first = false;
            }

            if (i.IsMarked)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(i);
                Console.ResetColor();
                continue;
            }

            Console.WriteLine(i);
        }
    }

    private IMenuItem? SetItem(ConsoleKeyInfo tecla)
    {

        if (tecla.Key == ConsoleKey.DownArrow)
        {
            if (_indexMarked == _menuItems.Length - 1)
            {
                return null;
            }

            _menuItems[_indexMarked].IsMarked = false;
            _menuItems[++_indexMarked].IsMarked = true;

            Render();
            return null;
        }

        if (tecla.Key == ConsoleKey.UpArrow)
        {

            if (_indexMarked == 0)
            {
                return null;
            }
            _menuItems[_indexMarked].IsMarked = false;
            _menuItems[--_indexMarked].IsMarked = true;
            Render();
            return null;
        }

        if (tecla.Key != ConsoleKey.Enter)
        {
            return null;
        }

        return _menuItems[_indexMarked];
    }

    public IMenuItem GetOption()
    {
        Render();

        var key = Console.ReadKey();
        var escolha = SetItem(key);

        while (escolha is null)
        {
            key = Console.ReadKey();
            escolha = SetItem(key);
        }

        return escolha;
    }
}
