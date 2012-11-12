using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using HMSDataAccess;
using Interfaces;
using UdaHtaDataAccess;

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

            IDeviceDataAccess idataAccess = new UdaHta();
            idataAccess.connectToDataBase();
        }
    }
}
