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
            irm.exportReportPDF(new Report(), "HelloWorld.pdf");
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
                                Patient pat = idm.ImportPatient(pr.PatientId, pr.ReportDevice);
                                var idPatient = pm.CreatePatient(pat);
                                DailyCarnet dailyCarnet = new DailyCarnet();
                                TemporaryData temporaryData = new TemporaryData();
                                Report rep = idm.ImportReport(pr.ReportId, pr.ReportDevice);
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
         
            /*
            IImportDataManagement idm = new ImportDataManagement();
            Report rep = idm.ImportReport("7", 0); //HMS
            Console.WriteLine(rep.Ident.ToString());
            ICollection<Measurement> lm = rep.getMeasureList();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("HORA" + "," + "PROMEDIO" + "," + "DIASTOLICA" + "," + "SISTOLICA" + "," + "HR" + "," + "COMENTARIO");

            foreach (Measurement m in lm)
            {
                sb.AppendLine(m.Time + "," + m.Average + "," + m.Diastolic + "," + m.Systolic + "," + m.HeartRate + "," + m.Comment);
            }

            using (StreamWriter outfile = new StreamWriter("reportOutFile.txt"))
            {
                outfile.Write(sb.ToString());
            }
            */
        }
    }
}
