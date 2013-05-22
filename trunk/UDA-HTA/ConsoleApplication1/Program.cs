using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using HMSDataAccess;
using Entities;
using DataAccess;
using BussinessLogic;
using InterfaceBussinessLogic;
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

            IImportDataManagement idm = new ImportDataManagement();
            IReportManagement rm = new ReportManagement();
            PatientManagement pm = new PatientManagement();

            ICollection<PatientReport> lpr = idm.ListNewPatientReports();
            foreach (PatientReport pr in lpr)
            {
/*                Console.Write(pr.PatientName);
                Console.Write(" , ");
                Console.Write(pr.PatientLastName);
                Console.Write(" , ");
                Console.Write(pr.PatientDocument);
                Console.Write(" , ");
                Console.Write(pr.ReportDate);
                Console.Write(" , ");
                Console.Write(pr.ReportDevice);
                Console.Write(" , ");
                Console.WriteLine(pr.ReportId);
 */

                Patient pat = new Patient();
                pat.DocumentId = pr.PatientDocument;
                pat.Name = pr.PatientName;
                pat.Surname = pr.PatientLastName;
                pat.IdInDevice = pr.PatientId.ToString();
                try
                {
                    var idPatient = pm.CreatePatient(pat);
                    DailyCarnet dailyCarnet = new DailyCarnet();
                    TemporaryData temporaryData = new TemporaryData();
                    Report rep = idm.ImportReport(pr.ReportId, pr.ReportDevice);
                    rm.addReport(rep, idPatient.ToString(), dailyCarnet, temporaryData);
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
