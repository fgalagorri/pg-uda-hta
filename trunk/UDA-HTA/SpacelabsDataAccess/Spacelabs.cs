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
            Report r = null;

            using (_db = new ABPEntities())
            {
                var reportSl = _db.tblAbpTest.FirstOrDefault(d => idReport.Equals(d.TestId.ToString()));
                if (reportSl != null)
                {
                    r = new Report
                        {
                            // TODO: verque es IdPatient "reportSl.MRN"
                            IdPatient = reportSl.PatientId.ToString(),
                            DevReportId = reportSl.TestId.ToString(),
                            DeviceId = deviceId,
                            BeginDate = reportSl.HookupStartTime.HasValue? reportSl.HookupStartTime.Value : DateTime.Now,
                            EndDate = reportSl.HookupEndTime.HasValue? reportSl.HookupEndTime.Value : DateTime.Now
                        };
                }
            }

            return r;
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
