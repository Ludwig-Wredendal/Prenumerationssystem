using System.Data.SqlClient;
using System.Data;
using String = System.String;
using Prenumerationssystem.Models;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace Prenumerationssystem.Models
{
    public class PrenumerantMetoder
    {
        public PrenumerantMetoder() { }
        // GET id
        public PrenumerantDetalj GetPrenumerantByPn(int prenumerationsnummer, out string errormsg)
        {
            errormsg = "";

            try
            {
                // Connection string
                string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=db_Prenumerant;Integrated Security=True";

                // SQL query with parameter
                string sqlQuery = "SELECT * FROM [tbl_prenumerant] WHERE pr_prenumerationsnummer = @id";

                using (SqlConnection dbConnection = new SqlConnection(connectionString))
                {
                    // Create command with parameter
                    SqlCommand dbCommand = new SqlCommand(sqlQuery, dbConnection);
                    dbCommand.Parameters.Add("@id", SqlDbType.Int).Value = prenumerationsnummer;

                    dbConnection.Open();

                    using (SqlDataReader reader = dbCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            PrenumerantDetalj pd = new PrenumerantDetalj
                            {
                                pr_prenumerationsnummer = Convert.ToInt32(reader["pr_prenumerationsnummer"]),
                                pr_fornamn = reader["pr_fornamn"].ToString(),
                                pr_efternamn = reader["pr_efternamn"].ToString(),
                                pr_utdelningsadress = reader["pr_utdelningsadress"].ToString(),
                                pr_personnummer = Convert.ToInt32(reader["pr_personnummer"]),
                                pr_telefonnummer = Convert.ToInt32(reader["pr_telefonnummer"]),
                                pr_postnummer = Convert.ToInt32(reader["pr_postnummer"]),
                                pr_ort = reader["pr_ort"].ToString()
                            };

                            return pd;
                        }
                        else
                        {
                            errormsg = "Det hämtas ingen prenumerant.";
                            return null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return null;
            }
        }


        // GET:
        public List<PrenumerantDetalj> GetPrenumerantWithDataReader(out string errormsg)
        {
            //skapa SQL-connection
            SqlConnection dbConnection = new SqlConnection();

            // Koppling mot SQL Server
            dbConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = db_Prenumerant; Integrated Security = True;";

            // sqlstring för att hämta alla studenter
            String sqlstring = "SELECT * FROM [tbl_prenumerant]";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            // Declare the SqlDataReader, which is used in
            // both the try block and the finnaly block.

            SqlDataReader reader = null;

            List<PrenumerantDetalj> prenumerantlista = new List<PrenumerantDetalj>();

            errormsg = "";

            try
            {
                // open the connection
                dbConnection.Open();

                // 1. get an instance of the SqlDataReader
                reader = dbCommand.ExecuteReader();

                // 2. read necessary columns of each block.
                while (reader.Read())
                {
                    //Läser ut data från datasetet
                    PrenumerantDetalj prenumerant = new PrenumerantDetalj();

                    prenumerant.pr_prenumerationsnummer = Convert.ToInt32(reader["pr_prenumerationsnummer"]);
                    prenumerant.pr_personnummer = Convert.ToInt32(reader["pr_personnummer"]);
                    prenumerant.pr_fornamn = reader["pr_fornamn"].ToString();
                    prenumerant.pr_efternamn = reader["pr_efternamn"].ToString();
                    prenumerant.pr_telefonnummer = Convert.ToInt32(reader["pr_telefonnummer"]);
                    prenumerant.pr_utdelningsadress = reader["pr_utdelningsadress"].ToString();
                    prenumerant.pr_postnummer = Convert.ToInt32(reader["pr_postnummer"]);
                    prenumerant.pr_ort = reader["pr_ort"].ToString();

                    prenumerantlista.Add(prenumerant);
                }
                reader.Close();
                return prenumerantlista;
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        // GET: id

    }
}
