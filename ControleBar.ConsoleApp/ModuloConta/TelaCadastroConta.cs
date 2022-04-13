using ControleBar.ConsoleApp.Compartilhado;
using ControleBar.ConsoleApp.ModuloGarcom;
using ControleBar.ConsoleApp.ModuloMesaFisica;
using ControleBar.ConsoleApp.ModuloProduto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleBar.ConsoleApp.ModuloMesa
{
    internal class TelaCadastroConta : TelaBase, ITelaCadastravel
    {
        private readonly RepositorioConta _repositorioConta;
        IRepositorio<MesaFisica> _repositorioMesaFisica;
        IRepositorio<Garcom> _repositorioGarcom;
        IRepositorio<Produto> _repositorioProduto;

        TelaCadastroMesaFisica _telaMesaFisica;
        TelaCadastroGarcom _telaGarcom;
        TelaCadastroProduto _telaProduto;
        private readonly Notificador _notificador;

        public TelaCadastroConta(RepositorioConta repositorioConta, IRepositorio<Garcom> repoGarca, IRepositorio<MesaFisica> repoMesa,
            Notificador notificador, TelaCadastroGarcom telaGarca, TelaCadastroMesaFisica telaMesaFisica, TelaCadastroProduto telaProd, IRepositorio<Produto> repoPro) : base("Cadastro de Contas")
        {
            _repositorioConta = repositorioConta;
            _telaMesaFisica = telaMesaFisica;
            _repositorioGarcom = repoGarca;
            _repositorioMesaFisica = repoMesa;
            _telaGarcom = telaGarca;
            _telaProduto = telaProd;
            _repositorioProduto = repoPro;
            _notificador = notificador;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Mesa");

            Conta novaMesa = ObterConta();

            _repositorioConta.Inserir(novaMesa);

            _notificador.ApresentarMensagem("Mesa cadastrada com sucesso!", TipoMensagem.Sucesso);
        }
        public void Editar()
        {
            int opcao = PegarOpcaoEdicao();
            
            MostrarTitulo("Editando Mesa");

            bool temRegistrosCadastrados = VisualizarRegistros("Pesquisando");

            if (temRegistrosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhuma conta cadastrada para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroGenero = ObterNumeroRegistro();

            Conta conta = _repositorioConta.SelecionarRegistro(numeroGenero);
            

            if(opcao == 1)
            {
                Console.WriteLine("Mostando os produtos cadastrados: ");
                _telaProduto.VisualizarRegistros("");
                while (true)
                {
                    Console.WriteLine("Informe o Id do produto a adicionar: ");
                    int idProduto = Convert.ToInt32(Console.ReadLine());
                    Produto prod = _repositorioProduto.SelecionarRegistro(idProduto);
                    Console.WriteLine("Informe o numero de produtos : ");
                    int numeroProd = Convert.ToInt32(Console.ReadLine());

                    for(int x =0; x<numeroProd; x++)
                    {
                        conta.AdicionarProdutoLista(prod);
                    }

                    Console.WriteLine();
                    Console.WriteLine("Deseja inserir mais produtos na conta? [s]");
                    if (Console.ReadLine().ToLower() == "s") continue;
                    else break;
                }
                
                
            }else if(opcao == 2)
            {
                Console.WriteLine("mostrando os produtos na conta: ");

                conta.MostrarProdutos();

                Console.WriteLine("Informe a posicao do produto a remover: ");
                int index = Convert.ToInt32(Console.ReadLine());
                
                conta.RemoverProdutoLista(index);

            }else if(opcao == 3)
            {
                conta.FecharConta();
            }else if(opcao == 4)
            {
                _repositorioConta.Excluir(conta._id);
            }

            bool conseguiuEditar = _repositorioConta.Editar(numeroGenero, conta);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Garçom editado com sucesso!", TipoMensagem.Sucesso);
        }
        private int PegarOpcaoVisualizacao()
        {
            MostrarTitulo("Pegando opcao Visualizacao");
             
            int opcao = 0;
            Console.WriteLine("Para Visualizar total do dia [1]");
            Console.WriteLine("Para Visualizar as contas em aberto [2]");
            Console.WriteLine("Para Visualizar as contas fechadas [3]");
            Console.WriteLine("Para Visualizar as todas [4]");

            opcao = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            return opcao;
        }
        private int PegarOpcaoEdicao()
        {
            MostrarTitulo("Pegando opcao edicao");

            int opcao = 0;
            Console.WriteLine("Para Adicionar produtos na conta [1]");
            Console.WriteLine("Para remover produtos na conta [2]");
            Console.WriteLine("Para Fechar conta [3]");
            Console.WriteLine("Para Remover conta [4]");
            opcao = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            return opcao;
        }
        public void Excluir()
        {
            MostrarTitulo("Excluindo Conta");

            bool temMesasDeClientes = VisualizarRegistros("Pesquisando");

            if (temMesasDeClientes == false)
            {
                _notificador.ApresentarMensagem("Nenhuma conta cadastrada para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroMesa = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioConta.Excluir(numeroMesa);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("conta excluída com sucesso!", TipoMensagem.Sucesso);
        }
        public bool VisualizarRegistros(string tipoVisualizacao)
        {

            if (tipoVisualizacao == "Tela")
            {
                int opcaoVisualizacao = PegarOpcaoVisualizacao();
                Console.Clear();

                switch (opcaoVisualizacao)
                {
                    case 1:
                        Console.WriteLine("");
                        Console.Write("Digite a data do Dia: ");
                        DateTime data = Convert.ToDateTime(Console.ReadLine());
                        double total = _repositorioConta.SelecionarTotalDia(data);
                        break;
                    case 2:
                        ApresentarContasTela(_repositorioConta.SelecionarContasAbertas());
                        break;
                    case 3:
                        ApresentarContasTela(_repositorioConta.SelecionarContasFechadas());
                        break;
                    case 4:
                        ApresentarContasTela(_repositorioConta.SelecionarTodos());
                        break;
                    default:
                        return false;
                }
                return true;

                
            }
            else
            {
                MostrarTitulo("Visualização de contas Cadastradas");
                ApresentarContasTela(_repositorioConta.SelecionarTodos());
                return true;
            }

                
        }

        private bool ApresentarContasTela(List<Conta> contas)
        {
            if (contas.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhuma conta disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Conta mesa in contas)
                Console.WriteLine(mesa.ToString());

            Console.ReadLine();

            return true;
        }

        private Conta ObterConta()
        {
            MostrarTitulo("Obtendo dados cadastrar conta");
            Console.WriteLine("Selecione o garçom");
            _telaGarcom.VisualizarRegistros("");
            int idGarca = _telaGarcom.ObterNumeroRegistro();

            Console.WriteLine("Selecione a Mesa");
            _telaMesaFisica.VisualizarRegistros("");
            int idMesa = _telaMesaFisica.ObterNumeroRegistro();

            return new Conta(_repositorioMesaFisica.SelecionarRegistro(idMesa), _repositorioGarcom.SelecionarRegistro(idGarca));
        }
        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID da conta que deseja selecionar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioConta.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID da Conta não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}
