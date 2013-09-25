using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using BussinessLogic;
using Entities;
using EventLogger;

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
            try
            {
                return controller.ListNewPatientReports();
            }
            catch(Exception exception)
            {
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], exception.Message);
                throw new Exception("No ha sido posible obtener la lista de estudios sin importar.");
            }
        }

        public Report ImportReport(string idReport, int device)
        {
            var importDataController = new ImportDataManagement();
            try
            {
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
                        report.Patient = patientController.GetPatient((long)idPatient);
                    }
                }

                return report;
            }
            catch(Exception exception)
            {
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], exception.Message);
                throw new Exception("No ha sido posible importar los datos solicitados");
            }

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
            catch (Exception exception)
            {
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], exception.Message);
                throw new Exception("El paciente no pudo ser creado, por favor, intentelo nuevamente");
            }


            try
            {
                report.Measures = importController.ImportMeasures(report);
                _importedReport = reportController.AddReport(report);
            }
            catch (Exception exception)
            {
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], exception.Message);
                var e = new Exception("El reporte no pudo ser creado, por favor, intentelo nuevamente");
                throw e;
            }

        }

    #endregion


    #region Report Updating

        public DiagnosisEdited UpdateDiagnosis(long reportId, string diagnosis)
        {
            var de = new DiagnosisEdited
                {
                    ReportId = reportId,
                    Diagnosis = diagnosis,
                    Doctor = _loggedUser,
                    DiagnosisDate = DateTime.Now
                };

            var cont = new ReportManagement();
            try
            {
                cont.UpdateDiagnosis(de.ReportId, de.Diagnosis, de.DiagnosisDate, de.Doctor.Name);
            }
            catch(Exception exception)
            {
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], exception.Message);
                throw new Exception("No se ha podido actualizar el diagnóstico, por favor, inténtelo nuevamente");
            }

            return de;
        }

        public void UpdateMeasure(long measureId, bool enabled, string comment)
        {
            try
            {
                new ReportManagement().UpdateMeasureInformation(measureId, enabled, comment);
            }
            catch(Exception exception)
            {
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], exception.Message);
                throw new Exception("No ha sido posible actualizar la información");
            }
        }

    #endregion


    #region Report Exportation

        public void ExportToPdf(Report report,bool includePatientData, bool includeDiagnostic, bool includeProfile, bool includeGraphic, bool includeMeasures, string filePath)
        {
            try
            {
                if (!Directory.Exists("Temp"))
                    Directory.CreateDirectory("Temp");

                string tempFile = "Temp\\tempfile" + report.UdaId + ".uda";
                ReportManagement rm = new ReportManagement();
                rm.ExportReportDocx(report, includePatientData, includeDiagnostic, includeProfile, includeGraphic,
                                    includeMeasures, tempFile);

                rm.ExportReportPDF(tempFile, filePath);
                File.Delete(tempFile);
            }
            catch(Exception exception)
            {
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], exception.Message);
                throw new Exception("No se ha podido exportar el informe, por favor, inténtelo nuevamente");
            }
        }

        public void ExportToDocx(Report report, bool includePatientData, bool includeDiagnostic, bool includeProfile, bool includeGraphic, bool includeMeasures, string filePath)
        {
            ReportManagement rm = new ReportManagement();
            try
            {
                rm.ExportReportDocx(report, includePatientData, includeDiagnostic, includeProfile, includeGraphic,
                                    includeMeasures, filePath);
            }
            catch (Exception exception)
            {
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], exception.Message);
                throw new Exception("No se ha podido exportar el informe, por favor, inténtelo nuevamente");
            }
        }

    #endregion


    #region Report Listing
        public ICollection<Report> ListFilteredReports(int? patientLowerAge, int? patientUpperAge, DateTime? reportSinceDate,
            DateTime? reportUntilDate, bool? isSmoker, bool? isDiabetic, bool? isHypertense, bool? isDysplidemic)
        {
            var rm = new ReportManagement();
            try
            {
                return rm.ListFilteredReports(patientLowerAge, patientUpperAge, reportSinceDate, reportUntilDate,
                                              isSmoker, isDiabetic, isHypertense, isDysplidemic);
            }
            catch (Exception exception)
            {
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], exception.Message);
                throw new Exception("No se ha podido obtener la información solicitada");
            }
        } 
    #endregion


    #region Patient Listing & Viewing

        public ICollection<PatientSearch> ListPatients(string documentId, string names, string surnames,
                                                       DateTime? birthDate, string registerNo)
        {
            var patientController = new PatientManagement();
            try
            {
                return patientController.ListPatients(documentId, names, surnames, birthDate, registerNo);
            }
            catch (Exception exception)
            {
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], exception.Message);
                throw new Exception("No se ha podido obtener la información solicitada");
            }
        }

        public Patient GetPatient(long patientId)
        {
            var patientController = new PatientManagement();
            try
            {
                return patientController.GetPatient(patientId);
            }
            catch (Exception exception)
            {
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], exception.Message);
                throw new Exception("No se ha podido obtener la información solicitada");
            }
        }

        public Patient GetPatientFullView(long patientId)
        {
            var patientController = new PatientManagement();
            try
            {
                var patient = patientController.GetPatient(patientId);
                patient.LastTempData = patientController.GetLastTempData(patientId);

                var reportController = new ReportManagement();
                patient.ReportList = reportController.ListPatientReports(patientId);

                return patient;
            }
            catch (Exception exception)
            {
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], exception.Message);
                throw new Exception("No se ha podido obtener la información solicitada");
            }
        }

        public TemporaryData GetPatientLastTempData(long patientId)
        {
            var patientController = new PatientManagement();
            try
            {
                return patientController.GetLastTempData(patientId);
            }
            catch (Exception exception)
            {
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], exception.Message);
                throw new Exception("No se ha podido obtener la información solicitada");
            }
        }

        public ICollection<Report> GetReportsOfPatient(long patientId)
        {
            var reportController = new ReportManagement();
            try
            {
                return reportController.ListPatientReports(patientId);
            }
            catch (Exception exception)
            {
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], exception.Message);
                throw new Exception("No se ha podido obtener la información solicitada");
            }
        }

        public void CreatePatient(Patient patient)
        {
            var pm = new PatientManagement();
            try
            {
                pm.CreatePatient(patient);
            }
            catch (Exception exception)
            {
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], exception.Message);
                throw new Exception("No fue posible crear el paciente, por favor, inténtelo nuevamente");
            }
        }

        public void EditPatient(Patient patient)
        {
            var pm = new PatientManagement();
            try
            {
                pm.EditPatient(patient);
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], "El paciente "+ patient.Names + "(" + patient.RegisterNumber + ")" + " fue modificado por " + _loggedUser.Login);
            }
            catch (Exception exception)
            {
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], exception.Message);
                throw new Exception("No fue posible actualizar la información del paciente");
            }
        }

    #endregion


    #region Drug Management

        public void AddDrug(string type, string name, string active)
        {
            var rm = new ReportManagement();
            try
            {
                rm.AddDrug(type, active, name);
            }
            catch (Exception exception)
            {
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], exception.Message);
                throw new Exception("Error al intentar insertar la droga");
            }
        }

        public void EditDrug(int id, string type, string name, string active)
        {
            var rm = new ReportManagement();
            try
            {
                rm.EditDrug(id, type, name, active);
            }
            catch (Exception exception)
            {
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], exception.Message);
                throw new Exception("No ha sido posible actualizar la información");
            }
        }

        public ICollection<string> GetDrugTypes()
        {
            var rm = new ReportManagement();
            try
            {
                return rm.GetDrugTypes();
            }
            catch (Exception exception)
            {
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], exception.Message);
                throw new Exception("No se pudieron obtener los tipos de drogas");
            }
        } 


        public ICollection<Drug> GetDrugs(string type, string active, string name)
        {
            var rm = new ReportManagement();
            try
            {
                return rm.GetDrugs(type, active, name);
            }
            catch (Exception exception)
            {
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], exception.Message);
                throw new Exception("No se ha podido obtener la informaciòn solicitada");
            }
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
                
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], userName + " ha ingresado");
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
            try
            {
                var encryptedPswd = cm.Sha256Encryipt(password);
                usr.Password = encryptedPswd;

                um.CreateUser(usr);
            }
            catch (Exception exception)
            {
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], exception.Message);
                throw new Exception("No ha sido posible crear el usuario");
            }
        }

        public void ChangePassword(string oldPswd, string newPswd, string newPswdRepeat)
        {
            var sm = new SessionManagement();
            var cm = new CriptographyManagement();
            try
            {
                oldPswd = cm.Sha256Encryipt(oldPswd);
                newPswd = cm.Sha256Encryipt(newPswd);
                newPswdRepeat = cm.Sha256Encryipt(newPswdRepeat);
                if (newPswd == newPswdRepeat)
                {
                    sm.ChangePassword(_loggedUser.Login, oldPswd, newPswd);                    
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception exception)
            {
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], exception.Message);
                throw new Exception("Alguno de los campos ingresados no es correcto");
                
            }
        }

        public ICollection<User> ListUsers(string name, string role, string login)
        {
            var um = new UserManagement();
            try
            {
                return um.ListUsers(name, role, login);
            }
            catch (Exception exception)
            {
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], exception.Message);
                throw new Exception("No se ha podido obtener la información solicitada");
            }
        }
 
        public void EditUser(int id, string Name, string role, string login)
        {
            var um = new UserManagement();
            User u = new User();
            u.Id = id;
            u.Name = Name;
            u.Role = role;
            u.Login = login;
            try
            {
                um.EditUser(u);
            }
            catch (Exception exception)
            {
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], exception.Message);
                throw new Exception("No se ha podido actualizar la información");
            }
        }

    #endregion


    #region Investigation Listing & Viewing

        public ICollection<InvestigationSearch> ListInvestigations(int? id, string name, DateTime? creationDate)
        {
            var im = new InvestigationManagement();
            try
            {
                return im.ListInvestigations(id, name, creationDate);
            }
            catch (Exception exception)
            {
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], exception.Message);
                throw new Exception("No se ha podido obtener la información solicitada");
            }
        }

        public Investigation CreateInvestigation(string name, string comment, DateTime creationDate)
        {
            var im = new InvestigationManagement();
            try
            {
                return im.CreateInvestigation(name, creationDate, comment);
            }
            catch (Exception exception)
            {
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], exception.Message);
                throw new Exception("No ha sido posible crear la investigación");
            }
        }

        public Investigation GetInvestigation(int idInvestigation)
        {
            var im = new InvestigationManagement();
            try
            {
                return im.GetInvestigation(idInvestigation);
            }
            catch (Exception exception)
            {
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], exception.Message);
                throw new Exception("No se ha podido obtener la información solicitada");
            }
        }

        public void EditInvestigation(Investigation investigation)
        {
            var im = new InvestigationManagement();
            try
            {
                im.EditInvestigation(investigation);
            }
            catch (Exception exception)
            {
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], exception.Message);
                throw new Exception("No se ha podido actualizar la información");
            }
        }

        public void AddReportToInvestigation(long idReport, long idPatient, int idInvestigation)
        {
            var im = new InvestigationManagement();
            try
            {
                im.AddReportToInvestigation(idReport, idPatient, idInvestigation);
            }
            catch (Exception exception)
            {
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], exception.Message);
                throw new Exception("No se pudo agregar el estudio a la investigación");
            }
        }

        public void DeleteReportFromResearch(Report report, Investigation investigation)
        {
            var im = new InvestigationManagement();
            try
            {
                im.DeleteReportFromInvestigation(report, investigation.IdInvestigation);
            }
            catch (Exception exception)
            {
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], exception.Message);
                throw new Exception("No se ha podido eliminar el reporte de la investigación");
            }
        }

        public void ExportInvestigationXLS(Investigation investigation, string filepath)
        {
            var im = new InvestigationManagement();
            try
            {
                im.ExportInvestigation(investigation, filepath);
            }
            catch (Exception exception)
            {
                LogFileManagement el = new LogFileManagement();
                el.ErrorLog(ConfigurationManager.AppSettings["LogPath"], exception.Message);
                throw new Exception("No se ha podido exportar la información");
            }
        }

    #endregion

    }
}