using System;
using System.Collections.Generic;
using System.Linq;
using DeviceDataAccess;
using Entities;

namespace SpacelabsDataAccess
{
    public class Spacelabs : IDeviceDataAccess
    {
        private const int deviceId = 1;
        private const string deviceName = "Spacelabs";
        private ABPEntities _db;

        public Spacelabs()
        {
            _db = new ABPEntities();
        }
        public void ConnectToDataBase()
        {
            //_db.Connection.Open();
        }
        public void CloseConnectionDataBase()
        {
            //_db.Connection.Close();
        }

        public Report GetReport(string idReport)
        {
            Guid id = Guid.Parse(idReport);
            Report report = null;

            using (_db = new ABPEntities())
            {
                var r = (from t in _db.tblAbpTest
                         join st in _db.tblAbpTestSummaryResults on t.TestId equals st.TestId
                         join sd in _db.tblAbpTestSummaryResults on t.TestId equals sd.TestId
                         join sn in _db.tblAbpTestSummaryResults on t.TestId equals sn.TestId
                         join p in _db.tblSysPatient on t.PatientId equals p.PatientId
                         join dtmp in _db.tblSysUsers on t.ConfirmUser1Id equals dtmp.UserId into temp1 
                         join mtmp in _db.tblAbpTestMedication on t.TestId equals mtmp.TestId into m
                         from d in temp1.DefaultIfEmpty()
                         where t.TestId == id && st.SummaryType == 1 &&
                               sd.SummaryType == 2 && sn.SummaryType == 3
                         select new
                             {
                                 t.TestId,
                                 t.HookupStartTime,
                                 t.HookupEndTime,
                                 t.Age,
                                 Weight = t.WtAtHookup,
                                 Height = (decimal) (t.HtAtHookup/(decimal) 100),
                                 SystolicDipping = t.SysDipping,
                                 DiastolicDipping = t.DiaDipping,
                                 Diagnosis = t.Interpretation,
                                 DiagnosisDate = t.ConfirmDate1,
                                 DiagnosisDoctorName = d.FirstName,
                                 DiagnosisDoctorLastname = d.LastName,

                                 SystolicTotalAvg = st.SystolicAvg,
                                 SystolicTotalSD = st.SystolicStandardDeviation,
                                 DiastolicTotalAvg = st.DiastolicAvg,
                                 DiastolicTotalSD = st.DiastolicStandardDeviation,
                                 MiddleTotalAvg = st.MapAvg,
                                 MiddleTotalSD = st.MapStandardDeviation,
                                 HRTotalAvg = st.HrAvg,
                                 HRTotalSD = st.HrStandardDeviation,

                                 SleepTimeEnd = sd.StartTime,
                                 SystolicDayMin = sd.SystolicMin,
                                 SystolicDayMax = sd.SystolicMax,
                                 SystolicDayAvg = sd.SystolicAvg,
                                 SystolicDaySD = sd.SystolicStandardDeviation,
                                 SystolicDayMaxTime = sd.SystolicMaxTime,
                                 SystolicDayMinTime = sd.SystolicMintime,
                                 DiastolicDayMin = sd.DiastolicMin,
                                 DiastolicDayMax = sd.DiastolicMax,
                                 DiastolicDayAvg = sd.DiastolicAvg,
                                 DiastolicDaySD = sd.DiastolicStandardDeviation,
                                 DiastolicDayMaxTime = sd.DiastolicMaxTime,
                                 DiastolicDayMinTime = sd.DiastolicMintime,
                                 MiddleDayAvg = sd.MapAvg,
                                 MiddleDaySD = sd.MapStandardDeviation,
                                 HRDayMin = sd.HrMin,
                                 HRDayMax = sd.HrMax,
                                 HRDayAvg = sd.HrAvg,
                                 HRDaySD = sd.HrStandardDeviation,
                                 HRDayMaxTime = sd.HrMaxTime,
                                 HRDayMinTime = sd.HrMintime,

                                 SleepTimeStart = sn.StartTime,
                                 SystolicNightMin = sn.SystolicMin,
                                 SystolicNightMax = sn.SystolicMax,
                                 SystolicNightAvg = sn.SystolicAvg,
                                 SystolicNightSD = sn.SystolicStandardDeviation,
                                 SystolicNightMaxTime = sn.SystolicMaxTime,
                                 SystolicNightMinTime = sn.SystolicMintime,
                                 DiastolicNightMin = sn.DiastolicMin,
                                 DiastolicNightMax = sn.DiastolicMax,
                                 DiastolicNightAvg = sn.DiastolicAvg,
                                 DiastolicNightSD = sn.DiastolicStandardDeviation,
                                 DiastolicNightMaxTime = sn.DiastolicMaxTime,
                                 DiastolicNightMinTime = sn.DiastolicMintime,
                                 MiddleNightAvg = sn.MapAvg,
                                 MiddleNightSD = sn.MapStandardDeviation,
                                 HRNightMin = sn.HrMin,
                                 HRNightMax = sn.HrMax,
                                 HRNightAvg = sn.HrAvg,
                                 HRNightSD = sn.HrStandardDeviation,
                                 HRNightMaxTime = sn.HrMaxTime,
                                 HRNightMinTime = sn.HrMintime,

                                 p.PatientId,
                                 p.FirstName,
                                 p.LastName,
                                 p.SecondLastName,
                                 CI = p.MRN,
                                 p.BirthDate,
                                 p.GenderId,
                                 p.Street1,
                                 p.Street2,
                                 p.City,
                                 p.DayPhone,
                                 p.PriEmailId,

                                 p.EmergencyContact,
                                 p.EmergencyPhone,
                                 
                                 Medications = m
                             }).FirstOrDefault();

                if (r != null)
                {
                    report = new Report
                        {
                            DeviceId = deviceId,
                            DeviceReportId = r.TestId.ToString(),
                            BeginDate = r.HookupStartTime,
                            EndDate = r.HookupEndTime,
                            Patient = new Patient
                                {
                                    Names = r.FirstName,
                                    Surnames = (r.LastName + " " + r.SecondLastName).Trim(),
                                    DocumentId = r.CI,
                                    BirthDate = r.BirthDate,
                                    Sex = (SexType?) r.GenderId - 1,
                                    Address = (r.Street1 + " " + r.Street2).Trim(),
                                    // Neighbourhood
                                    City = r.City,
                                    // Department
                                    Phone = r.DayPhone,
                                    Email = r.PriEmailId,
                                },

                            TemporaryData = new TemporaryData
                                {
                                    Age = r.Age,
                                    Weight = r.Weight,
                                    Height = r.Height
                                },

                            Carnet = new DailyCarnet
                                {
                                    SleepTimeStart = r.SleepTimeStart,
                                    SleepTimeEnd = r.SleepTimeEnd
                                },

                            SystolicTotalAvg = r.SystolicTotalAvg,
                            StandardDeviationSysTotal = r.SystolicTotalSD,
                            DiastolicTotalAvg = r.DiastolicTotalAvg,
                            StandardDeviationDiasTotal = r.DiastolicTotalSD,
                            MiddleTotalAvg = r.MiddleTotalAvg,
                            StandardDeviationTamTotal = r.MiddleTotalSD,
                            HeartRateTotalAvg = r.HRTotalAvg,
                            StandardDeviationHeartRateTotal = r.HRTotalSD,

                            SystolicDayAvg = r.SystolicDayAvg,
                            SystolicDayMax = r.SystolicDayMax,
                            SystolicDayMin = r.SystolicDayMin,
                            StandardDeviationSysDay = r.SystolicDaySD,
                            SystolicDayMaxTime = r.SystolicDayMaxTime,
                            SystolicDayMinTime = r.SystolicDayMinTime,
                            DiastolicDayAvg = r.DiastolicDayAvg,
                            DiastolicDayMax = r.DiastolicDayMax,
                            DiastolicDayMin = r.DiastolicDayMin,
                            StandardDeviationDiasDay = r.DiastolicDaySD,
                            DiastolicDayMaxTime = r.DiastolicDayMaxTime,
                            DiastolicDayMinTime = r.DiastolicDayMinTime,
                            MiddleDayAvg = r.MiddleDayAvg,
                            StandardDeviationTamDay = r.MiddleDaySD,
                            HeartRateDayAvg = r.HRDayAvg,
                            HeartRateDayMax = r.HRDayMax,
                            HeartRateDayMin = r.HRDayMin,
                            StandardDeviationHeartRateDay = r.HRDaySD,
                            HeartRateDayMaxTime = r.HRDayMaxTime,
                            HeartRateDayMinTime = r.HRDayMinTime,

                            SystolicNightAvg = r.SystolicNightAvg,
                            SystolicNightMax = r.SystolicNightMax,
                            SystolicNightMin = r.SystolicNightMin,
                            StandardDeviationSysNight = r.SystolicNightSD,
                            SystolicNightMaxTime = r.SystolicNightMaxTime,
                            SystolicNightMinTime = r.SystolicNightMinTime,
                            DiastolicNightAvg = r.DiastolicNightAvg,
                            DiastolicNightMax = r.DiastolicNightMax,
                            DiastolicNightMin = r.DiastolicNightMin,
                            StandardDeviationDiasNight = r.DiastolicNightSD,
                            DiastolicNightMaxTime = r.DiastolicNightMaxTime,
                            DiastolicNightMinTime = r.DiastolicNightMinTime,
                            MiddleNightAvg = r.MiddleNightAvg,
                            StandardDeviationTamNight = r.MiddleNightSD,
                            HeartRateNightAvg = r.HRNightAvg,
                            HeartRateNightMax = r.HRNightMax,
                            HeartRateNightMin = r.HRNightMin,
                            StandardDeviationHeartRateNight = r.HRNightSD,
                            HeartRateNightMaxTime = r.HRNightMaxTime,
                            HeartRateNightMinTime = r.HRNightMinTime,

                            SystolicDipping = r.SystolicDipping,
                            DiastolicDipping = r.DiastolicDipping,

                            Diagnosis = r.Diagnosis,
                            DiagnosisDate = r.DiagnosisDate,
                            Doctor = r.DiagnosisDoctorName + " " + r.DiagnosisDoctorLastname
                        };

                    report.Patient.EmergencyContactList
                          .Add(new EmergencyContact
                              {
                                  Name = r.EmergencyContact,
                                  Phone = r.EmergencyPhone
                              });
                    report.Patient.DeviceReferences.Add(new DeviceReference(deviceId, r.PatientId.ToString()));

                    // TODO OBTENER LA MEDICACIÓN
                    //r.Medications
                }
            }

            return report;
        }

