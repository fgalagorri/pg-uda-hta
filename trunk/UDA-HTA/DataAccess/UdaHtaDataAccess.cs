using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Entities;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace DataAccess
{
    public class UdaHtaDataAccess
    {
        private string ConnectionString = "SERVER=localhost;DATABASE=udahta_db;UID=root;PASSWORD=rootudahta;";
        private MySqlConnection conn;

        public UdaHtaDataAccess()
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
            int lastId = 0;

            MySqlCommand mcReport = new MySqlCommand("insertReport", conn);
            mcReport.CommandType = CommandType.StoredProcedure;
            mcReport.Parameters.Add(new MySqlParameter("id", lastId));
            mcReport.Parameters.Add(new MySqlParameter("begin_date", rep.BeginDate));
            mcReport.Parameters.Add(new MySqlParameter("end_date", rep.EndDate));
            mcReport.Parameters.Add(new MySqlParameter("doctor", rep.BeginDate));
            mcReport.Parameters.Add(new MySqlParameter("diagnosis", rep.Diagnosis));
            mcReport.Parameters.Add(new MySqlParameter("requestDoctor", rep.RequestDoctor));
            mcReport.Parameters.Add(new MySqlParameter("specialty", rep.Specialty));
            mcReport.Parameters.Add(new MySqlParameter("dayAvgSys", rep.DayAvgSys));
            mcReport.Parameters.Add(new MySqlParameter("nightAvgSys", rep.NightAvgSys));
            mcReport.Parameters.Add(new MySqlParameter("totalAvgSys", rep.TotalAvgSys));
            mcReport.Parameters.Add(new MySqlParameter("dayMaxSys", rep.DayMaxSys));
            mcReport.Parameters.Add(new MySqlParameter("nightMaxSys", rep.NightMaxSys));
            mcReport.Parameters.Add(new MySqlParameter("dayAvgDias", rep.DayAvgDias));
            mcReport.Parameters.Add(new MySqlParameter("nightAvgDias", rep.NightAvgDias));
            mcReport.Parameters.Add(new MySqlParameter("totalAvgDias", rep.TotalAvgDias));
            mcReport.Parameters.Add(new MySqlParameter("dayMaxDias", rep.DayMaxDias));
            mcReport.Parameters.Add(new MySqlParameter("nightMaxDias", rep.NightMaxDias));
            mcReport.Parameters.Add(new MySqlParameter("idDev", rep.IdDev));
            mcReport.Parameters.Add(new MySqlParameter("devReportId", rep.DevReportId));
            mcReport.Parameters.Add(new MySqlParameter("IdTemporaryData", rep.IdTemporaryData));
            mcReport.Parameters.Add(new MySqlParameter("IdDailyCarnet", rep.IdDailyCarnet));
            mcReport.Parameters.Add(new MySqlParameter("idPatient", idPatient));

            conn.Open();
            mcReport.ExecuteNonQuery();

            //Obtener lista de medidas para insertar en tabla Measurement
            ICollection<Measurement> lmeasure = rep.getMeasureList();
            MySqlCommand mcMeasure = new MySqlCommand("insertMeasure", conn);
            mcMeasure.CommandType = CommandType.StoredProcedure;

            foreach (Measurement m in lmeasure)
            {
                mcMeasure.Parameters.Add(new MySqlParameter("idReport", lastId));
                mcMeasure.Parameters.Add(new MySqlParameter("date",m.Time));
                mcMeasure.Parameters.Add(new MySqlParameter("systolic", m.Systolic));
                mcMeasure.Parameters.Add(new MySqlParameter("average", m.Average));
                mcMeasure.Parameters.Add(new MySqlParameter("diastolic", m.Diastolic));
                mcMeasure.Parameters.Add(new MySqlParameter("heart_rate",m.HeartRate));
                mcMeasure.Parameters.Add(new MySqlParameter("sleep", m.Sleep));

                mcReport.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void insertDailyCarnet()
        {
        }


        //Verifica que existe el nombre de usuario 'userName' en la base de datos y devuelve el password,
        //en caso de no existr devuelvo null
        public string getPassword(string userName)
        {
        //  string stm = "SELECT EXISTS(SELECT 1 FROM User WHERE login = '" + userName + "' LIMIT 1)";
            string stm = "SELECT pass FROM User WHERE login = '" + userName + "' LIMIT 1";
            MySqlCommand mc = new MySqlCommand(stm, conn);

            conn.Open();
            MySqlDataReader rdr = mc.ExecuteReader();
            
            string pswd = "";
            while ( rdr.Read() )
            {
        //      exists = rdr.GetInt16(0);
                pswd = rdr.GetString(0);
            }

            rdr.Close();
            conn.Close();

            return pswd;

        }
        
        //Actualiza la contrasena del usuario userName
        public bool updatePassword(string userName, string newPswd)
        {
            MySqlCommand mc = new MySqlCommand("updatePassword", conn);
            mc.CommandType = CommandType.StoredProcedure;
            mc.Parameters.Add(new MySqlParameter("login_var", userName));
            mc.Parameters.Add(new MySqlParameter("pass_var", newPswd));

            conn.Open();
            mc.ExecuteNonQuery();
            conn.Close();

            return true; 
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

        //Inserta un nuevo tipo de droga en la base de datos
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

        //Inserta una nueva droga en la base de datos
        public void insertDrug(int idDrug, string name, int idDrugTyp)
        {
            MySqlCommand mc = new MySqlCommand("insertDrug", conn);
            mc.CommandType = CommandType.StoredProcedure;
            mc.Parameters.Add(new MySqlParameter("id", idDrug));
            mc.Parameters.Add(new MySqlParameter("nam", name));
            mc.Parameters.Add(new MySqlParameter("idDrugType", idDrugTyp));
            conn.Open();
            mc.ExecuteNonQuery();
            conn.Close();
        }

        //Inserta una nueva droga en la base de datos
        public void insertInvestigation(int id, string nam, DateTime createDat)
        {
            MySqlCommand mc = new MySqlCommand("insertInvestigation", conn);
            mc.CommandType = CommandType.StoredProcedure;
            mc.Parameters.Add(new MySqlParameter("id", id));
            mc.Parameters.Add(new MySqlParameter("nam", nam));
            mc.Parameters.Add(new MySqlParameter("createDat", createDat));
            conn.Open();
            mc.ExecuteNonQuery();
            conn.Close();
        }

    }
}
