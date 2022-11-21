using Modelo_ASP_NET.DAO;
using Modelo_ASP_NET.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Modelo_ASP_NET.Livraria
{
    public partial class GerenciamentoLivros : System.Web.UI.Page
    {
        //Criando uma variável de instância de LivrosDAO (para não rpecisar instanciar uma sempre que
        // for usar).
        LivrosDAO ioLivrosDAO = new LivrosDAO();
        TipoLivroDAO ioTipoLivroDAO = new TipoLivroDAO();
        EditoresDAO ioEditoresDAO = new EditoresDAO();

        //Utilizando uma ViewState, como uma porpriedade privada de classe, para armazenar a lista de 
        // Livros cadastrados.
        public BindingList<Livros> ListaLivros
        {
            get
            {
                //Caso a ViewState esteja vazia, chama o método CarregaDados() para preencher os Livros.
                if ((BindingList<Livros>)ViewState["ViewStateListaLivros"] == null)
                    this.CarregaDados();
                //Retorna o conteúdo da ViewState
                return (BindingList<Livros>)ViewState["ViewStateListaLivros"];
            }
            set
            {
                ViewState["ViewStateListaLivros"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //Se o comando de carregamento da página não vier de um dos controles dela, execute o método CarregaDados().
            if (!this.IsPostBack)
                this.CarregaDados();
        }

        private void CarregaDados()
        {
            try
            {
                // Chamando o método BuscaLivros para salvar os Livros cadastrados na ViewState
                this.ListaLivros = this.ioLivrosDAO.BuscaLivros();
                //Indicando onde o GridView deve buscar os valores que serão listados
                this.gvGerenciamentoLivros.DataSource = this.ListaLivros.OrderBy(loLivro => loLivro.liv_nm_titulo);
                //Populando o GridView com os dados de ListaLivros
                this.gvGerenciamentoLivros.DataBind();
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falaha ao tentar recuperar Livros.');</script>");
            }

        }

        protected void BtnNovoLivro_Click(Object sender, EventArgs e)
        {
            try
            {
                decimal ldcIdLivro = this.ListaLivros.OrderByDescending(a => a.liv_id_livro).First().liv_id_livro + 1;           

                string lsTituloLivro = this.tbxCadastroTituiloLivro.Text;
                string lsResumoLivro = this.tbxCadastroResumoLivro.Text;
                decimal ldcPrecoLivro = Convert.ToDecimal(this.tbxCadastroPrecoLivro.Text);
                decimal ldcRoyaltyLivro = Convert.ToDecimal(this.tbxCadastroPrecoLivro.Text);
                int liNuEdicaoLivro = Convert.ToInt16(this.tbxCadastroNuEdicaoLivro.Text);
                string lsNomeEditor = this.tbxCadastroEditorLivro.Text;
                string lsNomeCategoria = this.tbxCadastroCategoriaLivro.Text;

                //Verifica se o idTipoLivro existe. Caso contrário cria               
                decimal ldcIdTipoLivro = Convert.ToDecimal(this.tbxCadastroCategoriaLivro.Text);
                if (ioTipoLivroDAO.BuscaTipoLivro(ldcIdTipoLivro).Count == 0)
                {
                    TipoLivroDAO auxTipoLivroDao = new TipoLivroDAO();
                    auxTipoLivroDao.InsereTipoLivro(new TipoLivro(ldcIdTipoLivro, lsNomeCategoria));
                }
                    

                //Verifica se o idEditorLivro existe. Caso contrário seta como -1                
                decimal ldcIdEditorLivro = Convert.ToDecimal(this.tbxCadastroEditorLivro.Text);
                if (ioEditoresDAO.BuscaEditores(ldcIdEditorLivro).Count == 0)
                {
                    EditoresDAO auxEditoresDao = new EditoresDAO();
                    auxEditoresDao.InsereEditor(new Editores(ldcIdEditorLivro, lsNomeEditor, "E-mail não informado", "url não informada"));
                }

                Livros loLivro = new Livros(ldcIdLivro, ldcIdTipoLivro, ldcIdEditorLivro, lsTituloLivro, ldcPrecoLivro, ldcRoyaltyLivro, lsResumoLivro, liNuEdicaoLivro,
                                            lsNomeEditor, lsNomeCategoria);

                this.ioLivrosDAO.InsereLivro(loLivro);

                LivroAutorDAO aux = new LivroAutorDAO();
                
                //aux.InsereLivroAutor(new LivroAutor())

                this.CarregaDados();
                HttpContext.Current.Response.Write("<script>alert('Livro cadastrado com sucesso!'); </script>");
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Erro no cadastro do Livro.'); </script>");
            }

            this.tbxCadastroCategoriaLivro.Text = String.Empty;
            this.tbxCadastroEditorLivro.Text = String.Empty;
            this.tbxCadastroNuEdicaoLivro.Text = String.Empty;
            this.tbxCadastroPrecoLivro.Text = String.Empty;
            this.tbxCadastroResumoLivro.Text = String.Empty;
            this.tbxCadastroRoyaltyLivro.Text = String.Empty;
            this.tbxCadastroTituiloLivro.Text = String.Empty;
        }

        protected void gvGerenciamentoLivros_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvGerenciamentoLivros.EditIndex = e.NewEditIndex;
            this.CarregaDados();
        }

        protected void gvGerenciamentoLivros_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //O índice -1 indica que nenhuma linha está sendo editada
            this.gvGerenciamentoLivros.EditIndex = -1;
            this.CarregaDados();
        }

        protected void gvGerenciamentoLivros_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            decimal ldcIdLivro = Convert.ToDecimal((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("lblEditIdLivro") as Label).Text);

            decimal ldcIdTipoLivro = Convert.ToDecimal((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("lblEditIdTipoLivro") as Label).Text);
            if (ioTipoLivroDAO.BuscaTipoLivro(ldcIdTipoLivro).Count == 0)
                ldcIdTipoLivro = -1;

            decimal ldcIdEditorLivro = Convert.ToDecimal((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("lblEditIdEditorLivro") as Label).Text);
            if (ioEditoresDAO.BuscaEditores(ldcIdEditorLivro).Count == 0)
                ldcIdEditorLivro = -1;

            string lsTituloLivro = (this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditTituloLivro") as TextBox).Text;
            string lsResumoLivro = (this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditResumoLivro") as TextBox).Text;
            decimal ldcPrecoLivro = Convert.ToDecimal((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditPrecoLivro") as TextBox).Text);
            decimal ldcRoyaltyLivro = Convert.ToDecimal((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditRoyaltyLivro") as TextBox).Text);
            int liNuEdicaoLivro = Convert.ToInt32((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditNuEdicaoLivro") as TextBox).Text);
            string lsNomeEditor = (this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditEditorLivro") as TextBox).Text;
            string lsNomeCategoria = (this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditCatergoriaLivro") as TextBox).Text;

            if (String.IsNullOrWhiteSpace(lsNomeCategoria))
                HttpContext.Current.Response.Write("<script>alert('Digite a categoria do livro.');</script>");
            else if (String.IsNullOrWhiteSpace(lsNomeEditor))
                HttpContext.Current.Response.Write("<script>alert('Digite o editor do livro.');</script>");
            else if (String.IsNullOrWhiteSpace(lsTituloLivro))
                HttpContext.Current.Response.Write("<script>alert('Digite o título do livro.');</script>");
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ldcPrecoLivro)))
                HttpContext.Current.Response.Write("<script>alert('Digite o preço do livro.');</script>");
            else if (String.IsNullOrWhiteSpace(Convert.ToString(ldcRoyaltyLivro)))
                HttpContext.Current.Response.Write("<script>alert('Digite o royalty do livro.');</script>");
            else if (String.IsNullOrWhiteSpace(lsResumoLivro))
                HttpContext.Current.Response.Write("<script>alert('Digite o resumo do livro.');</script>");
            else if (String.IsNullOrWhiteSpace(Convert.ToString(liNuEdicaoLivro)))
                HttpContext.Current.Response.Write("<script>alert('Digite o numero da edição do livro.');</script>");
            else
            {
                try
                {
                    Livros loLivro = new Livros(ldcIdLivro, ldcIdTipoLivro, ldcIdEditorLivro, lsTituloLivro, ldcPrecoLivro, ldcRoyaltyLivro, lsResumoLivro, liNuEdicaoLivro, 
                                                lsNomeEditor, lsNomeCategoria);

                    this.ioLivrosDAO.AtualizaLivro(loLivro);

                    this.gvGerenciamentoLivros.EditIndex = -1;

                    this.CarregaDados();
                    HttpContext.Current.Response.Write("<script>alert('livro atualizado com sucesso!');</script>");
                }
                catch
                {
                    HttpContext.Current.Response.Write("<script>alert('Erro na atualização do cadastro do livro.');</script>");
                }
            }
        }

        protected void gvGerenciamentoLivros_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow loRGridViewRow = this.gvGerenciamentoLivros.Rows[e.RowIndex];
                decimal ldcIdLivro = Convert.ToDecimal((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("lblIdLivro") as Label).Text);
                Livros loLivro = this.ioLivrosDAO.BuscaLivros(ldcIdLivro).FirstOrDefault();
                if (loLivro != null)
                {
                    LivrosDAO loLivrosDAO = new LivrosDAO();

                    this.ioLivrosDAO.RemoveLivro(loLivro);
                    this.CarregaDados();
                }
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Erro na remoção do livro selecionado.');</script>");
            }
        }
    }
}