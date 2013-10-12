using System.Collections.Generic;
using System.Linq;
using Entities;
using DataAccess;
using HMSDataAccess;
using SpacelabsDataAccess;
using DeviceDataAccess;

namespace BussinessLogic
{
    public class ImportDataManagement
    {

        private static ICollection<PatientReport> GetListNewPatientReports(DeviceDataAccess.DeviceController dda)
        {
            // Obtiene una lista de PatientReport
            var list = dda.ListAllReportsDeviceDataAccess();

            // Cierro la conexion con la base de datos
            dda.CloseDeviceDataAccess();

            return list;
        }

        public ICollection<PatientReport> ListNewPatientReports()
        {
            var list = new List<PatientReport>();

            // Lista de reportes pendientes de HMS
            var dda = new DeviceController(new HMS());
            list.AddRange(GetListNewPatientReports(dda));

            // Lista de reportes pendientes de spacelabs
            //dda = new DeviceController(new Spacelabs());
            //list.AddRange(GetListNewPatientReports(dda));

            var uda = new UdaHtaDataAccess();
            ICollection<PatientReport> listUda = uda.ListAllReports();

            list = (from l in list
                    where !(listUda.Any(a => a.ReportId.Equals(l.ReportId) && 
                                             a.ReportDevice.Equals(l.ReportDevice)))
                    select l).ToList();

            return list;
        }

        public Patient ImportPatient(string idPatient, int device)
        {
            var pat = new Patient();

            DeviceController dda;
            switch (device)
            {
                case 0:
                    // El dispositivo es HMS
                    dda = new DeviceController(new HMS());
                    break;
                case 1:
                    // El dispositivo es Spacelabs
                    dda = new DeviceController(new Spacelabs());
                    break;
                default:
                    // Error
                    dda = null;
                    break;
            }

            if (dda != null)
            {
                pat = dda.GetPatient(idPatient);

                dda.CloseDeviceDataAccess();
            }

            return pat;
        }

        public Report ImportReport(string idReport, int device)
        {
            Report report = null;

            DeviceController dda;
            switch (device)
            {
                case 0:
                    // El dispositivo es HMS
                    dda = new DeviceController(new HMS());
                    break;
                case 1:
                    // El dispositivo es Spacelabs
                    dda = new DeviceController(new Spacelabs());
                    break;
                default:
                    // Error
                    dda = null;
                    break;
            }

            if (dda != null) 
            {
                report = dda.GetReport(idReport);
                dda.CloseDeviceDataAccess();
            }

            return report;
        }

        public List<Measurement> ImportMeasures(Report report)
        {
            List<Measurement> lMeasures = null; 

            DeviceController dda;
            switch (report.DeviceId)
            {
                case 0:
                    // El dispositivo es HMS
                    dda = new DeviceController(new HMS());
                    break;
                case 1:
                    // El dispositivo es Spacelabs
                    dda = new DeviceController(new Spacelabs());
                    break;
                default:
                    // Error
                    dda = null;
                    break;
            }

            if (dda != null)
            {
                lMeasures = dda.GetMeasures(report);
                dda.CloseDeviceDataAccess();
            }

            return lMeasures;
        }
    }
}
