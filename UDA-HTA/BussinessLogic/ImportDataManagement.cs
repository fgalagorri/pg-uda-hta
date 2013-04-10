﻿using System;
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

        public ICollection<PatientReport> getListNewPatientReports(DeviceDataAccess.DeviceDataAccess dda)
        {
            PatientReport node = new PatientReport();
            ICollection<PatientReport> list = new List<PatientReport>();

            // Abro conexion con la base de datos
            dda.ConnectDeviceDataAccess();

            // Obtiene una lista de PatientReport
            list = dda.ListAllReportsDeviceDataAccess(); 

            // Cierro la conexion con la base de datos
            dda.CloseDeviceDataAccess();

            return list;
        }

        public ICollection<PatientReport> listNewPatientReports()
        {
            ICollection<PatientReport> list = new List<PatientReport>();

            // Obtener lista de reportes perdientes para cada dispositivo
            DeviceDataAccess.DeviceDataAccess dda;
            
            //Lista de reportes pendientes de HMS
            dda = new DeviceDataAccess.DeviceDataAccess(new HMS());
            list = getListNewPatientReports(dda);

            //Lista de reportes pendientes de spacelabs
            dda = new DeviceDataAccess.DeviceDataAccess(new Spacelabs());
            list.Concat(getListNewPatientReports(dda));


            return list;
        }

        public Report importData(int idReport, int device)
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
