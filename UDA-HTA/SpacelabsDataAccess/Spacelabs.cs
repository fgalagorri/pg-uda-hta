using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
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
            _db.Connection.Open();
        }

        public void CloseConnectionDataBase()
        {
            _db.Connection.Close();
        }

        public Report GetReport(string idReport)
        {
            return null;
        }

        public ICollection<Patient> ListPatients()
        {
            return null;
        }

        public ICollection<PatientReport> ListAllReports()
        {
            return (from test in _db.tblAbpTest
                    join patient in _db.tblSysPatient on test.PatientId equals patient.PatientId
                    select new PatientReport
                        {
                            PatientName = test.FirstName,
                            PatientLastName = test.LastName,
                            PatientDocument = patient.MRN,
                            ReportIdent = test.TestId.ToString(),
                            PatientIdent = patient.PatientId.ToString(),
                            ReportDevice = deviceId,
                            ReportDate = test.HookupStartTime ?? DateTime.MinValue
                        }).ToList();
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
