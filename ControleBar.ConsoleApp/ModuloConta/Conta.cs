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
    public class Conta : EntidadeBase
    {
        private double _valorTotal;
        private DateTime _diaConta;
        bool _aberta;
        private List<Produto> _listaProdutos;
        MesaFisica _mesaFisica;
        Garcom _garça;
        public double ValorTotal { get => _valorTotal; }
        public bool Aberta { get => _aberta;  }
        public DateTime DiaConta { get => _diaConta; set => _diaConta = value; }

        public override string ToString()
        {
            AtualizarValorConta();
            string aux = (Aberta) ? "Sim" : "Nao";
            return $"Id Conta; {_id}\nDia da conta: {DiaConta.Date}\nEsta aberta: {aux}\nValor Total: {ValorTotal}\nGarçom Responsavel: {_garça.Nome}" +
                $"\nMesa da conta: {_mesaFisica.Nome}";
        }
        public Conta(MesaFisica mesa, Garcom garça, List<Produto> listaProdutos = null)
        {
            _mesaFisica = mesa;
            _garça = garça;
            if (listaProdutos != null) _listaProdutos = listaProdutos;
            else _listaProdutos = new List<Produto>();
            _valorTotal = 0;
            _aberta = true;
            DiaConta = DateTime.Now;
        }

        private void AtualizarValorConta()
        {
            double valorTotal = 0;
            foreach (Produto prod in _listaProdutos)
            {
                valorTotal += prod.Preco;
            }
            _valorTotal = valorTotal;
        }
        public void AdicionarProdutoLista(Produto prod)
        {
            _listaProdutos.Add(prod);
            AtualizarValorConta();
        }
        public void RemoverProdutoLista(int id)
        {
            _listaProdutos.RemoveAt(id);
            AtualizarValorConta();
        }
        public void MostrarProdutos()
        {
            foreach( Produto prod in _listaProdutos)
            {
                Console.WriteLine(prod.ToString());
                Console.WriteLine("-------------------------");
            }
        }
        public bool FecharConta()
        {
            if (Aberta)
            {
                AtualizarValorConta();
                _aberta = false;
                _garça.ReceberGorjeta(ValorTotal * 0.1);
                return true;
            }
            return false;
        }

    }
}
