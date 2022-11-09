using ProjetoLivraria.DAO;
using ProjetoLivraria.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjetoLivraria.Livraria
{
    public partial class GerenciamentoAutores : System.Web.UI.Page
    {
        //Criando uma variável de instância de AutoresDAO (para não rpecisar instanciar uma sempre que
        // for usar).
        AutoresDAO ioAutoresDAO = new AutoresDAO();

        //Utilizando uma ViewState, como uma porpriedade privada de classe, para armazenar a lista de 
        // autores cadastrados.
        public BindingList<Autores> ListaAutores
        {
            get
            {
                //Caso a ViewState esteja vazia, chama o método CarregaDados() para preencher os autores.
                if ((BindingList<Autores>)ViewState["ViewStateListaAutores"] == null)
                    this.CarregaDados();
                //Retorna o conteúdo da ViewState
                return (BindingList<Autores>)ViewState["ViewStateListaAutores"];
            }
            set
            {
                ViewState["ViewStateListaAutores"] = value;
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
                // Chamando o método BuscaAutores para salvar os autores cadastrados na ViewState
                this.ListaAutores = this.ioAutoresDAO.BuscaAutores();
                //Indicando onde o GridView deve buscar os valores que serão listados
                this.gvGerenciamentoAutores.DataSource = this.ListaAutores.OrderBy(loAutor => loAutor.aut_nm_nome);
                //Populando o GridView com os dados de ListaAutores
                this.gvGerenciamentoAutores.DataBind();
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falaha ao tentar recuperar Autores.');</script>");
            }
            
        }

        // Criando o método BtnNovoAutor_Click descrito na propriedade OnClick deste botão no arquivo .aspx
        protected void BtnNovoAutor_Click(Object sender, EventArgs e)
        {
            try
            {
                //Utilizando Linq para obter maior ID de autores cadastrados e incrementando o valor em 1
                // para garantir que a chave primária não se repita (esse campo não é auto-increment no
                // banco).
                decimal ldcIdAutor = this.ListaAutores.OrderByDescending(a => a.aut_id_autor).First().aut_id_autor + 1;
                
                // Salavando os valores que o uduário preencheu em cada campo do formulário (utilizando o
                // "this.NomeDoControle" é possível recuperar o controle e acessar suas propriedades,
                // isso é possível pois todo controle ASP tem um ID único na página e deve ser marcado como
                // runat="server" parar virar um "ServerControl" e ser acessível aqui no "CodeBehind" da
                // página).
                string lsNomeAutor = this.txbCadastroNomeAutor.Text;
                string lsSobrenomeAutor = this.txbCadastroSobrenomeAutor.Text;
                string lsEmailAutor = this.txbCadastroEmailAutor.Text;

                // Instanciando um objeto do tipo Autores para seer adicionado (perceba que só existe um
                // construtor para essa classe onde devem ser passados todos os valores, fizemos isso como
                // mais uma forma de garantir que não será possível cadastrar autores com informações fal-
                // tando, mesmo que o banco permita isso - além dos RequiredFieldValidator).
                Autores loAutor = new Autores(ldcIdAutor, lsNomeAutor, lsSobrenomeAutor, lsEmailAutor);

                // Chamando o método inserir o novo autor na base de dados.
                this.ioAutoresDAO.InsereAutor(loAutor);

                // Atualizando a ViewState com o novo autor recém-inserido
                this.CarregaDados();
                HttpContext.Current.Response.Write("<script>alert('Autor cadastrado com sucesso!'); </script>");
            }
            catch{
                HttpContext.Current.Response.Write("<script>alert('Erro no cadastro do Autor.'); </script>");
            }
            // Limpando campos do formulário.
            this.txbCadastroNomeAutor.Text = String.Empty;
            this.txbCadastroSobrenomeAutor.Text = String.Empty;
            this.txbCadastroEmailAutor.Text = String.Empty;
        }

        protected void gvGerenciamentoAutores_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvGerenciamentoAutores.EditIndex = e.NewEditIndex;
            this.CarregaDados();
        }

        protected void gvGerenciamentoAutores_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //O índice -1 indica que nenhuma linha está sendo editada
            this.gvGerenciamentoAutores.EditIndex = -1;
            this.CarregaDados();
        }

        protected void gvGerenciamentoAutores_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Capturando os valores digitados pelo usuário nos campos de edição do GridView
            // e o ID do usuário que está sendo editado.
            decimal ldcIdAutor = Convert.ToDecimal((this.gvGerenciamentoAutores.Rows[e.RowIndex]
                .FindControl("lblEditIdAutor") as Label).Text);
            string lsNomeAutor = (this.gvGerenciamentoAutores.Rows[e.RowIndex].FindControl("tbxEditNomeAutor")
                as TextBox).Text;
            string lsSobrenomeAutor = (this.gvGerenciamentoAutores.Rows[e.RowIndex]
                .FindControl("tbxEditSobrenomeAutor") as TextBox).Text;
            string lsEmailAutor = (this.gvGerenciamentoAutores.Rows[e.RowIndex]
                .FindControl("tbxEditEmailAutor") as TextBox).Text;

            // Validando se todos os campos estão preenchidos, caso não estejam, é enviado uma mensagem para
            // o usuário.
            if (String.IsNullOrWhiteSpace(lsNomeAutor))
                HttpContext.Current.Response.Write("<script>alert('Digite o nome do autor.');</script>");
            else if (String.IsNullOrWhiteSpace(lsSobrenomeAutor))
                HttpContext.Current.Response.Write("<script>alert('Digite o sobrenome do autor.');</script>");
            else if (String.IsNullOrWhiteSpace(lsEmailAutor))
                HttpContext.Current.Response.Write("<script>alert('Digite o E-mail do autor.');</script>");
            else
            {
                try
                {
                    // Instanciando um objeto do tipo Autor com os dados digitados pelo usuário e o ID do
                    // autor que está sendo editado.
                    Autores loAutor = new Autores(ldcIdAutor, lsNomeAutor, lsSobrenomeAutor, lsEmailAutor);
                    // Chamando o método de atualização do DAO
                    this.ioAutoresDAO.AtualizaAutor(loAutor);
                    // Alterando a pripriedade EditIndex para -1 para indicar que acabou a edição dessa linha
                    this.gvGerenciamentoAutores.EditIndex = -1;
                    // Atualizando a lista de autores na ViewState e os valores do GridView.
                    this.CarregaDados();
                    HttpContext.Current.Response.Write("<script>alert('Autor atualizado com sucesso!');</script>");
                }
                catch
                {
                    HttpContext.Current.Response.Write("<script>alert('Erro na atualização do cadastro do autor.');</script>");
                }
            }
        }
    }
}