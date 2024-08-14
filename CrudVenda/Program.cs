using CrudVenda.Control;


var menuitens = new MenuItem[]
{
    new MenuItem { Name = "test", Description = "Chamaaa", Id = 1 },
    new MenuItem { Name = "test2", Description = "Chamaaa1221aaaaaaaaaaaaa2", Id = 2 },
    new MenuItem { Name = "test3", Description = "999999999999999909999", Id = 3 },
    new MenuItem { Name = "test4", Description = "Chamaaaaaaa", Id = 4 },
    new MenuItem { Name = "test5", Id = 5 }
};

Menu menu = new(menuitens);

var op = menu.GetOption();


