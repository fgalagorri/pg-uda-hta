using System;
using System.Collections.Generic;
using System.Linq;
using DeviceDataAccess;
using Entities;
using Entities.Tools;

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

        public ToolsReport GetReport(string idReport)
        {
            Guid id = Guid.Parse(idReport);
            ToolsReport report = null;

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
                                 s.DiastolicMin,
                                 s.DiastolicMax,
                                 s.DiastolicAvg,
                                 s.HrMin,
                                 s.HrMax,
                                 s.HrAvg,

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
                    report = new ToolsReport
                        {
                            ReportId = r.TestId.ToString(),
                            BeginDate = r.HookupStartTime,
                            EndDate = r.HookupEndTime,
                            
                            SystolicMin = r.SystolicMin,
                            SystolicMax = r.SystolicMax,
                            SystolicAvg = r.SystolicAvg,
                            DiastolicMin = r.DiastolicMin,
                            DiastolicMax = r.DiastolicMax,
                            DiastolicAvg = r.DiastolicAvg,
                            HeartRateMin = r.HrMin,
                            HeartRateMax = r.HrMax,
                            HeartRateAvg = r.HrAvg,
                            DeviceId = deviceId,
                            
                            Patient = new ToolsPatient
                                {
                                    PatientId = r.PatientId.ToString(),
                                    Names = r.FirstName,
                                    LastNames = r.Names,
                                    CI = r.CI,
                                    Birthday = r.BirthDate,
                                    Gender = (Sex?) r.GenderId,
                                    Address = r.Street,
                                    // Neighbourhood
                                    City = r.City,
                                    // Department
                                    Telephone = r.DayPhone,
                                    Email = r.PriEmailId
                                }
                        };
                }
            }

            return report;
        }

        public List<ToolsMeasurement> GetMeasures(string idReport)
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
