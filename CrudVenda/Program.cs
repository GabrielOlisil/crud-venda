using CrudVenda.Menu;
using CrudVenda.Entities;
using CrudVenda.Dao;
using System.ComponentModel.Design;





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

    var valorFinal = valor - valor * (desconto / 100);

    Console.WriteLine("Informe o tipo");
    var tipo = Console.ReadLine();


    System.Console.WriteLine("A venda será a vista {1} ou a prazo? {2}");
    var prazo = int.Parse(Console.ReadLine());

    var status = prazo == 1 ? "fechado" : "aberto";



    if (prazo != 1)
    {
        Console.WriteLine("Informe A quantidade de parcelas");
        totalParcelas = int.Parse(Console.ReadLine());
        for (int i = 0; i < totalParcelas; i++)
        {
            var recebimento = new Recebimento()
            {
                DataPagamento = DateTime.Today,
                DataVencimento = DateTime.Today.AddDays(30),
                Status = status,
                Valor = valor / totalParcelas,
            };

            recebimentos.Add(recebimento);
        }
    }
    else
    {
        var recebimento = new Recebimento()
        {
            DataPagamento = DateTime.Today,
            DataVencimento = DateTime.Today,
            Status = status,
            Valor = valor,
        };

        recebimentos.Add(recebimento);
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
        ValorFinal = valorFinal,
        Tipo = tipo,
        DataVenda = DateTime.Today,
        TotalParcelas = totalParcelas,
        Hora = DateTime.Now.ToString("HH:mm:ss"),
        Recebimentos = recebimentos
    };

    VendaDAO.Insert(venda);
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
    var vendas = VendaDAO.List();

    var menuVendas = new Menu(vendas.ToArray(), "Selecione a venda que deseja Atualizar");

    var option = menuVendas.GetOption() as Venda;

    if (option is null)
    {
        Console.WriteLine("A venda não pode ser encontrada");
        return;
    }

    Console.WriteLine("Insira o valor da venda");
    var valor = Convert.ToDouble(Console.ReadLine());
    Console.WriteLine("Insira o desconto");
    var desconto = Convert.ToDouble(Console.ReadLine());

    var valorFinal = valor - valor * (desconto / 100);

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

    Console.WriteLine("Informe A quantidade de parcelas");
    var totalParcelas = int.Parse(Console.ReadLine());

    option.Cliente = cliente;
    option.DataVenda = DateTime.Today;
    option.Hora = DateTime.Now.ToString("HH:mm:ss");
    option.Desconto = desconto;
    option.Tipo = tipo;
    option.ValorTotal = valor;
    option.ValorFinal = valorFinal;
    option.TotalParcelas = totalParcelas;

    VendaDAO.Update(option);


}

static void Deletar()
{
    var vendas = VendaDAO.List();

    var menuVendas = new Menu(vendas.ToArray(), "Selecione a venda que deseja Deletar");

    var option = menuVendas.GetOption() as Venda;

    if (option is not null)
    {
        VendaDAO.Delete(option);
    }


}

