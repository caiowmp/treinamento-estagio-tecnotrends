using System.Xml;

namespace Projeto1CSharpInt
{
    public partial class frmProdutos : Form
    {
        public frmProdutos()
        {
            InitializeComponent();
        }

        private void frmProdutos_Load(object sender, EventArgs e)
        {
            //CriarProduto();
            lblTitulo.Text = CarregarTitulo();
            CarregarProdutos();
        }

        private string CarregarTitulo()
        {
            XmlDocument documentoXml = new XmlDocument();
            documentoXml.Load(@"C:\Users\55719\Documents\Trends\C# Intermediario\Projeto1CSharpInt\Projeto1CSharpInt\TabelaProdutos.xml");
            XmlNode noTitulo = documentoXml.SelectSingleNode("/tabela/titulo");
            return noTitulo.InnerText;
        }

        private void CarregarProdutos()
        {
            XmlDocument documentoXml = new XmlDocument();
            documentoXml.Load(@"C:\Users\55719\Documents\Trends\C# Intermediario\Projeto1CSharpInt\Projeto1CSharpInt\TabelaProdutos.xml");
            XmlNodeList produtos = documentoXml.SelectNodes("/tabela/produtos/produto");
            foreach (XmlNode produto in produtos)
            {
                string representacaoProduto = "";
                string nome = produto.Attributes["nome"].Value;
                string preco = produto.Attributes["preco"].Value;
                string qtd = produto.Attributes["qtd"].Value;
                representacaoProduto = nome + " , " + preco + " , " + qtd;
                lbxProdutos.Items.Add(representacaoProduto);
            }
        }

        private void CriarProduto(Produto produto)
        {
            XmlDocument documentoXml = new XmlDocument();
            documentoXml.Load(@"C:\Users\55719\Documents\Trends\C# Intermediario\Projeto1CSharpInt\Projeto1CSharpInt\TabelaProdutos.xml");
            
            XmlAttribute atributoNome = documentoXml.CreateAttribute("nome");
            atributoNome.Value = produto.Nome;

            XmlAttribute atributoPreco = documentoXml.CreateAttribute("preco");
            atributoPreco.Value = produto.Preco;

            XmlAttribute atributoQtd = documentoXml.CreateAttribute("qtd");
            atributoQtd.Value = Convert.ToString(produto.Qtd);

            XmlNode novoProduto = documentoXml.CreateElement("produto");
            novoProduto.Attributes.Append(atributoNome);
            novoProduto.Attributes.Append(atributoPreco);
            novoProduto.Attributes.Append(atributoQtd);

            XmlNode produtos = documentoXml.SelectSingleNode("/tabela/produtos");
            produtos.AppendChild(novoProduto);

            documentoXml.Save(@"C:\Users\55719\Documents\Trends\C# Intermediario\Projeto1CSharpInt\Projeto1CSharpInt\TabelaProdutos.xml");
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string nome = txbNome.Text;
            string preco = txbPreco.Text;
            int qtd = Convert.ToInt32(txbQtd.Text);

            Produto produto = new Produto(nome, preco, qtd);
            CriarProduto(produto);
            lbxProdutos.Items.Clear();
            CarregarProdutos();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {

        }
    }
}