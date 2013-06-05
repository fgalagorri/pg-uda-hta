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
        private udahta_dbEntities udaContext;

        public UdaHtaDataAccess()
        {
            conn = new MySqlConnection(ConnectionString);
            udaContext = new udahta_dbEntities();
        }

        public void CloseConnectionDataBase()
        {
            conn.Close(); 
        }

        public bool ExistPatient(long? idPatient)
        {
            return udaContext.patientuda.Any(p => p.idPatientUda == idPatient);
        }


        public ICollection<PatientReport> ListAllReports()
        {
            ICollection<PatientReport> udaQuery = udaContext.report.Select(r => new PatientReport()
                {
                    ReportDevice = r.idDevice,
                    ReportId = r.deviceReportId
                }).ToList();

            return udaQuery;
        }

        // Devuelve una lista de los reportes del paciente 'patientId'
        public ICollection<Report> GetReportsByPatientId(long patientId)
        {
            ICollection<Report> lrep = new List<Report>();

            var query = udaContext.report
                .Where(r => r.patientuda_idPatientUda == patientId)
                .Select(r => new {r.idReport, r.dailycarnet_idDailyCarnet, r.patientuda_idPatientUda, r.temporarydata_idTemporaryData, r.begin_date, 
                                  r.dailycarnet, r.day_avg_dias, r.day_avg_sys, r.day_max_dias, r.day_max_sys, r.deviceReportId, r.diagnosis, 
                                  r.doctor, r.end_date, r.idDevice, r.investigation, r.measurement, r.night_avg_dias, r.night_avg_sys, 
                                  r.night_max_dias, r.night_max_sys, r.patientuda, r.request_doctor, r.specialty, r.temporarydata, r.total_avg_dias, 
                                  r.total_avg_sys}).ToList();

            foreach (var rep in query)
            {
                var report = new Report();
                report.BeginDate = rep.begin_date;
                
                //DailyCarnet
                report.Carnet.InitSystolic1 = rep.dailycarnet.init_sys1;
                report.Carnet.InitSystolic2 = rep.dailycarnet.init_sys2;
                report.Carnet.InitSystolic3 = rep.dailycarnet.init_sys3;
                
                report.Carnet.InitDiastolic1 = rep.dailycarnet.initial_dias1;
                report.Carnet.InitDiastolic2 = rep.dailycarnet.initial_dias2;
                report.Carnet.InitDiastolic3 = rep.dailycarnet.initial_dias3;
                
                report.Carnet.InitHeartRate1 = rep.dailycarnet.initial_hr1;
                report.Carnet.InitHeartRate2 = rep.dailycarnet.initial_hr2;
                report.Carnet.InitHeartRate3 = rep.dailycarnet.initial_hr3;

                report.Carnet.FinalSystolic1 = rep.dailycarnet.final_sys1;
                report.Carnet.FinalSystolic2 = rep.dailycarnet.final_sys2;
                report.Carnet.FinalSystolic3 = rep.dailycarnet.final_sys3;

                report.Carnet.FinalDiastolic1 = rep.dailycarnet.final_dias1;
                report.Carnet.FinalDiastolic2 = rep.dailycarnet.final_dias2;
                report.Carnet.FinalDiastolic3 = rep.dailycarnet.final_dias3;

                report.Carnet.FinalHeartRate1 = rep.dailycarnet.final_hr1;
                report.Carnet.FinalHeartRate2 = rep.dailycarnet.final_hr2;
                report.Carnet.FinalHeartRate3 = rep.dailycarnet.final_hr3;

                report.Carnet.MealTime = new DateTime(rep.dailycarnet.main_meal_time.Value.Year,
                                                      rep.dailycarnet.main_meal_time.Value.Month,
                                                      rep.dailycarnet.main_meal_time.Value.Day,
                                                      rep.dailycarnet.main_meal_time.Value.Hour,
                                                      rep.dailycarnet.main_meal_time.Value.Minute,
                                                      rep.dailycarnet.main_meal_time.Value.Second);

                report.Carnet.SleepQuality = rep.dailycarnet.how_sleep;

                report.Carnet.SleepTimeEnd = new DateTime(rep.dailycarnet.end_sleep_time.Value.Year,
                                                          rep.dailycarnet.end_sleep_time.Value.Month,
                                                          rep.dailycarnet.end_sleep_time.Value.Day,
                                                          rep.dailycarnet.end_sleep_time.Value.Hour,
                                                          rep.dailycarnet.end_sleep_time.Value.Minute,
                                                          rep.dailycarnet.end_sleep_time.Value.Second);

                report.Carnet.SleepTimeStart = new DateTime(rep.dailycarnet.begin_sleep_time.Value.Year,
                                                            rep.dailycarnet.begin_sleep_time.Value.Month,
                                                            rep.dailycarnet.begin_sleep_time.Value.Day,
                                                            rep.dailycarnet.begin_sleep_time.Value.Hour,
                                                            rep.dailycarnet.begin_sleep_time.Value.Minute,
                                                            rep.dailycarnet.begin_sleep_time.Value.Second
                                                            );

                report.Carnet.Technician.Name = rep.dailycarnet.technical;

                report.DeviceId = rep.idDevice;
                report.DeviceReportId = rep.deviceReportId;
                report.Diagnosis = rep.diagnosis;
                
                report.DiastolicDayAvg = rep.day_avg_dias;
                report.DiastolicDayMax = rep.day_max_sys;
                report.DiastolicNightAvg = rep.night_avg_dias;
                report.DiastolicNightMax = rep.night_max_dias;
                report.DiastolicTotalAvg = rep.total_avg_dias;
                
                report.Doctor.Name = rep.doctor;
                report.EndDate = rep.end_date;
                report.RequestDoctor = rep.request_doctor;
                report.RequestDoctorSpeciality = rep.specialty;

                //TemporaryData
                report.TemporaryData.IdTemporaryData = rep.temporarydata.idTemporaryData;
                report.TemporaryData.Age = rep.temporarydata.age;
                report.TemporaryData.BodyMassIndex = rep.temporarydata.body_mass_index;
                report.TemporaryData.Diabetic = rep.temporarydata.diabetic;
                report.TemporaryData.Dyslipidemia = rep.temporarydata.dyslipidemia;
                report.TemporaryData.FatPercentage = rep.temporarydata.fat_percentage;
                report.TemporaryData.Height = rep.temporarydata.height;
                report.TemporaryData.Hypertensive = rep.temporarydata.known_hypertensive;
                report.TemporaryData.Kcal = rep.temporarydata.kcal;
                report.TemporaryData.MusclePercentage = rep.temporarydata.muscle_percentage;
                report.TemporaryData.Smoker = rep.temporarydata.smoker;
                report.TemporaryData.Weight = report.TemporaryData.Weight;

            }

            return lrep;
        }


        public void insertReport(Report rep)
        {
            if (rep.Carnet != null)
            { //si DailyCarnet existe, insertar
                rep.DailyCarnetId = insertDailyCarnet(rep.Carnet);
            }

            if (rep.TemporaryData != null)
            { //si TemporaryData existe, insertar
                rep.TemporaryDataId = insertTemporaryData(rep.TemporaryData);    
            }
            

            ObjectParameter lastIdReport = new ObjectParameter("id",typeof(long));
            udaContext.insertReport(lastIdReport, rep.BeginDate, rep.EndDate, rep.Doctor.Name, rep.Diagnosis, rep.RequestDoctor,
                                    rep.RequestDoctorSpeciality, rep.SystolicDayAvg, rep.SystolicNightAvg, rep.SystolicTotalAvg, rep.SystolicDayMax, 
                                    rep.SystolicNightMax, rep.DiastolicDayAvg, rep.DiastolicNightAvg, rep.DiastolicTotalAvg, rep.DiastolicDayMax, 
                                    rep.DiastolicNightMax, rep.DeviceId, int.Parse(rep.DeviceReportId), rep.TemporaryDataId, rep.DailyCarnetId, 
                                    rep.Patient.UdaId);
            

            //Obtener lista de medidas para insertar en tabla Measurement
            ICollection<Measurement> lmeasure = rep.Measures;

            foreach (Measurement m in lmeasure)
            {
                udaContext.insertMeasurement(m.Time, m.Systolic, m.Middle, m.Diastolic, m.HeartRate, m.Asleep, m.Comment, (long?)lastIdReport.Value,
                                             rep.Patient.UdaId);
            }
            conn.Close();
        }

        public int? insertDailyCarnet(DailyCarnet dCarnet)
        {
            ObjectParameter lastIdDailyReport = new ObjectParameter("id", typeof(int));
            udaContext.insertDailyCarnet(lastIdDailyReport, dCarnet.Technician.Name, dCarnet.InitDiastolic1,
                                         dCarnet.InitDiastolic2, dCarnet.InitDiastolic3,
                                         dCarnet.InitHeartRate1, dCarnet.InitHeartRate2, dCarnet.InitHeartRate3,
                                         dCarnet.FinalDiastolic1, dCarnet.FinalDiastolic2, dCarnet.FinalDiastolic3,
                                         dCarnet.FinalHeartRate1, dCarnet.FinalHeartRate2, dCarnet.FinalHeartRate3,
                                         dCarnet.SleepTimeStart, dCarnet.SleepTimeEnd, dCarnet.SleepQuality, dCarnet.MealTime,
                                         dCarnet.InitSystolic1, dCarnet.InitSystolic2, dCarnet.InitSystolic3,
                                         dCarnet.FinalSystolic1, dCarnet.FinalSystolic2, dCarnet.FinalSystolic3
                                         );
            
            ObjectParameter lastIdCA = new ObjectParameter("id",typeof(int));
            foreach (var compl in dCarnet.Complications)
            {
                udaContext.insertComplications_Activities(lastIdCA, compl.Time.Hour, compl.Time.Minute, "COMPLICACION",
                                                          (int) lastIdDailyReport.Value, compl.Description);
            }

            ObjectParameter lastIdEff = new ObjectParameter("id",typeof(int));
            foreach (var effort in dCarnet.Efforts)
            {
                udaContext.insertComplications_Activities(lastIdEff, effort.Time.Hour, effort.Time.Minute, "ACTIVIDAD",
                                                          (int) lastIdDailyReport.Value, effort.Description);
            }

            return (int?) lastIdDailyReport.Value;
        }

        public int? insertTemporaryData(TemporaryData temporaryData)
        {
            ObjectParameter lastIdTempData = new ObjectParameter("id", typeof(int));
            udaContext.insertTemporaryData(lastIdTempData, temporaryData.Weight, temporaryData.Height, temporaryData.Age,
                                           temporaryData.BodyMassIndex, temporaryData.Smoker, temporaryData.Dyslipidemia,
                                           temporaryData.Diabetic, temporaryData.Hypertensive, temporaryData.FatPercentage,
                                           temporaryData.MusclePercentage, temporaryData.Kcal);

            foreach (var med in temporaryData.LMedicines)
            {
                insertMedicineDose(med,temporaryData.IdTemporaryData);
            }

            return (int?)lastIdTempData.Value;    

        }

        public void insertMedicineDose(MedicineDose medicineDose, int idTemporaryData)
        {
            udaContext.insertMedicineDose(medicineDose.Dose, medicineDose.Drug.Id, idTemporaryData);
        }

        //Verifica que existe el nombre de usuario 'userName' en la base de datos y devuelve el password,
        //en caso de no existr devuelvo null
        public string getPassword(string userName)
        {
            string stm = "SELECT pass FROM User WHERE login = '" + userName + "' LIMIT 1";
            MySqlCommand mc = new MySqlCommand(stm, conn);

            conn.Open();
            MySqlDataReader rdr = mc.ExecuteReader();
            
            string pswd = "";
            while ( rdr.Read() )
            {
                pswd = rdr.GetString(0);
            }

            rdr.Close();
            conn.Close();

            return pswd;

        }
        
        //Actualiza la contrasena del usuario userName
        public bool updatePassword(string userName, string newPswd)
        {
            try
            {
                udaContext.updatePassword(userName, newPswd);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        //Inserta la referencia a la base de pacientes
        public void insertPatientUda(long id)
        {
            try
            {
                udaContext.insertPatientUda(id);
            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                throw e;
            }
            
        }

        //Inserta una nueva instancia de la historia clinca del paciente
        public void insertMedicalHistory(long patientId, MedicalRecord medicalRecord)
        {
            ObjectParameter lastIdMedicalHistory = new ObjectParameter("id",typeof(int));
            udaContext.insertMedicalHistory(lastIdMedicalHistory, medicalRecord.Illness, medicalRecord.Since,
                                           medicalRecord.Until, medicalRecord.Comment, patientId);
        }
        
        //Inserta un nuevo usuario en la base de datos
        public void insertUser(int idUsuario, string login, string pass, string rol)
        {
            udaContext.insertUser(idUsuario, login, pass, rol);
        }

        //Inserta un nuevo tipo de droga en la base de datos
        public void insertDrugType(string type)
        {
            udaContext.insertDrugType(type);
        }

        //Inserta una nueva droga en la base de datos
        public void insertDrug(string name, int idDrugTyp)
        {
            udaContext.insertDrug(name,idDrugTyp);
        }


        /*
         * INVESTIGACIONES
         */

        //Listar investigaciones 
        public ICollection<Investigation> listInvestigations()
        {
            ICollection<Investigation> list = udaContext.investigation.Select(i => new Investigation(i.idInvestigation,i.name,i.creation_date)).ToList();
            return list;
        }

        //Inserta una nueva investigacion en la base de datos
        public int insertInvestigation(string nam, DateTime createDat)
        {
            ObjectParameter id = new ObjectParameter("id",typeof(int));
            udaContext.insertInvestigation(id, nam, createDat);
            return Convert.ToInt16(id.Value.ToString());
        }

        public void addReportToInvestigation(long idPatient, long idReport, int idInvestigation)
        {
            
        }

    }
}
