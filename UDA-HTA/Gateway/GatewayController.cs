﻿using System;
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
        private static Limits _limits;

    #endregion


    #region Session Variables Methods

        public void GetLastInsertedReport(out long patientId, out long reportId)
        {
            patientId = _importedPatient;
            reportId = _importedReport;
        }

        public Limits GetLimits()
        {
            return _limits;
        }

    #endregion


        private GatewayController()
        {
        }

        public static GatewayController GetInstance()
        {
            var rm = new ReportManagement();
            _limits = rm.GetLimits();
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
                if (report.Patient.UdaId != null)
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

        public void UpdateMeasure(long measureId, bool enabled, string comment)
        {
            new ReportManagement().UpdateMeasureInformation(measureId,enabled,comment);
        }

    #endregion


    #region Report Exportation

        public void ExportToPdf(Report report,bool includePatientData, bool includeDiagnostic, bool includeProfile, bool includeGraphic, bool includeMeasures, string filePath)
        {
            if (!Directory.Exists("Temp"))
                Directory.CreateDirectory("Temp");

            string tempFile = "Temp\\tempfile" + report.UdaId + ".uda";
            ReportManagement rm = new ReportManagement();
            rm.ExportReportDocx(report, includePatientData, includeDiagnostic, includeProfile, includeGraphic, includeMeasures, tempFile);

            rm.ExportReportPDF(tempFile, filePath);
            File.Delete(tempFile);
        }

        public void ExportToDocx(Report report, bool includePatientData, bool includeDiagnostic, bool includeProfile, bool includeGraphic, bool includeMeasures, string filePath)
        {
            ReportManagement rm = new ReportManagement();
            rm.ExportReportDocx(report, includePatientData, includeDiagnostic, includeProfile, includeGraphic, includeMeasures, filePath);
        }

    #endregion

    #region Report Listing
        public ICollection<Report> ListFilteredReports(int? patientLowerAge, int? patientUpperAge, DateTime? reportSinceDate,
            DateTime? reportUntilDate, bool? isSmoker, bool? isDiabetic, bool? isHypertense, bool? isDysplidemic)
        {
            var rm = new ReportManagement();
            return rm.ListFilteredReports(patientLowerAge, patientUpperAge, reportSinceDate, reportUntilDate, 
                                          isSmoker, isDiabetic, isHypertense, isDysplidemic);
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

        public void CreatePatient(Patient patient)
        {
            var pm = new PatientManagement();
            pm.CreatePatient(patient);
        }

        public void EditPatient(Patient patient)
        {
            var pm = new PatientManagement();
            pm.EditPatient(patient);
        }

    #endregion

    #region Drug Management
        public void AddDrug(string type, string name, string active)
        {
            var rm = new ReportManagement();
            rm.AddDrug(type, active, name);
        }

        public void EditDrug(int id, string type, string name, string active)
        {
            var rm = new ReportManagement();
            rm.EditDrug(id, type, name, active);
        }

        public ICollection<string> GetDrugTypes()
        {
            var rm = new ReportManagement();
            return rm.GetDrugTypes();
        } 


        public ICollection<Drug> GetDrugs(string type, string active, string name)
        {
            var rm = new ReportManagement();
            return rm.GetDrugs(type, active, name);
        } 

    #endregion


    #region Login Management

        public User Login(string userName, string pswd)
        {
            var cm = new CriptographyManagement();
            var encryptedPswd = cm.Sha256Encryipt(pswd);
            var sm = new SessionManagement();
            try
            {
                _loggedUser = sm.Login(userName, encryptedPswd);
            }
            catch (Exception)
            {
                // El usuario y/o password no son correctos
                var exception = new Exception("Nombre de usuario y/o contraseña no son correctos");
                throw exception;                
            }

            return _loggedUser;
        }

    #endregion

    #region User Management

        public void CreateUser(string userName, string login, string role, string password)
        {
            var um = new UserManagement();
            User usr = new User();
            usr.Login = login;
            usr.Name = userName;
            usr.Role = role;

            var cm = new CriptographyManagement();
            var encryptedPswd = cm.Sha256Encryipt(password);
            usr.Password = encryptedPswd;
            
            um.CreateUser(usr);
        }

        public void ChangePassword(string login, string oldPswd, string newPswd)
        {
            var sm = new SessionManagement();
            var cm = new CriptographyManagement();
            oldPswd = cm.Sha256Encryipt(oldPswd);
            newPswd = cm.Sha256Encryipt(newPswd);
            try
            {
                sm.ChangePassword(login, oldPswd, newPswd);
            }
            catch (Exception)
            {
                var exception = new Exception("Alguno de los campos ingresados no son correctos");
                throw exception;
            }
        }

        public ICollection<User> ListUsers(string name, string role, string login)
        {
            var um = new UserManagement();
            return um.ListUsers(name, role, login);
        }
 
        public void EditUser(int id, string Name, string role, string login)
        {
            var um = new UserManagement();
            User u = new User();
            u.Id = id;
            u.Name = Name;
            u.Role = role;
            u.Login = login;
            um.EditUser(u);
        }

    #endregion

    #region Investigation Listing & Viewing

        public ICollection<InvestigationSearch> ListInvestigations(int? id, string name, DateTime? creationDate)
        {
            var im = new InvestigationManagement();
            return im.ListInvestigations(id, name, creationDate);
        }

        public Investigation CreateInvestigation(string name, string comment, DateTime creationDate)
        {
            var im = new InvestigationManagement();
            return im.CreateInvestigation(name, creationDate, comment);
        }

        public Investigation GetInvestigation(int idInvestigation)
        {
            var im = new InvestigationManagement();
            return im.GetInvestigation(idInvestigation);
        }

        public void EditInvestigation(Investigation investigation)
        {
            var im = new InvestigationManagement();
            im.EditInvestigation(investigation);
        }

        public void AddReportToInvestigation(long idReport, long idPatient, int idInvestigation)
        {
            var im = new InvestigationManagement();
            im.AddReportToInvestigation(idReport, idPatient, idInvestigation);
        }

        public void DeleteReportFromResearch(Report report, Investigation investigation)
        {
            var im = new InvestigationManagement();
            im.DeleteReportFromInvestigation(report, investigation.IdInvestigation);
        }

        public void ExportInvestigationXLS(Investigation investigation, string filepath)
        {
            var im = new InvestigationManagement();
            im.ExportInvestigation(investigation,filepath);
        }

    #endregion

    }
}