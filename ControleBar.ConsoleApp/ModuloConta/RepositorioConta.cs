using ControleBar.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleBar.ConsoleApp.ModuloMesa
{
    public class RepositorioConta : RepositorioBase<Conta>
    {
        internal List<Conta> SelecionarContasFechadas()
        {
            List<Conta> contas = SelecionarTodos();
            List<Conta> novaLista = new List<Conta>();

            foreach(Conta cont in contas)
            {
                if (!cont.Aberta) novaLista.Add(cont);
            }
            return novaLista;
        }

        internal List<Conta> SelecionarContasAbertas()
        {
            List<Conta> contas = SelecionarTodos();
            List<Conta> novaLista = new List<Conta>();

            foreach (Conta cont in contas)
            {
                if (cont.Aberta) novaLista.Add(cont);
            }
            return novaLista;
        }

        internal double SelecionarTotalDia(DateTime dataFiltro)
        {
            List<Conta> contas = SelecionarContasFechadas();
            double total = 0;

            foreach (Conta cont in contas)
            {
                if (cont.DiaConta.Date == dataFiltro.Date) total += cont.ValorTotal;
            }
            return total;
        }
    }
}
