using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_Management.Models;

namespace Library_Management.DAO
{
    internal class LivroAutorDAO
    {
        private string connectionString = "Data Source=trendsclouddb01\\dev01;Database=treinamento_livraria;User ID=sagresadm;Password=sagesadm";


        public IEnumerable<LivroAutor> SelectLivroAutor(decimal? autID = null)
        {
            List<LivroAutor> resultQuery = new List<LivroAutor>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand sqlQuery;

                    if (autID != null)
                    {
                        sqlQuery = new SqlCommand("SELECT * FROM LIA_LIVRO_AUTOR WHERE LIA_ID_AUTOR = @idAutor", connection);
                        sqlQuery.Parameters.Add(new SqlParameter("@idAutor", autID));
                    }
                    else
                    {
                        sqlQuery = new SqlCommand("SELECT * FROM LIA_LIVRO_AUTOR", connection);
                    }


                    using (SqlDataReader reader = sqlQuery.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LivroAutor tempLivAut = new LivroAutor(reader.GetDecimal(0), reader.GetDecimal(1), reader.GetDecimal(2));
                            resultQuery.Add(tempLivAut);
                        }

                        connection.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro em tentar buscar as relações de livros com autores");
                }
            }


            return resultQuery;
        }

        public decimal InsertLivroAutor(LivroAutor newRelation)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    
                    decimal idResult;

                    SqlCommand sqlQuery = new SqlCommand("INSERT INTO LIA_LIVRO_AUTOR(LIA_ID_AUTOR, LIA_ID_LIVRO, LIA_PC_ROYALTY) VALUES(@idAutor, @idLivro, @royalty)", connection);

                    sqlQuery.Parameters.Add(new SqlParameter("@idAutor", newRelation.lia_id_autor));
                    sqlQuery.Parameters.Add(new SqlParameter("@idLivro", newRelation.lia_id_livro));
                    sqlQuery.Parameters.Add(new SqlParameter("@royalty", newRelation.lia_pc_royalty));

                    sqlQuery.ExecuteNonQuery();

                    idResult = newRelation.lia_id_autor;

                    return idResult;

                }
                catch
                {
                    throw new Exception("Erro em tentar inserir relação de livro com autor");
                }
            }
        }

        public int UpdateLivroAutor(LivroAutor newRelation)
        {

            int lineaffects;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand sqlQuery = new SqlCommand("UPDATE LIA_LIVRO_AUTOR SET LIA_ID_LIVRO = @idLivro, LIA_PC_ROYALTY = @royalty WHERE LIA_ID_AUTOR = @idAutor", connection);

                    sqlQuery.Parameters.Add(new SqlParameter("@idAutor", newRelation.lia_id_autor));
                    sqlQuery.Parameters.Add(new SqlParameter("@idLivro", newRelation.lia_id_livro));
                    sqlQuery.Parameters.Add(new SqlParameter("@royalty", newRelation.lia_pc_royalty));

                    lineaffects = sqlQuery.ExecuteNonQuery();

                    return lineaffects;
                }
                catch
                {
                    throw new Exception("Erro em tentar atualizar a relação de livro com autor");
                }
            }
        }

        public int DeleteLivroAutor(decimal livAutID)
        {
            int lineaffects;


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand sqlQuery = new SqlCommand("DELETE FROM LIA_LIVRO_AUTOR WHERE LIA_ID_AUTOR = @idAutor", connection);
                    sqlQuery.Parameters.Add(new SqlParameter("@idAutor", livAutID));

                    lineaffects = sqlQuery.ExecuteNonQuery();

                    return lineaffects;
                }
                catch
                {
                    throw new Exception("Erro em tentar deletar a relação de livro com autor");
                }
            }

        }
    }
}
