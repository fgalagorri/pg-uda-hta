using System.Collections.Generic;
using Entities;

namespace DeviceDataAccess
{
    public class DeviceController
    {
        private IDeviceDataAccess _deviceType; 

        public DeviceController(IDeviceDataAccess devTyp)
        {
            _deviceType = devTyp;
            ConnectDeviceDataAccess();
        }

        private void ConnectDeviceDataAccess()
        {
            _deviceType.ConnectToDataBase();
        }

        public void CloseDeviceDataAccess()
        {
            _deviceType.CloseConnectionDataBase();
        }

        public Report GetReport(string idReport)
        {
            return _deviceType.GetReport(idReport);
        }

        public List<Measurement> GetMeasures(Report report)
        {
            return _deviceType.GetMeasures(report);
        }

        public Patient GetPatient(string idPatient)
        {
            return _deviceType.GetPatient(idPatient);
        }

        public ICollection<Patient> ListPatientsDeviceDataAccess()
        {
            return _deviceType.ListPatients();
        }

        public ICollection<PatientReport> ListAllReportsDeviceDataAccess()
        {
            return _deviceType.ListAllReports();
        }

        public ICollection<Report> GetReportsByPatientIdDDA(string patientId)
        {
            return _deviceType.GetReportsByPatientId(patientId);
        }
    }
}
