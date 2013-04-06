using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeviceDataAccess;
using Entities;

namespace SpacelabsDataAccess
{
    public class Spacelabs : IDeviceDataAccess
    {
        public Spacelabs()
        {
        }

        public void connectToDataBase()
        {
        
        }

        public void closeConnectionDataBase()
        {

        }

        public Report getReport(int idReport)
        {
            return null;
        }

        public ICollection<Patient> ListPatients()
        {
            return null;
        }

        public ICollection<PatientReport> ListAllReports()
        {
            return null;
        }

        public ICollection<Report> ListAllPendingReports()
        {
            return null;
        }

        public ICollection<Report> GetReportsByPatientId(int patientId)
        {
            return null;
        }


    }
}
