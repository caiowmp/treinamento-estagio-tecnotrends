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
    public class AutoresDAO
    {
        private string connectionString = "Data Source=trendsclouddb01\\dev01;Database=treinamento_livraria;User ID=sagresadm;Password=sagresadm";

        public BindingList<Autores> SelectAutores(decimal? autID = null)
        {
            BindingList<Autores> resultQuery = new BindingList<Autores>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand sqlQuery;
                    Console.WriteLine(autID);

                    if (autID != null)
                    {
                        sqlQuery = new SqlCommand("SELECT * FROM AUT_AUTORES WHERE AUT_ID = @idAutor", connection);
                        sqlQuery.Parameters.Add(new SqlParameter("@idAutor", autID));
                    }
                    else
                    {
                        sqlQuery = new SqlCommand("SELECT * FROM AUT_AUTORES", connection);
                    }


                    using (SqlDataReader reader = sqlQuery.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Autores tempAut = new Autores(reader.GetDecimal(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                            resultQuery.Add(tempAut);
                        }

                        connection.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro em tentar buscar os autores");
                }
            }


            return resultQuery;
        }

        public decimal InsertAutor(Autores newAutor)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    decimal idResult;

                    SqlCommand sqlQuery = new SqlCommand("INSERT INTO AUT_AUTORES(AUT_ID_AUTOR, AUT_NM_NOME, AUT_NM_SOBRENOME, AUT_DS_EMAIL) VALUES(@idAutor, @nomeAutor, @sobrenomeAutor, @emailAutor)", connection);

                    sqlQuery.Parameters.Add(new SqlParameter("@idAutor", newAutor.aut_id_autor));
                    sqlQuery.Parameters.Add(new SqlParameter("@nomeAutor", newAutor.aut_nm_name));
                    sqlQuery.Parameters.Add(new SqlParameter("@sobrenomeAutor", newAutor.aut_nm_sobrenome));
                    sqlQuery.Parameters.Add(new SqlParameter("@emailAutor", newAutor.aut_ds_email));

                    sqlQuery.ExecuteNonQuery();

                    idResult = newAutor.aut_id_autor;

                    return idResult;

                }
                catch
                {
                    throw new Exception("Erro em tentar inserir novo autor");
                }
            }
        }

        public int UpdateAutor(Autores newAutor)
        {

            int lineaffects;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand sqlQuery = new SqlCommand("UPDATE AUT_AUTORES SET AUT_NM_NOME = @nomeAutor, AUT_NM_SOBRENOME = @sobrenomeAutor, AUT_DS_EMAIL = @emailAutor WHERE AUT_ID_AUTOR = @idAutor", connection);

                    sqlQuery.Parameters.Add(new SqlParameter("@idAutor", newAutor.aut_id_autor));
                    sqlQuery.Parameters.Add(new SqlParameter("@nomeAutor", newAutor.aut_nm_name));
                    sqlQuery.Parameters.Add(new SqlParameter("@sobrenomeAutor", newAutor.aut_nm_sobrenome));
                    sqlQuery.Parameters.Add(new SqlParameter("@emailAutor", newAutor.aut_ds_email));

                    lineaffects = sqlQuery.ExecuteNonQuery();

                    return lineaffects;
                }
                catch
                {
                    throw new Exception("Erro em tentar atualizar autor");
                }
            }
        }

    public int DeleteAutor(decimal autID)
        {
            int lineaffects;


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand sqlQuery = new SqlCommand("DELETE FROM AUT_AUTORES WHERE AUT_ID_AUTOR = @idAutor", connection);
                    sqlQuery.Parameters.Add(new SqlParameter("@idAutor", autID));

                    lineaffects = sqlQuery.ExecuteNonQuery();

                    return lineaffects;
                }
                catch
                {
                    throw new Exception("Erro em tentar deletar autor");
                }
            }

        }
    }
}
