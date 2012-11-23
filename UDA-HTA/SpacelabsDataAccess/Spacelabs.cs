using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeviceDataAccess;
using Interfaces;

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

        public ICollection<Patient> ListPatients()
        {
            return null;
        }

        public ICollection<Report> ListAllReports()
        {
            return null;
        }

        public ICollection<Report> GetReportsByPatientId(int patientId)
        {
            return null;
        }


    }
}
