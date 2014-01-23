using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;
using Entities;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace DataAccess
{
    public class UdaHtaDataAccess
    {
        private udahta_dbEntities udaContext;

        public UdaHtaDataAccess()
        {
        }

        public Report GetReport(long idReport)
        {
            using (udaContext = new udahta_dbEntities())
            {
                var qry = udaContext.report.Where(r => r.idReport == idReport)
                                    .Select(r => new
                                        {
                                            r.begin_date,
                                            r.dailycarnet,
                                            r.deviceReportId,
                                            r.diagnosis,
                                            r.diagnosis_date,
                                            r.doctor,
                                            r.end_date,
                                            r.idDevice,
                                            r.measurement,
                                            r.patientuda,
                                            r.idReport,
                                            r.requester,
                                            r.temporarydata,
                                            r.day_avg_dias,
                                            r.day_avg_sys,
                                            r.day_max_dias,
                                            r.day_max_sys,
                                            r.night_avg_dias,
                                            r.night_avg_sys,
                                            r.night_max_dias,
                                            r.night_max_sys,
                                            r.total_avg_dias,
                                            r.total_avg_sys,
                                            r.min_day_hr,
                                            r.min_night_hr,
                                            r.day_min_dias,
                                            r.day_min_sis,
                                            r.night_min_dias,
                                            r.night_min_sis,
                                            r.day_tam_avg,
                                            r.night_tam_avg,
                                            r.tot_tam_avg,
                                            r.day_avg_hr,
                                            r.max_day_hr,
                                            r.max_night_hr,
                                            r.night_avg_hr,
                                            r.tot_avg_hr,
                                            r.day_sd_tam,
                                            r.night_sd_tam,
                                            r.tot_sd_tam,
                                            r.day_sd_dias,
                                            r.night_sd_dias,
                                            r.tot_sd_dias,
                                            r.day_sd_sis,
                                            r.night_sd_sis,
                                            r.tot_sd_sis,
                                            r.tot_sd_hr,
                                            r.day_sd_hr,
                                            r.night_sd_hr,
                                            r.sys_dipping,
                                            r.dias_dipping
                                        }).FirstOrDefault();

                Report rep = null;

                if (qry != null)
                {
                    rep = new Report
                        {
                            BeginDate = qry.begin_date,
                            DiastolicDayAvg = qry.day_avg_dias,
                            SystolicDayAvg = qry.day_avg_sys,
                            DiastolicDayMax = qry.day_max_dias,
                            SystolicDayMax = qry.day_max_sys,
                            DeviceReportId = qry.deviceReportId,
                            Diagnosis = qry.diagnosis,
                            DiagnosisDate = qry.diagnosis_date,
                            EndDate = qry.end_date,
                            DeviceId = qry.idDevice,
                            DiastolicNightAvg = qry.night_avg_dias,
                            SystolicNightAvg = qry.night_avg_sys,
                            DiastolicNightMax = qry.night_max_dias,
                            SystolicNightMax = qry.night_max_sys,
                            Requester = qry.requester,
                            DiastolicTotalAvg = qry.total_avg_dias,
                            SystolicTotalAvg = qry.total_avg_sys,
                            MiddleDayAvg = qry.day_tam_avg,
                            MiddleNightAvg = qry.night_tam_avg,
                            MiddleTotalAvg = qry.tot_tam_avg,
                            HeartRateDayAvg = qry.day_avg_hr,
                            HeartRateNightAvg = qry.night_avg_hr,
                            HeartRateTotalAvg = qry.tot_avg_hr,
                            StandardDeviationTamNight = qry.night_sd_tam,
                            StandardDeviationTamDay = qry.day_sd_tam,
                            StandardDeviationTamTotal = qry.tot_sd_tam,
                            StandardDeviationDiasDay = qry.day_sd_dias,
                            StandardDeviationDiasNight = qry.night_sd_dias,
                            StandardDeviationDiasTotal = qry.tot_sd_dias,
                            StandardDeviationSysDay = qry.day_sd_sis,
                            StandardDeviationSysNight = qry.night_sd_sis,
                            StandardDeviationSysTotal = qry.tot_sd_sis,
                            StandardDeviationHeartRateDay = qry.day_sd_hr,
                            StandardDeviationHeartRateNight = qry.night_sd_hr,
                            StandardDeviationHeartRateTotal = qry.tot_sd_hr,
                            DiastolicDayMin = qry.day_min_dias,
                            DiastolicNightMin = qry.night_min_dias,
                            SystolicDayMin = qry.day_min_sis,
                            SystolicNightMin = qry.night_min_sis,
                            SystolicDipping = qry.sys_dipping,
                            DiastolicDipping = qry.dias_dipping,

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

                    var lmeasures = GetMeasures(idReport);
                    rep.Measures = rep.Measures.Concat(lmeasures).ToList();

                }

                return rep;
            }
        }

        public void UpdateReport(Report r)
        {
            using (udaContext = new udahta_dbEntities())
            {
                udaContext.updateReport(r.UdaId, r.SystolicTotalAvg, r.SystolicDayAvg, r.SystolicNightAvg,
                                        r.DiastolicTotalAvg, r.DiastolicDayAvg, r.DiastolicNightAvg,
                                        r.MiddleTotalAvg, r.MiddleDayAvg, r.MiddleNightAvg,
                                        r.HeartRateTotalAvg, r.HeartRateDayAvg, r.HeartRateNightAvg,
                                        r.StandardDeviationSysTotal, r.StandardDeviationSysDay, r.StandardDeviationSysNight,
                                        r.StandardDeviationDiasTotal, r.StandardDeviationDiasDay, r.StandardDeviationDiasNight,
                                        r.StandardDeviationTamTotal, r.StandardDeviationTamDay, r.StandardDeviationTamNight,
                                        r.StandardDeviationHeartRateTotal, r.StandardDeviationHeartRateDay, r.StandardDeviationHeartRateNight,
                                        r.SystolicDayMax, r.SystolicNightMax, 
                                        r.DiastolicDayMax, r.DiastolicNightMax,
                                        r.HeartRateDayMax, r.HeartRateNightMax,
                                        r.SystolicDayMin, r.SystolicNightMin,
                                        r.DiastolicDayMin, r.DiastolicNightMin,
                                        r.HeartRateDayMin, r.HeartRateNightMin,
                                        r.SystolicDipping, r.DiastolicDipping);
            }
        }

        public ICollection<MedicalRecord> GetMedicalHistory(long idPatient)
        {
            using (udaContext = new udahta_dbEntities())
            {
                return udaContext.medicalhistory
                                 .Where(m => m.patientuda_idPatientUda == idPatient)
                                 .Select(m => new MedicalRecord
                                     {
                                         Comment = m.comment,
                                         Id = m.idMedicalHistory,
                                         Illness = m.illness
                                     }).ToList();
            }
        }

        public ICollection<Measurement> GetMeasures(long idReport)
        {
            using (udaContext = new udahta_dbEntities())
            {
                return udaContext.measurement
                                 .Where(m => m.report_idReport == idReport)
                                 .Select(m => new Measurement
                                     {
                                         Id = m.idMeasurement,
                                         Time = m.date,
                                         Systolic = m.systolic,
                                         Diastolic = m.diastolic,
                                         Middle = m.average,
                                         HeartRate = m.heart_rate,
                                         Asleep = m.sleep,
                                         Valid = m.is_valid,
                                         Retry = m.is_retry.Value,
                                         IsEnabled = m.is_enabled,
                                         Comment = m.comment
                                     }).ToList();
            }
        }

        public void UpdateMeasureInformation(long idMeasure, bool enabled, string comment)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (udaContext = new udahta_dbEntities())
                {
                    udaContext.updateMeasure(idMeasure, enabled, comment);
                }            

                scope.Complete();
            }

        }

        public bool ExistPatient(long? idPatient)
        {
            using (udaContext = new udahta_dbEntities())
            {
                return udaContext.patientuda.Any(p => p.idPatientUda == idPatient);
            }
        }

        public void UpdateDiagnosis(long reportId, string diagnosis, DateTime diagnosisDate, string doctor)
        {
            using (udaContext = new udahta_dbEntities())
            {
                udaContext.updateDiagnosis(reportId, doctor, diagnosis, diagnosisDate);
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

        /*
         * Lista todos los reportes que cumplen con los filtros.
         */
        public ICollection<Report> ListFilteredReport(int? patientLowerAge, int? patientUpperAge, DateTime? reportSinceDate, 
            DateTime? reportUntilDate, bool? isSmoker, bool? isDiabetic, bool? isHipertense, bool? isDysplidemic)
        {
            ICollection<Report> result = new List<Report>();

            using (var udaContext = new udahta_dbEntities())
            {
                var list = udaContext.report.AsQueryable();

                if (reportSinceDate != null && reportUntilDate != null)
                {
                    list = list.Where(r => r.begin_date >= reportSinceDate && r.begin_date <= reportUntilDate);                    
                }
                else
                {
                    if (reportSinceDate != null)
                    {
                        list = list.Where(r => r.begin_date >= reportSinceDate);
                    }
                    if (reportUntilDate != null)
                    {
                        list = list.Where(r => r.begin_date <= reportUntilDate);
                    }

                }

                if (patientLowerAge != null && patientUpperAge != null)
                {
                    //  edad de paciente > lowerAge y < upperAger
                    list = list.Where(r => r.temporarydata.age >= patientLowerAge && r.temporarydata.age <= patientUpperAge);
                }
                else
                {
                    if (patientLowerAge != null)
                    {
                        // edad de paciente > lowerAge
                        list = list.Where(r => r.temporarydata.age >= patientLowerAge);
                    }

                    if (patientUpperAge != null)
                    {
                        // edad de paciente < upperAge
                        list = list.Where(r => r.temporarydata.age <= patientUpperAge);
                    }
                }

                if (isSmoker.Value)
                {
                    list = list.Where(r => r.temporarydata.smoker == true);
                }
                if (isHipertense.Value)
                {
                    list = list.Where(r => r.temporarydata.known_hypertensive == true);
                }
                if (isDiabetic.Value)
                {
                    list = list.Where(r => r.temporarydata.diabetic == true);
                }
                if (isDysplidemic.Value)
                {
                    list = list.Where(r => r.temporarydata.dyslipidemia == true);

                }

                var l = list.Select(r => new
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
                        r.diagnosis_date,
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
                        r.requester,
                        r.temporarydata,
                        r.total_avg_dias,
                        r.total_avg_sys,
                        r.day_tam_avg,
                        r.night_tam_avg,
                        r.tot_tam_avg,
                        r.day_avg_hr,
                        r.night_avg_hr,
                        r.tot_avg_hr,
                        r.night_sd_tam,
                        r.day_sd_tam,
                        r.tot_sd_tam,
                        r.day_sd_dias,
                        r.night_sd_dias,
                        r.tot_sd_dias,
                        r.day_sd_sis,
                        r.night_sd_sis,
                        r.tot_sd_sis,
                        r.day_sd_hr,
                        r.night_sd_hr,
                        r.tot_sd_hr,
                        r.day_min_dias,
                        r.night_min_dias,
                        r.day_min_sis,
                        r.night_min_sis,
                        r.max_day_hr,
                        r.max_night_hr,
                        r.min_day_hr,
                        r.min_night_hr,
                        r.sys_dipping,
                        r.dias_dipping
                    }).ToList();

                foreach (var qry in l)
                {
                    var rep = new Report
                    {
                        BeginDate = qry.begin_date,
                        DiastolicDayAvg = qry.day_avg_dias,
                        SystolicDayAvg = qry.day_avg_sys,
                        DiastolicDayMax = qry.day_max_dias,
                        SystolicDayMax = qry.day_max_sys,
                        DeviceReportId = qry.deviceReportId,
                        Diagnosis = qry.diagnosis,
                        DiagnosisDate = qry.diagnosis_date,
                        EndDate = qry.end_date,
                        DeviceId = qry.idDevice,
                        DiastolicNightAvg = qry.night_avg_dias,
                        SystolicNightAvg = qry.night_avg_sys,
                        DiastolicNightMax = qry.night_max_dias,
                        SystolicNightMax = qry.night_max_sys,
                        Requester = qry.requester,
                        DiastolicTotalAvg = qry.total_avg_dias,
                        SystolicTotalAvg = qry.total_avg_sys,
                        MiddleDayAvg = qry.day_tam_avg,
                        MiddleNightAvg = qry.night_tam_avg,
                        MiddleTotalAvg = qry.tot_tam_avg,
                        HeartRateDayAvg = qry.day_avg_hr,
                        HeartRateNightAvg = qry.night_avg_hr,
                        HeartRateTotalAvg = qry.tot_avg_hr,
                        StandardDeviationTamNight = qry.night_sd_tam,
                        StandardDeviationTamDay = qry.day_sd_tam,
                        StandardDeviationTamTotal = qry.tot_sd_tam,
                        StandardDeviationDiasDay = qry.day_sd_dias,
                        StandardDeviationDiasNight = qry.night_sd_dias,
                        StandardDeviationDiasTotal = qry.tot_sd_dias,
                        StandardDeviationSysDay = qry.day_sd_sis,
                        StandardDeviationSysNight = qry.night_sd_sis,
                        StandardDeviationSysTotal = qry.tot_sd_sis,
                        StandardDeviationHeartRateDay = qry.day_sd_hr,
                        StandardDeviationHeartRateNight = qry.night_sd_hr,
                        StandardDeviationHeartRateTotal = qry.tot_sd_hr,
                        DiastolicDayMin = qry.day_min_dias,
                        DiastolicNightMin = qry.night_min_dias,
                        SystolicDayMin = qry.day_min_sis,
                        SystolicNightMin = qry.night_min_sis,
                        HeartRateDayMax = qry.max_day_hr,
                        HeartRateNightMax = qry.max_night_hr,
                        HeartRateDayMin = qry.min_day_hr,
                        HeartRateNightMin = qry.min_night_hr,
                        SystolicDipping = qry.sys_dipping,
                        DiastolicDipping = qry.dias_dipping,

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

                    var lmeasures = GetMeasures(qry.idReport);
                    rep.Measures = rep.Measures.Concat(lmeasures).ToList();

                    result.Add(rep);
                }

                return result;
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
                                              r.begin_date,
                                              r.dailycarnet,
                                              r.deviceReportId,
                                              r.diagnosis,
                                              r.diagnosis_date,
                                              r.doctor,
                                              r.end_date,
                                              r.idDevice,
                                              r.measurement,
                                              r.patientuda,
                                              r.idReport,
                                              r.requester,
                                              r.temporarydata,
                                              r.day_avg_dias,
                                              r.day_avg_sys,
                                              r.day_max_dias,
                                              r.day_max_sys,
                                              r.night_avg_dias,
                                              r.night_avg_sys,
                                              r.night_max_dias,
                                              r.night_max_sys,
                                              r.total_avg_dias,
                                              r.total_avg_sys,
                                              r.min_day_hr,
                                              r.min_night_hr,
                                              r.day_min_dias,
                                              r.day_min_sis,
                                              r.night_min_dias,
                                              r.night_min_sis,
                                              r.day_tam_avg,
                                              r.night_tam_avg,
                                              r.tot_tam_avg,
                                              r.day_avg_hr,
                                              r.max_day_hr,
                                              r.max_night_hr,
                                              r.night_avg_hr,
                                              r.tot_avg_hr,
                                              r.day_sd_tam,
                                              r.night_sd_tam,
                                              r.tot_sd_tam,
                                              r.day_sd_dias,
                                              r.night_sd_dias,
                                              r.tot_sd_dias,
                                              r.day_sd_sis,
                                              r.night_sd_sis,
                                              r.tot_sd_sis,
                                              r.tot_sd_hr,
                                              r.day_sd_hr,
                                              r.night_sd_hr,
                                              r.sys_dipping,
                                              r.dias_dipping
                                          }).ToList();

                var measurements = udaContext.measurement
                                             .Where(m => m.report_patientuda_idPatientUda == patientId)
                                             .Select(m => new Measurement
                                                 {
                                                     Id = m.idMeasurement,
                                                     ReportId = m.report_idReport,
                                                     Time = m.date,
                                                     Systolic = m.systolic,
                                                     Middle = m.average,
                                                     Diastolic = m.diastolic,
                                                     HeartRate = m.heart_rate,
                                                     Valid = m.is_valid,
                                                     Retry = m.is_retry.Value,
                                                     IsEnabled = m.is_enabled,
                                                     Asleep = m.sleep,
                                                     Comment = m.comment
                                                 }).ToList();

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
                            DiagnosisDate = rep.diagnosis_date,
                            EndDate = rep.end_date,
                            DeviceId = rep.idDevice,
                            DiastolicNightAvg = rep.night_avg_dias,
                            SystolicNightAvg = rep.night_avg_sys,
                            DiastolicNightMax = rep.night_max_dias,
                            SystolicNightMax = rep.night_max_sys,
                            Requester = rep.requester,
                            DiastolicTotalAvg = rep.total_avg_dias,
                            SystolicTotalAvg = rep.total_avg_sys,
                            MiddleDayAvg = rep.day_tam_avg,
                            MiddleNightAvg = rep.night_tam_avg,
                            MiddleTotalAvg = rep.tot_tam_avg,
                            HeartRateDayAvg = rep.day_avg_hr,
                            HeartRateNightAvg = rep.night_avg_hr,
                            HeartRateTotalAvg = rep.tot_avg_hr,
                            StandardDeviationTamNight = rep.night_sd_tam,
                            StandardDeviationTamDay = rep.day_sd_tam,
                            StandardDeviationTamTotal = rep.tot_sd_tam,
                            StandardDeviationDiasDay = rep.day_sd_dias,
                            StandardDeviationDiasNight = rep.night_sd_dias,
                            StandardDeviationDiasTotal = rep.tot_sd_dias,
                            StandardDeviationSysDay = rep.day_sd_sis,
                            StandardDeviationSysNight = rep.night_sd_sis,
                            StandardDeviationSysTotal = rep.tot_sd_sis,
                            StandardDeviationHeartRateDay = rep.day_sd_hr,
                            StandardDeviationHeartRateNight = rep.night_sd_hr,
                            StandardDeviationHeartRateTotal = rep.tot_sd_hr,
                            DiastolicDayMin = rep.day_min_dias,
                            DiastolicNightMin = rep.night_min_dias,
                            SystolicDayMin = rep.day_min_sis,
                            SystolicNightMin = rep.night_min_sis,
                            SystolicDipping = rep.sys_dipping,
                            DiastolicDipping = rep.dias_dipping,

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
                    report.Carnet.SleepQualityDescription = rep.dailycarnet.sleep_comments;

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

                    foreach (var activity in rep.dailycarnet.complications_activities)
                    {
                        if (!activity.time.HasValue)
                        {
                            activity.time = report.BeginDate;
                        }

                        if (activity.specification == "COMPLICACION")
                        {
                            Complication complication = new Complication(activity.time.Value, activity.description);
                            report.Carnet.Complications.Add(complication);
                        }
                        else
                        {
                            //ACTIVIDAD/ESFUERZO
                            Effort effort = new Effort(activity.time.Value,activity.description);
                            report.Carnet.Efforts.Add(effort);
                        }
                    }

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


        public long InsertReport(Report rep)
        {
            ObjectParameter lastIdReport;
            using (TransactionScope transaction = new TransactionScope())
            {
                if (rep.Carnet != null)
                {
                    //si DailyCarnet existe, insertar
                    rep.DailyCarnetId = InsertDailyCarnet(rep.Carnet);
                }

                if (rep.TemporaryData != null)
                {
                    //si TemporaryData existe, insertar
                    rep.TemporaryDataId = InsertTemporaryData(rep.TemporaryData);
                }

                // Calculo de máximos, mínimos y promedios de Sys, Mid, Dias y HR
                var valid = rep.Measures.Where(m => m.Valid && m.IsEnabled).ToList();

                //Sobre el total de medidas
                int sysTotalAvg = (int) Math.Round(valid.Average(m => m.Systolic.Value));
                int diasTotalAvg = (int) Math.Round(valid.Average(m => m.Diastolic.Value));
                int hrTotalAvg = (int) Math.Round(valid.Average(m => m.HeartRate.Value));
                int middleTotalAvg = (int) Math.Round(valid.Average(m => m.Middle.Value));
                decimal sdSysTotal =
                    (decimal)
                    Math.Sqrt(
                        (double)
                        ((valid.Sum(m => (m.Systolic - sysTotalAvg)*(m.Systolic - sysTotalAvg)))/(double) valid.Count));
                decimal sdDiasTotal =
                    (decimal)
                    Math.Sqrt(
                        (double)
                        ((valid.Sum(m => (m.Diastolic - diasTotalAvg)*(m.Diastolic - diasTotalAvg)))/
                         (double) valid.Count));
                decimal sdHrTotal =
                    (decimal)
                    Math.Sqrt(
                        (double)
                        ((valid.Sum(m => (m.HeartRate - hrTotalAvg)*(m.HeartRate - hrTotalAvg)))/(double) valid.Count));
                decimal sdMiddleTot =
                    (decimal)
                    Math.Sqrt(
                        (double)
                        ((valid.Sum(m => (m.Middle - middleTotalAvg)*(m.Middle - middleTotalAvg)))/(double) valid.Count));

                int sysDayAvg = 0;
                int sysDayMax = 0;
                int sysDayMin = 0;

                int diasDayAvg = 0;
                int diasDayMax = 0;
                int diasDayMin = 0;

                int hrDayAvg = 0;
                int hrDayMax = 0;
                int hrDayMin = 0;

                int middleDayAvg = 0;

                //Desviacion estand
                int validDayCount = 0;
                decimal sdSysDay = 0;
                decimal sdDiasDay = 0;
                decimal sdHrDay = 0;
                decimal sdTamDay = 0;

                //Lista de medidas del dia
                var listMeasuresDay = valid.Where(m => !m.Asleep.Value).ToList();
                if (listMeasuresDay.Count() != 0)
                {
                    //Sobre medidas durante el dia
                    sysDayAvg = (int) Math.Round(listMeasuresDay.Average(m => m.Systolic.Value));
                    sysDayMax = listMeasuresDay.Max(m => m.Systolic.Value);
                    sysDayMin = listMeasuresDay.Min(m => m.Systolic.Value);

                    diasDayAvg = (int) Math.Round(listMeasuresDay.Average(m => m.Diastolic.Value));
                    diasDayMax = listMeasuresDay.Max(m => m.Diastolic.Value);
                    diasDayMin = listMeasuresDay.Min(m => m.Diastolic.Value);

                    hrDayAvg = (int) Math.Round(listMeasuresDay.Average(m => m.HeartRate.Value));
                    hrDayMax = listMeasuresDay.Max(m => m.HeartRate.Value);
                    hrDayMin = listMeasuresDay.Min(m => m.HeartRate.Value);

                    middleDayAvg = (int) Math.Round(listMeasuresDay.Average(m => m.Middle.Value));

                    //Desviacion estandar
                    validDayCount = valid.Count(m => !m.Asleep.Value);
                    sdSysDay =
                        (decimal)
                        Math.Sqrt(
                            (double)
                            ((listMeasuresDay.Sum(m => (m.Systolic - sysDayAvg)*(m.Systolic - sysDayAvg)))/
                             (double) validDayCount));
                    sdDiasDay =
                        (decimal)
                        Math.Sqrt(
                            (double)
                            ((listMeasuresDay.Sum(m => (m.Diastolic - diasDayAvg)*(m.Diastolic - diasDayAvg)))/
                             (double) validDayCount));
                    sdHrDay =
                        (decimal)
                        Math.Sqrt(
                            (double)
                            ((listMeasuresDay.Sum(m => (m.HeartRate - hrDayAvg)*(m.HeartRate - hrDayAvg)))/
                             (double) validDayCount));
                    sdTamDay =
                        (decimal)
                        Math.Sqrt(
                            (double)
                            ((listMeasuresDay.Sum(m => (m.Middle - middleDayAvg)*(m.Middle - middleDayAvg)))/
                             (double) validDayCount));

                }

                int sysNightAvg = 0;
                int sysNightMax = 0;
                int sysNightMin = 0;

                int diasNightAvg = 0;
                int diasNightMax = 0;
                int diasNightMin = 0;

                int hrNightAvg = 0;
                int hrNightMax = 0;
                int hrNightMin = 0;

                int middleNightAvg = 0;

                //Desviacion estandar
                int validNightCount = 0;
                decimal sdSysNight = 0;
                decimal sdDiasNight = 0;
                decimal sdTamNight = 0;
                decimal sdHrNight = 0;

                //Lista de medidas de la noche
                var listMeasuresNight = valid.Where(m => m.Asleep.Value).ToList();

                if (listMeasuresNight.Count() != 0)
                {
                    //Sobre medidas durante la noche
                    sysNightAvg = (int) Math.Round(listMeasuresNight.Average(m => m.Systolic.Value));
                    sysNightMax = listMeasuresNight.Max(m => m.Systolic.Value);
                    sysNightMin = listMeasuresNight.Min(m => m.Systolic.Value);

                    diasNightAvg = (int) Math.Round(listMeasuresNight.Average(m => m.Diastolic.Value));
                    diasNightMax = listMeasuresNight.Max(m => m.Diastolic.Value);
                    diasNightMin = listMeasuresNight.Min(m => m.Diastolic.Value);

                    hrNightAvg = (int) Math.Round(listMeasuresNight.Average(m => m.HeartRate.Value));
                    hrNightMax = listMeasuresNight.Max(m => m.HeartRate.Value);
                    hrNightMin = listMeasuresNight.Min(m => m.HeartRate.Value);

                    middleNightAvg = (int) Math.Round(listMeasuresNight.Average(m => m.Middle.Value));

                    //Desviacion estandar
                    validNightCount = listMeasuresNight.Count();
                    sdSysNight = (decimal) Math.Sqrt((double) ((listMeasuresNight
                                                                   .Sum(m =>
                                                                        (m.Systolic - sysNightAvg)*
                                                                        (m.Systolic - sysNightAvg)))/
                                                               (double) validNightCount));
                    sdDiasNight = (decimal) Math.Sqrt((double) ((listMeasuresNight
                                                                    .Sum(m =>
                                                                         (m.Diastolic - diasNightAvg)*
                                                                         (m.Diastolic - diasNightAvg)))/
                                                                (double) validNightCount));
                    sdHrNight = (decimal) Math.Sqrt((double) ((listMeasuresNight
                                                                  .Sum(m =>
                                                                       (m.HeartRate - hrNightAvg)*
                                                                       (m.HeartRate - hrNightAvg)))/
                                                              (double) validNightCount));
                    sdTamNight = (decimal) Math.Sqrt((double) ((listMeasuresNight
                                                                   .Sum(m =>
                                                                        (m.Middle - middleNightAvg)*
                                                                        (m.Middle - middleNightAvg)))/
                                                               (double) validNightCount));
                }

                decimal? sysDipping = null;
                decimal? diasDipping = null;

                if (listMeasuresDay.Count() > 0 && listMeasuresNight.Count() > 0)
                {
                    sysDipping = (sysDayAvg - sysNightAvg) / (decimal)sysDayAvg;
                    diasDipping = (diasDayAvg - diasNightAvg) / (decimal)diasDayAvg;
                }

                using (udaContext = new udahta_dbEntities())
                {
                    lastIdReport = new ObjectParameter("id", typeof (long));
                    udaContext.insertReport(lastIdReport, rep.BeginDate, rep.EndDate, rep.Doctor.Name,
                                            rep.Diagnosis, rep.DiagnosisDate, rep.Requester, rep.Specialty,
                                            sysDayAvg, sysNightAvg, sysTotalAvg, sysDayMax, sysNightMax,
                                            diasDayAvg, diasNightAvg, diasTotalAvg, diasDayMax, diasNightMax,
                                            rep.DeviceId, rep.DeviceReportId, rep.TemporaryDataId,
                                            rep.DailyCarnetId, rep.Patient.UdaId,
                                            sysDayMin, diasDayMin, sysNightMin, diasNightMin,
                                            hrTotalAvg, hrDayAvg, hrNightAvg,
                                            hrDayMax, hrNightMax, hrDayMin, hrNightMin,
                                            sdSysTotal, sdDiasTotal, sdSysDay, sdDiasDay, sdSysNight, sdDiasNight,
                                            sdMiddleTot, sdTamDay, sdTamNight, sdHrTotal, sdHrDay, sdHrNight,
                                            middleTotalAvg, middleDayAvg, middleNightAvg, sysDipping, diasDipping);

                    //Obtener lista de medidas para insertar en tabla Measurement
                    foreach (Measurement m in rep.Measures)
                    {
                        ObjectParameter lastIdMeasure = new ObjectParameter("id", typeof (long));
                        //m.IsEnabled, al momento de insertar un conjunto de medidas, siempre es true
                        m.IsEnabled = true;
                        udaContext.insertMeasurement(lastIdMeasure, m.Time, m.Systolic, m.Middle, m.Diastolic, m.HeartRate,
                                                     m.Asleep, m.Valid, m.Retry, m.IsEnabled, m.Comment, (long?) lastIdReport.Value,
                                                     rep.Patient.UdaId);
                        m.Id = (long) lastIdMeasure.Value;
                    }
                }

                transaction.Complete();
                return (long) lastIdReport.Value;
            }
        }

        public long? InsertDailyCarnet(DailyCarnet dCarnet)
        {
            using (udaContext = new udahta_dbEntities())
            {
                ObjectParameter lastIdDailyReport = new ObjectParameter("id", typeof (long));
                udaContext.insertDailyCarnet(lastIdDailyReport, dCarnet.Technician.Name, dCarnet.InitDiastolic1,
                                             dCarnet.InitDiastolic2, dCarnet.InitDiastolic3,
                                             dCarnet.InitHeartRate1, dCarnet.InitHeartRate2, dCarnet.InitHeartRate3,
                                             dCarnet.FinalDiastolic1, dCarnet.FinalDiastolic2, dCarnet.FinalDiastolic3,
                                             dCarnet.FinalHeartRate1, dCarnet.FinalHeartRate2, dCarnet.FinalHeartRate3,
                                             dCarnet.SleepTimeStart, dCarnet.SleepTimeEnd, dCarnet.SleepQuality,
                                             dCarnet.SleepQualityDescription, dCarnet.MealTime,
                                             dCarnet.InitSystolic1, dCarnet.InitSystolic2, dCarnet.InitSystolic3,
                                             dCarnet.FinalSystolic1, dCarnet.FinalSystolic2, dCarnet.FinalSystolic3);

                ObjectParameter lastIdCA = new ObjectParameter("id", typeof (int));
                foreach (var compl in dCarnet.Complications)
                {
                    udaContext.insertComplications_Activities(lastIdCA, compl.Time,
                                                              "COMPLICACION",
                                                              (long) lastIdDailyReport.Value, compl.Description);
                }

                ObjectParameter lastIdEff = new ObjectParameter("id", typeof (int));
                foreach (var effort in dCarnet.Efforts)
                {
                    udaContext.insertComplications_Activities(lastIdEff, effort.Time,
                                                              "ACTIVIDAD",
                                                              (long) lastIdDailyReport.Value, effort.Description);
                }

                return (long) lastIdDailyReport.Value;

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

        public int? InsertTemporaryData(TemporaryData temporaryData)
        {
            using (udaContext = new udahta_dbEntities())
            {
                ObjectParameter lastIdTempData = new ObjectParameter("id", typeof (int));
                udaContext.insertTemporaryData(lastIdTempData, temporaryData.Weight, temporaryData.Height,
                                               temporaryData.Age,
                                               temporaryData.BodyMassIndex, temporaryData.Smoker,
                                               temporaryData.Dyslipidemia,
                                               temporaryData.Diabetic, temporaryData.Hypertensive,
                                               temporaryData.FatPercentage,
                                               temporaryData.MusclePercentage, temporaryData.Kcal);

                temporaryData.IdTemporaryData = (int) lastIdTempData.Value;
                foreach (var med in temporaryData.Medication)
                {
                    var medicineDose = new MedicineDose();
                    medicineDose.Dose = med.Dose;
                    medicineDose.Drug = med.Drug;
                    medicineDose.Time = med.Time;
                    InsertMedicineDose(medicineDose, temporaryData.IdTemporaryData);
                }

                return (int) lastIdTempData.Value;
            }
        }

        public void InsertMedicineDose(MedicineDose medicineDose, int idTemporaryData)
        {
            udaContext.insertMedicineDose(medicineDose.Dose, medicineDose.Time, medicineDose.Drug.Id, idTemporaryData);
        }



        public User GetUser(string userName)
        {
            using (udaContext = new udahta_dbEntities())
            {
                return udaContext.user
                                 .Where(u => u.login.Equals(userName))
                                 .Select(u => new User
                                     {
                                         Login = u.login,
                                         Name = u.name,
                                         Password = u.password,
                                         Role = u.rol
                                     }).FirstOrDefault();
            }
        } 

        //Verifica que existe el nombre de usuario 'userName' en la base de datos y devuelve el password,
        //en caso de no existr devuelvo null
        public string GetPassword(string userName)
        {
            using (udaContext = new udahta_dbEntities())
            {
                ObjectParameter pswd = new ObjectParameter("pass_var", typeof(string));
                udaContext.getPassword(pswd, userName);

                return pswd.Value.ToString();
            }
        }

        //Actualiza la contrasena del usuario userName
        public void UpdatePassword(string userName, string newPswd)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (udaContext = new udahta_dbEntities())
                {
                    udaContext.updatePassword(userName, newPswd);
                }

                scope.Complete();
            }
        }

        //Inserta la referencia a la base de pacientes
        public void InsertPatientUda(long id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (udaContext = new udahta_dbEntities())
                {
                    udaContext.insertPatientUda(id);
                }

                scope.Complete();
            }
        }

        //Inserta una nueva instancia de la historia clinca del paciente
        public void InsertMedicalHistory(long patientId, MedicalRecord medicalRecord)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (udaContext = new udahta_dbEntities())
                {
                    ObjectParameter lastIdMedicalHistory = new ObjectParameter("id", typeof (int));
                    udaContext.insertMedicalHistory(lastIdMedicalHistory, medicalRecord.Illness,
                                                    medicalRecord.Comment, patientId);
                }

                scope.Complete();
            }
        }

        //Borra una instancia de la historia clinca del paciente
        public void DeleteMedicalHistory(long patientId, long medicalRecordId)
        {
            using (udaContext = new udahta_dbEntities())
            {
                udaContext.deleteMedicalHistory(patientId, medicalRecordId);
            }
        }


        #region Users
        //Inserta un nuevo usuario en la base de datos
        public int InsertUser(string login, string pass, string rol, string name)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                ObjectParameter lastIdUser;
                using (udaContext = new udahta_dbEntities())
                {
                    lastIdUser = new ObjectParameter("id", typeof (long));
                    udaContext.insertUser(lastIdUser, login, pass, rol, name);
                }

                scope.Complete();
                return (int) lastIdUser.Value;

            }

        }

        //Eliminar usuario
        public void DeleteUSer(User usr)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (udaContext = new udahta_dbEntities())
                {
                    udaContext.deleteUser(usr.Login);
                }
                scope.Complete();
            }
        }

        //Editar usuario
        public void EditUSer(User usr)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (udaContext = new udahta_dbEntities())
                {
                    udaContext.updateUser(usr.Id, usr.Login, usr.Name, usr.Role);
                }
                scope.Complete();
            }
        }

        public ICollection<User> GetUsers(string name, string role, string login)
        {
            using (udaContext = new udahta_dbEntities())
            {
                var list = udaContext.user.AsQueryable();

                if (!string.IsNullOrWhiteSpace(name))
                {
                    list = list.Where(u => u.name.Contains(name));
                }
                if (!string.IsNullOrWhiteSpace(role))
                {
                    list = list.Where(u => u.rol.Contains(role));
                }
                if (!string.IsNullOrWhiteSpace(login))
                {
                    list = list.Where(u => u.login.Contains(login));
                }
                
                return list.Select(u => new User
                                    {
                                        Id = u.idUser,
                                        Login = u.login,
                                        Name = u.name,
                                        Password = u.password,
                                        Role = u.rol
                                    }).ToList();
            }
        } 

    #endregion

    #region Drugs
        //Inserta un nuevo tipo de droga en la base de datos
        public void InsertDrugType(string type)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (udaContext = new udahta_dbEntities())
                {
                    udaContext.insertDrugType(type);
                }

                scope.Complete();
            }
        }

        //Inserta una nueva droga en la base de datos
        public void InsertDrug(string drugType, string active, string name)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (udaContext = new udahta_dbEntities())
                {
                    var type = udaContext.drugtype.FirstOrDefault(d => d.type == drugType);
                    udaContext.insertDrug(name, active, type.idDrugType);
                }

                scope.Complete();
            }
        }

        //Actualiza los datos de una droga
        public void EditDrug(int id, string name, string drugType, string active)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (udaContext = new udahta_dbEntities())
                {
                    var type = udaContext.drugtype.FirstOrDefault(d => d.type == drugType);
                    udaContext.updateDrug(id, name, active, type.idDrugType);
                }

                scope.Complete();
            }
            
        }
        
        //Obtener tipos de drogas
        public ICollection<string> GetDrugTypes()
        {
            ICollection<string> types = new List<string>();
            using (TransactionScope scope = new TransactionScope())
            {
                using (udaContext = new udahta_dbEntities())
                {
                    types = udaContext.drugtype.Select(d => d.type).ToList();
                }

                scope.Complete();
                return types;
            }            
        } 

        public ICollection<Drug> GetDrugs(string type, string active, string name)
        {
            using (udaContext = new udahta_dbEntities())
            {
                var qry = udaContext.drug.AsQueryable();

                if (!string.IsNullOrWhiteSpace(type))
                    qry = qry.Where(d => d.drugtype.type.Contains(type));
                if(!string.IsNullOrWhiteSpace(active))
                    qry = qry.Where(d => d.active.Contains(active));
                if (!string.IsNullOrWhiteSpace(name))
                    qry = qry.Where(d => d.name.Contains(name));

                return qry.Select(d => new Drug
                                    {
                                        Id = d.idDrug,
                                        Name = d.name,
                                        Active = d.active,
                                        Category = d.drugtype.type
                                    }).ToList();
            }
        }

    #endregion

        public void EditMedicalHistory(MedicalRecord medicalRecord, long patient_id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (udaContext = new udahta_dbEntities())
                {
                    udaContext.updateMedicalRecord(medicalRecord.Id, patient_id, medicalRecord.Illness, medicalRecord.Comment);
                }

                scope.Complete();
            }
        }

        public Limits GetLimits()
        {
            using (udaContext = new udahta_dbEntities())
            {
                var limits = udaContext.limitmeasure.Select(l => new Limits
                    {
                        MaxDiasDay = l.maxdiasday.Value,
                        MaxDiasNight = l.maxdiasnight.Value,
                        MaxDiasTotal = l.maxdiastotal.Value,
                        MaxSysDay = l.maxsysday.Value,
                        MaxSysNight = l.maxsysnight.Value,
                        MaxSysTotal = l.maxsystotal.Value,

                        HiDiasDay = l.highdiasday,
                        HiDiasNight = l.highdiasnight,
                        HiDiasTotal = l.highdiastotal,
                        HiSysDay = l.highsysday,
                        HiSysNight = l.highsysnight,
                        HiSysTotal = l.highsystotal

                        /*LoDiasDay = l.lowdiasday,
                        LoDiasNight = l.lowdiasnight,
                        LoDiasTotal = l.lowdiastotal,
                        LoSysDay = l.lowsysday,
                        LoSysNight = l.lowsysnight,
                        LoSysTotal = l.lowsystotal*/
                    }).FirstOrDefault();

                return limits;
            }
        }


        /*
         * INVESTIGACIONES
         */

        #region Investigaciones

        //Obtener investigacion
        public Investigation GetInvestigation(int id)
        {
            using (udaContext = new udahta_dbEntities())
            {
                var investigation = udaContext.investigation.Where(i => i.idInvestigation == id).Select(i => new 
                    {
                        i.idInvestigation,
                        i.comment,
                        i.creation_date,
                        i.name,
                        i.report
                        
                    }).FirstOrDefault();

                if (investigation != null)
                {
                    Investigation result = new Investigation(investigation.idInvestigation,investigation.name,investigation.creation_date,investigation.comment);

                    foreach (var qry in investigation.report)
                    {
                        var rep = new Report
                        {
                            BeginDate = qry.begin_date,
                            DiastolicDayAvg = qry.day_avg_dias,
                            SystolicDayAvg = qry.day_avg_sys,
                            DiastolicDayMax = qry.day_max_dias,
                            SystolicDayMax = qry.day_max_sys,
                            DeviceReportId = qry.deviceReportId,
                            Diagnosis = qry.diagnosis,
                            DiagnosisDate = qry.diagnosis_date,
                            EndDate = qry.end_date,
                            DeviceId = qry.idDevice,
                            DiastolicNightAvg = qry.night_avg_dias,
                            SystolicNightAvg = qry.night_avg_sys,
                            DiastolicNightMax = qry.night_max_dias,
                            SystolicNightMax = qry.night_max_sys,
                            Requester = qry.requester,
                            DiastolicTotalAvg = qry.total_avg_dias,
                            SystolicTotalAvg = qry.total_avg_sys,
                            MiddleDayAvg = qry.day_tam_avg,
                            MiddleNightAvg = qry.night_tam_avg,
                            MiddleTotalAvg = qry.tot_tam_avg,
                            HeartRateDayAvg = qry.day_avg_hr,
                            HeartRateNightAvg = qry.night_avg_hr,
                            HeartRateTotalAvg = qry.tot_avg_hr,
                            StandardDeviationTamNight = qry.night_sd_tam,
                            StandardDeviationTamDay = qry.day_sd_tam,
                            StandardDeviationTamTotal = qry.tot_sd_tam,
                            StandardDeviationDiasDay = qry.day_sd_dias,
                            StandardDeviationDiasNight = qry.night_sd_dias,
                            StandardDeviationDiasTotal = qry.tot_sd_dias,
                            StandardDeviationSysDay = qry.day_sd_sis,
                            StandardDeviationSysNight = qry.night_sd_sis,
                            StandardDeviationSysTotal = qry.tot_sd_sis,
                            StandardDeviationHeartRateDay = qry.day_sd_hr,
                            StandardDeviationHeartRateNight = qry.night_sd_hr,
                            StandardDeviationHeartRateTotal = qry.tot_sd_hr,
                            DiastolicDayMin = qry.day_min_dias,
                            DiastolicNightMin = qry.night_min_dias,
                            SystolicDayMin = qry.day_min_sis,
                            SystolicNightMin = qry.night_min_sis,
                            SystolicDipping = qry.sys_dipping,
                            DiastolicDipping = qry.dias_dipping,

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

                        var lmeasures = GetMeasures(qry.idReport);
                        rep.Measures = rep.Measures.Concat(lmeasures).ToList();
                        
                        result.LReports.Add(rep);
                    }

                    return result;                
                }
            }

            var exception = new Exception("No se ha encontrado la investigacion");
            throw exception;
        }

        //Listar investigaciones 
        public ICollection<InvestigationSearch> ListInvestigations(int? id, string name, DateTime? creationDate)
        {
            using (udaContext = new udahta_dbEntities())
            {
                var list = udaContext.investigation.AsQueryable();

                if (id.HasValue)
                {
                    list = list.Where(i => i.idInvestigation == id.Value);                    
                }
                if (!string.IsNullOrWhiteSpace(name))
                {
                    list = list.Where(i => i.name.Contains(name));
                }
                if (creationDate.HasValue)
                {
                    list = list.Where(i => i.creation_date == creationDate.Value);    
                }

                return list.Select(i => 
                    new InvestigationSearch
                        {
                            IdInvestigation = i.idInvestigation,
                            Name = i.name,
                            CreationDate = i.creation_date,
                            Comment = i.comment
                        }).ToList();
            }
        }

        //Inserta una nueva investigacion en la base de datos
        public int insertInvestigation(string nam, DateTime createDat, string comment)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                ObjectParameter id;

                using (udaContext = new udahta_dbEntities())
                {
                    id = new ObjectParameter("id", typeof (int));
                    udaContext.insertInvestigation(id, nam, createDat, comment);
                }

                scope.Complete();
                return Convert.ToInt16(id.Value.ToString());
            }
        }

        public void addReportToInvestigation(long idReport, long idPatient, int idInvestigation)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (udaContext = new udahta_dbEntities())
                {
                    udaContext.insertInvestigationHasReport(idInvestigation, idReport, idPatient);
                }

                scope.Complete();
            }
        }

        public void deleteReportFromInvestigation(long idPatient, long idReport, int idInvestigation)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (udaContext = new udahta_dbEntities())
                {
                    udaContext.deleteInvestigationHasReport(idInvestigation, idReport, idPatient);
                }

                scope.Complete();
            }
        }

        public void editInvestigation(Investigation investigation)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (udaContext = new udahta_dbEntities())
                {
                    udaContext.updateInvestigation(investigation.IdInvestigation, investigation.Name,investigation.CreationDate,investigation.Comment);
                }

                scope.Complete();
            }
            
        }

        #endregion
    }
}
