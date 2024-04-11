using System.Data.SqlClient;
using System.Data;
using String = System.String;
using Prenumerationssystem.Models;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using Microsoft.AspNetCore.Mvc;

namespace Prenumerationssystem.Models
{
    public class PrenumerantMetoder
    {
        public PrenumerantMetoder() { }

        public int PostPrenumerant(PrenumerantDetalj pd, out string errormsg)
        {
            //skapa SQL-connection
            SqlConnection dbConnection = new SqlConnection();

            // Koppling mot SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=db_Prenumerant;Integrated Security=True";

            // sqlstring och lägg till en student i databasen
            String sqlstring = "INSERT INTO tbl_prenumerant (pr_personnummer, pr_fornamn, pr_efternamn, pr_telefonnummer, pr_utdelningsadress, pr_postnummer, pr_ort) " +
                "VALUES (@personnummer, @fornamn,@efternamn,@telefonnummer, @utdelningsadress,@postnummer,@ort)";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);
             
            dbCommand.Parameters.Add("personnummer", SqlDbType.Int).Value = pd.pr_personnummer;
            dbCommand.Parameters.Add("fornamn", SqlDbType.NVarChar, 50).Value = pd.pr_fornamn;
            dbCommand.Parameters.Add("efternamn", SqlDbType.NVarChar, 50).Value = pd.pr_efternamn;
            dbCommand.Parameters.Add("telefonnummer", SqlDbType.Int).Value = pd.pr_telefonnummer;
            dbCommand.Parameters.Add("utdelningsadress", SqlDbType.NVarChar, 50).Value = pd.pr_utdelningsadress;
            dbCommand.Parameters.Add("postnummer", SqlDbType.Int).Value = pd.pr_postnummer;
            dbCommand.Parameters.Add("ort", SqlDbType.NVarChar, 50).Value = pd.pr_ort;

            try
            {
                dbConnection.Open();
                int i = 0;
                i = dbCommand.ExecuteNonQuery();
                if (i == 1) { errormsg = "Error"; }
                else { errormsg = "Det skapas inte en prenumerant i databasen."; }
                return (i);
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return 0;
            }
            finally
            {
                dbCommand.Connection.Close();
            }
        }
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

        // PUT:
        public PrenumerantDetalj EditPrenumerant(PrenumerantDetalj pd, int prenumerationsnummer, out string errormsg) 
        {
            //skapa SQL-connection
            SqlConnection dbConnection = new SqlConnection();

            // Koppling mot SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=db_Prenumerant;Integrated Security=True";

            // sqlstring och uppdatera till en student i databasen
            String sqlstring = "UPDATE tbl_prenumerant SET " +
                       "pr_personnummer = @personnummer, " +
                       "pr_fornamn = @fornamn, " +
                       "pr_efternamn = @efternamn, " +
                       "pr_telefonnummer = @telefonnummer, " +
                       "pr_utdelningsadress = @utdelningsadress, " +
                       "pr_postnummer = @postnummer, " +
                       "pr_ort = @ort " +
                       "WHERE pr_prenumerationsnummer = @id";
          
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);
            dbCommand.Parameters.Add("id", SqlDbType.Int).Value = prenumerationsnummer;


            dbCommand.Parameters.Add("personnummer", SqlDbType.Int).Value = pd.pr_personnummer;
            dbCommand.Parameters.Add("fornamn", SqlDbType.NVarChar, 50).Value = pd.pr_fornamn;
            dbCommand.Parameters.Add("efternamn", SqlDbType.NVarChar, 50).Value = pd.pr_efternamn;
            dbCommand.Parameters.Add("telefonnummer", SqlDbType.Int).Value = pd.pr_telefonnummer;
            dbCommand.Parameters.Add("utdelningsadress", SqlDbType.NVarChar, 50).Value = pd.pr_utdelningsadress;
            dbCommand.Parameters.Add("postnummer", SqlDbType.Int).Value = pd.pr_postnummer;
            dbCommand.Parameters.Add("ort", SqlDbType.NVarChar, 50).Value = pd.pr_ort;

            try
            {
                dbConnection.Open();
                int rowsAffected = dbCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    errormsg = "";
                    return pd;
                }
                else
                {
                    errormsg = "No prenumerant record updated. Prenumerant not found.";
                    return null;
                }
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return null;
            }
            finally
            {
                dbCommand.Connection.Close();
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
