using System.Runtime.CompilerServices;

namespace AgendaContatosTreinaWeb
{
    public partial class frmAgendaContatos : Form
    {
        private OperacaoEnum acao;
        public frmAgendaContatos()
        {
            InitializeComponent();
        }

        private void frmAgendaContatos_Shown(object sender, EventArgs e)
        {
            AlterarBotoesSalvarECancelar(false);
            AlterarBotoesInserirAlterarExcluir(true);
            CarregarListaContatos();
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
            Contato contato = new Contato()
            {
                Nome = txbNome.Text,
                Email = txbEmail.Text,
                NumeroTelefone = txbTelefone.Text
            };

            List<Contato> contatosList = new List<Contato>();
            foreach (Contato contatoDaLista in lbxContatos.Items)
            {
                contatosList.Add(contatoDaLista);
            }
            if (acao == OperacaoEnum.ALTERAR)
            {
                contatosList.RemoveAt(lbxContatos.SelectedIndex);
                contatosList.Insert(lbxContatos.SelectedIndex, contato);
            }
            else
            {
                contatosList.Add(contato);
            }
            ManipuladorArquvios.EscreverArquivo(contatosList);
            CarregarListaContatos();
            AlterarBotoesInserirAlterarExcluir(true);
            AlterarBotoesSalvarECancelar(false);
            LimparCampos();
            AlterarEstadoCampos(false);
        }

        private void CarregarListaContatos()
        {
            lbxContatos.Items.Clear();
            lbxContatos.Items.AddRange(ManipuladorArquvios.LerArquivo().ToArray());
        }

        private void LimparCampos()
        {
            txbEmail.Text = "";
            txbNome.Text = "";
            txbTelefone.Text = "";
        }

        private void AlterarEstadoCampos (bool estado)
        {
            txbTelefone.Enabled = estado;
            txbNome.Enabled = estado;
            txbEmail.Enabled = estado;
        }

        private void lbxContatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Contato contato = (Contato)lbxContatos.Items[lbxContatos.SelectedIndex];
            txbNome.Text = contato.Nome;
            txbEmail.Text = contato.Email;
            txbTelefone.Text = contato.NumeroTelefone;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza?", "Pergunta", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int indiceExcluido = lbxContatos.SelectedIndex;
                lbxContatos.SelectedIndex = 0;
                lbxContatos.Items.RemoveAt(indiceExcluido);
                List<Contato> contatosList = new List<Contato>();
                foreach (Contato contatoDaLista in lbxContatos.Items)
                {
                    contatosList.Add(contatoDaLista);
                }
                ManipuladorArquvios.EscreverArquivo(contatosList);
                CarregarListaContatos();
                LimparCampos();
            }
        }
    }
}