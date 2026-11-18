public class Loja
{
    
    List<Produto> produtos = new List<Produto>();

    public void CadastrarProduto()
    {

        Console.WriteLine("Nome do produto: ");
        string nome = Console.ReadLine()!;
        
        double preco = 0;
        while(true)
        {
            try
            {
               Console.WriteLine("Preço: ");
               double preco = Console.ReadLine()!;
               if(preco < 0)
               {
                Console.WriteLine("Erro, digite o preco novamente: ");
                continue;

               }
               break;
            }
            catch(FormatException)
            {
                Console.WriteLine("Digite o valor corretamente!");
            }  
        }

        int qtdEstoque = 0;
       while(true)
        {
            try
            {
                Console.WriteLine("Quantidade no estoque: ");
                int qtdEstoque = Console.ReadLine()!;
                if(qtdEstoque <= 0)
                {
                    Console.WriteLine("Erro, digite a quantidade no estoque novamente: ");
                    continue;
                }
                break;
            }
            catch(FormatException)
            {
                Console.WriteLine("Digite o valor corretamente: ");
            }
        }

        onsole.WriteLine(" ==== CATEGORIA ====");
        Console.WriteLine("1 - Bebidas");
        Console.WriteLine("2 - Alimentos");
        Console.WriteLine("3 - Hortifruti");
        Console.WriteLine("4 - Laticínios");
        Console.WriteLine("5 - Carnes");
        Console.WriteLine("6 - Padaria");
        Console.WriteLine("7 - Congelados");
        Console.WriteLine("8 - Limpeza");
        Console.WriteLine("9 - Higiene Pessoal");
        Console.WriteLine("10 - Doces e Snacks");
        Console.WriteLine("11 - Pet Shops");
        Console.WriteLine("12 - Enlatados e Conservas");

        string opcao = Console.ReadLine() ?? "";

        string categoria = opcao switch
        {
            "1" => "Bebidas",
            "2" => "Alimentos",
            "3" => "Hortifruti",
            "4" => "Laticínios",
            "5" => "Carnes",
            "6" => "Padaria",
            "7" => "Congelados",
            "8" => "Limpeza",
            "9" => "Higiene Pessoal",
            "10" => "Doces e Snacks",
            "11" => "Pet Shops",
            "12" => "Enlatados e Conservas",
            _ => ""
        };

        if(categoria == "")
        {
          Console.WriteLine("Categoria indisponivel");  
          return;
        }

        Produto produto = new Produto(nome, preco, categoria, qtdEstoque);
        AdicionarProduto(produto);

        SalvarProduto();
        
    }

     public void AdicionarProduto(Produto produto)
    {
        produtos.Add(produto);
    }

    private void SalvarProduto()
    {

        try
        {
            if(string.IsNullOrWhiteSpace(caminhoArquivo))
            {
                Console.WriteLine("Digite o caminho do arquivo para salvar: ");
                caminhoArquivo = Console.ReadLine() ?? "";
                if(string.IsNullOrWhiteSpace(caminhoArquivo))
                {
                    Console.WriteLine("Caminho invalido!");
                    caminhoArquivo = "";
                    return;
                }
            }

            string conteudo = string.Join(Environment.NewLine, produtos.Select(
                produto => $"{produto.nome};{produto.preco};{produto.categoria};{produto.qtdEstoque}"
            ));

            File.WriteAllText(caminhoArquivo,conteudo);
            Console.WriteLine("Produto salvo com sucesso!");
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Não foi possivel salvar o produto{ex.Message}");
        }
    }

   public void CarregarProduto()
{
    if (!File.Exists(caminhoArquivo))
    {
        Console.WriteLine("O arquivo não existe, nenhum produto foi salvo!");
        return;
    }

    string[] linhas = File.ReadAllLines(caminhoArquivo);

    foreach (string linha in linhas)
    {
        if (string.IsNullOrWhiteSpace(linha))
            continue;

        string[] partes = linha.Split(';');

        if (partes.Length != 4)
        {
            Console.WriteLine("Linha inválida no arquivo: " + linha);
            continue;
        }

        string nome = partes[0];

        if (!float.TryParse(partes[1], out float preco))
        {
            Console.WriteLine("Preço inválido: " + partes[1]);
            continue;
        }

        string categoria = partes[2];

        if (!int.TryParse(partes[3], out int qtdEstoque))
        {
            Console.WriteLine("Quantidade inválida: " + partes[3]);
            continue;
        }

        Produto produto = new Produto(nome, preco, categoria, qtdEstoque);

        AdicionarProduto(produto);
    }

    Console.WriteLine("Produtos carregados com sucesso!");
}

    public void RemoverProduto(string nome)
    {
        var ProdutoRemovido = produtos.FirstOrDefault(produto => produto.nome == nome);
        if(ProdutoRemovido != null)
        {
            Console.WriteLine("Produto removido: " + ProdutoRemovido.nome);
            Console.WriteLine("Preço: " + ProdutoRemovido.preco);
            Console.WriteLine("Categoria: " + ProdutoRemovido.categoria);
            produtos.Remove(ProdutoRemovido);
        }
        else
        {
            Console.WriteLine("produto n/ encontrado");
        }

        SalvarProduto();
    }

    public void Buscarproduto(string nome)
    {
        var ProdutoEncontrado = produtos.FirstOrDefault(produto => produto.nome == nome);
        if(ProdutoEncontrado != null)
        {
            Console.WriteLine("Produto encontrado: " + ProdutoEncontrado.nome);
            Console.WriteLine("Preço: " + ProdutoEncontrado.preco);
            Console.WriteLine("Quantidade no estoque: " + ProdutoEncontrado.qtdEstoque);
            Console.WriteLine("Categoria: " + ProdutoEncontrado.categoria);
        }
        else
        {
            Console.WriteLine("Produto n/ encontrado");
        }
    }

    public void BuscarCategoria(string categoria)
    {
        var CategoriaEncontrada = produtos.Where(produto => produto.categoria == categoria);
        if(produtos.Any(produto => produto.categoria == categoria))
        {
            foreach(Produto produto in CategoriaEncontrada) 
            {
                Console.WriteLine("Categoria encontrada: " + produto.categoria);
                Console.WriteLine("Nome: " + produto.nome);
            }
        }
        else
        {
            Console.WriteLine("categoria n/ encontrada");
        }
    }

    public void ExibirProdutos()
    {
        var ProdutoOrganizado = produtos.OrderByDescending(
        produto => produto.preco).Take(3);

        Console.WriteLine("Exibir produtos mais caros");


        foreach(Produto produto in ProdutoOrganizado)
        {
            Console.WriteLine("\nNome: " + produto.nome);
            Console.WriteLine("Preço: " + produto.preco);
            Console.WriteLine("Quantidade no estoque: " + produto.qtdEstoque);
            Console.WriteLine("Categoria: " + produto.categoria);
        }
    }
    
}