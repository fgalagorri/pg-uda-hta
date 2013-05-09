using System.Collections.Generic;
using System.Linq;
using InterfaceBussinessLogic;
using Entities;
using DataAccess;
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
            ICollection<PatientReport> list = null;

            //Lista de reportes pendientes de HMS
            DeviceController dda = new DeviceController(new HMS());
            ICollection<PatientReport> listHms = GetListNewPatientReports(dda);

            //Lista de reportes pendientes de spacelabs
            //dda = new DeviceController(new Spacelabs());
            //ICollection<PatientReport> listSl = GetListNewPatientReports(dda);
            ICollection<PatientReport> listSl = null;

            UdaHtaDataAccess uda = new UdaHtaDataAccess();
            ICollection<PatientReport> listUda = uda.ListAllReports();

            if (listUda != null && (listHms != null || listSl != null))
            {
                if (listHms != null)
                {
                    var udaQuery = from rUda in listUda
                                    where rUda.ReportDevice == 0
                                    select rUda;

                    var hmsQuery = from rUda in udaQuery
                                   join rHms in listHms on rUda.ReportIdent equals rHms.ReportIdent
                                   select rHms;

                    listHms = listHms.Intersect(hmsQuery).ToList();
                }
                
                if (listSl != null)
                {
                    
                    var udaQuery = (from rUda in listUda
                                    where rUda.ReportDevice == 1
                                    select rUda).ToList();

                    var slQuery = from rUda in udaQuery
                                  join rSl in listSl on rUda.ReportIdent equals rSl.ReportIdent
                                  select rSl;

                    listSl = listSl.Intersect(slQuery).ToList();
                }

                if (listHms != null && listSl != null)
                    list = listHms.Concat(listSl).ToList();

            }

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
