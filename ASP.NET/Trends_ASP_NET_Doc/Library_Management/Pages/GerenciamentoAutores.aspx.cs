using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library_Management.Models;
using Library_Management.DAO;
using System.ComponentModel;
using System.Drawing;

namespace Library_Management.Pages
{
    public partial class GerenciamentoAutores : System.Web.UI.Page
    {
        AutoresDAO autDAO = new AutoresDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.CarregarDados();
            }
        }

        public BindingList<Autores> ListaAutores
        {
            get
            {
                if ((BindingList<Autores>)ViewState["ViewStateListaAutores"] == null)
                    this.CarregarDados();
                return (BindingList<Autores>)ViewState["ViewStateListaAutores"];
            }
            set
            {
                ViewState["ViewStateListaAutores"] = value;
            }
        }

        private void CarregarDados()
        {
            this.ListaAutores = this.autDAO.SelectAutores();

            this.gvGerenciamentoAutores.DataSource = this.ListaAutores.OrderBy(Autor => Autor.aut_nm_name);

            this.gvGerenciamentoAutores.DataBind();
        }

        protected void BtnCadastrarAutor_Click(object sender, EventArgs e)
        {
            try
            {


                decimal ldcAutor = this.ListaAutores.OrderByDescending(a => a.aut_id_autor).First().aut_id_autor + 1;


                string lsNomeAutor = this.tbxCadastroNomeAutor.Text;
                string lsSobrenomeAutor = this.tbxCadastroSobrenomeAutor.Text;
                string lsEmailAutor = this.tbxCadastroEmailAutor.Text;

                Autores newAutor = new Autores(ldcAutor, lsNomeAutor, lsSobrenomeAutor, lsEmailAutor);

                this.autDAO.InsertAutor(newAutor);
                this.CarregarDados();
                HttpContext.Current.Response.Write(
                    "<script>" +
                    "alert('Autor cadastrado com sucesso!')" +
                    "</script>"
                    );
            } catch
            {
                HttpContext.Current.Response.Write(
                    "<script>" +
                    "alert('Erro no cadastro!')" +
                    "</script>"
                    );
            }
        }

        protected void gvGerenciamentoAutores_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvGerenciamentoAutores.EditIndex = e.NewEditIndex;
            this.CarregarDados();
        }

        protected void gvGerenciamentoAutores_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvGerenciamentoAutores.EditIndex = -1;
            this.CarregarDados();
        }

        protected void gvGerenciamentoAutores_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            
                decimal ldcAutor = Convert.ToDecimal((this.gvGerenciamentoAutores.Rows[e.RowIndex].FindControl("lblEditIdAutor") as Label).Text);
                string lsNomeAutor = ((TextBox)this.gvGerenciamentoAutores.Rows[e.RowIndex].FindControl("tbxEditNomeAutor")).Text;
                string lsSobrenomeAutor = ((TextBox)this.gvGerenciamentoAutores.Rows[e.RowIndex].FindControl("tbxEditSobrenomeAutor")).Text;
                string lsEmailAutor = ((TextBox)this.gvGerenciamentoAutores.Rows[e.RowIndex].FindControl("tbxEditEmailAutor")).Text;

                if (lsNomeAutor == "" || lsSobrenomeAutor == "" || lsEmailAutor == "")
                {
                    HttpContext.Current.Response.Write(
                        "<script>" +
                        "alert('Preencha todos os campos!')" +
                        "</script>"
                        );
                }
                else{
            try
            {
                Autores newAutor = new Autores(ldcAutor, lsNomeAutor, lsSobrenomeAutor, lsEmailAutor);

                this.autDAO.UpdateAutor(newAutor);
                this.gvGerenciamentoAutores.EditIndex = -1;
                this.CarregarDados();
                HttpContext.Current.Response.Write(
                    "<script>" +
                    "alert('Autor atualizado com sucesso!')" +
                    "</script>"
                    );
            } catch
            {
                HttpContext.Current.Response.Write(
                    "<script>" +
                    "alert('Erro na atualização!')" +
                    "</script>"
                    );
            }
        }
        }

        
        protected void gvGerenciamentoAutores_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                decimal ldcAutor = Convert.ToDecimal((this.gvGerenciamentoAutores.Rows[e.RowIndex].FindControl("lblIdAutor") as Label).Text);

                //if (findLivrosByAutor(ldcAutor).Count != 0) new Exception("Autor possui livros cadastrados!");
                
                this.autDAO.DeleteAutor(ldcAutor);
                this.CarregarDados();
                HttpContext.Current.Response.Write(
                    "<script>" +
                    "alert('Autor excluído com sucesso!')" +
                    "</script>"
                    );

                this.autDAO.DeleteAutor(ldcAutor);
                this.CarregarDados();
                HttpContext.Current.Response.Write(
                    "<script>" +
                    "alert('Autor excluído com sucesso!')" +
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