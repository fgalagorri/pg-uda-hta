using System;
using System.Collections.Generic;
using System.Linq;
using BussinessLogic;
using Entities;

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


        #region Report Importation

        public ICollection<PatientReport> GetNewReports()
        {
            var controller = new ImportDataManagement();
            return controller.ListNewPatientReports();
        }

        public Report ImportReport(string idReport, int device)
        {
            var importDataController = new ImportDataManagement();
            var report = importDataController.ImportReport(idReport, device);

            string idRef = report.Patient.DeviceReferences
                                 .Where(r => r.deviceType == 0)
                                 .Select(r => r.deviceReferenceId)
                                 .FirstOrDefault();
            if (!String.IsNullOrWhiteSpace(idRef))
            {
                var patientController = new PatientManagement();
                var idPatient = patientController.GetPatientIdIfExist(idRef, device);
                report.Patient.UdaId = idPatient;
                if (idPatient != null)
                {
                    // El paciente ya fue creado en la base de UDA-HTA => traigo la informacion y la sustituyo
                    report.Patient = patientController.GetPatient((long) idPatient);
                }
            }

            return report;
        }

        public void AddImportedData(Report report, bool patientModified)
        {
            var reportController = new ReportManagement();
            var patientController = new PatientManagement();
            var importController = new ImportDataManagement();
            
            /*
             * Si report.UdaId != null, entonces el paciente ya fue creado
             * Si la fecha de modificacion del paciente es de hoy, actualizar
             * En caso de que el id fuera null, dar de alta el paciente
             */
            if (report.Patient.UdaId != null)
            {
                if (patientModified)
                    patientController.EditPatient(report.Patient);
            }
            else
            {
                report.Patient.UdaId = patientController.CreatePatient(report.Patient);
            }

			report.Measures = importController.ImportMeasures(report);

            reportController.AddReport(report);
        }

        #endregion


        #region Patient Listing & Viewing

        public ICollection<PatientSearch> ListPatients(string documentId, string names, string surnames,
                                                       DateTime? birthDate, long? registerNo)
        {
            var patientController = new PatientManagement();
            return patientController.ListPatients(documentId, names, surnames, birthDate, registerNo);
        }

        public Patient GetPatient(long patientId)
        {
            var patientController = new PatientManagement();
            return patientController.GetPatient(patientId);
        }

        public Patient GetPatientFullView(long patientId)
        {
            var patientController = new PatientManagement();
            var patient = patientController.GetPatient(patientId);
            patient.LastTempData = patientController.GetLastTempData(patientId);

            var reportController = new ReportManagement();
            patient.ReportList = reportController.ListPatientReports(patientId);

            return patient;
        }

        public TemporaryData GetPatientLastTempData(long patientId)
        {
            var patientController = new PatientManagement();
            return patientController.GetLastTempData(patientId);
        }

        public ICollection<Report> GetReportsOfPatient(long patientId)
        {
            var reportController = new ReportManagement();
            return reportController.ListPatientReports(patientId);
        }

        #endregion
    }
}