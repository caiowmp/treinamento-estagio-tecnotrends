using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_Management.Models;

namespace Library_Management.DAO
{
    internal class TipoLivroDAO
    {
        private string connectionString = "Data Source=trendsclouddb01\\dev01;Database=treinamento_livraria;User ID=sagresadm;Password=sagesadm";


        public IEnumerable<TipoLivro> SelectTipoLivro(decimal? tipoLivroID = null)
        {
            List<TipoLivro> resultQuery = new List<TipoLivro>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand sqlQuery;

                    if (tipoLivroID != null)
                    {
                        sqlQuery = new SqlCommand("SELECT * FROM TIL_TIPO_LIVRO WHERE TIL_ID_TIPO_LIVRO = @idTipoLivro", connection);
                        sqlQuery.Parameters.Add(new SqlParameter("@idTipoLivro", tipoLivroID));
                    }
                    else
                    {
                        sqlQuery = new SqlCommand("SELECT * FROM TIL_TIPO_LIVRO", connection);
                    }


                    using (SqlDataReader reader = sqlQuery.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TipoLivro tempTipoLivro = new TipoLivro(reader.GetDecimal(0), reader.GetString(1));
                            resultQuery.Add(tempTipoLivro);
                        }

                        connection.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro em tentar buscar os tipos de livros");
                }
            }


            return resultQuery;
        }

        public decimal InsertAutor(TipoLivro newTipoLivro)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    decimal idResult;

                    SqlCommand sqlQuery = new SqlCommand("INSERT INTO TIL_TIPO_LIVRO(TIL_ID_TIPO_LIVRO, TIL_DS_DESCRICAO) VALUES(@idTipoLivro, @descricao)", connection);

                    sqlQuery.Parameters.Add(new SqlParameter("@idTipoLivro", newTipoLivro.til_id_tipo_livro));
                    sqlQuery.Parameters.Add(new SqlParameter("@descricao", newTipoLivro.til_ds_resumo));

                    sqlQuery.ExecuteNonQuery();

                    idResult = newTipoLivro.til_id_tipo_livro;

                    return idResult;

                }
                catch
                {
                    throw new Exception("Erro em tentar inserir novo tipo de livro");
                }
            }
        }

        public int UpdateAutor(TipoLivro newTipoLivro)
        {

            int lineaffects;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand sqlQuery = new SqlCommand("UPDATE TIL_TIPO_LIVRO SET TIL_DS_DESCRICAO = @descricao WHERE AUT_AUTOR = @idTipoLivro", connection);

                    sqlQuery.Parameters.Add(new SqlParameter("@idTipoLivro", newTipoLivro.til_id_tipo_livro));
                    sqlQuery.Parameters.Add(new SqlParameter("@descricao", newTipoLivro.til_ds_resumo));

                    lineaffects = sqlQuery.ExecuteNonQuery();

                    return lineaffects;
                }
                catch
                {
                    throw new Exception("Erro em tentar atualizar o tipo de livro");
                }
            }
        }

        public int DeleteAutor(decimal tipoLivroID)
        {
            int lineaffects;


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand sqlQuery = new SqlCommand("DELETE FROM TIL_TIPO_LIVRO WHERE TIL_ID_TIPO_LIVRO = @idTipoLivro", connection);
                    sqlQuery.Parameters.Add(new SqlParameter("@idTipoLivro", tipoLivroID));

                    lineaffects = sqlQuery.ExecuteNonQuery();

                    return lineaffects;
                }
                catch
                {
                    throw new Exception("Erro em tentar deletar tipo de livro");
                }
            }

        }
    }
}
