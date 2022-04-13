using ControleBar.ConsoleApp.ModuloGarcom;
using ControleBar.ConsoleApp.ModuloMesa;
using ControleBar.ConsoleApp.ModuloMesaFisica;
using ControleBar.ConsoleApp.ModuloProduto;
using System;

namespace ControleBar.ConsoleApp.Compartilhado
{
    public class TelaMenuPrincipal
    {
        private readonly IRepositorio<Garcom> repositorioGarcom;
        private readonly IRepositorio<Produto> repositorioProduto;
        private readonly RepositorioConta repositorioConta;
        private readonly IRepositorio<MesaFisica> repositorioMesaFisica;

        private readonly TelaCadastroGarcom telaCadastroGarcom;
        private readonly TelaCadastroProduto telaCadastroProduto;
        private readonly TelaCadastroConta telaCadastroConta;
        private readonly TelaCadastroMesaFisica telaCadastroMesaFisica;



        public TelaMenuPrincipal(Notificador notificador)
        {
            repositorioGarcom = new RepositorioGarcom();
            repositorioProduto = new RepositorioProduto();
            repositorioConta = new RepositorioConta();
            repositorioMesaFisica = new RepositorioMesaFisica();

            telaCadastroGarcom = new TelaCadastroGarcom(repositorioGarcom, notificador);
            telaCadastroProduto = new TelaCadastroProduto(repositorioProduto, notificador);
            telaCadastroMesaFisica = new TelaCadastroMesaFisica(repositorioMesaFisica, notificador);
            telaCadastroConta = new TelaCadastroConta(repositorioConta,repositorioGarcom,repositorioMesaFisica,notificador,telaCadastroGarcom,telaCadastroMesaFisica, telaCadastroProduto, repositorioProduto);
            
            PopularAplicacao();
        }

        public string MostrarOpcoes()
        {
            Console.Clear();

            Console.WriteLine("Controle de Mesas de Bar 1.0");

            Console.WriteLine();

            Console.WriteLine("Digite 1 para Gerenciar Garçons");
            Console.WriteLine("Digite 2 para Gerenciar Mesas fisicas");
            Console.WriteLine("Digite 3 para Gerenciar produtos");
            Console.WriteLine("Digite 4 para Gerenciar Contas de mesas de clientes");

            Console.WriteLine("Digite s para sair");

            string opcaoSelecionada = Console.ReadLine();

            return opcaoSelecionada;
        }

        public TelaBase ObterTela()
        {
            string opcao = MostrarOpcoes();

            TelaBase tela = null;

            if (opcao == "1")
                tela = telaCadastroGarcom;

            else if (opcao == "2")
                tela = telaCadastroMesaFisica;

            else if (opcao == "3")
                tela = telaCadastroProduto;

            else if (opcao == "4")
                tela = telaCadastroConta;

            else if (opcao == "5")
                tela = null;

            return tela;
        }

        private void PopularAplicacao()
        {
            var garcom = new Garcom("Julinho", "230.232.519-98");
            var mesa = new MesaFisica("Mesa 1");
            var produto = new Produto("X zao", 20,100);
            var produto2 = new Produto("Breja", 7, 1000);

            repositorioProduto.Inserir(produto);
            repositorioProduto.Inserir(produto2);
            repositorioMesaFisica.Inserir(mesa);
            repositorioGarcom.Inserir(garcom);

        }
    }
}
