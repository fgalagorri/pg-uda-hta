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
    public class UdaHta : IDeviceDataAccess
    {
        private string ConnectionString = "SERVER=localhost;DATABASE=udahta_db;UID=root;PASSWORD=rootudahta;";

        public UdaHta()
        {
        }

        public void connectToDataBase()
        {
            /*MySqlConnection conn = new MySqlConnection(ConnectionString);
            string query = "insert into User(idUsuario, login) values(2, 'usuario')";
            MySqlCommand mc = new MySqlCommand(query, conn);
            conn.Open();
            mc.ExecuteNonQuery();
            conn.Close();*/

            string Consulta = "SELECT * FROM User";
            MySqlConnection cnn = new MySqlConnection(ConnectionString);
            MySqlDataAdapter mda = new MySqlDataAdapter(Consulta, cnn);
            DataSet ds = new DataSet();
            mda.Fill(ds, "User");

            Console.WriteLine(ds.Tables[0].Rows[0].ItemArray[0].ToString());
            Console.WriteLine(ds.Tables[0].Rows[0].ItemArray[1].ToString());
            Console.WriteLine("");
            Console.WriteLine(ds.Tables[0].Rows[1].ItemArray[0].ToString());
            Console.WriteLine(ds.Tables[0].Rows[1].ItemArray[1].ToString());
            Console.WriteLine("lala");
        }

        public void closeConnectionDataBase()
        {
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

    }
}
