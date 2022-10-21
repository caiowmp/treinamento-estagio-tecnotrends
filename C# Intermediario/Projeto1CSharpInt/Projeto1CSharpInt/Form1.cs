using System.Xml;

namespace Projeto1CSharpInt
{
    public partial class frmProdutos : Form
    {
        private OperacaoEnum acao;
        public frmProdutos()
        {
            InitializeComponent();
        }

        private void frmProdutos_Load(object sender, EventArgs e)
        {
            lblTitulo.Text = CarregarTitulo();
            CarregarProdutos();
            AlterarEstadoCampos(false);
            AlterarBotoesInserirAlterarExcluir(true);
            AlterarBotoesSalvarECancelar(false);
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
            atributoQtd.Value = produto.Qtd;

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
            AlterarBotoesSalvarECancelar(true);
            AlterarBotoesInserirAlterarExcluir(false);
            AlterarEstadoCampos(true);
            acao = OperacaoEnum.ALTERAR;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AlterarEstadoCampos(true);
            AlterarBotoesSalvarECancelar(true);
            AlterarBotoesInserirAlterarExcluir(false);
            acao = OperacaoEnum.ADD;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (acao == OperacaoEnum.ADD)
            {
                string nome = txbNome.Text;
                string preco = txbPreco.Text;
                string qtd = txbQtd.Text;

                Produto produto = new Produto(nome, preco, qtd);
                CriarProduto(produto);
            }
            else if (acao == OperacaoEnum.ALTERAR)
            {
                XmlDocument documentoXml = new XmlDocument();
                documentoXml.Load(@"C:\Users\55719\Documents\Trends\C# Intermediario\Projeto1CSharpInt\Projeto1CSharpInt\TabelaProdutos.xml");
                XmlNodeList produtos = documentoXml.SelectNodes("/tabela/produtos/produto");
                XmlNode produto = produtos[lbxProdutos.SelectedIndex];

                XmlAttribute atributoNome = documentoXml.CreateAttribute("nome");

                XmlAttribute atributoPreco = documentoXml.CreateAttribute("preco");
                //atributoPreco.Value = produto.Preco;

                XmlAttribute atributoQtd = documentoXml.CreateAttribute("qtd");
                //atributoQtd.Value = produto.Qtd;

                produto.Attributes.Remove(atributoNome);
                atributoNome.Value = txbNome.Text;
                produto.Attributes.Append(atributoNome);

                produto.Attributes.Remove(atributoPreco);
                atributoPreco.Value = txbPreco.Text;
                produto.Attributes.Append(atributoPreco);

                produto.Attributes.Remove(atributoQtd);
                atributoQtd.Value = txbQtd.Text;  
                produto.Attributes.Append(atributoQtd);

                documentoXml.Save(@"C:\Users\55719\Documents\Trends\C# Intermediario\Projeto1CSharpInt\Projeto1CSharpInt\TabelaProdutos.xml");
            }
            else if (acao == OperacaoEnum.EXCLUIR)
            {
                LimparCampos();
            }
            lbxProdutos.Items.Clear();
            CarregarProdutos();
            AlterarBotoesInserirAlterarExcluir(true);
            AlterarBotoesSalvarECancelar(false);
            LimparCampos();
            AlterarEstadoCampos(false);
        }

        private void AlterarEstadoCampos(bool estado)
        {
            txbNome.Enabled = estado;
            txbPreco.Enabled = estado;
            txbQtd.Enabled = estado;
        }

        private void AlterarBotoesSalvarECancelar(bool estado)
        {
            btnSalvar.Enabled = estado;
            btnCancelar.Enabled = estado;
        }

        private void AlterarBotoesInserirAlterarExcluir(bool estado)
        {
            btnAlterar.Enabled = estado;
            btnAdd.Enabled = estado;
            btnExcluir.Enabled = estado;
        }

        private void lbxProdutos_SelectedIndexChanged(object sender, EventArgs e)
        {
            XmlDocument documentoXml = new XmlDocument();
            documentoXml.Load(@"C:\Users\55719\Documents\Trends\C# Intermediario\Projeto1CSharpInt\Projeto1CSharpInt\TabelaProdutos.xml");
            XmlNodeList produtos = documentoXml.SelectNodes("/tabela/produtos/produto");
            XmlNode produto = produtos[lbxProdutos.SelectedIndex];
            txbNome.Text = produto.Attributes["nome"].Value;
            txbPreco.Text = produto.Attributes["preco"].Value;
            txbQtd.Text = produto.Attributes["qtd"].Value;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            AlterarBotoesSalvarECancelar(false);
            AlterarBotoesInserirAlterarExcluir(true);
            AlterarEstadoCampos(false);
        }
        private void LimparCampos()
        {
            txbPreco.Text = "";
            txbNome.Text = "";
            txbQtd.Text = "";
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza?", "Pergunta", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                acao = OperacaoEnum.EXCLUIR;
                XmlDocument documentoXml = new XmlDocument();
                documentoXml.Load(@"C:\Users\55719\Documents\Trends\C# Intermediario\Projeto1CSharpInt\Projeto1CSharpInt\TabelaProdutos.xml");
                XmlNodeList produtos = documentoXml.SelectNodes("/tabela/produtos/produto");
                XmlNode produto = produtos[lbxProdutos.SelectedIndex];

                //documentoXml.RemoveAll();
                foreach (XmlNode produtoLista in produtos)
                {
                    if (produtoLista == produto)
                    {
                        produtoLista.ParentNode.RemoveChild(produtoLista);
                    }
                }

                documentoXml.Save(@"C:\Users\55719\Documents\Trends\C# Intermediario\Projeto1CSharpInt\Projeto1CSharpInt\TabelaProdutos.xml");
                lbxProdutos.Items.Clear();
                CarregarProdutos();
                LimparCampos();
                AlterarEstadoCampos(false);
            }
        }
    }
}