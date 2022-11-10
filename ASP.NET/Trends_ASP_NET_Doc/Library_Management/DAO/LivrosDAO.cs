using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_Management.Models;

namespace Library_Management.DAO
{
    internal class LivrosDAO
    {
        private string connectionString = "Data Source=trendsclouddb01\\dev01;Database=treinamento_livraria;User ID=sagresadm;Password=sagresadm";


        public BindingList<Livros> SelectLivros(decimal? livID = null)
        {
            BindingList<Livros> resultQuery = new BindingList<Livros>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand sqlQuery;

                    if (livID != null)
                    {
                        sqlQuery = new SqlCommand("SELECT * FROM LIV_LIVROS WHERE LIV_ID_LIVRO = @idLivro", connection);
                        sqlQuery.Parameters.Add(new SqlParameter("@idLivro", livID));
                    }
                    else
                    {
                        sqlQuery = new SqlCommand("SELECT LIV_ID_LIVRO, LIV_ID_TIPO_LIVRO, ISNULL(LIV_ID_EDITOR, 0), LIV_NM_TITULO, ISNULL(LIV_VL_PRECO, 0), LIV_PC_ROYALTY, ISNULL(LIV_DS_RESUMO, 'Vazio'), LIV_NU_EDICAO FROM LIV_LIVROS", connection);
                    }


                    using (SqlDataReader reader = sqlQuery.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            
                            
                            Livros tempLiv = new Livros(reader.GetDecimal(0), reader.GetDecimal(1), reader.GetDecimal(2), reader.GetString(3), reader.GetDecimal(4), reader.GetDecimal(5), reader.GetString(6), reader.GetInt32(7));

                            resultQuery.Add(tempLiv);
                        }

                        connection.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro em tentar buscar os livros");
                }
            }


            return resultQuery;
        }

        public decimal InsertLivro(Livros newLivro)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    decimal idResult;

                    SqlCommand sqlQuery = new SqlCommand("INSERT INTO LIV_LIVROS(LIV_ID_LIVRO, LIV_ID_TIPO_LIVRO, LIV_ID_EDITOR, LIV_NM_TITULO, LIV_VL_PRECO, LIV_PC_ROYALTY, LIV_DS_RESUMO, LIV_NU_EDICAO) " +
                        "VALUES(@idLivro, @idTipoLivro, @idEditor, @titulo, @preco, @royalty, @resumo, @edicao)", connection);

                    sqlQuery.Parameters.Add(new SqlParameter("@idLivro", newLivro.liv_id_livro));
                    sqlQuery.Parameters.Add(new SqlParameter("@idTipoLivro", newLivro.liv_id_tipo_livro));
                    sqlQuery.Parameters.Add(new SqlParameter("@idEditor", newLivro.liv_id_editor));
                    sqlQuery.Parameters.Add(new SqlParameter("@titulo", newLivro.liv_nm_titulo));
                    sqlQuery.Parameters.Add(new SqlParameter("@preco", newLivro.liv_vl_preco));
                    sqlQuery.Parameters.Add(new SqlParameter("@royalty", newLivro.liv_pc_royalty));
                    sqlQuery.Parameters.Add(new SqlParameter("@resumo", newLivro.liv_ds_resumo));
                    sqlQuery.Parameters.Add(new SqlParameter("@edicao", newLivro.liv_nu_edicao));

                    sqlQuery.ExecuteNonQuery();

                    idResult = newLivro.liv_id_livro;

                    return idResult;

                }
                catch
                {
                    throw new Exception("Erro em tentar inserir novo livro");
                }
            }
        }

        public int UpdateLivro(Livros newLivro)
        {

            int lineaffects;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand sqlQuery = new SqlCommand("UPDATE LIV_LIVROS SET LIV_ID_TIPO_LIVRO = @idTipoLivro, LIV_ID_EDITOR = @idEditor, LIV_NM_TITULO = @titulo, LIV_VL_PRECO = @preco, LIV_PC_ROYALTY = @royalty, LIV_DS_RESUMO = @resumo, LIV_NU_EDICAO = @edicao WHERE LIV_ID_LIVRO = @idLivro", connection);

                    sqlQuery.Parameters.Add(new SqlParameter("@idLivro", newLivro.liv_id_livro));
                    sqlQuery.Parameters.Add(new SqlParameter("@idTipoLivro", newLivro.liv_id_tipo_livro));
                    sqlQuery.Parameters.Add(new SqlParameter("@idEditor", newLivro.liv_id_editor));
                    sqlQuery.Parameters.Add(new SqlParameter("@titulo", newLivro.liv_nm_titulo));
                    sqlQuery.Parameters.Add(new SqlParameter("@preco", newLivro.liv_vl_preco));
                    sqlQuery.Parameters.Add(new SqlParameter("@royalty", newLivro.liv_pc_royalty));
                    sqlQuery.Parameters.Add(new SqlParameter("@resumo", newLivro.liv_ds_resumo));
                    sqlQuery.Parameters.Add(new SqlParameter("@edicao", newLivro.liv_nu_edicao));

                    lineaffects = sqlQuery.ExecuteNonQuery();

                    return lineaffects;
                }
                catch
                {
                    throw new Exception("Erro em tentar atualizar livro");
                }
            }
        }

        public int DeleteLivro(decimal livID)
        {
            int lineaffects;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand sqlQuery = new SqlCommand("DELETE FROM LIV_LIVROS WHERE LIV_ID_LIVRO = @idLivro", connection);
                    sqlQuery.Parameters.Add(new SqlParameter("@idLivro", livID));

                    lineaffects = sqlQuery.ExecuteNonQuery();

                    return lineaffects;
                }
                catch
                {
                    throw new Exception("Erro em tentar deletar livro");
                }
            }

        }
    }
}
