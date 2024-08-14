using CrudVenda.Control;
using CrudVenda.Entities;


var menuitens = new MenuItem[]
{
    new MenuItem { Name = "test", Description = "Chamaaa", Id = 1 },
    new MenuItem { Name = "test2", Description = "Chamaaa1221aaaaaaaaaaaaa2", Id = 2 },
    new MenuItem { Name = "test3", Description = "999999999999999909999", Id = 3 },
    new MenuItem { Name = "test4", Description = "Chamaaaaaaa", Id = 4 },
    new MenuItem { Name = "test5", Id = 5 }
};

Menu menu = new(menuitens);

var op = menu.GetOption() as MenuItem;

Console.WriteLine(op.Name);



var vendaMenuItens = new Venda[]{
    new Venda{Numero = 1, Quantidade = 23, Valor = 12.42},
    new Venda{Numero = 2, Quantidade = 2, Valor = 12.42},
    new Venda{Numero = 3, Quantidade = 223, Valor = 12.42},
    new Venda{Numero = 4, Quantidade = 234, Valor = 12.42}
};

Console.ReadKey();

var menuVenda = new Menu(vendaMenuItens);


var opVenda = menuVenda.GetOption() as Venda;


System.Console.WriteLine(opVenda);



