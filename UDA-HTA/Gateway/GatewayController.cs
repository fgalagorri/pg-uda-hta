using System.Collections.Generic;
using BussinessLogic;
using Entities;
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

        public Report ImportReport(string idReport, int device)
        {
            IImportDataManagement importDataController = new ImportDataManagement();
            var report = importDataController.ImportReport(idReport, device);

            IPatientManagement patientController = new PatientManagement();
            var idPatient = patientController.getPatientIdIfExist(report.Patient.DevicePatientId);
            if (idPatient != null)
            {
                // El paciente ya fue creado en la base de UDA-HTA => traigo la informacion y la sustituyo
                report.Patient = patientController.getPatientData((long)idPatient);
            }

            return report;
        }

        public void AddImportedData(Report report, bool patientModified)
        {
            IReportManagement reportController = new ReportManagement();
            IPatientManagement patientController = new PatientManagement();
            IImportDataManagement importController = new ImportDataManagement();
            
            /*
             * Si report.UdaId != null, entonces el paciente ya fue creado
             * Si la fecha de modificacion del paciente es de hoy, actualizar
             * En caso de que el id fuera null, dar de alta el paciente
             */
            if (report.Patient.UdaId != null)
            {
                if (patientModified)
                    patientController.editPatient(report.Patient);
            }
            else
            {
                report.Patient.UdaId = patientController.createPatient(report.Patient);
            }

			report.Measures = importController.ImportMeasures(report);

            reportController.addReport(report);
        }

        public ICollection<Patient> ListPatients()
        {
            IPatientManagement patientController = new PatientManagement();
            return patientController.listPatients();
        }

        public ICollection<Report> GettReportsOfPatient(long patientId)
        {
            IReportManagement reportController = new ReportManagement();
            return reportController.listPatientReports(patientId);
        }
    }
}