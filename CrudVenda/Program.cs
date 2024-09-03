using CrudVenda.Menu;
using CrudVenda.Entities;
using CrudVenda.Dao;
using System.ComponentModel.Design;
using System.Text;





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
            Name = "Inspecionar Venda"
        },
        new ()
        {
            Id = 3,
            Name = "Listar Vendas"
        },
        new ()
        {
            Id = 4,
            Name = "Atualizar Venda"
        },
        new ()
        {
            Id = 5,
            Name = "Deletar Venda"
        },
        new(){
            Id = 6,
            Name = "Sair"
        },
    };

    var menuInicio = new Menu(itensMenuInicio, "Navegue com as setas e selecione com enter uma opção");

    var opcao = menuInicio.GetOption() as MenuItem;

    if (opcao is null)
    {
        continue;
    }



    var optionActions = new Dictionary<int, Action>
    {
        { 1, Inserir },
        { 2, Inspecionar },
        { 3, Listar },
        { 4, Atualizar },
        { 5, Deletar },
        { 6, () => Environment.Exit(0) }
    };


    if (optionActions.ContainsKey(opcao.Id))
    {
        optionActions[opcao.Id]();
    }

    Console.WriteLine("Pressione qualquer tecla pra continhar");
    Console.ReadKey();

}


static void Inspecionar()
{

    Console.Clear();

    var title = new StringBuilder();

    title.Append("=================");
    title.Append(Environment.NewLine);
    title.Append("Inspecionar Venda");
    title.Append(Environment.NewLine);
    title.Append("=================");
    title.Append(Environment.NewLine);
    title.Append("Selecione a Venda");
    title.Append(Environment.NewLine);


    List<Venda>? vendas = null;
    try
    {
        vendas = VendaDAO.List();

    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro: " + ex.Message);
        return;
    }

    if (vendas.Count == 0)
    {
        Console.WriteLine("Não Existem vendas");
        return;
    }

    var menu = new Menu([.. vendas], title.ToString());


    if (menu.GetOption() is not Venda venda)
    {
        Console.WriteLine("Houve um erro ao selecionar a venda");
        return;
    }

    Console.Clear();

    List<Recebimento>? recebimentos = null;

    try
    {
        recebimentos = RecebimentoDAO.List(venda);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro: " + ex.Message);
    }


    Console.WriteLine("Dados da Venda:" + Environment.NewLine);

    Console.WriteLine($"ID: {venda.Id}");
    Console.WriteLine($"Valor Total: {venda.ValorTotal}");
    Console.WriteLine($"Desconto: {venda.Desconto}");
    Console.WriteLine($"Valor Final: {venda.ValorFinal}");
    Console.WriteLine($"Data: {venda.DataVenda?.ToShortDateString()}");
    Console.WriteLine($"Hora: {venda.Hora}");
    Console.WriteLine($"Total Parcelas: {venda.TotalParcelas}");



    if (recebimentos is not null)
    {
        Console.WriteLine(Environment.NewLine + "Recebimentos");
        recebimentos[0].RenderTitle();


        foreach (var recebimento in recebimentos)
        {
            Console.WriteLine(recebimento);
        }
    }
}


