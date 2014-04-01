using System.Collections.Generic;
using Entities;

namespace DeviceDataAccess
{
    public interface IDeviceDataAccess
    {
        void ConnectToDataBase();
        void CloseConnectionDataBase();
        Report GetReport(string idReport);
        List<Measurement> GetMeasures(Report report);
        Patient GetPatient(string idPatient);
        ICollection<Patient> ListPatients();
        ICollection<PatientReport> ListAllReports();
        ICollection<Report> GetReportsByPatientId(string patientId);
    }
}
