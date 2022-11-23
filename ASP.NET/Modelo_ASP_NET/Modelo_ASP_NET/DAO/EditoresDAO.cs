using Modelo_ASP_NET.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Modelo_ASP_NET.DAO
{
    public class EditoresDAO
    {
        SqlCommand ioQuery;
        //Instanciando o objeto SqlConnection para abrir a conexão com o banco de dados
        SqlConnection ioConexao;

        public BindingList<Editores> BuscaEditores(decimal? edi_id_editor = null)
        {
            //Criando uma lista de Editores que será retornada pela função
            BindingList<Editores> loListEditores = new BindingList<Editores>();

            //Criando conexão com o banco de daods, utilizando as informações que foram preenchidas
            // no Web.config na tag ConnectionStrings e nome ConnectionString
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    //Abrindo conexõa com o servidor
                    ioConexao.Open();

                    if (edi_id_editor != null)
                    {
                        //Montando a query que será executada para retornar o Editor, caso tenha sido passado um ID.
                        ioQuery = new SqlCommand("SELECT * FROM EDI_EDITORES WHERE EDI_ID_EDITOR = @idEditor", ioConexao);

                        //Criando a variável @idEditor e setando o seu valor com o ID recebido por parâmetro pela função
                        ioQuery.Parameters.Add(new SqlParameter("@idEditor", edi_id_editor));
                    }
                    else
                    {
                        //Caso não seja passado nenhum ID, a query deve retornar todos os Editores
                        ioQuery = new SqlCommand("SELECT * FROM EDI_EDITORES", ioConexao);
                    }
                    //Criando o bloco de leitura de dados do SQL server
                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        //Chamando a função de leitura antes de acessar os dados (uma vez para cada linha retornada na consulta)
                        while (loReader.Read())
                        {
                            //Instanciando um objeto do tipo Editor e preenchendo suas propriedades com os valores retornados pela consulta
                            Editores loNovoEditor = new Editores(loReader.GetDecimal(0), loReader.GetString(1), loReader.GetString(2), loReader.GetString(3));

                            //Incluindo Editor na lista criada anteriormente
                            loListEditores.Add(loNovoEditor);
                        }
                        //fechando objeto de leitura
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar o(s) editor(es)");
                }
            }
            return loListEditores;
        }

        public int InsereEditor(Editores aoNovoEditor)
        {
            //Caso o Editor não venha preenchido, é lançada uma exceção do tipo NullReferenceException
            if (aoNovoEditor == null)
                throw new NullReferenceException();

            int liQtdRegistrosInseridos = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("INSERT INTO EDI_EDITORES(EDI_ID_EDITOR, EDI_NM_EDITOR, EDI_DS_EMAIL, EDI_DS_URL) VALUES(@idEditor, @nomeEditor, @emailEditor, @urlEditor)", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", aoNovoEditor.edi_id_editores));
                    ioQuery.Parameters.Add(new SqlParameter("@nomeEditor", aoNovoEditor.edi_nm_editor));
                    ioQuery.Parameters.Add(new SqlParameter("@emailEditor", aoNovoEditor.edi_ds_email));
                    ioQuery.Parameters.Add(new SqlParameter("@urlEditor", aoNovoEditor.edi_ds_url));

                    //Executando o comando Transact-SQL e retornando a quantidade de linhas afetadas.
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw new Exception("Erro ao tentar cadastrar novo Editor");
                }
            }
            return liQtdRegistrosInseridos;
        }

        public int RemoveEditor(Editores aoEditor)
        {
            if (aoEditor == null)
                throw new ArgumentNullException();

            int liQtdRegistrosInseridos = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM EDI_EDITORES WHERE EDI_ID_EDITOR = @idEditor", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", aoEditor.edi_id_editores));

                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw new Exception("Erro ao tentar excluir Editor");
                }
            }
            return liQtdRegistrosInseridos;
        }

        public int AtualizaEditor(Editores aoEditor)
        {
            if (aoEditor == null)
                throw new ArgumentNullException();

            int liQtdRegistrosInseridos = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("UPDATE EDI_EDITORES SET EDI_NM_EDITOR = @nomeEditor, EDI_DS_EMAIL = @emailEditor, EDI_DS_URL = @urlEditor WHERE EDI_ID_EDITOR = @idEditor", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", aoEditor.edi_id_editores));
                    ioQuery.Parameters.Add(new SqlParameter("@nomeEditor", aoEditor.edi_nm_editor));
                    ioQuery.Parameters.Add(new SqlParameter("@emailEditor", aoEditor.edi_ds_email));
                    ioQuery.Parameters.Add(new SqlParameter("@urlEditor", aoEditor.edi_ds_url));

                    //Executando o comando Transact-SQL e retornando a quantidade de linhas afetadas.
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw new Exception("Erro ao tentar atualizar as informações do Editor");
                }
            }
            return liQtdRegistrosInseridos;
        }
    }
}