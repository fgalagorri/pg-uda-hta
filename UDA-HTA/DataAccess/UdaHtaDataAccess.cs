using System;
using System.Collections.Generic;
using System.Data.Objects;
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
            conn = new MySqlConnection(ConnectionString);
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

        public void CloseConnectionDataBase()
        {
            conn.Close(); 
        }

        public bool ExistPatient(int idPatient)
        {
            var udaContext = new udahta_dbEntities();
            return udaContext.patientuda.Any(p => p.idPatientUda == idPatient);
        }

        public ICollection<Patient> ListPatients()
        {
            return null;
        }

        public ICollection<PatientReport> ListAllReports()
        {
            var udaContext = new udahta_dbEntities();

            ICollection<PatientReport> udaQuery = udaContext.report.Select(r => new PatientReport()
                {
                    ReportDevice = r.idDevice,
                    ReportId = r.deviceReportId
                }).ToList();

            return udaQuery;
        }

        // Devuelve una lista de los reportes del paciente 'patientId'
        public ICollection<Report> GetReportsByPatientId(int patientId)
        {
            return null;
        }


        public void InsertReport(int idPatient, Report rep, DailyCarnet dCarnet, TemporaryData tempData)
        {
            var udaContext = new udahta_dbEntities();

            ObjectParameter lastIdDailyReport = new ObjectParameter("id", typeof(int));
            udaContext.insertDailyCarnet(lastIdDailyReport, rep.Technician.Name, dCarnet.InitDiastolic1,
                                         dCarnet.InitDiastolic2, dCarnet.InitDiastolic3,
                                         dCarnet.InitHeartRate1, dCarnet.InitHeartRate2, dCarnet.InitHeartRate3,
                                         dCarnet.FinalDiastolic1, dCarnet.FinalDiastolic2, dCarnet.FinalDiastolic3,
                                         dCarnet.FinalHeartRate1, dCarnet.FinalHeartRate2, dCarnet.FinalHeartRate3,
                                         dCarnet.SleepTimeStart, dCarnet.SleepTimeEnd, dCarnet.SleepQuality, dCarnet.MealTime,
                                         dCarnet.InitSystolic1, dCarnet.InitSystolic2, dCarnet.InitSystolic3,
                                         dCarnet.FinalSystolic1, dCarnet.FinalSystolic2, dCarnet.FinalSystolic3
                                         );

            rep.DailyCarnetId = (int?)lastIdDailyReport.Value;

            ObjectParameter lastIdTempData = new ObjectParameter("id", typeof(int));
            udaContext.insertTemporaryData(lastIdTempData, tempData.Weight, tempData.Height, tempData.Age,
                                           tempData.BodyMassIndex, tempData.Smoker, tempData.Dyslipidemia,
                                           tempData.Diabetic, tempData.Hypertensive, tempData.FatPercentage,
                                           tempData.MusclePercentage, tempData.Kcal);

            rep.TemporaryDataId = (int?) lastIdTempData.Value;

            ObjectParameter lastIdReport = new ObjectParameter("id",typeof(long));
            udaContext.insertReport(lastIdReport, rep.BeginDate, rep.EndDate, rep.Doctor.Name, rep.Diagnosis, rep.RequestDoctor,
                                    rep.RequestDoctorSpeciality, rep.SystolicDayAvg, rep.SystolicNightAvg, rep.SystolicTotalAvg, rep.SystolicDayMax, 
                                    rep.SystolicNightMax, rep.DiastolicDayAvg, rep.DiastolicNightAvg, rep.DiastolicTotalAvg, rep.DiastolicDayMax, 
                                    rep.DiastolicNightMax, rep.DeviceId, int.Parse(rep.DeviceReportId), rep.TemporaryDataId, rep.DailyCarnetId, 
                                    idPatient);
            

            //Obtener lista de medidas para insertar en tabla Measurement
            ICollection<Measurement> lmeasure = rep.Measures;

            foreach (Measurement m in lmeasure)
            {
                udaContext.insertMeasurement(m.Time, m.Systolic, m.Middle, m.Diastolic, m.HeartRate, m.Asleep, (long?)lastIdReport.Value,
                                             idPatient);
            }
            conn.Close();
        }

        public void InsertDailyCarnet()
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

        public void insertPatientUda(int id)
        {
            var udaContext = new udahta_dbEntities();
            try
            {
                udaContext.insertPatientUda(id);
            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                throw e;
            }
            
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
        public void insertDrugType(string type)
        {
            MySqlCommand mc = new MySqlCommand("insertDrugType", conn);
            mc.CommandType = CommandType.StoredProcedure;
            mc.Parameters.Add(new MySqlParameter("typ", type));
            conn.Open();
            mc.ExecuteNonQuery();
            conn.Close();
        }

        //Inserta una nueva droga en la base de datos
        public void insertDrug(string name, int idDrugTyp)
        {
            MySqlCommand mc = new MySqlCommand("insertDrug", conn);
            mc.CommandType = CommandType.StoredProcedure;
            mc.Parameters.Add(new MySqlParameter("nam", name));
            mc.Parameters.Add(new MySqlParameter("idDrugType", idDrugTyp));
            conn.Open();
            mc.ExecuteNonQuery();
            conn.Close();
        }

        //Inserta una nueva investigacion en la base de datos
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
