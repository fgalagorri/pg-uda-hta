using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;

namespace DeviceDataAccess
{
    public class DeviceDataAccess
    {
        private IDeviceDataAccess _deviceType; 

        public DeviceDataAccess(IDeviceDataAccess devTyp)
        {
            this._deviceType = devTyp;
        }

        public void ConnectDeviceDataAccess()
        {
            _deviceType.connectToDataBase();
        }

        public void CloseDeviceDataAccess()
        {
            _deviceType.closeConnectionDataBase();
        }

        public Report GetReport(int idReport)
        {
            return _deviceType.getReport(idReport);
        }

        public ICollection<Patient> ListPatientsDeviceDataAccess()
        {
            return _deviceType.ListPatients();
        }

        public ICollection<PatientReport> ListAllReportsDeviceDataAccess()
        {
            return _deviceType.ListAllReports();
        }

        public ICollection<Report> GetReportsByPatientIdDDA(int patientId)
        {
            return _deviceType.GetReportsByPatientId(patientId);
        }
    }
}
