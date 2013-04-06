using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;

namespace DeviceDataAccess
{
    public interface IDeviceDataAccess
    {
        void connectToDataBase();

        void closeConnectionDataBase();

        Report getReport(int idReport);

        ICollection<Patient> ListPatients();

        ICollection<PatientReport> ListAllReports();

        ICollection<Report> GetReportsByPatientId(int patientId);


    }
}