static void Inserir()
{
    Console.Clear();

    Console.WriteLine("==============");
    Console.WriteLine("Realizar Venda");
    Console.WriteLine("==============");

    int totalParcelas = 0;

    var recebimentos = new List<Recebimento>();

    Console.WriteLine("Realizar venda");
    Console.WriteLine("Insira o valor da venda");

    if (!double.TryParse(Console.ReadLine(), out var valor))
    {
        Console.WriteLine("Entrada inválida, abortando...");
        return;
    }



    Console.WriteLine("Insira a porcentagem de desconto");
    if (!double.TryParse(Console.ReadLine(), out var desconto))
    {
        Console.WriteLine("Entrada inválida, abortando...");
        return;
    }

    var valorFinal = valor - valor * (desconto / 100);


    Console.WriteLine("Informe o tipo");


    var tipo = Console.ReadLine();


    Console.WriteLine("A venda será a vista {1} ou a prazo? {2}");
    if (!int.TryParse(Console.ReadLine(), out var prazo))
    {
        Console.WriteLine("Entrada inválida, abortando...");
        return;
    }

    var status = prazo == 1 ? "fechado" : "aberto";



    if (prazo != 1)
    {
        Console.WriteLine("Informe A quantidade de parcelas");
        if (!int.TryParse(Console.ReadLine(), out totalParcelas))
        {
            Console.WriteLine("Entrada inválida, abortando...");
            return;
        }

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



    List<Cliente>? clientes = null;


    Console.WriteLine("Deseja incluir o cliente??");

    var optionCli = Console.ReadLine();

    Cliente? cliente = null;

    if (optionCli != "n")
    {
        try
        {
            clientes = ClienteDAO.List();
            var menu = new Menu(clientes.ToArray(), "Selecione o Cliente");
            cliente = menu.GetOption() as Cliente;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Não foi possível obter os clientes da base de dados: " + ex.Message);
        }
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

    try
    {
        VendaDAO.Insert(venda);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro: " + ex.Message);
    }
}

static void Listar()
{
    Console.Clear();

    Console.WriteLine("============");
    Console.WriteLine("listar Venda");
    Console.WriteLine("============");

    List<Venda>? vendas = null;
    try
    {
        vendas = VendaDAO.List();

    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro: " + ex.Message);
        return;
    }

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

    Console.Clear();

    Console.WriteLine("===============");
    Console.WriteLine("Atualizar Venda");
    Console.WriteLine("===============");

    List<Venda>? vendas = null;

    try
    {
        vendas = VendaDAO.List();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro: " + ex.Message);
        return;
    }

    if (vendas.Count() == 0)
    {
        Console.WriteLine("Não foram encontradas vendas");
        return;
    }
    var menuVendas = new Menu(vendas.ToArray(), "Selecione a venda que deseja Atualizar");

    var option = menuVendas.GetOption() as Venda;

    if (option is null)
    {
        Console.WriteLine("Entrada inválida, abortando...");
        return;
    }


    Console.WriteLine("Insira o valor da venda");
    if (!double.TryParse(Console.ReadLine(), out var valor))
    {
        Console.WriteLine("Entrada inválida, abortando...");
        return;
    }

    Console.WriteLine("Insira o desconto");
    if (!double.TryParse(Console.ReadLine(), out var desconto))
    {
        Console.WriteLine("Entrada inválida, abortando...");
        return;
    }

    var valorFinal = valor - valor * (desconto / 100);

    Console.WriteLine("Informe o tipo");
    var tipo = Console.ReadLine();

    var totalPOarcelas = option.TotalParcelas;
    Console.WriteLine("Informe A quantidade de parcelas");

    if (!int.TryParse(Console.ReadLine(), out var totalParcelas))
    {
        Console.WriteLine("Entrada inválida, abortando...");
        return;
    }


    Console.WriteLine("Deseja incluir o cliente??");

    var optionCli = Console.ReadLine();

    Cliente? cliente = null;

    if (optionCli != "n")
    {
        List<Cliente>? clientes = null;

        try
        {
            clientes = ClienteDAO.List();
            var menu = new Menu(clientes.ToArray(), "Selecione o Cliente");
            cliente = menu.GetOption() as Cliente;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro: " + ex.Message);
        }

    }



    option.Cliente = cliente;
    option.DataVenda = DateTime.Today;
    option.Hora = DateTime.Now.ToString("HH:mm:ss");
    option.Desconto = desconto;
    option.Tipo = tipo;
    option.ValorTotal = valor;
    option.ValorFinal = valorFinal;
    option.TotalParcelas = totalParcelas;

    try
    {

        VendaDAO.Update(option);
        Console.WriteLine("Venda Atualizada com sucesso");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro:" + ex.Message);
    }


}

static void Deletar()
{

    Console.Clear();

    Console.WriteLine("=============");
    Console.WriteLine("Deletar Venda");
    Console.WriteLine("=============");

    List<Venda>? vendas = null;
    Venda? option = null;
    try
    {
        vendas = VendaDAO.List();
        if (vendas.Count() == 0)
        {
            Console.WriteLine("Não foram encontradas vendas");
            return;
        }
        var menuVendas = new Menu(vendas.ToArray(), "Selecione a venda que deseja Deletar");
        option = menuVendas.GetOption() as Venda;
    }
    catch (Exception ex)
    {
        Console.WriteLine("Não foi possível obter as vendas: " + ex.Message);
    }




    if (option is not null)
    {
        try
        {
            VendaDAO.Delete(option);
        }
        catch
        {
            Console.WriteLine("Não foi possível excluir a venda");
        }
    }


}

