using ControleBar.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleBar.ConsoleApp.ModuloProduto
{
    public class Produto: EntidadeBase
    {
        string _nome;
        double _preco;
        int _quantidade;

        public string Nome { get => _nome;  }
        public double Preco { get => _preco;}
        public int Quantidade { get => _quantidade; }

        public Produto(string nome, double preco, int quantidade)
        {
            this._nome = nome;
            this._preco = preco;
            this._quantidade = quantidade;
        }
        public void AtualizarQuantiade(int quantidade)
        {
            _quantidade = quantidade;
        }
        public override string ToString()
        {
            return "Id: " + _id + Environment.NewLine +
                "Nome do produto: " + Nome + Environment.NewLine +
                "Preço do produto: R$" + Preco + Environment.NewLine +
                "Quantidade do produto: R$" + Quantidade + Environment.NewLine;
        }
    }
}
