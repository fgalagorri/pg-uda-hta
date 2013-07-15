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
                         where t.TestId == id && st.SummaryType == 1 && sd.SummaryType == 2 && sn.SummaryType == 3
                         select new
                             {
                                 t.TestId,
                                 t.HookupStartTime,
                                 t.HookupEndTime,

                                 SystolicTotalMin = st.SystolicMin,
                                 SystolicTotalMax = st.SystolicMax,
                                 SystolicTotalAvg = st.SystolicAvg,
                                 SystolicTotalMaxTime = st.SystolicMaxTime,
                                 SystolicTotalMinTime = st.SystolicMintime,
                                 DiastolicTotalMin = st.DiastolicMin,
                                 DiastolicTotalMax = st.DiastolicMax,
                                 DiastolicTotalAvg = st.DiastolicAvg,
                                 DiastolicTotalMaxTime = st.DiastolicMaxTime,
                                 DiastolicTotalMinTime = st.DiastolicMintime,
                                 HRTotalMin = st.HrMin,
                                 HRTotalMax = st.HrMax,
                                 HRTotalAvg = st.HrAvg,
                                 HRTotalMaxTime = st.HrMaxTime,
                                 HRTotalMinTime = st.HrMintime,

                                 SystolicDayMin = sd.SystolicMin,
                                 SystolicDayMax = sd.SystolicMax,
                                 SystolicDayAvg = sd.SystolicAvg,
                                 SystolicDayMaxTime = sd.SystolicMaxTime,
                                 SystolicDayMinTime = sd.SystolicMintime,
                                 DiastolicDayMin = sd.DiastolicMin,
                                 DiastolicDayMax = sd.DiastolicMax,
                                 DiastolicDayAvg = sd.DiastolicAvg,
                                 DiastolicDayMaxTime = sd.DiastolicMaxTime,
                                 DiastolicDayMinTime = sd.DiastolicMintime,
                                 HRDayMin = sd.HrMin,
                                 HRDayMax = sd.HrMax,
                                 HRDayAvg = sd.HrAvg,
                                 HRDayMaxTime = sd.HrMaxTime,
                                 HRDayMinTime = sd.HrMintime,

                                 SystolicNightMin = sn.SystolicMin,
                                 SystolicNightMax = sn.SystolicMax,
                                 SystolicNightAvg = sn.SystolicAvg,
                                 SystolicNightMaxTime = sn.SystolicMaxTime,
                                 SystolicNightMinTime = sn.SystolicMintime,
                                 DiastolicNightMin = sn.DiastolicMin,
                                 DiastolicNightMax = sn.DiastolicMax,
                                 DiastolicNightAvg = sn.DiastolicAvg,
                                 DiastolicNightMaxTime = sn.DiastolicMaxTime,
                                 DiastolicNightMinTime = sn.DiastolicMintime,
                                 HRNightMin = sn.HrMin,
                                 HRNightMax = sn.HrMax,
                                 HRNightAvg = sn.HrAvg,
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
                                 // Neighbourhood
                                 p.City,
                                 // Department
                                 p.DayPhone,
                                 p.PriEmailId
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

                            SystolicTotalAvg = r.SystolicTotalAvg,
                            SystolicTotalMax = r.SystolicTotalMax,
                            SystolicTotalMin = r.SystolicTotalMin,
                            SystolicTotalMaxTime = r.SystolicTotalMaxTime,
                            SystolicTotalMinTime = r.SystolicTotalMinTime,
                            DiastolicTotalAvg = r.DiastolicTotalAvg,
                            DiastolicTotalMax = r.DiastolicTotalMax,
                            DiastolicTotalMin = r.DiastolicTotalMin,
                            DiastolicTotalMaxTime = r.DiastolicTotalMaxTime,
                            DiastolicTotalMinTime = r.DiastolicTotalMinTime,
                            HeartRateTotalAvg = r.HRTotalAvg,
                            HeartRateTotalMax = r.HRTotalMax,
                            HeartRateTotalMin = r.HRTotalMin,
                            HeartRateTotalMaxTime = r.HRTotalMaxTime,
                            HeartRateTotalMinTime = r.HRTotalMinTime,

                            SystolicDayAvg = r.SystolicDayAvg,
                            SystolicDayMax = r.SystolicDayMax,
                            SystolicDayMin = r.SystolicDayMin,
                            SystolicDayMaxTime = r.SystolicDayMaxTime,
                            SystolicDayMinTime = r.SystolicDayMinTime,
                            DiastolicDayAvg = r.DiastolicDayAvg,
                            DiastolicDayMax = r.DiastolicDayMax,
                            DiastolicDayMin = r.DiastolicDayMin,
                            DiastolicDayMaxTime = r.DiastolicDayMaxTime,
                            DiastolicDayMinTime = r.DiastolicDayMinTime,
                            HeartRateDayAvg = r.HRDayAvg,
                            HeartRateDayMax = r.HRDayMax,
                            HeartRateDayMin = r.HRDayMin,
                            HeartRateDayMaxTime = r.HRDayMaxTime,
                            HeartRateDayMinTime = r.HRDayMinTime,

                            SystolicNightAvg = r.SystolicNightAvg,
                            SystolicNightMax = r.SystolicNightMax,
                            SystolicNightMin = r.SystolicNightMin,
                            SystolicNightMaxTime = r.SystolicNightMaxTime,
                            SystolicNightMinTime = r.SystolicNightMinTime,
                            DiastolicNightAvg = r.DiastolicNightAvg,
                            DiastolicNightMax = r.DiastolicNightMax,
                            DiastolicNightMin = r.DiastolicNightMin,
                            DiastolicNightMaxTime = r.DiastolicNightMaxTime,
                            DiastolicNightMinTime = r.DiastolicNightMinTime,
                            HeartRateNightAvg = r.HRNightAvg,
                            HeartRateNightMax = r.HRNightMax,
                            HeartRateNightMin = r.HRNightMin,
                            HeartRateNightMaxTime = r.HRNightMaxTime,
                            HeartRateNightMinTime = r.HRNightMinTime,
                        };

                    report.Patient.DeviceReferences.Add(new DeviceReference(deviceId,r.PatientId.ToString()));
                }
            }

            return report;
        }

        public List<Measurement> GetMeasures(Report report)
        {
            Guid testId = Guid.Parse(report.DeviceReportId);

            var measures = from d in _db.tblAbpTestRawData
                           where d.TestId == testId
                           orderby d.ReadingTime
                           select new Measurement
                               {
                                   Time = d.ReadingTime.Value,
                                   Systolic = d.Systolic.Value,
                                   Diastolic = d.Diastolic.Value,
                                   Middle = d.MAP.Value,
                                   HeartRate = d.HR.Value,
                                   Asleep = (report.Carnet.SleepTimeStart <= d.ReadingTime &&
                                             report.Carnet.SleepTimeEnd >= d.ReadingTime),
                                   Valid = d.EventCode.HasValue && d.EventCode.Value == 0,
                                   Retry = d.ReadingCode == 2
                               };

            return measures.ToList();
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
                                  PatientName = test.FirstName,
                                  PatientLastName = test.LastName,
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
                                PatientLastName = q.PatientLastName,
                                PatientDocument = q.PatientDocument,
                                ReportId = q.ReportId.ToString(),
                                ReportDate = q.ReportDate,
                                ReportDevice = q.ReportDevice
                            }).ToList();
            }   
        }

        public ICollection<Report> ListAllPendingReports()
        {
            return null;
        }

        public ICollection<Report> GetReportsByPatientId(string patientId)
        {
            return null;
        }


    }
}
