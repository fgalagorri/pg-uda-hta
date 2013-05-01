using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceBussinessLogic;
using Entities;
using DeviceDataAccess;
using HMSDataAccess;
using SpacelabsDataAccess;

namespace BussinessLogic
{
    public class ImportDataManagement : IImportDataManagement
    {

        public ICollection<PatientReport> GetListNewPatientReports(DeviceController dda)
        {
            PatientReport node = new PatientReport();
            ICollection<PatientReport> list = new List<PatientReport>();

            // Obtiene una lista de PatientReport
            list = dda.ListAllReportsDeviceDataAccess(); 

            // Cierro la conexion con la base de datos
            dda.CloseDeviceDataAccess();

            return list;
        }

        public ICollection<PatientReport> ListNewPatientReports()
        {
            ICollection<PatientReport> list = new List<PatientReport>();
            ICollection<PatientReport> listSL = new List<PatientReport>();

            // Obtener lista de reportes perdientes para cada dispositivo
            DeviceController dda;
            
            //Lista de reportes pendientes de HMS
            dda = new DeviceController(new HMS());
            list = GetListNewPatientReports(dda);

            //Lista de reportes pendientes de spacelabs
            dda = new DeviceController(new Spacelabs());
            listSL = GetListNewPatientReports(dda);
            if (listSL != null)
            {
                list.Concat(listSL);
            }

            return list;
        }

        public Report ImportData(string idReport, int device)
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
