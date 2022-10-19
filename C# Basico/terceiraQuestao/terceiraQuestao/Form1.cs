using System.Runtime.CompilerServices;
using terceiraQuestao;

namespace terceiraQuestao
{
    public partial class frmProdutos : Form
    {
        private OperacaoEnum acao;
        public frmProdutos()
        {
            InitializeComponent();
        }
        private void frmProdutos_Shown(object sender, EventArgs e)
        {
            AlterarBotoesSalvarECancelar(false);
            AlterarBotoesInserirAlterarExcluir(true);
            CarregarListaProdutos();
            AlterarEstadoCampos(false);
        }

        private void AlterarBotoesSalvarECancelar(bool estado)
        {
            btnSalvar.Enabled = estado;
            btnCancelar.Enabled = estado;
        }

        private void AlterarBotoesInserirAlterarExcluir(bool estado)
        {
            btnAlterar.Enabled = estado;
            btnInserir.Enabled = estado;
            btnExcluir.Enabled = estado;
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            AlterarBotoesSalvarECancelar(true);
            AlterarBotoesInserirAlterarExcluir(false);
            AlterarEstadoCampos(true);
            acao = OperacaoEnum.INSERIR;
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            AlterarBotoesSalvarECancelar(true);
            AlterarBotoesInserirAlterarExcluir(false);
            AlterarEstadoCampos(true);
            acao = OperacaoEnum.ALTERAR;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            AlterarBotoesSalvarECancelar(false);
            AlterarBotoesInserirAlterarExcluir(true);
            AlterarEstadoCampos(false);
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Produto produto = new Produto()
            {
                Nome = txbNome.Text,
                Preco = double.Parse(txbPreco.Text),
                Quantidade = int.Parse(txbQtd.Text)
            };

            List<Produto> produtosList = new List<Produto>();
            foreach (Produto produtoDaLista in lbxProdutos.Items)
            {
                produtosList.Add(produtoDaLista);
            }
            if (acao == OperacaoEnum.ALTERAR)
            {
                produtosList.RemoveAt(lbxProdutos.SelectedIndex);
                produtosList.Insert(lbxProdutos.SelectedIndex, produto);
            }
            else
            {
                produtosList.Add(produto);
            }
            ManipuladorArquvios.EscreverArquivo(produtosList);
            CarregarListaProdutos();
            AlterarBotoesInserirAlterarExcluir(true);
            AlterarBotoesSalvarECancelar(false);
            LimparCampos();
            AlterarEstadoCampos(false);
        }

        private void CarregarListaProdutos()
        {
            lbxProdutos.Items.Clear();
            lbxProdutos.Items.AddRange(ManipuladorArquvios.LerArquivo().ToArray());
        }

        private void LimparCampos()
        {
            txbPreco.Text = "";
            txbNome.Text = "";
            txbQtd.Text = "";
        }

        private void AlterarEstadoCampos(bool estado)
        {
            txbQtd.Enabled = estado;
            txbNome.Enabled = estado;
            txbPreco.Enabled = estado;
        }

        private void lbxProdutos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Produto produto = (Produto)lbxProdutos.Items[lbxProdutos.SelectedIndex];
            txbNome.Text = produto.Nome;
            txbPreco.Text = Convert.ToString(produto.Preco);
            txbQtd.Text = Convert.ToString(produto.Quantidade);
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza?", "Pergunta", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int indiceExcluido = lbxProdutos.SelectedIndex;
                lbxProdutos.SelectedIndex = 0;
                lbxProdutos.Items.RemoveAt(indiceExcluido);
                List<Produto> produtosList = new List<Produto>();
                foreach (Produto produtoDaLista in lbxProdutos.Items)
                {
                    produtosList.Add(produtoDaLista);
                }
                ManipuladorArquvios.EscreverArquivo(produtosList);
                CarregarListaProdutos();
                LimparCampos();
            }
        }
    }
}