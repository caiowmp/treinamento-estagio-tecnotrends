using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace terceiraQuestao
{
    public class Produto
    {
        public string Nome { get; set; }
        public double Preco { get; set; }
        public int Quantidade { get; set; }

        public Produto (string nome = "", double preco = 0, int quantidade = 0)
        {
            Nome = nome;
            Preco = preco;
            Quantidade = quantidade;
        }

        public override string ToString()
        {
            return String.Format("Produto: {0} Preço: {1} Qtd: {2}", Nome, Preco, Quantidade);
        }
    }
}
