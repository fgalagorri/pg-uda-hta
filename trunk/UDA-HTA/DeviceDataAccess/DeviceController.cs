using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using Entities.Tools;

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

        public ToolsReport GetReport(string idReport)
        {
            return _deviceType.GetReport(idReport);
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
