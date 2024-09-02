﻿using CrudVenda.Menu;
using CrudVenda.Entities;
using CrudVenda.Dao;

Console.Clear();

// teste

while (true)
{
    var itensMenuInicio = new MenuItem[]{
        new ()
        {
            Id = 1,
            Name = "Realizar Venda"
        },
        new ()
        {
            Id = 2,
            Name = "Listar Vendas"
        },
        new ()
        {
            Id = 3,
            Name = "Atualizar Venda"
        },
        new ()
        {
            Id = 4,
            Name = "Deletar Venda"
        },
    };

    var menuInicio = new Menu(itensMenuInicio, "Selecione uma Opção");

    var opcao = menuInicio.GetOption() as MenuItem;

    if (opcao is null)
    {
        continue;
    }



    var optionActions = new Dictionary<int, Action>
    {
        { 1, Inserir},
        { 2, Listar },
        { 3, Atualizar},
        { 4, Deletar }
    };


    if (optionActions.ContainsKey(opcao.Id))
    {
        optionActions[opcao.Id]();
    }

    Console.ReadKey();

}


static void Inserir()
{
    int totalParcelas = 0;

    var recebimentos = new List<Recebimento>();

    //teste
    Console.WriteLine("Realizar venda");
    Console.WriteLine("Insira o valor da venda");
    var valor = Convert.ToDouble(Console.ReadLine());
    Console.WriteLine("Insira o desconto");
    var desconto = Convert.ToDouble(Console.ReadLine());

    Console.WriteLine("Informe o tipo");
    var tipo = Console.ReadLine();

    Console.WriteLine("Informe A quantidade de parcelas");
    totalParcelas = int.Parse(Console.ReadLine());

    if (totalParcelas == 1)
    {
        var recebimento = new Recebimento()
        {
            DataPagamento = DateTime.Today,
            DataVencimento = DateTime.Today,
            Status = "fechado",
            Valor = valor,
        };

        recebimentos.Add(recebimento);
    }
    else
    {
        for (int i = 0; i < totalParcelas; i++)
        {
            var recebimento = new Recebimento()
            {
                DataPagamento = DateTime.Today,
                DataVencimento = DateTime.Today.AddDays(30),
                Status = "aberto",
                Valor = valor / totalParcelas,
            };

            recebimentos.Add(recebimento);
        }
    }


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
        TotalParcelas = totalParcelas,
        Hora = DateTime.Now.ToString("HH:mm:ss"),
        Recebimentos = recebimentos
    };

    VendaDAO.Insert(venda);

    Console.ReadKey();
}

static void Listar()
{
    var vendas = VendaDAO.List();

    if (vendas is not null && vendas.Count > 0)
    {
        vendas[0].RenderTitle();

        foreach (var itens in vendas)
        {
            Console.WriteLine(itens);
        }
    }
}

static void Atualizar()
{

}

static void Deletar()
{

}







#region fluxo principal
int totalParcelas = 0;

var recebimentos = new List<Recebimento>();

//teste
Console.WriteLine("Realizar venda");
Console.WriteLine("Insira o valor da venda");
var valor = Convert.ToDouble(Console.ReadLine());
Console.WriteLine("Insira o desconto");
var desconto = Convert.ToDouble(Console.ReadLine());

Console.WriteLine("Informe o tipo");
var tipo = Console.ReadLine();

Console.WriteLine("Informe A quantidade de parcelas");
totalParcelas = int.Parse(Console.ReadLine());

if (totalParcelas == 1)
{
    var recebimento = new Recebimento()
    {
        DataPagamento = DateTime.Today,
        DataVencimento = DateTime.Today,
        Status = "fechado",
        Valor = valor,
    };

    recebimentos.Add(recebimento);
}
else
{
    for (int i = 0; i < totalParcelas; i++)
    {
        var recebimento = new Recebimento()
        {
            DataPagamento = DateTime.Today,
            DataVencimento = DateTime.Today.AddDays(30),
            Status = "aberto",
            Valor = valor / totalParcelas,
        };

        recebimentos.Add(recebimento);
    }
}


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
    TotalParcelas = totalParcelas,
    Hora = DateTime.Now.ToString("HH:mm:ss"),
    Recebimentos = recebimentos
};

VendaDAO.Insert(venda);

Console.ReadKey();

Console.Clear();




#endregion



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



