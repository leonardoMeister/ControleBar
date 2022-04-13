using ControleBar.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleBar.ConsoleApp.ModuloMesaFisica
{
    internal class TelaCadastroMesaFisica : TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<MesaFisica> _repositorioMesa;
        private readonly Notificador _notificador;
        public TelaCadastroMesaFisica(IRepositorio<MesaFisica> repositorioMesa, Notificador notificador) : base("Cadastro de mesa fisica")
        {
            _repositorioMesa = repositorioMesa;
            _notificador = notificador;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Mesa fisica");

            MesaFisica mesaF = ObterMesa();

            _repositorioMesa.Inserir(mesaF);

            _notificador.ApresentarMensagem("Mesa fisica cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Mesa fisica");

            bool temRegistrosCadastrados = VisualizarRegistros("Pesquisando");

            if (temRegistrosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum Mesa cadastrada para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroGenero = ObterNumeroRegistro();

            MesaFisica mesaF = ObterMesa();

            bool conseguiuEditar = _repositorioMesa.Editar(numeroGenero, mesaF);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Mesa editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Mesa fisica");

            bool temMesa = VisualizarRegistros("Pesquisando");

            if (temMesa == false)
            {
                _notificador.ApresentarMensagem("Nenhuma mesa cadastrada para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numMesa = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioMesa.Excluir(numMesa);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Mesa fisica excluída com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Mesas Cadastradas");

            List<MesaFisica> mesasF = _repositorioMesa.SelecionarTodos();

            if (mesasF.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum Mesa fisica disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (MesaFisica mesa in mesasF)
                Console.WriteLine(mesa.ToString());

            Console.ReadLine();

            return true;
        }

        private MesaFisica ObterMesa()
        {
            Console.Write("Digite o nome do garçom: ");
            string nome = Console.ReadLine();

            return new MesaFisica(nome);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID da mesa fisica que deseja selecionar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioMesa.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID da mesa não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}
