using ProjetoLivraria.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjetoLivraria.DAO
{
    public class LivrosDAO
    {
        SqlCommand ioQuery;
        //Instanciando o objeto SqlConnection para abrir a conexão com o banco de dados
        SqlConnection ioConexao;

        public BindingList<Livros> BuscaLivros(decimal? liv_id_livro = null)
        {
            //Criando uma lista de Livros que será retornada pela função
            BindingList<Livros> loListLivros = new BindingList<Livros>();

            //Criando conexão com o banco de daods, utilizando as informações que foram preenchidas
            // no Web.config na tag ConnectionStrings e nome ConnectionString
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    //Abrindo conexõa com o servidor
                    ioConexao.Open();

                    if (liv_id_livro != null)
                    {
                        //Montando a query que será executada para retornar o Livro, caso tenha sido passado um ID.
                        ioQuery = new SqlCommand("SELECT * FROM LIV_LIVROS WHERE LIV_ID_LIVRO = @idLivro", ioConexao);

                        //Criando a variável @idLivro e setando o seu valor com o ID recebido por parâmetro pela função
                        ioQuery.Parameters.Add(new SqlParameter("@idLivro", liv_id_livro));
                    }
                    else
                    {
                        //Caso não seja passado nenhum ID, a query deve retornar todos os Livros
                        ioQuery = new SqlCommand("SELECT * FROM LIV_LIVROS", ioConexao);
                    }
                    //Criando o bloco de leitura de dados do SQL server
                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        //Chamando a função de leitura antes de acessar os dados (uma vez para cada linha retornada na consulta)
                        while (loReader.Read())
                        {
                            //Instanciando um objeto do tipo Livro e preenchendo suas propriedades com os valores retornados pela consulta
                            Livros loNovoLivro = new Livros (loReader.GetDecimal(0), loReader.GetDecimal(1), loReader.GetDecimal(2), 
                                loReader.GetString(3), loReader.GetDecimal(4), loReader.GetDecimal(5), loReader.GetString(6), 
                                loReader.GetInt16(7));

                            //Incluindo Livro na lista criada anteriormente
                            loListLivros.Add(loNovoLivro);
                        }
                        //fechando objeto de leitura
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar o(s) livro(s)");
                }
            }
            return loListLivros;
        }

        public int InsereLivro(Livros aoNovoLivro)
        {
            //Caso o Livro não venha preenchido, é lançada uma exceção do tipo NullReferenceException
            if (aoNovoLivro == null)
                throw new NullReferenceException();

            int liQtdRegistrosInseridos = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("INSERT INTO LIV_LIVROS(LIV_ID_LIVRO, LIV_ID_TIPO_LIVRO, LIV_ID_EDITOR, LIV_NM_TITULO," +
                        " LIV_VL_PRECO, LIV_PC_ROYALTY, LIV_DS_RESUMO, LIV_NU_EDICAO) VALUES(@idLivro, @idTipoLivro, @idEditor, " +
                        "@nomeTitulo, @valorPreco, @pcRoyalty, @dsResumo, @edicaoLivro)", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", aoNovoLivro.liv_id_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@idTipoLivro", aoNovoLivro.liv_id_tipo_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", aoNovoLivro.liv_id_editor));
                    ioQuery.Parameters.Add(new SqlParameter("@nomeTitulo", aoNovoLivro.liv_nm_titulo));
                    ioQuery.Parameters.Add(new SqlParameter("@valorPreco", aoNovoLivro.liv_vl_preco));
                    ioQuery.Parameters.Add(new SqlParameter("@pcRoyalty", aoNovoLivro.liv_pc_royalty));
                    ioQuery.Parameters.Add(new SqlParameter("@dsResumo", aoNovoLivro.liv_ds_resumo));
                    ioQuery.Parameters.Add(new SqlParameter("@edicaoLivro", aoNovoLivro.liv_nu_edicao));

                    //Executando o comando Transact-SQL e retornando a quantidade de linhas afetadas.
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw new Exception("Erro ao tentar cadastrar novo Livro");
                }
            }
            return liQtdRegistrosInseridos;
        }

        public int RemoveLivro(Livros aoLivro)
        {
            if (aoLivro == null)
                throw new ArgumentNullException();

            int liQtdRegistrosInseridos = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM LIV_LIVROS WHERE LIV_ID_LIVRO = @idLivro", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", aoLivro.liv_id_livro));

                    //Executando o comando Transact-SQL e retornando a quantidade de linhas afetadas.
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw new Exception("Erro ao tentar excluir Livro");
                }
            }
            return liQtdRegistrosInseridos;
        }

        public int AtualizaLivro(Livros aoLivro)
        {
            if (aoLivro == null)
                throw new ArgumentNullException();

            int liQtdRegistrosInseridos = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("UPDATE LIV_LIVROS SET LIV_ID_TIPO_LIRVRO = @idTipoLivro, LIV_ID_EDITOR = @idEditor, " +
                        "LIV_NM_TITULO = @nomeTitulo, LIV_VL_PRECO = @valorPreco, LIV_PC_ROYALTY = @pcRoyalty, LIV_DS_RESUMO = @dsResumo" +
                        ", LIV_NU_EDICAO = @edicaoLivro WHERE LIV_ID_LIVRO = @idLivro", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", aoLivro.liv_id_livro));

                    //Executando o comando Transact-SQL e retornando a quantidade de linhas afetadas.
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw new Exception("Erro ao tentar atualizar informações do Livro");
                }
            }
            return liQtdRegistrosInseridos;
        }
    }
}