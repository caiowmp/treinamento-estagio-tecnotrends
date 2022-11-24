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
        LivrosDAO ioLivrosDAO = new LivrosDAO();
        TipoLivroDAO ioTipoLivroDAO = new TipoLivroDAO();
        EditoresDAO ioEditoresDAO = new EditoresDAO();

        public BindingList<Livros> ListaLivros
        {
            get
            {
                if ((BindingList<Livros>)ViewState["ViewStateListaLivros"] == null)
                    this.CarregaDados();

                return (BindingList<Livros>)ViewState["ViewStateListaLivros"];
            }
            set
            {
                ViewState["ViewStateListaLivros"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["SessionAutorSelecionado"] as Autores) != null)
            {
                this.CarregaDadosComAutor((Session["SessionAutorSelecionado"] as Autores));

                Session["SessionAutorSelecionado"] = null;
            }
            else
            {
                if (!this.IsPostBack)
                    this.CarregaDados();
            }  
        }

        private void CarregaDadosComAutor(Autores autor)
        {
            try
            {
                this.ListaLivros = this.ioLivrosDAO.BuscaLivrosAutor(autor);

                this.gvGerenciamentoLivros.DataSource = this.ListaLivros.OrderBy(loLivro => loLivro.liv_nm_titulo);

                this.gvGerenciamentoLivros.DataBind();
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falaha ao tentar recuperar Livros do Autor.');</script>");
            }
        }

        private void CarregaDados()
        {
            try
            {
                this.ListaLivros = this.ioLivrosDAO.BuscaLivros();

                this.gvGerenciamentoLivros.DataSource = this.ListaLivros.OrderBy(loLivro => loLivro.liv_nm_titulo);

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
                
                TipoLivroDAO auxTipoLivroDao = new TipoLivroDAO();

                decimal ldcIdCategoria = auxTipoLivroDao.BuscaTipoLivro().OrderByDescending(a => a.til_id_tipo_livro).First().til_id_tipo_livro + 1;
                if (auxTipoLivroDao.BuscaTipoLivroPorNome(lsNomeCategoria).til_id_tipo_livro == -1)
                {
                    auxTipoLivroDao.InsereTipoLivro(new TipoLivro(ldcIdCategoria, lsNomeCategoria));
                }

                EditoresDAO auxEditoresDao = new EditoresDAO();

                decimal ldcIdEditorLivro = auxEditoresDao.BuscaEditores().OrderByDescending(a => a.edi_id_editores).First().edi_id_editores + 1;
                if (ioEditoresDAO.BuscaEditorNome(lsNomeEditor).edi_id_editores == -1)
                {
                    auxEditoresDao.InsereEditor(new Editores(ldcIdEditorLivro, lsNomeEditor, "E-mail não informado", "url não informada"));
                }

                Livros loLivro = new Livros(ldcIdLivro, ldcIdCategoria, ldcIdEditorLivro, lsTituloLivro, ldcPrecoLivro, ldcRoyaltyLivro, lsResumoLivro, liNuEdicaoLivro,
                                            lsNomeEditor, lsNomeCategoria);

                this.ioLivrosDAO.InsereLivro(loLivro);

                LivroAutorDAO aux = new LivroAutorDAO();

                //aux.InsereLivroAutor(new LivroAutor(ldcIdAutor, ldcIdLivro, ldcRoyaltyLivro));

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

            decimal ldcIdTipoLivro = ioTipoLivroDAO.BuscaTipoLivro().OrderByDescending(a => a.til_id_tipo_livro).First().til_id_tipo_livro + 1;
            string lsNomeCategoria = (this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditCategoriaLivro") as TextBox).Text;
            if (ioTipoLivroDAO.BuscaTipoLivroPorNome(lsNomeCategoria).til_id_tipo_livro != -1)
            {
                ldcIdTipoLivro = ioTipoLivroDAO.BuscaTipoLivroPorNome(lsNomeCategoria).til_id_tipo_livro;
                if (ioTipoLivroDAO.BuscaTipoLivro(ldcIdTipoLivro).Count == 0)
                    ioTipoLivroDAO.InsereTipoLivro(new TipoLivro(ldcIdTipoLivro, lsNomeCategoria));
            }
            else
            {
                ioTipoLivroDAO.InsereTipoLivro(new TipoLivro(ldcIdTipoLivro, lsNomeCategoria)); ;
            }

            decimal ldcIdEditorLivro = ioEditoresDAO.BuscaEditores().OrderByDescending(a => a.edi_id_editores).First().edi_id_editores + 1;
            string lsNomeEditor = (this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditEditorLivro") as TextBox).Text;
            if (ioEditoresDAO.BuscaEditorNome(lsNomeEditor).edi_id_editores != -1)
            {
                ldcIdEditorLivro = ioEditoresDAO.BuscaEditorNome(lsNomeEditor).edi_id_editores;
                if (ioEditoresDAO.BuscaEditores(ldcIdEditorLivro).Count == 0)
                    ioEditoresDAO.InsereEditor(new Editores(ldcIdEditorLivro, lsNomeEditor));
            }
            else
            {
                ioEditoresDAO.InsereEditor(new Editores(ldcIdEditorLivro, lsNomeEditor));
            }
            

            string lsTituloLivro = (this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditTituloLivro") as TextBox).Text;
            string lsResumoLivro = (this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditResumoLivro") as TextBox).Text;
            decimal ldcPrecoLivro = Convert.ToDecimal((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditPrecoLivro") as TextBox).Text);
            decimal ldcRoyaltyLivro = Convert.ToDecimal((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditRoyaltyLivro") as TextBox).Text);
            int liNuEdicaoLivro = Convert.ToInt32((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditNuEdicaoLivro") as TextBox).Text);
            

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
                    HttpContext.Current.Response.Write("<script>alert('Livro removido com sucesso!');</script>");
                }
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Erro na remoção do livro selecionado.');</script>");
            }
        }
    }
}