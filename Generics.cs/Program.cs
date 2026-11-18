
using System;

public class Program
{
    
   public static void Main(string[] args)
    {
        Loja loja = new Loja();

        
        loja.CarregarProduto();

        while (true)
        {
            Console.WriteLine("\n===== SISTEMA DE ESTOQUE =====");
            Console.WriteLine("1 - Cadastrar produto");
            Console.WriteLine("2 - Remover produto");
            Console.WriteLine("3 - Buscar produto");
            Console.WriteLine("4 - Buscar por categoria");
            Console.WriteLine("5 - Exibir os 3 produtos mais caros");
            Console.WriteLine("0 - Sair");

            Console.Write("\nEscolha uma opção: ");
            string opcao = Console.ReadLine() ?? "";

            switch (opcao)
            {
                case "1":
                    loja.CadastrarProduto();
                    break;

                case "2":
                    Console.Write("Digite o nome do produto que deseja remover: ");
                    string nomeRemover = Console.ReadLine() ?? "";
                    loja.RemoverProduto(nomeRemover);
                    break;

                case "3":
                    Console.Write("Digite o nome do produto que deseja buscar: ");
                    string nomeBuscar = Console.ReadLine() ?? "";
                    loja.Buscarproduto(nomeBuscar);
                    break;

                case "4":
                    Console.Write("Digite a categoria que deseja buscar: ");
                    string categoria = Console.ReadLine() ?? "";
                    loja.BuscarCategoria(categoria);
                    break;

                case "5":
                    loja.ExibirProdutos();
                    break;

                case "0":
                    Console.WriteLine("Encerrando o sistema...");
                    return;

                default:
                    Console.WriteLine("Opção inválida! Tente novamente.");
                    break;
            }
        }
    }
}

   