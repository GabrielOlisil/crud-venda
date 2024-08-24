using CrudVenda.Control;
using CrudVenda.Entities;
using CrudVenda.Conection;
using CrudVenda.Dao;

Console.Clear();

//teste



//teste
Console.WriteLine("Realizar venda");
Console.WriteLine("Insira o valor da venda");
var valor = Convert.ToDouble(Console.ReadLine());
Console.WriteLine("Insira o desconto");
var desconto = Convert.ToDouble(Console.ReadLine());

Console.WriteLine("Informe o tipo");
var tipo = Console.ReadLine();

var clientes = ClienteDAO.List();

Console.WriteLine("Deseja incluir o cliente??");

var optionCli = Console.ReadLine();

Cliente? cliente = null;

if (optionCli != "n")
{
    var menu = new Menu(clientes.ToArray(), "Selecione o Cliente");
    cliente = menu.GetOption() as Cliente;
}




var venda = new Venda
{
    Cliente = cliente,
    ValorTotal = valor,
    Desconto = desconto,
    Tipo = tipo,
    DataVenda = DateTime.Now,
    Hora = DateTime.Now.ToString("HH:mm:ss")
};

VendaDAO.Insert(venda);

Console.ReadKey();

Console.Clear();

var vendas = VendaDAO.List();

if (vendas is not null)
{
    vendas[0].RenderTitle();

    foreach (var itens in vendas)
    {
        Console.WriteLine(itens);
    }
}




// var cliente = ClienteDAO.Read(1);


// var venda = new Venda
// {
//     DataVenda = new DateTime(2024, 12, 13),
//     Hora = "12:00",
//     ValorTotal = 200,
//     Desconto = 0,
//     Tipo = "Sei la po",
//     Cliente = cliente
// };

// VendaDAO.Insert(venda);



// var list = VendaDAO.List();


// list[0].RenderTitle();

// foreach (var item in list)
// {
//     Console.WriteLine(item);
// }