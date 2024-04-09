using System.Data.SqlClient;
using System.Data;
using String = System.String;
using Prenumerationssystem.Models;

namespace Prenumerationssystem.Models
{
    public class PrenumerantMetoder
    {
        public PrenumerantMetoder() { }

        public List<PrenumerantDetalj> GetPrenumerantWithDataReader(out string errormsg)
        {
            //skapa SQL-connection
            SqlConnection dbConnection = new SqlConnection();

            // Koppling mot SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=db_Prenumerant;Integrated Security=True";

            // sqlstring för att hämta alla studenter
            String sqlstring = "SELECT * FROM [tbl_prenumerant]";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            // Declare the SqlDataReader, which is used in
            // both the try block and the finnaly block.

            SqlDataReader reader = null;

            List<PrenumerantDetalj> studentlista = new List<StudentDetalj>();

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
                    StudentDetalj Student = new StudentDetalj();

                    Student.St_Id = Convert.ToInt16(reader["St_Id"]);
                    Student.St_Fornamn = reader["St_Fornamn"].ToString();
                    Student.St_Efternamn = reader["St_Efternamn"].ToString();
                    Student.St_Epost = reader["St_Epost"].ToString();
                    Student.St_Universitet = Convert.ToInt16(reader["St_Universitet"]);
                    Student.St_Examensar = Convert.ToInt16(reader["St_Examensar"]);
                    Student.St_Program = Convert.ToInt16(reader["St_Program"]);
                    Student.St_Kurs = Convert.ToInt16(reader["St_Kurs"]);

                    studentlista.Add(Student);
                }
                reader.Close();
                return studentlista;
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
    }
}