        public List<Measurement> GetMeasures(Report report)
        {
            var sleepStart = new TimeSpan();
            var sleepEnd = new TimeSpan();
            Guid testId = Guid.Parse(report.DeviceReportId);
            bool sleepTimeValid = report.Carnet.SleepTimeStart.HasValue
                                  && report.Carnet.SleepTimeEnd.HasValue;

            if (sleepTimeValid)
            {
                sleepStart = report.Carnet.SleepTimeStart.Value.TimeOfDay;
                sleepEnd = report.Carnet.SleepTimeEnd.Value.TimeOfDay;
            }

            var measures = (from d in _db.tblAbpTestRawData
                            where d.TestId == testId
                            orderby d.ReadingTime
                            select new Measurement
                                {
                                    Time = d.ReadingTime.Value,
                                    Systolic = d.Systolic.Value,
                                    Diastolic = d.Diastolic.Value,
                                    Middle = d.MAP.Value,
                                    HeartRate = d.HR.Value,
                                    Asleep = false,
                                    Valid = d.EventCode.HasValue && d.EventCode.Value == 0,
                                    IsEnabled = d.EventCode.HasValue && d.EventCode.Value == 0,
                                    Retry = d.ReadingCode == 2
                                }).ToList();

            foreach (var m in measures)
            {
                m.Asleep = sleepTimeValid && m.Time.HasValue
                           && IsAsleep(sleepStart, sleepEnd, m.Time.Value);
            }

            return measures.ToList();
        }

