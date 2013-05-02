using System.Collections.Generic;
using System.Linq;
using InterfaceBussinessLogic;
using Entities;
using HMSDataAccess;
using SpacelabsDataAccess;
using DeviceDataAccess;

namespace BussinessLogic
{
    public class ImportDataManagement : IImportDataManagement
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
            //Lista de reportes pendientes de HMS
            DeviceController dda = new DeviceController(new HMS());
            ICollection<PatientReport> list = GetListNewPatientReports(dda);

            //Lista de reportes pendientes de spacelabs
            dda = new DeviceController(new Spacelabs());
            ICollection<PatientReport> listSl = GetListNewPatientReports(dda);
            
            if (listSl != null)
                list = list.Concat(listSl).ToList();

            return list;
        }

        public Patient ImportPatient(string idPatient, int device)
        {
            var pat = new Patient();

            DeviceDataAccess.DeviceController dda;
            switch (device)
            {
                case 0:
                    // El dispositivo es HMS
                    dda = new DeviceDataAccess.DeviceController(new HMS());
                    break;
                case 1:
                    // El dispositivo es Spacelabs
                    dda = new DeviceDataAccess.DeviceController(new Spacelabs());
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

    }
}
