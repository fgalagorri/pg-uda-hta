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
                         join s in _db.tblAbpTestSummaryResults on t.TestId equals s.TestId
                         join p in _db.tblSysPatient on t.PatientId equals p.PatientId
                         where t.TestId == id
                         select new
                             {
                                 t.TestId,
                                 t.HookupStartTime,
                                 t.HookupEndTime,

                                 s.SystolicMin,
                                 s.SystolicMax,
                                 s.SystolicAvg,
                                 s.SystolicMaxTime,
                                 s.SystolicMintime,
                                 s.DiastolicMin,
                                 s.DiastolicMax,
                                 s.DiastolicAvg,
                                 s.DiastolicMaxTime,
                                 s.DiastolicMintime,
                                 s.HrMin,
                                 s.HrMax,
                                 s.HrAvg,
                                 s.HrMaxTime,
                                 s.HrMintime,

                                 p.PatientId,
                                 p.FirstName,
                                 Names = p.LastName + " " + p.SecondLastName,
                                 CI = p.MRN,
                                 p.BirthDate,
                                 p.GenderId,
                                 Street = p.Street1 + " " + p.Street2,
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
                                    DevicePatientId = r.PatientId.ToString(),
                                    Names = r.FirstName,
                                    Surnames = r.Names,
                                    DocumentId = r.CI,
                                    BirthDate = r.BirthDate,
                                    Sex = (SexType?) r.GenderId,
                                    Address = r.Street,
                                    // Neighbourhood
                                    City = r.City,
                                    // Department
                                    Phone = r.DayPhone,
                                    Email = r.PriEmailId,
                                    DeviceId = deviceId
                                },

                            SystolicTotalAvg = r.SystolicAvg,
                            SystolicTotalMax = r.SystolicMax,
                            SystolicTotalMin = r.SystolicMin,
                            SystolicTotalMaxTime = r.SystolicMaxTime,
                            SystolicTotalMinTime = r.SystolicMintime,

                            DiastolicTotalAvg = r.DiastolicAvg,
                            DiastolicTotalMax = r.DiastolicMax,
                            DiastolicTotalMin = r.DiastolicMin,
                            DiastolicTotalMaxTime = r.DiastolicMaxTime,
                            DiastolicTotalMinTime = r.DiastolicMintime,

                            HeartRateAvg = r.HrAvg,
                            HeartRateMax = r.HrMax,
                            HeartRateMin = r.HrMin,
                            HeartRateMaxTime = r.HrMaxTime,
                            HeartRateMinTime = r.HrMintime
                        };
                }
            }

            return report;
        }

        public List<Measurement> GetMeasures(string idReport)
        {
            throw new NotImplementedException();
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
