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
    class ImportDataManagement : IImportDataManagement
    {

        private ICollection<Report.PatientReport> getListNewPatientReports(DeviceDataAccess.DeviceDataAccess dda)
        {
            Entities.Report.PatientReport node = new Report.PatientReport();
            ICollection<Entities.Report.PatientReport> list = new List<Entities.Report.PatientReport>();

            // Abro conexion con la base de datos
            dda.ConnectDeviceDataAccess();

            /*
             * TODO: del lado de la base una rutina que obtenga una lista de PatientReport
             */
            ICollection<Report> l = dda.ListAllReportsDeviceDataAccess(); 

            // Cierro la conexion con la base de datos
            dda.CloseDeviceDataAccess();

            return list;
        }

        private ICollection<Report.PatientReport> listNewPatientReports()
        {
            ICollection<Entities.Report.PatientReport> list = new List<Entities.Report.PatientReport>();

            // Obtener lista de reportes perdientes para cada dispositivo
            DeviceDataAccess.DeviceDataAccess dda;
            
            dda = new DeviceDataAccess.DeviceDataAccess(new HMS());
            list = getListNewPatientReports(dda);

            dda = new DeviceDataAccess.DeviceDataAccess(new Spacelabs());
            list.Concat(getListNewPatientReports(dda));


            return list;
        }

        private Report importData(int idReport, int device)
        {
            Report report = null;

            DeviceDataAccess.DeviceDataAccess dda;
            switch (device)
            {
                case 0:
                    // El dispositivo es HMS
                    dda = new DeviceDataAccess.DeviceDataAccess(new HMS());
                    break;
                case 1:
                    // El dispositivo es Spacelabs
                    dda = new DeviceDataAccess.DeviceDataAccess(new Spacelabs());
                    break;
                default:
                    // Error
                    dda = null;
                    break;
            }

            if (dda != null) 
            {
                dda.ConnectDeviceDataAccess();

                report = dda.GetReport(idReport);

                dda.CloseDeviceDataAccess();
            }

            return report;
        }

    }
}
