using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace terceiraQuestao
{
    public class ManipuladorArquvios
    {
        private static string EnderecoArquivo = AppDomain.CurrentDomain.BaseDirectory + "Produtos.txt";
        public static List<Produto> LerArquivo()
        {
            List<Produto> ProdutosList = new List<Produto>();

            if (File.Exists(@EnderecoArquivo))
            {
                using (StreamReader sr = File.OpenText(@EnderecoArquivo))
                {
                    while (sr.Peek() >= 0)
                    {
                        string linha = sr.ReadLine();
                        string[] linhaComSplit = linha.Split(';');
                        if (linhaComSplit.Count() == 3)
                        {
                            Produto Produto = new Produto();
                            Produto.Nome = linhaComSplit[0];
                            Produto.Preco = double.Parse(linhaComSplit[1]);
                            Produto.Quantidade = int.Parse(linhaComSplit[2]);
                            ProdutosList.Add(Produto);
                        }
                    }
                }
            }


            return ProdutosList;
        }

        public static void EscreverArquivo(List<Produto> ProdutoList)
        {

            StreamWriter sw = new StreamWriter(@EnderecoArquivo, false);
            foreach (Produto Produto in ProdutoList)
            {
                string linha = string.Format("{0};{1};{2}", Produto.Nome, Produto.Preco, Produto.Quantidade);
                sw.WriteLine(linha);
            }
            //limpa os buffers do sw
            sw.Flush();
            sw.Close();

        }
    }
}