        private bool IsAsleep(TimeSpan start, TimeSpan end, DateTime time)
        {
            if (start < end)
                return start <= time.TimeOfDay && time.TimeOfDay <= end;
            else
                return time.TimeOfDay < end || start <= time.TimeOfDay;
        }

        public Patient GetPatient(string idPatient)
        {
            return null;
        }

        public ICollection<Patient> ListPatients()
        {
            return null;
        }

        public ICollection<PatientReport> ListAllReports()
        {
            using (_db = new ABPEntities())
            {
                var qry = from test in _db.tblAbpTest
                          join patient in _db.tblSysPatient on test.PatientId equals patient.PatientId
                          select new
                              {
                                  PatientName = patient.FirstName,
                                  PatientLastName = patient.LastName,
                                  PatientSecondLastname = patient.SecondLastName,
                                  PatientDocument = patient.MRN,
                                  ReportId = test.TestId,
                                  PatientId = patient.PatientId,
                                  ReportDevice = deviceId,
                                  ReportDate = test.HookupStartTime
                              };

                return (from q in qry.AsEnumerable()
                        select new PatientReport
                            {
                                PatientId = q.PatientId.ToString(),
                                PatientName = q.PatientName,
                                PatientLastName = q.PatientLastName + " " + q.PatientSecondLastname,
                                PatientDocument = q.PatientDocument,
                                ReportId = q.ReportId.ToString(),
                                ReportDate = q.ReportDate,
                                ReportDevice = q.ReportDevice,
                                ReportDeviceName = deviceName
                            }).ToList();
            }   
        }

        public ICollection<Report> GetReportsByPatientId(string patientId)
        {
            return null;
        }
    }
}
