using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using Entities.Tools;

namespace DeviceDataAccess
{
    public interface IDeviceDataAccess
    {
        void ConnectToDataBase();
        void CloseConnectionDataBase();
        ToolsReport GetReport(string idReport);
        List<ToolsMeasurement> GetMeasures(string idReport);
        Patient GetPatient(string idPatient);
        ICollection<Patient> ListPatients();
        ICollection<PatientReport> ListAllReports();
        ICollection<Report> GetReportsByPatientId(string patientId);
    }
}
