using System;
using System.Collections;
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
        }

        public void CloseConnectionDataBase()
        {
            conn.Close(); 
        }

        public Report getReport(long idReport)
        {
            using (udaContext = new udahta_dbEntities())
            {                
                var qry = udaContext.report.Where(r => r.idReport == idReport).Select(r => new 
                    {
                        r.begin_date,
                        r.dailycarnet,
                        r.day_avg_dias,
                        r.day_avg_sys,
                        r.day_max_dias,
                        r.day_max_sys,
                        r.deviceReportId,
                        r.diagnosis,
                        r.doctor,
                        r.end_date,
                        r.idDevice,
                        r.measurement,
                        r.night_avg_dias,
                        r.night_avg_sys,
                        r.night_max_dias,
                        r.night_max_sys,
                        r.patientuda,
                        r.idReport,
                        r.request_doctor,
                        r.specialty,
                        r.temporarydata,
                        r.total_avg_dias,
                        r.total_avg_sys,
                    }).FirstOrDefault();

                Entities.Report rep = new Report()
                {
                    BeginDate = qry.begin_date,
                    DiastolicDayAvg = qry.day_avg_dias,
                    SystolicDayAvg = qry.day_avg_sys,
                    DiastolicDayMax = qry.day_max_dias,
                    SystolicDayMax = qry.day_max_sys,
                    DeviceReportId = qry.deviceReportId,
                    Diagnosis = qry.diagnosis,
                    EndDate = qry.end_date,
                    DeviceId = qry.idDevice,
                    DiastolicNightAvg = qry.night_avg_dias,
                    SystolicNightAvg = qry.night_avg_sys,
                    DiastolicNightMax = qry.night_max_dias,
                    SystolicNightMax = qry.night_max_sys,
                    RequestDoctor = qry.request_doctor,
                    RequestDoctorSpeciality = qry.specialty,
                    DiastolicTotalAvg = qry.total_avg_dias,
                    SystolicTotalAvg = qry.total_avg_sys,
                    UdaId = qry.idReport

                };

                //DailyCarnet
                rep.Carnet.InitSystolic1 = qry.dailycarnet.init_sys1;
                rep.Carnet.InitSystolic2 = qry.dailycarnet.init_sys2;
                rep.Carnet.InitSystolic3 = qry.dailycarnet.init_sys3;

                rep.Carnet.InitDiastolic1 = qry.dailycarnet.initial_dias1;
                rep.Carnet.InitDiastolic2 = qry.dailycarnet.initial_dias2;
                rep.Carnet.InitDiastolic3 = qry.dailycarnet.initial_dias3;

                rep.Carnet.InitHeartRate1 = qry.dailycarnet.initial_hr1;
                rep.Carnet.InitHeartRate2 = qry.dailycarnet.initial_hr2;
                rep.Carnet.InitHeartRate3 = qry.dailycarnet.initial_hr3;

                rep.Carnet.FinalSystolic1 = qry.dailycarnet.final_sys1;
                rep.Carnet.FinalSystolic2 = qry.dailycarnet.final_sys2;
                rep.Carnet.FinalSystolic3 = qry.dailycarnet.final_sys3;

                rep.Carnet.FinalDiastolic1 = qry.dailycarnet.final_dias1;
                rep.Carnet.FinalDiastolic2 = qry.dailycarnet.final_dias2;
                rep.Carnet.FinalDiastolic3 = qry.dailycarnet.final_dias3;

                rep.Carnet.FinalHeartRate1 = qry.dailycarnet.final_hr1;
                rep.Carnet.FinalHeartRate2 = qry.dailycarnet.final_hr2;
                rep.Carnet.FinalHeartRate3 = qry.dailycarnet.final_hr3;

                if (qry.dailycarnet.main_meal_time != null)
                {
                    rep.Carnet.MealTime = new DateTime(qry.dailycarnet.main_meal_time.Value.Year,
                                                            qry.dailycarnet.main_meal_time.Value.Month,
                                                            qry.dailycarnet.main_meal_time.Value.Day,
                                                            qry.dailycarnet.main_meal_time.Value.Hour,
                                                            qry.dailycarnet.main_meal_time.Value.Minute,
                                                            qry.dailycarnet.main_meal_time.Value.Second);
                }

                rep.Carnet.SleepQuality = qry.dailycarnet.how_sleep;

                rep.Carnet.SleepTimeEnd = new DateTime(qry.dailycarnet.end_sleep_time.Value.Year,
                                                            qry.dailycarnet.end_sleep_time.Value.Month,
                                                            qry.dailycarnet.end_sleep_time.Value.Day,
                                                            qry.dailycarnet.end_sleep_time.Value.Hour,
                                                            qry.dailycarnet.end_sleep_time.Value.Minute,
                                                            qry.dailycarnet.end_sleep_time.Value.Second);

                rep.Carnet.SleepTimeStart = new DateTime(qry.dailycarnet.begin_sleep_time.Value.Year,
                                                            qry.dailycarnet.begin_sleep_time.Value.Month,
                                                            qry.dailycarnet.begin_sleep_time.Value.Day,
                                                            qry.dailycarnet.begin_sleep_time.Value.Hour,
                                                            qry.dailycarnet.begin_sleep_time.Value.Minute,
                                                            qry.dailycarnet.begin_sleep_time.Value.Second
                                                            );

                rep.Carnet.Technician.Name = qry.dailycarnet.technical;

                //TemporaryData
                rep.TemporaryData.IdTemporaryData = qry.temporarydata.idTemporaryData;
                rep.TemporaryData.Age = qry.temporarydata.age;
                rep.TemporaryData.BodyMassIndex = qry.temporarydata.body_mass_index;
                rep.TemporaryData.Diabetic = qry.temporarydata.diabetic;
                rep.TemporaryData.Dyslipidemia = qry.temporarydata.dyslipidemia;
                rep.TemporaryData.FatPercentage = qry.temporarydata.fat_percentage;
                rep.TemporaryData.Height = qry.temporarydata.height;
                rep.TemporaryData.Hypertensive = qry.temporarydata.known_hypertensive;
                rep.TemporaryData.Kcal = qry.temporarydata.kcal;
                rep.TemporaryData.MusclePercentage = qry.temporarydata.muscle_percentage;
                rep.TemporaryData.Smoker = qry.temporarydata.smoker;
                rep.TemporaryData.Weight = qry.temporarydata.weight;

                rep.Patient.UdaId = qry.patientuda.idPatientUda;

                var lmeasures = getMeasures(idReport);
                rep.Measures = rep.Measures.Concat(lmeasures).ToList();
                return rep;
            }
        }

        public ICollection<Measurement> getMeasures(long idReport)
        {
            ICollection<Measurement> lmeasures = new List<Measurement>();

            using (udaContext = new udahta_dbEntities())
            {
                var qry = udaContext.measurement.Where(m => m.report_idReport == idReport).Select(m => new
                    {
                        m.average,
                        m.comment,
                        m.date,
                        m.diastolic,
                        m.heart_rate,
                        m.idMeasurement,
                        m.sleep,
                        m.systolic,
                    }).ToList();

                foreach (var qm in qry)
                {
                    Measurement measure = new Measurement();
                    measure.Asleep = qm.sleep;
                    measure.Comment = qm.comment;
                    measure.Diastolic = qm.diastolic;
                    measure.HeartRate = qm.heart_rate;
                    measure.Middle = qm.average;
                    measure.Systolic = qm.systolic;
                    measure.Time = qm.date;
                    lmeasures.Add(measure);
                }
            }

            return lmeasures;
        }

        public bool ExistPatient(long? idPatient)
        {
            using (udaContext = new udahta_dbEntities())
            {
                return udaContext.patientuda.Any(p => p.idPatientUda == idPatient);
            }
        }


        public ICollection<PatientReport> ListAllReports()
        {
            using (udaContext = new udahta_dbEntities())
            {
                ICollection<PatientReport> udaQuery = udaContext.report.Select(r => new PatientReport()
                {
                    ReportDevice = r.idDevice,
                    ReportId = r.deviceReportId
                }).ToList();

                return udaQuery;                
            }
        }

        // Devuelve una lista de los reportes del paciente 'patientId'
        public ICollection<Report> GetReportsByPatientId(long patientId)
        {
            ICollection<Report> lrep = new List<Report>();

            using (udaContext = new udahta_dbEntities())
            {
                var query = udaContext.report
                                      .Where(r => r.patientuda_idPatientUda == patientId)
                                      .Select(r => new
                                          {
                                              r.idReport,
                                              r.dailycarnet_idDailyCarnet,
                                              r.patientuda_idPatientUda,
                                              r.temporarydata_idTemporaryData,
                                              r.begin_date,
                                              r.dailycarnet,
                                              r.day_avg_dias,
                                              r.day_avg_sys,
                                              r.day_max_dias,
                                              r.day_max_sys,
                                              r.deviceReportId,
                                              r.diagnosis,
                                              r.doctor,
                                              r.end_date,
                                              r.idDevice,
                                              r.investigation,
                                              r.measurement,
                                              r.night_avg_dias,
                                              r.night_avg_sys,
                                              r.night_max_dias,
                                              r.night_max_sys,
                                              r.patientuda,
                                              r.request_doctor,
                                              r.specialty,
                                              r.temporarydata,
                                              r.total_avg_dias,
                                              r.total_avg_sys
                                          });

                var measurements = udaContext.measurement
                                             .Where(m => m.report_patientuda_idPatientUda == patientId)
                                             .Select(m => new Measurement
                                                 {
                                                     Asleep = m.sleep,
                                                     Systolic = m.systolic,
                                                     Middle = m.average,
                                                     Diastolic = m.diastolic,
                                                     HeartRate = m.heart_rate,
                                                     Time = m.date,
                                                     Comment = m.comment,
                                                     ReportId = m.report_idReport
                                                 });

                foreach (var rep in query)
                {
                    var report = new Report
                    {
                        BeginDate = rep.begin_date,
                        DiastolicDayAvg = rep.day_avg_dias,
                        SystolicDayAvg = rep.day_avg_sys,
                        DiastolicDayMax = rep.day_max_dias,
                        SystolicDayMax = rep.day_max_sys,
                        DeviceReportId = rep.deviceReportId,
                        Diagnosis = rep.diagnosis,
                        EndDate = rep.end_date,
                        DeviceId = rep.idDevice,
                        DiastolicNightAvg = rep.night_avg_dias,
                        SystolicNightAvg = rep.night_avg_sys,
                        DiastolicNightMax = rep.night_max_dias,
                        SystolicNightMax = rep.night_max_sys,
                        RequestDoctor = rep.request_doctor,
                        RequestDoctorSpeciality = rep.specialty,
                        DiastolicTotalAvg = rep.total_avg_dias,
                        SystolicTotalAvg = rep.total_avg_sys,
                        UdaId = rep.idReport
                    };

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

                    if (rep.dailycarnet.main_meal_time != null)
                    {
                        report.Carnet.MealTime = new DateTime(rep.dailycarnet.main_meal_time.Value.Year,
                                                              rep.dailycarnet.main_meal_time.Value.Month,
                                                              rep.dailycarnet.main_meal_time.Value.Day,
                                                              rep.dailycarnet.main_meal_time.Value.Hour,
                                                              rep.dailycarnet.main_meal_time.Value.Minute,
                                                              rep.dailycarnet.main_meal_time.Value.Second);
                    }

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
                    report.TemporaryData.Weight = rep.temporarydata.weight;

                    // Measurements del Reporte
                    report.Measures = measurements.Where(m => m.ReportId == report.UdaId)
                                                  .OrderBy(m => m.Time).ToList();

                    lrep.Add(report);
                }

                return lrep;

            }

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

            // Calculo de máximos, mínimos y promedios de Sys, Mid, Dias y HR
            var valid = rep.Measures.Where(m => m.Valid).ToList();

            int sysTotalAvg = (int)Math.Round(valid.Average(m => m.Systolic.Value));
            int sysDayAvg = (int) Math.Round(valid.Where(m => !m.Asleep.Value).Average(m => m.Systolic.Value));
            int sysNightAvg = (int)Math.Round(valid.Where(m => m.Asleep.Value).Average(m => m.Systolic.Value));
            int sysDayMax = valid.Where(m => !m.Asleep.Value).Max(m => m.Systolic.Value);
            int sysNightMax = valid.Where(m => m.Asleep.Value).Max(m => m.Systolic.Value);
            int sysDayMin = valid.Where(m => !m.Asleep.Value).Min(m => m.Systolic.Value);
            int sysNightMin = valid.Where(m => m.Asleep.Value).Min(m => m.Systolic.Value);

            int diasTotalAvg = (int)Math.Round(valid.Average(m => m.Diastolic.Value));
            int diasDayAvg = (int)Math.Round(valid.Where(m => !m.Asleep.Value).Average(m => m.Diastolic.Value));
            int diasNightAvg = (int)Math.Round(valid.Where(m => m.Asleep.Value).Average(m => m.Diastolic.Value));
            int diasDayMax = valid.Where(m => !m.Asleep.Value).Max(m => m.Diastolic.Value);
            int diasNightMax = valid.Where(m => m.Asleep.Value).Max(m => m.Diastolic.Value);
            int diasDayMin = valid.Where(m => !m.Asleep.Value).Min(m => m.Diastolic.Value);
            int diasNightMin = valid.Where(m => m.Asleep.Value).Min(m => m.Diastolic.Value);

            int hrTotalAvg = (int)Math.Round(valid.Average(m => m.HeartRate.Value));
            int hrDayAvg = (int)Math.Round(valid.Where(m => !m.Asleep.Value).Average(m => m.HeartRate.Value));
            int hrNightAvg = (int)Math.Round(valid.Where(m => m.Asleep.Value).Average(m => m.HeartRate.Value));
            int hrDayMax = valid.Where(m => !m.Asleep.Value).Max(m => m.HeartRate.Value);
            int hrNightMax = valid.Where(m => m.Asleep.Value).Max(m => m.HeartRate.Value);
            int hrDayMin = valid.Where(m => !m.Asleep.Value).Min(m => m.HeartRate.Value);
            int hrNightMin = valid.Where(m => m.Asleep.Value).Min(m => m.HeartRate.Value);
            

            using (udaContext = new udahta_dbEntities())
            {
                var lastIdReport = new ObjectParameter("id", typeof(long));
                udaContext.insertReport(lastIdReport, rep.BeginDate, rep.EndDate, rep.Doctor.Name,
                                        rep.Diagnosis, rep.RequestDoctor, rep.RequestDoctorSpeciality,
                                        sysDayAvg, sysNightAvg, sysTotalAvg, sysDayMax, sysNightMax,
                                        diasDayAvg, diasNightAvg, diasTotalAvg, diasDayMax, diasNightMax,
                                        rep.DeviceId, rep.DeviceReportId, rep.TemporaryDataId,
                                        rep.DailyCarnetId, rep.Patient.UdaId,
                                        sysDayMin, diasDayMin, sysNightMin, diasNightMin,
                                        hrTotalAvg, hrDayAvg, hrNightAvg,
                                        hrDayMax, hrNightMax, hrDayMin, hrNightMin);

                //Obtener lista de medidas para insertar en tabla Measurement
                ICollection<Measurement> lmeasure = rep.Measures;

                foreach (Measurement m in lmeasure)
                {
                    udaContext.insertMeasurement(m.Time, m.Systolic, m.Middle, m.Diastolic, m.HeartRate,
                                                 m.Asleep, m.Comment, (long?) lastIdReport.Value,
                                                 rep.Patient.UdaId);
                }
                
            }
            
        }

        public long? insertDailyCarnet(DailyCarnet dCarnet)
        {
            using (udaContext = new udahta_dbEntities())
            {
                ObjectParameter lastIdDailyReport = new ObjectParameter("id", typeof(long));
                udaContext.insertDailyCarnet(lastIdDailyReport, dCarnet.Technician.Name, dCarnet.InitDiastolic1,
                                             dCarnet.InitDiastolic2, dCarnet.InitDiastolic3,
                                             dCarnet.InitHeartRate1, dCarnet.InitHeartRate2, dCarnet.InitHeartRate3,
                                             dCarnet.FinalDiastolic1, dCarnet.FinalDiastolic2, dCarnet.FinalDiastolic3,
                                             dCarnet.FinalHeartRate1, dCarnet.FinalHeartRate2, dCarnet.FinalHeartRate3,
                                             dCarnet.SleepTimeStart, dCarnet.SleepTimeEnd, dCarnet.SleepQuality, dCarnet.MealTime,
                                             dCarnet.InitSystolic1, dCarnet.InitSystolic2, dCarnet.InitSystolic3,
                                             dCarnet.FinalSystolic1, dCarnet.FinalSystolic2, dCarnet.FinalSystolic3
                                             );

                ObjectParameter lastIdCA = new ObjectParameter("id", typeof(int));
                foreach (var compl in dCarnet.Complications)
                {
                    udaContext.insertComplications_Activities(lastIdCA, compl.Time.Hour, compl.Time.Minute, "COMPLICACION",
                                                              (int)lastIdDailyReport.Value, compl.Description);
                }

                ObjectParameter lastIdEff = new ObjectParameter("id", typeof(int));
                foreach (var effort in dCarnet.Efforts)
                {
                    udaContext.insertComplications_Activities(lastIdEff, effort.Time.Hour, effort.Time.Minute, "ACTIVIDAD",
                                                              (int)lastIdDailyReport.Value, effort.Description);
                }

                return (long)lastIdDailyReport.Value;
                
            }
        }

        public TemporaryData GetLastTempData(long patientId)
        {
            using (udaContext = new udahta_dbEntities())
            {
                var tempData = (from r in udaContext.report
                                join td in udaContext.temporarydata on
                                    r.temporarydata_idTemporaryData equals td.idTemporaryData
                                where r.patientuda_idPatientUda == patientId
                                orderby r.begin_date
                                select new TemporaryData
                                    {
                                        IdTemporaryData = td.idTemporaryData,
                                        Age = td.age,
                                        BodyMassIndex = td.body_mass_index,
                                        Diabetic = td.diabetic,
                                        Dyslipidemia = td.dyslipidemia,
                                        FatPercentage = td.fat_percentage,
                                        Height = td.height,
                                        Hypertensive = td.known_hypertensive,
                                        Kcal = td.kcal,
                                        MusclePercentage = td.muscle_percentage,
                                        Smoker = td.smoker,
                                        Weight = td.weight
                                    }).FirstOrDefault();
                
                //if(tempData != null)
                    // AGREGAR LA LISTA DE MEDICAMENTOS

                return tempData;
            }
        }

        public int? insertTemporaryData(TemporaryData temporaryData)
        {
            using (udaContext = new udahta_dbEntities())
            {
                ObjectParameter lastIdTempData = new ObjectParameter("id", typeof(int));
                udaContext.insertTemporaryData(lastIdTempData, temporaryData.Weight, temporaryData.Height, temporaryData.Age,
                                               temporaryData.BodyMassIndex, temporaryData.Smoker, temporaryData.Dyslipidemia,
                                               temporaryData.Diabetic, temporaryData.Hypertensive, temporaryData.FatPercentage,
                                               temporaryData.MusclePercentage, temporaryData.Kcal);

                foreach (var med in temporaryData.LMedicines)
                {
                    insertMedicineDose(med, temporaryData.IdTemporaryData);
                }

                return (int?)lastIdTempData.Value;                    
            }

        }

        public void insertMedicineDose(MedicineDose medicineDose, int idTemporaryData)
        {
            //udaContext.insertMedicineDose(medicineDose.Dose, medicineDose.Drug.Id, idTemporaryData);
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
                using (udaContext = new udahta_dbEntities())
                {
                    udaContext.updatePassword(userName, newPswd);
                    return true;                    
                }
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
                using (udaContext = new udahta_dbEntities())
                {
                    udaContext.insertPatientUda(id);                    
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                throw e;
            }
            
        }

        //Inserta una nueva instancia de la historia clinca del paciente
        public void insertMedicalHistory(long patientId, MedicalRecord medicalRecord)
        {
            using (udaContext = new udahta_dbEntities())
            {
                ObjectParameter lastIdMedicalHistory = new ObjectParameter("id", typeof(int));
                udaContext.insertMedicalHistory(lastIdMedicalHistory, medicalRecord.Illness, medicalRecord.Since,
                                               medicalRecord.Until, medicalRecord.Comment, patientId);                
            }
        }
        
        //Inserta un nuevo usuario en la base de datos
        public void insertUser(int idUsuario, string login, string pass, string rol)
        {
            using (udaContext = new udahta_dbEntities())
            {
                udaContext.insertUser(idUsuario, login, pass, rol);                
            }
        }

        //Inserta un nuevo tipo de droga en la base de datos
        public void insertDrugType(string type)
        {
            using (udaContext = new udahta_dbEntities())
            {
                udaContext.insertDrugType(type);                
            }
        }

        //Inserta una nueva droga en la base de datos
        public void insertDrug(string name, int idDrugTyp)
        {
            using (udaContext = new udahta_dbEntities())
            {
                udaContext.insertDrug(name, idDrugTyp);                
            }
        }


        /*
         * INVESTIGACIONES
         */

        //Listar investigaciones 
        public ICollection<Investigation> listInvestigations()
        {
            using (udaContext = new udahta_dbEntities())
            {
                ICollection<Investigation> list = udaContext.investigation.Select(i => new Investigation(i.idInvestigation, i.name, i.creation_date)).ToList();
                return list;
                

            }
        }

        //Inserta una nueva investigacion en la base de datos
        public int insertInvestigation(string nam, DateTime createDat)
        {
            using (udaContext = new udahta_dbEntities())
            {
                ObjectParameter id = new ObjectParameter("id", typeof(int));
                udaContext.insertInvestigation(id, nam, createDat);
                return Convert.ToInt16(id.Value.ToString());                
            }
        }

        public void addReportToInvestigation(long idPatient, long idReport, int idInvestigation)
        {
            
        }

    }
}
