using CrudVenda.Control;
using CrudVenda.Entities;

var vendaMenuItens = new Venda[]{
    new Venda{Numero = 1, Quantidade = 23, Valor = 12.42},
    new Venda{Numero = 2, Quantidade = 2, Valor = 12.42},
    new Venda{Numero = 3, Quantidade = 223, Valor = 12.42},
    new Venda{Numero = 4, Quantidade = 234, Valor = 12.42}
};


var menuVenda = new Menu(vendaMenuItens);


var opVenda = menuVenda.GetOption() as Venda;


System.Console.WriteLine(opVenda);



