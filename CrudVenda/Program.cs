using CrudVenda.Control;


var menuitens = new MenuItem[]
{
    new MenuItem { Name = "test", Description = "Chamaaa" },
    new MenuItem { Name = "test2", Description = "Chamaaa1221aaaaaaaaaaaaa2" },
    new MenuItem { Name = "test3", Description = "Chamaaa1111" },
    new MenuItem { Name = "test4", Description = "Chamaaaaaaa" },
    new MenuItem { Name = "test5", Description = "Chamaaa" }
};

Menu menu = new(menuitens);

var op = menu.GetOption();


