using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using Entities;

namespace DataAccess
{
    public class PatientDataAccess
    {
        private string ConnectionString = "SERVER=localhost;DATABASE=patient_info_db;UID=root;PASSWORD=rootudahta;";
        private MySqlConnection conn;

        public PatientDataAccess()
        {
        }

        public void connectToDataBase()
        {
            conn = new MySqlConnection(ConnectionString);
        }

        public void closeConnectionDataBase()
        {
            conn.Close();
        }

        public void insertPatient(Patient p)
        {
            MySqlCommand mc = new MySqlCommand("insertPatient", conn);
            mc.CommandType = CommandType.StoredProcedure;
            mc.Parameters.Add(new MySqlParameter("idInDev", p.IdInDevice));            
            mc.Parameters.Add(new MySqlParameter("name", p.Name));
            mc.Parameters.Add(new MySqlParameter("surname", p.Surname));
            mc.Parameters.Add(new MySqlParameter("addr", p.Address));
            mc.Parameters.Add(new MySqlParameter("dni", p.DocumentId));
            mc.Parameters.Add(new MySqlParameter("birth", p.BirthDate));
            mc.Parameters.Add(new MySqlParameter("sex", p.Sex));
            mc.Parameters.Add(new MySqlParameter("neighbour", p.Neighbour));
            mc.Parameters.Add(new MySqlParameter("city", p.City));
            mc.Parameters.Add(new MySqlParameter("phone", p.Phone));
            mc.Parameters.Add(new MySqlParameter("cell", p.CellPhone));
            mc.Parameters.Add(new MySqlParameter("email", p.EMail));

            conn.Open();
            mc.ExecuteNonQuery();
            conn.Close();

        }

        /*
         * Lista todos los pacientes existentes en la base.
         */
        public ICollection<Patient> listPatients()
        {
            ICollection<Patient> lp = new List<Patient>();

            string stm = "SELECT * FROM patient";
            MySqlCommand mc = new MySqlCommand(stm, conn);

            conn.Open();
            MySqlDataReader rdr = mc.ExecuteReader();

            while (rdr.Read())
            {
                Patient p = new Patient();
                p.Name = rdr.GetString(2);
                p.Surname = rdr.GetString(3);
                p.DocumentId = rdr.GetString(4);
                lp.Add(p);
            }

            rdr.Close();
            conn.Close();
            
            return lp;
        }

    }
}
