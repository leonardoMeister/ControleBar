using ControleBar.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleBar.ConsoleApp.ModuloMesaFisica
{
    public class MesaFisica: EntidadeBase
    {
        string _nome;
        bool _ocupada;

        public string Nome { get => _nome; set => _nome = value; }

        public MesaFisica(string nome)
        {
            Nome = nome;
            _ocupada = false;
        }
        public override string ToString()
        {
            string aux = (_ocupada) ? "sim" : "Nao";
            return $"Id Mesa: {_id}\nNome : {Nome}\n Esta ocupada: {aux}";
        }
        public void desocuparMesa()
        {
            _ocupada = false;
        }
        public bool LocarMesa()
        {
            if (!_ocupada)
            {
                _ocupada = true;
                return true;
            }
            return false;
        }
    }
}
