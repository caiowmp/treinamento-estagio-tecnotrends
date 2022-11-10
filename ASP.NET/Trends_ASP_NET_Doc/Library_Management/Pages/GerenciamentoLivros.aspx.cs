using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library_Management.Models;
using Library_Management.DAO;
using System.ComponentModel;

namespace Library_Management.Pages
{
    public partial class GerenciamentoLivros : System.Web.UI.Page
    {

        LivrosDAO livDAO = new LivrosDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.CarregarDados();
            }
        }

        public BindingList<Livros> ListaLivros
        {
            get
            {
                if ((BindingList<Livros>)ViewState["ViewStateListaLivros"] == null)
                    this.CarregarDados();
                return (BindingList<Livros>)ViewState["ViewStateListaLivros"];
            }
            set
            {
                ViewState["ViewStateListaLivros"] = value;
            }
        }

        private void CarregarDados()
        {
            this.ListaLivros = this.livDAO.SelectLivros();

            this.gvGerenciamentoLivros.DataSource = this.ListaLivros.OrderBy(Livro => Livro.liv_nm_titulo);

            this.gvGerenciamentoLivros.DataBind();
        }

        protected void btnCadastrarLivro_Click(object sender, EventArgs e)
        {
            try
            {
                decimal idLivro = this.ListaLivros.OrderByDescending(Livro => Livro.liv_id_livro).First().liv_id_livro + 1;

                string tituloLivro = tbxCadastroTituloLivro.Text;
                string resumoLivro = tbxCadastroResumoLivro.Text;
                int edicaoLivro = Convert.ToInt32(tbxCadastroEdicaoLivro.Text);
                decimal precoLivro = Convert.ToDecimal(tbxCadastroPrecoLivro.Text);
                decimal royaltyLivro = Convert.ToDecimal(tbxCadastroRoyaltyLivro.Text);
                decimal idTipoLivro = Convert.ToDecimal(tbxCadastroIdTipoLivro.Text);
                decimal idEditorLivro = Convert.ToDecimal(tbxCadastroIdEditorLivro.Text);

                Livros newLivro = new Livros(idLivro, idTipoLivro, idEditorLivro, tituloLivro, precoLivro, royaltyLivro, resumoLivro, edicaoLivro);

                this.livDAO.InsertLivro(newLivro);
                this.CarregarDados();
                HttpContext.Current.Response.Write(
                    "<script>" +
                    "alert('Livro cadastrado com sucesso!')" +
                    "</script>"
                    );

            }
            catch
            {
                HttpContext.Current.Response.Write(
                    "<script>" +
                    "alert('Erro no cadastro!')" +
                    "</script>"
                    );
            }

        }

        protected void gvGerenciamentoLivros_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvGerenciamentoLivros.EditIndex = e.NewEditIndex;
            this.CarregarDados();
        }

        protected void gvGerenciamentoLivros_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvGerenciamentoLivros.EditIndex = -1;
            this.CarregarDados();
        }

        protected void gvGerenciamentoLivros_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            decimal idLivro = Convert.ToDecimal((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("lblEditIdLivro") as Label).Text);
            string tituloLivro = (this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditTituloLivro") as TextBox).Text;
            string resumoLivro = (this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditResumoLivro") as TextBox).Text;
            int edicaoLivro = Convert.ToInt32((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditEdicaoLivro") as TextBox).Text);
            decimal precoLivro = Convert.ToDecimal((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditPrecoLivro") as TextBox).Text);
            decimal royaltyLivro = Convert.ToDecimal((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditRoyaltyLivro") as TextBox).Text);
            decimal idTipoLivro = Convert.ToDecimal((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditIdTipoLivro") as TextBox).Text);
            decimal idEditorLivro = Convert.ToDecimal((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditIdEditorLivro") as TextBox).Text);

            if (tituloLivro == "" || resumoLivro == "" || edicaoLivro == 0 || precoLivro == 0 || royaltyLivro == 0 || idTipoLivro == 0 || idEditorLivro == 0)
            {
                HttpContext.Current.Response.Write(
                    "<script>" +
                    "alert('Preencha todos os campos!')" +
                    "</script>"
                    );
            }
            else
            {
                try
                {
                    Livros newLivro = new Livros(idLivro, idTipoLivro, idEditorLivro, tituloLivro, precoLivro, royaltyLivro, resumoLivro, edicaoLivro);

                    this.livDAO.UpdateLivro(newLivro);
                    this.gvGerenciamentoLivros.EditIndex = -1;
                    this.CarregarDados();
                    HttpContext.Current.Response.Write(
                        "<script>" +
                        "alert('Livro atualizado com sucesso!')" +
                        "</script>"
                        );
                }
                catch
                {
                    HttpContext.Current.Response.Write(
                        "<script>" +
                        "alert('Erro na atualização!')" +
                        "</script>"
                        );
                }
            }

        }

        protected void gvGerenciamentoLivros_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                decimal idLivro = Convert.ToDecimal((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("lblIdLivro") as Label).Text);

                this.livDAO.DeleteLivro(idLivro);
                this.CarregarDados();
                HttpContext.Current.Response.Write(
                    "<script>" +
                    "alert('Livro excluído com sucesso!')" +
                    "</script>"
                    );

            } catch
            {
                HttpContext.Current.Response.Write(
                    "<script>" +
                    "alert('Erro na exclusão!')" +
                    "</script>"
                    );
            }
        }

    }
}