using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto1CSharpInt
{
    public class Produto
    {
        public string Nome { get; set; }
        public string Qtd { get; set; }
        public string Preco { get; set; }
        public Produto(string nome, string preco, string qtd)
        {
            Nome = nome;
            Preco = preco;
            Qtd = qtd;   
        }

    }
}
