using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public interface IDeviceDataAccess
    {

        void connectToDataBase();

        void closeConnectionDataBase();

        ICollection<Patient> ListPatients();

        ICollection<Report> ListAllReports();

        ICollection<Report> GetReportsByPatientId(int patientId);


    }
}
