using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_Management.Models;

namespace Library_Management.DAO
{
    internal class EditoresDAO
    {
        private string connectionString = "Data Source=trendsclouddb01\\dev01;Database=treinamento_livraria;User ID=sagresadm;Password=sagesadm";


        public IEnumerable<Editores> SelectEditores(decimal? ediID = null)
        {
            List<Editores> resultQuery = new List<Editores>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand sqlQuery;

                    if (ediID != null)
                    {
                        sqlQuery = new SqlCommand("SELECT * FROM EDI_EDITORES WHERE EDI_ID = @idEditor", connection);
                        sqlQuery.Parameters.Add(new SqlParameter("@idEditor", ediID));
                    }
                    else
                    {
                        sqlQuery = new SqlCommand("SELECT * FROM EDI_EDITORES", connection);
                    }


                    using (SqlDataReader reader = sqlQuery.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Editores tempEdi = new Editores(reader.GetDecimal(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                            resultQuery.Add(tempEdi);
                        }

                        connection.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro em tentar buscar os editores");
                }
            }


            return resultQuery;
        }

        public decimal InsertEditor(Editores newEditor)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    decimal idResult;

                    SqlCommand sqlQuery = new SqlCommand("INSERT INTO EDI_EDITORES(EDI_ID_EDITOR, EDI_NM_EDITOR, EDI_DS_URL, EDI_DS_EMAIL) VALUES(@idEditor, @nomeEditor, @urlEditor, @emailEditor)", connection);

                    sqlQuery.Parameters.Add(new SqlParameter("@idEditor", newEditor.edi_id_editor));
                    sqlQuery.Parameters.Add(new SqlParameter("@nomeEditor", newEditor.edi_nm_editor));
                    sqlQuery.Parameters.Add(new SqlParameter("@urlEditor", newEditor.edi_ds_url));
                    sqlQuery.Parameters.Add(new SqlParameter("@emailEditor", newEditor.edi_ds_email));

                    sqlQuery.ExecuteNonQuery();

                    idResult = newEditor.edi_id_editor;

                    return idResult;

                }
                catch
                {
                    throw new Exception("Erro em tentar inserir novo editor");
                }
            }
        }

        public int UpdateEditor(Editores newEditor)
        {

            int lineaffects;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand sqlQuery = new SqlCommand("UPDATE EDI_EDITORES SET EDI_NM_EDITOR = @nomeEditor, EDI_DS_URL = @urlEditor, EDI_DS_EMAIL = @emailEditor WHERE EDI_ID_EDITOR = @idEditor", connection);

                    sqlQuery.Parameters.Add(new SqlParameter("@idEditor", newEditor.edi_id_editor));
                    sqlQuery.Parameters.Add(new SqlParameter("@nomeEditor", newEditor.edi_nm_editor));
                    sqlQuery.Parameters.Add(new SqlParameter("@urlEditor", newEditor.edi_ds_url));
                    sqlQuery.Parameters.Add(new SqlParameter("@emailEditor", newEditor.edi_ds_email));

                    lineaffects = sqlQuery.ExecuteNonQuery();

                    return lineaffects;
                }
                catch
                {
                    throw new Exception("Erro em tentar atualizar editor");
                }
            }
        }

        public int DeleteEditor(decimal autID)
        {
            int lineaffects;


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand sqlQuery = new SqlCommand("DELETE FROM EDI_EDITORES WHERE EDI_ID_EDITOR = @idEditor", connection);
                    sqlQuery.Parameters.Add(new SqlParameter("@idEditor", autID));

                    lineaffects = sqlQuery.ExecuteNonQuery();

                    return lineaffects;
                }
                catch
                {
                    throw new Exception("Erro em tentar deletar editor");
                }
            }

        }
    }
}
