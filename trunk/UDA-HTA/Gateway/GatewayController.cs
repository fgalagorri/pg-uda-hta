using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BussinessLogic;
using Entities;

namespace Gateway
{
    public class GatewayController
    {
        /* Instance of the GatewayController */
        private static GatewayController _this;

        #region Session Variables

        private long _importedPatient;
        private long _importedReport;
        private User _loggedUser;

        #endregion


        #region Session Variables Methods

        public void GetLastInsertedReport(out long patientId, out long reportId)
        {
            patientId = _importedPatient;
            reportId = _importedReport;
        }

        #endregion


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
                                 .Where(r => r.deviceType == device)
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

            try
            {
                /*
                     * Si report.UdaId != null, entonces el paciente ya fue creado
                     * Si la fecha de modificacion del paciente es de hoy, actualizar
                     * En caso de que el id fuera null, dar de alta el paciente
                     */
                if (report.UdaId != null)
                {
                    if (patientModified)
                        patientController.EditPatient(report.Patient);
                }
                else
                {
                    //Creo el paciente
                    report.Patient.UdaId = patientController.CreatePatient(report.Patient);
                }   
                
                _importedPatient = report.Patient.UdaId.Value;
            }
            catch (Exception)
            {
                var e = new Exception("El paciente no pudo ser creado, por favor, intentelo nuevamente");
                throw e;
            }


            try
            {
                report.Measures = importController.ImportMeasures(report);
                _importedReport = reportController.AddReport(report);
            }
            catch (Exception)
            {
                var e = new Exception("El reporte no pudo ser creado, por favor, intentelo nuevamente");
                throw e;
            }

        }

        #endregion


        #region Report Updating

        public DiagnosisEdited UpdateDiagnosis(long reportId, string diagnosis)
        {
            // TODO - Ver el manejo de usuarios
            if (_loggedUser == null)
                _loggedUser = new User
                    {
                        Login = "fgalagorri",
                        Name = "Federico Galagorri",
                        Password = "1234567890",
                        Role = ""
                    };

            var de = new DiagnosisEdited
                {
                    ReportId = reportId,
                    Diagnosis = diagnosis,
                    Doctor = _loggedUser,
                    DiagnosisDate = DateTime.Now
                };

            var cont = new ReportManagement();
            cont.UpdateDiagnosis(de.ReportId, de.Diagnosis, de.DiagnosisDate, de.Doctor.Name);

            return de;
        }

        #endregion


        #region Report Exportation

        public void ExportToPdf(Report report, string filePath)
        {
            if (!Directory.Exists("Temp"))
                Directory.CreateDirectory("Temp");

            string tempFile = "Temp\\tempfile" + report.UdaId + ".uda";
            ReportManagement rm = new ReportManagement();
            rm.ExportReportDocx(report, tempFile);

            rm.ExportReportPDF(tempFile, filePath);
            File.Delete(tempFile);
        }

        public void ExportToDocx(Report report, string filePath)
        {
            ReportManagement rm = new ReportManagement();
            rm.ExportReportDocx(report, filePath);
        }

        #endregion


        #region Patient Listing & Viewing

        public ICollection<PatientSearch> ListPatients(string documentId, string names, string surnames,
                                                       DateTime? birthDate, string registerNo)
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


        #region Login Management

        public void Login()
        {
            _loggedUser = new User
            {
                Login = "fgalagorri",
                Name = "Federico Galagorri",
                Password = "1234567890",
                Role = ""
            };
        }

        #endregion

    }
}