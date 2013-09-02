using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using HMSDataAccess;
using Entities;
using DataAccess;
using BussinessLogic;
using DeviceDataAccess;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            IDeviceDataAccess iDataAccess = new HMS();
            iDataAccess.connectToDataBase();
            ICollection<Patient> patientList = iDataAccess.ListPatients();
            
            StringBuilder sb = new StringBuilder();
            int i;

            foreach (Patient p in patientList)
            {
                i = 0;
                ICollection<Report> repList = iDataAccess.GetReportsByPatientId(p.IdHms);
                p.ReportList = repList;
                sb.Append(p.DocumentId);
                sb.Append(" | ");
                while ((repList != null) && (i < repList.Count))
                {
                    sb.Append(p.ReportList.ElementAt(i).Ident);
                    i++;
                    sb.Append(" | ");
                }
                sb.AppendLine(" . ");
            }

            using (StreamWriter outfile = new StreamWriter("patientOutFile.txt"))
            {
                outfile.Write(sb.ToString());
            }

            iDataAccess.closeConnectionDataBase();
            */

            //UdaHtaDataAccess dataAccess = new UdaHtaDataAccess();
            //dataAccess.connectToDataBase();
            //dataAccess.insertUser(3, "login", "pass", "rol");
            //dataAccess.insertDrugType(2, "type1");
            //dataAccess.insertDrug(1, "DrugName", 2);
            //dataAccess.insertInvestigation(1, "inv1", new DateTime());
            //string pswd = dataAccess.getPassword("pepe");
            //dataAccess.closeConnectionDataBase();
            //Console.WriteLine(pswd);

            //SessionManagement sm = new SessionManagement();
            //bool logged = sm.login("pepe","pas");
            //if (logged)
            //    Console.WriteLine("LOGUEADO :)");
            //else
            //    Console.WriteLine("NO SE PUDO LOGUEAR :(");

            /* 
             * Exportar PDF

            IReportManagement irm = new ReportManagement();
            irm.exportReportPDF(new Report(), "HelloWorld.pdf")
             */

            /* Importar datos e impactarlos en base
             * 
             */
                        ImportDataManagement idm = new ImportDataManagement();
                        ReportManagement rm = new ReportManagement();
                        PatientManagement pm = new PatientManagement();

                        ICollection<PatientReport> lpr = idm.ListNewPatientReports();
                        foreach (PatientReport pr in lpr)
                        {
                            try
                            {
                                Report rep = idm.ImportReport(pr.ReportId, pr.ReportDevice);
                                var idPatient = pm.GetPatientIdIfExist(
                                    rep.Patient.DeviceReferences.Where(r => r.deviceType == 0)
                                       .Select(r => r.deviceReferenceId)
                                       .First()
                                       .ToString(), 0);
                                if (idPatient == null)
                                {
                                    idPatient = pm.CreatePatient(rep.Patient);
                                }
                                rep.Patient.UdaId = idPatient;

                                List<Measurement> lMeasurements = idm.ImportMeasures(rep);
                                rep.Measures = rep.Measures.Concat(lMeasurements).ToList();
                                rm.AddReport(rep);
                            }
                            catch (Exception e)
                            {
                                if (e.InnerException.Message.Contains("Duplicate entry"))
                                {
                                }
                                else
                                {
                                    throw;    
                                }
                            }
                        }
            
            /*/
            


            /* 
             * Exportar reporte
             *
            ReportManagement rm = new ReportManagement();
            var rep = rm.GetReport(6);
            if (rep != null)
            {
                PatientManagement pm = new PatientManagement();
                var pat = pm.GetPatient((long)rep.Patient.UdaId);
                rep.Patient = pat;
                rep.Diagnosis = "Diagnostico bla\r\nblablablabla\r\nulala lalalala\r\nDr. Who";
            }
            
            string filepath = "C:\\Users\\Public\\Documents\\Proyecto\\Generated doc\\prueba.docx";
            rm.ExportReportDocx(rep, filepath);

            rm.ExportReportPDF("C:\\Users\\Public\\Documents\\Proyecto\\Generated doc\\prueba.docx", "C:\\Users\\Public\\Documents\\Proyecto\\Generated doc\\prueba.pdf");
            */

            /*
             * Exportar investigacion
             */
            /*
            ICollection<Report> reports = new List<Report>();
            int i = 0;
                
            while (i <= 10)
            {
                Report report = new Report();
                report.UdaId = i;
                report.BeginDate = new DateTime(2013,i+1,i+1);
                
                reports.Add(report);
                i++;
            }
            */
            /*
            ReportManagement rm = new ReportManagement();
            var rep = rm.GetReport(6);
            var rep2 = rm.GetReport(7);
            
            InvestigationManagement im = new InvestigationManagement();
            Investigation investigation = new Investigation(1,"investigacion 1",new DateTime());
            investigation.LReports.Add(rep);
            investigation.LReports.Add(rep2); 
            
            im.ExportInvestigation(investigation, "C:\\Users\\Public\\Documents\\Proyecto\\Generated doc\\planilla_investigacion.xlsm");
             */
             
            /*
            Patient p = new Patient();
            p.DocumentId = "11111211";
            p.Names = "pepe";
            p.Surnames = "perez";
            p.DeviceReferences.Add(new DeviceReference(0,"8"));

            PatientManagement pm = new PatientManagement();
            pm.CreatePatient(p);
             */

            /*
             * Insertar usuario
             */
            var um = new UserManagement();
            User usr = new User();
            usr.Login = "SysAdmin";
            var uc = new CriptographyManagement();
            usr.Password = uc.Sha256Encryipt("password");
            usr.Role = "Admin";
            um.CreateUser(usr);
            //*/
        }
    }
}
