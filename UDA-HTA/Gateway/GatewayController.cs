using System.Collections.Generic;
using BussinessLogic;
using Entities;
using Entities.Tools;
using InterfaceBussinessLogic;

namespace Gateway
{
    public class GatewayController
    {
        /* Instance of the GatewayController */
        private static GatewayController _this;


        private GatewayController()
        {

        }


        public static GatewayController GetInstance()
        {
            return _this ?? (_this = new GatewayController());
        }


        public ICollection<PatientReport> GetNewReports()
        {
            IImportDataManagement controller = new ImportDataManagement();
            return controller.ListNewPatientReports();
        }

        public ToolsReport ImportReport(string idReport, int device)
        {
            IImportDataManagement controller = new ImportDataManagement();
            return controller.ImportReport(idReport, device);
        }
    }
}