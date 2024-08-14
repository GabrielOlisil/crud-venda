using CrudVenda.Control;


var menuitens = new MenuItem[]
{
    new MenuItem { Name = "test" },
    new MenuItem { Name = "test2" },
    new MenuItem { Name = "test3" },
    new MenuItem { Name = "test4" },
    new MenuItem { Name = "test5" }
};

Menu menu = new Menu(menuitens);

var op = menu.GetOption();


Console.WriteLine("Voce escolheu esta opção:" + op.Name);
