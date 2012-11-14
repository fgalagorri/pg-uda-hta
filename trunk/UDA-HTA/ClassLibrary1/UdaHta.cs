using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Interfaces;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace UdaHtaDataAccess
{
    public class UdaHta
    {
        private string ConnectionString = "SERVER=localhost;DATABASE=udahta_db;UID=root;PASSWORD=rootudahta;";
        private MySqlConnection conn;

        public UdaHta()
        {
        }

        public void connectToDataBase()
        {
            //string Consulta = "SELECT * FROM User";
            conn = new MySqlConnection(ConnectionString);
            /*MySqlDataAdapter mda = new MySqlDataAdapter(Consulta, cnn);
            DataSet ds = new DataSet();
            mda.Fill(ds, "User");

            Console.WriteLine(ds.Tables[0].Rows[0].ItemArray[0].ToString());
            Console.WriteLine(ds.Tables[0].Rows[0].ItemArray[1].ToString());
            Console.WriteLine("");
            Console.WriteLine(ds.Tables[0].Rows[1].ItemArray[0].ToString());
            Console.WriteLine(ds.Tables[0].Rows[1].ItemArray[1].ToString());
            */
        }

        public void closeConnectionDataBase()
        {
            conn.Close(); 
        }


        public ICollection<Patient> ListPatients()
        {
            return null;
        }

        public ICollection<Report> ListAllReports()
        {
            return null;
        }


        // Devuelve una lista de los reportes del paciente 'patientId'
        public ICollection<Report> GetReportsByPatientId(int patientId)
        {
            return null;
        }


        public void insertReport(int idPatient, Report rep)
        {
//            string query = "insert into User(idUsuario, login) values(2, 'usuario')";
//            string query = "INSERT INTO report(idPatient,Report_idReport) VALUES (" + idPatient + ", " + rep.Ident + ")";

            MySqlCommand mc = new MySqlCommand("insertReport", conn);
            mc.CommandType = CommandType.StoredProcedure;
            mc.Parameters.Add(new MySqlParameter("idRep", rep.Ident));
            mc.Parameters.Add(new MySqlParameter("idPat", idPatient));
            conn.Open();
            mc.ExecuteNonQuery();
            conn.Close();
        }

        //Inserta un nuevo usuario en la base de datos
        public void insertUser(int idUsuario, string login, string pass, string rol)
        {
            MySqlCommand mc = new MySqlCommand("insertUser", conn);
            mc.CommandType = CommandType.StoredProcedure;
            mc.Parameters.Add(new MySqlParameter("id", idUsuario));
            mc.Parameters.Add(new MySqlParameter("log", login));
            mc.Parameters.Add(new MySqlParameter("p", pass));
            mc.Parameters.Add(new MySqlParameter("r", rol));
            conn.Open();
            mc.ExecuteNonQuery();
            conn.Close();
        }

        //Inserta un nuevo usuario en la base de datos
        public void insertDrugType(int idDrugType, string type)
        {
            MySqlCommand mc = new MySqlCommand("insertDrugType", conn);
            mc.CommandType = CommandType.StoredProcedure;
            mc.Parameters.Add(new MySqlParameter("id", idDrugType));
            mc.Parameters.Add(new MySqlParameter("typ", type));
            conn.Open();
            mc.ExecuteNonQuery();
            conn.Close();
        }

    }
}
