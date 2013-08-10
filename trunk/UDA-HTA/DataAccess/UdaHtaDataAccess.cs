﻿using System;
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
                                            r.request_doctor,
                                            r.specialty,
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
                                            r.night_sd_hr
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
                            RequestDoctor = qry.request_doctor,
                            RequestDoctorSpeciality = qry.specialty,
                            DiastolicTotalAvg = qry.total_avg_dias,
                            SystolicTotalAvg = qry.total_avg_sys,
                            MiddleDayAvg = qry.day_tam_avg,
                            MiddleNightAvg = qry.night_tam_avg,
                            MiddleTotalAvg = qry.tot_tam_avg,
                            HeartRateDayAvg = qry.day_avg_hr,
                            HeartRateNightAvg = qry.night_avg_hr,
                            HeartRateTotalAvg = qry.tot_avg_hr,
                            StandarDeviationTamNight = qry.night_sd_tam,
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
                                         Retry = m.is_retry,
                                         Comment = m.comment
                                     }).ToList();
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
                                                     Retry = m.is_retry,
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
                var valid = rep.Measures.Where(m => m.Valid).ToList();

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

                using (udaContext = new udahta_dbEntities())
                {
                    lastIdReport = new ObjectParameter("id", typeof (long));
                    udaContext.insertReport(lastIdReport, rep.BeginDate, rep.EndDate, rep.Doctor.Name,
                                            rep.Diagnosis, rep.DiagnosisDate, rep.RequestDoctor,
                                            rep.RequestDoctorSpeciality,
                                            sysDayAvg, sysNightAvg, sysTotalAvg, sysDayMax, sysNightMax,
                                            diasDayAvg, diasNightAvg, diasTotalAvg, diasDayMax, diasNightMax,
                                            rep.DeviceId, rep.DeviceReportId, rep.TemporaryDataId,
                                            rep.DailyCarnetId, rep.Patient.UdaId,
                                            sysDayMin, diasDayMin, sysNightMin, diasNightMin,
                                            hrTotalAvg, hrDayAvg, hrNightAvg,
                                            hrDayMax, hrNightMax, hrDayMin, hrNightMin,
                                            sdSysTotal, sdDiasTotal, sdSysDay, sdDiasDay, sdSysNight, sdDiasNight,
                                            sdMiddleTot, sdTamDay, sdTamNight, sdHrTotal, sdHrDay, sdHrNight,
                                            middleTotalAvg, middleDayAvg, middleNightAvg);

                    //Obtener lista de medidas para insertar en tabla Measurement
                    ICollection<Measurement> lmeasure = rep.Measures;

                    foreach (Measurement m in lmeasure)
                    {
                        udaContext.insertMeasurement(m.Time, m.Systolic, m.Middle, m.Diastolic, m.HeartRate,
                                                     m.Asleep, m.Valid, m.Retry, m.Comment, (long?) lastIdReport.Value,
                                                     rep.Patient.UdaId);
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
                ObjectParameter lastIdDailyReport = new ObjectParameter("id", typeof(long));
                udaContext.insertDailyCarnet(lastIdDailyReport, dCarnet.Technician.Name, dCarnet.InitDiastolic1,
                                             dCarnet.InitDiastolic2, dCarnet.InitDiastolic3,
                                             dCarnet.InitHeartRate1, dCarnet.InitHeartRate2, dCarnet.InitHeartRate3,
                                             dCarnet.FinalDiastolic1, dCarnet.FinalDiastolic2, dCarnet.FinalDiastolic3,
                                             dCarnet.FinalHeartRate1, dCarnet.FinalHeartRate2, dCarnet.FinalHeartRate3,
                                             dCarnet.SleepTimeStart, dCarnet.SleepTimeEnd, dCarnet.SleepQuality,
                                             dCarnet.SleepQualityDescription, dCarnet.MealTime,
                                             dCarnet.InitSystolic1, dCarnet.InitSystolic2, dCarnet.InitSystolic3,
                                             dCarnet.FinalSystolic1, dCarnet.FinalSystolic2, dCarnet.FinalSystolic3);

                ObjectParameter lastIdCA = new ObjectParameter("id", typeof(int));
                foreach (var compl in dCarnet.Complications)
                {
                    udaContext.insertComplications_Activities(lastIdCA, compl.Time.Hour, compl.Time.Minute, "COMPLICACION",
                                                              (long)lastIdDailyReport.Value, compl.Description);
                }

                ObjectParameter lastIdEff = new ObjectParameter("id", typeof(int));
                foreach (var effort in dCarnet.Efforts)
                {
                    udaContext.insertComplications_Activities(lastIdEff, effort.Time.Hour, effort.Time.Minute, "ACTIVIDAD",
                                                              (long)lastIdDailyReport.Value, effort.Description);
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

        public int? InsertTemporaryData(TemporaryData temporaryData)
        {
            using (udaContext = new udahta_dbEntities())
            {
                ObjectParameter lastIdTempData = new ObjectParameter("id", typeof(int));
                udaContext.insertTemporaryData(lastIdTempData, temporaryData.Weight, temporaryData.Height, temporaryData.Age,
                                               temporaryData.BodyMassIndex, temporaryData.Smoker, temporaryData.Dyslipidemia,
                                               temporaryData.Diabetic, temporaryData.Hypertensive, temporaryData.FatPercentage,
                                               temporaryData.MusclePercentage, temporaryData.Kcal);
                // TODO MEDICINE 
                /*foreach (var med in temporaryData.Medication)
                {
                    insertMedicineDose(med, temporaryData.IdTemporaryData);
                }*/

                return (int?)lastIdTempData.Value;                    
            }
        }

        public void InsertMedicineDose(MedicineDose medicineDose, int idTemporaryData)
        {
            //udaContext.insertMedicineDose(medicineDose.Dose, medicineDose.Drug.Id, idTemporaryData);
        }

        //Verifica que existe el nombre de usuario 'userName' en la base de datos y devuelve el password,
        //en caso de no existr devuelvo null
        public string GetPassword(string userName)
        {
            using (udaContext = new udahta_dbEntities())
            {
                ObjectParameter pswd = new ObjectParameter("id", typeof(int));
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
                    try
                    {
                        udaContext.updatePassword(userName, newPswd);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
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
                    try
                    {
                        udaContext.insertPatientUda(id);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
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
                    try
                    {
                        ObjectParameter lastIdMedicalHistory = new ObjectParameter("id", typeof (int));
                        udaContext.insertMedicalHistory(lastIdMedicalHistory, medicalRecord.Illness,
                                                        medicalRecord.Comment, patientId);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                scope.Complete();
            }
        }
        
        //Inserta un nuevo usuario en la base de datos
        public int InsertUser(string login, string pass, string rol)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                ObjectParameter lastIdUser;
                using (udaContext = new udahta_dbEntities())
                {
                    try
                    {
                        lastIdUser = new ObjectParameter("id", typeof (long));
                        udaContext.insertUser(lastIdUser, login, pass, rol);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                scope.Complete();
                return (int)lastIdUser.Value;

            }
            
        }

        //Inserta un nuevo tipo de droga en la base de datos
        public void InsertDrugType(string type)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (udaContext = new udahta_dbEntities())
                {
                    try
                    {
                        udaContext.insertDrugType(type);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                
                scope.Complete();
            }
        }

        //Inserta una nueva droga en la base de datos
        public void InsertDrug(string name, int idDrugTyp)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (udaContext = new udahta_dbEntities())
                {
                    try
                    {
                        udaContext.insertDrug(name, idDrugTyp);
                    }
                    catch (Exception ex)
                    {
                        
                        throw ex;
                    }
                }
                
                scope.Complete();
            }
        }

        public void EditMedicalHistory(MedicalRecord medicalRecord, long patient_id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (udaContext = new udahta_dbEntities())
                {
                    try
                    {
                        udaContext.updateMedicalRecord(medicalRecord.Id, patient_id, medicalRecord.Illness, medicalRecord.Since,
                                                       medicalRecord.Until, medicalRecord.Comment);

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                scope.Complete();
            }
        }

        /*
         * INVESTIGACIONES
         */
        #region Investigaciones

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
            using (TransactionScope scope = new TransactionScope())
            {
                ObjectParameter id;

                using (udaContext = new udahta_dbEntities())
                {
                    try
                    {
                        id = new ObjectParameter("id", typeof(int));
                        udaContext.insertInvestigation(id, nam, createDat);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                scope.Complete();
                return Convert.ToInt16(id.Value.ToString());
            }
        }

        public void addReportToInvestigation(long idPatient, long idReport, int idInvestigation)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (udaContext = new udahta_dbEntities())
                {
                    try
                    {
                        udaContext.insertInvestigationHasReport(idInvestigation, idReport, idPatient);
                    }
                    catch (Exception ex)
                    {
                        
                        throw ex;
                    }
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
                    try
                    {
                        udaContext.deleteInvestigationHasReport(idInvestigation, idReport, idPatient);
                    }
                    catch (Exception ex)
                    {
                        
                        throw ex;
                    }
                }
                
                scope.Complete();
            }
        }

        #endregion
    }
}
